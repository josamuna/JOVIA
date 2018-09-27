using System;
using System.Windows.Forms;
using Npgsql;
using JOVIA_RPT;
using JOVIA_LIB;

namespace JOVIA_WIN
{
    public partial class RptDensite : UserControl
    {
        public RptDensite()
        {
            InitializeComponent();
        }
        private int id=0;

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            //Console.WriteLine(Factory.Instance.CalculDensitePopulation(cboTypeEntite.Text));
            try
            {
                rptDensite rpt = new rptDensite();
                NpgsqlCommand cmd = new NpgsqlCommand(Factory.Instance.CalculDensitePopulation(cboTypeEntite.Text,id), Factory.Instance.connect());
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
                cboEntite.DataSource = Factory.Instance.GetParametreProvinces();
            }
            else if (cboTypeEntite.Text == "Commune|Chefferie")
            {
                cboEntite.DataSource = Factory.Instance.GetVilleTeritoires();
            }
            else if (cboTypeEntite.Text == "Quartier|Localité")
            {
                cboEntite.DataSource = Factory.Instance.GetCommuneChefferieSecteurs();
            }
            else if (cboTypeEntite.Text == "Avenue|Village")
            {
                cboEntite.DataSource = Factory.Instance.GetQuartierLocalites();
            }
        }

        private void cboEntinte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTypeEntite.Text == "Province")
            {

            }
            else if (cboTypeEntite.Text == "Ville|Territoire")
            {
                id = ((ParametreProvince )cboEntite.SelectedItem).Id;
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

        private void RptDensite_Load(object sender, EventArgs e)
        {
            string[] type = new string[] { "Province", "Ville|Territoire", "Commune|Chefferie", "Quartier|Localité", "Avenue|Village" };
            cboTypeEntite.DataSource = type;
            cboTypeEntite.SelectedIndex = 0;

            cboEntite.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboEntite.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }
    }
}
