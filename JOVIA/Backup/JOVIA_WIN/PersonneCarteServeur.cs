using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using JOVIA_LIB_SERVEUR;
//using Microsoft.VisualBasic;

namespace JOVIA_WIN
{
    public partial class PersonneCarteServeur : UserControl
    {
        private string pathPhotoLoad = null;
        PersonneServeur personne = new PersonneServeur();
        PhotoServeur photoPers = new PhotoServeur();
        CarteServeur carte = new CarteServeur();
        AvenueVillageServeur avenueVil = new AvenueVillageServeur();

        public PersonneCarteServeur()
        {
            InitializeComponent();
        }

        private void initialisePersonne()
        {
            lblIdPersonne.Text = "";
            txtNomPersonne.Clear();
            txtPNomPersonne.Clear();
            txtPrenomPersonne.Clear();
            cboNomPerePersonne.Text = "";
            cboNomMerePersonne.Text = "";
            txtNumAvenuePersonne.Clear();
            txtNbrEnfantPersonne.Text = "0";
            lblNumNationalPersonne.Text = "";
            txtDateNaissancePersonne.Text = DateTime.Today.ToShortDateString();
            cboNiveauEtudePersonne.SelectedIndex = 0;
            cboEtatCivPersonne.SelectedIndex = 0;
            cboSexePersonne.SelectedIndex = 0;
            pbPhotoPersonne.Image = null;
            txtNomPersonne.Focus();
        }

        private void initialiseCarte()
        {
            lblIdCarte.Text = "";
            txtDateLivraison.Text = DateTime.Today.ToLongDateString();
            txtDateLivraison.Focus();
        }

        private void chargementCombo()
        {
            //Chargement des combo box pour les personnes et les avenues/Villages
            cboEtatCivPersonne.Items.Clear();
            cboNiveauEtudePersonne.Items.Clear();

            cboEtatCivPersonne.Items.Add("MARIE(E)");
            cboEtatCivPersonne.Items.Add("VEUF(VE)");
            cboEtatCivPersonne.Items.Add("CELIBATAIRE");
            cboEtatCivPersonne.SelectedIndex = 0;

            cboNiveauEtudePersonne.Items.Add("D4");
            cboNiveauEtudePersonne.Items.Add("D5");
            cboNiveauEtudePersonne.Items.Add("GRADUE");
            cboNiveauEtudePersonne.Items.Add("LICENCIE");
            cboNiveauEtudePersonne.Items.Add("DEA");
            cboNiveauEtudePersonne.Items.Add("DOCTORAT");
            cboNiveauEtudePersonne.SelectedIndex = 0;

            cboIdPersonne.DataSource = Factory1.Instance.GetPersonnes();
            cboNomMerePersonne.DataSource = Factory1.Instance.GetPersonnes();
            cboNomPerePersonne.DataSource = Factory1.Instance.GetPersonnes();
            cboIdAvVillage.DataSource = Factory1.Instance.GetAvenueVillages();

            if (cboNomPerePersonne.Items.Count > 0) cboNomPerePersonne.SelectedIndex = 0;
            else { }
            if (cboIdPersonne.Items.Count > 0) cboIdPersonne.SelectedIndex = 0;
            else { }
            if (cboNomMerePersonne.Items.Count > 0) cboNomMerePersonne.SelectedIndex = 0;
            else { }
            if (cboIdAvVillage.Items.Count > 0) cboIdAvVillage.SelectedIndex = 0;
            else { }
        }

        private void getAttributPersonne()
        {
            personne.Id = Convert.ToInt32(lblIdPersonne.Text);
            personne.Nom = txtNomPersonne.Text;
            personne.Postnom = txtPNomPersonne.Text;
            personne.Prenom = txtPrenomPersonne.Text;           
            personne.EtatCivile = cboEtatCivPersonne.Text;
            personne.Sexe = cboSexePersonne.Text;
            personne.Travail = chkTravail.Checked;
            personne.Numero = txtNumAvenuePersonne.Text;
            personne.NumeroNational = Factory1.Instance.generateNumIdNational(personne.Id_avenueVillage);
            personne.NombreEnfant = Convert.ToInt32(txtNbrEnfantPersonne.Text);
            personne.Niveau = cboNiveauEtudePersonne.Text;
            if (txtDateNaissancePersonne.Text.Equals("  /  /")) personne.Datenaissance = null;
            else personne.Datenaissance = Convert.ToDateTime(txtDateNaissancePersonne.Text);
            if (txtDateDecesPersonne.Text.Equals("  /  /")) personne.Datedeces = null;
            else personne.Datedeces = Convert.ToDateTime(txtDateDecesPersonne.Text);

            //Parametres pour la photo de la personne
            photoPers.Id_personne = Convert.ToInt32(lblIdPersonne.Text);
            photoPers.PhotoPersonne = pathPhotoLoad;
        }

