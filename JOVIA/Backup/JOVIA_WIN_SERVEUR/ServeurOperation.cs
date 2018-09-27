using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JOVIA_LIB_SERVEUR;
using System.Net;

namespace JOVIA_WIN_SERVEUR
{
    public partial class ServeurOperation : UserControl
    {
        Serveur serveur = new Serveur();

        public ServeurOperation()
        {
            InitializeComponent();
        }

        private void btnCloseManipServeur_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void reloadCombo()
        {
            cboIdProvince.DataSource = Factory1.Instance.GetProvinces();
        }

        private void reloadComboIPAdresse()
        {
            cboIPServeur.DataSource = Factory1.Instance.GetServeurIP();
        }

        private void getAttributServeur()
        {
            serveur.Id = Convert.ToInt32(lblIdServeur.Text);
            serveur.Designation = txtDesignationServeur.Text;
            serveur.Adresse_ip = cboIPServeur.Text;
        }

        private void refreshServeur()
        {
            dgvServeur.DataSource = Factory1.Instance.GetServeurs();

            if (dgvServeur.RowCount > 0)
            {
                btnUpdateServeur.Enabled = true;
                btnDeleteServeur.Enabled = true;
            }
        }

        private void initialiseServeur()
        {
            lblIdServeur.Text = "";
            txtDesignationServeur.Clear();
            cboIPServeur.Text = "";
            txtDesignationServeur.Focus();
        }

        private void ServeurOperation_Load(object sender, EventArgs e)
        {
            btnSaveServeur.Enabled = false;

            try
            {
                reloadCombo();

                dgvServeur.DataSource = Factory1.Instance.GetServeurs();

                //On rend invisible certaines colonne des DataGridView qui recup les dataproperty
                //correspondant aux noms des accesseurs
                int col = 0;

                foreach (DataGridViewColumn dgvc in dgvServeur.Columns)
                {
                    if (dgvc.DataPropertyName == "Id") dgvc.Visible = false;
                    else if (dgvc.DataPropertyName == "Id_province") dgvc.Visible = false;
                    dgvServeur.AutoResizeColumn(col);
                    col++;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("erreur lors de l'affichage", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            cboIPServeur.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboIPServeur.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            if (dgvServeur.RowCount <= 0)
            {
                btnSaveServeur.Enabled = false;
                btnUpdateServeur.Enabled = false;
                btnDeleteServeur.Enabled = false;
            }
        }

        private void cboIdProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                serveur.Id_province = Convert.ToInt32(((Province)cboIdProvince.SelectedItem).Id);
            }
            catch (Exception) { }
        }

        private void dgvServeur_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvServeur.SelectedRows.Count > 0)
                {
                    btnUpdateServeur.Enabled = true;
                    btnDeleteServeur.Enabled = true;
                    serveur = (Serveur)dgvServeur.SelectedRows[0].DataBoundItem;
                    //SetBindings();

                    lblIdServeur.Text = Convert.ToString(serveur.Id);
                    cboIdProvince.Text = Factory1.Instance.GetPronvince(serveur.Id_province).Designation;
                    txtDesignationServeur.Text = serveur.Designation;
                    cboIPServeur.Text = serveur.Adresse_ip;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur dans la zone d'affichage", "Erreur d'affichage");
            }
        }

        private void btnDeleteServeur_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Voulez - vous supprimer cet enregistrement ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    serveur.Id = Convert.ToInt32(lblIdServeur.Text);
                    serveur.Supprimmer();

                    reloadComboIPAdresse();

                    MessageBox.Show("Suppression éffectuée", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        refreshServeur();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    if (dgvServeur.RowCount <= 0)
                    {
                        btnSaveServeur.Enabled = false;
                        btnUpdateServeur.Enabled = false;
                        btnDeleteServeur.Enabled = false;
                    }

                    initialiseServeur();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Echec de la suppression", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnUpdateServeur_Click(object sender, EventArgs e)
        {
            try
            {
                getAttributServeur();

                serveur.Modifier();

                reloadComboIPAdresse();

                MessageBox.Show("Modification éffectuée", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                try
                {
                    refreshServeur();
                }
                catch (Exception)
                {
                    MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Echec de la modification, " + ex.Message, "Modification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnRefreshServeur_Click(object sender, EventArgs e)
        {
            try
            {
                refreshServeur();
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSaveServeur_Click(object sender, EventArgs e)
        {
            try
            {
                getAttributServeur();

                serveur.Enregistrer();

                reloadComboIPAdresse();

                btnSaveServeur.Enabled = false;

                try
                {
                    refreshServeur();
                }
                catch (Exception)
                {
                    MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                MessageBox.Show("Enregistrement éffectuée", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Echec de l'enregistrement, " + ex.Message, "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAddServeur_Click(object sender, EventArgs e)
        {
            btnSaveServeur.Enabled = true;
            try
            {
                lblIdServeur.Text = Convert.ToString(Factory1.Instance.ReNewIdValue(serveur));
                txtDesignationServeur.Clear();
                cboIPServeur.Text = "";
                txtDesignationServeur.Focus();
            }
            catch (Exception) { btnSaveServeur.Enabled = false; }
        }
    }
}
