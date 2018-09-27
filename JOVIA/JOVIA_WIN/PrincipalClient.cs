using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using JOVIA_LIB;
using JOVIA_LIB_SERVEUR;
using Microsoft.VisualBasic;

namespace JOVIA_WIN
{
    public partial class PrincipalClient : Form
    { 
        public PrincipalClient()
        {
            InitializeComponent();
            lblStatusMessage.Text = "JoVia - Version Client";
            Factory.ValUser = 0;
        }

        private void deseabledItems()
        {
            this.splitContainer1.Panel2.Controls.Clear();

            smPhotoWebcam.Enabled = false;
            mnUserManup.Enabled = false;
            mnManageUsers.Enabled = false;
            mnUserGestGroup.Enabled = false;
            mnServeur.Enabled = false;
            smTempMessage.Enabled = false;
            smParamModem.Enabled = false;
            smProvinceParam.Enabled = false;
            smAddModServeur.Enabled = false;

            smExecuterQuery.Enabled = false;
            smRestaurationBd.Enabled = false;

            lblVilleTer.Enabled = false;
            lblCommuneCheffQuaLoc.Enabled = false;
            lblAvVillage.Enabled = false;
            lblPersonneCarte.Enabled = false;

            btnEntite.Enabled = false;
            btnSousEntite.Enabled = false;
            btnAvVillage.Enabled = false;
            btnPersonneCarte.Enabled = false;

            rptTauxMortalite.Enabled = false;
            rptTauxNatalite.Enabled = false;
            rptTauxCroissance.Enabled = false;
        }

        private void btQuitter_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Principal_Leave(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblCommuneCheffQuaLoc_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                ComCheffQuartierLoc comcs = new ComCheffQuartierLoc();
                comcs.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(comcs);
            }
            else if (Factory.ValUser == 1 || Factory.ValUser == 2)//Utilisateur Serveur Central sur Serveur Provincial
            {
                ComCheffQuartierLocServeur comcs1 = new ComCheffQuartierLocServeur();
                comcs1.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(comcs1);
            }
            unitialiseLabels();
            ((Control)lblCommuneCheffQuaLoc).BackColor = Color.Silver;
            ((Control)lblCommuneCheffQuaLoc).ForeColor = Color.WhiteSmoke;
            
        }

