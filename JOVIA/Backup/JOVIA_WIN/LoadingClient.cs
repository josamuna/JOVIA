using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JOVIA_WIN
{
    public partial class LoadingClient : Form
    {
        private int temps = 0;

        public LoadingClient()
        {
            InitializeComponent();
        }

        private void tmTemps_Tick(object sender, EventArgs e)
        {
            temps++;
            if (temps == 6)
            {
                //this.Close();
                this.Visible = false;
                ConnexionBd fp = new ConnexionBd();
                fp.Visible = true;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
