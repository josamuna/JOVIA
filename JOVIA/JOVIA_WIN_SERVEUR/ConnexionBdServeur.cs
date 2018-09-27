using System;
using System.Windows.Forms;
using JOVIA_LIB_SERVEUR;

namespace JOVIA_WIN_SERVEUR
{
    public partial class ConnexionBdServeur : Form
    {
        PrincipalServeur principal = new PrincipalServeur();

        public ConnexionBdServeur()
        {
            InitializeComponent();
        }

        private void btfermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ConnexionBdServeur_Load(object sender, EventArgs e)
        {
            try
            {
                cboTypeDB.Items.Add("PostgreSQL");
                cboTypeDB.Items.Add("MySQL");
                cboTypeDB.Items.Add("Microsoft SQL Server");
                //cboTypeDB.Items.Add("Microsoft Access");
                cboTypeDB.SelectedIndex = 0;
            }
            catch (Exception) { }

            try
            {
                if (cboTypeDB.SelectedIndex == 0)
                {
                    //PostGresSQL
                    cboServeur.Text = Factory1.Instance.loadParam(0)[0];
                    txtBD.Text = Factory1.Instance.loadParam(0)[1];
                    txtNomUser.Text = Factory1.Instance.loadParam(0)[2];
                    txtPort.Text = Factory1.Instance.loadParam(0)[3];
                }
            }
            catch (Exception) { }
            txtUserSimple.Focus();
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboTypeDB.SelectedIndex == 0)
                {
                    //BD PostGresSQL
                    if (Factory1.Instance.VerifieDoConnect(Convert.ToInt32(txtPort.Text), cboServeur.Text, txtBD.Text, txtNomUser.Text, txtMotPass.Text, cboTypeDB.SelectedIndex))
                    {
                        CallVerifieConnect();
                    }
                    else MessageBox.Show("Echec de connexion", "Connexion à la base de données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (cboTypeDB.SelectedIndex == 1)
                {
                    //BD MySQL
                    if (Factory1.Instance.VerifieDoConnect(null, cboServeur.Text, txtBD.Text, txtNomUser.Text, txtMotPass.Text, cboTypeDB.SelectedIndex))
                    {
                        CallVerifieConnect();
                    }
                    else MessageBox.Show("Echec de connexion", "Connexion à la base de données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (cboTypeDB.SelectedIndex == 2)
                {
                    //BD SQLServer
                    if (Factory1.Instance.VerifieDoConnect(null, cboServeur.Text, txtBD.Text, txtNomUser.Text, txtMotPass.Text, cboTypeDB.SelectedIndex))
                    {
                        CallVerifieConnect();
                    }
                    else MessageBox.Show("Echec de connexion", "Connexion à la base de données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                //else if (cboTypeDB.SelectedIndex == 3)
                //{
                //    //BD Access
                //    if (Factory1.Instance.VerifieDoConnect(null, null, txtBD.Text, null, null, cboTypeDB.SelectedIndex))
                //    {
                //        CallVerifieConnect();
                //    }
                //    else MessageBox.Show("Echec de connexion", "Connexion à la base de données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connexion à la base de données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CallVerifieConnect()
        {
            bool ok = false;
            UtilisateurServeur utilisateur = new UtilisateurServeur();
            utilisateur.Nomuser = txtUserSimple.Text;//"superuserserveur", "superpasswordserveur"
            utilisateur.Motpass = txtPwdUserSimple.Text;

            if (Factory1.Instance.verifieLoginUser(utilisateur.Nomuser, utilisateur.Motpass)[2].Equals("0")) ok = true;//L'Utilisateur est un SuperAdministrateur National
            if (Factory1.Instance.verifieLoginUser(utilisateur.Nomuser, utilisateur.Motpass)[2].Equals("0")) ok = true;//L'Utilisateur est un Administrateur National
            else if (Factory1.Instance.verifieLoginUser(utilisateur.Nomuser, utilisateur.Motpass)[2].Equals("1")) ok = true;//L'Utilisateur est un utilisateur simple avec eulement acces limite au serveur National 

            if (ok)
            {
                MessageBox.Show("Connexion réussie", "Connexion à la base de données", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Visible = false;
                principal.Show();
            }
            else
            {
                MessageBox.Show("Echec de l'authentification de l'utilisateur", "Authentification de l'utilisateur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPwdUserSimple.Clear();
                txtUserSimple.Clear();
                txtUserSimple.Focus();
            }
        }

        private void cboTypeDB_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboTypeDB.SelectedIndex == 1 || cboTypeDB.SelectedIndex == 2)
            {
                lblPort.Enabled = false;
                txtPort.Enabled = false;
                lblUserId.Enabled = true;
                lblServeur.Enabled = true;
                cboServeur.Enabled = true;
                txtNomUser.Enabled = true;
                txtMotPass.Enabled = true;
                lblPwdUser.Enabled = true;
            }
            else if (cboTypeDB.SelectedIndex == 3)
            {
                lblPort.Enabled = false;
                lblUserId.Enabled = false;
                lblServeur.Enabled = false;
                txtPort.Enabled = false;
                cboServeur.Enabled = false;
                txtNomUser.Enabled = false;
                txtMotPass.Enabled = false;
                lblPwdUser.Enabled = false;
            }
            else
            {
                lblPort.Enabled = true;
                txtPort.Enabled = true;
                lblUserId.Enabled = true;
                lblServeur.Enabled = true;
                cboServeur.Enabled = true;
                txtNomUser.Enabled = true;
                txtMotPass.Enabled = true;
                lblPwdUser.Enabled = true;
            }
            //Recuperation des valeurs dans le ficier texte
            try
            {
                if (cboTypeDB.SelectedIndex == 0)
                {
                    //PostGresSQL
                    cboServeur.Text = Factory1.Instance.loadParam(0)[0];
                    txtBD.Text = Factory1.Instance.loadParam(0)[1];
                    txtNomUser.Text = Factory1.Instance.loadParam(0)[2];
                    txtPort.Text = Factory1.Instance.loadParam(0)[3];
                }
                else if (cboTypeDB.SelectedIndex == 1)
                {
                    //MySQL
                    cboServeur.Text = Factory1.Instance.loadParam(1)[0];
                    txtBD.Text = Factory1.Instance.loadParam(1)[1];
                    txtNomUser.Text = Factory1.Instance.loadParam(2)[2];
                }
                else if (cboTypeDB.SelectedIndex == 2)
                {
                    //SQLServer
                    cboServeur.Text = Factory1.Instance.loadParam(2)[0];
                    txtBD.Text = Factory1.Instance.loadParam(2)[1];
                    txtNomUser.Text = Factory1.Instance.loadParam(2)[2];
                }
                //else if (cboTypeDB.SelectedIndex == 3)
                //{
                //    //Access
                //    txtBD.Text = Factory1.Instance.loadParam(3)[0];
                //}
            }
            catch (Exception) { }
        }
    }
}
