using System;
using System.Drawing;
using System.Windows.Forms;
using JOVIA_LIB_SERVEUR;
using Microsoft.VisualBasic;

namespace JOVIA_WIN_SERVEUR
{
    public partial class PrincipalServeur : Form
    {
        public PrincipalServeur()
        {
            InitializeComponent();
            lblStatusMessage.Text = "JoVia - Version Serveur";
        }

        private void deseabledItems()
        {
            this.splitContainer1.Panel2.Controls.Clear();

            mnPhotoWebcam.Enabled = false;
            mnUserManup.Enabled = false;
            mnManageUsers.Enabled = false;
            mnGpU.Enabled = false;
            mnServeur.Enabled = false;

            smExecuterQuery.Enabled = false;
            smSauvegardeBd.Enabled = false;
            smRestaurationBd.Enabled = false;

            lblProvinceVilleTer.Enabled = false;
            lblCommuneCheffQuaLoc.Enabled = false;
            lblRecensseurAvVillage.Enabled = false;
            lblPersonneCarte.Enabled = false;

            btnEntite.Enabled = false;
            btnSousEntite.Enabled = false;
            btnAvVillage.Enabled = false;
            btnPersonneCarte.Enabled = false;

            rptTauxMortalite.Enabled = false;
            rptTauxNatalite.Enabled = false;
            rptTauxCroissance.Enabled = false;
            rptDensitePopulation.Enabled = false;
        }

        //methode qui perme d'initialiser tout les labels à l'etat unitial
        private void unitialiseLabels()
        {
            ((Control)lblProvinceVilleTer).BackColor = Color.Transparent;
            ((Control)lblProvinceVilleTer).ForeColor = Color.White;

            ((Control)lblCommuneCheffQuaLoc).BackColor = Color.Transparent;
            ((Control)lblCommuneCheffQuaLoc).ForeColor = Color.White;

            ((Control)lblRecensseurAvVillage).BackColor = Color.Transparent;
            ((Control)lblRecensseurAvVillage).ForeColor = Color.White;

            ((Control)lblPersonneCarte).BackColor = Color.Transparent;
            ((Control)lblPersonneCarte).ForeColor = Color.White;

            ((Control)lblAide).BackColor = Color.Transparent;
            ((Control)lblAide).ForeColor = Color.White;
        }

        private void Principal_Leave(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblCommuneCheffQuaLoc_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            ComCheffQuartierLocServeur comcs = new ComCheffQuartierLocServeur();
            comcs.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(comcs);

            unitialiseLabels();
            ((Control)lblCommuneCheffQuaLoc).BackColor = Color.White;
            ((Control)lblCommuneCheffQuaLoc).ForeColor = Color.Brown;
        }

        private void lblRecensseurAvVillage_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            AvenueVillageFormServeur avrecens = new AvenueVillageFormServeur();
            avrecens.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(avrecens);

            unitialiseLabels();
            ((Control)lblRecensseurAvVillage).BackColor = Color.White;
            ((Control)lblRecensseurAvVillage).ForeColor = Color.Brown;
        }

        private void lblPersonneCarte_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            PersonneCarteServeur perscarte = new PersonneCarteServeur();
            perscarte.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(perscarte);

