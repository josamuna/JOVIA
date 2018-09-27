using System;
using System.Windows.Forms;
using JOVIA_LIB_SERVEUR;
using Microsoft.VisualBasic;

namespace JOVIA_WIN
{
    public partial class AvenueVillageFormServeur : UserControl
    {
        AvenueVillageServeur avenueVill = new AvenueVillageServeur();
        QuartierLocaliteServeur quartierLoc = new QuartierLocaliteServeur();
        PersonneServeur personne = new PersonneServeur();

        public AvenueVillageFormServeur()
        {
            InitializeComponent();
        }

        private void getAttributAvenueVill()
        {
            avenueVill.Id = Convert.ToInt32(lblIdAvenueVillage.Text);
            avenueVill.Designation = txtDesignationAvenueVillage.Text;
        }

        private void refreshAvenueVill()
        {
            dgvAvenueVillage.DataSource = Factory1.Instance.GetAvenueVillages();

            if (dgvAvenueVillage.RowCount > 0)
            {
                btnRefrehAvenueVillage.Enabled = true;
                btnModifierAvenueVillage.Enabled = true;
            }

            int col = 0;
            foreach (DataGridViewColumn dgvc in dgvAvenueVillage.Columns)
            {
                dgvAvenueVillage.AutoResizeColumn(col);
                col++;
            }
        }

        private void initialiseAvenueVill()
        {
            lblIdAvenueVillage.Text = "";
            lblIdQuartierLoc.Text = "";
            txtDesignationAvenueVillage.Clear();
            txtDesignationAvenueVillage.Focus();
        }

        private void dgvAvenueVillage_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvAvenueVillage.SelectedRows.Count > 0)
                {
                    btnModifierAvenueVillage.Enabled = true;
                    btnDeleteAvenueVillage.Enabled = true;

                    avenueVill = (AvenueVillageServeur)dgvAvenueVillage.SelectedRows[0].DataBoundItem;
                    lblIdAvenueVillage.Text = avenueVill.Id.ToString();
                    cboIdQuartierLoc.Text = Factory1.Instance.GetQuartierLocalite(avenueVill.Id_quartierLocalite).ToString();
                    txtDesignationAvenueVillage.Text = Convert.ToString(avenueVill.Designation);
                    lblIdQuartierLoc.Text = Convert.ToString(avenueVill.Id_quartierLocalite);
                }
            }
            catch (Exception) { MessageBox.Show("Erreur dans la zone d'affichage","Erreur d'affichage"); }
        }

        private void cboIdQuartierLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                avenueVill.Id_quartierLocalite = Convert.ToInt32(((QuartierLocaliteServeur)(cboIdQuartierLoc.SelectedItem)).Id);
            }
            catch (Exception) { }
        }

        private void cboAveniVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAveniVillage.Text != "")
            {
                avenueVill = new AvenueVillageServeur();
                avenueVill = (AvenueVillageServeur)cboAveniVillage.SelectedItem;
                lblIdAvenueVillage.Text = avenueVill.Id.ToString();
                cboIdQuartierLoc.Text = Factory1.Instance.GetQuartierLocalite(avenueVill.Id_quartierLocalite).ToString();
                txtDesignationAvenueVillage.Text = Convert.ToString(avenueVill.Designation);
                lblIdQuartierLoc.Text = Convert.ToString(avenueVill.Id_quartierLocalite);
            }
        }

        private void AvenueVillageFormServeur_Load(object sender, EventArgs e)
        {
            //Chargement des combo box : Avenue Village et Recensseur
            try
            {
                cboAveniVillage.DataSource = Factory1.Instance.GetAvenueVillages();
                cboIdQuartierLoc.DataSource = Factory1.Instance.GetQuartierLocalites();
                if (cboIdQuartierLoc.Items.Count > 0) cboIdQuartierLoc.SelectedIndex = 0;
                else { }
            }
            catch (Exception) { }

            cboIdQuartierLoc.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboIdQuartierLoc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            cboAveniVillage.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboAveniVillage.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboAveniVillage.Text = null;

            try
            {
                dgvAvenueVillage.DataSource = Factory1.Instance.GetAvenueVillages();

                //On rend inisible certaines colonne des DataGridView aui recup les dataproperty
                //correspondant aux noms des accesseurs
                int col = 0;
                foreach (DataGridViewColumn dgvc in dgvAvenueVillage.Columns)
                {
                    if (dgvc.DataPropertyName == "Id") dgvc.Visible = false;
                    else if (dgvc.DataPropertyName == "Id_quartierLocalite") dgvc.Visible = false;
                    dgvAvenueVillage.AutoResizeColumn(col);
                    col++;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("erreur lors de l'affichage", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            btnSaveAvenueVillage.Enabled = false;

            if (dgvAvenueVillage.RowCount <= 0)
            {
                btnSaveAvenueVillage.Enabled = false;
                btnModifierAvenueVillage.Enabled = false;
                btnDeleteAvenueVillage.Enabled = false;
            }
        }

        private void btnCloseComCheffQuartierLoc_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnRefrehAvenueVillage_Click(object sender, EventArgs e)
        {
            try
            {
                refreshAvenueVill();
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAddAvenueVillage_Click(object sender, EventArgs e)
        {
            try
            {
                //considerez comme nouveau
                lblIdAvenueVillage.Text = Factory1.Instance.ReNewIdValue(avenueVill).ToString();
                txtDesignationAvenueVillage.Clear();
                lblIdQuartierLoc.Text = "";
                txtDesignationAvenueVillage.Focus();

                btnSaveAvenueVillage.Enabled = true;
            }
            catch (Exception) { btnSaveAvenueVillage.Enabled = false; }
        }

        private void btnSaveAvenueVillage_Click(object sender, EventArgs e)
        {
            try
            {
                if (avenueVill != null)
                {
                    getAttributAvenueVill();

                    avenueVill.Enregistrer();
                    btnSaveAvenueVillage.Enabled = false;
                    MessageBox.Show("Enregistrement éffectué", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        refreshAvenueVill();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Echec de l'enregistrement, " + ex.Message, "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnModifierAvenueVillage_Click(object sender, EventArgs e)
        {
            try
            {
                if (avenueVill != null)
                {
                    getAttributAvenueVill();

                    avenueVill.Modifier();
                    MessageBox.Show("Modification effectuée!", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        refreshAvenueVill();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Echec de la modification, " + ex.Message, "Modification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnDeleteAvenueVillage_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Voulez - vous vraiment supprimer cet enregistrement ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    avenueVill.Id = Convert.ToInt32(lblIdAvenueVillage.Text);
                    avenueVill.Supprimer();
                    MessageBox.Show("Suppression éffectuée", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        refreshAvenueVill();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    if (dgvAvenueVillage.RowCount <= 0)
                    {
                        btnSaveAvenueVillage.Enabled = false;
                        btnModifierAvenueVillage.Enabled = false;
                        btnDeleteAvenueVillage.Enabled = false;
                    }

                    initialiseAvenueVill();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Echec de la suppression", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
