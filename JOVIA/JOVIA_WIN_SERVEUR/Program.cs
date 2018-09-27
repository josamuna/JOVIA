using System;
using System.Windows.Forms;

namespace JOVIA_WIN_SERVEUR
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new ConnexionBdServeur());
            }
            catch (System.Runtime.InteropServices.ExternalException)
            {
                MessageBox.Show("Impossible de charger le fichier, l'application va se fermer", "Photo invalide", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }
        }
    }
}
