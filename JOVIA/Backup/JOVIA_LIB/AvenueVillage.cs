using System;
using System.Collections.Generic;

namespace JOVIA_LIB
{
    public class AvenueVillage
    {
        //declation of variable
        private int id;
        private int id_quartierLocalite;

        private string designation;
        private List<AvenueVillage> lstListAvenueVillage;

        public List<AvenueVillage> LstListAvenueVillage
        {
            get
            {
                if (lstListAvenueVillage == null) lstListAvenueVillage = new List<AvenueVillage>();
                return lstListAvenueVillage;
            }
        }

        //Transforme le contenu de la Liste en String visible
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

        public int Id_quartierLocalite
        {
            get { return id_quartierLocalite; }
            set { id_quartierLocalite = value; }
        }

        public string Designation
        {
            get { return designation; }
            set
            {
                if (value != null && value != "")
                {
                    designation = value.ToUpper();
                }
                else throw new Exception("la désignation de l'avenue ou village ne peut être vide"); 
            }
        }

        public void Enregistrer()
        {
            Factory.Instance.Save(this);
        }
        public void Supprimer()
        {
            Factory.Instance.Delete(this);
        }
        public void Modifier()
        {
            Factory.Instance.Update(this);
        }
    }
}
