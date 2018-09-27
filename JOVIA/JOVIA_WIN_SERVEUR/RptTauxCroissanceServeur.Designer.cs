using System;
using System.Windows.Forms;

namespace JOVIA_WIN_SERVEUR
{
    partial class RptTauxCroissanceServeur
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
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.btnclose = new System.Windows.Forms.ToolStripButton();
            this.btClose = new System.Windows.Forms.ToolStripButton();
            this.crvTauxCroissance = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAnneeFin = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAnneeDebut = new System.Windows.Forms.TextBox();
            this.cmdOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboEntite = new System.Windows.Forms.ComboBox();
            this.cboTypeEntite = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.CountItem = null;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnclose,
            this.btClose});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator1.MoveFirstItem = null;
            this.bindingNavigator1.MoveLastItem = null;
            this.bindingNavigator1.MoveNextItem = null;
            this.bindingNavigator1.MovePreviousItem = null;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = null;
            this.bindingNavigator1.Size = new System.Drawing.Size(986, 25);
            this.bindingNavigator1.TabIndex = 1;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // btnclose
            // 
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(23, 22);
            // 
            // btClose
            // 
            this.btClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btClose.Image = global::JOVIA_WIN_SERVEUR.Properties.Resources.Close;
            this.btClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(23, 22);
            this.btClose.Text = "Quitter";
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // crvTauxCroissance
            // 
            this.crvTauxCroissance.ActiveViewIndex = -1;
            this.crvTauxCroissance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvTauxCroissance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvTauxCroissance.Location = new System.Drawing.Point(0, 25);
            this.crvTauxCroissance.Name = "crvTauxCroissance";
            this.crvTauxCroissance.SelectionFormula = "";
            this.crvTauxCroissance.Size = new System.Drawing.Size(986, 594);
            this.crvTauxCroissance.TabIndex = 2;
            this.crvTauxCroissance.ViewTimeSelectionFormula = "";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtAnneeFin);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtAnneeDebut);
            this.groupBox1.Controls.Add(this.cmdOk);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboEntite);
            this.groupBox1.Controls.Add(this.cboTypeEntite);
            this.groupBox1.Location = new System.Drawing.Point(3, 61);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 202);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(6, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 79;
            this.label4.Text = "Année Fin :";
            // 
            // txtAnneeFin
            // 
            this.txtAnneeFin.Location = new System.Drawing.Point(9, 146);
            this.txtAnneeFin.Name = "txtAnneeFin";
            this.txtAnneeFin.Size = new System.Drawing.Size(155, 20);
            this.txtAnneeFin.TabIndex = 78;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(4, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 77;
            this.label2.Text = "Année Début :";
            // 
            // txtAnneeDebut
            // 
            this.txtAnneeDebut.Location = new System.Drawing.Point(7, 106);
            this.txtAnneeDebut.Name = "txtAnneeDebut";
            this.txtAnneeDebut.Size = new System.Drawing.Size(155, 20);
            this.txtAnneeDebut.TabIndex = 76;
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(115, 171);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(49, 21);
            this.cmdOk.TabIndex = 73;
            this.cmdOk.Text = "Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(3, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 71;
            this.label1.Text = "Entité :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(3, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 69;
            this.label3.Text = "Type :";
            // 
            // cboEntite
            // 
            this.cboEntite.FormattingEnabled = true;
            this.cboEntite.Location = new System.Drawing.Point(6, 66);
            this.cboEntite.Name = "cboEntite";
            this.cboEntite.Size = new System.Drawing.Size(156, 21);
            this.cboEntite.TabIndex = 70;
            this.cboEntite.SelectedIndexChanged += new System.EventHandler(this.cboEntinte_SelectedIndexChanged);
            // 
            // cboTypeEntite
            // 
            this.cboTypeEntite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTypeEntite.FormattingEnabled = true;
            this.cboTypeEntite.Items.AddRange(new object[] {
            "Masculin",
            "Feminin"});
            this.cboTypeEntite.Location = new System.Drawing.Point(7, 30);
            this.cboTypeEntite.Name = "cboTypeEntite";
            this.cboTypeEntite.Size = new System.Drawing.Size(155, 21);
            this.cboTypeEntite.TabIndex = 68;
            this.cboTypeEntite.SelectedIndexChanged += new System.EventHandler(this.cboTypeEntite_SelectedIndexChanged);
            // 
            // RptTauxCroissanceServeur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.crvTauxCroissance);
            this.Controls.Add(this.bindingNavigator1);
            this.Name = "RptTauxCroissanceServeur";
            this.Size = new System.Drawing.Size(986, 619);
            this.Load += new System.EventHandler(this.RptTauxCroissanceServeur_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private CrystalDecisions.Windows.Forms.CrystalReportViewer crvVilleTerritoire;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton btnclose;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvTauxCroissance;
        private GroupBox groupBox1;
        private Label label2;
        private TextBox txtAnneeDebut;
        private Button cmdOk;
        private Label label1;
        private Label label3;
        private ComboBox cboEntite;
        private ComboBox cboTypeEntite;
        private Label label4;
        private TextBox txtAnneeFin;
        private ToolStripButton btClose;
    }
}
