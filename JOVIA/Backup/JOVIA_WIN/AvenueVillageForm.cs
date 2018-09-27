using System;
using System.Windows.Forms;
using JOVIA_LIB;
using Microsoft.VisualBasic;

namespace JOVIA_WIN
{
    public partial class AvenueVillageForm : UserControl
    {
        AvenueVillage avenueVill = new AvenueVillage();
        QuartierLocalite quartierLoc = new QuartierLocalite();
        Personne personne = new Personne();

        public AvenueVillageForm()
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
            dgvAvenueVillage.DataSource = Factory.Instance.GetAvenueVillages();

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
                   

                    avenueVill = (AvenueVillage)dgvAvenueVillage.SelectedRows[0].DataBoundItem;
                    lblIdAvenueVillage.Text = avenueVill.Id.ToString();
                    cboIdQuartierLoc.Text = Factory.Instance.GetQuartierLocalite(avenueVill.Id_quartierLocalite).Designation;
                    txtDesignationAvenueVillage.Text = Convert.ToString(avenueVill.Designation);
                    lblIdQuartierLoc.Text = Convert.ToString(avenueVill.Id_quartierLocalite);
                }
            }
            catch (Exception) { MessageBox.Show("Erreur dans la zone d'affichage","Erreur d'affichage"); }
        }

        private void btnSearchAvenueVillage_Click(object sender, EventArgs e)
        {
        }

        private void cboIdQuartierLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                avenueVill.Id_quartierLocalite = Convert.ToInt32(((QuartierLocalite)(cboIdQuartierLoc.SelectedItem)).Id);
            }
            catch (Exception) { }
        }

        private void lblIdQuartierLoc_Click(object sender, EventArgs e)
        {

        }

        private void cboAvenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAvenu.Text != "")
            {
                avenueVill = (AvenueVillage)cboAvenu.SelectedItem;
                lblIdAvenueVillage.Text = avenueVill.Id.ToString();
                cboIdQuartierLoc.Text = Factory.Instance.GetQuartierLocalite(avenueVill.Id_quartierLocalite).Designation;
                txtDesignationAvenueVillage.Text = Convert.ToString(avenueVill.Designation);
                lblIdQuartierLoc.Text = Convert.ToString(avenueVill.Id_quartierLocalite);
            }
        }

        private void AvenueVillageForm_Load(object sender, EventArgs e)
        {
            //Chargement des combo box : Avenue Village et Recensseur
            try
            {
                cboAvenu.DataSource = Factory.Instance.GetAvenueVillages();
                cboIdQuartierLoc.DataSource = Factory.Instance.GetQuartierLocalites();
                if (cboIdQuartierLoc.Items.Count > 0) cboIdQuartierLoc.SelectedIndex = 0;
                else { }
            }
            catch (Exception) { }

            cboIdQuartierLoc.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboIdQuartierLoc.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            cboAvenu.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboAvenu.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboAvenu.Text = "";

            try
            {
                dgvAvenueVillage.DataSource = Factory.Instance.GetAvenueVillages();

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
                lblIdAvenueVillage.Text = Factory.Instance.ReNewIdValue(avenueVill).ToString();
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

        private void btnCloseComCheffQuartierLoc_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
