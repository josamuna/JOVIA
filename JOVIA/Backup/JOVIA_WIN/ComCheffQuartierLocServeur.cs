using System;
using System.Windows.Forms;
using JOVIA_LIB_SERVEUR;
using Microsoft.VisualBasic;

namespace JOVIA_WIN
{
    public partial class ComCheffQuartierLocServeur : UserControl
    {
        VilleTeritoireServeur villeTer = new VilleTeritoireServeur();
        CommuneChefferieSecteurServeur comChefSect = new CommuneChefferieSecteurServeur();
        QuartierLocaliteServeur quartierLoc = new QuartierLocaliteServeur();

        public ComCheffQuartierLocServeur()
        {
            InitializeComponent();
        }

        private void getAttributComChefSect()
        {
            comChefSect.Id = Convert.ToInt32(lblIdComChefSect.Text);
            comChefSect.Designation = txtDesignationComChefSect.Text;
            comChefSect.Superficie = Convert.ToInt32(txtSuperficieComChefSect.Text);
        }

        private void getAttributQuartierLoc()
        {
            quartierLoc.Id = Convert.ToInt32(lblIdQuartierLoc.Text);           
            quartierLoc.Designation = txtDesignationQuartierLoc.Text;
            quartierLoc.Superficie = Convert.ToInt32(txtSuperficieQuartierLoc.Text);
        }

        private void refreshQuartierLoc()
        {
            dgvQuartierLoc.DataSource = Factory1.Instance.GetQuartierLocalites();

            if (dgvQuartierLoc.RowCount > 0)
            {
                btnModifierQuartierLoc.Enabled = true;
                btnDeleteQuartierLoc.Enabled = true;
                btnSaveQuartierLoc.Enabled = true;
            }

            int col = 0;
            foreach (DataGridViewColumn dgvc in dgvQuartierLoc.Columns)
            {
                dgvQuartierLoc.AutoResizeColumn(col);
                col++;
            }
        }

        private void refreshComChefSect()
        {
            dgvComChefSect.DataSource = Factory1.Instance.GetCommuneChefferieSecteurs();

            if (dgvComChefSect.RowCount > 0)
            {
                btnModifierComChefSect.Enabled = true;
                btnDeleteComChefSect.Enabled = true;
            }

            int col = 0;
            foreach (DataGridViewColumn dgvc in dgvComChefSect.Columns)
            {
                dgvComChefSect.AutoResizeColumn(col);
                col++;
            }
        }

        private void initialiseComChefSect()
        {
            lblIdComChefSect.Text = "";
            txtDesignationComChefSect.Clear();
            txtSuperficieComChefSect.Clear();
            txtDesignationComChefSect.Focus();
        }

        private void initialiseQuartierLoc()
        {
            lblIdQuartierLoc.Text = "";
            txtDesignationQuartierLoc.Clear();
            txtSuperficieQuartierLoc.Clear();
            txtDesignationQuartierLoc.Focus();
        }