            unitialiseLabels();
            ((Control)lblPersonneCarte).BackColor = Color.White;
            ((Control)lblPersonneCarte).ForeColor = Color.Brown;
        }

        private void lblAide_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            AideContentServeur aide = new AideContentServeur();
            aide.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(aide);

            unitialiseLabels();
            ((Control)lblAide).BackColor = Color.White;
            ((Control)lblAide).ForeColor = Color.Brown;
        }

        private void lblCommuneCheffQuaLoc_MouseEnter(object sender, EventArgs e)
        {
            ((Control)lblCommuneCheffQuaLoc).ForeColor = Color.Brown;
            //((Control)lblCommuneCheffQuaLoc).BackColor = Color.Silver;
        }

        private void lblRecensseurAvVillage_MouseEnter(object sender, EventArgs e)
        {
            ((Control)lblRecensseurAvVillage).ForeColor = Color.Brown;
            //((Control)lblAvVillage).BackColor = Color.Silver;
        }

        private void lblAide_MouseEnter(object sender, EventArgs e)
        {
            ((Control)lblAide).ForeColor = Color.Brown;
            //((Control)lblAide).BackColor = Color.Silver;
        }

        private void lblPersonneCarte_MouseEnter(object sender, EventArgs e)
        {
            ((Control)lblPersonneCarte).ForeColor = Color.Brown;
            //((Control)lblPersonneCarte).BackColor = Color.Silver;
        }

        private void lblAide_MouseLeave(object sender, EventArgs e)
        {
            ((Control)lblAide).ForeColor = Color.White;
            //((Control)lblAide).BackColor = Color.Transparent;
        }

        private void lblPersonneCarte_MouseLeave(object sender, EventArgs e)
        {
            ((Control)lblPersonneCarte).ForeColor = Color.White;
            //((Control)lblPersonneCarte).BackColor = Color.Transparent;
        }

        private void lblRecensseurAvVillage_MouseLeave(object sender, EventArgs e)
        {
            ((Control)lblRecensseurAvVillage).ForeColor = Color.White;
            //((Control)lblAvVillage).BackColor = Color.Transparent;
        }

        private void lblCommuneCheffQuaLoc_MouseLeave(object sender, EventArgs e)
        {
            ((Control)lblCommuneCheffQuaLoc).ForeColor = Color.White;
            //((Control)lblCommuneCheffQuaLoc).BackColor = Color.Transparent;
        }

        private void Principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Factory1.Instance.closeConnection();
                Factory1.Instance.EmptyFileParametersUser();
            }
            catch (Exception) { }

            Application.Exit();
        }

        private void lblVilleTer_MouseLeave(object sender, EventArgs e)
        {
            ((Control)lblProvinceVilleTer).ForeColor = Color.White;
            //((Control)lblProvinceVilleTer).BackColor = Color.Transparent;
        }

        private void lblVilleTer_MouseEnter(object sender, EventArgs e)
        {
            ((Control)lblProvinceVilleTer).ForeColor = Color.Brown;
            //((Control)lblProvinceVilleTer).BackColor = Color.Silver;
        }

        private void smQuitter_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Voulez - vous vraiment quitter l'application ?", "Quitter l'application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                //this.Close();
                Application.Exit();
            }

            try
            {
                Factory1.Instance.closeConnection();
                Factory1.Instance.EmptyFileParametersUser();
            }
            catch (Exception) { }
        }

        private void smContenu_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            AideContentServeur aide = new AideContentServeur();
            aide.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(aide);
        }

        private void smAPropos_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            AProposServeur apropo = new AProposServeur();
            apropo.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(apropo);
        }

        private void btnQuitApp_Click(object sender, EventArgs e)
        {
            try
            {
                Factory1.Instance.closeConnection();
                btnDisconnect.Enabled = false;
                smDeconnexion.Enabled = false;
            }
            catch (Exception) { }
            deseabledItems();
        }

        private void mnUserManup_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            ManipulationUtilisateurServeur mu = new ManipulationUtilisateurServeur();
            mu.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(mu);
        }

        private void mnPhotoWebcam_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            ParametreWebCamServeur wcm = new ParametreWebCamServeur();
            wcm.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(wcm);
            this.splitContainer1.Panel2.Controls.Clear();
        }

        private void smDeconnexion_Click(object sender, EventArgs e)
        {
            try
            {
                Factory1.Instance.closeConnection();
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors de la déconnexion", "Déconnexion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            deseabledItems();
        }

        private void PrincipalServeur_Load(object sender, EventArgs e)
        {
            mnGpU.Enabled = false;
            this.splitContainer1.SplitterDistance = 195;
            //MessageBox.Show("Je suis active1");
            //Activation des menus suivant l'utilisateur connecte 
            try
            {
                if (Factory1.Instance.enabledDesabledObject() == 0)
                {
                    //Tout est active pour l'administrateur Provincial
                    mnPhotoWebcam.Enabled = true;
                    mnUserManup.Enabled = true;
                    mnManageUsers.Enabled = true;
                    mnGpU.Enabled = true;
                    mnServeur.Enabled = true;

                    smExecuterQuery.Enabled = true;
                    smSauvegardeBd.Enabled = true;
                    smRestaurationBd.Enabled = true;

                    lblProvinceVilleTer.Enabled = true;
                    lblCommuneCheffQuaLoc.Enabled = true;
                    lblRecensseurAvVillage.Enabled = true;
                    lblPersonneCarte.Enabled = true;

                    btnEntite.Enabled = true;
                    btnSousEntite.Enabled = true;
                    btnAvVillage.Enabled = true;
                    btnPersonneCarte.Enabled = true;
                }
                else if (Factory1.Instance.enabledDesabledObject() == 1)
                {
                    //Activation pour Utilisateur simple
                    mnPhotoWebcam.Enabled = true;
                    mnUserManup.Enabled = false;
                    mnManageUsers.Enabled = true;
                    mnServeur.Enabled = false;

                    mnGpU.Enabled = false;
                    smExecuterQuery.Enabled = false;
                    smSauvegardeBd.Enabled = false;
                    smRestaurationBd.Enabled = false;

                    lblProvinceVilleTer.Enabled = false;
                    lblCommuneCheffQuaLoc.Enabled = true;
                    lblRecensseurAvVillage.Enabled = true;
                    lblPersonneCarte.Enabled = true;

                    btnEntite.Enabled = false;
                    btnSousEntite.Enabled = true;
                    btnAvVillage.Enabled = true;
                    btnPersonneCarte.Enabled = true;
                }
            }
            catch (Exception) { } 
        }

        private void smSauvegardeBd_Click(object sender, EventArgs e)
        {
            dlgFile.Title = "Veuillez sélectionner l'emplacement de sauvegarde";

            if (dlgFile.ShowDialog() == DialogResult.OK)
            {
                string cheminAccesBd = dlgFile.FileName;
                try
                {
                    if (Factory1.Instance.GetTypeSGBDConnecting() == 1)
                    {
                        string lecteur = Interaction.InputBox("Veuillez saisir la lettre du lecteur sur lequel vous aller éffectuer la sauvegarde", "Lecteur de sauvegarde", "C", 300, 300);
                        string versionPostGreSQL = Interaction.InputBox("Veuillez saisir le numéro de la version de PostGreSQL que vous utilisez", "Version de PostGreSQL", "9.1", 300, 300);
                        string message = Factory1.Instance.BackupLocalDataBase(cheminAccesBd, lecteur, versionPostGreSQL);
                        MessageBox.Show("Sauvegarde éffectuée avec succès dans l'emplacement | " + message + " |", "Sauvegarde locale de la base des données", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string message = Factory1.Instance.BackupLocalDataBase(cheminAccesBd, null, null);
                        MessageBox.Show("Sauvegarde éffectuée avec succès dans l'emplacement | " + cheminAccesBd + " |", "Sauvegarde locale de la base des données", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Echec de la sauvegarde dans l'emplacement | " + cheminAccesBd + " |", "Sauvegarde locale de la base des données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void smRestaurationBd_Click(object sender, EventArgs e)
        {
            dlgFile.Title = "Veuillez sélectionner le fichier pour la restauration";

            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                string cheminAccesBd = dlgOpen.FileName;
                try
                {
                    if (Factory1.Instance.GetTypeSGBDConnecting() == 1)
                    {
                        string lecteur = Interaction.InputBox("Veuillez saisir la lettre du lecteur à partir duquel vous voulez éffectuer la restauration", "Lecteur source de restauration", "C", 300, 300);
                        string versionPostGreSQL = Interaction.InputBox("Veuillez saisir le numéro de la version de PostGreSQL que vous utilisez", "Version de PostGreSQL", "9.1", 300, 300);
                        string message = Factory1.Instance.RestoreDataBase(cheminAccesBd, lecteur, versionPostGreSQL);
                        MessageBox.Show("Sauvegarde éffectuée avec succès à partir de l'emplacement | " + message + " |", "Sauvegarde locale de la base des données", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string msg = Factory1.Instance.RestoreDataBase(cheminAccesBd, null, null);
                        MessageBox.Show("Restauration éffectuée avec succès à partir de l'emplacement | " + cheminAccesBd + " |", "Restauration de la base des données", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Echec de la restauration à parir de l'emplacement | " + cheminAccesBd + " |", "Restauration de la base des données", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void smExecuterQuery_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            ExecuteQueryServeur execQuery = new ExecuteQueryServeur();
            execQuery.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(execQuery);
        }

        private void btnAide_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            AideContentServeur aide = new AideContentServeur();
            aide.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(aide);
        }

        private void lblProvinceVilleTer_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            ProvinceForm pvt = new ProvinceForm();
            pvt.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(pvt);

            unitialiseLabels();
            ((Control)lblProvinceVilleTer).BackColor = Color.White;
            ((Control)lblProvinceVilleTer).ForeColor = Color.Brown;
        }

        private void btnEntite_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            ProvinceForm pvt = new ProvinceForm();
            pvt.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(pvt);
        }

        private void btnSousEntite_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            ComCheffQuartierLocServeur comcs = new ComCheffQuartierLocServeur();
            comcs.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(comcs);
        }

        private void btnPersonneCarte_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            PersonneCarteServeur perscarte = new PersonneCarteServeur();
            perscarte.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(perscarte);
        }

        private void btnAvVillage_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            AvenueVillageFormServeur avrecens = new AvenueVillageFormServeur();
            avrecens.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(avrecens);
        }

        private void mnServeur_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();

            ServeurOperation serveur = new ServeurOperation();
            serveur.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(serveur);
        }

        private void btnClosePrincipal_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Voulez - vous vraiment quitter l'application ?", "Quitter l'applcation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                //this.Close();
                Application.Exit();
            }
        }

        private void lblProvinceVilleTer_MouseMove(object sender, MouseEventArgs e)
        {
            ((Control)lblProvinceVilleTer).ForeColor = Color.Brown;
        }

        private void lblCommuneCheffQuaLoc_MouseMove(object sender, MouseEventArgs e)
        {
            ((Control)lblCommuneCheffQuaLoc).ForeColor = Color.Brown;
        }

        private void lblAvVillage_MouseMove(object sender, MouseEventArgs e)
        {
            ((Control)lblRecensseurAvVillage).ForeColor = Color.Brown;
        }

        private void lblPersonneCarte_MouseMove(object sender, MouseEventArgs e)
        {
            ((Control)lblPersonneCarte).ForeColor = Color.Brown;
        }

        private void lblAide_MouseMove(object sender, MouseEventArgs e)
        {
            ((Control)lblAide).ForeColor = Color.Brown;
        }

        private void rptTauxMortalite_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();
            RptTautMortaliteServeur form = new RptTautMortaliteServeur();
            form.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(form);
        }

        private void rptTauxNatalite_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();
            RptTautMortaliteServeur form = new RptTautMortaliteServeur();
            form.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(form);
        }

        private void rptTauxCroissance_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();
            RptTautMortaliteServeur form = new RptTautMortaliteServeur();
            form.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(form);
        }

        private void rptDensitePopulation_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();
            RptTautMortaliteServeur form = new RptTautMortaliteServeur();
            form.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(form);
        }

        private void carteIdentite_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2.Controls.Clear();
            RptCartePersonneServeur form = new RptCartePersonneServeur();
            form.Parent = this.splitContainer1.Panel2;
            this.splitContainer1.Panel2.Controls.Add(form);
        }
    }
}
