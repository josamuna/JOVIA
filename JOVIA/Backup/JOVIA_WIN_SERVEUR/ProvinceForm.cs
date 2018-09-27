using System;
using System.Windows.Forms;
using JOVIA_LIB_SERVEUR;
using Microsoft.VisualBasic;

namespace JOVIA_WIN_SERVEUR
{
    public partial class ProvinceForm : UserControl
    {
        Province province = new Province();
        VilleTeritoireServeur villeterritoire = new VilleTeritoireServeur();

        public ProvinceForm()
        {
            InitializeComponent();
        }

        private void reloadCombo()
        {
            cboIdProvince.DataSource = Factory1.Instance.GetProvinces();
        }

        private void initialiseVilleTer()
        {
            lblIdVilleTer.Text = "";
            txtDesignationVilleTer.Clear();
            txtSuperficieVilleTer.Clear();
            txtDesignationVilleTer.Focus();
        }

        private void initialiseProvince()
        {
            lblIdProvince.Text = "";
            txtDesignationProvince.Clear();
            txtSuperficieProvince.Clear(); 
            txtDesignationProvince.Focus();
        }

        private void refreshProvince()
        {
            dgvProvince.DataSource = Factory1.Instance.GetProvinces();

            if (dgvProvince.RowCount > 0)
            {
                btnUpdateProvince.Enabled = true;
                btnDeleteProvince.Enabled = true;
            } 
        }

        private void refreshVilleTer()
        {
            dgvVilleTer.DataSource = Factory1.Instance.GetVilleTeritoires();

            if (dgvVilleTer.RowCount > 0)
            {
                btnUpdateVilleTer.Enabled = true;
                btnDeleteVilleTer.Enabled = true;
            }
        }

        private void btnAddProvince_Click(object sender, EventArgs e)
        {
            try
            {
                btnSaveProvince.Enabled = true;
                lblIdProvince.Text = Convert.ToString(Factory1.Instance.ReNewIdValue(province));
                txtDesignationProvince.Clear();
                txtSuperficieProvince.Clear();
                txtDesignationProvince.Focus();
            }
            catch (Exception) { btnSaveProvince.Enabled = false; }
        }

        private void btnAddVilleTer_Click(object sender, EventArgs e)
        {
            try
            {
                btnSaveVilleTer.Enabled = true;
                lblIdVilleTer.Text = Convert.ToString(Factory1.Instance.ReNewIdValue(villeterritoire));
                txtDesignationVilleTer.Clear();
                txtSuperficieVilleTer.Clear();
                txtDesignationVilleTer.Focus();
            }
            catch (Exception) { btnSaveVilleTer.Enabled = false; }
        }

