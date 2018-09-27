using System;
using System.Collections.Generic;

namespace JOVIA_LIB
{
    public class RecherchePersonne
    {
        //decration of atribut
        private int idRech;
        private string nomRech;
        private string postnomRech;
        private string prenomRech;
        private string numeroNationalRech;
        private string nomPereRech;
        private string nomMereRech;
        private List<Personne> lstPersonne;

        public List<Personne> LstPersonne
        {
            get
            {
                if (lstPersonne == null) lstPersonne = new List<Personne>();
                return lstPersonne;
            }
        }

        public override string ToString()
        {
            return (this.NomRech + " " + this.PostnomRech + " " + this.PrenomRech).ToUpper();
        }

        //accesseur and mutators
        public int IdRech
        {
            get { return idRech; }
            set { idRech = value; }
        }

        public string NomRech
        {
            get { return nomRech; }
            set { nomRech = ValiderNom(value).ToUpper(); }
        }

        public string PostnomRech
        {
            get { return postnomRech; }
            set { postnomRech = ValiderNom(value).ToUpper(); }
        }

        public string PrenomRech
        {
            get { return prenomRech; }
            set { prenomRech = ValiderNom(value).ToUpper(); }
        }

        public string NomPereRech
        {
            get { return nomPereRech; }
            set { nomPereRech = value.ToUpper(); }
        }

        public string NomMereRech
        {
            get { return nomMereRech; }
            set { nomMereRech = value.ToUpper(); }
        }

        public string ValiderNom(String value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                char c = char.Parse(value.Substring(0, 1));
                if (!char.IsLetter(c)) throw new Exception("Le premier caractère doit être une lettre");
            }
            return value.ToUpper();
        }
        public string NumeroNational
        {
            get { return numeroNationalRech; }
            set { numeroNationalRech = value.ToUpper(); }
        }
    }
}
