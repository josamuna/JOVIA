using System;
using System.Windows.Forms;
using JOVIA_LIB;
using JOVIA_RPT;
using Npgsql;

namespace JOVIA_WIN
{
    public partial class RptCartePersonne : UserControl
    {
        public RptCartePersonne()
        {
            InitializeComponent();
        }
        Personne p = new Personne();
        private void btClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void RptCartePersonne_Load(object sender, EventArgs e)
        {
            cboPersonne.DataSource = Factory.Instance.GetPersonnes();

            cboPersonne.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboPersonne.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            if (cboPersonne.Items.Count > 0) cboPersonne.SelectedIndex = 0;
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            try
            {
                rptCarte rpt = new rptCarte();
                NpgsqlCommand cmd = new NpgsqlCommand(@"select numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance date,avenueVillage.designation as avenuevillage,quartierLocalite.designation as quartierLocalite,
	                                                communeChefferieSecteur.designation as communeChefferieSecteur,villeTerritoire.designation as villeTerritoire,parametreProvince.designation as province,photo.photo as photo
	                                                FROM parametreProvince,personne
	                                                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
	                                                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
	                                                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
	                                                INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
	                                                INNER JOIN photo ON photo.id_personne=personne.id
	                                                WHERE personne.id="+p.Id+"", Factory.Instance.connect());
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DtsTauxMortalite ds = new DtsTauxMortalite();
                da.Fill(ds, "doc");
                rpt.SetDataSource(ds.Tables["doc"]);
                crvCarte.ReportSource = rpt;
                crvCarte.Refresh();
                da.Dispose();
                ds.Dispose();
                cmd.Dispose();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Erreur de l'afichage du rapport", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboPersonne_SelectedIndexChanged(object sender, EventArgs e)
        {
            p.Id = ((Personne)cboPersonne.SelectedItem).Id;
        }
    }
}
