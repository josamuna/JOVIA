using System;
using System.Windows.Forms;
using Npgsql;
using JOVIA_RPT;
using JOVIA_LIB;

namespace JOVIA_WIN
{
    public partial class RptTautMortalite : UserControl
    {
        public RptTautMortalite()
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
                Console.WriteLine(Factory.Instance.fonctionCalculTauxMortalite(cboTypeEntite.Text, annee, id));
                annee = Convert.ToInt32(txtAnnee.Text);
                try
                {
                    rptTauxMortalite rpt = new rptTauxMortalite();
                    NpgsqlCommand cmd = new NpgsqlCommand(Factory.Instance.fonctionCalculTauxMortalite(cboTypeEntite.Text, annee, id), Factory.Instance.connect());
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                    DtsTauxMortalite ds = new DtsTauxMortalite();
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

        private void RptTautMortalite_Load(object sender, EventArgs e)
        {
            string[] type = new string[] { "Province", "Ville|Territoire", "Commune|Chefferie", "Quartier|Localité", "Avenue|Village" };
            cboTypeEntite.DataSource = type;
            cboTypeEntite.SelectedIndex = 0;

            cboEntite.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboEntite.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }
    }
}
