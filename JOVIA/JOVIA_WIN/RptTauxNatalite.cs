using System;
using System.Windows.Forms;
using JOVIA_LIB;
using JOVIA_RPT;
using Npgsql;

namespace JOVIA_WIN
{
    public partial class RptTauxNatalite : UserControl
    {
        public RptTauxNatalite()
        {
            InitializeComponent();
        }
        int annee = 0;
        int id = 0;
        private void btClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (txtAnnee.Text != "")
            {
                annee = Convert.ToInt32(txtAnnee.Text);
                try
                {
                    rptTautNatalite rpt = new rptTautNatalite();
                    NpgsqlCommand cmd = new NpgsqlCommand(Factory.Instance.fonctionCalculTauxNatalite(cboTypeEntite.Text, annee,id), Factory.Instance.connect());
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DtsTauxNatalite ds = new DtsTauxNatalite();
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
            else
            {
                MessageBox.Show("vous devez specifier l'année dont vous voulez afficher le Taux de natalité", "Taux de natalité", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAnnee.Focus();
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

        private void RptTauxNatalite_Load(object sender, EventArgs e)
        {
            string[] type = new string[] { "Province", "Ville|Territoire", "Commune|Chefferie", "Quartier|Localité", "Avenue|Village" };
            cboTypeEntite.DataSource = type;
            cboTypeEntite.SelectedIndex = 0;

            cboEntite.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboEntite.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
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
    }
}
