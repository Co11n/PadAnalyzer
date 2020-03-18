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
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.textBoxCache = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
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
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSymbols)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.mainMenu.Size = new System.Drawing.Size(1055, 24);
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
            this.loadPDBToolStripMenuItem.Click += new System.EventHandler(this.loadPDBToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // openPdbDialog
            // 
            this.openPdbDialog.Filter = "Symbol files|*.nss;*.elf|All files|*.*";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 24);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.progressBar);
            this.splitContainer2.Panel1.Controls.Add(this.textBoxCache);
            this.splitContainer2.Panel1.Controls.Add(this.label2);
            this.splitContainer2.Panel1.Controls.Add(this.label1);
            this.splitContainer2.Panel1.Controls.Add(this.textBoxFilter);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer2.Size = new System.Drawing.Size(1055, 340);
            this.splitContainer2.SplitterDistance = 25;
            this.splitContainer2.TabIndex = 1;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(341, 0);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(274, 23);
            this.progressBar.TabIndex = 5;
            // 
            // textBoxCache
            // 
            this.textBoxCache.Location = new System.Drawing.Point(301, 2);
            this.textBoxCache.Mask = "0000";
            this.textBoxCache.Name = "textBoxCache";
            this.textBoxCache.Size = new System.Drawing.Size(34, 20);
            this.textBoxCache.TabIndex = 4;
            this.textBoxCache.Text = "0";
            this.textBoxCache.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCache_KeyPress);
            this.textBoxCache.Leave += new System.EventHandler(this.textBoxCache_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Cache line";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filter";
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Location = new System.Drawing.Point(53, 2);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(179, 20);
            this.textBoxFilter.TabIndex = 0;
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridSymbols);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewSymbolInfo);
            this.splitContainer1.Size = new System.Drawing.Size(1055, 311);
            this.splitContainer1.SplitterDistance = 615;
            this.splitContainer1.TabIndex = 2;
            // 
            // dataGridSymbols
            // 
            this.dataGridSymbols.AllowUserToAddRows = false;
            this.dataGridSymbols.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.dataGridSymbols.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridSymbols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridSymbols.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridSymbols.Location = new System.Drawing.Point(0, 0);
            this.dataGridSymbols.Name = "dataGridSymbols";
            this.dataGridSymbols.ReadOnly = true;
            this.dataGridSymbols.RowHeadersVisible = false;
            this.dataGridSymbols.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridSymbols.Size = new System.Drawing.Size(615, 311);
            this.dataGridSymbols.TabIndex = 2;
            this.dataGridSymbols.SelectionChanged += new System.EventHandler(this.dataGridSymbols_SelectionChanged);
            this.dataGridSymbols.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridSymbols_SortCompare);
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
            this.dataGridViewSymbolInfo.Size = new System.Drawing.Size(436, 311);
            this.dataGridViewSymbolInfo.TabIndex = 0;
            this.dataGridViewSymbolInfo.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewSymbolInfo_CellPainting);
            this.dataGridViewSymbolInfo.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridSymbols_SortCompare);
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(241, 48);
            // 
            // copyTypeLayoutToClipboardToolStripMenuItem
            // 
            this.copyTypeLayoutToClipboardToolStripMenuItem.Name = "copyTypeLayoutToClipboardToolStripMenuItem";
            this.copyTypeLayoutToClipboardToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.copyTypeLayoutToClipboardToolStripMenuItem.Text = "Copy Type Layout To Clipboard";
            this.copyTypeLayoutToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyTypeLayoutToClipboardToolStripMenuItem_Click);
            // 
            // setPrefetchStartOffsetToolStripMenuItem
            // 
            this.setPrefetchStartOffsetToolStripMenuItem.Name = "setPrefetchStartOffsetToolStripMenuItem";
            this.setPrefetchStartOffsetToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.setPrefetchStartOffsetToolStripMenuItem.Text = "Set Prefetch Start Offset";
            this.setPrefetchStartOffsetToolStripMenuItem.Click += new System.EventHandler(this.setPrefetchStartOffsetToolStripMenuItem_Click);
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bgWorker_ProgressChanged);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // PadAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 364);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "PadAnalyzer";
            this.Text = "Pad Analyzer";
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceSymbols)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridSymbols;
        private System.Windows.Forms.DataGridView dataGridViewSymbolInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyTypeLayoutToClipboardToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox textBoxCache;
        private System.Windows.Forms.ToolStripMenuItem setPrefetchStartOffsetToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.DataGridViewTextBoxColumn colField;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldOffset;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFieldSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colType;
    }
}

