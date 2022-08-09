﻿namespace PadAnalyzer
{
    partial class PadAnalyzer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPDBToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openPdbDialog = new System.Windows.Forms.OpenFileDialog();
            this.bindingSourceSymbols = new System.Windows.Forms.BindingSource(this.components);
            this.mainInterfaceContainer = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.textBoxCache = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.tablePresentationComboBox = new System.Windows.Forms.ComboBox();
            this.exactSearch = new System.Windows.Forms.CheckBox();
            this.symbolTableContainer = new System.Windows.Forms.SplitContainer();
            this.dataGridSymbols = new System.Windows.Forms.DataGridView();
            this.dataGridViewSymbolInfo = new System.Windows.Forms.DataGridView();
            this.colField = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldOffset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFieldSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyTypeLayoutToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setPrefetchStartOffsetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.bgWorkerTableData = new System.ComponentModel.BackgroundWorker();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSymbols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainInterfaceContainer)).BeginInit();
            this.mainInterfaceContainer.Panel1.SuspendLayout();
            this.mainInterfaceContainer.Panel2.SuspendLayout();
            this.mainInterfaceContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.symbolTableContainer)).BeginInit();
            this.symbolTableContainer.Panel1.SuspendLayout();
            this.symbolTableContainer.Panel2.SuspendLayout();
            this.symbolTableContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSymbols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSymbolInfo)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1379, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadPDBToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadPDBToolStripMenuItem
            // 
            this.loadPDBToolStripMenuItem.Name = "loadPDBToolStripMenuItem";
            this.loadPDBToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.loadPDBToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.loadPDBToolStripMenuItem.Text = "Load PDB...";
            this.loadPDBToolStripMenuItem.Click += new System.EventHandler(this.LoadPDBToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // openPdbDialog
            // 
            this.openPdbDialog.Filter = "Symbol files|*.nss;*.elf;*.pdb|All files|*.*";
            // 
            // splitContainer2
            // 
            this.mainInterfaceContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainInterfaceContainer.IsSplitterFixed = true;
            this.mainInterfaceContainer.Location = new System.Drawing.Point(0, 24);
            this.mainInterfaceContainer.Name = "splitContainer2";
            this.mainInterfaceContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.mainInterfaceContainer.Panel1.Controls.Add(this.label3);
            this.mainInterfaceContainer.Panel1.Controls.Add(this.progressBar);
            this.mainInterfaceContainer.Panel1.Controls.Add(this.textBoxCache);
            this.mainInterfaceContainer.Panel1.Controls.Add(this.label2);
            this.mainInterfaceContainer.Panel1.Controls.Add(this.label1);
            this.mainInterfaceContainer.Panel1.Controls.Add(this.ProgressLabel);
            this.mainInterfaceContainer.Panel1.Controls.Add(this.textBoxFilter);
            this.mainInterfaceContainer.Panel1.Controls.Add(this.tablePresentationComboBox);
            this.mainInterfaceContainer.Panel1.Controls.Add(this.exactSearch);
            this.progressBar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.ProgressLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tablePresentationComboBox.Anchor = System.Windows.Forms.AnchorStyles.Right;

            // 
            // splitContainer2.Panel2
            // 
            this.mainInterfaceContainer.Panel2.Controls.Add(this.symbolTableContainer);
            this.mainInterfaceContainer.Size = new System.Drawing.Size(1379, 659);
            this.mainInterfaceContainer.SplitterDistance = 82;
            this.mainInterfaceContainer.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Exact search:";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(1102, 4);
            this.progressBar.Maximum = this.progressBar.Width;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(274, 23);
            this.progressBar.TabIndex = 5;
            // 
            // textBoxCache
            // 
            this.textBoxCache.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxCache.Location = new System.Drawing.Point(85, 55);
            this.textBoxCache.Mask = "0000";
            this.textBoxCache.Name = "textBoxCache";
            this.textBoxCache.Size = new System.Drawing.Size(34, 20);
            this.textBoxCache.TabIndex = 4;
            this.textBoxCache.Text = "0";
            this.textBoxCache.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxCache_KeyPress);
            this.textBoxCache.Leave += new System.EventHandler(this.TextBoxCache_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Cache line:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search tag:";
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoSize = true;
            this.ProgressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ProgressLabel.Location = new System.Drawing.Point(1018, 9);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(78, 13);
            this.ProgressLabel.TabIndex = 0;
            this.ProgressLabel.Text = "Load Progress:";
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxFilter.Location = new System.Drawing.Point(86, 6);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(179, 20);
            this.textBoxFilter.TabIndex = 0;
            this.textBoxFilter.TextChanged += new System.EventHandler(this.UseFilter);
            // 
            // tablePresentationComboBox
            // 
            this.tablePresentationComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tablePresentationComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tablePresentationComboBox.Items.AddRange(new object[] {
            "Class field data",
            "Class static field data",
            "Global static data"});
            this.tablePresentationComboBox.Location = new System.Drawing.Point(1102, 33);
            this.tablePresentationComboBox.Name = "tablePresentationComboBox";
            this.tablePresentationComboBox.Size = new System.Drawing.Size(222, 21);
            this.tablePresentationComboBox.TabIndex = 0;
            this.tablePresentationComboBox.SelectionChangeCommitted += new System.EventHandler(this.TablePresentationComboBox_ItemChanged);
            // 
            // exactSearch
            // 
            this.exactSearch.Location = new System.Drawing.Point(86, 29);
            this.exactSearch.Name = "exactSearch";
            this.exactSearch.Size = new System.Drawing.Size(104, 24);
            this.exactSearch.TabIndex = 0;
            this.exactSearch.CheckedChanged += new System.EventHandler(this.UseFilter);
            // 
            // splitContainer1
            // 
            this.symbolTableContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.symbolTableContainer.Location = new System.Drawing.Point(0, 0);
            this.symbolTableContainer.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.symbolTableContainer.Panel1.Controls.Add(this.dataGridSymbols);
            // 
            // splitContainer1.Panel2
            // 
            this.symbolTableContainer.Panel2.Controls.Add(this.dataGridViewSymbolInfo);
            this.symbolTableContainer.Size = new System.Drawing.Size(1379, 573);
            this.symbolTableContainer.SplitterDistance = 803;
            this.symbolTableContainer.TabIndex = 2;
            // 
            // dataGridSymbols
            // 
            this.dataGridSymbols.AllowUserToAddRows = false;
            this.dataGridSymbols.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.dataGridSymbols.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridSymbols.AutoGenerateColumns = true;
            this.dataGridSymbols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSymbols.DataSource = this.bindingSourceSymbols;
            this.dataGridSymbols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridSymbols.Location = new System.Drawing.Point(0, 0);
            this.dataGridSymbols.Name = "dataGridSymbols";
            this.dataGridSymbols.ReadOnly = true;
            this.dataGridSymbols.RowHeadersVisible = false;
            this.dataGridSymbols.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridSymbols.Size = new System.Drawing.Size(803, 573);
            this.dataGridSymbols.TabIndex = 2;
            this.dataGridSymbols.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.SetColumnSizeEquel);
            this.dataGridSymbols.SelectionChanged += new System.EventHandler(this.DataGridSymbols_SelectionChanged);
            this.dataGridSymbols.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.DataGridSymbols_SortCompare);
            // 
            // dataGridViewSymbolInfo
            // 
            this.dataGridViewSymbolInfo.AllowUserToAddRows = false;
            this.dataGridViewSymbolInfo.AllowUserToDeleteRows = false;
            this.dataGridViewSymbolInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSymbolInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colField,
            this.colFieldOffset,
            this.colFieldSize,
            this.colType});
            this.dataGridViewSymbolInfo.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridViewSymbolInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSymbolInfo.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewSymbolInfo.Name = "dataGridViewSymbolInfo";
            this.dataGridViewSymbolInfo.ReadOnly = true;
            this.dataGridViewSymbolInfo.RowHeadersVisible = false;
            this.dataGridViewSymbolInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewSymbolInfo.Size = new System.Drawing.Size(572, 573);
            this.dataGridViewSymbolInfo.TabIndex = 0;
            this.dataGridViewSymbolInfo.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DataGridViewSymbolInfo_CellPainting);
            this.dataGridViewSymbolInfo.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.DataGridSymbols_SortCompare);
            // 
            // colField
            // 
            this.colField.HeaderText = "Field";
            this.colField.Name = "colField";
            this.colField.ReadOnly = true;
            this.colField.Width = 210;
            // 
            // colFieldOffset
            // 
            this.colFieldOffset.HeaderText = "Offset";
            this.colFieldOffset.Name = "colFieldOffset";
            this.colFieldOffset.ReadOnly = true;
            // 
            // colFieldSize
            // 
            this.colFieldSize.HeaderText = "Size";
            this.colFieldSize.Name = "colFieldSize";
            this.colFieldSize.ReadOnly = true;
            // 
            // colType
            // 
            this.colType.HeaderText = "Type";
            this.colType.Name = "colType";
            this.colType.ReadOnly = true;
            this.colType.Width = 200;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyTypeLayoutToClipboardToolStripMenuItem,
            this.setPrefetchStartOffsetToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(239, 48);
            // 
            // copyTypeLayoutToClipboardToolStripMenuItem
            // 
            this.copyTypeLayoutToClipboardToolStripMenuItem.Name = "copyTypeLayoutToClipboardToolStripMenuItem";
            this.copyTypeLayoutToClipboardToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.copyTypeLayoutToClipboardToolStripMenuItem.Text = "Copy Type Layout To Clipboard";
            this.copyTypeLayoutToClipboardToolStripMenuItem.Click += new System.EventHandler(this.CopyTypeLayoutToClipboardToolStripMenuItem_Click);
            // 
            // setPrefetchStartOffsetToolStripMenuItem
            // 
            this.setPrefetchStartOffsetToolStripMenuItem.Name = "setPrefetchStartOffsetToolStripMenuItem";
            this.setPrefetchStartOffsetToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.setPrefetchStartOffsetToolStripMenuItem.Text = "Set Prefetch Start Offset";
            this.setPrefetchStartOffsetToolStripMenuItem.Click += new System.EventHandler(this.SetPrefetchStartOffsetToolStripMenuItem_Click);
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorker_RunWorkerCompleted);
            // 
            // bgWorkerTableData
            // 
            this.bgWorkerTableData.WorkerReportsProgress = true;
            this.bgWorkerTableData.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorkerTableData_DoWork);
            this.bgWorkerTableData.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BgWorkerTableData_ProgressChanged);
            this.bgWorkerTableData.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorkerTableData_RunWorkerCompleted);
            // 
            // PadAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1379, 683);
            this.Controls.Add(this.mainInterfaceContainer);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "PadAnalyzer";
            this.Text = "Pad Analyzer";
            this.Load += new System.EventHandler(this.SetColumnSizeEquel);
            this.Resize += new System.EventHandler(this.SetColumnSizeEquel);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSymbols)).EndInit();
            this.mainInterfaceContainer.Panel1.ResumeLayout(false);
            this.mainInterfaceContainer.Panel1.PerformLayout();
            this.mainInterfaceContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainInterfaceContainer)).EndInit();
            this.mainInterfaceContainer.ResumeLayout(false);
            this.symbolTableContainer.Panel1.ResumeLayout(false);
            this.symbolTableContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.symbolTableContainer)).EndInit();
            this.symbolTableContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridSymbols)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSymbolInfo)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadPDBToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openPdbDialog;
        private System.Windows.Forms.BindingSource bindingSourceSymbols;
        private System.Windows.Forms.SplitContainer mainInterfaceContainer;
        private System.Windows.Forms.SplitContainer symbolTableContainer;
        private System.Windows.Forms.DataGridView dataGridSymbols;
        private System.Windows.Forms.DataGridView dataGridViewSymbolInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.ComboBox tablePresentationComboBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyTypeLayoutToClipboardToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ProgressLabel;
        private System.Windows.Forms.MaskedTextBox textBoxCache;
        private System.Windows.Forms.ToolStripMenuItem setPrefetchStartOffsetToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.ComponentModel.BackgroundWorker bgWorkerTableData;
        private System.Windows.Forms.DataGridViewTextBoxColumn colField;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldOffset;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
        private System.Windows.Forms.CheckBox exactSearch;
        private System.Windows.Forms.Label label3;
    }
}

