using System;
using System.Windows.Forms;

namespace JOVIA_WIN_SERVEUR
{
    partial class RptDensiteServeur
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
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
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
            this.btnClose});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator1.MoveFirstItem = null;
            this.bindingNavigator1.MoveLastItem = null;
            this.bindingNavigator1.MoveNextItem = null;
            this.bindingNavigator1.MovePreviousItem = null;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = null;
            this.bindingNavigator1.Size = new System.Drawing.Size(1020, 25);
            this.bindingNavigator1.TabIndex = 1;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // btnClose
            // 
            this.btnClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClose.Image = global::JOVIA_WIN_SERVEUR.Properties.Resources.Close;
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(23, 22);
            this.btnClose.Text = "Quitter";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 25);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.SelectionFormula = "";
            this.crystalReportViewer1.Size = new System.Drawing.Size(1020, 575);
            this.crystalReportViewer1.TabIndex = 2;
            this.crystalReportViewer1.ViewTimeSelectionFormula = "";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.cmdOk);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboEntite);
            this.groupBox1.Controls.Add(this.cboTypeEntite);
            this.groupBox1.Location = new System.Drawing.Point(3, 110);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(172, 127);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(114, 99);
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
            this.label1.Location = new System.Drawing.Point(3, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 71;
            this.label1.Text = "Dans :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(3, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 69;
            this.label3.Text = "selectioner quoi :";
            // 
            // cboEntite
            // 
            this.cboEntite.FormattingEnabled = true;
            this.cboEntite.Location = new System.Drawing.Point(6, 72);
            this.cboEntite.Name = "cboEntite";
            this.cboEntite.Size = new System.Drawing.Size(157, 21);
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
            this.cboTypeEntite.Location = new System.Drawing.Point(6, 32);
            this.cboTypeEntite.Name = "cboTypeEntite";
            this.cboTypeEntite.Size = new System.Drawing.Size(157, 21);
            this.cboTypeEntite.TabIndex = 68;
            this.cboTypeEntite.SelectedIndexChanged += new System.EventHandler(this.cboTypeEntite_SelectedIndexChanged);
            // 
            // RptDensiteServeur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.crystalReportViewer1);
            this.Controls.Add(this.bindingNavigator1);
            this.Name = "RptDensiteServeur";
            this.Size = new System.Drawing.Size(1020, 600);
            this.Load += new System.EventHandler(this.RptDensiteServeur_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private CrystalDecisions.Windows.Forms.CrystalReportViewer crvBureau;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private GroupBox groupBox1;
        private Button cmdOk;
        private Label label1;
        private Label label3;
        private ComboBox cboEntite;
        private ComboBox cboTypeEntite;
        private ToolStripButton btnClose;
    }
}
