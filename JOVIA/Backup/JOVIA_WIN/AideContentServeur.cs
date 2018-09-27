using System;
using System.Windows.Forms;

namespace JOVIA_WIN
{
    public partial class AideContentServeur : UserControl
    {
        public AideContentServeur()
        {
            InitializeComponent(); 
        }

        private void cmdCloseAide_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void AideContent_Load(object sender, EventArgs e)
        {
            //
        }
    }
}