        private void ComCheffQuartierLoc_Load(object sender, EventArgs e)
        {
            //Chargement des combo box : Commune Chefferie Secteur et Quartier Localite
            try
            {
                cboRechercheCommChefSect.DataSource = Factory1.Instance.GetCommuneChefferieSecteurs();
                cboRechercheQuartLocalit.DataSource = Factory1.Instance.GetQuartierLocalites();
                cboIdComChefSect.DataSource = Factory1.Instance.GetCommuneChefferieSecteurs();
                cboVilleTeritoire.DataSource = Factory1.Instance.GetVilleTeritoires();

                if (cboVilleTeritoire.Items.Count > 0) cboVilleTeritoire.SelectedIndex = 0;
                else { }
                if (cboIdComChefSect.Items.Count > 0) cboIdComChefSect.SelectedIndex = 0;
                else { }
            }
            catch (Exception) { }

            cboIdComChefSect.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboIdComChefSect.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            cboVilleTeritoire.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboVilleTeritoire.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            cboRechercheCommChefSect.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboRechercheCommChefSect.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboRechercheCommChefSect.Text = "";

            cboRechercheQuartLocalit.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboRechercheQuartLocalit.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboRechercheQuartLocalit.Text = "";

            try
            {
                dgvComChefSect.DataSource = Factory1.Instance.GetCommuneChefferieSecteurs();
                dgvQuartierLoc.DataSource = Factory1.Instance.GetQuartierLocalites();

                //On rend inisible certaines colonne des DataGridView qui recup les dataproperty
                //correspondant aux noms des accesseurs
                int col1 = 0, col2 = 0;
                foreach (DataGridViewColumn dgvc in dgvComChefSect.Columns)
                {
                    if (dgvc.DataPropertyName == "Id") dgvc.Visible = false;
                    else if (dgvc.DataPropertyName == "Id_VilleTeritoire") dgvc.Visible = false;
                    dgvComChefSect.AutoResizeColumn(col1);
                    col1++;
                }

                foreach (DataGridViewColumn dgvc in dgvQuartierLoc.Columns)
                {
                    if (dgvc.DataPropertyName == "Id") dgvc.Visible = false;
                    else if (dgvc.DataPropertyName == "Id_communeChefferieSecteur") dgvc.Visible = false;
                    dgvQuartierLoc.AutoResizeColumn(col2);
                    col2++;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("erreur lors de l'affichage", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            btnSaveComChefSect.Enabled = false;
            btnSaveQuartierLoc.Enabled = false;

            if (dgvComChefSect.RowCount <= 0)
            {
                btnSaveComChefSect.Enabled = false;
                btnModifierComChefSect.Enabled = false;
                btnDeleteComChefSect.Enabled = false;
            }

            if (dgvQuartierLoc.RowCount <= 0)
            {
                btnSaveQuartierLoc.Enabled = false;
                btnModifierQuartierLoc.Enabled = false;
                btnDeleteQuartierLoc.Enabled = false;
            } 
        }

        private void dgvComChefSect_SelectionChanged(object sender, EventArgs e)
        {
           try
           {
               if (dgvComChefSect.SelectedRows.Count > 0)
                {
                    btnModifierComChefSect.Enabled = true;
                    btnDeleteComChefSect.Enabled = true;

                    comChefSect = (CommuneChefferieSecteurServeur)dgvComChefSect.SelectedRows[0].DataBoundItem;
                    lblIdComChefSect.Text = comChefSect.Id.ToString();
                    cboVilleTeritoire.Text = Factory1.Instance.GetVilleTeritoire(comChefSect.Id_VilleTeritoire).ToString();
                    txtDesignationComChefSect.Text = Convert.ToString(comChefSect.Designation);
                    txtSuperficieComChefSect.Text = Convert.ToString(comChefSect.Superficie);
                }
           }
           catch (Exception) { MessageBox.Show("Erreur dans la zone d'affichage", "Erreur d'affichage"); }
        }

        private void dgvQuartierLoc_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvQuartierLoc.SelectedRows.Count > 0)
                {
                    btnModifierQuartierLoc.Enabled = true;
                    btnDeleteQuartierLoc.Enabled = true;

                    quartierLoc = (QuartierLocaliteServeur)dgvQuartierLoc.SelectedRows[0].DataBoundItem;
                    lblIdQuartierLoc.Text = quartierLoc.Id.ToString();
                    cboIdComChefSect.Text = Factory1.Instance.GetCommuneChefferieSecteur(quartierLoc.Id_communeChefferieSecteur).ToString();
                    txtDesignationQuartierLoc.Text = Convert.ToString(quartierLoc.Designation);
                    txtSuperficieQuartierLoc.Text = Convert.ToString(quartierLoc.Superficie);
                }
            }
            catch (Exception) { MessageBox.Show("Erreur dans la zone d'affichage", "Erreur d'affichage"); }
        }

        private void btnModifierQuartierLoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (quartierLoc != null)
                {
                    getAttributQuartierLoc();

                    quartierLoc.Modifier();
                    MessageBox.Show("Modification effectuée!", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        refreshQuartierLoc();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Echec de la modification", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboVilleTeritoire_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comChefSect.Id_VilleTeritoire = Convert.ToInt32(((VilleTeritoireServeur)cboVilleTeritoire.SelectedItem).Id);
            }catch(Exception){}
        }

