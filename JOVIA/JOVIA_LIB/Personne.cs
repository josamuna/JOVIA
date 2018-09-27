using System;
using System.Collections.Generic;

namespace JOVIA_LIB
{
    public class Personne
    {
        //decration of atribut
        private int id;
        private int id_avenueVillage;
        private string nom;
        private string postnom;
        private string prenom;
        private int? id_pere = null;
        private int? id_mere = null;
        private string etatcivile;
        private string sexe;
        private bool travail;
        private string numeroNational;   
        private string numero;    
        private int nombreEnfant;
        private string niveau;
        private int anneeSaved = DateTime.Now.Year;

        private DateTime? datenaissance = null;
        private DateTime? datedeces = null;
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
            return (this.Nom + " " + this.Postnom + " " + this.Prenom).ToUpper();
        }

        //accesseur and mutators
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int Id_avenueVillage
        {
            get { return id_avenueVillage; }
            set { id_avenueVillage = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = ValiderNom(value,false).ToUpper(); }
        }

        public string Postnom
        {
            get { return postnom; }
            set { postnom = ValiderNom(value, true).ToUpper(); }
        }

        public string Prenom
        {
            get { return prenom; }
            set { prenom = ValiderNom(value, true).ToUpper(); }
        }

        public int? Id_pere
        {
            get { return id_pere; }
            set { id_pere = value; }
        }

        public int? Id_mere
        {
            get { return id_mere; }
            set { id_mere = value; }
        }

        public string EtatCivile
        {
            get { return etatcivile; }
            set { etatcivile = value.ToUpper(); }
        }

        public string Sexe
        {
            get { return sexe; }
            set { sexe = value.ToUpper(); }
        }

        public Boolean Travail
        {
            get { return travail; }
            set { travail = value; }
        }
        public string ValiderNom(String value, bool autoriserNull)
        {
            if (!string.IsNullOrEmpty(value))
            {
                char c = char.Parse(value.Substring(0, 1));
                if (!char.IsLetter(c)) throw new Exception("Le premier caractère doit être une lettre");
            }
            else if (string.IsNullOrEmpty(value) && !autoriserNull)
            {
                throw new Exception("Le nom est obligatoire !");
            }

            return value.ToUpper();
        }
        public string NumeroNational
        {
            get { return numeroNational; }
            set { numeroNational = value.ToUpper(); }
        }
        public string Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        public int NombreEnfant
        {
            get { return nombreEnfant; }
            set 
            {
                if (value < 0)
                {
                    throw new Exception("Le nombre d'enfant ne peut être négatif !!");
                }else if (value > 100)
                {
                    throw new Exception("Le nombre d'enfant est eronné !!");
                }
                else nombreEnfant = value;                
            }
        }
        public DateTime? Datenaissance
        {
            get { return datenaissance; }
            set
            {
                if (value.HasValue)
                {
                    if (value.Equals("01/01/0001")) { }
                    if (value.Value.Year > 1 && value.Value.Year <= 1920)
                        throw new Exception("La date de naissance est invalide, l'année doit être supérieure à 1920 !");
                    else if (DateTime.Compare(value.Value, DateTime.Today) > 0)
                        throw new Exception("La date de naissance ne peut être supérieur à la date d'aujourd'hui");
                }
                datenaissance = value;
            }
        }
        public DateTime? Datedeces
        {
            get { return datedeces; }
            set
            {
                if (value.HasValue)
                {
                    if (value.Equals("01/01/0001")) { }
                    if (value.Value.Year > 1 && value.Value.Year <= 1920)
                        throw new Exception("La date de decès est invalide, l'année doit être supérieure à 1920 !");
                    else if (DateTime.Compare(value.Value, DateTime.Today) > 0)
                        throw new Exception("La date de déces ne peut être supérieur à la date d'aujourd'hui");
                }
                datedeces = value;
            }
        }
        public string Niveau
        {
            get { return niveau; }
            set { niveau = value.ToUpper(); }
        }

        public int AnneeSaved
        {
            get { return anneeSaved; }
            set { anneeSaved = value; }
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
        public void ModifierDeces()
        {
            Factory.Instance.UpdateDeces(this);
        }
    }
}
