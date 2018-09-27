using System;
using System.Windows.Forms;
using Npgsql;
using JOVIA_RPT_SERVEUR;
using JOVIA_LIB_SERVEUR;

namespace JOVIA_WIN_SERVEUR
{
    public partial class RptDensiteServeur : UserControl
    {
        public RptDensiteServeur()
        {
            InitializeComponent();
        }
        private int id=0;

        private void cmdOk_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(Factory1.Instance.CalculDensitePopulation(cboTypeEntite.Text));
            try
            {
                rptDensite rpt = new rptDensite();
                NpgsqlCommand cmd = new NpgsqlCommand(Factory1.Instance.CalculDensitePopulation(cboTypeEntite.Text,id), Factory1.Instance.connect());
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                DtsDensite ds = new DtsDensite();
                da.Fill(ds, "doc");
                rpt.SetDataSource(ds.Tables["doc"]);
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
                da.Dispose();
                ds.Dispose();
                cmd.Dispose();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Erreur de l'afichage du rapport", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboTypeEntite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTypeEntite.Text == "Province")
            {
                
            }
            else if (cboTypeEntite.Text == "Ville|Territoire")
            {
                cboEntite.DataSource = Factory1.Instance.GetProvinces();
            }
            else if (cboTypeEntite.Text == "Commune|Chefferie")
            {
                cboEntite.DataSource = Factory1.Instance.GetVilleTeritoires();
            }
            else if (cboTypeEntite.Text == "Quartier|Localité")
            {
                cboEntite.DataSource = Factory1.Instance.GetCommuneChefferieSecteurs();
            }
            else if (cboTypeEntite.Text == "Avenue|Village")
            {
                cboEntite.DataSource = Factory1.Instance.GetQuartierLocalites();
            }
        }

        private void cboEntinte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTypeEntite.Text == "Province")
            {

            }
            else if (cboTypeEntite.Text == "Ville|Territoire")
            {
                id = ((Province )cboEntite.SelectedItem).Id;
            }
            else if (cboTypeEntite.Text == "Commune|Chefferie")
            {
                id = ((VilleTeritoireServeur)cboEntite.SelectedItem).Id;
            }
            else if (cboTypeEntite.Text == "Quartier|Localité")
            {
                id = ((CommuneChefferieSecteurServeur)cboEntite.SelectedItem).Id;
            }
            else if (cboTypeEntite.Text == "Avenue|Village")
            {
                id = ((QuartierLocaliteServeur)cboEntite.SelectedItem).Id;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void RptDensiteServeur_Load(object sender, EventArgs e)
        {
            string[] type = new string[] { "Province", "Ville|Territoire", "Commune|Chefferie", "Quartier|Localité", "Avenue|Village" };
            cboTypeEntite.DataSource = type;
            cboTypeEntite.SelectedIndex = 0;

            cboEntite.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboEntite.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }
    }
}
