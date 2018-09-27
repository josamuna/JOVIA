using System;
using System.Collections.Generic;
using System.Windows.Forms;
using JOVIA_LIB;

namespace JOVIA_WIN
{
    public partial class Modem : UserControl
    {
        Utilisateur utilisateur = new Utilisateur();
        EnvoieSMS envoie = new EnvoieSMS();

        public Modem()
        {
            InitializeComponent();
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            try
            {
                SMS.Instance.OpenConnection(SMS.Instance.RecupNumeroPort(cboPort.Text), Convert.ToInt32(cboBaud.Text), Convert.ToInt32(cboTimeOut.Text));

                SMS.Instance.InstanceEcouteurReceptionSMS();

                cmdConnect.Enabled = false;
                if (cmdConnect.Enabled == true) cmdDisconnect.Enabled = false;
                else cmdDisconnect.Enabled = true;

                MessageBox.Show("Connexion réussie", "Connexion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Echec de la connexion", "Connexion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Modem_Load(object sender, EventArgs e)
        {
            cmdConnect.Enabled = true;
            cmdDisconnect.Enabled = false;
            cmdTest.Visible = false;

            try
            {
                loadPortModem();  
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Certains paramètres n'ont pas été chargés, rassurez - vous que le Modem est braché", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors du chargement des paramètres du Modem", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            loadParamModem();

            //On verifie le statut de la connection au port pour activer ou 
            //desactiver les bouton de connexion et deconnexion
            try
            {
                if (SMS.Instance.GetStatusConnectionModem())
                {
                    cmdConnect.Enabled = false;
                    cmdDisconnect.Enabled = true;
                }else
                {
                    cmdConnect.Enabled = true;
                    cmdDisconnect.Enabled = false;
                }
            }
            catch (Exception) { }
        }

        private void loadPortModem()
        {
            cboPort.Items.Clear();
            foreach (string val in SMS.Instance.GetAllports())
                cboPort.Items.Add(val);
            cboPort.SelectedIndex = 0;
        }

        private void loadParamModem()
        {
            cboBaud.Items.Clear();
            cboTimeOut.Items.Clear();

            foreach (int val in SMS.Instance.LoadBaudPorts())
                cboBaud.Items.Add(val);
            cboBaud.SelectedIndex = 0;

            foreach (int val in SMS.Instance.LoadTimeOut())
                cboTimeOut.Items.Add(val);
            cboTimeOut.SelectedIndex = 0;
        }

        private void cmdDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                SMS.Instance.CloseConnection();

                cmdDisconnect.Enabled = false;
                if (cmdDisconnect.Enabled == true) cmdConnect.Enabled = false;
                else cmdConnect.Enabled = true;
            }
            catch (Exception)
            {
                cmdConnect.Enabled = true;
                cmdDisconnect.Enabled = false;
                MessageBox.Show("Port du Modem déconnecté", "Déconnexion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmdSendMsg_Click(object sender, EventArgs e)
        {
            List<string> liste = new List<string>();
            EnvoieSMS envoie = new EnvoieSMS();
            EnvoieMsgAgent envoieAgent = new EnvoieMsgAgent();
            bool okSendMsg = false;

            try
            {
                envoie.Destinataire = txtDestinataire.Text;
                envoie.MessageEnvoye = txtMsgSend.Text;

                envoieAgent.NumeroTelephone = txtDestinataire.Text;
                envoieAgent.Message_envoye = txtMsgSend.Text;

                if (chkMultiple.Checked) liste = SMS.Instance.SendManySMS(txtMsgSend.Text, txtDestinataire.Text);
                else SMS.Instance.SendOneSMS(txtMsgSend.Text, txtDestinataire.Text);
                okSendMsg = true;

                MessageBox.Show("Message envoyé", "Envoie SMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("Echec de l'envoie du SMS", "Envoie SMS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            try
            {
                if (okSendMsg)
                {
                    if (chkMultiple.Checked) Factory.Instance.SaveManySMS(envoie, liste);
                    else
                    {
                        envoie.Id = Factory.Instance.ReNewIdValue(envoie);
                        Factory.Instance.SaveOneSMS(envoie);
                    }
                    MessageBox.Show("Message enregistrés", "Envoie SMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("Message non enregistré", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors de l'insertion du(des) SMS envoyé(s)", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmdTest_Click(object sender, EventArgs e)
        {
            try
            {
                //Factory.Instance.insertData("4,AMIDA,KAMATE,NADINE,MASIALA,TUSABE,F,MARIE,TRUE,05/02/1980,josam,jos|099135032|27/12/2012");  Good Data
                //Factory.Instance.DoData("1,judith,,sifa,kuka sadam,emmanuella silisa,f,celibataire,true,22/03/1989,josam,jos|099135032|29/12/2012"); //Bad datenaissnce
                //Factory.Instance.insertData("2,01/05/1944,vianney,via|0813870366|27/12/2012");
                MessageBox.Show("Insertion", "Succes inserted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur d'insertion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                loadPortModem();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Certains paramètres n'ont pas été chargés, rassurez - vous que le Modem est braché ", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            loadParamModem();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
