using System;

namespace JOVIA_LIB
{
    public class EnvoieSMS
    {
        private int id;
        private string destinataire;
        private string message_envoye;
        private DateTime dateenvoie = DateTime.Today;

        public EnvoieSMS()
        {
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Destinataire
        {
            get { return destinataire; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Le(s) numéro(s) du(des) destinataire(s) n'est(ne sont) pas valide(s)");
                else destinataire = value;
            }
        }

        public string MessageEnvoye
        {
            get { return message_envoye; }
            set { message_envoye = value; }
        }

        public DateTime DateEnvoie
        {
            get { return dateenvoie; }
        }
    }
}
