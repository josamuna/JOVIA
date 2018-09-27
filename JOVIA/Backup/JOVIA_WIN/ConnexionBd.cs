using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using JOVIA_LIB;

namespace JOVIA_WIN
{
    public partial class ConnexionBd : Form
    {
        PrincipalClient principal = new PrincipalClient();
        public ConnexionBd()
        {
            InitializeComponent(); 
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            try
            {               
                if (cboTypeDB.SelectedIndex == 0)
                {
                    //BD PostGresSQL
                    if (Factory.Instance.VerifieDoConnect(Convert.ToInt32(txtPort.Text), cboServeur.Text, txtBD.Text, txtNomUser.Text, txtMotPass.Text, cboTypeDB.SelectedIndex))
                    {
                        CallVerifieConnect();
                    }
                    else MessageBox.Show("Echec de connexion", "Connexion à la base de données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (cboTypeDB.SelectedIndex == 1)
                {
                    //BD MySQL
                    if (Factory.Instance.VerifieDoConnect(null, cboServeur.Text, txtBD.Text, txtNomUser.Text, txtMotPass.Text, cboTypeDB.SelectedIndex))
                    {
                        CallVerifieConnect();
                    }
                    else MessageBox.Show("Echec de connexion", "Connexion à la base de données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (cboTypeDB.SelectedIndex == 2)
                {
                    //BD SQLServer
                    if (Factory.Instance.VerifieDoConnect(null,cboServeur.Text, txtBD.Text, txtNomUser.Text, txtMotPass.Text, cboTypeDB.SelectedIndex))
                    {
                        CallVerifieConnect();
                    }
                    else MessageBox.Show("Echec de connexion", "Connexion à la base de données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                //else if (cboTypeDB.SelectedIndex == 3)
                //{
                //    //BD Access
                //    if (Factory.Instance.VerifieDoConnect(null,null,txtBD.Text,null,null,cboTypeDB.SelectedIndex))
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
            Utilisateur utilisateur = new Utilisateur();//"superuser", "superpassword"
            utilisateur.Nomuser = txtUserSimple.Text;
            utilisateur.Motpass = txtPwdUserSimple.Text;

            if (Factory.Instance.verifieLoginUser(utilisateur.Nomuser, utilisateur.Motpass)[2].Equals("0")) ok = true;//L'Utilisateur est un SuperAdministrateur Provincial
            else if (Factory.Instance.verifieLoginUser(utilisateur.Nomuser, utilisateur.Motpass)[2].Equals("0")) ok = true;//L'Utilisateur est un Administrateur Provincial
            else if (Factory.Instance.verifieLoginUser(utilisateur.Nomuser, utilisateur.Motpass)[2].Equals("1")) ok = true;//L'Utilisateur est un Recensseur
            else if (Factory.Instance.verifieLoginUser(utilisateur.Nomuser, utilisateur.Motpass)[2].Equals("2")) ok = true;//L'Utilisateur est un Agent de commune
            //else if (Factory.Instance.verifieLoginUser(utilisateur.Nomuser, utilisateur.Motpass)[2].Equals("3")) ok = true;//L'Utilisateur est un Recensseur 
            //else if (Factory.Instance.verifieLoginUser(utilisateur.Nomuser, utilisateur.Motpass)[2].Equals("4")) ok = true;//L'Utilisateur est un Agent de commune 

            if (ok)
            {
                MessageBox.Show("Connexion réussie", "Connexion à la base de données", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //On cree la table pour param des users
                try
                {
                    Factory.Instance.CreateTableTempUser();
                }
                catch (Exception) { }

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

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void ConnexionBd_Load(object sender, EventArgs e)
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
                    cboServeur.Text = Factory.Instance.loadParam(0)[0];
                    txtBD.Text = Factory.Instance.loadParam(0)[1];
                    txtNomUser.Text = Factory.Instance.loadParam(0)[2];
                    txtPort.Text = Factory.Instance.loadParam(0)[3];
                }
            }
            catch (Exception) { }
            txtUserSimple.Focus();
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
                    cboServeur.Text = Factory.Instance.loadParam(0)[0];
                    txtBD.Text = Factory.Instance.loadParam(0)[1];
                    txtNomUser.Text = Factory.Instance.loadParam(0)[2];
                    txtPort.Text = Factory.Instance.loadParam(0)[3];
                }
                else if (cboTypeDB.SelectedIndex == 1)
                {
                    //MySQL
                    cboServeur.Text = Factory.Instance.loadParam(1)[0];
                    txtBD.Text = Factory.Instance.loadParam(1)[1];
                    txtNomUser.Text = Factory.Instance.loadParam(2)[2];
                }
                else if (cboTypeDB.SelectedIndex == 2)
                {
                    //SQLServer
                    cboServeur.Text = Factory.Instance.loadParam(2)[0];
                    txtBD.Text = Factory.Instance.loadParam(2)[1];
                    txtNomUser.Text = Factory.Instance.loadParam(2)[2];
                }
                //else if (cboTypeDB.SelectedIndex == 3)
                //{
                //    //Access
                //    txtBD.Text = Factory.Instance.loadParam(3)[0];
                //}
            }
            catch (Exception) { }
        }

        private void btfermer_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