        private void lblRecensseurAvVillage_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                AvenueVillageForm avrecens = new AvenueVillageForm();
                avrecens.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(avrecens);
            }
            else if (Factory.ValUser == 1 || Factory.ValUser == 2)//Utilisateur Serveur Central sur Serveur Provincial
            {
                AvenueVillageFormServeur avrecens = new AvenueVillageFormServeur();
                avrecens.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(avrecens);
            }
            unitialiseLabels();
            ((Control)lblAvVillage).BackColor = Color.Silver;
            ((Control)lblAvVillage).ForeColor = Color.WhiteSmoke;
        }

        private void lblPersonneCarte_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                PersonneCarte perscarte = new PersonneCarte();
                perscarte.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(perscarte);
            }
            else if (Factory.ValUser == 1 || Factory.ValUser == 2)//Utilisateur Serveur Central sur Serveur Provincial
            {
                PersonneCarteServeur perscarte = new PersonneCarteServeur();
                perscarte.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(perscarte);
            }
            unitialiseLabels();
            ((Control)lblPersonneCarte).BackColor = Color.Silver;
            ((Control)lblPersonneCarte).ForeColor = Color.WhiteSmoke;

        }

        private void lblAide_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                AideContent aide = new AideContent();
                aide.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(aide);
            }
            else if (Factory.ValUser == 1 || Factory.ValUser == 2)//Utilisateur Serveur Central sur Serveur Provincial
            {
                AideContentServeur aide = new AideContentServeur();
                aide.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(aide);
            }
            unitialiseLabels();
            ((Control)lblAide).BackColor = Color.Silver;
            ((Control)lblAide).ForeColor = Color.WhiteSmoke;
        }

        private void lblCommuneCheffQuaLoc_MouseEnter(object sender, EventArgs e)
        {
            ((Control)lblCommuneCheffQuaLoc).ForeColor = Color.WhiteSmoke;
            //((Control)lblCommuneCheffQuaLoc).BackColor = Color.Silver;
        }

        private void lblRecensseurAvVillage_MouseEnter(object sender, EventArgs e)
        {
            ((Control)lblAvVillage).ForeColor = Color.WhiteSmoke;
            //((Control)lblAvVillage).BackColor = Color.Silver;
        }

        private void lblAide_MouseEnter(object sender, EventArgs e)
        {
            ((Control)lblAide).ForeColor = Color.WhiteSmoke;
            //((Control)lblAide).BackColor = Color.Silver;
        }

        private void lblPersonneCarte_MouseEnter(object sender, EventArgs e)
        {
            ((Control)lblPersonneCarte).ForeColor = Color.WhiteSmoke;
            //((Control)lblPersonneCarte).BackColor = Color.Silver;
        }

        private void lblAide_MouseLeave(object sender, EventArgs e)
        {
            
            //((Control)lblAide).BackColor = Color.Transparent;

            if (((Control)lblAide).BackColor == Color.Silver)
            {
                ((Control)lblAide).ForeColor = Color.WhiteSmoke;
            }
            else
            {
                ((Control)lblAide).ForeColor = Color.Silver;
            }
        }

        private void lblPersonneCarte_MouseLeave(object sender, EventArgs e)
        {
            if (((Control)lblPersonneCarte).BackColor == Color.Silver)
            {
                ((Control)lblPersonneCarte).ForeColor = Color.WhiteSmoke;
            }
            else
            {
                ((Control)lblPersonneCarte).ForeColor = Color.Silver;
            }
        }

        private void lblRecensseurAvVillage_MouseLeave(object sender, EventArgs e)
        {
            if (((Control)lblAvVillage).BackColor == Color.Silver)
            {
                ((Control)lblAvVillage).ForeColor = Color.WhiteSmoke;
            }
            else
            {
                ((Control)lblAvVillage).ForeColor = Color.Silver;
            }
        }

        private void lblCommuneCheffQuaLoc_MouseLeave(object sender, EventArgs e)
        {

            if (((Control)lblCommuneCheffQuaLoc).BackColor == Color.Silver)
            {
                ((Control)lblCommuneCheffQuaLoc).ForeColor = Color.WhiteSmoke;
            }
            else
            {
                ((Control)lblCommuneCheffQuaLoc).ForeColor = Color.Silver;
            }
        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Factory.Instance.DropTableTempUser();
                Factory.Instance.closeConnection();
                Factory.Instance.EmptyFileParametersUser();
            }
            catch (Exception) { }
            try
            {
                SMS.Instance.CloseConnection();
            }
            catch (Exception) { };

            Application.Exit();
        }

        private void activationUserClient()
        {
            smDeconnexion.Enabled = true;
            smDeconnexionDistante.Enabled = false;
            smAddModServeur.Enabled = false;

            if (Factory.Instance.enabledDesabledObject() == 0)
            {
                //Tout est active pour l'administrateur Provincial
                smTempMessage.Enabled = true;
                smParamModem.Enabled = true;
                smPhotoWebcam.Enabled = true;
                mnUserManup.Enabled = true;
                mnUserGestGroup.Enabled = true;
                smProvinceParam.Enabled = true;
                smAddModServeur.Enabled = false;
                smExecuterQuery.Enabled = true;
                mnSauvegardeLocale.Enabled = true;
                mnSauvegardeDistante.Enabled = false;
                smRestaurationBd.Enabled = true;
                btnConnecteServerNational.Enabled = true;
                smConnexionDist.Enabled = true;

                btnEntite.Enabled = true;
                btnSousEntite.Enabled = true;
                btnAvVillage.Enabled = true;
                btnPersonneCarte.Enabled = true;

                lblVilleTer.Enabled = true;
                lblCommuneCheffQuaLoc.Enabled = true;
                lblAvVillage.Enabled = true;
                lblPersonneCarte.Enabled = true;
            }
            else if (Factory.Instance.enabledDesabledObject() == 1)
            {
                //Activation pour le recensseur
                smTempMessage.Enabled = false;
                smParamModem.Enabled = false;
                smPhotoWebcam.Enabled = false;
                mnUserManup.Enabled = false;
                mnUserGestGroup.Enabled = false;
                smProvinceParam.Enabled = false;
                smAddModServeur.Enabled = false;
                smExecuterQuery.Enabled = false;
                mnSauvegardeLocale.Enabled = false;
                mnSauvegardeDistante.Enabled = false;
                smRestaurationBd.Enabled = false;
                btnConnecteServerNational.Enabled = false;
                smConnexionDist.Enabled = false;

                btnEntite.Enabled = false;
                btnSousEntite.Enabled = false;
                btnAvVillage.Enabled = false;
                btnPersonneCarte.Enabled = false;

                lblVilleTer.Enabled = false;
                lblCommuneCheffQuaLoc.Enabled = false;
                lblAvVillage.Enabled = false;
                lblPersonneCarte.Enabled = false;
            }
            else if (Factory.Instance.enabledDesabledObject() == 2)
            {
                //Activation pour l'agent de commune
                smTempMessage.Enabled = false;
                smParamModem.Enabled = false;
                smPhotoWebcam.Enabled = true;
                mnUserManup.Enabled = false;
                mnUserGestGroup.Enabled = false;
                smProvinceParam.Enabled = false;
                smAddModServeur.Enabled = false;
                smExecuterQuery.Enabled = false;
                mnSauvegardeLocale.Enabled = false;
                mnSauvegardeDistante.Enabled = false;
                smRestaurationBd.Enabled = false;
                btnConnecteServerNational.Enabled = false;
                smConnexionDist.Enabled = false;

                btnEntite.Enabled = true;
                btnSousEntite.Enabled = true;
                btnAvVillage.Enabled = true;
                btnPersonneCarte.Enabled = true;

                lblVilleTer.Enabled = true;
                lblCommuneCheffQuaLoc.Enabled = true;
                lblAvVillage.Enabled = true;
                lblPersonneCarte.Enabled = true;
            }
        }

        private void PrincipalClient_Load(object sender, EventArgs e)
        {
            mnGpU.Enabled = false;
            //Activation des menus suivant l'utilisateur connecte 
            try
            {
                activationUserClient();
            }
            catch (Exception) { }
            this.splitContainer1.SplitterDistance = 195;
            //this.splitContainer1.MaximumSize = new System.Drawing.Size(824, 500);
        }

        private void lblVilleTer_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                VilleTerritoireForm pvt = new VilleTerritoireForm();
                pvt.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(pvt);
            }
            else if (Factory.ValUser == 1 || Factory.ValUser == 2)//Utilisateur Serveur Central sur Serveur Provincial
            {
                ProvinceForm pvt1 = new ProvinceForm();
                pvt1.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(pvt1);
            }
            unitialiseLabels();
            ((Control)lblVilleTer).BackColor = Color.Silver;
            ((Control)lblVilleTer).ForeColor = Color.WhiteSmoke;
        }

        //methode qui perme d'initialiser tout les labels à l'etat unitial
        private void unitialiseLabels()
        {
            ((Control)lblVilleTer).BackColor = Color.Transparent;
            ((Control)lblVilleTer).ForeColor = Color.Silver;

            ((Control)lblCommuneCheffQuaLoc).BackColor = Color.Transparent;
            ((Control)lblCommuneCheffQuaLoc).ForeColor = Color.Silver; 

            ((Control)lblAvVillage).BackColor = Color.Transparent;
            ((Control)lblAvVillage).ForeColor = Color.Silver;

            ((Control)lblPersonneCarte).BackColor = Color.Transparent; 
            ((Control)lblPersonneCarte).ForeColor = Color.Silver;

            ((Control)lblAide).BackColor = Color.Transparent;
            ((Control)lblAide).ForeColor = Color.Silver;
        }
        private void lblVilleTer_MouseLeave(object sender, EventArgs e)
        {
            if (((Control)lblVilleTer).BackColor == Color.Silver)
            {
                ((Control)lblVilleTer).ForeColor = Color.WhiteSmoke;
            }
            else
            {
                ((Control)lblVilleTer).ForeColor = Color.Silver;
            }
        }

        private void lblVilleTer_MouseEnter(object sender, EventArgs e)
        {
            ((Control)lblVilleTer).ForeColor = Color.WhiteSmoke;
            //((Control)lblVilleTer).BackColor = Color.Silver;
        }

        private void enableDesabledAdminServeurCentral()
        {
            smTempMessage.Enabled = false;
            smParamModem.Enabled = false;
            smPhotoWebcam.Enabled = true;
            mnUserManup.Enabled = true;
            mnUserGestGroup.Enabled = true;
            smAddModServeur.Enabled = true;
            smProvinceParam.Enabled = false;
            smExecuterQuery.Enabled = true;
            mnSauvegardeLocale.Enabled = false;
            mnSauvegardeDistante.Enabled = false;
            mnRapport.Enabled = true;
            btnConnecteServerNational.Enabled = false;
            smConnexionDist.Enabled = false;
            lblCommuneCheffQuaLoc.Enabled = true;
            lblAvVillage.Enabled = true;
            lblVilleTer.Enabled = true;
            lblPersonneCarte.Enabled = true;

            smDeconnexionDistante.Enabled = true;
            smDeconnexion.Enabled = false;
        }

        private void splitContainer1_Panel2_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (e.Control.Name.ToString().Equals("ConnectBdServeur"))
            //On active ou on desactive ce que la personne ne doit pas voir en se connectant au serveur centrale
            //En outre on lui donne acces au formulaire des personnes pour pouvoir en rechercher et les cas echeant 
            //delivrer une carte
            {
                try
                {
                    if (Factory.ValUser == 0)
                    { //On ne s'est pas connecte au serveur distant et ValUser = 0
                        smDeconnexion.Enabled = true;
                        mnSauvegardeDistante.Enabled = false;
                    }
                    else if (Factory.ValUser == 1) //Super Administrateur
                    {
                        //Connexion distante reussie
                        btnConnecteServerNational.Enabled = false;
                        smAddModServeur.Enabled = true;
                        mnSauvegardeLocale.Enabled = false;
                        smRestaurationBd.Enabled = false;
                        smConnexionDist.Enabled = false;
                        enableDesabledAdminServeurCentral();
                        mnSauvegardeDistante.Enabled = true;
                        lblStatusMessage.Text = "Vous êtes actuellement connecté au serveur distant";
                    }
                    else if (Factory1.Instance.enabledDesabledObject() == 0)//Administrateur serveur Central
                    {
                        //activation pour l'administrateur Central certain item pour le serveur Provincial sont inaccessibles
                        Factory.ValUser = 1;
                        enableDesabledAdminServeurCentral();
                        lblStatusMessage.Text = "Vous êtes actuellement connecté au serveur distant";
                    }
                    else if (Factory1.Instance.enabledDesabledObject() == 1)//Utilisateur simple serveur central
                    {
                        //Activation pour un utilisateur simple vis-a-vis du serveur Central
                        Factory.ValUser = 2;
                        smTempMessage.Enabled = false;
                        smParamModem.Enabled = false;
                        smPhotoWebcam.Enabled = true;
                        mnUserManup.Enabled = false;
                        mnUserGestGroup.Enabled = false;
                        smProvinceParam.Enabled = false;
                        smAddModServeur.Enabled = false;
                        smExecuterQuery.Enabled = false;
                        mnSauvegardeLocale.Enabled = false;
                        mnSauvegardeDistante.Enabled = false;
                        smRestaurationBd.Enabled = false;
                        smConnexionDist.Enabled = false;
                        btnConnecteServerNational.Enabled = false;
                        lblCommuneCheffQuaLoc.Enabled = true;
                        lblAvVillage.Enabled = true;
                        lblVilleTer.Enabled = true;
                        lblPersonneCarte.Enabled = true;

                        smDeconnexionDistante.Enabled = true;
                        smDeconnexion.Enabled = false;

                        lblStatusMessage.Text = "Vous êtes actuellement connecté au serveur distant";
                    }
                    //else if (Factory.ValUser > 0)
                    //{
                    //    if (Factory.IsAdmin) mnSauvegardeDistante.Enabled = true;
                    //    else mnSauvegardeDistante.Enabled = false;
                    //}

                    //if(Factory.IsAdmin) mnSauvegardeDistante.Enabled = true;
                    //else mnSauvegardeDistante.Enabled = false;
                }
                catch (Exception) { }
            }
        }

        private void smDeconnexionDistante_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();
            try
            {
                Factory1.Instance.closeConnection();
                Factory1.Instance.EmptyFileParametersUserDistante();
                btnConnecteServerNational.Enabled = true;
                smProvinceParam.Enabled = true;
                smAddModServeur.Enabled = false;
                mnSauvegardeLocale.Enabled = true;
                mnSauvegardeDistante.Enabled = true;
                smRestaurationBd.Enabled = true;
                smConnexionDist.Enabled = true;
                smDeconnexionDistante.Enabled = false;
                smDeconnexion.Enabled = true;
                Factory.ValUser = 0;
                mnSauvegardeDistante.Enabled = false;
                lblStatusMessage.Text = "JoVia - Version Client";
                //On reactive les item usuelles pour le user connecte au Serveur provincial
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur lors de la déconnexion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            //Activation des menus suivant l'utilisateur connecte 
            try
            {
                activationUserClient();
            }
            catch (Exception) { }
        }

        private void smConnexionDist_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            ConnectBdServeur pvt = new ConnectBdServeur();
            pvt.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(pvt);
        }

        private void smRestaurationBd_Click(object sender, EventArgs e)
        {
            dlgFile.Title = "Veuillez sélectionner le fichier pour la restauration";

            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                string cheminAccesBd = dlgOpen.FileName;
                try
                {
                    if (Factory.Instance.GetTypeSGBDConnecting() == 1)
                    {
                        string lecteur = Interaction.InputBox("Veuillez saisir la lettre du lecteur à partir duquel vous voulez éffectuer la restauration", "Lecteur source de restauration", "C", 300, 300);
                        string versionPostGreSQL = Interaction.InputBox("Veuillez saisir le numéro de la version de PostGreSQL que vous utilisez", "Version de PostGreSQL", "9.1", 300, 300);
                        string message = Factory.Instance.RestoreDataBase(cheminAccesBd, lecteur, versionPostGreSQL);
                        MessageBox.Show("restauration éffectuée avec succès", "restauration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string msg = Factory.Instance.RestoreDataBase(cheminAccesBd, null, null);
                        MessageBox.Show("Restauration éffectuée avec succès à partir de l'emplacement | " + cheminAccesBd + " |", "Restauration de la base des données", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Echec de la restauration à parir de l'emplacement | " + cheminAccesBd + " |", "Restauration de la base des données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void mnSauvegardeLocale_Click(object sender, EventArgs e)
        {
            dlgFile.Title = "Veuillez sélectionner l'emplacement de sauvegarde";

            if (dlgFile.ShowDialog() == DialogResult.OK)
            {
                string cheminAccesBd = dlgFile.FileName;
                try
                {
                    if (Factory.Instance.GetTypeSGBDConnecting() == 1)
                    {
                        string lecteur = Interaction.InputBox("Veuillez saisir la lettre du lecteur sur lequel vous aller éffectuer la sauvegarde", "Lecteur de sauvegarde", "C", 300, 300);
                        string versionPostGreSQL = Interaction.InputBox("Veuillez saisir le numéro de la version de PostGreSQL que vous utilisez", "Version de PostGreSQL", "9.1", 300, 300);
                        string message = Factory.Instance.BackupLocalDataBase(cheminAccesBd, lecteur, versionPostGreSQL);
                        MessageBox.Show("Sauvegarde éffectuée avec succès dans l'emplacement | " + message + " |", "Sauvegarde locale de la base des données", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string message = Factory.Instance.BackupLocalDataBase(cheminAccesBd, null, null);
                        MessageBox.Show("Sauvegarde éffectuée avec succès dans l'emplacement | " + cheminAccesBd + " |", "Sauvegarde locale de la base des données", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Echec de la sauvegarde dans l'emplacement | " + cheminAccesBd + " |", "Sauvegarde locale de la base des données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void smExecuterQuery_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                ExecuteQuery execQuery = new ExecuteQuery();
                execQuery.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(execQuery);
            }
            else if (Factory.ValUser == 1)//Utilisateur Serveur Central sur Serveur Provincial
            {
                ExecuteQueryServeur execQuery1 = new ExecuteQueryServeur();
                execQuery1.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(execQuery1);
            }
        }

        private void mnUserManup_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                ManipulationUtilisateur mu = new ManipulationUtilisateur();
                mu.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(mu);
            }
            else if (Factory.ValUser == 1)//Utilisateur Serveur Central sur Serveur Provincial
            {
                ManipulationUtilisateurServeur mu1 = new ManipulationUtilisateurServeur();
                mu1.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(mu1);
            }
        }

        private void mnPhotoWebcam_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                ParametreWebCam wcm = new ParametreWebCam();
                wcm.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(wcm);
            }
            else if (Factory.ValUser == 1)//Utilisateur Serveur Central sur Serveur Provincial
            {
                ParametreWebCamServeur wcm = new ParametreWebCamServeur();
                wcm.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(wcm);
            }
        }

        private void btnAide_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                AideContent aide = new AideContent();
                aide.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(aide);
            }
            else if (Factory.ValUser == 1 || Factory.ValUser == 2)//Utilisateur Serveur Central sur Serveur Provincial
            {
                AideContentServeur aide1 = new AideContentServeur();
                aide1.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(aide1);
            }
        }

        private void smParamModem_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                Modem mdm = new Modem();
                mdm.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(mdm);
            }
        }

        private void smTempMessage_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                MesageNotInserted msnInsert = new MesageNotInserted();
                msnInsert.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(msnInsert);
            }
        }

        private void smDeconnexion_Click(object sender, EventArgs e)
        {
            try
            {
                this.splitContainer1.Panel2.Controls.Clear();
                Factory.Instance.DropTableTempUser();
                Factory.Instance.closeConnection();
                Factory.Instance.EmptyFileParametersUser();
                smDeconnexion.Enabled = false;
                smDeconnexionDistante.Enabled = false;
                btnConnecteServerNational.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur lors de la déconnexion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            try
            {
                SMS.Instance.CloseConnection();
            }
            catch (Exception) { };
            deseabledItems();
        }

        private void btnClosePrincipal_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Voulez - vous vraiment quitter l'application ?", "Quitter l'application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                //this.Close();
                Application.Exit();
            }

            try
            {
                Factory.Instance.closeConnection();
            }
            catch (Exception) { }
        }

        private void smQuitApp_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Voulez - vous vraiment quitter l'application ?", "Quitter l'application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                //this.Close();
                Application.Exit();
            }

            try
            {
                Factory.Instance.closeConnection();
            }
            catch (Exception) { }
        }

        private void smConnecteServerNational_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            ConnectBdServeur cb = new ConnectBdServeur();
            cb.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(cb);

            smDeconnexion.Enabled = false;
        }

        private void btnEntite_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                VilleTerritoireForm pvt = new VilleTerritoireForm();
                pvt.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(pvt);
            }
            else if (Factory.ValUser == 1 || Factory.ValUser == 2)//Utilisateur Serveur Central sur Serveur Provincial
            {
                ProvinceForm pvt1 = new ProvinceForm();
                pvt1.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(pvt1);
            }
        }

        private void btnSousEntite_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                ComCheffQuartierLoc comcs = new ComCheffQuartierLoc();
                comcs.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(comcs);
            }
            else if (Factory.ValUser == 1 || Factory.ValUser == 2)//Utilisateur Serveur Central sur Serveur Provincial
            {
                ComCheffQuartierLocServeur comcs1 = new ComCheffQuartierLocServeur();
                comcs1.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(comcs1);
            }
        }

        private void btnAvVillage_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                AvenueVillageForm avrecens = new AvenueVillageForm();
                avrecens.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(avrecens);
            }
            else if (Factory.ValUser == 1 || Factory.ValUser == 2)//Utilisateur Serveur Central sur Serveur Provincial
            {
                AvenueVillageFormServeur avrecens = new AvenueVillageFormServeur();
                avrecens.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(avrecens);
            }
        }

        private void btnPersonneCarte_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                PersonneCarte perscarte = new PersonneCarte();
                perscarte.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(perscarte);
            }
            else if (Factory.ValUser == 1 || Factory.ValUser == 2)//Utilisateur Serveur Central sur Serveur Provincial
            {
                PersonneCarteServeur perscarte = new PersonneCarteServeur();
                perscarte.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(perscarte);
            }
        }

        private void smProvinceParam_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                ParametreProvinceForm msnInsert = new ParametreProvinceForm();
                msnInsert.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(msnInsert);
            }
        }

        private void mnSauvegardeDistante_Click(object sender, EventArgs e)
        {
            try
            {
                Factory.Instance.ExecuteBackupDistant();
                MessageBox.Show("Sauvegarde éffectuée avec succès", "Sauvegarde de la base des données", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur lors de la sauvegarde distante de la base des données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lblVilleTer_MouseMove(object sender, MouseEventArgs e)
        {
            ((Control)lblVilleTer).ForeColor = Color.WhiteSmoke;
        }

        private void rptDensitePopulation_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                RptDensite form = new RptDensite();
                form.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(form);
            }
            else if (Factory.ValUser == 1 || Factory.ValUser == 2)//Utilisateur Serveur Central sur Serveur Provincial
            {
                //RptDensiteServeur form = new RptDensiteServeur();
                //form.Parent = this.splitContainer1.Panel2;
                //this.splitContainer1.Panel2.Controls.Add(form);
            }
        }

        private void rptTauxMortalite_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                RptTautMortalite form = new RptTautMortalite();
                form.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(form);
            }
            else if (Factory.ValUser == 1 || Factory.ValUser == 2)//Utilisateur Serveur Central sur Serveur Provincial
            {
                //RptTautMortaliteServeur form = new RptTautMortaliteServeur();
                //form.Parent = this.splitContainer1.Panel2;
                //this.splitContainer1.Panel2.Controls.Add(form);
            }
        }

        private void rptTauxNatalite_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                RptTauxNatalite form = new RptTauxNatalite();
                form.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(form);
            }
            else if (Factory.ValUser == 1 || Factory.ValUser == 2)//Utilisateur Serveur Central sur Serveur Provincial
            {
                //RptTauxNataliteSereur form = new RptTauxNataliteSereur();
                //form.Parent = this.splitContainer1.Panel2;
                //this.splitContainer1.Panel2.Controls.Add(form);
            }
        }

        private void rptTauxCroissance_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                RptTauxCroissance form = new RptTauxCroissance();
                form.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(form);
            }
            else if (Factory.ValUser == 1 || Factory.ValUser == 2)//Utilisateur Serveur Central sur Serveur Provincial
            {
                //RptTauxCroissanceServeur form = new RptTauxCroissanceServeur();
                //form.Parent = this.splitContainer1.Panel2;
                //this.splitContainer1.Panel2.Controls.Add(form);
            }
        }

        private void carteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            if (Factory.ValUser == 0)//Utilisateur Provincial
            {
                RptCartePersonne form = new RptCartePersonne();
                form.Parent = this.splitContainer1.Panel2;
                this.splitContainer1.Panel2.Controls.Add(form);
            }
            else if (Factory.ValUser == 1 || Factory.ValUser == 2)//Utilisateur Serveur Central sur Serveur Provincial
            {
                //RptCartePersonneServeur form = new RptCartePersonneServeur();
                //form.Parent = this.splitContainer1.Panel2;
                //this.splitContainer1.Panel2.Controls.Add(form);
            }
        }                   
    }
}
