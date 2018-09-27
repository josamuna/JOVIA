namespace JOVIA_WIN
{
    partial class ServeurOperation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServeurOperation));
            this.dgvServeur = new System.Windows.Forms.DataGridView();
            this.bdNavProvVilleTer = new System.Windows.Forms.BindingNavigator(this.components);
            this.btnCloseManipServeur = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cboIPServeur = new System.Windows.Forms.ComboBox();
            this.cboIdProvince = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDesignationServeur = new System.Windows.Forms.TextBox();
            this.lblIdServeur = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bdNavigateProvince = new System.Windows.Forms.BindingNavigator(this.components);
            this.btnAddServeur = new System.Windows.Forms.ToolStripButton();
            this.btnRefreshServeur = new System.Windows.Forms.ToolStripButton();
            this.btnSaveServeur = new System.Windows.Forms.ToolStripButton();
            this.btnUpdateServeur = new System.Windows.Forms.ToolStripButton();
            this.btnDeleteServeur = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvServeur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdNavProvVilleTer)).BeginInit();
            this.bdNavProvVilleTer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdNavigateProvince)).BeginInit();
            this.bdNavigateProvince.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvServeur
            // 
            this.dgvServeur.AllowUserToAddRows = false;
            this.dgvServeur.AllowUserToDeleteRows = false;
            this.dgvServeur.BackgroundColor = System.Drawing.Color.White;
            this.dgvServeur.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvServeur.Location = new System.Drawing.Point(15, 182);
            this.dgvServeur.MultiSelect = false;
            this.dgvServeur.Name = "dgvServeur";
            this.dgvServeur.ReadOnly = true;
            this.dgvServeur.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvServeur.Size = new System.Drawing.Size(405, 158);
            this.dgvServeur.TabIndex = 2;
            this.dgvServeur.SelectionChanged += new System.EventHandler(this.dgvServeur_SelectionChanged);
            // 
            // bdNavProvVilleTer
            // 
            this.bdNavProvVilleTer.AddNewItem = null;
            this.bdNavProvVilleTer.BackColor = System.Drawing.Color.Transparent;
            this.bdNavProvVilleTer.CountItem = null;
            this.bdNavProvVilleTer.DeleteItem = null;
            this.bdNavProvVilleTer.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bdNavProvVilleTer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCloseManipServeur,
            this.toolStripSeparator1});
            this.bdNavProvVilleTer.Location = new System.Drawing.Point(0, 0);
            this.bdNavProvVilleTer.MoveFirstItem = null;
            this.bdNavProvVilleTer.MoveLastItem = null;
            this.bdNavProvVilleTer.MoveNextItem = null;
            this.bdNavProvVilleTer.MovePreviousItem = null;
            this.bdNavProvVilleTer.Name = "bdNavProvVilleTer";
            this.bdNavProvVilleTer.PositionItem = null;
            this.bdNavProvVilleTer.Size = new System.Drawing.Size(432, 25);
            this.bdNavProvVilleTer.TabIndex = 62;
            this.bdNavProvVilleTer.Text = "bindingNavigator1";
            // 
            // btnCloseManipServeur
            // 
            this.btnCloseManipServeur.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnCloseManipServeur.BackColor = System.Drawing.Color.Transparent;
            this.btnCloseManipServeur.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCloseManipServeur.Image = global::JOVIA_WIN.Properties.Resources.Close;
            this.btnCloseManipServeur.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseManipServeur.Name = "btnCloseManipServeur";
            this.btnCloseManipServeur.Size = new System.Drawing.Size(23, 22);
            this.btnCloseManipServeur.Text = "Quitter";
            this.btnCloseManipServeur.Click += new System.EventHandler(this.btnCloseManipServeur_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cboIPServeur
            // 
            this.cboIPServeur.FormattingEnabled = true;
            this.cboIPServeur.Location = new System.Drawing.Point(109, 155);
            this.cboIPServeur.Name = "cboIPServeur";
            this.cboIPServeur.Size = new System.Drawing.Size(147, 21);
            this.cboIPServeur.TabIndex = 70;
            // 
            // cboIdProvince
            // 
            this.cboIdProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIdProvince.FormattingEnabled = true;
            this.cboIdProvince.Location = new System.Drawing.Point(109, 101);
            this.cboIdProvince.Name = "cboIdProvince";
            this.cboIdProvince.Size = new System.Drawing.Size(227, 21);
            this.cboIdProvince.TabIndex = 69;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 20);
            this.label2.TabIndex = 68;
            this.label2.Text = "Province :";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 158);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(68, 21);
            this.label6.TabIndex = 66;
            this.label6.Text = "Adresse IP :";
            // 
            // txtDesignationServeur
            // 
            this.txtDesignationServeur.Location = new System.Drawing.Point(109, 128);
            this.txtDesignationServeur.Name = "txtDesignationServeur";
            this.txtDesignationServeur.Size = new System.Drawing.Size(227, 20);
            this.txtDesignationServeur.TabIndex = 63;
            // 
            // lblIdServeur
            // 
            this.lblIdServeur.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblIdServeur.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblIdServeur.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblIdServeur.Location = new System.Drawing.Point(109, 72);
            this.lblIdServeur.Name = "lblIdServeur";
            this.lblIdServeur.Size = new System.Drawing.Size(136, 23);
            this.lblIdServeur.TabIndex = 67;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(14, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 21);
            this.label5.TabIndex = 65;
            this.label5.Text = "Code :";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(12, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 21);
            this.label4.TabIndex = 64;
            this.label4.Text = "Désignation :";
            // 
            // bdNavigateProvince
            // 
            this.bdNavigateProvince.AddNewItem = null;
            this.bdNavigateProvince.BackColor = System.Drawing.Color.Transparent;
            this.bdNavigateProvince.CountItem = null;
            this.bdNavigateProvince.DeleteItem = null;
            this.bdNavigateProvince.Dock = System.Windows.Forms.DockStyle.None;
            this.bdNavigateProvince.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bdNavigateProvince.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddServeur,
            this.btnRefreshServeur,
            this.btnSaveServeur,
            this.btnUpdateServeur,
            this.btnDeleteServeur});
            this.bdNavigateProvince.Location = new System.Drawing.Point(4, 27);
            this.bdNavigateProvince.MoveFirstItem = null;
            this.bdNavigateProvince.MoveLastItem = null;
            this.bdNavigateProvince.MoveNextItem = null;
            this.bdNavigateProvince.MovePreviousItem = null;
            this.bdNavigateProvince.Name = "bdNavigateProvince";
            this.bdNavigateProvince.PositionItem = null;
            this.bdNavigateProvince.Size = new System.Drawing.Size(118, 25);
            this.bdNavigateProvince.TabIndex = 71;
            this.bdNavigateProvince.Text = "bindingNavigator1";
            // 
            // btnAddServeur
            // 
            this.btnAddServeur.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAddServeur.Image = global::JOVIA_WIN.Properties.Resources.navBarAddIcon_2x;
            this.btnAddServeur.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddServeur.Name = "btnAddServeur";
            this.btnAddServeur.Size = new System.Drawing.Size(23, 22);
            this.btnAddServeur.Text = "Ajouter";
            // 
            // btnRefreshServeur
            // 
            this.btnRefreshServeur.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefreshServeur.Image = global::JOVIA_WIN.Properties.Resources.update1;
            this.btnRefreshServeur.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshServeur.Name = "btnRefreshServeur";
            this.btnRefreshServeur.Size = new System.Drawing.Size(23, 22);
            this.btnRefreshServeur.Text = "Refresh";
            // 
            // btnSaveServeur
            // 
            this.btnSaveServeur.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveServeur.Image = global::JOVIA_WIN.Properties.Resources.check_2x1;
            this.btnSaveServeur.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveServeur.Name = "btnSaveServeur";
            this.btnSaveServeur.Size = new System.Drawing.Size(23, 22);
            this.btnSaveServeur.Text = "Enregistrer";
            // 
            // btnUpdateServeur
            // 
            this.btnUpdateServeur.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUpdateServeur.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateServeur.Image")));
            this.btnUpdateServeur.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpdateServeur.Name = "btnUpdateServeur";
            this.btnUpdateServeur.Size = new System.Drawing.Size(23, 22);
            this.btnUpdateServeur.Text = "Modifier";
            // 
            // btnDeleteServeur
            // 
            this.btnDeleteServeur.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteServeur.Image = global::JOVIA_WIN.Properties.Resources.mp_delete_md_n_lt_2x1;
            this.btnDeleteServeur.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDeleteServeur.Name = "btnDeleteServeur";
            this.btnDeleteServeur.Size = new System.Drawing.Size(23, 22);
            this.btnDeleteServeur.Text = "Supprimer";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(3, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 37);
            this.groupBox1.TabIndex = 72;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(160, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 15);
            this.label1.TabIndex = 73;
            this.label1.Text = "Opération sur le serveur";
            // 
            // ServeurOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::JOVIA_WIN.Properties.Resources.img_home_news_background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvServeur);
            this.Controls.Add(this.bdNavigateProvince);
            this.Controls.Add(this.cboIPServeur);
            this.Controls.Add(this.cboIdProvince);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtDesignationServeur);
            this.Controls.Add(this.lblIdServeur);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.bdNavProvVilleTer);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "ServeurOperation";
            this.Size = new System.Drawing.Size(432, 353);
            this.Load += new System.EventHandler(this.ServeurOperation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvServeur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bdNavProvVilleTer)).EndInit();
            this.bdNavProvVilleTer.ResumeLayout(false);
            this.bdNavProvVilleTer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bdNavigateProvince)).EndInit();
            this.bdNavigateProvince.ResumeLayout(false);
            this.bdNavigateProvince.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvServeur;
        private System.Windows.Forms.BindingNavigator bdNavProvVilleTer;
        private System.Windows.Forms.ToolStripButton btnCloseManipServeur;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ComboBox cboIPServeur;
        private System.Windows.Forms.ComboBox cboIdProvince;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDesignationServeur;
        private System.Windows.Forms.Label lblIdServeur;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.BindingNavigator bdNavigateProvince;
        private System.Windows.Forms.ToolStripButton btnAddServeur;
        private System.Windows.Forms.ToolStripButton btnRefreshServeur;
        private System.Windows.Forms.ToolStripButton btnSaveServeur;
        private System.Windows.Forms.ToolStripButton btnUpdateServeur;
        private System.Windows.Forms.ToolStripButton btnDeleteServeur;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;

    }
}
