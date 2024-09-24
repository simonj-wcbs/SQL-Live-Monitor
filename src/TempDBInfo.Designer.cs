namespace SQLMonitor
{
    partial class TempDBInfo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.progressBar1 = new SQLMonitor.CustomProgressBar();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.dgLocks = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgLocks)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 12);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(776, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // cmbCategory
            // 
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(12, 41);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(776, 21);
            this.cmbCategory.TabIndex = 1;
            // 
            // dgLocks
            // 
            this.dgLocks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLocks.Location = new System.Drawing.Point(12, 68);
            this.dgLocks.Name = "dgLocks";
            this.dgLocks.Size = new System.Drawing.Size(776, 370);
            this.dgLocks.TabIndex = 2;
            // 
            // TempDBInfo
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgLocks);
            this.Controls.Add(this.cmbCategory);
            this.Controls.Add(this.progressBar1);
            this.Name = "TempDBInfo";
            this.Text = "TempDBInfo";
            ((System.ComponentModel.ISupportInitialize)(this.dgLocks)).EndInit();
            this.ResumeLayout(false);

        }

        // Declare all your controls here, but only once
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.DataGridView dgLocks;
    }
}