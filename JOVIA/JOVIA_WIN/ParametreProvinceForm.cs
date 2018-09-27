using System;
using System.Windows.Forms;
using JOVIA_LIB;

namespace JOVIA_WIN
{
    public partial class ParametreProvinceForm : UserControl
    {
        ParametreProvince paramProvince = new ParametreProvince();

        public ParametreProvinceForm()
        {
            InitializeComponent();
        }

        private void getAttributparamProvince()
        {
            paramProvince.Id = Convert.ToInt32(lblIdParamProvince.Text);
            paramProvince.Designation = txtDesignationProvince.Text;
            paramProvince.Id_province = Convert.ToInt32(txtIdProvince.Text);
        }

        private void refreshParamProvince()
        {
            dgvParamProv.DataSource = Factory.Instance.GetParametreProvinces();

            if (dgvParamProv.RowCount > 0)
            {
                btnUpdateParamProv.Enabled = true;
                btnDeleteParamProv.Enabled = true;
            }
            else btnAddParamProv.Enabled = true;
        }

        private void initialiseParamProvince()
        {
            lblIdParamProvince.Text = "";
            txtDesignationProvince.Clear();
            txtIdProvince.Clear();
            txtDesignationProvince.Focus();
        }

        private void dgvParamProv_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvParamProv.SelectedRows.Count > 0)
                {
                    btnUpdateParamProv.Enabled = true;
                    btnDeleteParamProv.Enabled = true;
                    btnAddParamProv.Enabled = false;
                    paramProvince = (ParametreProvince)dgvParamProv.SelectedRows[0].DataBoundItem;
                    //SetBindings();

                    lblIdParamProvince.Text = Convert.ToString(paramProvince.Id);
                    txtDesignationProvince.Text = paramProvince.Designation;
                    txtIdProvince.Text = Convert.ToString(paramProvince.Id_province);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur dans la zone d'affichage", "Erreur d'affichage");
            }
        }

        private void btnCloseParamProv_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ParametreProvinceForm_Load(object sender, EventArgs e)
        {
            btnSaveParamProv.Enabled = false;

            try
            {
                dgvParamProv.DataSource = Factory.Instance.GetParametreProvinces();

                //On rend invisible certaines colonne des DataGridView qui recup les dataproperty
                //correspondant aux noms des accesseurs
                int col = 0;

                foreach (DataGridViewColumn dgvc in dgvParamProv.Columns)
                {
                    if (dgvc.DataPropertyName == "Id") dgvc.Visible = false;
                    dgvParamProv.AutoResizeColumn(col);
                    col++;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("erreur lors de l'affichage", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (dgvParamProv.RowCount <= 0)
            {
                btnSaveParamProv.Enabled = false;
                btnUpdateParamProv.Enabled = false;
                btnDeleteParamProv.Enabled = false;
                btnAddParamProv.Enabled = true;
            }
        }

        private void btnAddParamProv_Click(object sender, EventArgs e)
        {
            btnSaveParamProv.Enabled = true;
            try
            {
                lblIdParamProvince.Text = Convert.ToString(Factory.Instance.ReNewIdValue(paramProvince));
                txtDesignationProvince.Clear();
                txtIdProvince.Clear();
                txtIdProvince.Focus();
            }
            catch (Exception) { btnSaveParamProv.Enabled = false; }
        }

        private void btnRefreshParamProv_Click(object sender, EventArgs e)
        {
            try
            {
                refreshParamProvince();
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSaveParamProv_Click(object sender, EventArgs e)
        {
            try
            {
                getAttributparamProvince();

                paramProvince.Enregistrer();

                btnSaveParamProv.Enabled = false;

                try
                {
                    refreshParamProvince();
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

        private void btnUpdateParamProv_Click(object sender, EventArgs e)
        {
            try
            {
                getAttributparamProvince();

                paramProvince.Modifier();

                MessageBox.Show("Modification éffectuée", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);

                try
                {
                    refreshParamProvince();
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

        private void btnDeleteParamProv_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("Voulez - vous supprimer cet enregistrement ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    paramProvince.Id = Convert.ToInt32(lblIdParamProvince.Text);
                    paramProvince.Supprimmer();

                    MessageBox.Show("Suppression éffectuée", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        refreshParamProvince();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    if (dgvParamProv.RowCount <= 0)
                    {
                        btnSaveParamProv.Enabled = false;
                        btnUpdateParamProv.Enabled = false;
                        btnDeleteParamProv.Enabled = false;
                    }

                    initialiseParamProvince();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Echec de la suppression", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
