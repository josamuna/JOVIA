using System;
using System.Data;
using System.Windows.Forms;
using JOVIA_RPT;
using MySql.Data.MySqlClient;
using Npgsql;
using JOVIA_LIB;

namespace JOVIA_WIN
{
    public partial class DensitePopulation : UserControl
    {
        public DensitePopulation()
        {
            InitializeComponent();
        }

        private void DensitePopulation_Load(object sender, EventArgs e)
        {
            try
            {
                //NpgsqlCommand cmd = null;
                ////rptDensitePopulation rpt = new rptDensitePopulation();
                //if (Factory.Instance.GetTypeSGBDConnecting() == 1)
                //{
                //    //PostgGreSql
                //    cmd = new NpgsqlCommand("SELECT fonctionCalculDensitePopulation('province',0)", (NpgsqlConnection)Factory.Instance.Con);
                //}
                //else if (Factory.Instance.GetTypeSGBDConnecting() == 2) { }//MySql

                //NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //da.Fill(ds, "doc");
                //rpt.SetDataSource(ds.Tables["doc"]);
                //crvDensitePopulation.ReportSource = rpt;
                //crvDensitePopulation.Refresh();
                //da.Dispose();
                //ds.Dispose();
                //cmd.Dispose();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Erreur de l'afichage du rapport", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
