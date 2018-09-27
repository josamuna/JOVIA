using System;

namespace JOVIA_LIB
{
    public class EnvoieMsgAgent
    {
        private string numerotelephone;
        private string message_envoye;
        private DateTime dateenvoie = DateTime.Today;

        public string NumeroTelephone
        {
            get { return numerotelephone; }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new Exception("Le numéro de téléphone est invalide");
                else numerotelephone = value;
            }
        }

        public string Message_envoye
        {
            get { return message_envoye; }
            set { message_envoye = value; }
        }

        public DateTime Dateenvoie
        {
            get { return dateenvoie; }
            set { dateenvoie = value; }
        }
    }
}
