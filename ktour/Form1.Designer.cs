namespace ktour
{
    partial class Form1
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
            this.solveButton = new System.Windows.Forms.Button();
            this.solutionBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.ChessBoardGrid = new System.Windows.Forms.DataGridView();
            this.forwardButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.statusLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.colsUpDown = new System.Windows.Forms.NumericUpDown();
            this.rowsUpDown = new System.Windows.Forms.NumericUpDown();
            this.boardSizeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ChessBoardGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colsUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowsUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // solveButton
            // 
            this.solveButton.Enabled = false;
            this.solveButton.Location = new System.Drawing.Point(3, 3);
            this.solveButton.Name = "solveButton";
            this.solveButton.Size = new System.Drawing.Size(75, 23);
            this.solveButton.TabIndex = 0;
            this.solveButton.Text = "Solve";
            this.solveButton.UseVisualStyleBackColor = true;
            this.solveButton.Click += new System.EventHandler(this.solveButton_Click);
            // 
            // solutionBackgroundWorker
            // 
            this.solutionBackgroundWorker.WorkerReportsProgress = true;
            this.solutionBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.solutionBackgroundWorker_DoWork);
            this.solutionBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.SolutionBackgroundWorker_ProgressChanged);
            this.solutionBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.solutionBackgroundWorker_RunWorkerCompleted);
            // 
            // ChessBoardGrid
            // 
            this.ChessBoardGrid.AllowUserToAddRows = false;
            this.ChessBoardGrid.AllowUserToDeleteRows = false;
            this.ChessBoardGrid.AllowUserToResizeColumns = false;
            this.ChessBoardGrid.AllowUserToResizeRows = false;
            this.ChessBoardGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ChessBoardGrid.ColumnHeadersVisible = false;
            this.ChessBoardGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChessBoardGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.ChessBoardGrid.Location = new System.Drawing.Point(0, 0);
            this.ChessBoardGrid.MultiSelect = false;
            this.ChessBoardGrid.Name = "ChessBoardGrid";
            this.ChessBoardGrid.ReadOnly = true;
            this.ChessBoardGrid.RowHeadersVisible = false;
            this.ChessBoardGrid.RowTemplate.Height = 41;
            this.ChessBoardGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.ChessBoardGrid.Size = new System.Drawing.Size(631, 390);
            this.ChessBoardGrid.TabIndex = 1;
            this.ChessBoardGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ChessBoardGrid_CellDoubleClick);
            // 
            // forwardButton
            // 
            this.forwardButton.Enabled = false;
            this.forwardButton.Location = new System.Drawing.Point(82, 3);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(75, 23);
            this.forwardButton.TabIndex = 2;
            this.forwardButton.Text = "Forward";
            this.forwardButton.UseVisualStyleBackColor = true;
            this.forwardButton.Click += new System.EventHandler(this.forwardButton_Click);
            // 
            // backButton
            // 
            this.backButton.Enabled = false;
            this.backButton.Location = new System.Drawing.Point(163, 3);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(75, 23);
            this.backButton.TabIndex = 3;
            this.backButton.Text = "Back";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.statusLabel);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.colsUpDown);
            this.splitContainer1.Panel1.Controls.Add(this.rowsUpDown);
            this.splitContainer1.Panel1.Controls.Add(this.boardSizeButton);
            this.splitContainer1.Panel1.Controls.Add(this.backButton);
            this.splitContainer1.Panel1.Controls.Add(this.forwardButton);
            this.splitContainer1.Panel1.Controls.Add(this.solveButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ChessBoardGrid);
            this.splitContainer1.Size = new System.Drawing.Size(631, 441);
            this.splitContainer1.SplitterDistance = 47;
            this.splitContainer1.TabIndex = 4;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(3, 29);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(238, 13);
            this.statusLabel.TabIndex = 9;
            this.statusLabel.Text = "Double-click a starting square to place the knight";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(349, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Cols:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(257, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Rows:";
            // 
            // colsUpDown
            // 
            this.colsUpDown.Location = new System.Drawing.Point(385, 6);
            this.colsUpDown.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.colsUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.colsUpDown.Name = "colsUpDown";
            this.colsUpDown.Size = new System.Drawing.Size(39, 20);
            this.colsUpDown.TabIndex = 6;
            this.colsUpDown.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // rowsUpDown
            // 
            this.rowsUpDown.Location = new System.Drawing.Point(300, 6);
            this.rowsUpDown.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.rowsUpDown.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.rowsUpDown.Name = "rowsUpDown";
            this.rowsUpDown.Size = new System.Drawing.Size(43, 20);
            this.rowsUpDown.TabIndex = 5;
            this.rowsUpDown.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // boardSizeButton
            // 
            this.boardSizeButton.Location = new System.Drawing.Point(430, 4);
            this.boardSizeButton.Name = "boardSizeButton";
            this.boardSizeButton.Size = new System.Drawing.Size(146, 23);
            this.boardSizeButton.TabIndex = 4;
            this.boardSizeButton.Text = "Change Board Size/Reset";
            this.boardSizeButton.UseVisualStyleBackColor = true;
            this.boardSizeButton.Click += new System.EventHandler(this.boardSizeButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 441);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Knight\'s Tour";
            ((System.ComponentModel.ISupportInitialize)(this.ChessBoardGrid)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.colsUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rowsUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button solveButton;
        private System.ComponentModel.BackgroundWorker solutionBackgroundWorker;
        private System.Windows.Forms.DataGridView ChessBoardGrid;
        private System.Windows.Forms.Button forwardButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button boardSizeButton;
        private System.Windows.Forms.NumericUpDown colsUpDown;
        private System.Windows.Forms.NumericUpDown rowsUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label statusLabel;
    }
}

