using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using MySql.Data.MySqlClient;
using Npgsql;

namespace JOVIA_LIB_SERVEUR
{
    public class Factory1
    {
        private static Factory1 staticFact;
        public IDbConnection con;
        public NpgsqlConnection conn;
        private static string stringConnect = "";
        private static string fileNamePostGres = "parametresPostGres.par";
        private static string fileNameMySQL = "fileNameMySQL.par";
        private static string fileNameSQLServer = "parametresSQLServer.par";
        //private static string fileNameAccess = "parametresAccess.par";

        private static string fileNamePostGresDistante = "parametresPostGres2.par";
        private static string fileNameMySQLDistante = "fileNameMySQL2.par";
        private static string fileNameSQLServerDistante = "parametresSQLServer2.par";
        //private static string fileNameAccessDistante = "parametresAccess2.par";

        private Factory1()
        {
        }

        #region INITIALISATION DE LA FACTORY POUR LE COTE SERVEUR CENTRALE
        public static Factory1 Instance
        {
            get
            {
                if (staticFact == null) staticFact = new Factory1();
                return staticFact; 
            }
        }
        #endregion

        #region VERIFICATION DE L'EXTENSION DU FICHIER (JPG,JPEG OU PNG)
        public bool verifiePhotoExtension(string fileName)
        {
            bool ok = false;
            string strReverse = "";
            for (int i = 0, j = fileName.Length - 1; i < fileName.Length; i++, j--) strReverse = strReverse + fileName[j];

            if (strReverse[0].ToString().ToLower() == 'g'.ToString() && strReverse[1].ToString().ToLower() == 'p'.ToString()
            && strReverse[2].ToString().ToLower() == 'j'.ToString() && strReverse[3].ToString() == '.'.ToString())
                //Fifhier jpg
                ok = true;
            else if (strReverse[0].ToString() == 'g'.ToString() && strReverse[1].ToString() == 'e'.ToString()
            && strReverse[2].ToString() == 'p'.ToString() && strReverse[3].ToString() == 'j'.ToString()
            && strReverse[4].ToString() == '.'.ToString())
                //Fifhier jpg
                ok = true;
            else if (strReverse[0].ToString().ToLower() == 'g'.ToString() && strReverse[1].ToString().ToLower() == 'n'.ToString()
            && strReverse[2].ToString().ToLower() == 'p'.ToString() && strReverse[3].ToString() == '.'.ToString())
                //Fifhier jpg
                ok = true;
            else throw new Exception("le format de la photo n'est pas valide, veuillez charger une photo au format jpg ou png svp !");
            return ok;
        }

        #endregion

        #region Ouverture de la connection \\ Ici pour les reports
        /// <summary>
        /// Permet l'ouverture de la connection à la base de données pour afficher le report
        /// et retourne un objet représentant la connexion à la dite base de données
        /// </summary>
        /// <returns>Objet IDbConnection</returns>
        public NpgsqlConnection connect()
        {
            conn = new NpgsqlConnection(stringConnect);
            conn.Open();
            if (conn.State.ToString().Equals("")) throw new Exception("Impossible de charger les données de la Base de données !!");
            else if (conn.State.ToString().Equals("Open")) { }
            else conn.Open();
            return conn;
        }
        #endregion

        #region Fermeture de la connexion à la base de données une fois déconnecté
        /// <summary>
        /// Permet la fermeture de la connexion à la base de données
        /// </summary>
        public void closeConnection()
        {
            con = null;
        }
        #endregion

        #region Verification et exécution de la connexion à la BD pour une base PostGres,MySQL,SQLServer ou Access(2003,2007 ou 2010)
        /// <summary>
        /// Permet de vérifier les valeurs de connexion à la base de données avant d'effectuer la connexion proprement dite
        /// le paramètre valueDB a pour valeur 0=>Pour PosyGresSQL, 1=>Pour MySQL, 2=> Pour SQLServer et 3=> Pour Accesss (2003, 2007 ou 2010)
        /// </summary>
        /// <param name="port">Numero de port avec comme numero de port local 5432 pour PostGresSQL</param>
        /// <param name="serveur">Nom du serveur et serveur par defaut localhost pour PostGresSQL et root pour MySql</param>
        /// <param name="database">Nom de la BD ou son chemin d'acces pour une base Access</param>
        /// <param name="userName">Nom d'utilisateur et utilisateur par defaut postgres pour PostGresSQ, sa pour SQLServer et root pour MySQLL</param>
        /// <param name="password">Mot de passe</param>
        /// <param name="stringConnect">Chaine de connexion</param>
        /// <param name="valueDB">entier representant la BD a choisir</param>
        /// <returns>un booleen</returns>
        public bool VerifieDoConnect(int? port, string serveur, string database, string userName, string password, int valueDB)
        {
            bool ok = false;
            if (valueDB == 0 || valueDB == 1 || valueDB == 2)
            {
                if (string.IsNullOrEmpty(serveur))
                    throw new Exception("Ce serveur n'existe pas !!");
                else if (string.IsNullOrEmpty(database))
                    throw new Exception("Cette base de données n'existe pas !!");
                else
                {
                    if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                    {
                        ok = false;
                        throw new Exception("Les informations d'authentification sont invalides !!");
                    }
                    else
                    {
                        if (valueDB == 0)
                        {
                            //PostGresSQL
                            if (port == 0 || string.IsNullOrEmpty(Convert.ToString(port)))
                            {
                                ok = false;
                                throw new Exception("Le numéro de port est invalide !!");
                            }
                            else
                            {
                                stringConnect = "Server=" + serveur + ";Port=" + port + ";UID=" + userName + ";Password=" + password + ";Database=" + database;//PosgresSQL
                                con = new NpgsqlConnection(stringConnect);
                                con.Open();
                                saveParamConnection(serveur, database, userName, Convert.ToString(port), valueDB);
                                ok = true;
                            }
                        }
                        else if (valueDB == 1)
                        {
                            //MySQL
                            stringConnect = "Server=" + serveur + ";Database=" + database + ";Uid=" + userName + ";Pwd=" + password;
                            con = new MySqlConnection(stringConnect);
                            con.Open();
                            saveParamConnection(serveur, database, userName, null, valueDB);

                            ok = true;
                        }
                        else if (valueDB == 2)
                        {
                            //SQLServer
                            stringConnect = "Data Source=" + serveur + ";Initial Catalog=" + database + ";User ID=" + userName + ";Password=" + password + ";Integrated Security=" + false;//SQLServer
                            //stringConnect = "Server=" + serveur + ";Initial Catalog=" + database + ";User ID=" + userName + ";Password=" + password + ";Persist Security Info=" + securite;
                            con = new SqlConnection(stringConnect);
                            con.Open();
                            saveParamConnection(serveur, database, userName, null, valueDB);

                            ok = true;
                        }
                    }
                }
            }
            else
            {
                //Access 2003, 2007 ou 2010
                if (string.IsNullOrEmpty(database))
                    throw new Exception("Cette base de données n'existe pas !!");
                else
                {//.accdb=bdcca. et .mdb=bdm.
                    string strReverse = "";
                    for (int i = 0, j = database.Length - 1; i < database.Length; i++, j--) strReverse = strReverse + database[j];

                    if (strReverse[0].ToString() == 'b'.ToString() && strReverse[1].ToString() == 'd'.ToString()
                        && strReverse[2].ToString() == 'c'.ToString() && strReverse[3].ToString() == 'c'.ToString()
                        && strReverse[4].ToString() == 'a'.ToString() && strReverse[5].ToString() == '.'.ToString())
                        //Fichier Microsoft Access 2007 ou 2010
                        stringConnect = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + database;//Acsess
                    else if (strReverse[0].ToString() == 'b'.ToString() && strReverse[1].ToString() == 'd'.ToString()
                        && strReverse[2].ToString() == 'm'.ToString() && strReverse[3].ToString() == '.'.ToString())
                        //Fichier Microsoft Access 2003
                        stringConnect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database;//Acsess
                    else throw new Exception("Fichier de base de donnée Microsoft Access inconnu !!");

                    con = new OleDbConnection(stringConnect);
                    con.Open();
                    saveParamConnection(null, database, null, null, valueDB);

                    ok = true;
                }
            }

            return ok;
        }

        /// <summary>
        ///Permet d'enregistrer la chaîne de connexion pour une base des donnée PostGresSQL dans un fichier texte 
        /// </summary>
        private static void saveParamConnection(string serveur, string bd, string userName, string port, int valueBD)
        {
            if (valueBD == 0)
            {
                //PostGresSQL
                using (StreamWriter srw = new StreamWriter(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNamePostGres, false))
                {
                    srw.WriteLine("{0};{1};{2};{3}", serveur, bd, userName, port);
                    srw.Close();
                }
            }
            else if (valueBD == 1)
            {
                //MySQL
                using (StreamWriter srw = new StreamWriter(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameMySQL, false))
                {
                    srw.WriteLine("{0};{1};{2}", serveur, bd, userName);
                    srw.Close();
                }
            }
            else if (valueBD == 2)
            {
                using (StreamWriter srw = new StreamWriter(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameSQLServer, false))
                {
                    srw.WriteLine("{0};{1};{2}", serveur, bd, userName);
                    srw.Close();
                }
            }
            //else if (valueBD == 3)
            //{
            //    using (StreamWriter srw = new StreamWriter(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameAccess, false))
            //    {
            //        srw.WriteLine("{0}", bd);
            //        srw.Close();
            //    }
            //}
        }

        /// <summary>
        /// Permet de charger la chaîne de connection à partir d'un fichier texte pour une Base PostGresSql et retourne un tableau
        /// contenant ces différentes valeurs (Serveur, Base de données, Nom d'utilisateur et numero de port)
        /// </summary>
        /// <returns>retourne un tableau</returns>
        public string[] loadParam(int valueDB)
        {
            string[] values = { };
            if (valueDB == 0)
            {
                //PostGresSQL
                values = new string[4];
                if (File.Exists(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNamePostGres))
                {
                    using (StreamReader sr = new StreamReader(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNamePostGres))
                    {
                        if (!sr.EndOfStream)
                        {
                            string str = sr.ReadLine();
                            string[] value = str.Split(new char[] { ';' });
                            values[0] = value[0];
                            values[1] = value[1];
                            values[2] = value[2];
                            values[3] = value[3];
                            sr.Close();
                        }
                    }
                }
            }
            else if (valueDB == 1)
            {
                //MySQL
                values = new string[3];
                if (File.Exists(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameMySQL))
                {
                    using (StreamReader sr = new StreamReader(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameMySQL))
                    {
                        if (!sr.EndOfStream)
                        {
                            string str = sr.ReadLine();
                            string[] value = str.Split(new char[] { ';' });
                            values[0] = value[0];
                            values[1] = value[1];
                            values[2] = value[2];
                            sr.Close();
                        }
                    }
                }
            }
            else if (valueDB == 2)
            {
                //SQLServer
                values = new string[3];
                if (File.Exists(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameSQLServer))
                {
                    using (StreamReader sr = new StreamReader(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameSQLServer))
                    {
                        if (!sr.EndOfStream)
                        {
                            string str = sr.ReadLine();
                            string[] value = str.Split(new char[] { ';' });
                            values[0] = value[0];
                            values[1] = value[1];
                            values[2] = value[2];
                            sr.Close();
                        }
                    }
                }
            }
            //else if (valueDB == 3)
            //{
            //    //Access
            //    values = new string[1];
            //    if (File.Exists(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameAccess))
            //    {
            //        using (StreamReader sr = new StreamReader(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameAccess))
            //        {
            //            if (!sr.EndOfStream)
            //            {
            //                string str = sr.ReadLine();
            //                //string[] value = str.Split(new char[] { ';' });
            //                values[0] = str;
            //                sr.Close();
            //            }
            //        }
            //    }
            //}

            return values;
        }

        #endregion

        #region Verification et exécution de la connexion à la BD pour une base PostGres,MySQL,SQLServer ou Access(2003,2007 ou 2010) pour une connexion distante
        /// <summary>
        /// Permet de vérifier les valeurs de connexion à la base de données avant d'effectuer la connexion proprement dite
        /// le paramètre valueDB a pour valeur 0=>Pour PosyGresSQL, 1=>Pour MySQL, 2=> Pour SQLServer et 3=> Pour Accesss (2003, 2007 ou 2010)
        /// </summary>
        /// <param name="port">Numero de port avec comme numero de port local 5432 pour PostGresSQL</param>
        /// <param name="serveur">Nom du serveur et serveur par defaut localhost pour PostGresSQL et root pour MySql</param>
        /// <param name="database">Nom de la BD ou son chemin d'acces pour une base Access</param>
        /// <param name="userName">Nom d'utilisateur et utilisateur par defaut postgres pour PostGresSQ, sa pour SQLServer et root pour MySQLL</param>
        /// <param name="password">Mot de passe</param>
        /// <param name="stringConnect">Chaine de connexion</param>
        /// <param name="valueDB">entier representant la BD a choisir</param>
        /// <returns>un booleen</returns>
        public bool VerifieDoConnectDistante(int? port, string serveur, string database, string userName, string password, int valueDB)
        {
            bool ok = false;
            if (valueDB == 0 || valueDB == 1 || valueDB == 2)
            {
                if (string.IsNullOrEmpty(serveur))
                    throw new Exception("Ce serveur n'existe pas !!");
                else if (string.IsNullOrEmpty(database))
                    throw new Exception("Cette base de données n'existe pas !!");
                else
                {
                    if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                    {
                        ok = false;
                        throw new Exception("Les informations d'authentification sont invalides !!");
                    }
                    else
                    {
                        if (valueDB == 0)
                        {
                            //PostGresSQL
                            if (port == 0 || string.IsNullOrEmpty(Convert.ToString(port)))
                            {
                                ok = false;
                                throw new Exception("Le numéro de port est invalide !!");
                            }
                            else
                            {
                                stringConnect = "Server=" + serveur + ";Port=" + port + ";UID=" + userName + ";Password=" + password + ";Database=" + database;//PosgresSQL
                                con = new NpgsqlConnection(stringConnect);
                                con.Open();
                                saveParamConnectionDistante(serveur, database, userName, Convert.ToString(port), valueDB);
                                ok = true;
                            }
                        }
                        else if (valueDB == 1)
                        {
                            //MySQL
                            stringConnect = "Server=" + serveur + ";Database=" + database + ";Uid=" + userName + ";Pwd=" + password;
                            con = new MySqlConnection(stringConnect);
                            con.Open();
                            saveParamConnectionDistante(serveur, database, userName, null, valueDB);

                            ok = true;
                        }
                        else if (valueDB == 2)
                        {
                            //SQLServer
                            stringConnect = "Data Source=" + serveur + ";Initial Catalog=" + database + ";User ID=" + userName + ";Password=" + password + ";Integrated Security=" + false;//SQLServer
                            //stringConnect = "Server=" + serveur + ";Initial Catalog=" + database + ";User ID=" + userName + ";Password=" + password + ";Persist Security Info=" + securite;
                            con = new SqlConnection(stringConnect);
                            con.Open();
                            saveParamConnectionDistante(serveur, database, userName, null, valueDB);

                            ok = true;
                        }
                    }
                }
            }
            else
            {
                //Access 2003, 2007 ou 2010
                if (string.IsNullOrEmpty(database))
                    throw new Exception("Cette base de données n'existe pas !!");
                else
                {//.accdb=bdcca. et .mdb=bdm.
                    string strReverse = "";
                    for (int i = 0, j = database.Length - 1; i < database.Length; i++, j--) strReverse = strReverse + database[j];

                    if (strReverse[0].ToString() == 'b'.ToString() && strReverse[1].ToString() == 'd'.ToString()
                        && strReverse[2].ToString() == 'c'.ToString() && strReverse[3].ToString() == 'c'.ToString()
                        && strReverse[4].ToString() == 'a'.ToString() && strReverse[5].ToString() == '.'.ToString())
                        //Fichier Microsoft Access 2007 ou 2010
                        stringConnect = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + database;//Acsess
                    else if (strReverse[0].ToString() == 'b'.ToString() && strReverse[1].ToString() == 'd'.ToString()
                        && strReverse[2].ToString() == 'm'.ToString() && strReverse[3].ToString() == '.'.ToString())
                        //Fichier Microsoft Access 2003
                        stringConnect = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + database;//Acsess
                    else throw new Exception("Fichier de base de donnée Microsoft Access inconnu !!");

                    con = new OleDbConnection(stringConnect);
                    con.Open();
                    saveParamConnectionDistante(null, database, null, null, valueDB);

                    ok = true;
                }
            }

            return ok;
        }

        /// <summary>
        ///Permet d'enregistrer la chaîne de connexion pour une base des donnée PostGresSQL dans un fichier texte 
        /// </summary>
        private static void saveParamConnectionDistante(string serveur, string bd, string userName, string port, int valueBD)
        {
            if (valueBD == 0)
            {
                //PostGresSQL
                using (StreamWriter srw = new StreamWriter(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNamePostGresDistante, false))
                {
                    srw.WriteLine("{0};{1};{2};{3}", serveur, bd, userName, port);
                    srw.Close();
                }
            }
            else if (valueBD == 1)
            {
                //MySQL
                using (StreamWriter srw = new StreamWriter(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameMySQLDistante, false))
                {
                    srw.WriteLine("{0};{1};{2}", serveur, bd, userName);
                    srw.Close();
                }
            }
            else if (valueBD == 2)
            {
                //SQLServer
                using (StreamWriter srw = new StreamWriter(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameSQLServerDistante, false))
                {
                    srw.WriteLine("{0};{1};{2}", serveur, bd, userName);
                    srw.Close();
                }
            }
            //else if (valueBD == 3)
            //{
            //    using (StreamWriter srw = new StreamWriter(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameAccess, false))
            //    {
            //        srw.WriteLine("{0}", bd);
            //        srw.Close();
            //    }
            //}
        }

        /// <summary>
        /// Permet de charger la chaîne de connection à partir d'un fichier texte pour une Base PostGresSql et retourne un tableau
        /// contenant ces différentes valeurs (Serveur, Base de données, Nom d'utilisateur et numero de port)
        /// </summary>
        /// <returns>retourne un tableau</returns>
        public string[] loadParamDistante(int valueDB)
        {
            string[] values = { };
            if (valueDB == 0)
            {
                //PostGresSQL
                values = new string[4];
                if (File.Exists(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNamePostGresDistante))
                {
                    using (StreamReader sr = new StreamReader(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNamePostGresDistante))
                    {
                        if (!sr.EndOfStream)
                        {
                            string str = sr.ReadLine();
                            string[] value = str.Split(new char[] { ';' });
                            values[0] = value[0];
                            values[1] = value[1];
                            values[2] = value[2];
                            values[3] = value[3];
                            sr.Close();
                        }
                    }
                }
            }
            else if (valueDB == 1)
            {
                //MySQL
                values = new string[3];
                if (File.Exists(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameMySQLDistante))
                {
                    using (StreamReader sr = new StreamReader(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameMySQLDistante))
                    {
                        if (!sr.EndOfStream)
                        {
                            string str = sr.ReadLine();
                            string[] value = str.Split(new char[] { ';' });
                            values[0] = value[0];
                            values[1] = value[1];
                            values[2] = value[2];
                            sr.Close();
                        }
                    }
                }
            }
            else if (valueDB == 2)
            {
                //SQLServer
                values = new string[3];
                if (File.Exists(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameSQLServerDistante))
                {
                    using (StreamReader sr = new StreamReader(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameSQLServerDistante))
                    {
                        if (!sr.EndOfStream)
                        {
                            string str = sr.ReadLine();
                            string[] value = str.Split(new char[] { ';' });
                            values[0] = value[0];
                            values[1] = value[1];
                            values[2] = value[2];
                            sr.Close();
                        }
                    }
                }
            }
            //else if (valueDB == 3)
            //{
            //    //Access
            //    values = new string[1];
            //    if (File.Exists(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameAccessDistante))
            //    {
            //        using (StreamReader sr = new StreamReader(updateCreateDirectory("JOVIA_SEVEUR") + @"\" + fileNameAccessDistante))
            //        {
            //            if (!sr.EndOfStream)
            //            {
            //                string str = sr.ReadLine();
            //                //string[] value = str.Split(new char[] { ';' });
            //                values[0] = str;
            //                sr.Close();
            //            }
            //        }
            //    }
            //}

            return values;
        }

        #endregion

