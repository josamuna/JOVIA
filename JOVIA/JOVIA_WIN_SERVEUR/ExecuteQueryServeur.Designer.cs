namespace JOVIA_WIN_SERVEUR
{
    partial class ExecuteQueryServeur
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExecuteQueryServeur));
            this.btnCloseExecuteQuery = new System.Windows.Forms.ToolStripButton();
            this.TxtQuery = new System.Windows.Forms.RichTextBox();
            this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.bindingNavigatorQuery = new System.Windows.Forms.BindingNavigator(this.components);
            this.btnCloseQuery = new System.Windows.Forms.ToolStripButton();
            this.bnavQuery = new System.Windows.Forms.BindingNavigator(this.components);
            this.btnLoadFile = new System.Windows.Forms.ToolStripButton();
            this.btnSaveFile = new System.Windows.Forms.ToolStripButton();
            this.btnCreateFile = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorQuery)).BeginInit();
            this.bindingNavigatorQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bnavQuery)).BeginInit();
            this.bnavQuery.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCloseExecuteQuery
            // 
            this.btnCloseExecuteQuery.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnCloseExecuteQuery.BackColor = System.Drawing.SystemColors.Control;
            this.btnCloseExecuteQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCloseExecuteQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseExecuteQuery.Image")));
            this.btnCloseExecuteQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseExecuteQuery.Name = "btnCloseExecuteQuery";
            this.btnCloseExecuteQuery.Size = new System.Drawing.Size(23, 22);
            this.btnCloseExecuteQuery.Text = "Quitter";
            this.btnCloseExecuteQuery.Click += new System.EventHandler(this.btnCloseExecuteQuery_Click);
            // 
            // TxtQuery
            // 
            this.TxtQuery.Location = new System.Drawing.Point(7, 58);
            this.TxtQuery.Name = "TxtQuery";
            this.TxtQuery.Size = new System.Drawing.Size(511, 282);
            this.TxtQuery.TabIndex = 66;
            this.TxtQuery.Text = "";
            // 
            // dlgOpenFile
            // 
            this.dlgOpenFile.Filter = "\"Text File|* .txt|All file|*.*\"";
            // 
            // bindingNavigatorQuery
            // 
            this.bindingNavigatorQuery.AddNewItem = null;
            this.bindingNavigatorQuery.CountItem = null;
            this.bindingNavigatorQuery.DeleteItem = null;
            this.bindingNavigatorQuery.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bindingNavigatorQuery.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCloseQuery});
            this.bindingNavigatorQuery.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigatorQuery.MoveFirstItem = null;
            this.bindingNavigatorQuery.MoveLastItem = null;
            this.bindingNavigatorQuery.MoveNextItem = null;
            this.bindingNavigatorQuery.MovePreviousItem = null;
            this.bindingNavigatorQuery.Name = "bindingNavigatorQuery";
            this.bindingNavigatorQuery.PositionItem = null;
            this.bindingNavigatorQuery.Size = new System.Drawing.Size(528, 25);
            this.bindingNavigatorQuery.TabIndex = 67;
            this.bindingNavigatorQuery.Text = "bindingNavigator1";
            // 
            // btnCloseQuery
            // 
            this.btnCloseQuery.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnCloseQuery.BackColor = System.Drawing.SystemColors.Control;
            this.btnCloseQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCloseQuery.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseQuery.Image")));
            this.btnCloseQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseQuery.Name = "btnCloseQuery";
            this.btnCloseQuery.Size = new System.Drawing.Size(23, 22);
            this.btnCloseQuery.Text = "Quitter";
            this.btnCloseQuery.Click += new System.EventHandler(this.btnCloseQuery_Click);
            // 
            // bnavQuery
            // 
            this.bnavQuery.AddNewItem = null;
            this.bnavQuery.CountItem = null;
            this.bnavQuery.DeleteItem = null;
            this.bnavQuery.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bnavQuery.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLoadFile,
            this.btnSaveFile,
            this.btnCreateFile});
            this.bnavQuery.Location = new System.Drawing.Point(0, 25);
            this.bnavQuery.MoveFirstItem = null;
            this.bnavQuery.MoveLastItem = null;
            this.bnavQuery.MoveNextItem = null;
            this.bnavQuery.MovePreviousItem = null;
            this.bnavQuery.Name = "bnavQuery";
            this.bnavQuery.PositionItem = null;
            this.bnavQuery.Size = new System.Drawing.Size(528, 25);
            this.bnavQuery.TabIndex = 68;
            this.bnavQuery.Text = "bindingNavigator1";
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLoadFile.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadFile.Image")));
            this.btnLoadFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(23, 22);
            this.btnLoadFile.Text = "Ouvrir un fichier";
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveFile.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveFile.Image")));
            this.btnSaveFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(23, 22);
            this.btnSaveFile.Text = "Enregistrer le fichier";
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // btnCreateFile
            // 
            this.btnCreateFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCreateFile.Image = ((System.Drawing.Image)(resources.GetObject("btnCreateFile.Image")));
            this.btnCreateFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCreateFile.Name = "btnCreateFile";
            this.btnCreateFile.Size = new System.Drawing.Size(23, 22);
            this.btnCreateFile.Text = "Création";
            this.btnCreateFile.Click += new System.EventHandler(this.btnCreateFile_Click);
            // 
            // ExecuteQueryServeur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.bnavQuery);
            this.Controls.Add(this.bindingNavigatorQuery);
            this.Controls.Add(this.TxtQuery);
            this.Name = "ExecuteQueryServeur";
            this.Size = new System.Drawing.Size(528, 351);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigatorQuery)).EndInit();
            this.bindingNavigatorQuery.ResumeLayout(false);
            this.bindingNavigatorQuery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bnavQuery)).EndInit();
            this.bnavQuery.ResumeLayout(false);
            this.bnavQuery.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton btnCloseExecuteQuery;
        private System.Windows.Forms.RichTextBox TxtQuery;
        private System.Windows.Forms.OpenFileDialog dlgOpenFile;
        private System.Windows.Forms.SaveFileDialog dlgSaveFile;
        private System.Windows.Forms.BindingNavigator bindingNavigatorQuery;
        private System.Windows.Forms.ToolStripButton btnCloseQuery;
        private System.Windows.Forms.BindingNavigator bnavQuery;
        private System.Windows.Forms.ToolStripButton btnLoadFile;
        private System.Windows.Forms.ToolStripButton btnSaveFile;
        private System.Windows.Forms.ToolStripButton btnCreateFile;
    }
}
