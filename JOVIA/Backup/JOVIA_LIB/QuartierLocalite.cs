using System;
using System.Collections.Generic;

namespace JOVIA_LIB
{
    public class QuartierLocalite
    {
        //declaration of attribut
        private int id;
        private int id_communeChefferieSecteur;
        private string designation;
        private int superficie = 0;
        private List<QuartierLocalite> lstQuartierLocalite;

        public List<QuartierLocalite> LstQuartierLocalite
        {
            get
            {
                if (lstQuartierLocalite == null) lstQuartierLocalite = new List<QuartierLocalite>();
                return lstQuartierLocalite;
            }
        }

        public override string ToString()
        {
            return this.Designation.ToUpper();
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
            Factory.Instance.Save(this);
        }

        public void Modifier()
        {
            Factory.Instance.Update(this);
        }

        public void Supprimer()
        {
            Factory.Instance.Delete(this);
        }    
    }
}