        #region CREATION DU REPERTOIRE PERMETTANT DE GARDER LES PARAMETRES DU FICHIER POUR LA CONNEXION A LA BASE
        private static string updateCreateDirectory(string nomRepertoire)
        {
            string cheminAccesRepertoire = "";
            //Recuperation du nom du lecteur dans lequel le projet se trouve
            string lecteur = Environment.CurrentDirectory.ToString().Substring(0, 2);
            DirectoryInfo directory = new DirectoryInfo(lecteur + @"\" + nomRepertoire);
            if (!directory.Exists)
            {
                //Creation du repertoire
                directory.Create();
                directory.Attributes = FileAttributes.Hidden;
                cheminAccesRepertoire = directory.FullName;
            }
            else
            {
                //Dossier existant
                cheminAccesRepertoire = directory.FullName;
            }
            return cheminAccesRepertoire;
        }
        #endregion

        #region REGENERATION POUR UN NOUVEL ID AUTO INCREMENTE PAR RAPPORT AU DERNIER ID DE LA TABLE
        /// <summary>
        /// Permet d'obtenir un nouveau ID pour l'objet passe en paramètre
        /// </summary>
        /// <param name="parametre">Paramètre de type Object</param>
        /// <returns>Un entier</returns>
        public int ReNewIdValue(object obj)
        {
            int goodId = 0;
            string tablename = "";
            string[] tbNanetable = new string[] { "personne", "villeTerritoire", "communeChefferieSecteur", "quartierLocalite", "avenueVillage", "utilisateur", "carte", "telephone", "categorieUtilisateur", "province", "photo", "serveur" };
            if (obj is PersonneServeur) tablename = Convert.ToString(tbNanetable[0]);
            else if (obj is VilleTeritoireServeur) tablename = Convert.ToString(tbNanetable[1]);
            else if (obj is CommuneChefferieSecteurServeur) tablename = Convert.ToString(tbNanetable[2]);
            else if (obj is QuartierLocaliteServeur) tablename = Convert.ToString(tbNanetable[3]);
            else if (obj is AvenueVillageServeur) tablename = Convert.ToString(tbNanetable[4]);
            else if (obj is UtilisateurServeur) tablename = Convert.ToString(tbNanetable[5]);
            else if (obj is CarteServeur) tablename = Convert.ToString(tbNanetable[6]);
            else if (obj is TelephoneServeur) tablename = Convert.ToString(tbNanetable[7]);
            else if (obj is CategorieUtilisateurServeur) tablename = Convert.ToString(tbNanetable[8]);
            else if (obj is Province) tablename = Convert.ToString(tbNanetable[9]);
            else if (obj is PhotoServeur) tablename = Convert.ToString(tbNanetable[10]);
            else if (obj is Serveur) tablename = Convert.ToString(tbNanetable[11]);
            else tablename = "";
            //if (con.State.ToString().Equals("Open")) { }
            //else con.Open();
            con.Close();
            con.Open();
            IDbCommand cmdRenewID = con.CreateCommand();
            cmdRenewID.CommandText = "SELECT MAX(id) AS maxid FROM " + tablename;

            IDataReader dr = cmdRenewID.ExecuteReader();
            if (dr.Read())
            {
                if (dr["maxid"] == DBNull.Value) goodId = 1;
                else goodId = Convert.ToInt32(dr["maxid"]) + 1;
            }
            dr.Close();
            cmdRenewID.Dispose();
            con.Close();
            return goodId;
        }
        #endregion

        #region Execution de la modification du nom d'utilisateur seulement
        /// <summary>
        /// Permet de modifier uniquement le nom de l'utilisateur en lui passant en paramètre l'ancien nom d'utilisateur ansi que le nouveau a changer
        /// </summary>
        /// <param name="oldNameUser">String</param>
        /// /// <param name="newNameUser">String</param>
        public void UpdateUserName(string oldNameUser, string newNameUser)
        {
            int idUtilisateur = 0;
            //if (con.State.ToString().Equals("Open")) { }
            //else con.Open();
            con.Close();
            con.Open();
            IDbCommand cmdGetIdUserName = null;
            cmdGetIdUserName = con.CreateCommand();
            cmdGetIdUserName.CommandText = "select id from utilisateur where nomuser=@nomuser";

            IDataParameter paramNomuser = cmdGetIdUserName.CreateParameter();
            paramNomuser.ParameterName = "@nomuser";
            paramNomuser.Value = oldNameUser;
            cmdGetIdUserName.Parameters.Add(paramNomuser);
            IDataReader dr = cmdGetIdUserName.ExecuteReader();
            if (dr.Read()) idUtilisateur = Convert.ToInt32(dr["id"]);

            dr.Close();
            cmdGetIdUserName.Dispose();

            IDbCommand cmdUpdateUserName = null;
            cmdUpdateUserName = con.CreateCommand();
            cmdUpdateUserName.CommandText = "update utilisateur set nomuser=@nomuser WHERE id=@id";

            IDataParameter paramId = cmdUpdateUserName.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = idUtilisateur;
            IDataParameter paramNomuser1 = cmdUpdateUserName.CreateParameter();
            paramNomuser1.ParameterName = "@nomuser";
            paramNomuser1.Value = newNameUser;
            cmdUpdateUserName.Parameters.Add(paramId);
            cmdUpdateUserName.Parameters.Add(paramNomuser1);

            cmdUpdateUserName.ExecuteNonQuery();
            cmdUpdateUserName.Dispose();
            con.Close();
        }
        #endregion

        #region Execution de la modification du mot de passe de l'utilisateur seulement
        /// <summary>
        /// Permet de modifier uniquement le mot de passe de l'utilisateur en lui passant en paramètre le nom d'utilisateur et le nouveau mot de passe de ce dernier
        /// </summary>
        /// <param name="userName">String</param>
        /// <param name="password">String</param>
        public void UpdatePasswordUser(string userName, string password)
        {
            int idUtilisateur = 0;
            //if (con.State.ToString().Equals("Open")) { }
            //else con.Open();
            con.Close();
            con.Open();
            IDbCommand cmdGetIdUserName2 = null;
            cmdGetIdUserName2 = con.CreateCommand();
            cmdGetIdUserName2.CommandText = "select id from utilisateur where nomuser=@nomuser";

            IDataParameter paramNomuser = cmdGetIdUserName2.CreateParameter();
            paramNomuser.ParameterName = "@nomuser";
            paramNomuser.Value = userName;
            cmdGetIdUserName2.Parameters.Add(paramNomuser);

            IDataReader dr = cmdGetIdUserName2.ExecuteReader();
            if (dr.Read()) idUtilisateur = Convert.ToInt32(dr["id"]);

            dr.Close();
            cmdGetIdUserName2.Dispose();

            IDbCommand cmdUpdatePasswordUser = null;
            cmdUpdatePasswordUser = con.CreateCommand();
            cmdUpdatePasswordUser.CommandText = "update utilisateur set motpass=@motpass WHERE id=@id";

            IDataParameter paramId = cmdUpdatePasswordUser.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = idUtilisateur;
            IDataParameter paramMotpass = cmdUpdatePasswordUser.CreateParameter();
            paramMotpass.ParameterName = "@motpass";
            paramMotpass.Value = password;
            cmdUpdatePasswordUser.Parameters.Add(paramId);
            cmdUpdatePasswordUser.Parameters.Add(paramMotpass);

            cmdUpdatePasswordUser.ExecuteNonQuery();
            cmdUpdatePasswordUser.Dispose();
            con.Close();
        }
        #endregion

        #region Execution de la modification de la personne avec une date de deces
        /// <summary>
        /// Permet de modifier une personne en ajoutant la date de décès
        /// </summary>
        /// <param name="obj">Object Personne</param>
        internal void UpdateDeces(PersonneServeur pers)
        {
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdUpdateDeces = null;

            cmdUpdateDeces = con.CreateCommand();
            cmdUpdateDeces.CommandText = "update personne set datedeces=@datedeces where numeroNational=@numeroNational";

            IDataParameter paramNumeroNational = cmdUpdateDeces.CreateParameter();
            paramNumeroNational.ParameterName = "@numeroNational";
            paramNumeroNational.Value = pers.NumeroNational;
            IDataParameter paramDatedeces = cmdUpdateDeces.CreateParameter();
            paramDatedeces.ParameterName = "@datedeces";
            paramDatedeces.Value = pers.Datedeces;
            cmdUpdateDeces.Parameters.Add(paramNumeroNational);
            cmdUpdateDeces.Parameters.Add(paramDatedeces);
            cmdUpdateDeces.ExecuteNonQuery();
            cmdUpdateDeces.Dispose();
            con.Close();
        }
        #endregion

        #region Recuperation de la photo de la personne
        /// <summary>
        /// Permet de récupéré une photo que devrait être affichée dans un objet imagebox
        /// et reçoit en paramètre l'object de type photo (personne)
        /// </summary>
        /// <param name="obj">Objet de la classe</param>
        /// <returns>Retourne un objet MemoryStream</returns>
        public MemoryStream GetPhotoPersonne(PhotoServeur photoPers)
        {
            MemoryStream ms = new MemoryStream();

            if (con.State.Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetPhoto = con.CreateCommand();
            cmdGetPhoto.CommandText = "SELECT photo FROM photo WHERE id=@id";
            IDataParameter paramPhotoPers = cmdGetPhoto.CreateParameter();
            paramPhotoPers.ParameterName = "@id";
            paramPhotoPers.Value = photoPers.Id;
            cmdGetPhoto.Parameters.Add(paramPhotoPers);
            ms = new MemoryStream((Byte[])cmdGetPhoto.ExecuteScalar());

            cmdGetPhoto.Dispose();
            con.Close();
            return ms;
        }
        #endregion

        #region Personne(Operation sur la personne)
        /// <summary>
        /// Permet de retourner un personne suivant son identifiant passé en paramètre
        /// </summary>
        /// <param name="id">Indetifinat de la Personne</param>
        /// <returns>Une personne</returns>
        public PersonneServeur GetPersonne(int id)
        {
            PersonneServeur personne = null;

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetPersonneServeur = con.CreateCommand();
            cmdGetPersonneServeur.CommandText = "select * from personne where id=@id";
            IDataParameter paramId = cmdGetPersonneServeur.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            cmdGetPersonneServeur.Parameters.Add(paramId);

            IDataReader dr = cmdGetPersonneServeur.ExecuteReader();
            if (dr.Read()) personne = getPersonne(dr);

            dr.Close();
            cmdGetPersonneServeur.Dispose();
            con.Close();
            return personne;
        }

        private static PersonneServeur getPersonne(IDataReader dr)
        {
            PersonneServeur personne = new PersonneServeur();
            personne.Id = Convert.ToInt32(dr["id"].ToString());
            personne.Id_avenueVillage = Convert.ToInt32(dr["id_avenueVillage"]);
            personne.Nom = dr["nom"].ToString();
            personne.Postnom = dr["postnom"].ToString();
            personne.Prenom = dr["prenom"].ToString();

            int tempIdPere = (dr["id_pere"]) == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(dr["id_pere"]);
            int tempIdMere = (dr["id_mere"]) == DBNull.Value ? Convert.ToInt32(null) : Convert.ToInt32(dr["id_mere"]);

            if (tempIdPere == 0) personne.Id_pere = null;
            else personne.Id_pere = tempIdPere;

            if (tempIdMere == 0) personne.Id_mere = null;
            else personne.Id_mere = tempIdMere;

            personne.Sexe = dr["sexe"].ToString();
            personne.EtatCivile = dr["etativil"].ToString();
            personne.NumeroNational = dr["numeroNational"].ToString();
            personne.Numero = dr["numero"].ToString();
            personne.NombreEnfant = Convert.ToInt32(dr["nombreEnfant"]);
            personne.Niveau = dr["niveauEtude"].ToString();
            personne.Travail = Convert.ToBoolean(dr["travail"]);
            personne.Datenaissance = dr["datenaissance"].Equals(DBNull.Value) ? Convert.ToDateTime(null) : Convert.ToDateTime(dr["datenaissance"]);
            personne.Datedeces = dr["datedeces"].Equals(DBNull.Value) ? Convert.ToDateTime(null) : Convert.ToDateTime(dr["datedeces"]);
            return personne;
        }

        /// <summary>
        /// Permet de retourner toutes les personnes
        /// </summary>
        /// <returns>Liste des personnes</returns>
        public List<PersonneServeur> GetPersonnes()
        {
            List<PersonneServeur> list = new List<PersonneServeur>();

            con.Close();
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();
            IDbCommand cmdGetPersonneServeurs = con.CreateCommand();
            cmdGetPersonneServeurs.CommandText = "SELECT * FROM personne ORDER BY id ASC";
            IDataReader dr = cmdGetPersonneServeurs.ExecuteReader();
            while (dr.Read())
            {
                list.Add(getPersonne(dr));
            }
            dr.Close();
            cmdGetPersonneServeurs.Dispose();
            con.Close();
            return list;
        }

        /// <summary>
        /// Permet de récupérer la date de naisssance d'une personne connaissant son identifiant et la retourne
        /// </summary>
        /// <param name="idPersonne">Identifiant de la personne</param>
        /// <returns>Date de naissance nullable</returns>
        public DateTime? GetDateNaissance(string numNational)
        {
            DateTime? dateNaiss = new DateTime?();

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetNaissance = con.CreateCommand();
            cmdGetNaissance.CommandText = "SELECT datenaissance FROM personne WHERE numeroNational=@numeroNational";

            IDataParameter paramNumeroNational = cmdGetNaissance.CreateParameter();
            paramNumeroNational.ParameterName = "@numeroNational";
            paramNumeroNational.Value = numNational;
            cmdGetNaissance.Parameters.Add(paramNumeroNational);

            IDataReader dr = cmdGetNaissance.ExecuteReader();

            if (dr.Read())
            {
                if (dr["datenaissance"].Equals(DBNull.Value)) dateNaiss = null;
                else dateNaiss = Convert.ToDateTime(dr["datenaissance"]);
            }
            else { dateNaiss = null; }

            dr.Close();
            cmdGetNaissance.Dispose();
            con.Close();
            return dateNaiss;
        }

        public int GetIdPersonneServeur(string numeroNational)
        {
            int intIdentifiant = 0;

            con.Close();
            con.Open();

            IDbCommand cmdGetIdPersonneServeur = con.CreateCommand();
            cmdGetIdPersonneServeur.CommandText = "SELECT id FROM personne WHERE numeroNational=@numeroNational";

            IDataParameter paramNumeroNational = cmdGetIdPersonneServeur.CreateParameter();
            paramNumeroNational.ParameterName = "@numeroNational";
            paramNumeroNational.Value = numeroNational;
            cmdGetIdPersonneServeur.Parameters.Add(paramNumeroNational);

            IDataReader dr = cmdGetIdPersonneServeur.ExecuteReader();

            if (dr.Read()) intIdentifiant = Convert.ToInt32(dr["id"]);
            else { }
            dr.Close();
            cmdGetIdPersonneServeur.Dispose();
            con.Close();

            return intIdentifiant;
        }

        /// <summary>
        /// Permet d'éffectuer la recherche d'une Personne en passant en paramètre son Numéro National. Et retourne successivement
        /// L'identifiant de la Personne, l'identifiant de l'Avenue, le nom, le PostNom, le Prénom, l'identifiant du père, , l'identifiant de la mère,
        /// le Sexe, l'Etat civil, le Numéro National, le Numéro de residence, le nombre d'Enfant, le Niveau d'étude, Le Statut professionnel, la Date de naissance,
        /// la Date de decès, le code de la Photo ainsi que le string représentant la photo elle même le tout dans un tableau
        /// </summary>
        /// <param name="numeroNational">Numero National de la Personne</param>
        /// <returns>Une List de Personne</returns>
        public List<PersonneServeur> SearchPersonneServeur(string numeroNational)
        {
            List<PersonneServeur> list = new List<PersonneServeur>();

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();


            IDbCommand cmdSearchPersonneServeur = con.CreateCommand();
            cmdSearchPersonneServeur.CommandText = "SELECT id,id_avenueVillage,id_pere,id_mere,numeroNational," +
                "numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,nombreEnfant,niveauEtude " +
                "FROM personne WHERE numeroNational=@numeroNational";
            IDataParameter paramNumeroNat = cmdSearchPersonneServeur.CreateParameter();
            paramNumeroNat.ParameterName = "@numeroNational";
            paramNumeroNat.Value = numeroNational.ToUpper();
            cmdSearchPersonneServeur.Parameters.Add(paramNumeroNat);

            IDataReader dr = cmdSearchPersonneServeur.ExecuteReader();

            if (dr.Read())
            {
                list.Add(getPersonne(dr));
            }
            else throw new Exception("Le numéro National ' " + numeroNational + " ' recherché n'a pas été trouvé dans la base des données");

            dr.Close();
            cmdSearchPersonneServeur.Dispose();
            con.Close();
            return list;
        }
        #endregion

        #region PHOTO PersonneServeur(Operation sur la photo de la peronne)
        /// <summary>
        /// Permet de retourner la photo de la peronne suivant son identifiant passé en paramètre
        /// </summary>
        /// <param name="id">Indetifinat de la Personne</param>
        /// <returns>Une photo de la peronne</returns>
        public PhotoServeur GetPhoto(int idPersonneServeur)
        {
            PhotoServeur photoPers = null;

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetPhoto = con.CreateCommand();
            cmdGetPhoto.CommandText = "select *from photo where id_personne=@id_personne";

            IDataParameter paramId_personne = cmdGetPhoto.CreateParameter();
            paramId_personne.ParameterName = "@id_personne";
            paramId_personne.Value = idPersonneServeur;
            cmdGetPhoto.Parameters.Add(paramId_personne);

            IDataReader dr = cmdGetPhoto.ExecuteReader();
            if (dr.Read()) photoPers = getPhoto(dr);

            dr.Close();
            cmdGetPhoto.Dispose();
            con.Close();
            return photoPers;
        }

        private static PhotoServeur getPhoto(IDataReader dr)
        {
            PhotoServeur photoPers = new PhotoServeur();
            photoPers.Id = Convert.ToInt32(dr["id"]);
            photoPers.Id_personne = Convert.ToInt32(dr["id_personne"]);
            photoPers.PhotoPersonne = dr["photo"].ToString();
            return photoPers;
        }
        public int GetIdPhoto(int idPersonneServeur,string numeroNational)
        {
            int idPersonne = 0;

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetIdPhoto = con.CreateCommand();
            cmdGetIdPhoto.CommandText = @"select p.id AS idPersPhoto from photo AS p INNER JOIN personne AS pers ON 
            pers.id=p.id_personne where pers.id=@idPers and pers.numeroNational=@numNational";

            IDataParameter paramIdPers = cmdGetIdPhoto.CreateParameter();
            paramIdPers.ParameterName = "@idPers";
            paramIdPers.Value = idPersonneServeur;
            IDataParameter paramNumNational = cmdGetIdPhoto.CreateParameter();
            paramNumNational.ParameterName = "@numNational";
            paramNumNational.Value = numeroNational;
            cmdGetIdPhoto.Parameters.Add(paramIdPers);
            cmdGetIdPhoto.Parameters.Add(paramNumNational);

            IDataReader dr = cmdGetIdPhoto.ExecuteReader();
            if (dr.Read()) idPersonne = Convert.ToInt32(dr["idPersPhoto"]);

            dr.Close();
            cmdGetIdPhoto.Dispose();
            con.Close();
            return idPersonne;
        }
        #endregion

        #region Generation du numero d'identification national de la personne
        /// <summary>
        /// Permet de générer un numéro d'identification National pour la personne à enregistrer
        /// </summary>
        /// <returns>string</returns>
        public string generateNumIdNational(int idAvenue)
        {
            PersonneServeur personne = new PersonneServeur();
            string strProvince = "";

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetDesiProv = con.CreateCommand();
            cmdGetDesiProv.CommandText = "SELECT designation FROM Province";

            IDataReader dr = cmdGetDesiProv.ExecuteReader();
            if (dr.Read()) strProvince = Convert.ToString(dr["designation"]);
            dr.Close();
            cmdGetDesiProv.Dispose();
            con.Close();

            //Une fois la designation complete recuperee on split suivant l'espace et on prend
            //respectivement les premiers caractere, et si pas d'espace on prend les deux premiers caracteres

            bool spaceFound = false;
            string strSplited = "", strNumeroNational = "";
            for (int i = 0; i < strProvince.Length; i++)
            {
                if (char.IsSeparator(strProvince[i]))
                {
                    spaceFound = true;
                    break;
                }
                else spaceFound = false;
            }

            string[] tbSplit = strProvince.Split(new char[] { ' ' });

            //S'il ya un espace on prend les premiers caracteres (Debut et apres espace)
            if (spaceFound) strSplited = tbSplit[0].Substring(0, 1) + tbSplit[1].Substring(0, 1);
            else
                //Et s'il n'a pas d'espace ont prend les deux premiers caracteres de la province
                strSplited = strProvince.Substring(0, 2);

            //Apres on concatene avec L'ID de la personne
            strNumeroNational = strSplited + this.ReNewIdValue(personne) + idAvenue;
            return strNumeroNational;
        }
        #endregion

        #region CONVERTION DE L'IMAGE EN BINAIRE
        /// <summary>
        /// Permet de convertire l'image en données binaires
        /// </summary>
        /// <param name="filePath">Chemin de l'image</param>
        /// <returns>L'image sous forme de byte[]</returns>
        private byte[] GetImage(string cheminFichier)
        {
            FileStream fs = new FileStream(cheminFichier, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            byte[] img = br.ReadBytes((int)fs.Length);
            br.Close();
            fs.Close();
            return img;
        }
        #endregion

        #region RECUPERATION DU NIVEAU DE L'UTILISATEUR POUR S'EN SERVIR A ENABLED OU DESABLED DES MENUS
        /// <summary>
        /// Permet de retourner un entier représentant le niveau d'un utilisateur Côté National
        /// 0=>Administrateur, 1=>Utilisateur avec accès limité
        /// </summary>
        /// <returns>Entier</returns>
        public int enabledDesabledObject()
        {
            int intOk = 12;
            if (this.ReadFileParametersUser()[2].Equals("0")) intOk = 0;
            else if (this.ReadFileParametersUser()[2].Equals("1")) intOk = 1;
            return intOk;
        }
        #endregion

        #region VERIFICATION DE L'AUTHENTIFICATION DE L'AGENT ET RECUPERATION DE SA CATEGORIE + INSCRIPTION ET SUPPRESSION DANS UN FICHIER TXT COTE PC
        /// <summary>
        /// Permet de verifier les paramètres de connexion de l'utilisateur, donc username et password
        /// et retourne un tableau contenant successivement l'Id de la catégorie utilisateur, la désignation
        /// de la catégorie, le niveau de l'utilisateur ainsi que son Id en tant que personne
        /// </summary>
        /// <param name="username">String nom d'utilisateur</param>
        /// <param name="password">String mot de passe</param>
        /// <returns>Tableau des string</returns>
        public string[] verifieLoginUser(string username, string password)
        {
            string[] tbValue = new string[4];
            bool okActivateUser = false;

            if (username.Equals("superuserserveur") && password.Equals("superpasswordserveur"))
            {
                tbValue[0] = "";
                tbValue[1] = "";
                tbValue[2] = "0";
                tbValue[3] = "";

                StreamWriter write = new StreamWriter(updateCreateDirectory("JOVIA_SEVEUR") + @"\parametresUser.par", false);
                write.WriteLine("{0};{1};{2};{3}", tbValue[0], tbValue[1], tbValue[2], tbValue[3]);
                write.Close();
            }
            else
            {
                //Echec de la connexion en superAdministrateur alors on peut se connecte en Administrateur 
                //ou en invite
                if (con.State.ToString().Equals("Open")) { }
                else con.Open();

                IDbCommand cmdVeriLoginUI = con.CreateCommand();
                cmdVeriLoginUI.CommandText = @"SELECT c.id AS idCatUser,c.designation AS designationCat,c.groupe AS groupe,u.id_personne AS id_personne,u.activation AS activation
                FROM categorieUtilisateur AS c INNER JOIN utilisateur AS u ON c.id=u.id_categorieUtilisateur WHERE nomuser=@nomuser AND motpass=@motpass";
                IDataParameter paramNomUser = cmdVeriLoginUI.CreateParameter();
                paramNomUser.ParameterName = "@nomuser";
                paramNomUser.Value = username;
                IDataParameter paramMotPass = cmdVeriLoginUI.CreateParameter();
                paramMotPass.ParameterName = "@motpass";
                paramMotPass.Value = password;
                cmdVeriLoginUI.Parameters.Add(paramNomUser);
                cmdVeriLoginUI.Parameters.Add(paramMotPass);

                IDataReader dr = cmdVeriLoginUI.ExecuteReader();
                
                if (dr.Read())
                {
                    tbValue[0] = Convert.ToString(dr["idCatUser"]);
                    tbValue[1] = Convert.ToString(dr["designationCat"]);
                    tbValue[2] = Convert.ToString(dr["groupe"]).ToUpper().Equals("ADMINISTRATEUR") ? "0" : "1";
                    tbValue[3] = Convert.ToString(dr["id_personne"]);
                    okActivateUser = Convert.ToBoolean(dr["activation"]);

                    //Si desvaleurs sont trouvee et que la personne se connecte tout en etant active,on les inscrits 
                    //dans un fichier text dont le contenu sera supprime apres deconnection de l'utilisateur
                    if (okActivateUser)
                    {
                        StreamWriter write = new StreamWriter(updateCreateDirectory("JOVIA_SEVEUR") + @"\parametresUser.par", false);
                        write.WriteLine("{0};{1};{2};{3}", tbValue[0], tbValue[1], tbValue[2], tbValue[3]);
                        write.Close();
                    }
                    else
                    {
                        tbValue[0] = "";
                        tbValue[1] = "";
                        tbValue[2] = "";
                        tbValue[3] = "";
                    }
                }
                else
                {
                    tbValue[0] = "";
                    tbValue[1] = "";
                    tbValue[2] = "";
                    tbValue[3] = "";
                }

                dr.Close();
                cmdVeriLoginUI.Dispose();
                con.Close();
            }

            return tbValue;
        }

        /// <summary>
        /// Permet de vider le fichier contenant les paramètres de l'utilsateur connecté
        /// </summary>
        public void EmptyFileParametersUser()
        {
            StreamWriter write = new StreamWriter(updateCreateDirectory("JOVIA_SEVEUR") + @"\parametresUser.par", false);
            write.WriteLine("{0}", "");
            write.Close();
        }

        /// <summary>
        /// Permet de lire le fichier contenant les paramètres de l'utilisateur connecté 
        /// pour une quelconque fin (l'Id de la catégorie utilisateur, la désignation
        /// de la catégorie, le niveau de l'utilisateur ainsi que son Id en tant que personne)
        /// </summary>
        /// <returns>Tableau des string</returns>
        public string[] ReadFileParametersUser()
        {
            string[] tbValues = new string[4];
            StreamReader read = new StreamReader(updateCreateDirectory("JOVIA_SEVEUR") + @"\parametresUser.par");
            if (!read.EndOfStream)
            {
                string lectureLigne = read.ReadLine();
                string[] tbRecupValues = lectureLigne.Split(new char[] { ';' });
                tbValues[0] = tbRecupValues[0];
                tbValues[1] = tbRecupValues[1];
                tbValues[2] = tbRecupValues[2];
                tbValues[3] = tbRecupValues[3];
                read.Close();
            }
            return tbValues;
        }
        #endregion

        #region VERIFICATION DE L'AUTHENTIFICATION DE L'AGENT ET RECUPERATION DE SA CATEGORIE + INSCRIPTION ET SUPPRESSION DANS UN FICHIER TXT COTE PC POUR UNE CONNEXION DISTANTE
        /// <summary>
        /// Permet de verifier les paramètres de connexion de l'utilisateur, donc username et password
        /// et retourne un tableau contenant successivement l'Id de la catégorie utilisateur, la désignation
        /// de la catégorie, le niveau de l'utilisateur ainsi que son Id en tant que personne
        /// </summary>
        /// <param name="username">String nom d'utilisateur</param>
        /// <param name="password">String mot de passe</param>
        /// <returns>Tableau des string</returns>
        public string[] verifieLoginUserDistante(string username, string password)
        {
            string[] tbValue = new string[4];

            if (username.Equals("superuserserveur") && password.Equals("superpasswordserveur"))
            {
                tbValue[0] = "";
                tbValue[1] = "";
                tbValue[2] = "0";
                tbValue[3] = "";

                StreamWriter write = new StreamWriter(updateCreateDirectory("JOVIA_SEVEUR") + @"\parametresUser.par", false);
                write.WriteLine("{0};{1};{2};{3}", tbValue[0], tbValue[1], tbValue[2], tbValue[3]);
                write.Close();
            }
            else
            {
                //Echec de la connexion en superAdministrateur alors on peut se connecte en Administrateur 
                //ou en invite
                if (con.State.ToString().Equals("Open")) { }
                else con.Open();

                IDbCommand cmdVeriLoginUI = con.CreateCommand();
                cmdVeriLoginUI.CommandText = @"SELECT c.id AS idCatUser,c.designation AS designationCat,c.groupe AS groupe,u.id_personne AS id_personne
                FROM categorieUtilisateur AS c INNER JOIN utilisateur AS u ON c.id=u.id_categorieUtilisateur WHERE nomuser=@nomuser AND motpass=@motpass";
                IDataParameter paramNomUser = cmdVeriLoginUI.CreateParameter();
                paramNomUser.ParameterName = "@nomuser";
                paramNomUser.Value = username;
                IDataParameter paramMotPass = cmdVeriLoginUI.CreateParameter();
                paramMotPass.ParameterName = "@motpass";
                paramMotPass.Value = password;
                cmdVeriLoginUI.Parameters.Add(paramNomUser);
                cmdVeriLoginUI.Parameters.Add(paramMotPass);

                IDataReader dr = cmdVeriLoginUI.ExecuteReader();

                if (dr.Read())
                {
                    tbValue[0] = Convert.ToString(dr["idCatUser"]);
                    tbValue[1] = Convert.ToString(dr["designationCat"]);
                    tbValue[2] = Convert.ToString(dr["groupe"]).ToUpper().Equals("ADMINISTRATEUR") ? "0" : "1";
                    tbValue[3] = Convert.ToString(dr["id_personne"]);

                    //Si desvaleurs sont trouvee et que la personne se connecte ,on les inscrits 
                    //dans un fichier text dont le contenu sera supprime apres deconnection de l'utilisateur
                    StreamWriter write = new StreamWriter(updateCreateDirectory("JOVIA_SEVEUR") + @"\parametresUser2.par", false);
                    write.WriteLine("{0};{1};{2};{3}", tbValue[0], tbValue[1], tbValue[2], tbValue[3]);
                    write.Close();
                }
                else
                {
                    tbValue[0] = "";
                    tbValue[1] = "";
                    tbValue[2] = "";
                    tbValue[3] = "";
                }

                dr.Close();
                cmdVeriLoginUI.Dispose();
                con.Close();
            }

            return tbValue;
        }

        /// <summary>
        /// Permet de vider le fichier contenant les paramètres de l'utilsateur connecté
        /// </summary>
        public void EmptyFileParametersUserDistante()
        {
            StreamWriter write = new StreamWriter(updateCreateDirectory("JOVIA_SEVEUR") + @"\parametresUser2.par", false);
            write.WriteLine("{0}", "");
            write.Close();
        }

        /// <summary>
        /// Permet de lire le fichier contenant les paramètres de l'utilisateur connecté 
        /// pour une quelconque fin (l'Id de la catégorie utilisateur, la désignation
        /// de la catégorie, le niveau de l'utilisateur ainsi que son Id en tant que personne)
        /// </summary>
        /// <returns>Tableau des string</returns>
        public string[] ReadFileParametersUserDistante()
        {
            string[] tbValues = new string[4];
            StreamReader read = new StreamReader(updateCreateDirectory("JOVIA_SEVEUR") + @"\parametresUser2.par");
            if (!read.EndOfStream)
            {
                string lectureLigne = read.ReadLine();
                string[] tbRecupValues = lectureLigne.Split(new char[] { ';' });
                tbValues[0] = tbRecupValues[0];
                tbValues[1] = tbRecupValues[1];
                tbValues[2] = tbRecupValues[2];
                tbValues[3] = tbRecupValues[3];
                read.Close();
            }
            return tbValues;
        }
        #endregion

        #region Execution de l'enregistrement quelque soit la classe appellante
        /// <summary>
        /// Permet d'enregistrer un item dans la base de données quelque soit le type d'objet
        /// </summary>
        /// <param name="obj">Object</param>
        internal void Save(object obj)
        {
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdSave = null,cmdSave1 = null;
            //IDbTransaction transaction = null;
            bool okSave = false;

            if (obj is Province)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = "insert into province(id,designation,superficie) values(@id,@designation,@superficie)";
                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((Province)obj).Id;
                IDataParameter paramDesi = cmdSave.CreateParameter();
                paramDesi.ParameterName = "@designation";
                paramDesi.Value = ((Province)obj).Designation;
                IDataParameter paramSuperficie = cmdSave.CreateParameter();
                paramSuperficie.ParameterName = "@superficie";
                paramSuperficie.Value = ((Province)obj).Superficie;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramDesi);
                cmdSave.Parameters.Add(paramSuperficie);
            }
            else if (obj is Serveur)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = @"insert into serveur(id,id_province,designation,adresse_ip)
                    values(@id,@id_province,@designation,@adresse_ip)";
                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((Serveur)obj).Id;
                IDataParameter paramIdProv = cmdSave.CreateParameter();
                paramIdProv.ParameterName = "@id_province";
                paramIdProv.Value = ((Serveur)obj).Id_province;
                IDataParameter paramDesi = cmdSave.CreateParameter();
                paramDesi.ParameterName = "@designation";
                paramDesi.Value = ((Serveur)obj).Designation;
                IDataParameter paramIPAdresse = cmdSave.CreateParameter();
                paramIPAdresse.ParameterName = "@adresse_ip";
                paramIPAdresse.Value = ((Serveur)obj).Adresse_ip;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramIdProv);
                cmdSave.Parameters.Add(paramDesi);
                cmdSave.Parameters.Add(paramIPAdresse);                
            }
            else if (obj is VilleTeritoireServeur)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = @"insert into villeTerritoire(id,id_province,designation,superficie)
                    values(@id,@id_province,@designation,@superficie)";
                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((VilleTeritoireServeur)obj).Id;
                IDataParameter paramIdProv = cmdSave.CreateParameter();
                paramIdProv.ParameterName = "@id_province";
                paramIdProv.Value = ((VilleTeritoireServeur)obj).Id_pronvince;
                IDataParameter paramDesi = cmdSave.CreateParameter();
                paramDesi.ParameterName = "@designation";
                paramDesi.Value = ((VilleTeritoireServeur)obj).Designation;
                IDataParameter paramSuperficie = cmdSave.CreateParameter();
                paramSuperficie.ParameterName = "@superficie";
                paramSuperficie.Value = ((VilleTeritoireServeur)obj).Superficie;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramIdProv);
                cmdSave.Parameters.Add(paramDesi);
                cmdSave.Parameters.Add(paramSuperficie);
            }
            else if (obj is CommuneChefferieSecteurServeur)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = @"insert into communeChefferieSecteur(id,id_villeTerritoire,designation,superficie) 
                values(@id,@id_villeTerritoire,@designation,@superficie)";
                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((CommuneChefferieSecteurServeur)obj).Id;
                IDataParameter paramId_villeTerritoire = cmdSave.CreateParameter();
                paramId_villeTerritoire.ParameterName = "@id_villeTerritoire";
                paramId_villeTerritoire.Value = ((CommuneChefferieSecteurServeur)obj).Id_VilleTeritoire;
                IDataParameter paramDesignation = cmdSave.CreateParameter();
                paramDesignation.ParameterName = "@designation";
                paramDesignation.Value = ((CommuneChefferieSecteurServeur)obj).Designation;
                IDataParameter paramSuperficie = cmdSave.CreateParameter();
                paramSuperficie.ParameterName = "@superficie";
                paramSuperficie.Value = ((CommuneChefferieSecteurServeur)obj).Superficie;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramId_villeTerritoire);
                cmdSave.Parameters.Add(paramDesignation);
                cmdSave.Parameters.Add(paramSuperficie);
            }
            else if (obj is QuartierLocaliteServeur)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = @"insert into quartierLocalite(id,id_communeChefferieSecteur,designation,superficie) 
                    values(@id,@id_communeChefferieSecteur,@designation,@superficie)";
                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((QuartierLocaliteServeur)obj).Id;
                IDataParameter paramId_communeChefferieSecteur = cmdSave.CreateParameter();
                paramId_communeChefferieSecteur.ParameterName = "@id_communeChefferieSecteur";
                paramId_communeChefferieSecteur.Value = ((QuartierLocaliteServeur)obj).Id_communeChefferieSecteur;
                IDataParameter paramDesignation = cmdSave.CreateParameter();
                paramDesignation.ParameterName = "@designation";
                paramDesignation.Value = ((QuartierLocaliteServeur)obj).Designation;
                IDataParameter paramSuperficie = cmdSave.CreateParameter();
                paramSuperficie.ParameterName = "@superficie";
                paramSuperficie.Value = ((QuartierLocaliteServeur)obj).Superficie;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramId_communeChefferieSecteur);
                cmdSave.Parameters.Add(paramDesignation);
                cmdSave.Parameters.Add(paramSuperficie);
            }
            else if (obj is PersonneServeur)
            {
                if (((PersonneServeur)obj).Id_pere == null && ((PersonneServeur)obj).Id_mere == null) { okSave= true; }
                else if (((PersonneServeur)obj).Id_pere == ((PersonneServeur)obj).Id_mere) throw new Exception("Le père ne peut à la fois être la mère");
                else if (((PersonneServeur)obj).Id_mere == ((PersonneServeur)obj).Id_pere) throw new Exception("La mère ne peut à la fois être le père");
                else
                {
                    okSave = true;
                }
                if (okSave)
                {
                    ////Pour la version serveur on insere une personne qu'a partir du PC et de ce fait en l'inserant on insere aussi sa 
                    ////photo et cela se fera dans une transaction
                    //transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);

                    cmdSave1 = con.CreateCommand();
                    cmdSave1.CommandText = @"insert into personne(id,id_avenueVillage,nom,postnom,prenom,id_pere,id_mere,sexe,etativil,travail,numeroNational,numero,nombreEnfant,niveauEtude,datenaissance,datedeces,anneeSaved)
                    values(@id,@id_avenueVillage,@nom,@postnom,@prenom,@id_pere,@id_mere,@sexe,@etativil,@travail,@numeroNational,@numero,@nombreEnfant,@niveauEtude,@datenaissance,@datedeces,@anneeSaved)";

                    IDataParameter paramId = cmdSave1.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = ((PersonneServeur)obj).Id;
                    IDataParameter paramId_avenueVillage = cmdSave1.CreateParameter();
                    paramId_avenueVillage.ParameterName = "@id_avenueVillage";
                    paramId_avenueVillage.Value = ((PersonneServeur)obj).Id_avenueVillage;
                    IDataParameter paramNom = cmdSave1.CreateParameter();
                    paramNom.ParameterName = "@nom";
                    paramNom.Value = ((PersonneServeur)obj).Nom;
                    IDataParameter paramPostNom = cmdSave1.CreateParameter();
                    paramPostNom.ParameterName = "@postnom";
                    paramPostNom.Value = ((PersonneServeur)obj).Postnom;
                    IDataParameter paramPrenom = cmdSave1.CreateParameter();
                    paramPrenom.ParameterName = "@prenom";
                    paramPrenom.Value = ((PersonneServeur)obj).Prenom;
                    IDataParameter paramId_pere = cmdSave1.CreateParameter();
                    paramId_pere.ParameterName = "@id_pere";
                    paramId_pere.Value = ((PersonneServeur)obj).Id_pere;
                    IDataParameter paramId_mere = cmdSave1.CreateParameter();
                    paramId_mere.ParameterName = "@id_mere";
                    paramId_mere.Value = ((PersonneServeur)obj).Id_mere;
                    IDataParameter paramSexe = cmdSave1.CreateParameter();
                    paramSexe.ParameterName = "@sexe";
                    paramSexe.Value = ((PersonneServeur)obj).Sexe;
                    IDataParameter paramEtatCivil = cmdSave1.CreateParameter();
                    paramEtatCivil.ParameterName = "@etativil";
                    paramEtatCivil.Value = ((PersonneServeur)obj).EtatCivile;
                    IDataParameter paramTravail = cmdSave1.CreateParameter();
                    paramTravail.ParameterName = "@travail";
                    paramTravail.Value = ((PersonneServeur)obj).Travail;
                    IDataParameter paramNumNat = cmdSave1.CreateParameter();
                    paramNumNat.ParameterName = "@numeroNational";
                    paramNumNat.Value = ((PersonneServeur)obj).NumeroNational;
                    IDataParameter paramNumero = cmdSave1.CreateParameter();
                    paramNumero.ParameterName = "@numero";
                    paramNumero.Value = ((PersonneServeur)obj).Numero;
                    IDataParameter paramNbrEnf = cmdSave1.CreateParameter();
                    paramNbrEnf.ParameterName = "@nombreEnfant";
                    paramNbrEnf.Value = ((PersonneServeur)obj).NombreEnfant;
                    IDataParameter paramNiveauEtude = cmdSave1.CreateParameter();
                    paramNiveauEtude.ParameterName = "@niveauEtude";
                    paramNiveauEtude.Value = ((PersonneServeur)obj).Niveau;
                    IDataParameter paramDateNaiss = cmdSave1.CreateParameter();
                    paramDateNaiss.ParameterName = "@datenaissance";
                    paramDateNaiss.Value = ((PersonneServeur)obj).Datenaissance;
                    IDataParameter paramDateDeces = cmdSave1.CreateParameter();
                    paramDateDeces.ParameterName = "@datedeces";
                    paramDateDeces.Value = ((PersonneServeur)obj).Datedeces;
                    IDataParameter paramAnneeSaved = cmdSave1.CreateParameter();
                    paramAnneeSaved.ParameterName = "@anneeSaved";
                    paramAnneeSaved.Value = ((PersonneServeur)obj).AnneeSaved;

                    cmdSave1.Parameters.Add(paramId);
                    cmdSave1.Parameters.Add(paramId_avenueVillage);
                    cmdSave1.Parameters.Add(paramNom);
                    cmdSave1.Parameters.Add(paramPostNom);
                    cmdSave1.Parameters.Add(paramPrenom);
                    cmdSave1.Parameters.Add(paramId_pere);
                    cmdSave1.Parameters.Add(paramId_mere);
                    cmdSave1.Parameters.Add(paramSexe);
                    cmdSave1.Parameters.Add(paramEtatCivil);
                    cmdSave1.Parameters.Add(paramTravail);
                    cmdSave1.Parameters.Add(paramNumNat);
                    cmdSave1.Parameters.Add(paramNumero);
                    cmdSave1.Parameters.Add(paramNbrEnf);
                    cmdSave1.Parameters.Add(paramNiveauEtude);
                    cmdSave1.Parameters.Add(paramDateNaiss);
                    cmdSave1.Parameters.Add(paramDateDeces);
                    cmdSave1.Parameters.Add(paramAnneeSaved);
                    cmdSave1.ExecuteNonQuery();
                    cmdSave1.Dispose();
                } 
            }
            else if (obj is PhotoServeur)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = "insert into photo(id,id_personne,photo) values(@id,@id_personne,@photo)";

                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((PhotoServeur)obj).Id;
                IDataParameter paramId_personne = cmdSave.CreateParameter();
                paramId_personne.ParameterName = "@id_personne";
                paramId_personne.Value = ((PhotoServeur)obj).Id_personne;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramId_personne);

                if ((((PhotoServeur)obj).PhotoPersonne) == null)
                {
                    IDataParameter paramPhoto = cmdSave.CreateParameter();
                    paramPhoto.ParameterName = "@photo";
                    paramPhoto.Value = null;
                    cmdSave.Parameters.Add(paramPhoto);
                }
                else
                {
                    IDataParameter paramPhoto = cmdSave.CreateParameter();
                    paramPhoto.ParameterName = "@photo";
                    paramPhoto.Value = GetImage(((PhotoServeur)obj).PhotoPersonne);
                    cmdSave.Parameters.Add(paramPhoto);
                }
            }
            else if (obj is AvenueVillageServeur)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = @"insert into avenueVillage(id,id_quartierLocalite,designation) 
                values(@id,@id_quartierLocalite,@designation)";
                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((AvenueVillageServeur)obj).Id;
                IDataParameter paramId_quartierLocalite = cmdSave.CreateParameter();
                paramId_quartierLocalite.ParameterName = "@id_quartierLocalite";
                paramId_quartierLocalite.Value = ((AvenueVillageServeur)obj).Id_quartierLocalite;
                IDataParameter paramDesignation = cmdSave.CreateParameter();
                paramDesignation.ParameterName = "@designation";
                paramDesignation.Value = ((AvenueVillageServeur)obj).Designation;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramId_quartierLocalite);
                cmdSave.Parameters.Add(paramDesignation);
            }
            else if (obj is CarteServeur)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = "insert into carte(id,id_personne,datelivraison) values(@id,@id_personne,@datelivraison)";
                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((CarteServeur)obj).Id;
                IDataParameter paramId_personne = cmdSave.CreateParameter();
                paramId_personne.ParameterName = "@id_personne";
                paramId_personne.Value = ((CarteServeur)obj).Id_personne;
                IDataParameter paramDatelivraison = cmdSave.CreateParameter();
                paramDatelivraison.ParameterName = "@datelivraison";
                paramDatelivraison.Value = ((CarteServeur)obj).Datelivraison;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramId_personne);
                cmdSave.Parameters.Add(paramDatelivraison);
            }
            else if (obj is UtilisateurServeur)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = @"insert into utilisateur(id,id_personne,id_categorieUtilisateur,activation,nomuser,motpass)
                values(@id,@id_personne,@id_categorieUtilisateur,@nomuser,@motpass)";

                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((UtilisateurServeur)obj).Id;
                IDataParameter paramId_personne = cmdSave.CreateParameter();
                paramId_personne.ParameterName = "@id_personne";
                paramId_personne.Value = ((UtilisateurServeur)obj).Id_personne;
                IDataParameter paramId_categorieUtilisateur = cmdSave.CreateParameter();
                paramId_categorieUtilisateur.ParameterName = "@id_categorieUtilisateur";
                paramId_categorieUtilisateur.Value = ((UtilisateurServeur)obj).Id_categorieUtilisateur;
                IDataParameter paramActivation = cmdSave.CreateParameter();
                paramActivation.ParameterName = "@activation";
                paramActivation.Value = ((UtilisateurServeur)obj).Activation;
                IDataParameter paramNomuser = cmdSave.CreateParameter();
                paramNomuser.ParameterName = "@nomuser";
                paramNomuser.Value = ((UtilisateurServeur)obj).Nomuser;
                IDataParameter paramMotpass = cmdSave.CreateParameter();
                paramMotpass.ParameterName = "@motpass";
                paramMotpass.Value = ((UtilisateurServeur)obj).Motpass;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramId_personne);
                cmdSave.Parameters.Add(paramId_categorieUtilisateur);
                cmdSave.Parameters.Add(paramActivation);
                cmdSave.Parameters.Add(paramNomuser);
                cmdSave.Parameters.Add(paramMotpass);
            }
            else if (obj is CategorieUtilisateurServeur)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = "insert into categorieUtilisateur(id,designation,groupe) values(@id,@designation,@groupe)";

                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((CategorieUtilisateurServeur)obj).Id;
                IDataParameter paramDesignation = cmdSave.CreateParameter();
                paramDesignation.ParameterName = "@designation";
                paramDesignation.Value = ((CategorieUtilisateurServeur)obj).Designation;
                IDataParameter paramGroupe = cmdSave.CreateParameter();
                paramGroupe.ParameterName = "@groupe";
                paramGroupe.Value = ((CategorieUtilisateurServeur)obj).Groupe;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramDesignation);
                cmdSave.Parameters.Add(paramGroupe);
            }
            else if (obj is TelephoneServeur)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = "insert into telephone(id,numero,id_utilisateur) values(@id,@numero,@id_utilisateur)";

                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((TelephoneServeur)obj).Id;
                IDataParameter paramNumero = cmdSave.CreateParameter();
                paramNumero.ParameterName = "@numero";
                paramNumero.Value = ((TelephoneServeur)obj).Numero;
                IDataParameter paramId_utilisateur = cmdSave.CreateParameter();
                paramId_utilisateur.ParameterName = "@id_utilisateur";
                paramId_utilisateur.Value = ((TelephoneServeur)obj).Id_utilisateur;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramNumero);
                cmdSave.Parameters.Add(paramId_utilisateur);
            }

            if (!okSave) 
            {
                cmdSave.ExecuteNonQuery();
                cmdSave.Dispose();
            }
            con.Close();
        }
        #endregion

        #region Execution de la modification quelque soit la classe appellante
        /// <summary>
        /// Permet de modifier un item dans la base de données quelque soit le type d'objet
        /// </summary>
        /// <param name="obj"></param>
        internal void Update(object obj)
        {
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdUpdate = null;
            bool ok = false, okUpdate = false;

            if (obj is Province)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = "update province set designation=@designation,superficie=@superficie where id=@id";

                IDataParameter paramDesi = cmdUpdate.CreateParameter();
                paramDesi.ParameterName = "@designation";
                paramDesi.Value = ((Province)obj).Designation;
                IDataParameter paramSuperficie = cmdUpdate.CreateParameter();
                paramSuperficie.ParameterName = "@superficie";
                paramSuperficie.Value = ((Province)obj).Superficie;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((Province)obj).Id;
             
                cmdUpdate.Parameters.Add(paramDesi);
                cmdUpdate.Parameters.Add(paramSuperficie);
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is Serveur)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = "update serveur set id_province=@id_province,designation=@designation,adresse_ip=@adresse_ip where id=@id";

                IDataParameter paramIdProv = cmdUpdate.CreateParameter();
                paramIdProv.ParameterName = "@id_province";
                paramIdProv.Value = ((Serveur)obj).Id_province;
                IDataParameter paramDesi = cmdUpdate.CreateParameter();
                paramDesi.ParameterName = "@designation";
                paramDesi.Value = ((Serveur)obj).Designation;
                IDataParameter paramIPAdresse = cmdUpdate.CreateParameter();
                paramIPAdresse.ParameterName = "@adresse_ip";
                paramIPAdresse.Value = ((Serveur)obj).Adresse_ip;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((Serveur)obj).Id;
                
                cmdUpdate.Parameters.Add(paramIdProv);
                cmdUpdate.Parameters.Add(paramDesi);
                cmdUpdate.Parameters.Add(paramIPAdresse);
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is VilleTeritoireServeur)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = "update villeTerritoire set id_province=@id_province,designation=@designation,superficie=@superficie where id=@id";

                IDataParameter paramIdProv = cmdUpdate.CreateParameter();
                paramIdProv.ParameterName = "@id_province";
                paramIdProv.Value = ((VilleTeritoireServeur)obj).Id_pronvince;
                IDataParameter paramDesi = cmdUpdate.CreateParameter();
                paramDesi.ParameterName = "@designation";
                paramDesi.Value = ((VilleTeritoireServeur)obj).Designation;
                IDataParameter paramSuperficie = cmdUpdate.CreateParameter();
                paramSuperficie.ParameterName = "@superficie";
                paramSuperficie.Value = ((VilleTeritoireServeur)obj).Superficie;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((VilleTeritoireServeur)obj).Id;
                
                cmdUpdate.Parameters.Add(paramIdProv);
                cmdUpdate.Parameters.Add(paramDesi);
                cmdUpdate.Parameters.Add(paramSuperficie);
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is CommuneChefferieSecteurServeur)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = "update communeChefferieSecteur set id_villeTerritoire=@id_villeTerritoire,designation=@designation,superficie=@superficie where id=@id";

                IDataParameter paramId_villeTerritoire = cmdUpdate.CreateParameter();
                paramId_villeTerritoire.ParameterName = "@id_villeTerritoire";
                paramId_villeTerritoire.Value = ((CommuneChefferieSecteurServeur)obj).Id_VilleTeritoire;
                IDataParameter paramDesignation = cmdUpdate.CreateParameter();
                paramDesignation.ParameterName = "@designation";
                paramDesignation.Value = ((CommuneChefferieSecteurServeur)obj).Designation;
                IDataParameter paramSuperficie = cmdUpdate.CreateParameter();
                paramSuperficie.ParameterName = "@superficie";
                paramSuperficie.Value = ((CommuneChefferieSecteurServeur)obj).Superficie;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((CommuneChefferieSecteurServeur)obj).Id;
                
                cmdUpdate.Parameters.Add(paramId_villeTerritoire);
                cmdUpdate.Parameters.Add(paramDesignation);
                cmdUpdate.Parameters.Add(paramSuperficie);
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is QuartierLocaliteServeur)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = "update quartierLocalite set id_communeChefferieSecteur=@id_communeChefferieSecteur,designation=@designation,superficie=@superficie where id=@id";

                IDataParameter paramId_communeChefferieSecteur = cmdUpdate.CreateParameter();
                paramId_communeChefferieSecteur.ParameterName = "@id_communeChefferieSecteur";
                paramId_communeChefferieSecteur.Value = ((QuartierLocaliteServeur)obj).Id_communeChefferieSecteur;
                IDataParameter paramDesignation = cmdUpdate.CreateParameter();
                paramDesignation.ParameterName = "@designation";
                paramDesignation.Value = ((QuartierLocaliteServeur)obj).Designation;
                IDataParameter paramSuperficie = cmdUpdate.CreateParameter();
                paramSuperficie.ParameterName = "@superficie";
                paramSuperficie.Value = ((QuartierLocaliteServeur)obj).Superficie;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((QuartierLocaliteServeur)obj).Id;
                
                cmdUpdate.Parameters.Add(paramId_communeChefferieSecteur);
                cmdUpdate.Parameters.Add(paramDesignation);
                cmdUpdate.Parameters.Add(paramSuperficie);
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is PersonneServeur)
            {
                if (((PersonneServeur)obj).Id_pere == null && ((PersonneServeur)obj).Id_mere == null) { okUpdate = true; }
                else if (((PersonneServeur)obj).Id_pere == ((PersonneServeur)obj).Id_mere) throw new Exception("Le père ne peut à la fois être la mère");
                else if (((PersonneServeur)obj).Id_mere == ((PersonneServeur)obj).Id_pere) throw new Exception("La mère ne peut à la fois être le père");
                else
                {
                    okUpdate = true;
                }
                if (okUpdate)
                {
                    cmdUpdate = con.CreateCommand();
                    cmdUpdate.CommandText = @"update personne set id_avenueVillage=@id_avenueVillage,nom=@nom,postnom=@postnom,prenom=@prenom,id_pere=@id_pere,id_mere=@id_mere,
                    sexe=@sexe,etativil=@etativil,travail=@travail,numero=@numero,nombreEnfant=@nombreEnfant,niveauEtude=@niveauEtude,
                    datenaissance=@datenaissance,datedeces=@datedeces where id=@id";

                    IDataParameter paramId_avenueVillage = cmdUpdate.CreateParameter();
                    paramId_avenueVillage.ParameterName = "@id_avenueVillage";
                    paramId_avenueVillage.Value = ((PersonneServeur)obj).Id_avenueVillage;
                    IDataParameter paramNom = cmdUpdate.CreateParameter();
                    paramNom.ParameterName = "@nom";
                    paramNom.Value = ((PersonneServeur)obj).Nom;
                    IDataParameter paramPostNom = cmdUpdate.CreateParameter();
                    paramPostNom.ParameterName = "@postnom";
                    paramPostNom.Value = ((PersonneServeur)obj).Postnom;
                    IDataParameter paramPrenom = cmdUpdate.CreateParameter();
                    paramPrenom.ParameterName = "@prenom";
                    paramPrenom.Value = ((PersonneServeur)obj).Prenom;
                    IDataParameter paramId_pere = cmdUpdate.CreateParameter();
                    paramId_pere.ParameterName = "@id_pere";
                    paramId_pere.Value = ((PersonneServeur)obj).Id_pere;
                    IDataParameter paramId_mere = cmdUpdate.CreateParameter();
                    paramId_mere.ParameterName = "@id_mere";
                    paramId_mere.Value = ((PersonneServeur)obj).Id_mere;
                    IDataParameter paramSexe = cmdUpdate.CreateParameter();
                    paramSexe.ParameterName = "@sexe";
                    paramSexe.Value = ((PersonneServeur)obj).Sexe;
                    IDataParameter paramEtatCivil = cmdUpdate.CreateParameter();
                    paramEtatCivil.ParameterName = "@etativil";
                    paramEtatCivil.Value = ((PersonneServeur)obj).EtatCivile;
                    IDataParameter paramTravail = cmdUpdate.CreateParameter();
                    paramTravail.ParameterName = "@travail";
                    paramTravail.Value = ((PersonneServeur)obj).Travail;
                    IDataParameter paramNumero = cmdUpdate.CreateParameter();
                    paramNumero.ParameterName = "@numero";
                    paramNumero.Value = ((PersonneServeur)obj).Numero;
                    IDataParameter paramNbrEnf = cmdUpdate.CreateParameter();
                    paramNbrEnf.ParameterName = "@nombreEnfant";
                    paramNbrEnf.Value = ((PersonneServeur)obj).NombreEnfant;
                    IDataParameter paramNiveauEtude = cmdUpdate.CreateParameter();
                    paramNiveauEtude.ParameterName = "@niveauEtude";
                    paramNiveauEtude.Value = ((PersonneServeur)obj).Niveau;
                    IDataParameter paramDateNaiss = cmdUpdate.CreateParameter();
                    paramDateNaiss.ParameterName = "@datenaissance";
                    paramDateNaiss.Value = ((PersonneServeur)obj).Datenaissance;
                    IDataParameter paramDateDeces = cmdUpdate.CreateParameter();
                    paramDateDeces.ParameterName = "@datedeces";
                    paramDateDeces.Value = ((PersonneServeur)obj).Datedeces;
                    IDataParameter paramId = cmdUpdate.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = ((PersonneServeur)obj).Id;
                    
                    cmdUpdate.Parameters.Add(paramId_avenueVillage);
                    cmdUpdate.Parameters.Add(paramNom);
                    cmdUpdate.Parameters.Add(paramPostNom);
                    cmdUpdate.Parameters.Add(paramPrenom);
                    cmdUpdate.Parameters.Add(paramId_pere);
                    cmdUpdate.Parameters.Add(paramId_mere);
                    cmdUpdate.Parameters.Add(paramSexe);
                    cmdUpdate.Parameters.Add(paramEtatCivil);
                    cmdUpdate.Parameters.Add(paramTravail);
                    cmdUpdate.Parameters.Add(paramNumero);
                    cmdUpdate.Parameters.Add(paramNbrEnf);
                    cmdUpdate.Parameters.Add(paramNiveauEtude);
                    cmdUpdate.Parameters.Add(paramDateNaiss);
                    cmdUpdate.Parameters.Add(paramDateDeces);
                    cmdUpdate.Parameters.Add(paramId);
                }
            }
            else if (obj is PhotoServeur)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = "update photo set id_personne=@id_personne,photo=@photo where id=@id";

                IDataParameter paramId_personne = cmdUpdate.CreateParameter();
                paramId_personne.ParameterName = "@id_personne";
                paramId_personne.Value = ((PhotoServeur)obj).Id_personne;

                cmdUpdate.Parameters.Add(paramId_personne);

                if ((((PhotoServeur)obj).PhotoPersonne) == null)
                {
                    IDataParameter paramPhoto = cmdUpdate.CreateParameter();
                    paramPhoto.ParameterName = "@photo";
                    paramPhoto.Value = null;
                    cmdUpdate.Parameters.Add(paramPhoto);
                }
                else
                {
                    IDataParameter paramPhoto = cmdUpdate.CreateParameter();
                    paramPhoto.ParameterName = "@photo";
                    paramPhoto.Value = GetImage(((PhotoServeur)obj).PhotoPersonne);
                    cmdUpdate.Parameters.Add(paramPhoto);
                }
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((PhotoServeur)obj).Id;
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is AvenueVillageServeur)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = @"update avenueVillage set id_quartierLocalite=@id_quartierLocalite,designation=@designation
                where id=@id";

                IDataParameter paramId_quartierLocalite = cmdUpdate.CreateParameter();
                paramId_quartierLocalite.ParameterName = "@id_quartierLocalite";
                paramId_quartierLocalite.Value = ((AvenueVillageServeur)obj).Id_quartierLocalite;
                IDataParameter paramDesignation = cmdUpdate.CreateParameter();
                paramDesignation.ParameterName = "@designation";
                paramDesignation.Value = ((AvenueVillageServeur)obj).Designation;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((AvenueVillageServeur)obj).Id;
                
                cmdUpdate.Parameters.Add(paramId_quartierLocalite);
                cmdUpdate.Parameters.Add(paramDesignation);
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is UtilisateurServeur)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = @"update utilisateur set id_personne=@id_personne,
                id_categorieUtilisateur=@id_categorieUtilisateur,activation=@activation,nomuser=@nomuser where id=@id";

                IDataParameter paramId_personne = cmdUpdate.CreateParameter();
                paramId_personne.ParameterName = "@id_personne";
                paramId_personne.Value = ((UtilisateurServeur)obj).Id_personne;
                IDataParameter paramId_categorieUtilisateur = cmdUpdate.CreateParameter();
                paramId_categorieUtilisateur.ParameterName = "@id_categorieUtilisateur";
                paramId_categorieUtilisateur.Value = ((UtilisateurServeur)obj).Id_categorieUtilisateur;
                IDataParameter paramActivation = cmdUpdate.CreateParameter();
                paramActivation.ParameterName = "@activation";
                paramActivation.Value = ((UtilisateurServeur)obj).Activation;
                IDataParameter paramNomuser = cmdUpdate.CreateParameter();
                paramNomuser.ParameterName = "@nomuser";
                paramNomuser.Value = ((UtilisateurServeur)obj).Nomuser;
                //IDataParameter paramMotpass = cmdUpdate.CreateParameter();
                //paramMotpass.ParameterName = "@motpass";
                //paramMotpass.Value = ((UtilisateurServeur)obj).Motpass;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((UtilisateurServeur)obj).Id;

                cmdUpdate.Parameters.Add(paramId_personne);
                cmdUpdate.Parameters.Add(paramId_categorieUtilisateur);
                cmdUpdate.Parameters.Add(paramActivation);
                cmdUpdate.Parameters.Add(paramNomuser);
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is CarteServeur)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = "update carte set id_personne=@id_personne,datelivraison=@datelivraison where id=@id";

                IDataParameter paramId_personne = cmdUpdate.CreateParameter();
                paramId_personne.ParameterName = "@id_personne";
                paramId_personne.Value = ((CarteServeur)obj).Id_personne;
                IDataParameter paramDatelivraison = cmdUpdate.CreateParameter();
                paramDatelivraison.ParameterName = "@datelivraison";
                paramDatelivraison.Value = ((CarteServeur)obj).Datelivraison;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((CarteServeur)obj).Id;
                
                cmdUpdate.Parameters.Add(paramId_personne);
                cmdUpdate.Parameters.Add(paramDatelivraison);
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is CategorieUtilisateurServeur)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = "update categorieUtilisateur set designation=@designation,groupe=@groupe where id=@id";

                IDataParameter paramDesignation = cmdUpdate.CreateParameter();
                paramDesignation.ParameterName = "@designation";
                paramDesignation.Value = ((CategorieUtilisateurServeur)obj).Designation;
                IDataParameter paramGroupe = cmdUpdate.CreateParameter();
                paramGroupe.ParameterName = "@groupe";
                paramGroupe.Value = ((CategorieUtilisateurServeur)obj).Groupe;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((CategorieUtilisateurServeur)obj).Id;
                
                cmdUpdate.Parameters.Add(paramDesignation);
                cmdUpdate.Parameters.Add(paramGroupe);
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is TelephoneServeur)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = "update telephone set id_utilisateur=@id_utilisateur,numero=@numero where id=@id";

                IDataParameter paramNumero = cmdUpdate.CreateParameter();
                paramNumero.ParameterName = "@numero";
                paramNumero.Value = ((TelephoneServeur)obj).Numero;
                IDataParameter paramId_utilisateur = cmdUpdate.CreateParameter();
                paramId_utilisateur.ParameterName = "@id_utilisateur";
                paramId_utilisateur.Value = ((TelephoneServeur)obj).Id_utilisateur;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((TelephoneServeur)obj).Id;
                
                cmdUpdate.Parameters.Add(paramNumero);
                cmdUpdate.Parameters.Add(paramId_utilisateur);
                cmdUpdate.Parameters.Add(paramId);
            }
            if (!ok || okUpdate)
            {
                int valueUpdate = cmdUpdate.ExecuteNonQuery();
                cmdUpdate.Dispose();
                if (valueUpdate == 0) throw new Exception("rassurez vous que cet enregistrement existe réellement et réessayez svp");
            }
            
            con.Close(); 
        }
        #endregion

        #region Execution de la suppression quelque soit la classe appellante
        /// <summary>
        /// Permet de supprimer un item dans la base de données quelque soit le type d'objet
        /// </summary>
        /// <param name="obj">Object</param>
        internal void Delete(object obj)
        {
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdDelete = null;

            if (obj is Province)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from province where id=@id";
                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((Province)obj).Id;
                cmdDelete.Parameters.Add(paramId);
            }
            else if (obj is Serveur)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from serveur where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((Serveur)obj).Id;
                cmdDelete.Parameters.Add(paramId);
            }
            else if (obj is VilleTeritoireServeur)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from villeTerritoire where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((VilleTeritoireServeur)obj).Id;
                cmdDelete.Parameters.Add(paramId);
            }
            else if (obj is CommuneChefferieSecteurServeur)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from communeChefferieSecteur where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((CommuneChefferieSecteurServeur)obj).Id;
                cmdDelete.Parameters.Add(paramId);
            }
            else if (obj is QuartierLocaliteServeur)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from quartierLocalite where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((QuartierLocaliteServeur)obj).Id;
                cmdDelete.Parameters.Add(paramId);
            }
            //else if (obj is Personne)
            //{
            //    cmdDelete  = con.CreateCommand();
            //cmdDelete.CommandText = "delete from personne where id=@id";
            //    cmdDelete.Parameters.Add(new NpgsqlParameter("@id", ((Personne)obj).Id));
            //}
            //else if (obj is Photo)
            //{
            //    cmdDelete  = con.CreateCommand();
            //cmdDelete.CommandText = "delete from photo where id=@id";
            //    cmdDelete.Parameters.Add(new NpgsqlParameter("@id", ((Photo)obj).Id));
            //}
            else if (obj is VilleTeritoireServeur)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from villeTerritoire where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((VilleTeritoireServeur)obj).Id;
                cmdDelete.Parameters.Add(paramId);
            }
            else if (obj is AvenueVillageServeur)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from avenueVillage where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((AvenueVillageServeur)obj).Id;
                cmdDelete.Parameters.Add(paramId);
            }
            else if (obj is UtilisateurServeur)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from utilisateur where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((UtilisateurServeur)obj).Id;
                cmdDelete.Parameters.Add(paramId);
            }
            else if (obj is CategorieUtilisateurServeur)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from categorieUtilisateur where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((CategorieUtilisateurServeur)obj).Id;
                cmdDelete.Parameters.Add(paramId);
            }
            else if (obj is TelephoneServeur)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from telephone where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((TelephoneServeur)obj).Id;
                cmdDelete.Parameters.Add(paramId);
            }
            //else if (obj is Carte)
            //{
            //    cmdDelete  = con.CreateCommand();
            //cmdDelete.CommandText = "delete from carte where id=@id";
            //    cmdDelete.Parameters.Add("@id", ((Carte)obj).Id);
            //}

            cmdDelete.ExecuteNonQuery();
            cmdDelete.Dispose();
            con.Close();
        }
        #endregion
        
        #region Pronvince(Operation sur les provinces)
        /// <summary>
        /// Retourner une province
        /// </summary>
        /// <param name="id">Idntifiant</param>
        /// <returns>Objet Province</returns>
        public Province GetPronvince(int id)
        {
            Province province = null;

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetProvince = con.CreateCommand();
            cmdGetProvince.CommandText = "select * from province where id=@id";

            IDataParameter paramId = cmdGetProvince.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            cmdGetProvince.Parameters.Add(paramId);

            IDataReader dr = cmdGetProvince.ExecuteReader();
            if (dr.Read()) province = getProvince(dr);

            dr.Close();
            cmdGetProvince.Dispose();
            con.Close();
            return province;
        }
        private static Province getProvince(IDataReader dr)
        {
            Province province = new Province();
            province.Id = Convert.ToInt32(dr["id"]);
            province.Designation = dr["designation"].ToString();
            province.Superficie = Convert.ToInt64(dr["superficie"]);
            return province;
        }
        /// <summary>
        /// Retourner toutes les provinces
        /// </summary>
        /// <returns></returns>
        public List<Province> GetProvinces()
        {
            List<Province> list = new List<Province>();

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetProvinces = con.CreateCommand();
            cmdGetProvinces.CommandText = "SELECT * FROM province ORDER BY id ASC";
            IDataReader dr = cmdGetProvinces.ExecuteReader();
            while (dr.Read())
            {
                list.Add(getProvince(dr));
            }
            dr.Close();
            cmdGetProvinces.Dispose();
            con.Close();
            return list;
        }

        /// <summary>
        /// Permet d'éffectuer la recherche d'une province en passant en paramètre sa désignation. Et retourne successivement
        /// L'identifiant de la Province, la dite désignation et sa superficie dans un tableau
        /// </summary>
        /// <param name="designation">Désignation de la Province</param>
        /// <returns>Une Liste des Provinces</returns>
        public List<Province> SearchProvince(string designation)
        {
            List<Province> list = new List<Province>(); 
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdSearchProvince = con.CreateCommand();
            cmdSearchProvince.CommandText = "SELECT id,designation,superficie FROM province WHERE designation LIKE @designation";
            IDataParameter paramDesignation = cmdSearchProvince.CreateParameter();
            paramDesignation.ParameterName = "@designation";
            paramDesignation.Value = designation.ToUpper() + "%";
            cmdSearchProvince.Parameters.Add(paramDesignation);

            IDataReader dr = cmdSearchProvince.ExecuteReader();

            bool ok = false;
            while (dr.Read())
            {
                list.Add(getProvince(dr));
                ok = true;
            }
            if(!ok) throw new Exception("L'élement ' " + designation + " ' recherché n'a pas été trouvé dans la base des données");

            dr.Close();
            cmdSearchProvince.Dispose();
            con.Close();
            return list;
        }
        #endregion

        #region Serveur(Operation sur les Serveur)
        /// <summary>
        /// Permet de retourner un Serveur suivant son identifiant passé en paramètre
        /// </summary>
        /// <param name="id">Indetifinat du Serveur</param>
        /// <returns>Un Serveur</returns>
        public Serveur GetServeur(int id)
        {
            Serveur serveur = null;

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetServeur = con.CreateCommand();
            cmdGetServeur.CommandText = "select * from serveur where id=@id";
            IDataParameter paramId = cmdGetServeur.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            cmdGetServeur.Parameters.Add(paramId);

            IDataReader dr = cmdGetServeur.ExecuteReader();
            if (dr.Read()) serveur = getServeur(dr);

            dr.Close();
            cmdGetServeur.Dispose();
            con.Close();
            return serveur;
        }

        private static Serveur getServeur(IDataReader dr)
        {
            Serveur serveur = new Serveur();
            serveur.Id = Convert.ToInt32(dr["id"]);
            serveur.Id_province = Convert.ToInt32(dr["id_province"]);
            serveur.Designation = Convert.ToString(dr["designation"]);
            serveur.Adresse_ip = Convert.ToString(dr["adresse_ip"]);
            return serveur;
        }
        /// <summary>
        /// Retourner tous les Serveurs
        /// </summary>
        /// <returns></returns>
        public List<Serveur> GetServeurs()
        {
            List<Serveur> list = new List<Serveur>();

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetServeurs = con.CreateCommand();
            cmdGetServeurs.CommandText = "SELECT * FROM serveur ORDER BY id ASC";
            IDataReader dr = cmdGetServeurs.ExecuteReader();
            while (dr.Read())
            {
                list.Add(getServeur(dr));
            }
            dr.Close();
            cmdGetServeurs.Dispose();
            con.Close();
            return list;
        }

        /// <summary>
        /// Retourner toutes les adrese IP des serveurs
        /// </summary>
        /// <returns></returns>
        public List<string> GetServeurIP()
        {
            List<string> list = new List<string>();

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetServeurIP = con.CreateCommand();
            cmdGetServeurIP.CommandText = "SELECT adresse_ip FROM serveur ORDER BY id ASC";
            IDataReader dr = cmdGetServeurIP.ExecuteReader();
            while (dr.Read())
            {
                list.Add(Convert.ToString(dr["adresse_ip"]));
            }
            dr.Close();
            cmdGetServeurIP.Dispose();
            con.Close();
            return list;
        }
        #endregion

        #region villeTerritoire(Operation sur la villeTerritoire)
        /// <summary>
        /// Permet de retourner une Ville ou Territoire suivant son identifiant passé en paramètre
        /// </summary>
        /// <param name="id">Indetifinat de la Ville ou du Territoire</param>
        /// <returns>Une Ville Territoire</returns>
        public VilleTeritoireServeur GetVilleTeritoire(int id)
        {
            VilleTeritoireServeur villeter = null;

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetVilleTeritoire = con.CreateCommand();
            cmdGetVilleTeritoire.CommandText = "select * from villeTerritoire where id=@id";
            IDataParameter paramId = cmdGetVilleTeritoire.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            cmdGetVilleTeritoire.Parameters.Add(paramId);

            IDataReader dr = cmdGetVilleTeritoire.ExecuteReader();
            if (dr.Read()) villeter = getVilleTeritoire(dr);

            dr.Close();
            cmdGetVilleTeritoire.Dispose();
            con.Close();
            return villeter;
        }

        private static VilleTeritoireServeur getVilleTeritoire(IDataReader dr)
        {
            VilleTeritoireServeur villeter = new VilleTeritoireServeur();
            villeter.Id = Convert.ToInt32(dr["id"]);
            villeter.Id_pronvince = Convert.ToInt32(dr["id_province"]);
            villeter.Designation = dr["designation"].ToString();
            villeter.Superficie = Convert.ToInt32(dr["superficie"]);
            return villeter;
        }
        /// <summary>
        /// Retourner toutes les Villes ou Teritoires
        /// </summary>
        /// <returns></returns>
        public List<VilleTeritoireServeur> GetVilleTeritoires()
        {
            List<VilleTeritoireServeur> list = new List<VilleTeritoireServeur>();

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetVilleTers = con.CreateCommand();
            cmdGetVilleTers.CommandText = "SELECT * FROM villeTerritoire ORDER BY id ASC";
            IDataReader dr = cmdGetVilleTers.ExecuteReader();
            while (dr.Read())
            {
                list.Add(getVilleTeritoire(dr));
            }
            dr.Close();
            cmdGetVilleTers.Dispose();
            con.Close();
            return list;
        }

        /// <summary>
        /// Permet d'éffectuer la recherche d'une Ville ou Teritoire en passant en paramètre sa désignation. Et retourne successivement
        /// l'identifiant de la Province, la dite désignation et sa superficie dans un tableau
        /// </summary>
        /// <param name="designation">Désignation de la ville ou du Territoire</param>
        /// <returns>Une Liste des villeTerritoire</returns>
        public List<VilleTeritoireServeur> SearchVilleTerritoire(string designation)
        {
            List<VilleTeritoireServeur> list = new List<VilleTeritoireServeur>();
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();


            IDbCommand cmdSearchvilleTerritoire = con.CreateCommand();
            cmdSearchvilleTerritoire.CommandText = "SELECT id,id_province,designation,superficie FROM villeTerritoire WHERE designation LIKE @designation";
            IDataParameter paramDesignation = cmdSearchvilleTerritoire.CreateParameter();
            paramDesignation.ParameterName = "@designation";
            paramDesignation.Value = designation.ToUpper() + "%";
            cmdSearchvilleTerritoire.Parameters.Add(paramDesignation);

            IDataReader dr = cmdSearchvilleTerritoire.ExecuteReader();

            bool ok = false;
            while (dr.Read())
            {
                list.Add(getVilleTeritoire(dr));
                ok = true;
            }
            if(!ok) throw new Exception("L'élement ' " + designation + " ' recherché n'a pas été trouvé dans la base des données");

            dr.Close();
            cmdSearchvilleTerritoire.Dispose();
            con.Close();
            return list;
        }
        #endregion

        #region CommuneChefferieSecteur(Operation sur CommuneChefferieSecteur)
        /// <summary>
        /// Permet de retourner une Commune ou Secteur ou Chefferie suivant son identifiant passé en paramètre
        /// </summary>
        /// <param name="id">Indetifinat de la Commune ou Secteur ou Chefferie</param>
        /// <returns>Une Commune ou Secteur ou Chefferie</returns>
        public CommuneChefferieSecteurServeur GetCommuneChefferieSecteur(int id)
        {
            CommuneChefferieSecteurServeur commSectCheff = null;

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetCommuneChefferieSecteur = con.CreateCommand();
            cmdGetCommuneChefferieSecteur.CommandText = "select * from communeChefferieSecteur where id=@id";
            IDataParameter paramId = cmdGetCommuneChefferieSecteur.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            cmdGetCommuneChefferieSecteur.Parameters.Add(paramId);

            IDataReader dr = cmdGetCommuneChefferieSecteur.ExecuteReader();
            if (dr.Read()) commSectCheff = getCommuneChefferieSecteur(dr);

            dr.Close();
            cmdGetCommuneChefferieSecteur.Dispose();
            con.Close();
            return commSectCheff;
        }

        private static CommuneChefferieSecteurServeur getCommuneChefferieSecteur(IDataReader dr)
        {
            CommuneChefferieSecteurServeur commCheffeSect = new CommuneChefferieSecteurServeur();
            commCheffeSect.Id = Convert.ToInt32(dr["id"]);
            commCheffeSect.Id_VilleTeritoire = Convert.ToInt32(dr["id_villeTerritoire"]);
            commCheffeSect.Designation = Convert.ToString(dr["designation"]);
            commCheffeSect.Superficie = Convert.ToInt32(dr["superficie"]);
            return commCheffeSect;
        }
        /// <summary>
        /// Retourne tous CommuneChefferieSecteur
        /// </summary>
        /// <returns></returns>
        public List<CommuneChefferieSecteurServeur> GetCommuneChefferieSecteurs()
        {
            List<CommuneChefferieSecteurServeur> list = new List<CommuneChefferieSecteurServeur>();

            //if (con.State.ToString().Equals("Open")) { }
            //else con.Open();
            con.Close();
            con.Open();

            IDbCommand cmdGetConChefSect = con.CreateCommand();
            cmdGetConChefSect.CommandText = "SELECT * FROM communeChefferieSecteur ORDER BY id ASC";
            IDataReader dr = cmdGetConChefSect.ExecuteReader();
            while (dr.Read())
            {
                list.Add(getCommuneChefferieSecteur(dr));
            }

            dr.Close();
            cmdGetConChefSect.Dispose();
            con.Close();
            return list;
        }

        /// <summary>
        /// Permet d'éffectuer la recherche d'une Communem d'une Chefferie ou d'un  Secteur en passant en paramètre sa désignation. Et retourne successivement
        /// l'identifiant de la ville ou territoire, la dite désignation et sa superficie dans une List
        /// </summary>
        /// <param name="designation">Désignation CommuneChefferieSecteur</param>
        /// <returns>List des CommuneChefferieSecteur</returns>
        public List<CommuneChefferieSecteurServeur> SearchCommuneChefferieSecteur(string designation)
        {
            List<CommuneChefferieSecteurServeur> list = new List<CommuneChefferieSecteurServeur>();
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdSearchCommuneChefferieSecteur = con.CreateCommand();
            cmdSearchCommuneChefferieSecteur.CommandText = "SELECT id,id_villeTerritoire,designation,superficie FROM communeChefferieSecteur WHERE designation LIKE @designation";
            IDataParameter paramDesignation = cmdSearchCommuneChefferieSecteur.CreateParameter();
            paramDesignation.ParameterName = "@designation";
            paramDesignation.Value = designation.ToUpper() + "%";
            cmdSearchCommuneChefferieSecteur.Parameters.Add(paramDesignation);

            IDataReader dr = cmdSearchCommuneChefferieSecteur.ExecuteReader();

            bool ok = false;

            while (dr.Read())
            {
                list.Add(getCommuneChefferieSecteur(dr));
                ok = true;
            }
            if (!ok) throw new Exception("L'élement ' " + designation + " ' recherché n'a pas été trouvé dans la base des données");

            dr.Close();
            cmdSearchCommuneChefferieSecteur.Dispose();
            con.Close();
            return list;
        }
        #endregion

        #region QuartierLocalite(operation sur QuartierLocalite)
        /// <summary>
        /// Permet de retourner un Quartier ou une Localité suivant son identifiant passé en paramètre
        /// </summary>
        /// <param name="id">Indetifinat du Quartier ou de la Localité</param>
        /// <returns>un Quartier ou Localité</returns>
        public QuartierLocaliteServeur GetQuartierLocalite(int id)
        {
            QuartierLocaliteServeur quartLoc = null;

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetQuartierLocalite = con.CreateCommand();
            cmdGetQuartierLocalite.CommandText = "select * from quartierLocalite where id=@id";
            IDataParameter paramId = cmdGetQuartierLocalite.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            cmdGetQuartierLocalite.Parameters.Add(paramId);

            IDataReader dr = cmdGetQuartierLocalite.ExecuteReader();
            if (dr.Read()) quartLoc = getQuartierLocalite(dr);

            dr.Close();
            cmdGetQuartierLocalite.Dispose();
            con.Close();
            return quartLoc;
        }

        private static QuartierLocaliteServeur getQuartierLocalite(IDataReader dr)
        {
            QuartierLocaliteServeur quartierLoc = new QuartierLocaliteServeur();
            quartierLoc.Id = Convert.ToInt32(dr["id"]);
            quartierLoc.Id_communeChefferieSecteur = Convert.ToInt32(dr["id_communeChefferieSecteur"]);
            quartierLoc.Designation = Convert.ToString(dr["designation"]);
            quartierLoc.Superficie = Convert.ToInt32(dr["superficie"]);
            return quartierLoc;
        }

        /// <summary>
        /// retourne tous les QuartierLocalite
        /// </summary>
        /// <returns></returns>
        public List<QuartierLocaliteServeur> GetQuartierLocalites()
        {
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            List<QuartierLocaliteServeur> list = new List<QuartierLocaliteServeur>();
            IDbCommand cmdGetQuartLoc = con.CreateCommand();
            cmdGetQuartLoc.CommandText = "select *from quartierLocalite ORDER BY id ASC";
            IDataReader dr = cmdGetQuartLoc.ExecuteReader();
            while (dr.Read())
            {
                list.Add(getQuartierLocalite(dr));
            }
            dr.Close();
            cmdGetQuartLoc.Dispose();
            con.Close();
            return list;
        }

        /// <summary>
        /// Permet d'éffectuer la recherche d'un  Quartier ou d'une Localité en passant en paramètre sa désignation. Et retourne successivement
        /// L'identifiant du Quartierou de la Localite, la dite désignation et sa superficie dans une list
        /// </summary>
        /// <param name="designation">Désignation Quartier ou Localité</param>
        /// <returns>List des  Quartier ou Localité</returns>
        public List<QuartierLocaliteServeur> SearchQuartierLocalite(string designation)
        {
            List<QuartierLocaliteServeur> list = new List<QuartierLocaliteServeur>();
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();


            IDbCommand cmdSearchQuartierLocalite = con.CreateCommand();
            cmdSearchQuartierLocalite.CommandText = "SELECT id,id_communeChefferieSecteur,designation,superficie FROM quartierLocalite WHERE designation LIKE @designation";
            IDataParameter paramDesignation = cmdSearchQuartierLocalite.CreateParameter();
            paramDesignation.ParameterName = "@designation";
            paramDesignation.Value = designation.ToUpper() + "%";
            cmdSearchQuartierLocalite.Parameters.Add(paramDesignation);

            IDataReader dr = cmdSearchQuartierLocalite.ExecuteReader();

            bool ok = false;

            while (dr.Read())
            {
                list.Add(getQuartierLocalite(dr));
                ok = true;
            }
            if (!ok) throw new Exception("L'élement ' " + designation + " ' recherché n'a pas été trouvé dans la base des données");

            dr.Close();
            cmdSearchQuartierLocalite.Dispose();
            con.Close();
            return list;
        }
        #endregion

        #region AvenueVillage(operation sur AvenueVillage)
        /// <summary>
        /// Permet de retourner une Avenue ou un Village suivant son identifiant passé en paramètre
        /// </summary>
        /// <param name="id">Indetifinat de l'Avenue ou du Village</param>
        /// <returns>une Avenueou un Village</returns>
        public AvenueVillageServeur GetAvenueVillage(int id)
        {
            AvenueVillageServeur avenueVill = null;

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetAvenueVillage = con.CreateCommand();
            cmdGetAvenueVillage.CommandText = "select * from avenueVillage where id=@id";
            IDataParameter paramId = cmdGetAvenueVillage.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            cmdGetAvenueVillage.Parameters.Add(paramId);

            IDataReader dr = cmdGetAvenueVillage.ExecuteReader();
            if (dr.Read()) avenueVill = getAvenueVillage(dr);

            dr.Close();
            cmdGetAvenueVillage.Dispose();
            con.Close();
            return avenueVill;
        }

        private static AvenueVillageServeur getAvenueVillage(IDataReader dr)
        {
            AvenueVillageServeur avenueVillage = new AvenueVillageServeur();
            avenueVillage.Id = Convert.ToInt32(dr["id"]);
            avenueVillage.Id_quartierLocalite = Convert.ToInt32(dr["id_quartierLocalite"]);
            avenueVillage.Designation = Convert.ToString(dr["designation"]);
            return avenueVillage;
        }

        /// <summary>
        /// retourne tous les AvenueVillage
        /// </summary>
        /// <returns>Liste des Avenues et villages</returns>
        public List<AvenueVillageServeur> GetAvenueVillages()
        {
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            List<AvenueVillageServeur> list = new List<AvenueVillageServeur>();
            IDbCommand cmdGetAvVil = con.CreateCommand();
            cmdGetAvVil.CommandText = "select *from avenueVillage";
            IDataReader dr = cmdGetAvVil.ExecuteReader();
            while (dr.Read())
            {
                list.Add(getAvenueVillage(dr));
            }
            dr.Close();
            cmdGetAvVil.Dispose();
            con.Close();
            return list;
        }

        /// <summary>
        /// Permet d'éffectuer la recherche d'une Avenue ou d'un village en passant en paramètre sa désignation. Et retourne successivement
        /// l'identifiant de l'Avenue ou du Village, la dite désignation et sa superficie dans une List
        /// </summary>
        /// <param name="designation">Désignation AvenueVillage</param>
        /// <returns>List des AvenueVillage</returns>
        public List<AvenueVillageServeur> SearchAvenueVillage(string designation)
        {
            List<AvenueVillageServeur> list = new List<AvenueVillageServeur>();
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();


            IDbCommand cmdSearchAvenueVillage = con.CreateCommand();
            cmdSearchAvenueVillage.CommandText = "SELECT id,id_quartierLocalite,designation FROM avenueVillage WHERE designation LIKE @designation";
            IDataParameter paramDesignation = cmdSearchAvenueVillage.CreateParameter();
            paramDesignation.ParameterName = "@designation";
            paramDesignation.Value = designation.ToUpper() + "%";
            cmdSearchAvenueVillage.Parameters.Add(paramDesignation);

            IDataReader dr = cmdSearchAvenueVillage.ExecuteReader();

            bool ok = false;
            while (dr.Read())
            {
                list.Add(getAvenueVillage(dr));
                ok = true;
            }
            if (!ok) throw new Exception("L'élement ' " + designation + " ' recherché n'a pas été trouvé dans la base des données");

            dr.Close();
            cmdSearchAvenueVillage.Dispose();
            con.Close();
            return list;
        }
        #endregion

        #region Utilisateur(operation sur les utilisateurs)
        /// <summary>
        /// Permet de retourner un Utilisateur suivant son identifiant passé en paramètre
        /// </summary>
        /// <param name="id">Indetifinat de l'Utilisateur</param>
        /// <returns>Un Utilisateur</returns>
        public UtilisateurServeur GetUtilisateur(int id)
        {
            UtilisateurServeur utilisateur = null;

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetUtilisateur = con.CreateCommand();
            cmdGetUtilisateur.CommandText = "select * from utilisateur where id=@id";
            IDataParameter paramId = cmdGetUtilisateur.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            cmdGetUtilisateur.Parameters.Add(paramId);

            IDataReader dr = cmdGetUtilisateur.ExecuteReader();
            if (dr.Read()) utilisateur = getUtilisateur(dr);

            dr.Close();
            cmdGetUtilisateur.Dispose();
            con.Close();
            return utilisateur;
        }

        private static UtilisateurServeur getUtilisateur(IDataReader dr)
        {
            UtilisateurServeur ut = new UtilisateurServeur();
            ut.Id = Convert.ToInt32(dr["id"]);
            ut.Id_personne = Convert.ToInt32(dr["id_personne"]);
            ut.Id_categorieUtilisateur = Convert.ToInt32(dr["id_categorieUtilisateur"]);
            ut.Nomuser = Convert.ToString(dr["nomuser"]);
            ut.Motpass = Convert.ToString(dr["motpass"]);
            ut.Activation = Convert.ToBoolean(dr["activation"]);

            return ut;
        }

        /// <summary>
        /// retourne tous les Utilisateurs
        /// </summary>
        /// <returns>Liste des Utilisateurs</returns>
        public List<UtilisateurServeur> GetUtilisateurs()
        {
            //if (con.State.ToString().Equals("Open")) { }
            //else con.Open();
            con.Close();
            con.Open();

            List<UtilisateurServeur> list = new List<UtilisateurServeur>();
            IDbCommand cmdGetUser = con.CreateCommand();
            cmdGetUser.CommandText = "select *from utilisateur order by id asc";
            IDataReader dr = cmdGetUser.ExecuteReader();
            while (dr.Read())
            {
                list.Add(getUtilisateur(dr));
            }
            dr.Close();
            cmdGetUser.Dispose();
            con.Close();
            return list;
        }

        /// <summary>
        /// Permet d'éffectuer la recherche d'une utilisateur en passant en paramètre son nom d'utilisateur. Et retourne successivement
        /// L'identifiant de l'utilisateur, l'identifiant de sa catéhorie qinsi que son no, d'utilisateur dans un tableau
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <returns>Un tableau des String</returns>
        public string[] SearchUtilisateur(string userName)
        {
            string[] tbValues = { };
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdSearchUtilisateur = con.CreateCommand();
            cmdSearchUtilisateur.CommandText = "SELECT id,id_personne,id_categorieUtilisateur,activation,nomuser FROM utilisateur WHERE nomuser=@nomuser";
            IDataParameter paramUsername = cmdSearchUtilisateur.CreateParameter();
            paramUsername.ParameterName = "@nomuser";
            paramUsername.Value = userName;
            cmdSearchUtilisateur.Parameters.Add(paramUsername);

            IDataReader dr = cmdSearchUtilisateur.ExecuteReader();

            if (dr.Read())
            {
                tbValues = new string[4];
                tbValues[0] = Convert.ToString(dr["id"]);
                tbValues[1] = Convert.ToString(dr["id_personne"]);
                tbValues[2] = dr["id_categorieUtilisateur"].ToString();
                tbValues[3] = Convert.ToString(dr["nomuser"]);
                tbValues[4] = Convert.ToString(dr["activation"]);
            }
            else throw new Exception("Le nom d'utilisateur ' " + userName + " ' recherché n'a pas été trouvé dans la base des données");

            dr.Close();
            cmdSearchUtilisateur.Dispose();
            con.Close();
            return tbValues;
        }
        #endregion

        #region CategorieUtilisateur(Operation sur les CategorieUtilisateurs)
        /// <summary>
        /// Permet de retourner une CategorieUtilisateur suivant son identifiant passé en paramètre
        /// </summary>
        /// <param name="id">Indetifinat de la CategorieUtilisateur</param>
        /// <returns>Une CategorieUtilisateur</returns>
        public CategorieUtilisateurServeur GetCategorieUtilisateur(int id)
        {
            CategorieUtilisateurServeur categorieUser = null;

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetCategorieUtilisateur = con.CreateCommand();
            cmdGetCategorieUtilisateur.CommandText = "select * from categorieUtilisateur where id=@id";
            IDataParameter paramId = cmdGetCategorieUtilisateur.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            cmdGetCategorieUtilisateur.Parameters.Add(paramId);

            IDataReader dr = cmdGetCategorieUtilisateur.ExecuteReader();
            if (dr.Read()) categorieUser = getCategorie(dr);

            dr.Close();
            cmdGetCategorieUtilisateur.Dispose();
            con.Close();
            return categorieUser;
        }

        private static CategorieUtilisateurServeur getCategorie(IDataReader dr)
        {
            CategorieUtilisateurServeur cu = new CategorieUtilisateurServeur();
            cu.Id = Convert.ToInt32(dr["id"]);
            cu.Designation = dr["designation"].ToString();
            cu.Groupe = Convert.ToString(dr["groupe"]);
            return cu;
        }
        /// <summary>
        /// Retourner toutes les Categorie des utilisateurs
        /// </summary>
        /// <returns>List des CategorieUtilisateur</returns>
        public List<CategorieUtilisateurServeur> GetCategorieUtilisateurs()
        {
            List<CategorieUtilisateurServeur> list = new List<CategorieUtilisateurServeur>();

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetCategorieUtilisateurs = con.CreateCommand();
            cmdGetCategorieUtilisateurs.CommandText = "SELECT * FROM categorieUtilisateur ORDER BY id ASC";
            IDataReader dr = cmdGetCategorieUtilisateurs.ExecuteReader();
            while (dr.Read())
            {
                list.Add(getCategorie(dr));
            }
            dr.Close();
            cmdGetCategorieUtilisateurs.Dispose();
            con.Close();
            return list;
        }
        #endregion

        #region Telephone(Operation sur les téléphone)
        private static TelephoneServeur getTel(IDataReader dr)
        {
            TelephoneServeur tel = new TelephoneServeur();
            tel.Id = Convert.ToInt32(dr["id"]);
            tel.Numero = dr["numero"].ToString();
            tel.Id_utilisateur = Convert.ToInt32(dr["id_utilisateur"]);
            return tel;
        }
        /// <summary>
        /// Retourner tous les numéros de téléphone des utilisateurs
        /// </summary>
        /// <returns>List des telephone</returns>
        public List<TelephoneServeur> GetTelephones(int value)
        {
            List<TelephoneServeur> list = new List<TelephoneServeur>();

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetTelephone = con.CreateCommand();
            cmdGetTelephone.CommandText = "SELECT *FROM telephone  WHERE id_utilisateur= @id_utilisateur";

            IDataParameter paramId_utilisateur = cmdGetTelephone.CreateParameter();
            paramId_utilisateur.ParameterName = "@id_utilisateur";
            paramId_utilisateur.Value = value;
            cmdGetTelephone.Parameters.Add(paramId_utilisateur);

            IDataReader dr = cmdGetTelephone.ExecuteReader();
            while (dr.Read())
            {
                list.Add(getTel(dr));
            }
            dr.Close();
            cmdGetTelephone.Dispose();
            con.Close();
            return list;
        }
        #endregion

        #region Carte(Operation sur les cartes)
        private static CarteServeur getCarte(IDataReader dr)
        {
            CarteServeur carte = new CarteServeur();
            carte.Id = Convert.ToInt32(dr["id"]);
            carte.Id_personne = Convert.ToInt32(dr["id_personne"]);
            carte.Datelivraison = Convert.ToDateTime(dr["datelivraison"]);
            return carte;
        }
        /// <summary>
        /// Retourner tout les cartes
        /// </summary>
        /// <returns>List des cartes</returns>
        public List<CarteServeur> GetCartes()
        {
            List<CarteServeur> list = new List<CarteServeur>();

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetCarte = con.CreateCommand();
            cmdGetCarte.CommandText = "SELECT * FROM carte ORDER BY id ASC";
            IDataReader dr = cmdGetCarte.ExecuteReader();
            while (dr.Read())
            {
                list.Add(getCarte(dr));
            }
            dr.Close();
            cmdGetCarte.Dispose();
            con.Close();
            return list;
        }
        #endregion

        #region METHODE PERMETTANT DE VERIFIER QUE LE NUMERO D'IDENTIFICATION NATIONAL PASSE EN PARAMETRE EXISTE
        private bool VerifieExistanceNumNational(string numeroNational)
        {
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            bool ok = false;
            IDbCommand cmdVerifieNumNational = con.CreateCommand();
            cmdVerifieNumNational.CommandText = "SELECT id FROM personne WHERE numeroNational=@numeroNational";

            IDataParameter paramNumeroNational = cmdVerifieNumNational.CreateParameter();
            paramNumeroNational.ParameterName = "@numeroNational";
            paramNumeroNational.Value = numeroNational;
            cmdVerifieNumNational.Parameters.Add(paramNumeroNational);

            IDataReader dr = cmdVerifieNumNational.ExecuteReader();

            if (dr.Read()) ok = true;
            else ok = false;

            cmdVerifieNumNational.Dispose();
            dr.Dispose();
            con.Close();
            return ok;
        }
        #endregion

        #region RECUPERATION DU TYPE D'INSTANCE DE LA BASE DES DONNEES POUR CONNAITRE SUR QUELLE BD ON EST CONNECTE
        /// <summary>
        /// Permet de retourner un entier représentant le type de SGBD sur lequel on sest éffectivement connecté, ainsi on a
        /// 1 pour une Base des données PostGreSQL
        /// 2 pour une Base des données MySQL
        /// 3 pour une Base des données SQLServer et
        /// 4 pour une Base des données Access 2003 - 2007 ou 2010
        /// </summary>
        /// <returns></returns>
        public int GetTypeSGBDConnecting()
        {
            int sgbd = 0;
            if (con.GetType().ToString().Equals("Npgsql.NpgsqlConnection")) sgbd = 1;
            else if (con.GetType().ToString().Equals("MySql.Data.MySqlClient.MySqlConnection")) sgbd = 2;
            else if (con.GetType().ToString().Equals("System.Data.SqlClient.SqlConnection")) sgbd = 3;
            else if (con.GetType().ToString().Equals("System.Data.OleDb.OleDbConnection")) sgbd = 4;
            return sgbd;
        }
        #endregion

        #region VERIFICATION DU MOT DE PASSE CONNAISSANT LE USERNAME DE L'UTILISATEUR
        /// <summary>
        /// Permet de vérifier le mot de passe de l'utilisateur passé en parametre
        /// </summary>
        /// <param name="userName">String Nom user</param>
        /// <returns>Booleen</returns>
        public bool VerifiePassword(string userName)
        {
            bool ok = false;
            //if (con.State.ToString().Equals("Open")) { }
            //else con.Open();
            con.Close();
            con.Open();

            IDbCommand cmdVerifPwd = con.CreateCommand();
            cmdVerifPwd.CommandText = "SELECT motpass FROM utilisateur WHERE nomuser=@nomuser";

            IDataParameter paramNomuser = cmdVerifPwd.CreateParameter();
            paramNomuser.ParameterName = "@nomuser";
            paramNomuser.Value = userName;
            cmdVerifPwd.Parameters.Add(paramNomuser);

            IDataReader dr = cmdVerifPwd.ExecuteReader();

            if (dr.Read())
            {
                if (!dr["motpass"].Equals(DBNull.Value)) ok = true;
                else ok = false;
            }

            dr.Close();
            cmdVerifPwd.Dispose();
            con.Close();
            return ok;
        }
        #endregion
        
        #region EXECUTION D'UNE QUERY
        /// <summary>
        /// Permet l'exécution de la requête passée en paramètre
        /// </summary>
        /// <param name="requete">String</param>
        public void ExecuteOneQyery(string requete)
        {
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdExecuteQuery = con.CreateCommand();
            cmdExecuteQuery.CommandText = requete;
            cmdExecuteQuery.ExecuteNonQuery();
            cmdExecuteQuery.Dispose();
            con.Close();
        }
        #endregion

        #region OPERATION SUR LA SAUVEGARDE LOCALE DE LA BD
        /// <summary>
        /// Permet d'éffectuer une sauvegarde locale de la Base des données en passant en paramètre le chemin
        /// d'accès ou l'emplacement du fichier de sauvegarde
        /// </summary>
        /// <param name="cheminAcces">Strin chemin d'acces Bd</param>
        /// <param name="lecteur">string</param>
        /// <param name="versionPostDreSQL">string</param>
        /// <returns>string</returns>
        public string BackupLocalDataBase(string cheminAcces, string lecteur, string versionPostDreSQL)
        {
            string requete = "", strNameFile = "", strTempChaine = "", strTempChaine2 = "", strGoodFile = "", strPathBatch = "", strMsgPath = "";
            if (string.IsNullOrEmpty(cheminAcces))
            {
                throw new Exception("Le chemin d'accès pour la sauvegarde de la base des données est invalide !!");
            }
            else
            {
                //cheminAcces = @"c:\temp\MonFichier.bak";
                if (this.GetTypeSGBDConnecting() == 1)
                {
                    //Bd PostGres
                    //Avant tout on commence a recuperer le repertoire dans leauel sera stocker le fichier Batch
                    string[] tbCheminPath = cheminAcces.Split(new char[] { '\\' });

                    for (int i = 0; i < tbCheminPath.Length; i++)
                    {
                        if (i == tbCheminPath.Length - 1) { }
                        else strPathBatch = strPathBatch + tbCheminPath[i] + @"\";
                    }
                    //traitement du chemin d'acces passe en parametre pour recuprer le nom du fichier
                    //On renverse la chaine pour recuperer le nom du fichier
                    for (int i = 0, j = cheminAcces.Length - 1; i < cheminAcces.Length; i++, j--) strTempChaine = strTempChaine + cheminAcces[j];
                    foreach (char str in strTempChaine)
                    {
                        if (str.ToString().Equals(@"\")) //Premier caractere \ trouve on quitte
                            break;
                        else
                        {
                            //Nane file reverted
                            strNameFile = strNameFile + str.ToString();
                        }
                    }
                    //On enleve l'extension du fichier par defaut pour la seter manuellement
                    //Avant cela on renverse encore pour enlever cette partie
                    for (int i = 0, j = strNameFile.Length - 1; i < strNameFile.Length; i++, j--) strTempChaine2 = strTempChaine2 + strNameFile[j];

                    foreach (char str in strTempChaine2)
                    {
                        if (str.ToString().Equals(@".")) //Premier caractere . trouve on quitte
                            break;
                        else
                        {
                            //Good Nane file
                            strGoodFile = strGoodFile + str.ToString();
                        }
                    }
                    //Affectation des valeur du string qui formera le contenu du fichier Batch de sauvegarde
                    string valuesFileBatch =
                    @"SET d=%DATE:~6,4%%DATE:~3,2%%DATE:~0,2%
                    REM Sauvegarde des bases de données, dans un fichier au f
                    " + lecteur + @":\Progra~1\PostgreSQL\" + versionPostDreSQL + @"\bin\pg_dump -Fc -U postgres " + con.Database + "> " + strGoodFile + "%d%.backup";

                    //Creation du fichier Batch dans l'emplacement choisi

                    using (StreamWriter srw = new StreamWriter(strPathBatch + @"scriptExecuteBackup.bat", false))
                    {
                        srw.WriteLine("{0}", valuesFileBatch);
                        srw.Close();
                    }

                    //Execution du batch File
                    Process p = new Process();
                    p.StartInfo.WorkingDirectory = strPathBatch;//On rend le repertoire choisi comme repertoire de travail
                    p.StartInfo.FileName = strPathBatch + @"\scriptExecuteBackup.bat";//On sette le chemin du fichier .bat;
                    //p.StartInfo.UseShellExecute = false;
                    //p.StartInfo.RedirectStandardError = true;
                    //p.StartInfo.RedirectStandardInput = true;
                    //p.StartInfo.RedirectStandardOutput = true;
                    //p.StartInfo.CreateNoWindow = true;

                    p.Start();//On execute le fichier .bat
                    p.WaitForExit();

                    strMsgPath = strPathBatch;
                }
                else if (this.GetTypeSGBDConnecting() == 2)
                {
                    //Bd MySQL
                }
                else if (this.GetTypeSGBDConnecting() == 3)
                {
                    //Bd SQLServer
                    strPathBatch = null;
                    lecteur = null;
                    versionPostDreSQL = null;
                    requete = "USE master " +
                              "BACKUP DATABASE " + con.Database + " " +
                              "TO DISK = N'" + cheminAcces + "' WITH NOFORMAT," +
                              "NOINIT,NAME = N'" + con.Database + "_Complete_BackUpBase'";
                    this.ExecuteOneQyery(@requete);
                }
                else if (this.GetTypeSGBDConnecting() == 4)
                {
                    //Bd Access
                }
            }
            return strMsgPath;
        }
        #endregion

        #region OPERATION SUR LA RESTAURATION DE LA BD
        /// <summary>
        /// Permet d'éffetctuer la restauration de la base des données à partir d'un fichier archive et prend respectivement
        /// comme paramètre le chemin d'accès du fichier de restauration, la lettre du lecteur de restauration ainsi que le numéro
        /// de version de PostGreSQL utilisé sur le serveur
        /// </summary>
        /// <param name="cheminAcces">string</param>
        /// <param name="lecteur">string</param>
        /// <param name="versionPostDreSQL">string</param>
        /// <returns>string</returns>
        public string RestoreDataBase(string cheminAcces, string lecteur, string versionPostDreSQL)
        {
            string requete = "", strPathBatch = "", strMsgPath = null;
            if (string.IsNullOrEmpty(cheminAcces))
            {
                throw new Exception("Le chemin d'accès pour la restoration de de la base des données est invalide !!");
            }
            else
            {
                //cheminAcces = @"c:\temp\MonFichier.bak";
                if (this.GetTypeSGBDConnecting() == 1)
                {
                    //Bd PostGres
                    if (this.GetTypeSGBDConnecting() == 1)
                    {
                        //Bd PostGres
                        //Avant tout on commence a recuperer le repertoire dans lequel sera stocker le fichier Batch
                        string[] tbCheminPath = cheminAcces.Split(new char[] { '\\' });
                        for (int i = 0; i < tbCheminPath.Length; i++)
                        {
                            if (i == tbCheminPath.Length - 1) { }
                            else strPathBatch = strPathBatch + tbCheminPath[i] + @"\";
                        }

                        //Affectation des valeur du string qui formera le contenu du fichier Batch de sauvegarde
                        string valuesFileBatch =
                        lecteur + @":\Progra~1\PostgreSQL\" + versionPostDreSQL + @"\bin\pg_restore -U postgres --dbname " + con.Database + " " + cheminAcces;

                        //Creation du fichier Batch dans l'emplacement choisi

                        using (StreamWriter srw = new StreamWriter(strPathBatch + @"scriptExecuteRestore.bat", false))
                        {
                            srw.WriteLine("{0}", valuesFileBatch);
                            srw.Close();
                        }

                        //Execution du batch File
                        Process p = new Process();
                        p.StartInfo.WorkingDirectory = strPathBatch;//On rend le repertoire dans lequel se trouve le fichier comme repertoire de travail
                        p.StartInfo.FileName = strPathBatch + @"\scriptExecuteRestore.bat";//On sette le chemin du fichier .bat;
                        //p.StartInfo.UseShellExecute = false;
                        //p.StartInfo.RedirectStandardError = true;
                        //p.StartInfo.RedirectStandardInput = true;
                        //p.StartInfo.RedirectStandardOutput = true;
                        //p.StartInfo.CreateNoWindow = true;

                        p.Start();//On execute le fichier .bat
                        p.WaitForExit();

                        strMsgPath = strPathBatch;
                    }
                    else if (this.GetTypeSGBDConnecting() == 2)
                    {
                        //Bd MySQL
                    }
                    else if (this.GetTypeSGBDConnecting() == 3)
                    {
                        //Bd SQLServer
                        requete = "USE master " +
                                  "SELECT 'kill',spid FROM sysprocesses " +
                                  "WHERE dbid=db_id('" + con.Database + "') " +
                            //"GO " +
                                  "RESTORE DATABASE " + con.Database + " " +
                                  "FROM DISK = N'" + cheminAcces + "'";
                        this.ExecuteOneQyery(@requete);
                    }
                    else if (this.GetTypeSGBDConnecting() == 4)
                    {
                        //Bd Access
                    }
                }
                return strMsgPath;
            }
        }
        #endregion

        #region EXECUTION DU BACKUP DISTANT SUR LE SERVEUR CONCERNE
        /// <summary>
        /// Permet d'exécuter la sauvegarde dans le serveur distant via le client en passant en paramètre
        /// le code de la Province et une liste contenant toutes les requêtes à exécuter en bach
        /// </summary>
        /// <param name="idProvince">Code ou Id de la Province</param>
        /// <param name="listQuery">Liste contenant toute les requêtes</param>
        public void executeBackupFromClient(int idProvince, List<string> listQuery, List<byte[]> listPhoto, List<string> listQueryUpdate, List<byte[]> listPhotoUpdate)
        {
            bool okParamServeur = false,okPhoto = false;
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdVerifieParamProv = null, cmdDoQuery = null, cmdGenerateId = null, cmdDoQueryUpdate = null;
            IDbTransaction transaction = null;
            cmdVerifieParamProv = con.CreateCommand();
            cmdVerifieParamProv.CommandText = "SELECT designation FROM province WHERE id=@id";

            IDbDataParameter paramId = cmdVerifieParamProv.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = idProvince;
            cmdVerifieParamProv.Parameters.Add(paramId);

            IDataReader dr1 = cmdVerifieParamProv.ExecuteReader();

            if (dr1.Read()) okParamServeur = true;
            dr1.Close();
            cmdVerifieParamProv.Dispose();

            try
            {               
                //La transaction s'execute dans un try catch pour detecter une erreur
                transaction = con.BeginTransaction(IsolationLevel.Serializable);
                
                if (okParamServeur)
                {
                    //========================================= INSERTION ===============================
                    IDataReader dr2 = null;
                    int goodId = 0;
                    int incrementeRecordListe = 0;
                    byte[] photoPers = { };

                    //On execute la sauvegarde, donc les insertions en cascade
                    foreach (string str in listQuery)
                    {
                        photoPers = listPhoto[incrementeRecordListe];
                        //On commence par generer un nouvel Id pour la table concernee
                        string[] tbValue = str.Split(new char[] { '|' });
                        string req1 = tbValue[0];
                        cmdGenerateId = con.CreateCommand();
                        cmdGenerateId.CommandText = "SELECT MAX(id) AS maxid FROM " + tbValue[1];
                        cmdGenerateId.Transaction = transaction;
                        dr2 = cmdGenerateId.ExecuteReader();

                        if (dr2.Read())
                        {
                            if (dr2["maxid"] == DBNull.Value) goodId = 1;
                            else goodId = Convert.ToInt32(dr2["maxid"]) + 1;
                        }
                        dr2.Close();
                        cmdGenerateId.Dispose();
                        //Fin de la creation de l'Id et vérification pour valeur null en string
                        string tempReq = "", tempValueChamp = "";
                        string[] tbValueChamp = tbValue[0].Split(new char[] { ',' });
                        int i = 1;

                        foreach (string strValue in tbValueChamp)
                        {
                            i++;
                            if (strValue.Equals("'null'") || strValue.Equals("'NULL'")) tempValueChamp = strValue.Replace(strValue, "NULL") + ",";
                            else tempValueChamp = strValue + ",";
                            tempReq = tempReq + tempValueChamp;
                            if (i == tbValueChamp.Length) break;
                        }
                        //recuperation de la good requete
                        string goodRequete = "", goodRequetePhoto = "";
                        if (tbValue[1].Equals("villeTerritoire")) goodRequete = tempReq + goodId + "," + idProvince + ")";
                        else if (tbValue[1].Equals("photo"))
                        {
                            //La photo sera un parametre recupere dans la meme table recupQuery
                            okPhoto = true;
                            goodRequetePhoto = tempReq + goodId;
                            goodRequete = "insert into photo(id_personne,id,photo) values(@id_personne,@id,@photo)";
                            Console.WriteLine("goodRequetePhoto = " + goodRequetePhoto);
                        }
                        else
                        {
                            okPhoto = false;
                            goodRequete = tempReq + goodId + ")";
                        }

                        cmdDoQuery = con.CreateCommand();
                        cmdDoQuery.CommandText = goodRequete;

                        if (okPhoto)
                        {
                            //On commence par spliter la requete pour recuperer les parametres
                            string[] tbParamPhoto = goodRequetePhoto.Split(new char[] { ',' });
                            //Les good param seront 3=id_personne et 4=id
                            Console.WriteLine("tbParamPhoto[3] = " + tbParamPhoto[3]);
                            Console.WriteLine("tbParamPhoto[4] = " + tbParamPhoto[4]);
                            int paramIdPersonne = Convert.ToInt32(tbParamPhoto[3]);
                            int paramIdPhotoInt = Convert.ToInt32(tbParamPhoto[4]);
                            Console.WriteLine("============== paramIdPersonne = {0} et paramIdPhoto = {1}", paramIdPersonne, paramIdPhotoInt);
                            //insertion pour photo
                            IDataParameter paramPhoto = cmdDoQuery.CreateParameter();
                            paramPhoto.ParameterName = "@photo";
                            paramPhoto.Value = photoPers;
                            IDataParameter paramId_personne = cmdDoQuery.CreateParameter();
                            paramId_personne.ParameterName = "@id_personne";
                            paramId_personne.Value = paramIdPersonne;
                            IDataParameter paramIdPhoto = cmdDoQuery.CreateParameter();
                            paramIdPhoto.ParameterName = "@id";
                            paramIdPhoto.Value = paramIdPhotoInt;
                            cmdDoQuery.Parameters.Add(paramPhoto);
                            cmdDoQuery.Parameters.Add(paramId_personne);
                            cmdDoQuery.Parameters.Add(paramIdPhoto);
                        }

                        Console.WriteLine("goodRequete = " + goodRequete);

                        //Execution de la query
                        cmdDoQuery.Transaction = transaction;
                        cmdDoQuery.ExecuteNonQuery();
                        incrementeRecordListe++;
                        cmdDoQuery.Dispose();
                    } 

                    //========================================= MODIFICATION ===============================

                    int incrementeRecordListeUpdate = 0;
                    byte[] photoPersUpdate = { };
                    //On execute la sauvegarde, donc les modifications en cascade
                    foreach (string strUpdate in listQueryUpdate)
                    {
                        photoPersUpdate = listPhotoUpdate[incrementeRecordListeUpdate];
                        //On recupere les valeurs splitees dans la requete pour avoir les deux grandes parties:
                        //Valeurs des champ et nom de la table
                        string[] tbValueSplite = strUpdate.Split(new char[] { '|' });

                        //Maintenant on recupere les valeurs des champs suivant la table concernee et attribution 
                        //pour requete parametree
                        if(tbValueSplite[1].Equals("villeTerritoire"))
                        {
                            string[] tbValuesChamp = tbValueSplite[0].Split(new char[] { ';' });//On recupere les valeurs de ces champs
                            cmdDoQueryUpdate = con.CreateCommand();
                            cmdDoQueryUpdate.CommandText = "update villeTerritoire set id_province=@id_province,designation=@designation,superficie=@superficie where id=@id";

                            IDataParameter paramIdProv = cmdDoQueryUpdate.CreateParameter();
                            paramIdProv.ParameterName = "@id_province";
                            paramIdProv.Value = idProvince;
                            IDataParameter paramDesi = cmdDoQueryUpdate.CreateParameter();
                            paramDesi.ParameterName = "@designation";
                            paramDesi.Value = tbValuesChamp[1];
                            IDataParameter paramSuperficie = cmdDoQueryUpdate.CreateParameter();
                            paramSuperficie.ParameterName = "@superficie";
                            paramSuperficie.Value = Convert.ToInt32(tbValuesChamp[2]);
                            IDataParameter paramIdVt = cmdDoQueryUpdate.CreateParameter();
                            paramIdVt.ParameterName = "@id";
                            paramIdVt.Value = Convert.ToInt32(tbValuesChamp[0]);

                            cmdDoQueryUpdate.Parameters.Add(paramIdProv);
                            cmdDoQueryUpdate.Parameters.Add(paramDesi);
                            cmdDoQueryUpdate.Parameters.Add(paramSuperficie);
                            cmdDoQueryUpdate.Parameters.Add(paramIdVt);
                        }
                        else if(tbValueSplite[1].Equals("communeChefferieSecteur"))
                        {
                            string[] tbValuesChamp = tbValueSplite[0].Split(new char[] { ';' });//On recupere les valeurs de ces champs
                            cmdDoQueryUpdate = con.CreateCommand();
                            cmdDoQueryUpdate.CommandText = "update communeChefferieSecteur set id_villeTerritoire=@id_villeTerritoire,designation=@designation,superficie=@superficie where id=@id";

                            IDataParameter paramId_villeTerritoire = cmdDoQueryUpdate.CreateParameter();
                            paramId_villeTerritoire.ParameterName = "@id_villeTerritoire";
                            paramId_villeTerritoire.Value = Convert.ToInt32(tbValuesChamp[1]);
                            IDataParameter paramDesignation = cmdDoQueryUpdate.CreateParameter();
                            paramDesignation.ParameterName = "@designation";
                            paramDesignation.Value = tbValuesChamp[2];
                            IDataParameter paramSuperficie = cmdDoQueryUpdate.CreateParameter();
                            paramSuperficie.ParameterName = "@superficie";
                            paramSuperficie.Value = Convert.ToInt32(tbValuesChamp[3]);
                            IDataParameter paramIdCs = cmdDoQueryUpdate.CreateParameter();
                            paramIdCs.ParameterName = "@id";
                            paramIdCs.Value = Convert.ToInt32(tbValuesChamp[0]);

                            cmdDoQueryUpdate.Parameters.Add(paramId_villeTerritoire);
                            cmdDoQueryUpdate.Parameters.Add(paramDesignation);
                            cmdDoQueryUpdate.Parameters.Add(paramSuperficie);
                            cmdDoQueryUpdate.Parameters.Add(paramIdCs);
                        }
                        else if(tbValueSplite[1].Equals("quartierLocalite"))
                        {
                            string[] tbValuesChamp = tbValueSplite[0].Split(new char[] { ';' });//On recupere les valeurs de ces champs
                            cmdDoQueryUpdate = con.CreateCommand();
                            cmdDoQueryUpdate.CommandText = "update quartierLocalite set id_communeChefferieSecteur=@id_communeChefferieSecteur,designation=@designation,superficie=@superficie where id=@id";

                            IDataParameter paramId_communeChefferieSecteur = cmdDoQueryUpdate.CreateParameter();
                            paramId_communeChefferieSecteur.ParameterName = "@id_communeChefferieSecteur";
                            paramId_communeChefferieSecteur.Value = Convert.ToInt32(tbValuesChamp[1]);
                            IDataParameter paramDesignation = cmdDoQueryUpdate.CreateParameter();
                            paramDesignation.ParameterName = "@designation";
                            paramDesignation.Value = tbValuesChamp[2];
                            IDataParameter paramSuperficie = cmdDoQueryUpdate.CreateParameter();
                            paramSuperficie.ParameterName = "@superficie";
                            paramSuperficie.Value = Convert.ToInt32(tbValuesChamp[3]);
                            IDataParameter paramIdQt = cmdDoQueryUpdate.CreateParameter();
                            paramIdQt.ParameterName = "@id";
                            paramIdQt.Value = Convert.ToInt32(tbValuesChamp[0]);

                            cmdDoQueryUpdate.Parameters.Add(paramId_communeChefferieSecteur);
                            cmdDoQueryUpdate.Parameters.Add(paramDesignation);
                            cmdDoQueryUpdate.Parameters.Add(paramSuperficie);
                            cmdDoQueryUpdate.Parameters.Add(paramIdQt);
                        }
                        else if(tbValueSplite[1].Equals("avenueVillage"))
                        {
                            string[] tbValuesChamp = tbValueSplite[0].Split(new char[] { ';' });//On recupere les valeurs de ces champs
                            cmdDoQueryUpdate = con.CreateCommand();
                            cmdDoQueryUpdate.CommandText = @"update avenueVillage set id_quartierLocalite=@id_quartierLocalite,designation=@designation where id=@id";

                            IDataParameter paramId_quartierLocalite = cmdDoQueryUpdate.CreateParameter();
                            paramId_quartierLocalite.ParameterName = "@id_quartierLocalite";
                            paramId_quartierLocalite.Value = Convert.ToInt32(tbValuesChamp[1]);
                            IDataParameter paramDesignation = cmdDoQueryUpdate.CreateParameter();
                            paramDesignation.ParameterName = "@designation";
                            paramDesignation.Value = tbValuesChamp[2];
                            IDataParameter paramIdAv = cmdDoQueryUpdate.CreateParameter();
                            paramIdAv.ParameterName = "@id";
                            paramIdAv.Value = Convert.ToInt32(tbValuesChamp[0]);

                            cmdDoQueryUpdate.Parameters.Add(paramId_quartierLocalite);
                            cmdDoQueryUpdate.Parameters.Add(paramDesignation);
                            cmdDoQueryUpdate.Parameters.Add(paramIdAv);
                        }
                        else if(tbValueSplite[1].Equals("personne"))
                        {
                            string[] tbValuesChamp = tbValueSplite[0].Split(new char[] { ';' });//On recupere les valeurs de ces champs
                            
                            cmdDoQueryUpdate = con.CreateCommand();
                            cmdDoQueryUpdate.CommandText = @"update personne set id_avenueVillage=@id_avenueVillage,nom=@nom,postnom=@postnom,prenom=@prenom,id_pere=@id_pere,id_mere=@id_mere,
                            sexe=@sexe,etativil=@etativil,travail=@travail,numero=@numero,nombreEnfant=@nombreEnfant,niveauEtude=@niveauEtude,
                            datenaissance=@datenaissance,datedeces=@datedeces where id=@id";
                            //OLD.id 0 ,NEW.id_avenueVillage 1,idPere 2,idMere 3,NEW.numeroNational 4,numAv 5
                            //,NEW.nom 6,pNomPers 7,prenomPers 8,NEW.sexe 9,NEW.etativil 10,dNaiss 11,
                            //dDeces 12,NEW.travail 13,NEW.nombreEnfant 14,NEW.niveauEtude 15
                            IDataParameter paramId_avenueVillage = cmdDoQueryUpdate.CreateParameter();
                            paramId_avenueVillage.ParameterName = "@id_avenueVillage";
                            paramId_avenueVillage.Value = Convert.ToInt32(tbValuesChamp[1]);
                            IDataParameter paramNom = cmdDoQueryUpdate.CreateParameter();
                            paramNom.ParameterName = "@nom";
                            paramNom.Value = tbValuesChamp[6];
                            IDataParameter paramPostNom = cmdDoQueryUpdate.CreateParameter();
                            paramPostNom.ParameterName = "@postnom";
                            paramPostNom.Value = ((tbValuesChamp[7].Equals("null")) || (tbValuesChamp[7].Equals("NULL")) ? null : tbValuesChamp[7]);
                            IDataParameter paramPrenom = cmdDoQueryUpdate.CreateParameter();
                            paramPrenom.ParameterName = "@prenom";
                            paramPrenom.Value = ((tbValuesChamp[8].Equals("null")) || (tbValuesChamp[8].Equals("NULL")) ? null : tbValuesChamp[8]);
                            IDataParameter paramId_pere = cmdDoQueryUpdate.CreateParameter();
                            paramId_pere.ParameterName = "@id_pere";
                            paramId_pere.Value = ((tbValuesChamp[2].Equals("null")) || (tbValuesChamp[2].Equals("NULL")) ? null : tbValuesChamp[2]);
                            IDataParameter paramId_mere = cmdDoQueryUpdate.CreateParameter();
                            paramId_mere.ParameterName = "@id_mere";
                            paramId_mere.Value = ((tbValuesChamp[3].Equals("null")) || (tbValuesChamp[3].Equals("NULL")) ? null : tbValuesChamp[3]);
                            IDataParameter paramSexe = cmdDoQueryUpdate.CreateParameter();
                            paramSexe.ParameterName = "@sexe";
                            paramSexe.Value = tbValuesChamp[9];
                            IDataParameter paramEtatCivil = cmdDoQueryUpdate.CreateParameter();
                            paramEtatCivil.ParameterName = "@etativil";
                            paramEtatCivil.Value = tbValuesChamp[10];
                            IDataParameter paramTravail = cmdDoQueryUpdate.CreateParameter();
                            paramTravail.ParameterName = "@travail";

                            if (tbValuesChamp[13] == "TRUE" || tbValuesChamp[13] == "true" || tbValuesChamp[13] == "T" || tbValuesChamp[13] == "t" || tbValuesChamp[13] == "1") paramTravail.Value = "1";
                            else if (tbValuesChamp[13] == "FALSE" || tbValuesChamp[13] == "false" || tbValuesChamp[13] == "F" || tbValuesChamp[13] == "f" || tbValuesChamp[13] == "0") paramTravail.Value = "0";

                            IDataParameter paramNumero = cmdDoQueryUpdate.CreateParameter();
                            paramNumero.ParameterName = "@numero";
                            paramNumero.Value = ((tbValuesChamp[5].Equals("null")) || (tbValuesChamp[5].Equals("NULL")) ? null : tbValuesChamp[5]);
                            IDataParameter paramNbrEnf = cmdDoQueryUpdate.CreateParameter();
                            paramNbrEnf.ParameterName = "@nombreEnfant";
                            paramNbrEnf.Value = Convert.ToInt32(tbValuesChamp[14]);
                            IDataParameter paramNiveauEtude = cmdDoQueryUpdate.CreateParameter();
                            paramNiveauEtude.ParameterName = "@niveauEtude";
                            paramNiveauEtude.Value = tbValuesChamp[15];
                            IDataParameter paramDateNaiss = cmdDoQueryUpdate.CreateParameter();
                            paramDateNaiss.ParameterName = "@datenaissance";
                            paramDateNaiss.Value = ((tbValuesChamp[11].Equals("null")) || (tbValuesChamp[11].Equals("NULL")) ? null : tbValuesChamp[11]);
                            IDataParameter paramDateDeces = cmdDoQueryUpdate.CreateParameter();
                            paramDateDeces.ParameterName = "@datedeces";
                            paramDateDeces.Value = ((tbValuesChamp[12].Equals("null")) || (tbValuesChamp[12].Equals("NULL")) ? null : tbValuesChamp[12]);
                            IDataParameter paramIdP = cmdDoQueryUpdate.CreateParameter();
                            paramIdP.ParameterName = "@id";
                            paramIdP.Value = Convert.ToInt32(tbValuesChamp[0]);

                            cmdDoQueryUpdate.Parameters.Add(paramId_avenueVillage);
                            cmdDoQueryUpdate.Parameters.Add(paramNom);
                            cmdDoQueryUpdate.Parameters.Add(paramPostNom);
                            cmdDoQueryUpdate.Parameters.Add(paramPrenom);
                            cmdDoQueryUpdate.Parameters.Add(paramId_pere);
                            cmdDoQueryUpdate.Parameters.Add(paramId_mere);
                            cmdDoQueryUpdate.Parameters.Add(paramSexe);
                            cmdDoQueryUpdate.Parameters.Add(paramEtatCivil);
                            cmdDoQueryUpdate.Parameters.Add(paramTravail);
                            cmdDoQueryUpdate.Parameters.Add(paramNumero);
                            cmdDoQueryUpdate.Parameters.Add(paramNbrEnf);
                            cmdDoQueryUpdate.Parameters.Add(paramNiveauEtude);
                            cmdDoQueryUpdate.Parameters.Add(paramDateNaiss);
                            cmdDoQueryUpdate.Parameters.Add(paramDateDeces);
                            cmdDoQueryUpdate.Parameters.Add(paramIdP);
                        }
                        else if(tbValueSplite[1].Equals("photo"))
                        {
                            //La photo sera un parametre recupere dans la meme table recupQuery
                            string[] tbValuesChamp = tbValueSplite[0].Split(new char[] { ';' });//On recupere les valeurs de ces champs

                            //On commence par spliter la requete pour recuperer les parametres
                            IDataParameter paramPhoto = cmdDoQueryUpdate.CreateParameter();
                            paramPhoto.ParameterName = "@photo";
                            paramPhoto.Value = photoPersUpdate;
                            IDataParameter paramId_personne = cmdDoQueryUpdate.CreateParameter();
                            paramId_personne.ParameterName = "@id_personne";
                            paramId_personne.Value = Convert.ToInt32(tbValuesChamp[1]);
                            IDataParameter paramIdPhoto = cmdDoQueryUpdate.CreateParameter();
                            paramIdPhoto.ParameterName = "@id";
                            paramIdPhoto.Value = Convert.ToInt32(tbValuesChamp[0]);
                            cmdDoQueryUpdate.Parameters.Add(paramPhoto);
                            cmdDoQueryUpdate.Parameters.Add(paramId_personne);
                            cmdDoQueryUpdate.Parameters.Add(paramIdPhoto);
                        }
                        else if(tbValueSplite[1].Equals("carte"))
                        {
                            string[] tbValuesChamp = tbValueSplite[0].Split(new char[] { ';' });//On recupere les valeurs de ces champs
                            cmdDoQueryUpdate = con.CreateCommand();
                            cmdDoQueryUpdate.CommandText = "update carte set id_personne=@id_personne,datelivraison=@datelivraison where id=@id";

                            IDataParameter paramId_personne = cmdDoQueryUpdate.CreateParameter();
                            paramId_personne.ParameterName = "@id_personne";
                            paramId_personne.Value = Convert.ToInt32(tbValuesChamp[1]);
                            IDataParameter paramDatelivraison = cmdDoQueryUpdate.CreateParameter();
                            paramDatelivraison.ParameterName = "@datelivraison";
                            paramDatelivraison.Value = Convert.ToDateTime(tbValuesChamp[2]);
                            IDataParameter paramIdCt = cmdDoQueryUpdate.CreateParameter();
                            paramIdCt.ParameterName = "@id";
                            paramIdCt.Value = Convert.ToInt32(tbValuesChamp[0]);

                            cmdDoQueryUpdate.Parameters.Add(paramId_personne);
                            cmdDoQueryUpdate.Parameters.Add(paramDatelivraison);
                            cmdDoQueryUpdate.Parameters.Add(paramIdCt);
                        }
                        else if(tbValueSplite[1].Equals("categorieUtilisateur"))
                        {
                            string[] tbValuesChamp = tbValueSplite[0].Split(new char[] { ';' });//On recupere les valeurs de ces champs
                            cmdDoQueryUpdate = con.CreateCommand();
                            cmdDoQueryUpdate.CommandText = "update categorieUtilisateur set designation=@designation,groupe=@groupe where id=@id";

                            IDataParameter paramDesignation = cmdDoQueryUpdate.CreateParameter();
                            paramDesignation.ParameterName = "@designation";
                            paramDesignation.Value = tbValuesChamp[1];
                            IDataParameter paramGroupe = cmdDoQueryUpdate.CreateParameter();
                            paramGroupe.ParameterName = "@groupe";
                            paramGroupe.Value = tbValuesChamp[2];
                            IDataParameter paramIdCatU = cmdDoQueryUpdate.CreateParameter();
                            paramIdCatU.ParameterName = "@id";
                            paramIdCatU.Value = Convert.ToInt32(tbValuesChamp[0]);

                            cmdDoQueryUpdate.Parameters.Add(paramDesignation);
                            cmdDoQueryUpdate.Parameters.Add(paramGroupe);
                            cmdDoQueryUpdate.Parameters.Add(paramIdCatU);
                        }
                        else if(tbValueSplite[1].Equals("utilisateur"))
                        {
                            string[] tbValuesChamp = tbValueSplite[0].Split(new char[] { ';' });//On recupere les valeurs de ces champs
                            cmdDoQueryUpdate = con.CreateCommand();
                            cmdDoQueryUpdate.CommandText = @"update utilisateur set id_personne=@id_personne,
                            id_categorieUtilisateur=@id_categorieUtilisateur,activation=@activation,nomuser=@nomuser where id=@id";

                            IDataParameter paramId_personne = cmdDoQueryUpdate.CreateParameter();
                            paramId_personne.ParameterName = "@id_personne";
                            paramId_personne.Value = Convert.ToInt32(tbValuesChamp[1]);
                            IDataParameter paramId_categorieUtilisateur = cmdDoQueryUpdate.CreateParameter();
                            paramId_categorieUtilisateur.ParameterName = "@id_categorieUtilisateur";
                            paramId_categorieUtilisateur.Value = Convert.ToInt32(tbValuesChamp[2]);
                            IDataParameter paramActivation = cmdDoQueryUpdate.CreateParameter();
                            paramActivation.ParameterName = "@activation";

                            if (tbValuesChamp[3] == "TRUE" || tbValuesChamp[3] == "true" || tbValuesChamp[3] == "T" || tbValuesChamp[3] == "t" || tbValuesChamp[3] == "1") paramActivation.Value = "1";
                            else if (tbValuesChamp[3] == "FALSE" || tbValuesChamp[3] == "false" || tbValuesChamp[3] == "F" || tbValuesChamp[3] == "f" || tbValuesChamp[3] == "0") paramActivation.Value = "0";
                            
                            IDataParameter paramNomuser = cmdDoQueryUpdate.CreateParameter();
                            paramNomuser.ParameterName = "@nomuser";
                            paramNomuser.Value = tbValuesChamp[4];
                            IDataParameter paramIdU = cmdDoQueryUpdate.CreateParameter();
                            paramIdU.ParameterName = "@id";
                            paramIdU.Value = Convert.ToInt32(tbValuesChamp[0]);

                            cmdDoQueryUpdate.Parameters.Add(paramId_personne);
                            cmdDoQueryUpdate.Parameters.Add(paramId_categorieUtilisateur);
                            cmdDoQueryUpdate.Parameters.Add(paramActivation);
                            cmdDoQueryUpdate.Parameters.Add(paramNomuser);
                            cmdDoQueryUpdate.Parameters.Add(paramIdU);
                        }
                        else if(tbValueSplite[1].Equals("telephone"))
                        {
                            string[] tbValuesChamp = tbValueSplite[0].Split(new char[] { ';' });//On recupere les valeurs de ces champs
                            cmdDoQueryUpdate = con.CreateCommand();
                            cmdDoQueryUpdate.CommandText = "update telephone set id_utilisateur=@id_utilisateur,numero=@numero where id=@id";

                            IDataParameter paramNumero = cmdDoQueryUpdate.CreateParameter();
                            paramNumero.ParameterName = "@numero";
                            paramNumero.Value = tbValuesChamp[2];
                            IDataParameter paramId_utilisateur = cmdDoQueryUpdate.CreateParameter();
                            paramId_utilisateur.ParameterName = "@id_utilisateur";
                            paramId_utilisateur.Value = Convert.ToInt32(tbValuesChamp[1]);
                            IDataParameter paramIdTf = cmdDoQueryUpdate.CreateParameter();
                            paramIdTf.ParameterName = "@id";
                            paramIdTf.Value = Convert.ToInt32(tbValuesChamp[0]);

                            cmdDoQueryUpdate.Parameters.Add(paramNumero);
                            cmdDoQueryUpdate.Parameters.Add(paramId_utilisateur);
                            cmdDoQueryUpdate.Parameters.Add(paramIdTf);
                        }

                        //Execution de la query
                        cmdDoQueryUpdate.Transaction = transaction;
                        cmdDoQueryUpdate.ExecuteNonQuery();
                        incrementeRecordListeUpdate++;
                        cmdDoQueryUpdate.Dispose();
                    }

                    transaction.Commit();//Le comite s'executera une et une seule foid a la fin de tout pour valider ou non les requetes executees
                }
                else throw new Exception("Impossible d'éffectuer la sauvegarde car les paramètres de la Povince sont invalide, veuillez contacter l'Administrateur");
            }
            catch (Exception ex)
            {
                //Erreur d'execution de la transaction
                if (transaction != null)
                {
                    transaction.Rollback();
                    throw new Exception("Echec de la sauvegarde, veuillez recommencer svp ,"+ex.Message);
                }
            }
            con.Close();
        }
        #endregion

        #region FONCTIONS DE CALCUL
        /// <summary>
        /// Fonction qui calcule la Densite de la population (Nombre des habitants par Km carre) et la retourne sous forme d'un nombre décimal
        /// et prend 2 paramètres dont le nom de la table concernée et un entier 0->Provincial et 1->National
        /// </summary>
        /// <param name="nomTable">Nom de la table</param>
        /// <param name="entierCategorie">Catégorie concernée</param>
        /// <returns>Un nombre décimal</returns>
        public string CalculDensitePopulation(string nomTable, int id)
        {
            String strquery = "";
            if (nomTable.Equals("Province"))
            {
                strquery = @"SELECT province.designation ,province.superficie,(count(personne.id))/POW(province.superficie,1) as densitePop
			    FROM province,personne 
			    INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			    INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			    INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			    INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire 
			    GROUP BY province.designation,province.designation ,province.superficie";
            }

            else if (nomTable.Equals("Ville|Territoire"))
            {
                strquery = @"SELECT villeTerritoire.designation,villeTerritoire.superficie, (COUNT(personne.id))/POW(villeTerritoire.superficie ,1) as densitePop
			    FROM personne 
			    INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			    INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			    INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			    INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			    GROUP BY villeTerritoire.designation,villeTerritoire.superficie";
            }
            else if (nomTable.Equals("Commune|Chefferie"))
            {
                strquery = @"SELECT communeChefferieSecteur.designation,communeChefferieSecteur.superficie,COUNT(personne.id)/POW(communeChefferieSecteur.superficie,1) as densitePop
			    FROM personne 
			    INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			    INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			    INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			    INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
                WHERE communeChefferieSecteur.id_villeTerritoire=" + id + @"
			    GROUP BY communeChefferieSecteur.designation,communeChefferieSecteur.superficie";
            }
            else if (nomTable.Equals("Quartier|Localité"))
            {
                strquery = @"SELECT quartierLocalite.designation,quartierLocalite.superficie,COUNT(personne.id)/POW(quartierLocalite.superficie,1) as densitePop
			    FROM personne 
			    INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			    INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			    INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			    INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
                WHERE quartierLocalite.id_communeChefferieSecteur=" + id + @"
			    GROUP BY quartierLocalite.designation,quartierLocalite.superficie";
            }
            else if (nomTable.Equals("Avenue|Village"))
            {

                strquery = @"SELECT avenueVillage.designation,avenueVillage.superficie,COUNT(personne.id)/POW(avenueVillage.superficie,1) as densitePop
			    FROM personne 
			    INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			    INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			    INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			    INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
                WHERE avenueVillage.id_quartierLocalite=" + id + @"
			    GROUP BY avenueVillage.designation,avenueVillage.superficie";
            }
            return strquery;
        }
        /// <summary>
        /// Fonction qui calcule le taux de natalite (Nombre des naissances vivantes l'an divisé par la population moyenne l'an) 
        /// et retourne la requete la calculant et prend 2 paramètres : un string representant le nom de la table et l'année en cours
        /// </summary>
        /// <param name="nomTable">String</param>
        /// <param name="anneCours">Entier</param>
        /// <returns>String</returns>
        public string fonctionCalculTauxNatalite(string nomTable, int anneCours, int id)
        {
            string dateJanvier = "1/1/" + anneCours.ToString();
            string dateDecembre = "31/12/" + anneCours.ToString();
            string strquery = "";
            if (nomTable.Equals("Province"))
            {
                strquery = @"SELECT DISTINCT province.designation,province.superficie,(CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)))/
                        (SELECT (SELECT (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM province,personne 
	                        INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
	                        INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
	                        INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
	                        INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
	                        WHERE personne.dateNaissance <= '" + dateDecembre + @"'
	                        AND personne.dateDeces ISNULL )+
                        (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM province,personne 
	                        INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
	                        INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
	                        INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
	                        INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
	                        WHERE personne.dateNaissance <= '" + dateJanvier + @"'
	                        AND personne.dateDeces ISNULL ))/2) as TauxNatalite,
                        (EXTRACT(YEAR FROM personne.dateNaissance)) as Annee FROM province,personne 
                        INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
                        INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
                        INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
                        INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
                        WHERE (EXTRACT(YEAR FROM personne.dateNaissance))=" + anneCours + @"
                        AND personne.dateDeces ISNULL GROUP BY province.designation,province.superficie,(EXTRACT(YEAR FROM personne.dateNaissance))";
            }
            else if (nomTable.Equals("Ville|Territoire"))
            {
                strquery = @"SELECT DISTINCT villeTerritoire.designation,villeTerritoire.superficie,(CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)))/
	                        (SELECT (SELECT (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM personne 
		                        INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
		                        INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
		                        INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
		                        INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
		                        WHERE personne.dateNaissance <= '" + dateDecembre + @"'
		                        AND personne.dateDeces ISNULL)+
	                        (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM personne 
		                        INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
		                        INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
		                        INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
		                        INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
		                        WHERE personne.dateNaissance <= '" + dateJanvier + @"'
		                        AND personne.dateDeces ISNULL))/2) as TauxNatalite,
                            (EXTRACT(YEAR FROM personne.dateNaissance)) as Annee FROM personne 
                            INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
                            INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
                            INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
                            INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
                            WHERE (EXTRACT(YEAR FROM personne.dateNaissance))=" + anneCours + @"
                            AND personne.dateDeces ISNULL GROUP BY villeTerritoire.designation,villeTerritoire.superficie,(EXTRACT(YEAR FROM personne.dateNaissance))";
            }
            else if (nomTable.Equals("Commune|Chefferie"))
            {
                strquery = @"SELECT DISTINCT communeChefferieSecteur.designation,communeChefferieSecteur.superficie,(CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)))/
	                            (SELECT (SELECT (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM personne 
		                            INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
		                            INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
		                            INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
		                            WHERE personne.dateNaissance <= '" + dateDecembre + @"'
		                            AND personne.dateDeces ISNULL)+
	                            (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM personne 
		                            INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
		                            INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
		                            INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
		                            WHERE personne.dateNaissance <= '" + dateJanvier + @"'
		                            AND personne.dateDeces ISNULL))/2) as TauxNatalite,
                                (EXTRACT(YEAR FROM personne.dateNaissance)) as Annee FROM personne 
                                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
                                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
                                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
                                WHERE (EXTRACT(YEAR FROM personne.dateNaissance))=" + anneCours + @" and communeChefferieSecteur.id_villeTerritoire=" + id + @"
                                AND personne.dateDeces ISNULL GROUP BY communeChefferieSecteur.designation,communeChefferieSecteur.superficie,(EXTRACT(YEAR FROM personne.dateNaissance))";
            }
            else if (nomTable.Equals("Quartier|Localité"))
            {
                strquery =
                @"SELECT DISTINCT quartierLocalite.designation,quartierLocalite.superficie,(CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)))/
	                (SELECT (SELECT (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM personne 
		                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
		                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
		                WHERE personne.dateNaissance <= '" + dateDecembre + @"'
		                AND personne.dateDeces ISNULL)+
	                (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM personne 
		                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
		                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
		                WHERE personne.dateNaissance <= '" + dateJanvier + @"'
		                AND personne.dateDeces ISNULL))/2) as TauxNatalite,
                    (EXTRACT(YEAR FROM personne.dateNaissance)) as Annee FROM personne 
                    INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
                    INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
                    WHERE (EXTRACT(YEAR FROM personne.dateNaissance))=" + anneCours + @" and quartierLocalite.id_communeChefferieSecteur=" + id + @"
                    AND personne.dateDeces ISNULL GROUP BY quartierLocalite.designation,quartierLocalite.superficie,(EXTRACT(YEAR FROM personne.dateNaissance))";
            }
            else if (nomTable.Equals("Avenue|Village"))
            {
                strquery =
                @"SELECT DISTINCT avenueVillage.designation,(CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)))/
	                (SELECT (SELECT (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM personne 
		                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
		                WHERE personne.dateNaissance <= '" + dateDecembre + @"'
		                AND personne.dateDeces ISNULL)+
	                (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM personne 
		                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
		                WHERE personne.dateNaissance <= '" + dateJanvier + @"'
		                AND personne.dateDeces ISNULL))/2) as TauxNatalite,
                    (EXTRACT(YEAR FROM personne.dateNaissance)) as Annee FROM personne 
                    INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
                    WHERE (EXTRACT(YEAR FROM personne.dateNaissance))=" + anneCours + @" and avenueVillage.id_quartierLocalite=" + id + @"
                    AND personne.dateDeces ISNULL GROUP BY avenueVillage.designation,(EXTRACT(YEAR FROM personne.dateNaissance))";
            }
            return strquery;
        }

        /// <summary>
        /// Fonction qui calcule le taux de mortalité (Nombre des décès l'an divisé par la population moyenne l'an) 
        /// et retourne la requête la calculant et prend 2 paramètres : un string représentant le nom de la table
        /// </summary>
        /// <param name="nomTable">String</param>
        /// <param name="anneCours">Entier</param>
        /// <returns>String</returns>                  
        public string fonctionCalculTauxMortalite(string nomTable, int anneCours, int id)
        {
            string dateJanvier = "1/1/" + anneCours.ToString();
            string dateDecembre = "1/1/" + anneCours.ToString();
            string strquery = "";
            if (nomTable.Equals("Province"))
            {
                strquery =
                @"SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2))/
		                (SELECT (SELECT (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM province,personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			                INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			                WHERE personne.dateDeces <= '" + dateDecembre + @"')+
		                (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM province,personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			                INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			                WHERE personne.dateDeces <= '" + dateJanvier + @"'))/2) 
	                AS TauxMortalite,province.designation,province.superficie,EXTRACT(YEAR FROM personne.dateDeces)as annee FROM province,personne 
	                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
	                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
	                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
	                INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
	                WHERE (EXTRACT(YEAR FROM personne.dateDeces))=" + anneCours + @"
	                AND personne.dateDeces IS NOT NULL GROUP BY province.designation,province.superficie,EXTRACT(YEAR FROM personne.dateDeces)";
            }
            else if (nomTable.Equals("Ville|Territoire"))
            {
                strquery =
                @"SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2))/
		                (SELECT (SELECT (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			                INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			                WHERE personne.dateDeces <= '" + dateDecembre + @"')+
		                (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			                INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			                WHERE personne.dateDeces <= '" + dateJanvier + @"'))/2) 
	                AS TauxMortalite,villeTerritoire.designation,villeTerritoire.superficie,EXTRACT(YEAR FROM personne.dateDeces) as annee FROM personne 
	                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
	                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
	                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
	                INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
	                WHERE (EXTRACT(YEAR FROM personne.dateDeces))=" + anneCours + @" 
	                AND personne.dateDeces IS NOT NULL GROUP BY villeTerritoire.designation,villeTerritoire.superficie,EXTRACT(YEAR FROM personne.dateDeces)";
            }
            else if (nomTable.Equals("Commune|Chefferie"))
            {
                strquery =
                @"SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2))/
	                (SELECT (SELECT (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM personne 
		                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
		                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
		                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
		                WHERE personne.dateDeces <= '" + dateDecembre + @"')+
	                (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM personne 
		                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
		                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
		                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
		                WHERE personne.dateDeces <= '" + dateJanvier + @"'))/2) 
                AS TauxMortalite,communeChefferieSecteur.designation,communeChefferieSecteur.superficie,EXTRACT(YEAR FROM personne.dateDeces) as annee FROM personne 
                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
                WHERE (EXTRACT(YEAR FROM personne.dateDeces))=" + anneCours + @" and communeChefferieSecteur.id_villeTerritoire=" + id + @"
                AND personne.dateDeces IS NOT NULL GROUP BY communeChefferieSecteur.designation,communeChefferieSecteur.superficie,EXTRACT(YEAR FROM personne.dateDeces)";
            }
            else if (nomTable.Equals("Quartier|Localité"))
            {
                strquery =
                @"SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2))/
		            (SELECT (SELECT (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM personne 
			            INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			            INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			            WHERE personne.dateDeces <= '" + dateDecembre + @"')+
		            (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM personne 
			            INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			            INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			            WHERE personne.dateDeces <= '" + dateJanvier + @"'))/2) 
	            AS TauxMortalite,quartierLocalite.designation,quartierLocalite.superficie,EXTRACT(YEAR FROM personne.dateDeces) as annee FROM personne 
	            INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
	            INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
	            WHERE (EXTRACT(YEAR FROM personne.dateDeces))=" + anneCours + @" and quartierLocalite.id_communeChefferieSecteur=" + id + @"
	            AND personne.dateDeces IS NOT NULL GROUP BY quartierLocalite.designation,quartierLocalite.superficie,EXTRACT(YEAR FROM personne.dateDeces)";
            }
            else if (nomTable.Equals("Avenue|Village"))
            {
                strquery =
                @"SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2))/
	                (SELECT (SELECT (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM personne 
		                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
		                WHERE personne.dateDeces <= '" + dateDecembre + @"')+
	                (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM personne 
		                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
		                WHERE personne.dateDeces <= '" + dateJanvier + @"'))/2) 
                AS TauxMortalite,avenueVillage.designation,EXTRACT(YEAR FROM personne.dateDeces) as annee FROM personne 
                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
                WHERE (EXTRACT(YEAR FROM personne.dateDeces))=" + anneCours + @" and avenueVillage.id_quartierLocalite=" + id + @"
                AND personne.dateDeces IS NOT NULL GROUP BY avenueVillage.designation,EXTRACT(YEAR FROM personne.dateDeces)";
            }
            return strquery;
        }
        /// <summary>
        /// /// <summary>
        /// Fonction qui calcule le taux de croissance ((Y-X)*((racine caree PY/PX)-1))*100 
        /// et retourne la requête qui la calcule et prend 3 paramètres : un object représentant la classe conceree 
        /// l'année de début X  et un entier représentant l'année de fin Y
        /// <param name="obj">Object de la classe concernee</param>
        /// <param name="anneeDebut">Annee de debut X</param>
        /// <param name="anneeFin">Annee de fin Y</param>
        /// <returns>String</returns>
        public string fonctionCalculTauxCroissance(object obj, int anneeDebut, int anneeFin)
        {
            string strquery = "";
            if (obj is Province)
            {
                strquery =
                @"SELECT((" + anneeFin + @"-" + anneeDebut + @")*
		            (SELECT SQRT(
			            (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeY FROM province,personne 
				            INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
				            INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
				            INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
				            INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
				            WHERE personne.anneeSaved='" + anneeFin + @"')/

			            (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeX FROM province,personne 
				            INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
				            INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
				            INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
				            INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
				            WHERE personne.anneeSaved='" + anneeDebut + @"')-1)))*100 
	                AS TauxCroissance,province.designation from province";
            }
            else if (obj is VilleTeritoireServeur)
            {
                strquery =
                @"SELECT((" + anneeFin + @"-" + anneeDebut + @")*
	                (SELECT SQRT(
		                (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeY FROM personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			                INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			                WHERE personne.anneeSaved='" + anneeFin + @"')/

		                (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeX FROM province,personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			                INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			                WHERE personne.anneeSaved='" + anneeDebut + @"')-1)))*100 
                    AS TauxCroissance,villeTerritoire.designation from villeTerritoire";
            }
            else if (obj is CommuneChefferieSecteurServeur)
            {
                strquery =
                @"SELECT((" + anneeFin + @"-" + anneeDebut + @")*
	                (SELECT SQRT(
		                (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeY FROM personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			                WHERE personne.anneeSaved='" + anneeFin + @"' AND communeChefferieSecteur.id=" + ((CommuneChefferieSecteurServeur)obj).Id + @")/

		                (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeX FROM province,personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			                WHERE personne.anneeSaved='" + anneeDebut + @"' AND communeChefferieSecteur.id=" + ((CommuneChefferieSecteurServeur)obj).Id + @")-1)))*100 
                    AS TauxCroissance,communeChefferieSecteur.designation from communeChefferieSecteur";
            }
            else if (obj is QuartierLocaliteServeur)
            {
                strquery =
                @"SELECT((" + anneeFin + @"-" + anneeDebut + @")*
	                (SELECT SQRT(
		                (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeY FROM personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			                WHERE personne.anneeSaved='" + anneeFin + @"' AND quartierLocalite.id=" + ((QuartierLocaliteServeur)obj).Id + @")/

		                (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeX FROM province,personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			                WHERE personne.anneeSaved='" + anneeDebut + @"' AND quartierLocalite.id=" + ((QuartierLocaliteServeur)obj).Id + @")-1)))*100 
                    AS TauxCroissance,quartierLocalite.designation from quartierLocalite";
            }
            else if (obj is AvenueVillageServeur)
            {
                strquery =
                @"SELECT((" + anneeFin + @"-" + anneeDebut + @")*
	                (SELECT SQRT(
		                (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeY FROM personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                WHERE personne.anneeSaved='" + anneeFin + @"' AND avenueVillage.id=" + ((AvenueVillageServeur)obj).Id + @")/

		                (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeX FROM province,personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                WHERE personne.anneeSaved='" + anneeDebut + @"' AND avenueVillage.id=" + ((AvenueVillageServeur)obj).Id + @")-1)))*100 
                    AS TauxCroissance,avenueVillage.designation from avenueVillage";
            }

            return strquery;
        }
        #endregion
    }
}
