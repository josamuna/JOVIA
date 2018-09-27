using System;
using System.Collections.Generic;

namespace JOVIA_LIB_SERVEUR
{
    public class AvenueVillageServeur
    {
        //declation of variable
        private int id;
        private int id_quartierLocalite;

        private string designation;
        private List<AvenueVillageServeur> lstListAvenueVillage;

        public List<AvenueVillageServeur> LstListAvenueVillage
        {
            get
            {
                if (lstListAvenueVillage == null) lstListAvenueVillage = new List<AvenueVillageServeur>();
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
            Factory1.Instance.Save(this);
        }
        public void Supprimer()
        {
            Factory1.Instance.Delete(this);
        }
        public void Modifier()
        {
            Factory1.Instance.Update(this);
        }
    }
}
