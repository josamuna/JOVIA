using System;

namespace JOVIA_LIB
{
    public class Telephone
    {
        //general declations
        private int id;
        private string numero;
        private int id_utilisateur;

        //accesseur and mutators
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Numero
        {
            get { return numero; }
            set
            {
                if (value != null && value != "")
                {
                    numero = value;
                }
                else throw new Exception("Le numéro de téléphone de l'utilisateur ne peut être vide");
            }
        }

        public int Id_utilisateur
        {
            get { return id_utilisateur; }
            set { id_utilisateur = value; }
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
