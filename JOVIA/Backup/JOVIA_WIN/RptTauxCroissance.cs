using System;
using System.Windows.Forms;
using JOVIA_LIB;
using JOVIA_RPT;
using Npgsql;

namespace JOVIA_WIN
{
    public partial class RptTauxCroissance : UserControl
    {
        object obj = null;
        ParametreProvince province = new ParametreProvince();
        VilleTeritoire villeTerritoire = new VilleTeritoire();
        CommuneChefferieSecteur communeChefferieSecteur = new CommuneChefferieSecteur();
        QuartierLocalite quartierLocalite = new QuartierLocalite();
        AvenueVillage avenueVillage = new AvenueVillage();
        private int id = 0;

        public RptTauxCroissance()
        {
            InitializeComponent();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void RptTauxCroissance_Load(object sender, EventArgs e)
        {
            string[] type = new string[] { "Province", "Ville|Territoire", "Commune|Chefferie", "Quartier|Localité", "Avenue|Village" };
            cboTypeEntite.DataSource = type;
            cboTypeEntite.SelectedIndex = 0;

            cboEntite.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboEntite.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        private void cboTypeEntite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTypeEntite.Text == "Province")
            {
                obj = province;
            }
            else if (cboTypeEntite.Text == "Ville|Territoire")
            {
                cboEntite.DataSource = Factory.Instance.GetParametreProvinces();
                obj = villeTerritoire;
            }
            else if (cboTypeEntite.Text == "Commune|Chefferie")
            {
                cboEntite.DataSource = Factory.Instance.GetVilleTeritoires();
                obj = communeChefferieSecteur;
            }
            else if (cboTypeEntite.Text == "Quartier|Localité")
            {
                cboEntite.DataSource = Factory.Instance.GetCommuneChefferieSecteurs();
                obj = quartierLocalite;
            }
            else if (cboTypeEntite.Text == "Avenue|Village")
            {
                cboEntite.DataSource = Factory.Instance.GetQuartierLocalites();
                obj = avenueVillage;
            }
        }

        private void cboEntinte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTypeEntite.Text == "Province")
            {
                
            }
            else if (cboTypeEntite.Text == "Ville|Territoire")
            {
                id = ((ParametreProvince)cboEntite.SelectedItem).Id;               
            }
            else if (cboTypeEntite.Text == "Commune|Chefferie")
            {
                id = ((VilleTeritoire)cboEntite.SelectedItem).Id;
            }
            else if (cboTypeEntite.Text == "Quartier|Localité")
            {
                id = ((CommuneChefferieSecteur)cboEntite.SelectedItem).Id;
            }
            else if (cboTypeEntite.Text == "Avenue|Village")
            {
                id = ((QuartierLocalite)cboEntite.SelectedItem).Id;
            }
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (txtAnneeDebut.Text != "" && txtAnneeFin.Text != "")
            {
                try
                {
                    rptTauxCroissance rpt = new rptTauxCroissance();
                    NpgsqlCommand cmd = new NpgsqlCommand(Factory.Instance.fonctionCalculTauxCroissance(obj,Convert.ToInt32(txtAnneeDebut.Text),Convert.ToInt32(txtAnneeFin.Text)), Factory.Instance.connect());
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DtsTauxCroissance ds = new DtsTauxCroissance();
                    da.Fill(ds, "doc");
                    rpt.SetDataSource(ds.Tables["doc"]);
                    crvTauxCroissance.ReportSource = rpt;
                    crvTauxCroissance.Refresh();
                    da.Dispose();
                    ds.Dispose();
                    cmd.Dispose();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message, "Erreur de l'afichage du rapport", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("vous devez specifier l'année de début et celle de fin pout afficher le Taux de croissance", "Taux de croissance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (string.IsNullOrEmpty(txtAnneeDebut.Text)) txtAnneeDebut.Focus();
                else if (string.IsNullOrEmpty(txtAnneeFin.Text)) txtAnneeFin.Focus();
            }
        }
    }
}