        private void getAttributCarte()
        {
            carte.Id = Convert.ToInt32(lblIdCarte.Text);
            carte.Datelivraison = Convert.ToDateTime(txtDateLivraison.Text);
        }

        private void refreshPersonne()
        {
            dgvPersonne.DataSource = Factory1.Instance.GetPersonnes();

            if (dgvPersonne.RowCount > 0)
            {
                btnModifierPersonne.Enabled = true;
                //btnDeletePersonne.Enabled = true;
            }
        }

        private void refreshCarte()
        {
            dgvCarte.DataSource = Factory1.Instance.GetCartes();

            if (dgvCarte.RowCount > 0)
            {
                btnModifierCarte.Enabled = true;
                //btnDeleteCarte.Enabled = true;
            }
        }

        private void btnAddPersonne_Click(object sender, EventArgs e)
        {
            try
            {
                lblIdPersonne.Text = Convert.ToString(Factory1.Instance.ReNewIdValue(personne));
                photoPers.Id = Factory1.Instance.ReNewIdValue(photoPers);
                photoPers.Id_personne = Convert.ToInt32(lblIdPersonne.Text);
                txtNomPersonne.Clear();
                txtPNomPersonne.Clear();
                txtPrenomPersonne.Clear();
                cboNomPerePersonne.Text = "";
                cboNomMerePersonne.Text = "";
                txtNbrEnfantPersonne.Text = "0"; 
                txtNumAvenuePersonne.Clear();
                lblNumNationalPersonne.Text = "";
                chkTravail.Checked = false;
                pbPhotoPersonne.Image = null;
                txtDateNaissancePersonne.Text = DateTime.Today.ToShortDateString();
                txtDateDecesPersonne.Clear();
                cboSexePersonne.SelectedIndex = 0;
                cboEtatCivPersonne.SelectedIndex = 0;
                cboNiveauEtudePersonne.SelectedIndex = 0;
                txtNomPersonne.Focus();

                btnSavePersonne.Enabled = true;
            }
            catch (Exception) { btnSavePersonne.Enabled = false; }
        }

