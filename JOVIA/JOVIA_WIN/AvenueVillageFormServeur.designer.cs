namespace JOVIA_WIN
{
    partial class AvenueVillageFormServeur
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AvenueVillageFormServeur));
            this.lblIdAvenueVillage = new System.Windows.Forms.Label();
            this.dgvAvenueVillage = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDesignationAvenueVillage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboAveniVillage = new System.Windows.Forms.ComboBox();
            this.lblIdQuartierLoc = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cboIdQuartierLoc = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCloseComCheffQuartierLoc = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigator2 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bnvCandidat = new System.Windows.Forms.BindingNavigator(this.components);
            this.btnAddAvenueVillage = new System.Windows.Forms.ToolStripButton();
            this.btnRefrehAvenueVillage = new System.Windows.Forms.ToolStripButton();
            this.btnSaveAvenueVillage = new System.Windows.Forms.ToolStripButton();
            this.btnModifierAvenueVillage = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteAvenueVillage = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvenueVillage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator2)).BeginInit();
            this.bindingNavigator2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bnvCandidat)).BeginInit();
            this.bnvCandidat.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblIdAvenueVillage
            // 
            this.lblIdAvenueVillage.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblIdAvenueVillage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdAvenueVillage.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblIdAvenueVillage.Location = new System.Drawing.Point(129, 82);
            this.lblIdAvenueVillage.Name = "lblIdAvenueVillage";
            this.lblIdAvenueVillage.Size = new System.Drawing.Size(100, 23);
            this.lblIdAvenueVillage.TabIndex = 64;
            // 
            // dgvAvenueVillage
            // 
            this.dgvAvenueVillage.AllowUserToAddRows = false;
            this.dgvAvenueVillage.AllowUserToDeleteRows = false;
            this.dgvAvenueVillage.BackgroundColor = System.Drawing.Color.White;
            this.dgvAvenueVillage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvAvenueVillage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAvenueVillage.Location = new System.Drawing.Point(15, 203);
            this.dgvAvenueVillage.MultiSelect = false;
            this.dgvAvenueVillage.Name = "dgvAvenueVillage";
            this.dgvAvenueVillage.ReadOnly = true;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAvenueVillage.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAvenueVillage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAvenueVillage.Size = new System.Drawing.Size(566, 129);
            this.dgvAvenueVillage.TabIndex = 2;
            this.dgvAvenueVillage.SelectionChanged += new System.EventHandler(this.dgvAvenueVillage_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Code:";
            // 
            // txtDesignationAvenueVillage
            // 
            this.txtDesignationAvenueVillage.Location = new System.Drawing.Point(129, 143);
            this.txtDesignationAvenueVillage.Name = "txtDesignationAvenueVillage";
            this.txtDesignationAvenueVillage.Size = new System.Drawing.Size(452, 20);
            this.txtDesignationAvenueVillage.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(368, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 69;
            this.label3.Text = "Rechercher :";
            // 
            // cboAveniVillage
            // 
            this.cboAveniVillage.FormattingEnabled = true;
            this.cboAveniVillage.Items.AddRange(new object[] {
            "Masculin",
            "Feminin"});
            this.cboAveniVillage.Location = new System.Drawing.Point(444, 14);
            this.cboAveniVillage.Name = "cboAveniVillage";
            this.cboAveniVillage.Size = new System.Drawing.Size(137, 21);
            this.cboAveniVillage.TabIndex = 68;
            // 
            // lblIdQuartierLoc
            // 
            this.lblIdQuartierLoc.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblIdQuartierLoc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblIdQuartierLoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIdQuartierLoc.Location = new System.Drawing.Point(128, 169);
            this.lblIdQuartierLoc.Name = "lblIdQuartierLoc";
            this.lblIdQuartierLoc.Size = new System.Drawing.Size(453, 20);
            this.lblIdQuartierLoc.TabIndex = 62;
            this.lblIdQuartierLoc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.DarkMagenta;
            this.label9.Location = new System.Drawing.Point(12, 175);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 13);
            this.label9.TabIndex = 61;
            this.label9.Text = "Code Quarier/Localité :";
            // 
            // cboIdQuartierLoc
            // 
            this.cboIdQuartierLoc.FormattingEnabled = true;
            this.cboIdQuartierLoc.Items.AddRange(new object[] {
            "Masculin",
            "Feminin"});
            this.cboIdQuartierLoc.Location = new System.Drawing.Point(129, 114);
            this.cboIdQuartierLoc.Name = "cboIdQuartierLoc";
            this.cboIdQuartierLoc.Size = new System.Drawing.Size(452, 21);
            this.cboIdQuartierLoc.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 146);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 13);
            this.label11.TabIndex = 43;
            this.label11.Text = "Désignation :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "Quarier/Localité :";
            // 
            // btnCloseComCheffQuartierLoc
            // 
            this.btnCloseComCheffQuartierLoc.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnCloseComCheffQuartierLoc.BackColor = System.Drawing.Color.Transparent;
            this.btnCloseComCheffQuartierLoc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCloseComCheffQuartierLoc.Image = global::JOVIA_WIN.Properties.Resources.mp_cancelsearch_cu_s_2x;
            this.btnCloseComCheffQuartierLoc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseComCheffQuartierLoc.Name = "btnCloseComCheffQuartierLoc";
            this.btnCloseComCheffQuartierLoc.Size = new System.Drawing.Size(23, 22);
            this.btnCloseComCheffQuartierLoc.Text = "Quitter";
            this.btnCloseComCheffQuartierLoc.Click += new System.EventHandler(this.btnCloseComCheffQuartierLoc_Click);
            // 
            // bindingNavigator2
            // 
            this.bindingNavigator2.AddNewItem = null;
            this.bindingNavigator2.BackColor = System.Drawing.Color.Transparent;
            this.bindingNavigator2.CountItem = null;
            this.bindingNavigator2.DeleteItem = null;
            this.bindingNavigator2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bindingNavigator2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCloseComCheffQuartierLoc});
            this.bindingNavigator2.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator2.MoveFirstItem = null;
            this.bindingNavigator2.MoveLastItem = null;
            this.bindingNavigator2.MoveNextItem = null;
            this.bindingNavigator2.MovePreviousItem = null;
            this.bindingNavigator2.Name = "bindingNavigator2";
            this.bindingNavigator2.PositionItem = null;
            this.bindingNavigator2.Size = new System.Drawing.Size(598, 25);
            this.bindingNavigator2.TabIndex = 66;
            this.bindingNavigator2.Text = "Avenu/Village";
            // 
            // bnvCandidat
            // 
            this.bnvCandidat.AddNewItem = null;
            this.bnvCandidat.BackColor = System.Drawing.Color.Transparent;
            this.bnvCandidat.CountItem = null;
            this.bnvCandidat.DeleteItem = null;
            this.bnvCandidat.Dock = System.Windows.Forms.DockStyle.None;
            this.bnvCandidat.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bnvCandidat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddAvenueVillage,
            this.btnRefrehAvenueVillage,
            this.btnSaveAvenueVillage,
            this.btnModifierAvenueVillage,
            this.btnDeleteAvenueVillage});
            this.bnvCandidat.Location = new System.Drawing.Point(6, 29);
            this.bnvCandidat.MoveFirstItem = null;
            this.bnvCandidat.MoveLastItem = null;
            this.bnvCandidat.MoveNextItem = null;
            this.bnvCandidat.MovePreviousItem = null;
            this.bnvCandidat.Name = "bnvCandidat";
            this.bnvCandidat.PositionItem = null;
            this.bnvCandidat.Size = new System.Drawing.Size(149, 25);
            this.bnvCandidat.TabIndex = 70;
            this.bnvCandidat.Text = "bindingNavigator1";
            // 
            // btnAddAvenueVillage
            // 
            this.btnAddAvenueVillage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddAvenueVillage.Image = global::JOVIA_WIN.Properties.Resources.navBarAddIcon_2x;
            this.btnAddAvenueVillage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddAvenueVillage.Name = "btnAddAvenueVillage";
            this.btnAddAvenueVillage.Size = new System.Drawing.Size(23, 22);
            this.btnAddAvenueVillage.Text = "Ajouter";
            this.btnAddAvenueVillage.Click += new System.EventHandler(this.btnAddAvenueVillage_Click);
            // 
            // btnRefrehAvenueVillage
            // 
            this.btnRefrehAvenueVillage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefrehAvenueVillage.Image = global::JOVIA_WIN.Properties.Resources.update1;
            this.btnRefrehAvenueVillage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefrehAvenueVillage.Name = "btnRefrehAvenueVillage";
            this.btnRefrehAvenueVillage.Size = new System.Drawing.Size(23, 22);
            this.btnRefrehAvenueVillage.Text = "Refresh";
            this.btnRefrehAvenueVillage.Click += new System.EventHandler(this.btnRefrehAvenueVillage_Click);
            // 
            // btnSaveAvenueVillage
            // 
            this.btnSaveAvenueVillage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveAvenueVillage.Image = global::JOVIA_WIN.Properties.Resources.check_2x1;
            this.btnSaveAvenueVillage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAvenueVillage.Name = "btnSaveAvenueVillage";
            this.btnSaveAvenueVillage.Size = new System.Drawing.Size(23, 22);
            this.btnSaveAvenueVillage.Text = "Enregistrer";
            this.btnSaveAvenueVillage.Click += new System.EventHandler(this.btnSaveAvenueVillage_Click);
            // 
            // btnModifierAvenueVillage
            // 
            this.btnModifierAvenueVillage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnModifierAvenueVillage.Image = ((System.Drawing.Image)(resources.GetObject("btnModifierAvenueVillage.Image")));
            this.btnModifierAvenueVillage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModifierAvenueVillage.Name = "btnModifierAvenueVillage";
            this.btnModifierAvenueVillage.Size = new System.Drawing.Size(23, 22);
            this.btnModifierAvenueVillage.Text = "Modifier";
            this.btnModifierAvenueVillage.Click += new System.EventHandler(this.btnModifierAvenueVillage_Click);
            // 
            // btnDeleteAvenueVillage
            // 
            this.btnDeleteAvenueVillage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteAvenueVillage.Image = global::JOVIA_WIN.Properties.Resources.mp_delete_md_n_lt_2x1;
            this.btnDeleteAvenueVillage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteAvenueVillage.Name = "btnDeleteAvenueVillage";
            this.btnDeleteAvenueVillage.Size = new System.Drawing.Size(23, 22);
            this.btnDeleteAvenueVillage.Text = "Supprimer";
            this.btnDeleteAvenueVillage.Click += new System.EventHandler(this.btnDeleteAvenueVillage_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboAveniVillage);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(0, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(595, 41);
            this.groupBox1.TabIndex = 71;
            this.groupBox1.TabStop = false;
            // 
            // AvenueVillageFormServeur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::JOVIA_WIN.Properties.Resources.img_emissions_player_background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.bnvCandidat);
            this.Controls.Add(this.bindingNavigator2);
            this.Controls.Add(this.lblIdAvenueVillage);
            this.Controls.Add(this.dgvAvenueVillage);
            this.Controls.Add(this.cboIdQuartierLoc);
            this.Controls.Add(this.lblIdQuartierLoc);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDesignationAvenueVillage);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "AvenueVillageFormServeur";
            this.Size = new System.Drawing.Size(598, 346);
            this.Load += new System.EventHandler(this.AvenueVillageFormServeur_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAvenueVillage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator2)).EndInit();
            this.bindingNavigator2.ResumeLayout(false);
            this.bindingNavigator2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bnvCandidat)).EndInit();
            this.bnvCandidat.ResumeLayout(false);
            this.bnvCandidat.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblIdAvenueVillage;
        private System.Windows.Forms.DataGridView dgvAvenueVillage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDesignationAvenueVillage;
        private System.Windows.Forms.ComboBox cboIdQuartierLoc;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripButton btnCloseComCheffQuartierLoc;
        private System.Windows.Forms.BindingNavigator bindingNavigator2;
        private System.Windows.Forms.Label lblIdQuartierLoc;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboAveniVillage;
        private System.Windows.Forms.BindingNavigator bnvCandidat;
        private System.Windows.Forms.ToolStripButton btnAddAvenueVillage;
        private System.Windows.Forms.ToolStripButton btnRefrehAvenueVillage;
        private System.Windows.Forms.ToolStripButton btnSaveAvenueVillage;
        private System.Windows.Forms.ToolStripButton btnModifierAvenueVillage;
        private System.Windows.Forms.ToolStripButton btnDeleteAvenueVillage;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
