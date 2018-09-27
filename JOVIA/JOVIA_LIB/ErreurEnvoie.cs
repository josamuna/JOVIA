using System;

namespace JOVIA_LIB
{
    public class ErreurEnvoie
    {
        //general declaration
        private int id;
        private string expediteur;
        private string message;
        private DateTime date_envoie;
        private string erreur;

        //mutateurs et accesseurs
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Expediteur
        {
            get { return expediteur; }
            set { expediteur = value; }
        }
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        public DateTime Date_envoie
        {
            get { return date_envoie; }
            set { date_envoie = value; }
        }
        public string Erreur
        {
            get { return erreur; }
            set { erreur = value; }
        }


        //methode
        public void Enregistrer()
        {
            Factory.Instance.Save(this);
        }
    }
}
