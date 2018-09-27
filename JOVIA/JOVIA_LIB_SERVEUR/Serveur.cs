using System;
using System.Collections.Generic;
using System.Net;

namespace JOVIA_LIB_SERVEUR
{
    public class Serveur
    {
        private int id;
        private int id_province;
        private string designation;
        private string adresse_ip;

        private List<Serveur> lstServeur;

        public List<Serveur> LstServeur
        {
            get
            {
                if (lstServeur == null) lstServeur = new List<Serveur>();
                return lstServeur;
            }
        }

        public override string ToString()
        {
            return this.Designation.ToUpper() + " => " + this.Adresse_ip;
        }

        //accesseurs et mutateurs
        public int Id
        {
            get { return id; }
            set { id = value; }

        }

        public int Id_province
        {
            get { return id_province; }
            set { id_province = value; }
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
                else throw new Exception("La désignation ne peut être vide");
            }
        }

        public string Adresse_ip
        {
            get { return adresse_ip; }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new Exception("L'adresse IP du serveur ne peut être vide");
                else if (isValideIPAdresse(value)) adresse_ip = value;
                else throw new Exception("L'adresse IP du serveur est invalide"); 
            }
        }

        /// <summary>
        /// Vérification et validation de l'adresse IP du serveur
        /// </summary>
        /// <param name="strIPAdresse">Qdresse IP en string</param>
        /// <returns>Booléen</returns>
        private bool isValideIPAdresse(string strIPAdresse)
        {
            IPAddress adresse = null;
            return IPAddress.TryParse(strIPAdresse, out adresse);
        }

        //mehodes
        public void Enregistrer()
        {
            Factory1.Instance.Save(this);
        }
        public void Supprimmer()
        {
            Factory1.Instance.Delete(this);
        }
        public void Modifier()
        {
            Factory1.Instance.Update(this);
        }
    }
}
