namespace JOVIA_WIN
{
    partial class MesageNotInserted
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.bnvCandidat = new System.Windows.Forms.BindingNavigator(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdRefrech = new System.Windows.Forms.ToolStripButton();
            this.btnclose = new System.Windows.Forms.ToolStripButton();
            this.dgvEnvoie = new System.Windows.Forms.DataGridView();
            this.txtExpediteur = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtErreur = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bnvCandidat)).BeginInit();
            this.bnvCandidat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnvoie)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCode
            // 
            this.txtCode.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtCode.Location = new System.Drawing.Point(110, 45);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(129, 20);
            this.txtCode.TabIndex = 0;
            // 
            // txtDate
            // 
            this.txtDate.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtDate.Location = new System.Drawing.Point(112, 228);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(245, 20);
            this.txtDate.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Code:";
            // 
            // txtMessage
            // 
            this.txtMessage.AcceptsTab = true;
            this.txtMessage.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtMessage.Location = new System.Drawing.Point(112, 257);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtMessage.Size = new System.Drawing.Size(246, 149);
            this.txtMessage.TabIndex = 4;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 260);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 43;
            this.label11.Text = "Message:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 231);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "Date:";
            // 
            // bnvCandidat
            // 
            this.bnvCandidat.AddNewItem = null;
            this.bnvCandidat.AutoSize = false;
            this.bnvCandidat.BackColor = System.Drawing.Color.Transparent;
            this.bnvCandidat.CountItem = null;
            this.bnvCandidat.DeleteItem = null;
            this.bnvCandidat.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bnvCandidat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.cmdRefrech,
            this.btnclose});
            this.bnvCandidat.Location = new System.Drawing.Point(0, 0);
            this.bnvCandidat.MoveFirstItem = null;
            this.bnvCandidat.MoveLastItem = null;
            this.bnvCandidat.MoveNextItem = null;
            this.bnvCandidat.MovePreviousItem = null;
            this.bnvCandidat.Name = "bnvCandidat";
            this.bnvCandidat.PositionItem = null;
            this.bnvCandidat.Size = new System.Drawing.Size(595, 25);
            this.bnvCandidat.TabIndex = 59;
            this.bnvCandidat.Text = "bindingNavigator1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // cmdRefrech
            // 
            this.cmdRefrech.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdRefrech.Image = global::JOVIA_WIN.Properties.Resources.update1;
            this.cmdRefrech.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdRefrech.Name = "cmdRefrech";
            this.cmdRefrech.Size = new System.Drawing.Size(23, 22);
            this.cmdRefrech.Text = "Refresh";
            this.cmdRefrech.Click += new System.EventHandler(this.cmdRefrech_Click);
            // 
            // btnclose
            // 
            this.btnclose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnclose.BackColor = System.Drawing.Color.Transparent;
            this.btnclose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnclose.Image = global::JOVIA_WIN.Properties.Resources.Close;
            this.btnclose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(23, 22);
            this.btnclose.Text = "Quitter";
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // dgvEnvoie
            // 
            this.dgvEnvoie.AllowUserToAddRows = false;
            this.dgvEnvoie.AllowUserToDeleteRows = false;
            this.dgvEnvoie.BackgroundColor = System.Drawing.Color.White;
            this.dgvEnvoie.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEnvoie.Location = new System.Drawing.Point(370, 45);
            this.dgvEnvoie.MultiSelect = false;
            this.dgvEnvoie.Name = "dgvEnvoie";
            this.dgvEnvoie.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvEnvoie.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvEnvoie.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEnvoie.Size = new System.Drawing.Size(209, 361);
            this.dgvEnvoie.TabIndex = 5;
            this.dgvEnvoie.SelectionChanged += new System.EventHandler(this.dgvEnvoie_SelectionChanged);
            // 
            // txtExpediteur
            // 
            this.txtExpediteur.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtExpediteur.Location = new System.Drawing.Point(110, 74);
            this.txtExpediteur.Name = "txtExpediteur";
            this.txtExpediteur.Size = new System.Drawing.Size(129, 20);
            this.txtExpediteur.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 61;
            this.label2.Text = "Expediteur :";
            // 
            // txtErreur
            // 
            this.txtErreur.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txtErreur.Location = new System.Drawing.Point(110, 100);
            this.txtErreur.Multiline = true;
            this.txtErreur.Name = "txtErreur";
            this.txtErreur.Size = new System.Drawing.Size(247, 118);
            this.txtErreur.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 63;
            this.label3.Text = "Message d\'erreur :";
            // 
            // MesageNotInserted
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::JOVIA_WIN.Properties.Resources.img_home_news_background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.txtErreur);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtExpediteur);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bnvCandidat);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.dgvEnvoie);
            this.Controls.Add(this.txtDate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtMessage);
            this.DoubleBuffered = true;
            this.Name = "MesageNotInserted";
            this.Size = new System.Drawing.Size(595, 417);
            this.Load += new System.EventHandler(this.MesageNotInserted_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bnvCandidat)).EndInit();
            this.bnvCandidat.ResumeLayout(false);
            this.bnvCandidat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnvoie)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.BindingNavigator bnvCandidat;
        private System.Windows.Forms.ToolStripButton btnclose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton cmdRefrech;
        private System.Windows.Forms.DataGridView dgvEnvoie;
        private System.Windows.Forms.TextBox txtExpediteur;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtErreur;
        private System.Windows.Forms.Label label3;
    }
}