        private void cboIdComChefSect_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                quartierLoc.Id_communeChefferieSecteur = Convert.ToInt32(((CommuneChefferieSecteurServeur)cboIdComChefSect.SelectedItem).Id);
                lblIdComChefSect.Text = Convert.ToString(((CommuneChefferieSecteurServeur)cboIdComChefSect.SelectedItem).Id);
                txtDesignationComChefSect.Text = Convert.ToString(((CommuneChefferieSecteurServeur)cboIdComChefSect.SelectedItem).Designation);
                txtSuperficieComChefSect.Text = Convert.ToString(((CommuneChefferieSecteurServeur)cboIdComChefSect.SelectedItem).Superficie);
                cboVilleTeritoire.Text = Factory1.Instance.GetVilleTeritoire(Convert.ToInt32(Convert.ToInt32(((CommuneChefferieSecteurServeur)cboIdComChefSect.SelectedItem).Id_VilleTeritoire))).Designation;
            }
            catch (Exception) { }
        }

        private void cboRechercheCommChefSect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRechercheCommChefSect.Text != "")
            {
                comChefSect = (CommuneChefferieSecteurServeur)cboRechercheCommChefSect.SelectedItem;
                lblIdComChefSect.Text = comChefSect.Id.ToString();
                cboVilleTeritoire.Text = Factory1.Instance.GetVilleTeritoire(comChefSect.Id_VilleTeritoire).Designation;
                txtDesignationComChefSect.Text = Convert.ToString(comChefSect.Designation);
                txtSuperficieComChefSect.Text = Convert.ToString(comChefSect.Superficie);
            }
        }

        private void cboRechercheQuartLocalit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRechercheQuartLocalit.Text != "")
            {
                quartierLoc = (QuartierLocaliteServeur)cboRechercheQuartLocalit.SelectedItem;
                lblIdQuartierLoc.Text = quartierLoc.Id.ToString();
                cboIdComChefSect.Text = Factory1.Instance.GetCommuneChefferieSecteur(quartierLoc.Id_communeChefferieSecteur).Designation;
                txtDesignationQuartierLoc.Text = Convert.ToString(quartierLoc.Designation);
                txtSuperficieQuartierLoc.Text = Convert.ToString(quartierLoc.Superficie);
            }
        }

        private void btnCloseComCheffQuartierLoc_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAddComChefSect_Click(object sender, EventArgs e)
        {
            try
            {
                //considerez comme nouveau
                lblIdComChefSect.Text = Factory1.Instance.ReNewIdValue(comChefSect).ToString();
                txtDesignationComChefSect.Clear();
                txtSuperficieComChefSect.Clear();
                txtDesignationComChefSect.Focus();

                btnSaveComChefSect.Enabled = true;
            }
            catch (Exception) { btnSaveComChefSect.Enabled = false; }
        }

        private void btnRefrehComChefSect_Click(object sender, EventArgs e)
        {
            try
            {
                refreshComChefSect();
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSaveComChefSect_Click(object sender, EventArgs e)
        {
            try
            {
                if (comChefSect != null)
                {
                    getAttributComChefSect();

                    comChefSect.Enregistrer();
                    btnSaveComChefSect.Enabled = false;
                    MessageBox.Show("Enregistrement éffectué", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        cboIdComChefSect.DataSource = Factory1.Instance.GetCommuneChefferieSecteurs();
                        if (cboIdComChefSect.Items.Count > 0) cboIdComChefSect.SelectedIndex = 0;
                        else { }
                    }
                    catch (Exception) { }

                    try
                    {
                        refreshComChefSect();
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

        private void btnModifierComChefSect_Click(object sender, EventArgs e)
        {
            try
            {
                if (comChefSect != null)
                {
                    getAttributComChefSect();

                    comChefSect.Modifier();
                    MessageBox.Show("Modification effectuée!", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        refreshComChefSect();
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

        private void btnDeleteComChefSect_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Voulez - vous vraiment supprimer cet enregistrement ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    comChefSect.Id = Convert.ToInt32(lblIdComChefSect.Text);
                    comChefSect.Supprimer();
                    MessageBox.Show("Suppression éffectuée", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        cboIdComChefSect.DataSource = Factory1.Instance.GetCommuneChefferieSecteurs();
                        if (cboIdComChefSect.Items.Count > 0) cboIdComChefSect.SelectedIndex = 0;
                        else { }
                    }
                    catch (Exception) { }

                    try
                    {
                        refreshComChefSect();
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(e1.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    if (dgvComChefSect.RowCount <= 0)
                    {
                        btnSaveComChefSect.Enabled = false;
                        btnModifierComChefSect.Enabled = false;
                        btnDeleteComChefSect.Enabled = false;
                    }

                    initialiseComChefSect();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Echec de la suppression", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAddQuartierLoc_Click(object sender, EventArgs e)
        {
            try
            {
                //considerez comme nouveau
                lblIdQuartierLoc.Text = Factory1.Instance.ReNewIdValue(quartierLoc).ToString();
                txtDesignationQuartierLoc.Clear();
                txtSuperficieQuartierLoc.Clear();
                txtDesignationQuartierLoc.Focus();

                btnSaveQuartierLoc.Enabled = true;
            }
            catch (Exception) { btnRefreshQuartierLoc.Enabled = false; }
        }

        private void btnRefreshQuartierLoc_Click(object sender, EventArgs e)
        {
            try
            {
                refreshQuartierLoc();
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSaveQuartierLoc_Click(object sender, EventArgs e)
        {
            try
            {
                if (quartierLoc != null)
                {
                    getAttributQuartierLoc();

                    quartierLoc.Enregistrer();
                    btnSaveQuartierLoc.Enabled = false;
                    MessageBox.Show("Enregistrement éffectué", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        refreshQuartierLoc();
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(e1.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Echec de l'enregistrement, " + ex.Message, "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnDeleteQuartierLoc_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Voulez - vous vraiment supprimer cet enregistrement ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    quartierLoc.Id = Convert.ToInt32(lblIdQuartierLoc.Text);
                    quartierLoc.Supprimer();
                    MessageBox.Show("Suppression éffectuée", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        refreshQuartierLoc();
                    }
                    catch (Exception e1)
                    {
                        MessageBox.Show(e1.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    if (dgvQuartierLoc.RowCount <= 0)
                    {
                        btnSaveQuartierLoc.Enabled = false;
                        btnModifierQuartierLoc.Enabled = false;
                        btnDeleteQuartierLoc.Enabled = false;
                    }

                    initialiseQuartierLoc();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Echec de la suppression", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            } 
        }
    }
}