        private void btnCloseProvinceVilleTerritoire_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSaveProvince_Click(object sender, EventArgs e)
        {
            try
            {
                province.Id = Convert.ToInt32(lblIdProvince.Text);
                province.Designation = txtDesignationProvince.Text;
                province.Superficie = Convert.ToInt64(txtSuperficieProvince.Text);

                province.Enregistrer();

                btnSaveProvince.Enabled = false;
                MessageBox.Show("Enregistrement éffectuée", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);

                try
                {
                    reloadCombo();
                }
                catch (Exception) { }

                try
                {
                    refreshProvince();
                }
                catch (Exception)
                {
                    MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Echec de l'enregistrement, " + ex.Message, "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnRefreshProvince_Click(object sender, EventArgs e)
        {
            try
            {
                refreshProvince();
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSaveVilleTer_Click(object sender, EventArgs e)
        {
            try
            {
                villeterritoire.Id = Convert.ToInt32(lblIdVilleTer.Text);
                villeterritoire.Designation = txtDesignationVilleTer.Text;
                villeterritoire.Superficie = Convert.ToInt32(txtSuperficieVilleTer.Text);

                villeterritoire.Enregistrer();

                btnSaveVilleTer.Enabled = false;

                try
                {
                    refreshVilleTer();
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

        private void btnRefreshVilleTer_Click(object sender, EventArgs e)
        {
            try
            {
                refreshVilleTer();
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnUpdateProvince_Click(object sender, EventArgs e)
        {
            try
            {
                province.Id = Convert.ToInt32(lblIdProvince.Text);
                province.Designation = txtDesignationProvince.Text;
                province.Superficie = Convert.ToInt64(txtSuperficieProvince.Text);

                province.Modifier();
                MessageBox.Show("Modification éffectuée", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                try
                {
                    reloadCombo();
                }
                catch (Exception) { }

                try
                {
                    refreshProvince();
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

        private void btnUpdateVilleTer_Click(object sender, EventArgs e)
        {
            try
            {
                villeterritoire.Id = Convert.ToInt32(lblIdVilleTer.Text);
                villeterritoire.Designation = txtDesignationVilleTer.Text;
                villeterritoire.Superficie = Convert.ToInt32(txtSuperficieVilleTer.Text);

                villeterritoire.Modifier();

                MessageBox.Show("Modification éffectuée", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                try
                {
                    refreshVilleTer();
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

        private void btnDeleteProvince_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Voulez - vous supprimer cet enregistrement ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    province.Id = Convert.ToInt32(lblIdProvince.Text);
                    province.Supprimmer();

                    MessageBox.Show("Suppression éffectuée", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        reloadCombo();
                    }
                    catch (Exception) { }

                    try
                    {
                        refreshProvince();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    if (dgvProvince.RowCount <= 0)
                    {
                        btnSaveProvince.Enabled = false;
                        btnUpdateProvince.Enabled = false;
                        btnDeleteProvince.Enabled = false;
                    }

                    initialiseProvince();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Echec de la suppression", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnDeleteVilleTer_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Voulez - vous supprimer cer enregistrement ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    villeterritoire.Id = Convert.ToInt32(lblIdVilleTer.Text);
                    villeterritoire.Supprimmer();

                    MessageBox.Show("Suppression éffectuée", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        refreshVilleTer();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    if (dgvVilleTer.RowCount <= 0)
                    {
                        btnSaveVilleTer.Enabled = false;
                        btnUpdateVilleTer.Enabled = false;
                        btnDeleteVilleTer.Enabled = false;
                    }

                    initialiseVilleTer();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Echec de la suppression", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvProvince_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvProvince.SelectedRows.Count > 0)
                {
                    province = (Province)dgvProvince.SelectedRows[0].DataBoundItem;

                    //SetBindings();
                    btnUpdateProvince.Enabled = true;
                    btnDeleteProvince.Enabled = true;
                    btnSearchProvince.Enabled = true;
                    lblIdProvince.Text = Convert.ToString(province.Id);
                    txtDesignationProvince.Text = province.Designation;
                    txtSuperficieProvince.Text = Convert.ToString(province.Superficie);
                }
            }
            catch (Exception) { MessageBox.Show("Erreur dans la zone d'affichage", "Erreur d'affichage"); }
        }

        private void dgvVilleTer_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvVilleTer.SelectedRows.Count > 0)
                {
                    btnUpdateVilleTer.Enabled = true;
                    btnDeleteVilleTer.Enabled = true;
                    btnSearchVilleTer.Enabled = true;
                    villeterritoire = (VilleTeritoireServeur)dgvVilleTer.SelectedRows[0].DataBoundItem;
                    //SetBindings();

                    lblIdVilleTer.Text = Convert.ToString(villeterritoire.Id);
                    cboIdProvince.Text = Factory1.Instance.GetPronvince(villeterritoire.Id_pronvince).Designation;
                    txtDesignationVilleTer.Text = villeterritoire.Designation;
                    txtSuperficieVilleTer.Text = Convert.ToString(villeterritoire.Superficie);
                }
            }
            catch (Exception) { MessageBox.Show("Erreur dans la zone d'affichage", "Erreur d'affichage"); }
        }

        private void btnSearchProvince_Click(object sender, EventArgs e)
        {
            string valueDesi = Interaction.InputBox("Veuillez saisir la désignation de la Province à rechercher", "Recherche Ville/Territoire", "", 100, 100);

            try
            {
                dgRechercheProvince.DataSource = Factory1.Instance.SearchProvince(valueDesi);

                //On rend inisible certaines colonne des DataGridView qui recup les dataproperty
                //correspondant aux noms des accesseurs
                foreach (DataGridViewColumn dgvc in dgRechercheProvince.Columns)
                {
                    if (dgvc.DataPropertyName == "Id") dgvc.Visible = false;
                    dgRechercheProvince.AutoResizeColumn(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Recherche", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSearchVilleTer_Click(object sender, EventArgs e)
        {
            string valueDesi = Interaction.InputBox("Veuillez saisir la désignation de la Ville ou du territoire à rechercher", "Recherche Ville/Territoire", "", 100, 100);

            try
            {
                dgRechercheVilleTer.DataSource = Factory1.Instance.SearchVilleTerritoire(valueDesi);

                //On rend inisible certaines colonne des DataGridView qui recup les dataproperty
                //correspondant aux noms des accesseurs
                int col = 0;
                foreach (DataGridViewColumn dgvc in dgRechercheVilleTer.Columns)
                {
                    if (dgvc.DataPropertyName == "Id") dgvc.Visible = false;
                    dgRechercheVilleTer.AutoResizeColumn(col);
                    col++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Recherche", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ProvinceForm_Load(object sender, EventArgs e)
        {
            btnSaveProvince.Enabled = false;
            btnSaveVilleTer.Enabled = false;

            try
            {
                reloadCombo();

                dgvProvince.DataSource = Factory1.Instance.GetProvinces();
                dgvVilleTer.DataSource = Factory1.Instance.GetVilleTeritoires();

                //On rend invisible certaines colonne des DataGridView qui recup les dataproperty
                //correspondant aux noms des accesseurs
                int col1 = 0, col2 = 0;
                foreach (DataGridViewColumn dgvc in dgvProvince.Columns)
                {
                    if (dgvc.DataPropertyName == "Id") dgvc.Visible = false;
                    dgvProvince.AutoResizeColumn(col1);
                    col1++;
                }

                foreach (DataGridViewColumn dgvc in dgvVilleTer.Columns)
                {
                    if (dgvc.DataPropertyName == "Id") dgvc.Visible = false;
                    //else if (dgvc.DataPropertyName == "Id_pronvince") dgvc.Visible = false;
                    dgvVilleTer.AutoResizeColumn(col2);
                    col2++;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("erreur lors de l'affichage", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (dgvProvince.RowCount <= 0)
            {
                btnSaveProvince.Enabled = false;
                btnUpdateProvince.Enabled = false;
                btnDeleteProvince.Enabled = false;
                btnSearchProvince.Enabled = false;
            }

            if (dgvVilleTer.RowCount <= 0)
            {
                btnSaveVilleTer.Enabled = false;
                btnUpdateVilleTer.Enabled = false;
                btnDeleteVilleTer.Enabled = false;
                btnSearchVilleTer.Enabled = false;
            }
        }

        private void cboIdProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                villeterritoire.Id_pronvince = Convert.ToInt32(((Province)cboIdProvince.SelectedItem).Id);
            }
            catch (Exception) { }
        }

        private void dgRechercheProvince_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgRechercheProvince.SelectedRows.Count > 0)
                {
                    province = (Province)dgRechercheProvince.SelectedRows[0].DataBoundItem;
                    lblIdProvince.Text = province.Id.ToString();
                    txtDesignationProvince.Text = Convert.ToString(province.Designation);
                    txtSuperficieProvince.Text = Convert.ToString(province.Superficie);
                }
            }
            catch (Exception) { MessageBox.Show("Erreur dans la zone d'affichage de recherche", "Erreur d'affichage de recherche"); }
        }

        private void dgRechercheVilleTer_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgRechercheVilleTer.SelectedRows.Count > 0)
                {
                    villeterritoire = ( VilleTeritoireServeur)dgRechercheVilleTer.SelectedRows[0].DataBoundItem;
                    lblIdVilleTer.Text = villeterritoire.Id.ToString();
                    cboIdProvince.Text = Factory1.Instance.GetPronvince(villeterritoire.Id_pronvince).Designation;
                    txtDesignationVilleTer.Text = Convert.ToString(villeterritoire.Designation);
                    txtSuperficieVilleTer.Text = Convert.ToString(villeterritoire.Superficie);
                }
            }
            catch (Exception) { MessageBox.Show("Erreur dans la zone d'affichage de recherche", "Erreur d'affichage de recherche"); }
        }
    }
}
