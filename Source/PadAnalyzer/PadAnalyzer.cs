using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace PadAnalyzer
{
    public partial class PadAnalyzer : Form
    {
        public PadAnalyzer()
        {
            InitializeComponent();
            
            ResetDataTable();
            CreateDataTableColumns(m_table, TableViewTypes.ClassFieldData);
            
            string[] arguments = Environment.GetCommandLineArgs();

            if (arguments.Length == 2)
            {
                String path = arguments[1];
                currentFileName = System.IO.Path.GetFileName(path);
                this.Text = "Pad Analyzer: Loading " + currentFileName;
                bgWorker.RunWorkerAsync(path);
            }
        }

        enum TableViewTypes
        {
            NoneType = 0,
            ClassFieldData,
            ClassStaticData,
            GlobalStaticData
        };

        class PaddingCalculation
        {
            private SymbolInfo typeSymbol = null;

            private SymbolInfo currentChild = null;
            private int currentIndex = 0;
            private int nextChildOffset = 0;
            private int unnamedUnionStartIndex = -1;

            private List<Tuple<int, int>> paddingList = new List<Tuple<int, int>>();

            /// <summary>
            /// Update Child State with i child
            /// </summary>
            /// <param name="i"></param>
            private void UpdateChildState(int i)
            {
                currentIndex = i;

                currentChild = typeSymbol.m_children[currentIndex];
                nextChildOffset = typeSymbol.m_children[i + 1].m_offset;
            }

            /// <summary>
            /// Sets the final child and the next child offset as type size
            /// </summary>
            private void SetFinalState()
            {
                currentIndex = typeSymbol.m_children.Count - 1;

                currentChild = typeSymbol.m_children[currentIndex];
                nextChildOffset = (int)typeSymbol.m_size;
            }

            /// <summary>
            /// Calculate padding for base class
            /// </summary>
            private void CalculatePaddingForBaseClass()
            {
                int realChildSize = nextChildOffset - currentChild.m_offset;

                if (realChildSize <= currentChild.m_size)
                {
                    currentChild.m_size = realChildSize;
                    currentChild.m_padding = 0;
                }
                else
                {
                    currentChild.m_padding = realChildSize - currentChild.m_size;
                }

                typeSymbol.m_padding += currentChild.m_padding;
            }

            /// <summary>
            /// The class for compare padding tupples
            /// </summary>
            private class PaddingOffsetComparer : IComparer<Tuple<int, int>>
            {
                public int Compare(Tuple<int, int> x, Tuple<int, int> y)
                {
                    if (x.Item1 > y.Item1)
                    {
                        return 1;
                    }

                    return -1;                    
                }
            }

            /// <summary>
            /// Calculated padding of current child
            /// </summary>
            /// <param name="info"></param>
            /// <param name="currentChild"></param>
            private void CalculateChildPadding()
            {
                if (currentChild.IsBase())
                {
                    CalculatePaddingForBaseClass();
                }
                else
                {
                    if (currentChild.m_offset >= nextChildOffset)
                    {
                        if (unnamedUnionStartIndex == -1)
                        {
                            unnamedUnionStartIndex = currentIndex;
                        }
                    }
                    else
                    {
                        currentChild.m_padding = (nextChildOffset - currentChild.m_offset) - currentChild.m_size;

                        if (currentChild.m_padding > 0)
                        {
                            int paddingStartOffset = currentChild.m_offset + currentChild.m_size;

                            paddingList.Add(Tuple.Create(paddingStartOffset, paddingStartOffset + currentChild.m_padding));
                        }
                    }
                }
            }

            /// <summary>
            /// Calculates the padding of typeSymbol
            /// </summary>
            private void CorrectPaddingCalculation()
            {
                if (unnamedUnionStartIndex != -1)
                {
                    PaddingOffsetComparer paddingOffsetComparer = new PaddingOffsetComparer();
                    paddingList.Sort(paddingOffsetComparer);

                    int offsetIndex;
                    Tuple<int, int> dymmySearchObject = null;
                    int boundFieldOffset;

                    for (int i = unnamedUnionStartIndex; i < typeSymbol.m_children.Count; ++i)
                    {
                        dymmySearchObject = Tuple.Create(typeSymbol.m_children[i].m_offset, 0);

                        offsetIndex = paddingList.BinarySearch(dymmySearchObject, paddingOffsetComparer);
                        offsetIndex = (offsetIndex < 0) ? ~offsetIndex : offsetIndex;

                        boundFieldOffset = typeSymbol.m_children[i].m_offset + typeSymbol.m_children[i].m_size;
                        int cnt = paddingList.Count;

                        for (int j = offsetIndex; j < cnt; ++j)
                        {
                            if (paddingList[j].Item2 < boundFieldOffset)
                            {
                                paddingList.RemoveAt(j);
                                cnt--;
                            }
                            else
                            {
                                if (paddingList[j].Item1 < boundFieldOffset)
                                {
                                    paddingList[j] = Tuple.Create(boundFieldOffset, paddingList[j].Item2);
                                }
                            }
                        }

                    }
                }

                for (int i = 0; i < paddingList.Count; ++i)
                {
                    typeSymbol.m_padding += (paddingList[i].Item2 - paddingList[i].Item1);
                }
            }

            /// <summary>
            /// Initialize the PaddingCalculation class with symbol type
            /// </summary>
            /// <param name="inSym"></param>
            public void Initialize(SymbolInfo inSym)
            {
                typeSymbol = inSym;
                currentIndex = 0;
            }

            /// <summary>
            /// Runs the paddings calculations for typeSymbol children
            /// </summary>
            public void Run()
            {
                int lastChildIndex = typeSymbol.m_children.Count - 1;

                for (int i = 0; i < lastChildIndex; ++i)
                {
                    UpdateChildState(i);
                    CalculateChildPadding();
                }

                SetFinalState();
                CalculateChildPadding();

                CorrectPaddingCalculation();
            }

        }

        Dictionary<string, TableViewTypes> dictViewTableTypes =
            new Dictionary<string, TableViewTypes>()
        {
            {"Class field data", TableViewTypes.ClassFieldData},
            {"Class static field data", TableViewTypes.ClassStaticData},
            {"Global static data", TableViewTypes.GlobalStaticData}
        };

        class SymbolInfo
        {
            public bool HasChildren() { return m_children != null; }
            public void AddChild(SymbolInfo child)
            {
                if (m_children == null)
                {
                    m_children = new List<SymbolInfo>();
                }

                m_children.Add(child);
            }

            public bool IsBase()
            {
                return m_name.IndexOf("Base: ") == 0;
            }

            public static int CompareOffsets(SymbolInfo x, SymbolInfo y)
            {
                // Base classes have to go first.
                if (x.IsBase() && !y.IsBase())
                    return -1;
                if (!x.IsBase() && y.IsBase())
                    return 1;

                return (x.m_offset == y.m_offset) ? 0 :
                    (x.m_offset < y.m_offset) ? -1 : 1;
            }

            public string m_name;
            public uint m_type_id;
            public string m_type_name;

            public int m_offset;
            public int m_size;
            public int m_padding = 0;

            public List<SymbolInfo> m_children;
        };

        CsDebugScript.Engine.ISymbolProviderModule sym;

        readonly string[] baseTypes = { "none", "void", "char", "wchar", "4", "5", "int", "uint", "float", "BCD", "bool", "11", "12", "long", "ulong" };

        Dictionary<string, SymbolInfo> m_symbols = new Dictionary<string, SymbolInfo>();

        DataTable m_table = null;

        long m_prefetchStartOffset = 0;

        String currentFileName;

        /// <summary>
        /// Function to calculate tables column width
        /// </summary>
        private void SetColumnSizeEquel(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridSymbols.Columns.Count; ++i)
            {
                dataGridSymbols.Columns[i].Width = dataGridSymbols.Width / dataGridSymbols.Columns.Count;
            }

            for (int i = 0; i < dataGridViewSymbolInfo.Columns.Count; ++i)
            {
                dataGridViewSymbolInfo.Columns[i].Width = dataGridViewSymbolInfo.Width / dataGridViewSymbolInfo.Columns.Count;
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadPDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsDataTableBusy())
            {
                MessageBox.Show("Cannot load new file. Table is still processing.");
                return;
            }

            if (openPdbDialog.ShowDialog() == DialogResult.OK)
            {
                currentFileName = System.IO.Path.GetFileName(openPdbDialog.FileName);

                // Prepare GUI
                this.textBoxFilter.Text = "";
                progressBar.Value = 0;
                this.Text = "Pad Analyzer: Loading " + currentFileName;
                this.tablePresentationComboBox.Enabled = false;
                this.tablePresentationComboBox.Text = "Class field data";

                ResetDataTable();
                CreateDataTableColumns(m_table, TableViewTypes.ClassFieldData);

                bgWorker.RunWorkerAsync(openPdbDialog.FileName);
            }
        }

        private void CreateDataTableColumns(DataTable table, TableViewTypes tableType)
        {
            Dictionary<string, Type> columnNames = new Dictionary<string, Type>();
            
            if (tableType == TableViewTypes.ClassFieldData)
            {
                columnNames["Symbol"] = Type.GetType("System.String");
                columnNames["Size"] = Type.GetType("System.Int32");
                columnNames["Padding"] = Type.GetType("System.Int32");
                columnNames["Padding/Size"] = Type.GetType("System.Double");                
            }
            else if (tableType == TableViewTypes.ClassStaticData || tableType == TableViewTypes.GlobalStaticData)
            {
                columnNames["Symbol"] = Type.GetType("System.String");
                columnNames["Size"] = Type.GetType("System.Int32");
                columnNames["Type"] = Type.GetType("System.String");
                columnNames["Object file"] = Type.GetType("System.String");
            }
            else
            {
                throw new ArgumentException("Not correct parameter", nameof(tableType));
            }

            foreach (string columnName in columnNames.Keys)
            {
                DataColumn column = new DataColumn()
                {
                    ColumnName = columnName,
                    DataType = columnNames[columnName],
                    ReadOnly = true
                };

                table.Columns.Add(column);
            }
        }

        private void PopulateDataTable(string fileName)
        {
            m_symbols.Clear();

            if (fileName.ToLower().EndsWith(".pdb"))
            {
                CsDebugScript.Engine.SymbolProviders.DiaSymbolProvider dw = new CsDebugScript.Engine.SymbolProviders.DiaSymbolProvider();
                sym = dw.LoadModule(fileName);
            }
            else
            {
                CsDebugScript.DwarfSymbolProvider.DwarfSymbolProvider dw = new CsDebugScript.DwarfSymbolProvider.DwarfSymbolProvider();
                sym = dw.LoadModule(fileName);
            }

            foreach (uint typeId in sym.GetAllTypes())
            {
                TryAddSymbol(typeId);
            }
        }

        private SymbolInfo TryAddSymbol(uint typeId)
        {
            uint size = sym.GetTypeSize(typeId);

            if (size > 0)
            {
                string typeName = sym.GetTypeName(typeId);

                if (!m_symbols.ContainsKey(typeName))
                {
                    SymbolInfo info = new SymbolInfo()
                    {
                        m_name = typeName,
                        m_type_id = typeId,
                        m_type_name = "",

                        m_offset = 0,
                        m_size = (int)size,
                        m_padding = 0,
                    };

                    m_symbols[info.m_name] = info;

                    ProcessChildren(info, typeId);

                    return info;
                }
                else
                {
                    return m_symbols[typeName];
                }
            }

            return null;
        }

        private ulong GetCacheLineSize()
        {
            return Convert.ToUInt32(textBoxCache.Text);
        }

        private void ProcessChildren(SymbolInfo info, uint typeId)
        {
            foreach (Tuple<uint, int> baseClass in sym.GetTypeDirectBaseClasses(typeId).Values)
            {
                if (ProcessBase(baseClass.Item1, baseClass.Item2, out SymbolInfo childInfo))
                {
                    info.AddChild(childInfo);
                }
            }

            if (info.HasChildren())
            {
                info.m_children.Sort(SymbolInfo.CompareOffsets);
            }

            foreach (string fieldName in sym.GetTypeFieldNames(typeId))
            {
                Tuple<uint, int> fieldTypeAndOffset = sym.GetTypeFieldTypeAndOffset(typeId, fieldName);

                if (ProcessChild(fieldName, fieldTypeAndOffset.Item1, fieldTypeAndOffset.Item2, out SymbolInfo childInfo))
                {
                    info.AddChild(childInfo);
                }
            }

            // Sort children by offset, recalc padding.
            // Sorting is not needed normally (for data fields), but sometimes base class order is wrong.
            // The padding value for union/other intergar type symbol will be space for next different offset value 
            if (info.HasChildren())
            {
                if (sym.GetTypeTag(info.m_type_id) == CsDebugScript.Engine.CodeTypeTag.Enum)
                {
                    foreach (SymbolInfo childInfo in info.m_children)
                    {
                        childInfo.m_padding = 0;
                    }

                    return;
                }

                PaddingCalculation processingSymbolState = new PaddingCalculation();

                processingSymbolState.Initialize(info);
                processingSymbolState.Run();
            }
        }

        private bool ProcessBase(uint typeId, int memOffset, out SymbolInfo info)
        {
            string typeName = sym.GetTypeName(typeId);
            uint typeSize = sym.GetTypeSize(typeId);

            info = new SymbolInfo()
            {
                m_name = "Base: " + typeName,
                m_type_id = typeId,
                m_size = (int)typeSize,
                m_offset = memOffset,
                m_type_name = typeName
            };

            SymbolInfo typeInfo = TryAddSymbol(typeId);

            if (typeInfo != null)
            {
                info.m_padding += typeInfo.m_padding;
            }

            return true;
        }

        private bool ProcessChild(string fieldName, uint typeId, int memOffset, out SymbolInfo info)
        {
            info = new SymbolInfo()
            {
                m_name = fieldName,
                m_type_id = typeId,
                m_type_name = sym.GetTypeName(typeId),

                m_offset = memOffset,
                m_size = (int)sym.GetTypeSize(typeId),
                m_padding = 0
            };

            TryAddSymbol(typeId);

            return true;
        }

        private SymbolInfo FindSelectedSymbolInfo()
        {
            if (dataGridSymbols.SelectedRows.Count == 0)
                return null;

            DataGridViewRow selectedRow = dataGridSymbols.SelectedRows[0];
            string symbolName = selectedRow.Cells[0].Value.ToString();

            SymbolInfo info = FindSymbolInfo(symbolName);
            return info;
        }

        private SymbolInfo FindSymbolInfo(string name)
        {
            SymbolInfo info;
            m_symbols.TryGetValue(name, out info);
            return info;
        }

        private void ResetDataTable()
        {
            if (m_table != null)
            {
                m_table.Rows.Clear();
                m_table.Columns.Clear();
            }

            m_table = new DataTable("Symbols");

            bindingSourceSymbols.DataSource = m_table;
        }

        private void DataGridSymbols_SelectionChanged(object sender, EventArgs e)
        {
            m_prefetchStartOffset = 0;
            ShowSelectedSymbolInfo();
        }

        void ShowSelectedSymbolInfo()
        {
            SymbolInfo info = FindSelectedSymbolInfo();

            if (info != null)
            {
                TableViewTypes selectedTableView = dictViewTableTypes[this.tablePresentationComboBox.Text];

                if (selectedTableView == TableViewTypes.ClassFieldData)
                {
                    ShowSymbolInfo(info);
                }
                else if (selectedTableView == TableViewTypes.ClassStaticData || selectedTableView == TableViewTypes.GlobalStaticData)
                {
                    ShowSymbolStaticInfo(info);
                }                
            }
        }

        void ShowSymbolInfo(SymbolInfo info)
        {
            dataGridViewSymbolInfo.Rows.Clear();

            if (!info.HasChildren())
            {
                return;
            }

            long cacheLineSize = (long)GetCacheLineSize();
            long prevCacheBoundaryOffset = m_prefetchStartOffset;

            long numCacheLines = 0;
            long numCacheLinesBetweenChildren = 0;
            long childEndOffset;
            long cacheLineBound = 0;
            long cacheLineOffset = 0;
            string intersectStatus = "";

            int paddingOffset = 0;

            foreach (SymbolInfo child in info.m_children)
            {
                // Present child info
                string[] row = { child.m_name, child.m_offset.ToString(), child.m_size.ToString(), child.m_type_name };
                dataGridViewSymbolInfo.Rows.Add(row);

                // Present child padding info
                if (child.m_padding > 0)
                {
                    paddingOffset = child.m_offset + child.m_size;

                    string[] paddingRow = { "Padding", paddingOffset.ToString(), child.m_padding.ToString(), "" };
                    dataGridViewSymbolInfo.Rows.Add(paddingRow);
                }

                if (cacheLineSize > 0)
                {
                    childEndOffset = child.m_offset + child.m_size + child.m_padding;

                    numCacheLinesBetweenChildren = (childEndOffset - prevCacheBoundaryOffset) / cacheLineSize;
                    numCacheLines += numCacheLinesBetweenChildren;

                    if (numCacheLinesBetweenChildren > 0)
                    {
                        cacheLineBound = numCacheLines * cacheLineSize;

                        if (cacheLineBound != childEndOffset)
                        {
                            intersectStatus = string.Format("Intesects cacheline: {0}", child.m_name);
                        }
                        else
                        {
                            intersectStatus = "";
                        }

                        cacheLineOffset = m_prefetchStartOffset + numCacheLines * cacheLineSize;

                        string cacheInfo = string.Format("{0}x{1}", numCacheLinesBetweenChildren, cacheLineSize);
                        string[] boundaryRow = { "Cacheline boundary", cacheLineOffset.ToString(), cacheInfo, intersectStatus };
                        dataGridViewSymbolInfo.Rows.Add(boundaryRow);

                        prevCacheBoundaryOffset = cacheLineOffset;
                    }
                }
            }
        }

        void ShowSymbolStaticInfo(SymbolInfo info)
        {
            // Nothing to show at the moment
            dataGridViewSymbolInfo.Rows.Clear();            
        }

        private void DataGridSymbols_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Index > 0)
            {
                e.Handled = true;
                e.SortResult = Int32.Parse(e.CellValue1.ToString()) - Int32.Parse(e.CellValue2.ToString());
            }
        }

        private void DataGridViewSymbolInfo_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewSymbolInfo.Rows)
            {
                DataGridViewCell cell = row.Cells[0];

                if (cell.Value.ToString().StartsWith("Padding"))
                {
                    cell.Style.BackColor = Color.LightGray;
                    row.Cells[1].Style.BackColor = Color.LightGray;
                    row.Cells[2].Style.BackColor = Color.LightGray;
                    row.Cells[3].Style.BackColor = Color.LightGray;
                }
                else if (cell.Value.ToString().IndexOf("Base: ") == 0)
                {
                    cell.Style.BackColor = Color.LightGreen;
                    row.Cells[1].Style.BackColor = Color.LightGreen;
                    row.Cells[2].Style.BackColor = Color.LightGreen;
                    row.Cells[3].Style.BackColor = Color.LightGreen;
                }
                else if (cell.Value.ToString().IndexOf("Cacheline boundary") >= 0)
                {
                    DataGridViewCell cellType = row.Cells[3];

                    if (cellType.Value.ToString().IndexOf("Intesects cacheline") == 0)
                    {
                        cell.Style.BackColor = Color.OrangeRed;
                        row.Cells[1].Style.BackColor = Color.OrangeRed;
                        row.Cells[2].Style.BackColor = Color.OrangeRed;
                        row.Cells[3].Style.BackColor = Color.OrangeRed;
                    }
                    else
                    {
                        cell.Style.BackColor = Color.LightPink;
                        row.Cells[1].Style.BackColor = Color.LightPink;
                        row.Cells[2].Style.BackColor = Color.LightPink;
                        row.Cells[3].Style.BackColor = Color.LightPink;
                    }
                }
            }
        }

        private void UseFilter(object sender, EventArgs e)
        {
            if (textBoxFilter.Text.Length == 0)
            {
                bindingSourceSymbols.Filter = null;
            }
            else
            {
                string filterToUse = null;

                if (exactSearch.Checked)
                {
                    filterToUse = "Symbol = " + '\'' +  textBoxFilter.Text + '\'';
                }
                else
                {
                    filterToUse = "Symbol LIKE '*" + textBoxFilter.Text + "*'";
                }
                    
                bindingSourceSymbols.Filter = filterToUse;
            }
        }

        private void DumpSymbolInfo(System.IO.TextWriter tw, SymbolInfo info)
        {
            tw.WriteLine("Symbol: " + info.m_name);
            tw.WriteLine("Size: " + info.m_size.ToString());
            tw.WriteLine("Total padding: " + info.m_padding.ToString());
            tw.WriteLine("Members");
            tw.WriteLine("-------");

            foreach (SymbolInfo child in info.m_children)
            {
                if (child.m_padding > 0)
                {
                    long paddingOffset = child.m_offset - child.m_padding;
                    tw.WriteLine(String.Format("{0,-40} {1,5} {2,5}", "****Padding", paddingOffset, child.m_padding));
                }

                tw.WriteLine(String.Format("{0,-40} {1,5} {2,5}", child.m_name, child.m_offset, child.m_size));
            }
            // Final structure padding.
            if (info.m_padding > 0)
            {
                long paddingOffset = (long)info.m_size - info.m_padding;
                tw.WriteLine(String.Format("{0,-40} {1,5} {2,5}", "****Padding", paddingOffset, info.m_padding));
            }
        }

        private void CopyTypeLayoutToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SymbolInfo info = FindSelectedSymbolInfo();

            if (info != null)
            {
                System.IO.StringWriter tw = new System.IO.StringWriter();
                DumpSymbolInfo(tw, info);
                Clipboard.SetText(tw.ToString());
            }
        }

        private void TextBoxCache_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Escape)
            {
                ShowSelectedSymbolInfo();
            }

            base.OnKeyPress(e);
        }

        private void TextBoxCache_Leave(object sender, EventArgs e)
        {
            ShowSelectedSymbolInfo();
        }

        private void SetPrefetchStartOffsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewSymbolInfo.SelectedRows.Count != 0)
            {
                DataGridViewRow selectedRow = dataGridViewSymbolInfo.SelectedRows[0];
                long symbolOffset = Convert.ToUInt32(selectedRow.Cells[1].Value);
                m_prefetchStartOffset = symbolOffset;
                ShowSelectedSymbolInfo();
            }
        }

        private bool IsDataTableBusy()
        {
            return bgWorkerTableData.IsBusy || bgWorker.IsBusy;
        }

        private class SymbolAddressComparator : IComparer<Tuple<string, uint, ulong>>
        {
            public int Compare(Tuple<string, uint, ulong> a, Tuple<string, uint, ulong> b)
            {
                if (a.Item2 > b.Item2)
                {
                    return 1;
                }
                else if (a.Item2 < b.Item2)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        } 

        private void FillDataTable(object sender, TableViewTypes selectedTableView)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            int progressPercent = 0;
            int prevProgressPercent = -1;
            int i = 0;

            m_table.BeginLoadData();

            if (selectedTableView == TableViewTypes.ClassFieldData)
            {
                foreach (SymbolInfo info in m_symbols.Values)
                {
                    DataRow row = m_table.NewRow();

                    row["Symbol"] = info.m_name;
                    row["Size"] = info.m_size;
                    row["Padding"] = info.m_padding;
                    row["Padding/Size"] = (double)info.m_padding / info.m_size;

                    m_table.Rows.Add(row);

                    // Calculate the current progress
                    i++;
                    progressPercent = (int)((i * 100) / m_symbols.Count);

                    if (prevProgressPercent < progressPercent)
                    {
                        worker.ReportProgress(progressPercent);
                        prevProgressPercent = progressPercent;
                    }
                }
            }
            else if (selectedTableView == TableViewTypes.ClassStaticData)
            {
                foreach (SymbolInfo info in m_symbols.Values)
                {
                    string[] staticFieldsList = sym.GetTypeStaticFieldNames(info.m_type_id);

                    foreach(string staticFieldName in staticFieldsList)
                    {
                        // Type info
                        string typeName = sym.GetTypeName(info.m_type_id);

                        // Static fields info
                        Tuple<uint, ulong> fieldInfo = sym.GetTypeStaticFieldTypeAndAddress(info.m_type_id, staticFieldName);
                        uint staticFieldTypeId = fieldInfo.Item1;
                        string staticFieldType = sym.GetTypeName(staticFieldTypeId);
                        uint staticFieldSize = sym.GetTypeSize(staticFieldTypeId);

                        string fullStaticName = typeName + "::" + staticFieldName;

                        DataRow row = m_table.NewRow();

                        row["Symbol"] = fullStaticName;
                        row["Size"] = staticFieldSize;
                        row["Type"] = staticFieldType;

                        m_table.Rows.Add(row);
                    }

                    // Calculate the current progress
                    i++;
                    progressPercent = ((i * 100) / m_symbols.Count);

                    if (prevProgressPercent < progressPercent)
                    {
                        worker.ReportProgress(progressPercent);
                        prevProgressPercent = progressPercent;
                    }
                }
            }
            else if (selectedTableView == TableViewTypes.GlobalStaticData)
            {
                List<Tuple<string, uint, uint>> globalVariableList = sym.GetGlobalVariablesInfo();
                List<Tuple<string, uint, ulong>> sectionContribInfo = sym.GetSectionsContribInfoList();
                int sectionContribIndex = -1;

                SymbolAddressComparator symAddressCompare = new SymbolAddressComparator();

                foreach (var (variableName, variableRelativeVirtualAddress, variableTypeId) in globalVariableList)
                {
                    uint globalSymSize = sym.GetTypeSize(variableTypeId);
                    string globalSymType = sym.GetTypeName(variableTypeId);

                    Tuple<string, uint,  ulong> searchObject = new Tuple<string, uint, ulong>("", variableRelativeVirtualAddress, 0);

                    sectionContribIndex = sectionContribInfo.BinarySearch(searchObject, symAddressCompare);
                    sectionContribIndex = (sectionContribIndex < 0) ? (sectionContribIndex ^ (-1)) - 1: sectionContribIndex;

                    DataRow row = m_table.NewRow();

                    row["Symbol"] = variableName;
                    row["Size"] = globalSymSize;
                    row["Type"] = globalSymType;
                    row["Object file"] = "No object file";

                    if (sectionContribIndex >= 0)
                    {
                        ulong endSectionRVA = sectionContribInfo[sectionContribIndex].Item2 + sectionContribInfo[sectionContribIndex].Item3;

                        if (variableRelativeVirtualAddress < endSectionRVA)
                        {
                            string objectFileName = System.IO.Path.GetFileName(sectionContribInfo[sectionContribIndex].Item1);
                            row["Object file"] = objectFileName;
                        }
                    }

                    m_table.Rows.Add(row);

                    // Calculate the current progress
                    i++;
                    progressPercent = ((i * 100) / globalVariableList.Count);

                    if (prevProgressPercent < progressPercent)
                    {
                        worker.ReportProgress(progressPercent);
                        prevProgressPercent = progressPercent;
                    }
                }
            }

            m_table.EndLoadData();            
        }

        private void TablePresentationComboBox_ItemChanged(object sender, EventArgs e)
        {
            ComboBox viewTablecomboBox = (ComboBox)sender;
            TableViewTypes selectedTableView = dictViewTableTypes[tablePresentationComboBox.Text];

            if (IsDataTableBusy())
            {
                MessageBox.Show("Cannot change the item. Table is still processing.");
            }
            else
            {
                textBoxFilter.Text = "";
                tablePresentationComboBox.Enabled = false;
                ResetDataTable();
                CreateDataTableColumns(m_table, selectedTableView);
                
                bgWorkerTableData.RunWorkerAsync(selectedTableView);
            }
        }

        private void BgWorkerTableData_DoWork(object sender, DoWorkEventArgs e)
        {
            FillDataTable(sender, (TableViewTypes)e.Argument);
        }

        private void BgWorkerTableData_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = ((progressBar.Maximum * e.ProgressPercentage) / 100);
        }

        private void BgWorkerTableData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // Sort by name by default (ascending)
            dataGridSymbols.Sort(dataGridSymbols.Columns[0], ListSortDirection.Ascending);
            bindingSourceSymbols.Filter = null;// "Symbol LIKE '*rde*'";

            ShowSelectedSymbolInfo();

            progressBar.Value = progressBar.Maximum;
            tablePresentationComboBox.Enabled = true;
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            PopulateDataTable(e.Argument as string);
            FillDataTable(sender, TableViewTypes.ClassFieldData);
        }

        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = ((progressBar.Maximum * e.ProgressPercentage) / 100);
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGridSymbols.Sort(dataGridSymbols.Columns[0], ListSortDirection.Ascending);
            bindingSourceSymbols.Filter = null;

            ShowSelectedSymbolInfo();

            Text = "Pad Analyzer: Loaded " + currentFileName;
            tablePresentationComboBox.Enabled = true;
        }
    }
}