        private void btnClosePersonneCarte_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSaveCarte_Click(object sender, EventArgs e)
        {
            try
            {
                if (carte != null)
                {
                    getAttributCarte();

                    carte.Enregistrer();
                    btnSaveCarte.Enabled = false;
                    MessageBox.Show("Enregistrement éffectué", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        refreshCarte();
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

        private void btnRefreshPersonne_Click(object sender, EventArgs e)
        {
            try
            {
                refreshPersonne();
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvCarte_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvCarte.SelectedRows.Count > 0)
                {
                    btnModifierCarte.Enabled = true;

                    carte = (CarteServeur)dgvCarte.SelectedRows[0].DataBoundItem;

                    lblIdCarte.Text = carte.Id.ToString();
                    cboIdPersonne.Text = Factory1.Instance.GetPersonne(carte.Id_personne).ToString(); 
                    txtDateLivraison.Text = Convert.ToString(carte.Datelivraison);
                }
            }
            catch (Exception) { MessageBox.Show("Erreur dans la zone d'affichage", "Erreur d'affichage"); }
        }

        private void btnAddCarte_Click(object sender, EventArgs e)
        {
            try
            {
                lblIdCarte.Text = Convert.ToString(Factory1.Instance.ReNewIdValue(carte));
                carte.Id = Factory1.Instance.ReNewIdValue(carte);
                txtDateLivraison.Text= DateTime.Today.ToShortDateString();
                txtDateLivraison.Focus();

                btnSaveCarte.Enabled = true;
            }
            catch (Exception) { btnSaveCarte.Enabled = false; }
        }

        private void btnRefreshCarte_Click(object sender, EventArgs e)
        {
            try
            {
                refreshCarte();
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur lors de l'actualisation", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnModifierPersonne_Click(object sender, EventArgs e)
        {
            try
            {
                if (personne != null)
                {
                    getAttributPersonne();
                    personne.Modifier();

                    //On modifie la photo de la personne
                    photoPers.Modifier();
                    MessageBox.Show("Modification effectuée!", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pathPhotoLoad = null;
                }
                try
                {
                    chargementCombo();
                }
                catch (Exception) { }

                try
                {
                    refreshPersonne();
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

        private void lblLoadPhoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (dlgFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileInfo file = new FileInfo(dlgFile.FileName);
                    pbPhotoPersonne.Load(dlgFile.FileName);
                    //On verifie d'abord que l'extension est .jpg ou .png
                    if (Factory1.Instance.verifiePhotoExtension(dlgFile.FileName))
                    {
                        pathPhotoLoad = file.FullName;
                    }
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Impossible de charger le fichier", "Photo invalide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Impossible de charger le fichier, " + ex.Message, "Photo invalide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void PersonneCarte_Load(object sender, EventArgs e)
        {
            txtDateLivraison.Text = DateTime.Today.ToShortDateString();
            try
            {
                chargementCombo();
            }
            catch (Exception) { }

            //combo box pour le sexe du candidat
            cboSexePersonne.Items.Add("M");
            cboSexePersonne.Items.Add("F");
            cboSexePersonne.SelectedIndex = 0;

            cboNomMerePersonne.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboNomMerePersonne.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            cboNomPerePersonne.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboNomPerePersonne.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            cboIdAvVillage.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboIdAvVillage.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            cboIdPersonne.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboIdPersonne.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            btnSavePersonne.Enabled = false;
            btnSaveCarte.Enabled = false;
            btnDeletePersonne.Enabled = false;
            btnDeleteCarte.Enabled = false;
            
            try
            {
                dgvPersonne.DataSource = Factory1.Instance.GetPersonnes();
                dgvCarte.DataSource = Factory1.Instance.GetCartes();

                //On rend inisible certaines colonne des DataGridView qui recup les dataproperty
                //correspondant aux noms des accesseurs
                int col1 = 0, col2 = 0;
                foreach (DataGridViewColumn dgvc in dgvPersonne.Columns)
                {
                    if (dgvc.DataPropertyName == "Id") dgvc.Visible = false;
                    else if (dgvc.DataPropertyName == "Id_avenueVillage") dgvc.Visible = false;
                    else if (dgvc.DataPropertyName == "Id_pere") dgvc.Visible = false;
                    else if (dgvc.DataPropertyName == "Id_mere") dgvc.Visible = false;
                    else if (dgvc.DataPropertyName == "NewMotpass") dgvc.Visible = false;
                    else if (dgvc.DataPropertyName == "ConfNewMotpass") dgvc.Visible = false;
                    else if (dgvc.DataPropertyName == "ConfMotpass") dgvc.Visible = false;
                    dgvPersonne.AutoResizeColumn(col1);
                    col1++;
                }

                foreach (DataGridViewColumn dgvc in dgvCarte.Columns)
                {
                    if (dgvc.DataPropertyName == "Id") dgvc.Visible = false;
                    else if (dgvc.DataPropertyName == "Id_personne") dgvc.Visible = false;
                    dgvCarte.AutoResizeColumn(col2);
                    col2++;
                }

                //Chargement de la photo
                try
                {
                    pbPhotoPersonne.Image = Bitmap.FromStream(Factory1.Instance.GetPhotoPersonne(photoPers));
                }
                catch (Exception)
                {
                    pbPhotoPersonne.Image = null;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("erreur lors de l'affichage", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (dgvPersonne.RowCount <= 0)
            {
                btnModifierPersonne.Enabled = false;
                //btnDeletePersonne.Enabled = false;
            }

            if (dgvCarte.RowCount <= 0)
            {
                btnModifierCarte.Enabled = false;
                //btnDeleteCarte.Enabled = false;
            }
        }

        private void btnSavePersonne_Click(object sender, EventArgs e)
        {
            try
            {
                if (personne != null)
                {
                    getAttributPersonne();

                    personne.Enregistrer();

                    //Apres l'insertion de la personne, on insere sa photo
                    photoPers.Enregistrer();
                    
                    MessageBox.Show("Enregistrement éffectué", "Enregistrement", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pathPhotoLoad = null;
                }
                try
                {
                    chargementCombo();
                }
                catch (Exception) { }

                try
                {
                    refreshPersonne();
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

        private void dgvPersonne_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvPersonne.SelectedRows.Count > 0)
                {
                    btnModifierPersonne.Enabled = true;

                    personne = (PersonneServeur)dgvPersonne.SelectedRows[0].DataBoundItem;

                    lblIdPersonne.Text = personne.Id.ToString();
                    photoPers.Id = Factory1.Instance.GetIdPhoto(personne.Id,personne.NumeroNational);//Id de la photo
                    cboIdAvVillage.Text = Factory1.Instance.GetAvenueVillage(personne.Id_avenueVillage).ToString();
                    txtNomPersonne.Text = personne.Nom;
                    txtPNomPersonne.Text = personne.Postnom;
                    txtPrenomPersonne.Text = personne.Prenom;

                    if (personne.Id_pere.HasValue) cboNomPerePersonne.Text = Factory1.Instance.GetPersonne(Convert.ToInt32(personne.Id_pere)).ToString();//.Nom + " " + Factory1.Instance.GetPersonne(Convert.ToInt32(personne.Id_pere)).Postnom + " " + Factory1.Instance.GetPersonne(Convert.ToInt32(personne.Id_pere)).Prenom;
                    else cboNomPerePersonne.Text = "";
                    if (personne.Id_mere.HasValue) cboNomMerePersonne.Text = Factory1.Instance.GetPersonne(Convert.ToInt32(personne.Id_mere)).ToString();//.Nom + " " + Factory1.Instance.GetPersonne(Convert.ToInt32(personne.Id_mere)).Postnom + " " + Factory1.Instance.GetPersonne(Convert.ToInt32(personne.Id_mere)).Prenom;
                    else cboNomMerePersonne.Text = "";

                    cboSexePersonne.Text = personne.Sexe;
                    cboEtatCivPersonne.Text = personne.EtatCivile;
                    txtNumAvenuePersonne.Text = personne.Numero;
                    txtNbrEnfantPersonne.Text = personne.NombreEnfant.ToString();
                    lblNumNationalPersonne.Text = personne.NumeroNational;
                    cboNiveauEtudePersonne.Text = personne.Niveau;
                    if ((personne.Travail).ToString().Equals("True")) chkTravail.Checked = true;
                    else chkTravail.Checked = false;

                    if (!personne.Datenaissance.HasValue) txtDateNaissancePersonne.Text = "";
                    else txtDateNaissancePersonne.Text = Convert.ToString(personne.Datenaissance);
                    if (txtDateNaissancePersonne.Text.Equals("01/01/0001")) txtDateNaissancePersonne.Text = "";
                    if (!personne.Datedeces.HasValue) txtDateDecesPersonne.Text = "";
                    else txtDateDecesPersonne.Text = Convert.ToString(personne.Datedeces);
                    if (txtDateDecesPersonne.Text.Equals("01/01/0001")) txtDateDecesPersonne.Text = "";

                    //Chargement de la photo
                    try
                    {
                        pbPhotoPersonne.Image = Bitmap.FromStream(Factory1.Instance.GetPhotoPersonne(Factory1.Instance.GetPhoto(personne.Id)));
                    }
                    catch (Exception)
                    {
                        pbPhotoPersonne.Image = null;
                    }
                }
            }
            catch (Exception) { MessageBox.Show("Erreur dans la zone d'affichage", "Erreur d'affichage"); }
        }

        private void btnDeletePersonne_Click(object sender, EventArgs e)
        {
        }

        private void btnModifierCarte_Click(object sender, EventArgs e)
        {
            try
            {
                if (carte != null)
                {
                    getAttributCarte();
                    carte.Modifier();
                    MessageBox.Show("Modification effectuée!", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                try
                {
                    refreshCarte();
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

        private void btnDeleteCarte_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    DialogResult result = MessageBox.Show("Voulez - vous vraiment supprimer cet enregistrement ?", "Suppression", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (result == DialogResult.Yes)
            //    {
            //        carte.Id = Convert.ToInt32(lblIdCarte.Text);
            //        carte.Supprimer();
            //        MessageBox.Show("Suppression éffectuée", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        try
            //        {
            //            refreshCarte();
            //        }
            //        catch (Exception e1)
            //        {
            //            MessageBox.Show(e1.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        }

            //        if (dgvCarte.RowCount > 0)
            //        {
            //            btnModifierCarte.Enabled = true;
            //            btnDeleteCarte.Enabled = true;
            //        }

            //        initialiseCarte();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }

        private void btnSearchPersonne_Click(object sender, EventArgs e)
        {
            //string numNational = Interaction.InputBox("Veuillez saisir le numéro National de la personne à rechercher", "Recherche Personne");

            //try
            //{
            //    dgTempPersonne.DataSource = Factory1.Instance.SearchPersonne(numNational);
            //    personne = (Personne)dgTempPersonne.Rows[0].DataBoundItem;
            //    lblIdPersonne.Text = Convert.ToString(personne.Id);
            //    cboIdAvVillage.Text = Factory1.Instance.GetAvenueVillage(Convert.ToInt32(personne.Id_avenueVillage)).Designation; ;
            //    txtNomPersonne.Text = Convert.ToString(personne.Nom);
            //    txtPNomPersonne.Text = Convert.ToString(personne.Postnom);
            //    txtPrenomPersonne.Text = Convert.ToString(personne.Prenom);
            //    if (personne.Id_pere.HasValue) cboNomPerePersonne.Text = Factory1.Instance.GetPersonneServeur(Convert.ToInt32(personne.Id_pere)).Nom + " " + Factory1.Instance.GetPersonneServeur(Convert.ToInt32(personne.Id_pere)).Postnom + " " + Factory1.Instance.GetPersonneServeur(Convert.ToInt32(personne.Id_pere)).Prenom;
            //    else cboNomPerePersonne.Text = "";
            //    if (personne.Id_mere.HasValue) cboNomMerePersonne.Text = Factory1.Instance.GetPersonneServeur(Convert.ToInt32(personne.Id_mere)).Nom + " " + Factory1.Instance.GetPersonneServeur(Convert.ToInt32(personne.Id_mere)).Postnom + " " + Factory1.Instance.GetPersonneServeur(Convert.ToInt32(personne.Id_mere)).Prenom;
            //    else cboNomMerePersonne.Text = "";    
            //    cboSexePersonne.Text = Convert.ToString(personne.Sexe);
            //    cboEtatCivPersonne.Text = Convert.ToString(personne.EtatCivile);
            //    lblNumNationalPersonne.Text = Convert.ToString(personne.NumeroNational);
            //    txtNbrEnfantPersonne.Text = Convert.ToString(personne.NombreEnfant);
            //    cboNiveauEtudePersonne.Text = Convert.ToString(personne.Niveau);
            //    chkTravail.Checked = Convert.ToBoolean(personne.Travail.ToString().ToLower());
            //    txtDateNaissancePersonne.Text = Convert.ToString(personne.Datenaissance);
            //    txtDateDecesPersonne.Text = Convert.ToString(personne.Datedeces);

            //    //Pour la photo de la personne
            //    try
            //    {
            //        pbPhotoPersonne.Image = Bitmap.FromStream(Factory1.Instance.GetPhotoPersonne(Factory1.Instance.GetPhoto(Convert.ToInt32(lblIdPersonne.Text))));
            //    }
            //    catch (Exception)
            //    {
            //        pbPhotoPersonne.Image = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Recherche", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //}
        }

        private void cboIdAvVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                personne.Id_avenueVillage = Convert.ToInt32(((AvenueVillageServeur)cboIdAvVillage.SelectedItem).Id);
                //lblIdAvenueVillage.Text = Convert.ToString(((AvenueVillageServeur)cboIdAvVillage.SelectedItem).Id);
            }
            catch (Exception) { }
        }

        private void cboNomPerePersonne_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNomPerePersonne.Text.Equals("")) personne.Id_pere = null;
            else personne.Id_pere = Convert.ToInt32(((PersonneServeur)cboNomPerePersonne.SelectedItem).Id);    
        }

        private void cboNomMerePersonne_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNomMerePersonne.Text.Equals("")) personne.Id_mere = null;
            else personne.Id_mere = Convert.ToInt32(((PersonneServeur)cboNomMerePersonne.SelectedItem).Id);
        }

        private void cboIdPersonne_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                carte.Id_personne = Convert.ToInt32(((PersonneServeur)cboIdPersonne.SelectedItem).Id);

                personne.Id = Convert.ToInt32(((PersonneServeur)cboIdPersonne.SelectedItem).Id);
                cboIdAvVillage.Text = Factory1.Instance.GetAvenueVillage(Convert.ToInt32(((PersonneServeur)cboIdPersonne.SelectedItem).Id_avenueVillage)).Designation;
                lblIdPersonne.Text = Convert.ToString(((PersonneServeur)cboIdPersonne.SelectedItem).Id);
                txtNomPersonne.Text = Convert.ToString(((PersonneServeur)cboIdPersonne.SelectedItem).Nom);
                txtPNomPersonne.Text = Convert.ToString(((PersonneServeur)cboIdPersonne.SelectedItem).Postnom);
                txtPrenomPersonne.Text = Convert.ToString(((PersonneServeur)cboIdPersonne.SelectedItem).Prenom);

                if (((PersonneServeur)cboIdPersonne.SelectedItem).Id_pere.HasValue) cboNomPerePersonne.Text = Factory1.Instance.GetPersonne(Convert.ToInt32(((PersonneServeur)cboIdPersonne.SelectedItem).Id_pere)).Nom + " " + Factory1.Instance.GetPersonne(Convert.ToInt32(((PersonneServeur)cboIdPersonne.SelectedItem).Id_pere)).Postnom + " " + Factory1.Instance.GetPersonne(Convert.ToInt32(((PersonneServeur)cboIdPersonne.SelectedItem).Id_pere)).Prenom;
                else cboNomPerePersonne.Text = "";
                if (((PersonneServeur)cboIdPersonne.SelectedItem).Id_mere.HasValue) cboNomMerePersonne.Text = Factory1.Instance.GetPersonne(Convert.ToInt32(((PersonneServeur)cboIdPersonne.SelectedItem).Id_mere)).Nom + " " + Factory1.Instance.GetPersonne(Convert.ToInt32(((PersonneServeur)cboIdPersonne.SelectedItem).Id_mere)).Postnom + " " + Factory1.Instance.GetPersonne(Convert.ToInt32(((PersonneServeur)cboIdPersonne.SelectedItem).Id_mere)).Prenom;
                else cboNomMerePersonne.Text = "";

                cboSexePersonne.Text = Convert.ToString(((PersonneServeur)cboIdPersonne.SelectedItem).Sexe);
                cboEtatCivPersonne.Text = Convert.ToString(((PersonneServeur)cboIdPersonne.SelectedItem).EtatCivile);
                if (Convert.ToString(((PersonneServeur)cboIdPersonne.SelectedItem).Travail) == "True") chkTravail.Checked = true;

                else chkTravail.Checked = false;
                lblNumNationalPersonne.Text = Convert.ToString(((PersonneServeur)cboIdPersonne.SelectedItem).NumeroNational);
                txtNumAvenuePersonne.Text = Convert.ToString(((PersonneServeur)cboIdPersonne.SelectedItem).Numero);
                txtNbrEnfantPersonne.Text = Convert.ToString(((PersonneServeur)cboIdPersonne.SelectedItem).NombreEnfant);
                cboNiveauEtudePersonne.Text = Convert.ToString(((PersonneServeur)cboIdPersonne.SelectedItem).Niveau);

                if (!((PersonneServeur)cboIdPersonne.SelectedItem).Datenaissance.HasValue) txtDateNaissancePersonne.Text = "";
                else txtDateNaissancePersonne.Text = Convert.ToString(((PersonneServeur)cboIdPersonne.SelectedItem).Datenaissance);
                if (txtDateNaissancePersonne.Text.Equals("01/01/0001")) txtDateNaissancePersonne.Text = "";
                if (!((PersonneServeur)cboIdPersonne.SelectedItem).Datedeces.HasValue) txtDateDecesPersonne.Text = "";
                else txtDateDecesPersonne.Text = Convert.ToString(((PersonneServeur)cboIdPersonne.SelectedItem).Datedeces);
                if (txtDateDecesPersonne.Text.Equals("01/01/0001")) txtDateDecesPersonne.Text = "";

                //txtDateNaissancePersonne.Text = Convert.ToString(((PersonneServeur)cboIdPersonne.SelectedItem).Datenaissance);
                //txtDateDecesPersonne.Text = Convert.ToString(((PersonneServeur)cboIdPersonne.SelectedItem).Datedeces);
            }
            catch (Exception) { }

            //Chargement de la photo
            try
            {
                pbPhotoPersonne.Image = Bitmap.FromStream(Factory1.Instance.GetPhotoPersonne(Factory1.Instance.GetPhoto(personne.Id)));
            }
            catch (Exception)
            {
                pbPhotoPersonne.Image = null;
            }
        }
    }
}
