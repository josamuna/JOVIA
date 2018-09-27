using System;
using System.Windows.Forms;

namespace JOVIA_WIN
{
    public partial class AideContent : UserControl
    {
        public AideContent()
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

        //Appel de l'ecouteur de reception du SMS 
        //private void comm_MessageReceived(object sender, GsmComm.GsmCommunication.MessageReceivedEventArgs e)
        //{
        //    try
        //    {
        //        //On verifie le login de l'utilisateur
        //        Factory.Instance.AuthentificateUserToSensSMS(SMS.Instance.ReceiveSMS());

        //        //On recupere le SMS et on l'insere dans la base des donnees
        //        Factory.Instance.DoData(SMS.Instance.ReceiveSMS(),Convert.ToInt32(Factory.Instance.ReadFileParametersUser()[2]));
        //    }
        //    catch (Exception) { }
        //}
    }
}
