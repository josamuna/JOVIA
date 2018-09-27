namespace JOVIA_WIN
{
    partial class ConnectBdServeur
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectBdServeur));
            this.txtPwdUserSimple = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtUserSimple = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.bnavSvc = new System.Windows.Forms.BindingNavigator(this.components);
            this.btfermer = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTypeDB = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtBD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboServeur = new System.Windows.Forms.ComboBox();
            this.txtMotPass = new System.Windows.Forms.TextBox();
            this.txtNomUser = new System.Windows.Forms.TextBox();
            this.lblPwdUser = new System.Windows.Forms.Label();
            this.lblUserId = new System.Windows.Forms.Label();
            this.lblServeur = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bnavSvc)).BeginInit();
            this.bnavSvc.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPwdUserSimple
            // 
            this.txtPwdUserSimple.BackColor = System.Drawing.Color.White;
            this.txtPwdUserSimple.ForeColor = System.Drawing.Color.SeaShell;
            this.txtPwdUserSimple.Location = new System.Drawing.Point(108, 41);
            this.txtPwdUserSimple.Name = "txtPwdUserSimple";
            this.txtPwdUserSimple.PasswordChar = '*';
            this.txtPwdUserSimple.Size = new System.Drawing.Size(286, 20);
            this.txtPwdUserSimple.TabIndex = 7;
            this.txtPwdUserSimple.Text = "superpasswordserveur";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.txtPwdUserSimple);
            this.groupBox3.Controls.Add(this.txtUserSimple);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(23, 244);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(403, 76);
            this.groupBox3.TabIndex = 83;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Coordonnées de l\'Utilisateur";
            // 
            // txtUserSimple
            // 
            this.txtUserSimple.BackColor = System.Drawing.Color.White;
            this.txtUserSimple.Location = new System.Drawing.Point(109, 15);
            this.txtUserSimple.Name = "txtUserSimple";
            this.txtUserSimple.Size = new System.Drawing.Size(285, 20);
            this.txtUserSimple.TabIndex = 6;
            this.txtUserSimple.Text = "superuserserveur";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 42;
            this.label7.Text = "Mot de passe :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 13);
            this.label8.TabIndex = 41;
            this.label8.Text = "Nom utilisateur  :";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(243)))), ((int)(((byte)(244)))));
            this.groupBox2.Controls.Add(this.cmdCancel);
            this.groupBox2.Controls.Add(this.cmdConnect);
            this.groupBox2.Location = new System.Drawing.Point(250, 333);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(176, 51);
            this.groupBox2.TabIndex = 84;
            this.groupBox2.TabStop = false;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancel.Image")));
            this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCancel.Location = new System.Drawing.Point(94, 13);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(74, 26);
            this.cmdCancel.TabIndex = 9;
            this.cmdCancel.Text = "&Annuler";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdConnect
            // 
            this.cmdConnect.Image = ((System.Drawing.Image)(resources.GetObject("cmdConnect.Image")));
            this.cmdConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdConnect.Location = new System.Drawing.Point(6, 13);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(82, 26);
            this.cmdConnect.TabIndex = 8;
            this.cmdConnect.Text = "Co&nnecter";
            this.cmdConnect.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // bnavSvc
            // 
            this.bnavSvc.AddNewItem = null;
            this.bnavSvc.BackColor = System.Drawing.Color.Transparent;
            this.bnavSvc.CountItem = null;
            this.bnavSvc.DeleteItem = null;
            this.bnavSvc.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bnavSvc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btfermer});
            this.bnavSvc.Location = new System.Drawing.Point(0, 0);
            this.bnavSvc.MoveFirstItem = null;
            this.bnavSvc.MoveLastItem = null;
            this.bnavSvc.MoveNextItem = null;
            this.bnavSvc.MovePreviousItem = null;
            this.bnavSvc.Name = "bnavSvc";
            this.bnavSvc.PositionItem = null;
            this.bnavSvc.Size = new System.Drawing.Size(449, 25);
            this.bnavSvc.TabIndex = 81;
            this.bnavSvc.Text = "bindingNavigator1";
            // 
            // btfermer
            // 
            this.btfermer.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btfermer.BackColor = System.Drawing.Color.Transparent;
            this.btfermer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btfermer.Image = global::JOVIA_WIN.Properties.Resources.Close;
            this.btfermer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btfermer.Name = "btfermer";
            this.btfermer.Size = new System.Drawing.Size(23, 22);
            this.btfermer.Text = "Fermer";
            this.btfermer.Click += new System.EventHandler(this.btfermer_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundImage = global::JOVIA_WIN.Properties.Resources.img_home_news_background;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboTypeDB);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.lblPort);
            this.groupBox1.Controls.Add(this.txtBD);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboServeur);
            this.groupBox1.Controls.Add(this.txtMotPass);
            this.groupBox1.Controls.Add(this.txtNomUser);
            this.groupBox1.Controls.Add(this.lblPwdUser);
            this.groupBox1.Controls.Add(this.lblUserId);
            this.groupBox1.Controls.Add(this.lblServeur);
            this.groupBox1.Location = new System.Drawing.Point(17, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(418, 357);
            this.groupBox1.TabIndex = 82;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(100, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 15);
            this.label1.TabIndex = 32;
            this.label1.Text = "Veuillez entrez les informations de connexion :";
            // 
            // cboTypeDB
            // 
            this.cboTypeDB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTypeDB.FormattingEnabled = true;
            this.cboTypeDB.Location = new System.Drawing.Point(165, 44);
            this.cboTypeDB.Name = "cboTypeDB";
            this.cboTypeDB.Size = new System.Drawing.Size(235, 21);
            this.cboTypeDB.TabIndex = 0;
            this.cboTypeDB.SelectedValueChanged += new System.EventHandler(this.cboTypeDB_SelectedValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(142, 13);
            this.label6.TabIndex = 31;
            this.label6.Text = "Type de base des données :";
            // 
            // txtPort
            // 
            this.txtPort.BackColor = System.Drawing.Color.White;
            this.txtPort.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtPort.Location = new System.Drawing.Point(167, 124);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(79, 20);
            this.txtPort.TabIndex = 3;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(17, 128);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(80, 13);
            this.lblPort.TabIndex = 29;
            this.lblPort.Text = "Numéro de port";
            // 
            // txtBD
            // 
            this.txtBD.BackColor = System.Drawing.Color.White;
            this.txtBD.Location = new System.Drawing.Point(165, 98);
            this.txtBD.Name = "txtBD";
            this.txtBD.Size = new System.Drawing.Size(235, 20);
            this.txtBD.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 15);
            this.label2.TabIndex = 28;
            this.label2.Text = "Base de données :";
            // 
            // cboServeur
            // 
            this.cboServeur.BackColor = System.Drawing.Color.White;
            this.cboServeur.FormattingEnabled = true;
            this.cboServeur.Location = new System.Drawing.Point(165, 71);
            this.cboServeur.Name = "cboServeur";
            this.cboServeur.Size = new System.Drawing.Size(235, 21);
            this.cboServeur.TabIndex = 1;
            // 
            // txtMotPass
            // 
            this.txtMotPass.BackColor = System.Drawing.Color.White;
            this.txtMotPass.ForeColor = System.Drawing.Color.SeaShell;
            this.txtMotPass.Location = new System.Drawing.Point(167, 178);
            this.txtMotPass.Name = "txtMotPass";
            this.txtMotPass.PasswordChar = '*';
            this.txtMotPass.Size = new System.Drawing.Size(233, 20);
            this.txtMotPass.TabIndex = 5;
            this.txtMotPass.Text = "ijn";
            // 
            // txtNomUser
            // 
            this.txtNomUser.BackColor = System.Drawing.Color.White;
            this.txtNomUser.Location = new System.Drawing.Point(167, 152);
            this.txtNomUser.Name = "txtNomUser";
            this.txtNomUser.Size = new System.Drawing.Size(233, 20);
            this.txtNomUser.TabIndex = 4;
            // 
            // lblPwdUser
            // 
            this.lblPwdUser.AutoSize = true;
            this.lblPwdUser.Location = new System.Drawing.Point(16, 177);
            this.lblPwdUser.Name = "lblPwdUser";
            this.lblPwdUser.Size = new System.Drawing.Size(77, 13);
            this.lblPwdUser.TabIndex = 23;
            this.lblPwdUser.Text = "Mot de passe :";
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(16, 152);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(101, 13);
            this.lblUserId.TabIndex = 22;
            this.lblUserId.Text = "Nom de l\'utilisateur :";
            // 
            // lblServeur
            // 
            this.lblServeur.AutoSize = true;
            this.lblServeur.Location = new System.Drawing.Point(17, 74);
            this.lblServeur.Name = "lblServeur";
            this.lblServeur.Size = new System.Drawing.Size(88, 13);
            this.lblServeur.TabIndex = 18;
            this.lblServeur.Text = "Nom du serveur :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(188, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 15);
            this.label3.TabIndex = 33;
            this.label3.Text = "Connection";
            // 
            // ConnectBdServeur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::JOVIA_WIN.Properties.Resources.img_emissions_player_background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.bnavSvc);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "ConnectBdServeur";
            this.Size = new System.Drawing.Size(449, 405);
            this.Load += new System.EventHandler(this.ConnectBdServeur_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bnavSvc)).EndInit();
            this.bnavSvc.ResumeLayout(false);
            this.bnavSvc.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPwdUserSimple;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtUserSimple;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.ComboBox cboTypeDB;
        private System.Windows.Forms.ToolStripButton btfermer;
        private System.Windows.Forms.BindingNavigator bnavSvc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtBD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboServeur;
        private System.Windows.Forms.TextBox txtMotPass;
        private System.Windows.Forms.TextBox txtNomUser;
        private System.Windows.Forms.Label lblPwdUser;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.Label lblServeur;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;

    }
}
