namespace JOVIA_WIN
{
    partial class ParametreWebCamServeur
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
            this.bindingNavigator2 = new System.Windows.Forms.BindingNavigator(this.components);
            this.btnClosePersonneCarte = new System.Windows.Forms.ToolStripButton();
            this.dlgSavePhoto = new System.Windows.Forms.SaveFileDialog();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCapture = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.picturePreview = new System.Windows.Forms.PictureBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboAudioSource = new System.Windows.Forms.ComboBox();
            this.cboVideoSource = new System.Windows.Forms.ComboBox();
            this.ptbxCapture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator2)).BeginInit();
            this.bindingNavigator2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picturePreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbxCapture)).BeginInit();
            this.SuspendLayout();
            // 
            // bindingNavigator2
            // 
            this.bindingNavigator2.AddNewItem = null;
            this.bindingNavigator2.BackColor = System.Drawing.Color.Transparent;
            this.bindingNavigator2.CountItem = null;
            this.bindingNavigator2.DeleteItem = null;
            this.bindingNavigator2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bindingNavigator2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnClosePersonneCarte});
            this.bindingNavigator2.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator2.MoveFirstItem = null;
            this.bindingNavigator2.MoveLastItem = null;
            this.bindingNavigator2.MoveNextItem = null;
            this.bindingNavigator2.MovePreviousItem = null;
            this.bindingNavigator2.Name = "bindingNavigator2";
            this.bindingNavigator2.PositionItem = null;
            this.bindingNavigator2.Size = new System.Drawing.Size(612, 25);
            this.bindingNavigator2.TabIndex = 65;
            this.bindingNavigator2.Text = "bindingNavigator2";
            // 
            // btnClosePersonneCarte
            // 
            this.btnClosePersonneCarte.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnClosePersonneCarte.BackColor = System.Drawing.Color.Transparent;
            this.btnClosePersonneCarte.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClosePersonneCarte.Image = global::JOVIA_WIN.Properties.Resources.Close;
            this.btnClosePersonneCarte.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClosePersonneCarte.Name = "btnClosePersonneCarte";
            this.btnClosePersonneCarte.Size = new System.Drawing.Size(23, 22);
            this.btnClosePersonneCarte.Text = "Quitter";
            this.btnClosePersonneCarte.Click += new System.EventHandler(this.btnClosePersonneCarte_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.White;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.ForeColor = System.Drawing.Color.Navy;
            this.btnSave.Location = new System.Drawing.Point(387, 356);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 25);
            this.btnSave.TabIndex = 74;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCapture
            // 
            this.btnCapture.BackColor = System.Drawing.Color.White;
            this.btnCapture.Enabled = false;
            this.btnCapture.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCapture.ForeColor = System.Drawing.Color.Navy;
            this.btnCapture.Location = new System.Drawing.Point(281, 356);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(100, 25);
            this.btnCapture.TabIndex = 73;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = false;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.BackgroundImage = global::JOVIA_WIN.Properties.Resources.img_home_news_background;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.picturePreview);
            this.groupBox1.Controls.Add(this.btnApply);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cboAudioSource);
            this.groupBox1.Controls.Add(this.cboVideoSource);
            this.groupBox1.Location = new System.Drawing.Point(11, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 344);
            this.groupBox1.TabIndex = 71;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Location = new System.Drawing.Point(97, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Configuration";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(14, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Preview";
            // 
            // picturePreview
            // 
            this.picturePreview.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picturePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picturePreview.Location = new System.Drawing.Point(17, 169);
            this.picturePreview.Name = "picturePreview";
            this.picturePreview.Size = new System.Drawing.Size(228, 169);
            this.picturePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picturePreview.TabIndex = 2;
            this.picturePreview.TabStop = false;
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnApply.ForeColor = System.Drawing.Color.Navy;
            this.btnApply.Location = new System.Drawing.Point(160, 129);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(87, 25);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(9, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Audio Source";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(9, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Video Source";
            // 
            // cboAudioSource
            // 
            this.cboAudioSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAudioSource.FormattingEnabled = true;
            this.cboAudioSource.Location = new System.Drawing.Point(12, 93);
            this.cboAudioSource.Name = "cboAudioSource";
            this.cboAudioSource.Size = new System.Drawing.Size(235, 21);
            this.cboAudioSource.TabIndex = 3;
            // 
            // cboVideoSource
            // 
            this.cboVideoSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboVideoSource.FormattingEnabled = true;
            this.cboVideoSource.Location = new System.Drawing.Point(12, 43);
            this.cboVideoSource.Name = "cboVideoSource";
            this.cboVideoSource.Size = new System.Drawing.Size(235, 21);
            this.cboVideoSource.TabIndex = 2;
            // 
            // ptbxCapture
            // 
            this.ptbxCapture.BackColor = System.Drawing.Color.White;
            this.ptbxCapture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ptbxCapture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ptbxCapture.Location = new System.Drawing.Point(281, 37);
            this.ptbxCapture.Name = "ptbxCapture";
            this.ptbxCapture.Size = new System.Drawing.Size(320, 300);
            this.ptbxCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbxCapture.TabIndex = 72;
            this.ptbxCapture.TabStop = false;
            // 
            // ParametreWebCamServeur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::JOVIA_WIN.Properties.Resources.img_home_player_background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ptbxCapture);
            this.Controls.Add(this.bindingNavigator2);
            this.DoubleBuffered = true;
            this.Name = "ParametreWebCamServeur";
            this.Size = new System.Drawing.Size(612, 391);
            this.Load += new System.EventHandler(this.ParametreWebCamServeur_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator2)).EndInit();
            this.bindingNavigator2.ResumeLayout(false);
            this.bindingNavigator2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picturePreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbxCapture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingNavigator bindingNavigator2;
        private System.Windows.Forms.ToolStripButton btnClosePersonneCarte;
        private System.Windows.Forms.SaveFileDialog dlgSavePhoto;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox picturePreview;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboAudioSource;
        private System.Windows.Forms.ComboBox cboVideoSource;
        private System.Windows.Forms.PictureBox ptbxCapture;
    }
}
