using System;
using System.Collections.Generic;

namespace JOVIA_LIB_SERVEUR
{
    public class QuartierLocaliteServeur
    {
        //declaration of attribut
        private int id;
        private int id_communeChefferieSecteur;
        private string designation;
        private int superficie = 0;
        private List<QuartierLocaliteServeur> lstQuartierLocalite;

        public List<QuartierLocaliteServeur> LstQuartierLocalite
        {
            get
            {
                if (lstQuartierLocalite == null) lstQuartierLocalite = new List<QuartierLocaliteServeur>();
                return lstQuartierLocalite;
            }
        }

        public override string ToString()
        {
            return this.Designation;
        }

        //accesseurs and mutators
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Id_communeChefferieSecteur
        {
            get { return id_communeChefferieSecteur; }
            set { id_communeChefferieSecteur = value; }
        }

        public string Designation
        {
            get { return designation; }
            set { designation = value.ToUpper(); }
        }

        public int Superficie
        {
            get { return superficie; }
            set { superficie = value; }
        }
        public void Enregistrer()
        {
            Factory1.Instance.Save(this);
        }

        public void Modifier()
        {
            Factory1.Instance.Update(this);
        }

        public void Supprimer()
        {
            Factory1.Instance.Delete(this);
        }    
    }
}
