using System;
using System.Windows.Forms;
using JOVIA_LIB;
using Microsoft.VisualBasic;

namespace JOVIA_WIN
{
    public partial class VilleTerritoireForm : UserControl
    {
        VilleTeritoire vt = new VilleTeritoire();

        public VilleTerritoireForm()
        {
            InitializeComponent();
        }

        //Appel de l'ecouteur de reception du SMS 
        //private void comm_MessageReceived(object sender, GsmComm.GsmCommunication.MessageReceivedEventArgs e)
        //{
        //    try
        //    {
        //        //On verifie le login de l'utilisateur
        //        Factory.Instance.AuthentificateUserToSensSMS(SMS.Instance.ReceiveSMS());

        //        //On recupere le SMS et on l'insere dans la base des donnees
        //        Factory.Instance.DoData(SMS.Instance.ReceiveSMS(), Convert.ToInt32(Factory.Instance.ReadFileParametersUser()[2]));
        //    }
        //    catch (Exception) { }
        //}

        private void initialiseVilleTer()
        {
            lblIdVilleTer.Text = "";
            txtDesignationVilleTer.Clear();
            txtSuperficieVilleTer.Clear();
            txtDesignationVilleTer.Focus();
        }

        private void refreshVilleTer()
        {
            dgvVilleTer.DataSource = Factory.Instance.GetVilleTeritoires();

            if (dgvVilleTer.RowCount > 0)
            {
                btnUpdateVilleTer.Enabled = true;
                btnDeleteVilleTer.Enabled = true;
            }

            int col = 0;
            foreach (DataGridViewColumn dgvc in dgvVilleTer.Columns)
            {
                dgvVilleTer.AutoResizeColumn(col);
                col++;
            }
        }

        private void dgvVilleTer_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvVilleTer.SelectedRows.Count > 0)
                {
                    btnUpdateVilleTer.Enabled = true;
                    btnDeleteVilleTer.Enabled = true;

                    vt = (VilleTeritoire)dgvVilleTer.SelectedRows[0].DataBoundItem;

                    //SetBindings();

                    lblIdVilleTer.Text = Convert.ToString(vt.Id);
                    txtDesignationVilleTer.Text = vt.Designation;
                    txtSuperficieVilleTer.Text = Convert.ToString(vt.Superficie);
                }
            }
            catch (Exception) { MessageBox.Show("Erreur dans la zone d'affichage", "Erreur d'affichage"); }
        }

        private void cboRechercherVilleT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRechercherVilleT.Text != "")
            {
                vt = (VilleTeritoire)cboRechercherVilleT.SelectedItem;
                lblIdVilleTer.Text = Convert.ToString(vt.Id);
                txtDesignationVilleTer.Text = vt.Designation;
                txtSuperficieVilleTer.Text = Convert.ToString(vt.Superficie);
            }
        }

        private void btnAddVilleTer_Click(object sender, EventArgs e)
        {
            try
            {
                btnSaveVilleTer.Enabled = true;
                lblIdVilleTer.Text = Convert.ToString(Factory.Instance.ReNewIdValue(vt));
                txtDesignationVilleTer.Clear();
                txtSuperficieVilleTer.Clear();
                txtDesignationVilleTer.Focus();
            }
            catch (Exception) { btnSaveVilleTer.Enabled = false; }
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

        private void btnSaveVilleTer_Click(object sender, EventArgs e)
        {
            try
            {
                vt.Id = Convert.ToInt32(lblIdVilleTer.Text);
                vt.Designation = txtDesignationVilleTer.Text;
                vt.Superficie = Convert.ToInt32(txtSuperficieVilleTer.Text);

                vt.Enregistrer();

                btnSaveVilleTer.Enabled = false;

                MessageBox.Show("Enregistrement éffectuée", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                try
                {
                    refreshVilleTer();
                }
                catch (Exception)
                {
                    MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Echec de l'enregistrement", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnUpdateVilleTer_Click(object sender, EventArgs e)
        {
            try
            {
                vt.Id = Convert.ToInt32(lblIdVilleTer.Text);
                vt.Designation = txtDesignationVilleTer.Text;
                vt.Superficie = Convert.ToInt32(txtSuperficieVilleTer.Text);

                vt.Modifier();

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
            catch (Exception)
            {
                MessageBox.Show("Echec de la modification", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnDeleteVilleTer_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Voulez - vous supprimer cer enregistrement ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    vt.Id = Convert.ToInt32(lblIdVilleTer.Text);
                    vt.Supprimmer();

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

        private void btnCloseProvinceVilleTerritoire_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void VilleTerritoireForm_Load(object sender, EventArgs e)
        {
            btnSaveVilleTer.Enabled = false;

            try
            {
                cboRechercherVilleT.DataSource = Factory.Instance.GetVilleTeritoires();
                dgvVilleTer.DataSource = Factory.Instance.GetVilleTeritoires();

                //On rend inisible certaines colonne des DataGridView qui recup les dataproperty
                //correspondant aux noms des accesseurs

                foreach (DataGridViewColumn dgvc in dgvVilleTer.Columns)
                {
                    if (dgvc.DataPropertyName == "Id") dgvc.Visible = false;
                    dgvVilleTer.AutoResizeColumn(0);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("erreur lors de l'affichage", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (dgvVilleTer.RowCount <= 0)
            {
                btnSaveVilleTer.Enabled = false;
                btnUpdateVilleTer.Enabled = false;
                btnDeleteVilleTer.Enabled = false;
            }


            cboRechercherVilleT.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboRechercherVilleT.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboRechercherVilleT.Text = "";
        }
    }
}
