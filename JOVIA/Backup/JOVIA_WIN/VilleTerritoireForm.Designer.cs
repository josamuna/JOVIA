namespace JOVIA_WIN
{
    partial class VilleTerritoireForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VilleTerritoireForm));
            this.bdNavProvVilleTer = new System.Windows.Forms.BindingNavigator(this.components);
            this.btnCloseProvinceVilleTerritoire = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.label2 = new System.Windows.Forms.Label();
            this.cboRechercherVilleT = new System.Windows.Forms.ComboBox();
            this.dgvVilleTer = new System.Windows.Forms.DataGridView();
            this.txtSuperficieVilleTer = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.bdNavigateVilleTerritoire = new System.Windows.Forms.BindingNavigator(this.components);
            this.btnAddVilleTer = new System.Windows.Forms.ToolStripButton();
            this.btnRefreshVilleTer = new System.Windows.Forms.ToolStripButton();
            this.btnSaveVilleTer = new System.Windows.Forms.ToolStripButton();
            this.btnUpdateVilleTer = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteVilleTer = new System.Windows.Forms.ToolStripButton();
            this.txtDesignationVilleTer = new System.Windows.Forms.TextBox();
            this.lblIdVilleTer = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bdNavProvVilleTer)).BeginInit();
            this.bdNavProvVilleTer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVilleTer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdNavigateVilleTerritoire)).BeginInit();
            this.bdNavigateVilleTerritoire.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bdNavProvVilleTer
            // 
            this.bdNavProvVilleTer.AddNewItem = null;
            this.bdNavProvVilleTer.BackColor = System.Drawing.Color.Transparent;
            this.bdNavProvVilleTer.CountItem = null;
            this.bdNavProvVilleTer.DeleteItem = null;
            this.bdNavProvVilleTer.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bdNavProvVilleTer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCloseProvinceVilleTerritoire,
            this.toolStripSeparator1});
            this.bdNavProvVilleTer.Location = new System.Drawing.Point(0, 0);
            this.bdNavProvVilleTer.MoveFirstItem = null;
            this.bdNavProvVilleTer.MoveLastItem = null;
            this.bdNavProvVilleTer.MoveNextItem = null;
            this.bdNavProvVilleTer.MovePreviousItem = null;
            this.bdNavProvVilleTer.Name = "bdNavProvVilleTer";
            this.bdNavProvVilleTer.PositionItem = null;
            this.bdNavProvVilleTer.Size = new System.Drawing.Size(581, 25);
            this.bdNavProvVilleTer.TabIndex = 61;
            this.bdNavProvVilleTer.Text = "bindingNavigator1";
            // 
            // btnCloseProvinceVilleTerritoire
            // 
            this.btnCloseProvinceVilleTerritoire.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnCloseProvinceVilleTerritoire.BackColor = System.Drawing.Color.Transparent;
            this.btnCloseProvinceVilleTerritoire.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCloseProvinceVilleTerritoire.Image = global::JOVIA_WIN.Properties.Resources.Close;
            this.btnCloseProvinceVilleTerritoire.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseProvinceVilleTerritoire.Name = "btnCloseProvinceVilleTerritoire";
            this.btnCloseProvinceVilleTerritoire.Size = new System.Drawing.Size(23, 22);
            this.btnCloseProvinceVilleTerritoire.Text = "Quitter";
            this.btnCloseProvinceVilleTerritoire.Click += new System.EventHandler(this.btnCloseProvinceVilleTerritoire_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(355, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 81;
            this.label2.Text = "Rechercher :";
            // 
            // cboRechercherVilleT
            // 
            this.cboRechercherVilleT.FormattingEnabled = true;
            this.cboRechercherVilleT.Items.AddRange(new object[] {
            "Masculin",
            "Feminin"});
            this.cboRechercherVilleT.Location = new System.Drawing.Point(430, 13);
            this.cboRechercherVilleT.Name = "cboRechercherVilleT";
            this.cboRechercherVilleT.Size = new System.Drawing.Size(137, 21);
            this.cboRechercherVilleT.TabIndex = 80;
            this.cboRechercherVilleT.SelectedIndexChanged += new System.EventHandler(this.cboRechercherVilleT_SelectedIndexChanged);
            // 
            // dgvVilleTer
            // 
            this.dgvVilleTer.AllowUserToAddRows = false;
            this.dgvVilleTer.AllowUserToDeleteRows = false;
            this.dgvVilleTer.BackgroundColor = System.Drawing.Color.White;
            this.dgvVilleTer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvVilleTer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVilleTer.Location = new System.Drawing.Point(290, 75);
            this.dgvVilleTer.MultiSelect = false;
            this.dgvVilleTer.Name = "dgvVilleTer";
            this.dgvVilleTer.ReadOnly = true;
            this.dgvVilleTer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVilleTer.Size = new System.Drawing.Size(283, 72);
            this.dgvVilleTer.TabIndex = 76;
            this.dgvVilleTer.SelectionChanged += new System.EventHandler(this.dgvVilleTer_SelectionChanged);
            // 
            // txtSuperficieVilleTer
            // 
            this.txtSuperficieVilleTer.Location = new System.Drawing.Point(84, 127);
            this.txtSuperficieVilleTer.Name = "txtSuperficieVilleTer";
            this.txtSuperficieVilleTer.Size = new System.Drawing.Size(200, 20);
            this.txtSuperficieVilleTer.TabIndex = 75;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(12, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 25);
            this.label7.TabIndex = 78;
            this.label7.Text = "Superficie :";
            // 
            // bdNavigateVilleTerritoire
            // 
            this.bdNavigateVilleTerritoire.AddNewItem = null;
            this.bdNavigateVilleTerritoire.AutoSize = false;
            this.bdNavigateVilleTerritoire.BackColor = System.Drawing.Color.Transparent;
            this.bdNavigateVilleTerritoire.CountItem = null;
            this.bdNavigateVilleTerritoire.DeleteItem = null;
            this.bdNavigateVilleTerritoire.Dock = System.Windows.Forms.DockStyle.None;
            this.bdNavigateVilleTerritoire.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bdNavigateVilleTerritoire.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddVilleTer,
            this.btnRefreshVilleTer,
            this.btnSaveVilleTer,
            this.btnUpdateVilleTer,
            this.btnDeleteVilleTer});
            this.bdNavigateVilleTerritoire.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.bdNavigateVilleTerritoire.Location = new System.Drawing.Point(2, 28);
            this.bdNavigateVilleTerritoire.MoveFirstItem = null;
            this.bdNavigateVilleTerritoire.MoveLastItem = null;
            this.bdNavigateVilleTerritoire.MoveNextItem = null;
            this.bdNavigateVilleTerritoire.MovePreviousItem = null;
            this.bdNavigateVilleTerritoire.Name = "bdNavigateVilleTerritoire";
            this.bdNavigateVilleTerritoire.PositionItem = null;
            this.bdNavigateVilleTerritoire.Size = new System.Drawing.Size(118, 23);
            this.bdNavigateVilleTerritoire.TabIndex = 77;
            this.bdNavigateVilleTerritoire.Text = "bindingNavigator1";
            // 
            // btnAddVilleTer
            // 
            this.btnAddVilleTer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddVilleTer.Image = global::JOVIA_WIN.Properties.Resources.navBarAddIcon_2x;
            this.btnAddVilleTer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddVilleTer.Name = "btnAddVilleTer";
            this.btnAddVilleTer.Size = new System.Drawing.Size(23, 20);
            this.btnAddVilleTer.Text = "Ajouter";
            this.btnAddVilleTer.Click += new System.EventHandler(this.btnAddVilleTer_Click);
            // 
            // btnRefreshVilleTer
            // 
            this.btnRefreshVilleTer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefreshVilleTer.Image = global::JOVIA_WIN.Properties.Resources.update1;
            this.btnRefreshVilleTer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshVilleTer.Name = "btnRefreshVilleTer";
            this.btnRefreshVilleTer.Size = new System.Drawing.Size(23, 20);
            this.btnRefreshVilleTer.Text = "Refresh";
            this.btnRefreshVilleTer.Click += new System.EventHandler(this.btnRefreshVilleTer_Click);
            // 
            // btnSaveVilleTer
            // 
            this.btnSaveVilleTer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveVilleTer.Image = global::JOVIA_WIN.Properties.Resources.check_2x1;
            this.btnSaveVilleTer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveVilleTer.Name = "btnSaveVilleTer";
            this.btnSaveVilleTer.Size = new System.Drawing.Size(23, 20);
            this.btnSaveVilleTer.Text = "Enregistrer";
            this.btnSaveVilleTer.Click += new System.EventHandler(this.btnSaveVilleTer_Click);
            // 
            // btnUpdateVilleTer
            // 
            this.btnUpdateVilleTer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUpdateVilleTer.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateVilleTer.Image")));
            this.btnUpdateVilleTer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpdateVilleTer.Name = "btnUpdateVilleTer";
            this.btnUpdateVilleTer.Size = new System.Drawing.Size(23, 20);
            this.btnUpdateVilleTer.Text = "Modifier";
            this.btnUpdateVilleTer.Click += new System.EventHandler(this.btnUpdateVilleTer_Click);
            // 
            // btnDeleteVilleTer
            // 
            this.btnDeleteVilleTer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteVilleTer.Image = global::JOVIA_WIN.Properties.Resources.mp_delete_md_n_lt_2x1;
            this.btnDeleteVilleTer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteVilleTer.Name = "btnDeleteVilleTer";
            this.btnDeleteVilleTer.Size = new System.Drawing.Size(23, 20);
            this.btnDeleteVilleTer.Text = "Supprimer";
            this.btnDeleteVilleTer.Click += new System.EventHandler(this.btnDeleteVilleTer_Click);
            // 
            // txtDesignationVilleTer
            // 
            this.txtDesignationVilleTer.Location = new System.Drawing.Point(84, 101);
            this.txtDesignationVilleTer.Name = "txtDesignationVilleTer";
            this.txtDesignationVilleTer.Size = new System.Drawing.Size(200, 20);
            this.txtDesignationVilleTer.TabIndex = 74;
            // 
            // lblIdVilleTer
            // 
            this.lblIdVilleTer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIdVilleTer.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblIdVilleTer.Location = new System.Drawing.Point(84, 75);
            this.lblIdVilleTer.Name = "lblIdVilleTer";
            this.lblIdVilleTer.Size = new System.Drawing.Size(100, 20);
            this.lblIdVilleTer.TabIndex = 79;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 22);
            this.label3.TabIndex = 73;
            this.label3.Text = "Code :";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 72;
            this.label1.Text = "Désignation :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboRechercherVilleT);
            this.groupBox1.Location = new System.Drawing.Point(3, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(575, 40);
            this.groupBox1.TabIndex = 82;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(250, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 15);
            this.label4.TabIndex = 83;
            this.label4.Text = "Ville/ Térritoire";
            // 
            // VilleTerritoireForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::JOVIA_WIN.Properties.Resources.bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvVilleTer);
            this.Controls.Add(this.txtSuperficieVilleTer);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.bdNavigateVilleTerritoire);
            this.Controls.Add(this.txtDesignationVilleTer);
            this.Controls.Add(this.lblIdVilleTer);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bdNavProvVilleTer);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "VilleTerritoireForm";
            this.Size = new System.Drawing.Size(581, 157);
            this.Load += new System.EventHandler(this.VilleTerritoireForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bdNavProvVilleTer)).EndInit();
            this.bdNavProvVilleTer.ResumeLayout(false);
            this.bdNavProvVilleTer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVilleTer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdNavigateVilleTerritoire)).EndInit();
            this.bdNavigateVilleTerritoire.ResumeLayout(false);
            this.bdNavigateVilleTerritoire.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingNavigator bdNavProvVilleTer;
        private System.Windows.Forms.ToolStripButton btnCloseProvinceVilleTerritoire;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboRechercherVilleT;
        private System.Windows.Forms.DataGridView dgvVilleTer;
        private System.Windows.Forms.TextBox txtSuperficieVilleTer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.BindingNavigator bdNavigateVilleTerritoire;
        private System.Windows.Forms.ToolStripButton btnAddVilleTer;
        private System.Windows.Forms.ToolStripButton btnRefreshVilleTer;
        private System.Windows.Forms.ToolStripButton btnSaveVilleTer;
        private System.Windows.Forms.ToolStripButton btnUpdateVilleTer;
        private System.Windows.Forms.ToolStripButton btnDeleteVilleTer;
        private System.Windows.Forms.TextBox txtDesignationVilleTer;
        private System.Windows.Forms.Label lblIdVilleTer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
    }
}
