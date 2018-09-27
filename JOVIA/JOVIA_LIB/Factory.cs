using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using JOVIA_LIB_SERVEUR;
using MySql.Data.MySqlClient;
using Npgsql;

namespace JOVIA_LIB
{
    public class Factory
    {
        private IDbConnection con;
        public NpgsqlConnection conn;
        public IDbConnection Con
        {
            get { return con; }
            set { con = value; }
        }
        private static bool isAdmin;
        private static Factory staticFactory;
        public static string stringConnect = "";
        private static string fileNamePostGres = "parametresPostGres.par";
        private static string fileNameMySQL = "fileNameMySQL.par";
        private static string fileNameSQLServer = "parametresSQLServer.par";
        private static string fileNameAccess = "parametresAccess.par";
        private static int valUser = 0;//entier permettant de categoriser un utilisateur connecte au serveur Provincial ou National 
        //0 par defaut et 1 pour National     

        private Factory()
        {
        }

        #region VALEUR POUR CATEGORISATION CONNEXION
        public static int ValUser
        {
            get { return valUser; }
            set { valUser = value; }
        }
        #endregion

        #region PERMET DE DISTINGUER LE USER CONNECTE AU SERVEUR DISTANT ET L'ADMIN PROV CONNECTE AU SERVEUR DISTANT
        public static bool IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; }
        }
        #endregion

        #region Initialisation de la Factory
        public static Factory Instance
        {
            get
            {
                if (staticFactory == null)
                    staticFactory = new Factory();
                return staticFactory;
            }
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
                using (StreamWriter srw = new StreamWriter(updateCreateDirectory("JOVIA_CLIENT") + @"\" + fileNamePostGres, false))
                {
                    srw.WriteLine("{0};{1};{2};{3}", serveur, bd, userName, port);
                    srw.Close();
                }
            }
            else if (valueBD == 1)
            {
                //MySQL
                using (StreamWriter srw = new StreamWriter(updateCreateDirectory("JOVIA_CLIENT") + @"\" + fileNameMySQL, false))
                {
                    srw.WriteLine("{0};{1};{2}", serveur, bd, userName);
                    srw.Close();
                }
            }
            else if (valueBD == 2)
            {
                using (StreamWriter srw = new StreamWriter(updateCreateDirectory("JOVIA_CLIENT") + @"\" + fileNameSQLServer, false))
                {
                    srw.WriteLine("{0};{1};{2}", serveur, bd, userName);
                    srw.Close();
                }
            }
            else if (valueBD == 3)
            {
                using (StreamWriter srw = new StreamWriter(updateCreateDirectory("JOVIA_CLIENT") + @"\" + fileNameAccess, false))
                {
                    srw.WriteLine("{0}", bd);
                    srw.Close();
                }
            }
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
                if (File.Exists(updateCreateDirectory("JOVIA_CLIENT") + @"\" + fileNamePostGres))
                {
                    using (StreamReader sr = new StreamReader(updateCreateDirectory("JOVIA_CLIENT") + @"\" + fileNamePostGres))
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
                if (File.Exists(updateCreateDirectory("JOVIA_CLIENT") + @"\" + fileNameMySQL))
                {
                    using (StreamReader sr = new StreamReader(updateCreateDirectory("JOVIA_CLIENT") + @"\" + fileNameMySQL))
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
                if (File.Exists(updateCreateDirectory("JOVIA_CLIENT") + @"\" + fileNameSQLServer))
                {
                    using (StreamReader sr = new StreamReader(updateCreateDirectory("JOVIA_CLIENT") + @"\" + fileNameSQLServer))
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
            else if (valueDB == 3)
            {
                //Access
                values = new string[1];
                if (File.Exists(updateCreateDirectory("JOVIA_CLIENT") + @"\" + fileNameAccess))
                {
                    using (StreamReader sr = new StreamReader(updateCreateDirectory("JOVIA_CLIENT") + @"\" + fileNameAccess))
                    {
                        if (!sr.EndOfStream)
                        {
                            string str = sr.ReadLine();
                            //string[] value = str.Split(new char[] { ';' });
                            values[0] = str;
                            sr.Close();
                        }
                    }
                }
            }

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

        #region RECUPERATION DU NIVEAU DE L'UTILISATEUR POUR S'EN SERVIR A ENABLED OU DESABLED DES MENUS
        /// <summary>
        /// Permet de retourner un entier représentant le niveau d'un utilisateur Côté Provincial
        /// 0=>Administrateur, 1=>Recensseur et 2=>Agent de commune
        /// </summary>
        /// <returns>Entier</returns>
        public int enabledDesabledObject()
        {
            int intOk = 12;
            if (this.ReadFileParametersUser()[2].Equals("0")) intOk = 0;
            else if (this.ReadFileParametersUser()[2].Equals("1")) intOk = 1;
            else if (this.ReadFileParametersUser()[2].Equals("2")) intOk = 2;
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

            if (username.Equals("superuser") && password.Equals("superpassword"))
            {
                tbValue[0] = "";
                tbValue[1] = "";
                tbValue[2] = "0";
                tbValue[3] = "";

                Factory.IsAdmin = true;

                StreamWriter write = new StreamWriter(updateCreateDirectory("JOVIA_CLIENT") + @"\parametresUser.par", false);
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
                string groupeUser = "";

                if (dr.Read())
                {
                    tbValue[0] = Convert.ToString(dr["idCatUser"]);
                    tbValue[1] = Convert.ToString(dr["designationCat"]);
                    groupeUser = Convert.ToString(dr["groupe"]).ToUpper();
                    if (groupeUser.Equals("ADMINISTRATEUR")) tbValue[2] = "0";//Admin ou superAdmin
                    else if (groupeUser.Equals("RECENSEUR")) tbValue[2] = "1";//Recensseur
                    else if (groupeUser.Equals("AGENT DE COMMUNE")) tbValue[2] = "2";//Agent de commune
                    tbValue[3] = Convert.ToString(dr["id_personne"]);
                    okActivateUser = Convert.ToBoolean(dr["activation"]);

                    //Si desvaleurs sont trouvee et que la personne se connecte tout en etant active ,on les inscrits 
                    //dans un fichier text dont le contenu sera supprime apres deconnection de l'utilisateur
                    if (okActivateUser)
                    {
                        if (groupeUser.Equals("ADMINISTRATEUR")) Factory.IsAdmin = true;
                        else Factory.IsAdmin = false;

                        StreamWriter write = new StreamWriter(updateCreateDirectory("JOVIA_CLIENT") + @"\parametresUser.par", false);
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
            StreamWriter write = new StreamWriter(updateCreateDirectory("JOVIA_CLIENT") + @"\parametresUser.par", false);
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
            StreamReader read = new StreamReader(updateCreateDirectory("JOVIA_CLIENT") + @"\parametresUser.par");
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

        #region VERIFICATION DE L'AUTHENTIFICATION DE L'AGENT ET RECUPERATION DE SA CATEGORIE + INSCRIPTION ET SUPPRESSION DANS UN FICHIER TXT COTE SMS
        /// <summary>
        /// Permet de verifier les paramètres de connexion de l'utilisateur, donc username et password
        /// et retourne un tableau contenant successivement l'Id de la catégorie utilisateur, la désignation
        /// de la catégorie, le groupe de l'utilisateur ainsi que son Id en tant que personne
        /// </summary>
        /// <param name="username">String nom d'utilisateur</param>
        /// <param name="password">String mot de passe</param>
        /// <returns>Tableau des string</returns>
        private string[] verifieLoginUserSMS(string username, string password)
        {
            string[] tbValue;
            bool valueUser = false;

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdVeriLoginUI = con.CreateCommand();
            cmdVeriLoginUI.CommandText = @"SELECT c.id AS idCatUser,c.designation AS designationCat,c.groupe AS gpUser,u.id_personne AS id_personne,u.activation
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
            tbValue = new string[4];
            if (dr.Read())
            {
                tbValue[0] = Convert.ToString(dr["idCatUser"]);
                tbValue[1] = Convert.ToString(dr["designationCat"]);
                tbValue[2] = Convert.ToString(dr["gpUser"]);
                tbValue[3] = Convert.ToString(dr["id_personne"]);
                valueUser = Convert.ToBoolean(dr["activation"]);

                if (!valueUser)
                {
                    tbValue[0] = null;
                    tbValue[1] = null;
                    tbValue[2] = null;
                    tbValue[3] = null;
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
            return tbValue;
        }

        #endregion

        #region ParametreProvince(Operation sur les ParametreProvince)
        private static ParametreProvince getParametreProvince(IDataReader dr)
        {
            ParametreProvince parametreProvince = new ParametreProvince();
            parametreProvince.Id = Convert.ToInt32(dr["id"]);
            parametreProvince.Id_province = Convert.ToInt32(dr["id_province"]);
            parametreProvince.Designation = Convert.ToString(dr["designation"]);
            return parametreProvince;
        }
        /// <summary>
        /// Retourner tous les Paramètre pour la Province
        /// </summary>
        /// <returns></returns>
        public List<ParametreProvince> GetParametreProvinces()
        {
            List<ParametreProvince> list = new List<ParametreProvince>();

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetParametreProvinces = con.CreateCommand();
            cmdGetParametreProvinces.CommandText = "SELECT * FROM parametreProvince";
            IDataReader dr = cmdGetParametreProvinces.ExecuteReader();
            while (dr.Read())
            {
                list.Add(getParametreProvince(dr));
            }
            dr.Close();
            cmdGetParametreProvinces.Dispose();
            con.Close();
            return list;
        }
        #endregion

        #region Generation du numero d'identification national de la personne
        /// <summary>
        /// Permet de générer un numéro d'identification National pour la personne à enregistrer
        /// </summary>
        /// <returns>string</returns>
        public string generateNumIdNational(int idAvenue)
        {
            Personne personne = new Personne();
            string strProvince = "";

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetDesiProv = con.CreateCommand();
            cmdGetDesiProv.CommandText = "SELECT designation FROM parametreProvince";

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

        #region Recuperation de la photo de la personne
        /// <summary>
        /// Permet de récupéré une photo que devrait être affichée dans un objet imagebox
        /// et reçoit en paramètre l'object de type photo (personne)
        /// </summary>
        /// <param name="obj">Objet de la classe</param>
        /// <returns>Retourne un objet MemoryStream</returns>
        public MemoryStream GetPhotoPersonne(Photo photoPers)
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
            string[] tbNanetable = new string[] { "personne", "villeTerritoire", "communeChefferieSecteur", "quartierLocalite", "avenueVillage", "utilisateur", "carte", "telephone", "categorieUtilisateur", "photo", "parametreProvince", "envoie", "erreurEnvoie" };
            if (obj is Personne) tablename = Convert.ToString(tbNanetable[0]);
            else if (obj is VilleTeritoire) tablename = Convert.ToString(tbNanetable[1]);
            else if (obj is CommuneChefferieSecteur) tablename = Convert.ToString(tbNanetable[2]);
            else if (obj is QuartierLocalite) tablename = Convert.ToString(tbNanetable[3]);
            else if (obj is AvenueVillage) tablename = Convert.ToString(tbNanetable[4]);
            else if (obj is Utilisateur) tablename = Convert.ToString(tbNanetable[5]);
            else if (obj is Carte) tablename = Convert.ToString(tbNanetable[6]);
            else if (obj is Telephone) tablename = Convert.ToString(tbNanetable[7]);
            else if (obj is CategorieUtilisateur) tablename = Convert.ToString(tbNanetable[8]);
            else if (obj is Photo) tablename = Convert.ToString(tbNanetable[9]);
            else if (obj is ParametreProvince) tablename = Convert.ToString(tbNanetable[10]);
            else if (obj is EnvoieSMS) tablename = Convert.ToString(tbNanetable[11]);
            else if (obj is ErreurEnvoie) tablename = Convert.ToString(tbNanetable[12]);
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

        #region Execution de l'enregistrement quelque soit la classe appellante
        /// <summary>
        /// Permet d'enregistrer un item dans la base de données quelque soit le type d'objet
        /// </summary>
        /// <param name="obj">Object</param>
        internal void Save(object obj)
        {
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdSave = null, cmdSave1 = null;
            bool okSave = false;

            if (obj is VilleTeritoire)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = @"insert into villeTerritoire(id,designation,superficie)
                    values(@id,@designation,@superficie)";
                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((VilleTeritoire)obj).Id;
                IDataParameter paramDesi = cmdSave.CreateParameter();
                paramDesi.ParameterName = "@designation";
                paramDesi.Value = ((VilleTeritoire)obj).Designation;
                IDataParameter paramSuperficie = cmdSave.CreateParameter();
                paramSuperficie.ParameterName = "@superficie";
                paramSuperficie.Value = ((VilleTeritoire)obj).Superficie;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramDesi);
                cmdSave.Parameters.Add(paramSuperficie);
            }
            else if (obj is CommuneChefferieSecteur)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = @"insert into communeChefferieSecteur(id,id_villeTerritoire,designation,superficie)
                values(@id,@id_villeTerritoire,@designation,@superficie)";
                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((CommuneChefferieSecteur)obj).Id;
                IDataParameter paramId_villeTerritoire = cmdSave.CreateParameter();
                paramId_villeTerritoire.ParameterName = "@id_villeTerritoire";
                paramId_villeTerritoire.Value = ((CommuneChefferieSecteur)obj).Id_VilleTeritoire;
                IDataParameter paramDesignation = cmdSave.CreateParameter();
                paramDesignation.ParameterName = "@designation";
                paramDesignation.Value = ((CommuneChefferieSecteur)obj).Designation;
                IDataParameter paramSuperficie = cmdSave.CreateParameter();
                paramSuperficie.ParameterName = "@superficie";
                paramSuperficie.Value = ((CommuneChefferieSecteur)obj).Superficie;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramId_villeTerritoire);
                cmdSave.Parameters.Add(paramDesignation);
                cmdSave.Parameters.Add(paramSuperficie);
            }
            else if (obj is QuartierLocalite)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = @"insert into quartierLocalite(id,id_communeChefferieSecteur,designation,superficie) 
                    values(@id,@id_communeChefferieSecteur,@designation,@superficie)";
                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((QuartierLocalite)obj).Id;
                IDataParameter paramId_communeChefferieSecteur = cmdSave.CreateParameter();
                paramId_communeChefferieSecteur.ParameterName = "@id_communeChefferieSecteur";
                paramId_communeChefferieSecteur.Value = ((QuartierLocalite)obj).Id_communeChefferieSecteur;
                IDataParameter paramDesignation = cmdSave.CreateParameter();
                paramDesignation.ParameterName = "@designation";
                paramDesignation.Value = ((QuartierLocalite)obj).Designation;
                IDataParameter paramSuperficie = cmdSave.CreateParameter();
                paramSuperficie.ParameterName = "@superficie";
                paramSuperficie.Value = ((QuartierLocalite)obj).Superficie;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramId_communeChefferieSecteur);
                cmdSave.Parameters.Add(paramDesignation);
                cmdSave.Parameters.Add(paramSuperficie);
            }
            else if (obj is Personne)
            {
                if (((Personne)obj).Id_pere == null && ((Personne)obj).Id_mere == null) { okSave = true; }
                else if (((Personne)obj).Id_pere == ((Personne)obj).Id_mere) throw new Exception("Le père ne peut à la fois être la mère");
                else if (((Personne)obj).Id_mere == ((Personne)obj).Id_pere) throw new Exception("La mère ne peut à la fois être le père");
                else
                {
                    okSave = true;
                }
                if (okSave)
                {
                    cmdSave1 = con.CreateCommand();
                    cmdSave1.CommandText = @"insert into personne(id,id_avenueVillage,nom,postnom,prenom,id_pere,id_mere,sexe,etativil,travail,numeroNational,numero,nombreEnfant,niveauEtude,datenaissance,datedeces,anneeSaved)
                    values(@id,@id_avenueVillage,@nom,@postnom,@prenom,@id_pere,@id_mere,@sexe,@etativil,@travail,@numeroNational,@numero,@nombreEnfant,@niveauEtude,@datenaissance,@datedeces,@anneeSaved)";

                    IDataParameter paramId = cmdSave1.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = ((Personne)obj).Id;
                    IDataParameter paramId_avenueVillage = cmdSave1.CreateParameter();
                    paramId_avenueVillage.ParameterName = "@id_avenueVillage";
                    paramId_avenueVillage.Value = ((Personne)obj).Id_avenueVillage;
                    IDataParameter paramNom = cmdSave1.CreateParameter();
                    paramNom.ParameterName = "@nom";
                    paramNom.Value = ((Personne)obj).Nom;
                    IDataParameter paramPostNom = cmdSave1.CreateParameter();
                    paramPostNom.ParameterName = "@postnom";
                    paramPostNom.Value = ((Personne)obj).Postnom;
                    IDataParameter paramPrenom = cmdSave1.CreateParameter();
                    paramPrenom.ParameterName = "@prenom";
                    paramPrenom.Value = ((Personne)obj).Prenom;
                    IDataParameter paramId_pere = cmdSave1.CreateParameter();
                    paramId_pere.ParameterName = "@id_pere";
                    paramId_pere.Value = ((Personne)obj).Id_pere;
                    IDataParameter paramId_mere = cmdSave1.CreateParameter();
                    paramId_mere.ParameterName = "@id_mere";
                    paramId_mere.Value = ((Personne)obj).Id_mere;
                    IDataParameter paramSexe = cmdSave1.CreateParameter();
                    paramSexe.ParameterName = "@sexe";
                    paramSexe.Value = ((Personne)obj).Sexe;
                    IDataParameter paramEtatCivil = cmdSave1.CreateParameter();
                    paramEtatCivil.ParameterName = "@etativil";
                    paramEtatCivil.Value = ((Personne)obj).EtatCivile;
                    IDataParameter paramTravail = cmdSave1.CreateParameter();
                    paramTravail.ParameterName = "@travail";
                    paramTravail.Value = ((Personne)obj).Travail;
                    IDataParameter paramNumNat = cmdSave1.CreateParameter();
                    paramNumNat.ParameterName = "@numeroNational";
                    paramNumNat.Value = ((Personne)obj).NumeroNational;
                    IDataParameter paramNumero = cmdSave1.CreateParameter();
                    paramNumero.ParameterName = "@numero";
                    paramNumero.Value = ((Personne)obj).Numero;
                    IDataParameter paramNbrEnf = cmdSave1.CreateParameter();
                    paramNbrEnf.ParameterName = "@nombreEnfant";
                    paramNbrEnf.Value = ((Personne)obj).NombreEnfant;
                    IDataParameter paramNiveauEtude = cmdSave1.CreateParameter();
                    paramNiveauEtude.ParameterName = "@niveauEtude";
                    paramNiveauEtude.Value = ((Personne)obj).Niveau;
                    IDataParameter paramDateNaiss = cmdSave1.CreateParameter();
                    paramDateNaiss.ParameterName = "@datenaissance";
                    paramDateNaiss.Value = ((Personne)obj).Datenaissance;
                    IDataParameter paramDateDeces = cmdSave1.CreateParameter();
                    paramDateDeces.ParameterName = "@datedeces";
                    paramDateDeces.Value = ((Personne)obj).Datedeces;
                    IDataParameter paramAnneeSaved = cmdSave1.CreateParameter();
                    paramAnneeSaved.ParameterName = "@anneeSaved";
                    paramAnneeSaved.Value = ((Personne)obj).AnneeSaved;

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
            else if (obj is Photo)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = "insert into photo(id,id_personne,photo) values(@id,@id_personne,@photo)";

                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((Photo)obj).Id;
                IDataParameter paramId_personne = cmdSave.CreateParameter();
                paramId_personne.ParameterName = "@id_personne";
                paramId_personne.Value = ((Photo)obj).Id_personne;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramId_personne);

                if ((((Photo)obj).PhotoPersonne) == null)
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
                    paramPhoto.Value = GetImage(((Photo)obj).PhotoPersonne);
                    cmdSave.Parameters.Add(paramPhoto);
                }
            }
            else if (obj is AvenueVillage)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = @"insert into avenueVillage(id,id_quartierLocalite,designation)
                values(@id,@id_quartierLocalite,@designation)";

                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((AvenueVillage)obj).Id;
                IDataParameter paramId_quartierLocalite = cmdSave.CreateParameter();
                paramId_quartierLocalite.ParameterName = "@id_quartierLocalite";
                paramId_quartierLocalite.Value = ((AvenueVillage)obj).Id_quartierLocalite;
                IDataParameter paramDesignation = cmdSave.CreateParameter();
                paramDesignation.ParameterName = "@designation";
                paramDesignation.Value = ((AvenueVillage)obj).Designation;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramId_quartierLocalite);
                cmdSave.Parameters.Add(paramDesignation);
            }
            else if (obj is Carte)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = @"insert into carte(id,id_personne,datelivraison) 
                values(@id,@id_personne,@datelivraison)";

                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((Carte)obj).Id;
                IDataParameter paramId_personne = cmdSave.CreateParameter();
                paramId_personne.ParameterName = "@id_personne";
                paramId_personne.Value = ((Carte)obj).Id_personne;
                IDataParameter paramDatelivraison = cmdSave.CreateParameter();
                paramDatelivraison.ParameterName = "@datelivraison";
                paramDatelivraison.Value = ((Carte)obj).Datelivraison;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramId_personne);
                cmdSave.Parameters.Add(paramDatelivraison);
            }
            else if (obj is ErreurEnvoie)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = @"insert into erreurEnvoie(id,expediteur,message,date_envoie,erreur)
                values(@id,@expediteur,@message,@date_envoie,@erreur)";

                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((ErreurEnvoie)obj).Id;
                IDataParameter paramExpediteur = cmdSave.CreateParameter();
                paramExpediteur.ParameterName = "@expediteur";
                paramExpediteur.Value = ((ErreurEnvoie)obj).Expediteur;
                IDataParameter paramMessage = cmdSave.CreateParameter();
                paramMessage.ParameterName = "@message";
                paramMessage.Value = ((ErreurEnvoie)obj).Message;
                IDataParameter paramDate_envoie = cmdSave.CreateParameter();
                paramDate_envoie.ParameterName = "@date_envoie";
                paramDate_envoie.Value = ((ErreurEnvoie)obj).Date_envoie;
                IDataParameter paramErreur = cmdSave.CreateParameter();
                paramErreur.ParameterName = "@erreur";
                paramErreur.Value = ((ErreurEnvoie)obj).Erreur;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramExpediteur);
                cmdSave.Parameters.Add(paramMessage);
                cmdSave.Parameters.Add(paramDate_envoie);
                cmdSave.Parameters.Add(paramErreur);
            }
            else if (obj is Utilisateur)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = @"insert into utilisateur(id,id_personne,id_categorieUtilisateur,activation,nomuser,motpass)
                values(@id,@id_personne,@id_categorieUtilisateur,@activation,@nomuser,@motpass)";

                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((Utilisateur)obj).Id;
                IDataParameter paramId_personne = cmdSave.CreateParameter();
                paramId_personne.ParameterName = "@id_personne";
                paramId_personne.Value = ((Utilisateur)obj).Id_personne;
                IDataParameter paramId_categorieUtilisateur = cmdSave.CreateParameter();
                paramId_categorieUtilisateur.ParameterName = "@id_categorieUtilisateur";
                paramId_categorieUtilisateur.Value = ((Utilisateur)obj).Id_categorieUtilisateur;
                IDataParameter paramActivation = cmdSave.CreateParameter();
                paramActivation.ParameterName = "@activation";
                paramActivation.Value = ((Utilisateur)obj).Activation;
                IDataParameter paramNomuser = cmdSave.CreateParameter();
                paramNomuser.ParameterName = "@nomuser";
                paramNomuser.Value = ((Utilisateur)obj).Nomuser;
                IDataParameter paramMotpass = cmdSave.CreateParameter();
                paramMotpass.ParameterName = "@motpass";
                paramMotpass.Value = ((Utilisateur)obj).Motpass;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramId_personne);
                cmdSave.Parameters.Add(paramId_categorieUtilisateur);
                cmdSave.Parameters.Add(paramActivation);
                cmdSave.Parameters.Add(paramNomuser);
                cmdSave.Parameters.Add(paramMotpass);
            }
            else if (obj is CategorieUtilisateur)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = @"insert into categorieUtilisateur(id,designation,groupe)
                values(@id,@designation,@groupe)";

                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((CategorieUtilisateur)obj).Id;
                IDataParameter paramDesignation = cmdSave.CreateParameter();
                paramDesignation.ParameterName = "@designation";
                paramDesignation.Value = ((CategorieUtilisateur)obj).Designation;
                IDataParameter paramGroupe = cmdSave.CreateParameter();
                paramGroupe.ParameterName = "@groupe";
                paramGroupe.Value = ((CategorieUtilisateur)obj).Groupe;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramDesignation);
                cmdSave.Parameters.Add(paramGroupe);
            }
            else if (obj is Telephone)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = @"insert into telephone(id,numero,id_utilisateur) 
                values(@id,@numero,@id_utilisateur)";

                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((Telephone)obj).Id;
                IDataParameter paramNumero = cmdSave.CreateParameter();
                paramNumero.ParameterName = "@numero";
                paramNumero.Value = ((Telephone)obj).Numero;
                IDataParameter paramId_utilisateur = cmdSave.CreateParameter();
                paramId_utilisateur.ParameterName = "@id_utilisateur";
                paramId_utilisateur.Value = ((Telephone)obj).Id_utilisateur;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramNumero);
                cmdSave.Parameters.Add(paramId_utilisateur);
            }
            else if (obj is ParametreProvince)
            {
                cmdSave = con.CreateCommand();
                cmdSave.CommandText = @"insert into parametreProvince(id,id_province,designation)
                    values(@id,@id_province,@designation)";
                IDataParameter paramId = cmdSave.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((ParametreProvince)obj).Id;
                IDataParameter paramIdProv = cmdSave.CreateParameter();
                paramIdProv.ParameterName = "@id_province";
                paramIdProv.Value = ((ParametreProvince)obj).Id_province;
                IDataParameter paramDesi = cmdSave.CreateParameter();
                paramDesi.ParameterName = "@designation";
                paramDesi.Value = ((ParametreProvince)obj).Designation;
                cmdSave.Parameters.Add(paramId);
                cmdSave.Parameters.Add(paramIdProv);
                cmdSave.Parameters.Add(paramDesi);
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

            if (obj is VilleTeritoire)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = "update villeTerritoire set designation=@designation,superficie=@superficie where id=@id";

                IDataParameter paramDesi = cmdUpdate.CreateParameter();
                paramDesi.ParameterName = "@designation";
                paramDesi.Value = ((VilleTeritoire)obj).Designation;
                IDataParameter paramSuperficie = cmdUpdate.CreateParameter();
                paramSuperficie.ParameterName = "@superficie";
                paramSuperficie.Value = ((VilleTeritoire)obj).Superficie;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((VilleTeritoire)obj).Id;

                cmdUpdate.Parameters.Add(paramDesi);
                cmdUpdate.Parameters.Add(paramSuperficie);
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is CommuneChefferieSecteur)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = "update communeChefferieSecteur set id_villeTerritoire=@id_villeTerritoire,designation=@designation,superficie=@superficie where id=@id";

                IDataParameter paramId_villeTerritoire = cmdUpdate.CreateParameter();
                paramId_villeTerritoire.ParameterName = "@id_villeTerritoire";
                paramId_villeTerritoire.Value = ((CommuneChefferieSecteur)obj).Id_VilleTeritoire;
                IDataParameter paramDesignation = cmdUpdate.CreateParameter();
                paramDesignation.ParameterName = "@designation";
                paramDesignation.Value = ((CommuneChefferieSecteur)obj).Designation;
                IDataParameter paramSuperficie = cmdUpdate.CreateParameter();
                paramSuperficie.ParameterName = "@superficie";
                paramSuperficie.Value = ((CommuneChefferieSecteur)obj).Superficie;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((CommuneChefferieSecteur)obj).Id;

                cmdUpdate.Parameters.Add(paramId_villeTerritoire);
                cmdUpdate.Parameters.Add(paramDesignation);
                cmdUpdate.Parameters.Add(paramSuperficie);
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is QuartierLocalite)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = "update quartierLocalite set id_communeChefferieSecteur=@id_communeChefferieSecteur,designation=@designation,superficie=@superficie where id=@id";

                IDataParameter paramId_communeChefferieSecteur = cmdUpdate.CreateParameter();
                paramId_communeChefferieSecteur.ParameterName = "@id_communeChefferieSecteur";
                paramId_communeChefferieSecteur.Value = ((QuartierLocalite)obj).Id_communeChefferieSecteur;
                IDataParameter paramDesignation = cmdUpdate.CreateParameter();
                paramDesignation.ParameterName = "@designation";
                paramDesignation.Value = ((QuartierLocalite)obj).Designation;
                IDataParameter paramSuperficie = cmdUpdate.CreateParameter();
                paramSuperficie.ParameterName = "@superficie";
                paramSuperficie.Value = ((QuartierLocalite)obj).Superficie;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((QuartierLocalite)obj).Id;

                cmdUpdate.Parameters.Add(paramId_communeChefferieSecteur);
                cmdUpdate.Parameters.Add(paramDesignation);
                cmdUpdate.Parameters.Add(paramSuperficie);
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is Personne)
            {
                if (((Personne)obj).Id_pere == null && ((Personne)obj).Id_mere == null) { okUpdate = true; }
                else if (((Personne)obj).Id_pere == ((Personne)obj).Id_mere) throw new Exception("Le père ne peut à la fois être la mère");
                else if (((Personne)obj).Id_mere == ((Personne)obj).Id_pere) throw new Exception("La mère ne peut à la fois être le père");
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
                    paramId_avenueVillage.Value = ((Personne)obj).Id_avenueVillage;
                    IDataParameter paramNom = cmdUpdate.CreateParameter();
                    paramNom.ParameterName = "@nom";
                    paramNom.Value = ((Personne)obj).Nom;
                    IDataParameter paramPostNom = cmdUpdate.CreateParameter();
                    paramPostNom.ParameterName = "@postnom";
                    paramPostNom.Value = ((Personne)obj).Postnom;
                    IDataParameter paramPrenom = cmdUpdate.CreateParameter();
                    paramPrenom.ParameterName = "@prenom";
                    paramPrenom.Value = ((Personne)obj).Prenom;
                    IDataParameter paramId_pere = cmdUpdate.CreateParameter();
                    paramId_pere.ParameterName = "@id_pere";
                    paramId_pere.Value = ((Personne)obj).Id_pere;
                    IDataParameter paramId_mere = cmdUpdate.CreateParameter();
                    paramId_mere.ParameterName = "@id_mere";
                    paramId_mere.Value = ((Personne)obj).Id_mere;
                    IDataParameter paramSexe = cmdUpdate.CreateParameter();
                    paramSexe.ParameterName = "@sexe";
                    paramSexe.Value = ((Personne)obj).Sexe;
                    IDataParameter paramEtatCivil = cmdUpdate.CreateParameter();
                    paramEtatCivil.ParameterName = "@etativil";
                    paramEtatCivil.Value = ((Personne)obj).EtatCivile;
                    IDataParameter paramTravail = cmdUpdate.CreateParameter();
                    paramTravail.ParameterName = "@travail";
                    paramTravail.Value = ((Personne)obj).Travail;
                    IDataParameter paramNumero = cmdUpdate.CreateParameter();
                    paramNumero.ParameterName = "@numero";
                    paramNumero.Value = ((Personne)obj).Numero;
                    IDataParameter paramNbrEnf = cmdUpdate.CreateParameter();
                    paramNbrEnf.ParameterName = "@nombreEnfant";
                    paramNbrEnf.Value = ((Personne)obj).NombreEnfant;
                    IDataParameter paramNiveauEtude = cmdUpdate.CreateParameter();
                    paramNiveauEtude.ParameterName = "@niveauEtude";
                    paramNiveauEtude.Value = ((Personne)obj).Niveau;
                    IDataParameter paramDateNaiss = cmdUpdate.CreateParameter();
                    paramDateNaiss.ParameterName = "@datenaissance";
                    paramDateNaiss.Value = ((Personne)obj).Datenaissance;
                    IDataParameter paramDateDeces = cmdUpdate.CreateParameter();
                    paramDateDeces.ParameterName = "@datedeces";
                    paramDateDeces.Value = ((Personne)obj).Datedeces;
                    IDataParameter paramId = cmdUpdate.CreateParameter();
                    paramId.ParameterName = "@id";
                    paramId.Value = ((Personne)obj).Id;

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
            else if (obj is Photo)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = "update photo set id_personne=@id_personne,photo=@photo where id=@id";

                IDataParameter paramId_personne = cmdUpdate.CreateParameter();
                paramId_personne.ParameterName = "@id_personne";
                paramId_personne.Value = ((Photo)obj).Id_personne;

                cmdUpdate.Parameters.Add(paramId_personne);

                if ((((Photo)obj).PhotoPersonne) == null)
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
                    paramPhoto.Value = GetImage(((Photo)obj).PhotoPersonne);
                    cmdUpdate.Parameters.Add(paramPhoto);
                }
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((Photo)obj).Id;
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is AvenueVillage)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = "update avenueVillage set id_quartierLocalite=@id_quartierLocalite,designation=@designation " +
                "where id=@id";

                IDataParameter paramId_quartierLocalite = cmdUpdate.CreateParameter();
                paramId_quartierLocalite.ParameterName = "@id_quartierLocalite";
                paramId_quartierLocalite.Value = ((AvenueVillage)obj).Id_quartierLocalite;
                IDataParameter paramDesignation = cmdUpdate.CreateParameter();
                paramDesignation.ParameterName = "@designation";
                paramDesignation.Value = ((AvenueVillage)obj).Designation;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((AvenueVillage)obj).Id;

                cmdUpdate.Parameters.Add(paramId_quartierLocalite);
                cmdUpdate.Parameters.Add(paramDesignation);
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is Utilisateur)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = @"update utilisateur set id_personne=@id_personne,
                id_categorieUtilisateur=@id_categorieUtilisateur,activation=@activation,nomuser=@nomuser where id=@id";

                IDataParameter paramId_personne = cmdUpdate.CreateParameter();
                paramId_personne.ParameterName = "@id_personne";
                paramId_personne.Value = ((Utilisateur)obj).Id_personne;
                IDataParameter paramId_categorieUtilisateur = cmdUpdate.CreateParameter();
                paramId_categorieUtilisateur.ParameterName = "@id_categorieUtilisateur";
                paramId_categorieUtilisateur.Value = ((Utilisateur)obj).Id_categorieUtilisateur;
                IDataParameter paramActivation = cmdUpdate.CreateParameter();
                paramActivation.ParameterName = "@activation";
                paramActivation.Value = ((Utilisateur)obj).Activation;
                IDataParameter paramNomuser = cmdUpdate.CreateParameter();
                paramNomuser.ParameterName = "@nomuser";
                paramNomuser.Value = ((Utilisateur)obj).Nomuser;
                //IDataParameter paramMotpass = cmdUpdate.CreateParameter();
                //paramMotpass.ParameterName = "@motpass";
                //paramMotpass.Value = ((UtilisateurServeur)obj).Motpass;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((Utilisateur)obj).Id;

                cmdUpdate.Parameters.Add(paramId_personne);
                cmdUpdate.Parameters.Add(paramId_categorieUtilisateur);
                cmdUpdate.Parameters.Add(paramActivation);
                cmdUpdate.Parameters.Add(paramNomuser);
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is Carte)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = "update carte set id_personne=@id_personne,datelivraison=@datelivraison " +
                "where id=@id";

                IDataParameter paramId_personne = cmdUpdate.CreateParameter();
                paramId_personne.ParameterName = "@id_personne";
                paramId_personne.Value = ((Carte)obj).Id_personne;
                IDataParameter paramDatelivraison = cmdUpdate.CreateParameter();
                paramDatelivraison.ParameterName = "@datelivraison";
                paramDatelivraison.Value = ((Carte)obj).Datelivraison;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((Carte)obj).Id;

                cmdUpdate.Parameters.Add(paramId_personne);
                cmdUpdate.Parameters.Add(paramDatelivraison);
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is CategorieUtilisateur)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = "update categorieUtilisateur set designation=@designation,groupe=@groupe " +
                "where id=@id";

                IDataParameter paramDesignation = cmdUpdate.CreateParameter();
                paramDesignation.ParameterName = "@designation";
                paramDesignation.Value = ((CategorieUtilisateur)obj).Designation;
                IDataParameter paramGroupe = cmdUpdate.CreateParameter();
                paramGroupe.ParameterName = "@groupe";
                paramGroupe.Value = ((CategorieUtilisateur)obj).Groupe;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((CategorieUtilisateur)obj).Id;

                cmdUpdate.Parameters.Add(paramDesignation);
                cmdUpdate.Parameters.Add(paramGroupe);
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is Telephone)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = "update telephone set id_utilisateur=@id_utilisateur,numero=@numero " +
                "where id=@id";

                IDataParameter paramNumero = cmdUpdate.CreateParameter();
                paramNumero.ParameterName = "@numero";
                paramNumero.Value = ((Telephone)obj).Numero;
                IDataParameter paramId_utilisateur = cmdUpdate.CreateParameter();
                paramId_utilisateur.ParameterName = "@id_utilisateur";
                paramId_utilisateur.Value = ((Telephone)obj).Id_utilisateur;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((Telephone)obj).Id;

                cmdUpdate.Parameters.Add(paramNumero);
                cmdUpdate.Parameters.Add(paramId_utilisateur);
                cmdUpdate.Parameters.Add(paramId);
            }
            else if (obj is ParametreProvince)
            {
                cmdUpdate = con.CreateCommand();
                cmdUpdate.CommandText = @"update parametreProvince set id_province=@id_province,designation=@designation where id=@id";

                IDataParameter paramIdProv = cmdUpdate.CreateParameter();
                paramIdProv.ParameterName = "@id_province";
                paramIdProv.Value = ((ParametreProvince)obj).Id_province;
                IDataParameter paramDesi = cmdUpdate.CreateParameter();
                paramDesi.ParameterName = "@designation";
                paramDesi.Value = ((ParametreProvince)obj).Designation;
                IDataParameter paramId = cmdUpdate.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((ParametreProvince)obj).Id;
                cmdUpdate.Parameters.Add(paramIdProv);
                cmdUpdate.Parameters.Add(paramDesi);
                cmdUpdate.Parameters.Add(paramId);
            }

            if (!ok || okUpdate)
            {
                int valueUpdate = cmdUpdate.ExecuteNonQuery();
                cmdUpdate.Dispose();
                if (valueUpdate == 0) throw new Exception("rassurez vous que cet enregistrement existe réellement et réessayez svp");
            }
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

            IDataParameter paramNomuser1 = cmdUpdateUserName.CreateParameter();
            paramNomuser1.ParameterName = "@nomuser";
            paramNomuser1.Value = newNameUser;
            IDataParameter paramId = cmdUpdateUserName.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = idUtilisateur;

            cmdUpdateUserName.Parameters.Add(paramNomuser1);
            cmdUpdateUserName.Parameters.Add(paramId);

            int misJour = cmdUpdateUserName.ExecuteNonQuery();
            cmdUpdateUserName.Dispose();
            con.Close();
            if (misJour == 0) throw new Exception("rassurez vous que cet enregistrement existe réellement et réessayez svp");
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

            IDataParameter paramMotpass = cmdUpdatePasswordUser.CreateParameter();
            paramMotpass.ParameterName = "@motpass";
            paramMotpass.Value = password;
            IDataParameter paramId = cmdUpdatePasswordUser.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = idUtilisateur;

            cmdUpdatePasswordUser.Parameters.Add(paramMotpass);
            cmdUpdatePasswordUser.Parameters.Add(paramId);

            int misJour = cmdUpdatePasswordUser.ExecuteNonQuery();
            cmdUpdatePasswordUser.Dispose();
            con.Close();
            if (misJour == 0) throw new Exception("rassurez vous que cet enregistrement existe réellement et réessayez svp");
        }
        #endregion

        #region Execution de la modification de la personne avec une date de deces
        /// <summary>
        /// Permet de modifier une personne en ajoutant la date de décès
        /// </summary>
        /// <param name="obj">Object Personne</param>
        internal void UpdateDeces(Personne pers)
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
            int misJour = cmdUpdateDeces.ExecuteNonQuery();
            cmdUpdateDeces.Dispose();
            con.Close();
            if (misJour == 0) throw new Exception("rassurez vous que cet enregistrement existe réellement et réessayez svp");
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

            if (obj is VilleTeritoire)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from villeTerritoire where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((VilleTeritoire)obj).Id;
                cmdDelete.Parameters.Add(paramId);
            }
            else if (obj is CommuneChefferieSecteur)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from communeChefferieSecteur where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((CommuneChefferieSecteur)obj).Id;
                cmdDelete.Parameters.Add(paramId);
            }
            else if (obj is QuartierLocalite)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from quartierLocalite where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((QuartierLocalite)obj).Id;
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
            else if (obj is VilleTeritoire)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from villeTerritoire where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((VilleTeritoire)obj).Id;
                cmdDelete.Parameters.Add(paramId);
            }
            else if (obj is AvenueVillage)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from avenueVillage where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((AvenueVillage)obj).Id;
                cmdDelete.Parameters.Add(paramId);
            }
            else if (obj is Utilisateur)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from utilisateur where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((Utilisateur)obj).Id;
                cmdDelete.Parameters.Add(paramId);
            }
            else if (obj is CategorieUtilisateur)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from categorieUtilisateur where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((CategorieUtilisateur)obj).Id;
                cmdDelete.Parameters.Add(paramId);
            }
            else if (obj is Telephone)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from telephone where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((Telephone)obj).Id;
                cmdDelete.Parameters.Add(paramId);
            }
            else if (obj is ParametreProvince)
            {
                cmdDelete = con.CreateCommand();
                cmdDelete.CommandText = "delete from parametreProvince where id=@id";

                IDataParameter paramId = cmdDelete.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((ParametreProvince)obj).Id;
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

        #region Personne(Operation sur la personne)
        /// <summary>
        /// Permet de retourner un personne suivant son identifiant passé en paramètre
        /// </summary>
        /// <param name="id">Indetifinat de la Personne</param>
        /// <returns>Une personne</returns>
        public Personne GetPersonne(int id)
        {
            Personne personne = null;

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetPersonne = con.CreateCommand();
            cmdGetPersonne.CommandText = "select * from personne where id=@id";
            IDataParameter paramId = cmdGetPersonne.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            cmdGetPersonne.Parameters.Add(paramId);

            IDataReader dr = cmdGetPersonne.ExecuteReader();
            if (dr.Read()) personne = getPersonne(dr);

            dr.Close();
            cmdGetPersonne.Dispose();
            con.Close();
            return personne;
        }

        private static Personne getPersonne(IDataReader dr)
        {
            Personne personne = new Personne();
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
        public List<Personne> GetPersonnes()
        {
            List<Personne> list = new List<Personne>();

            con.Close();
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();
            IDbCommand cmdGetPersonnes = con.CreateCommand();
            cmdGetPersonnes.CommandText = "SELECT * FROM personne ORDER BY id ASC";
            IDataReader dr = cmdGetPersonnes.ExecuteReader();
            while (dr.Read())
            {
                list.Add(getPersonne(dr));
            }
            dr.Close();
            cmdGetPersonnes.Dispose();
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

        public int GetIdPersonne(string numeroNational)
        {
            int intIdentifiant = 0;

            con.Close();
            con.Open();

            IDbCommand cmdGetIdPersonne = con.CreateCommand();
            cmdGetIdPersonne.CommandText = "SELECT id FROM personne WHERE numeroNational=@numeroNational";

            IDataParameter paramNumeroNational = cmdGetIdPersonne.CreateParameter();
            paramNumeroNational.ParameterName = "@numeroNational";
            paramNumeroNational.Value = numeroNational;
            cmdGetIdPersonne.Parameters.Add(paramNumeroNational);

            IDataReader dr = cmdGetIdPersonne.ExecuteReader();

            if (dr.Read()) intIdentifiant = Convert.ToInt32(dr["id"]);
            else { }
            dr.Close();
            cmdGetIdPersonne.Dispose();
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
        public List<Personne> SearchPersonne(string numeroNational)
        {
            List<Personne> list = new List<Personne>();

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();


            IDbCommand cmdSearchPersonne = con.CreateCommand();
            cmdSearchPersonne.CommandText = @"SELECT id,id_avenueVillage,id_pere,id_mere,numeroNational,numero,
            nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,nombreEnfant,niveauEtude FROM personne 
            WHERE numeroNational=@numeroNational";
            IDataParameter paramNumeroNat = cmdSearchPersonne.CreateParameter();
            paramNumeroNat.ParameterName = "@numeroNational";
            paramNumeroNat.Value = numeroNational.ToUpper();
            cmdSearchPersonne.Parameters.Add(paramNumeroNat);

            IDataReader dr = cmdSearchPersonne.ExecuteReader();

            if (dr.Read())
            {
                list.Add(getPersonne(dr));
            }
            else throw new Exception("Le numéro National ' " + numeroNational + " ' recherché n'a pas été trouvé dans la base des données");

            dr.Close();
            cmdSearchPersonne.Dispose();
            con.Close();
            return list;
        }
        #endregion

        #region PHOTO Personne(Operation sur la photo de la peronne)
        /// <summary>
        /// Permet de retourner la photo de la peronne suivant son identifiant passé en paramètre
        /// </summary>
        /// <param name="id">Indetifinat de la Personne</param>
        /// <returns>Une photo de la peronne</returns>
        public Photo GetPhoto(int idPersonne)
        {
            Photo photoPers = null;

            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetPhoto = con.CreateCommand();
            cmdGetPhoto.CommandText = "select *from photo where id_personne=@id_personne";

            IDataParameter paramId_personne = cmdGetPhoto.CreateParameter();
            paramId_personne.ParameterName = "@id_personne";
            paramId_personne.Value = idPersonne;
            cmdGetPhoto.Parameters.Add(paramId_personne);

            IDataReader dr = cmdGetPhoto.ExecuteReader();
            if (dr.Read()) photoPers = getPhoto(dr);

            dr.Close();
            cmdGetPhoto.Dispose();
            con.Close();
            return photoPers;
        }

        private static Photo getPhoto(IDataReader dr)
        {
            Photo photoPers = new Photo();
            photoPers.Id = Convert.ToInt32(dr["id"]);
            photoPers.Id_personne = Convert.ToInt32(dr["id_personne"]);
            photoPers.PhotoPersonne = dr["photo"].ToString();
            return photoPers;
        }
        public int GetIdPhoto(int idPersonneServeur, string numeroNational)
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

        #region villeTerritoire(Operation sur la villeTerritoire)
        /// <summary>
        /// Permet de retourner une Ville ou Territoire suivant son identifiant passé en paramètre
        /// </summary>
        /// <param name="id">Indetifinat de la Ville ou du Territoire</param>
        /// <returns>Une Ville Territoire</returns>
        public VilleTeritoire GetVilleTeritoire(int id)
        {
            VilleTeritoire villeter = null;

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

        private static VilleTeritoire getVilleTeritoire(IDataReader dr)
        {
            VilleTeritoire villeter = new VilleTeritoire();
            villeter.Id = Convert.ToInt32(dr["id"]);
            villeter.Designation = dr["designation"].ToString();
            villeter.Superficie = Convert.ToInt32(dr["superficie"]);
            return villeter;
        }
        /// <summary>
        /// Retourner toutes les Villes ou Teritoires
        /// </summary>
        /// <returns></returns>
        public List<VilleTeritoire> GetVilleTeritoires()
        {
            List<VilleTeritoire> list = new List<VilleTeritoire>();

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
        /// l'identifiant de la Province, la dite désignation et sa superficie dans une List
        /// </summary>
        /// <param name="designation">Désignation de la villeTerritoire</param>
        /// <returns>List des Villes Territoires</returns>
        public List<VilleTeritoire> SearchVilleTerritoire(string designation)
        {
            List<VilleTeritoire> list = new List<VilleTeritoire>();
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();


            IDbCommand cmdSearchvilleTerritoire = con.CreateCommand();
            cmdSearchvilleTerritoire.CommandText = "SELECT id,designation,superficie FROM villeTerritoire WHERE designation LIKE @designation";
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
            if (!ok) throw new Exception("L'élement ' " + designation + " ' recherché n'a pas été trouvé dans la base des données");

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
        public CommuneChefferieSecteur GetCommuneChefferieSecteur(int id)
        {
            CommuneChefferieSecteur commSectCheff = null;

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

        private static CommuneChefferieSecteur getCommuneChefferieSecteur(IDataReader dr)
        {
            CommuneChefferieSecteur commCheffeSect = new CommuneChefferieSecteur();
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
        public List<CommuneChefferieSecteur> GetCommuneChefferieSecteurs()
        {
            List<CommuneChefferieSecteur> list = new List<CommuneChefferieSecteur>();

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
        public List<CommuneChefferieSecteur> SearchCommuneChefferieSecteur(string designation)
        {
            List<CommuneChefferieSecteur> list = new List<CommuneChefferieSecteur>();
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
        public QuartierLocalite GetQuartierLocalite(int id)
        {
            QuartierLocalite quartLoc = null;

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

        private static QuartierLocalite getQuartierLocalite(IDataReader dr)
        {
            QuartierLocalite quartierLoc = new QuartierLocalite();
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
        public List<QuartierLocalite> GetQuartierLocalites()
        {
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            List<QuartierLocalite> list = new List<QuartierLocalite>();
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
        public List<QuartierLocalite> SearchQuartierLocalite(string designation)
        {
            List<QuartierLocalite> list = new List<QuartierLocalite>();
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
        /// <returns>une Avenue ou un Village</returns>
        public AvenueVillage GetAvenueVillage(int id)
        {
            AvenueVillage avenueVill = null;

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

        private static AvenueVillage getAvenueVillage(IDataReader dr)
        {
            AvenueVillage avenueVillage = new AvenueVillage();
            avenueVillage.Id = Convert.ToInt32(dr["id"]);
            avenueVillage.Id_quartierLocalite = Convert.ToInt32(dr["id_quartierLocalite"]);
            avenueVillage.Designation = Convert.ToString(dr["designation"]);
            return avenueVillage;
        }

        /// <summary>
        /// retourne tous les AvenueVillage
        /// </summary>
        /// <returns>Liste des Avenues et villages</returns>
        public List<AvenueVillage> GetAvenueVillages()
        {
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            List<AvenueVillage> list = new List<AvenueVillage>();
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
        public List<AvenueVillage> SearchAvenueVillage(string designation)
        {
            List<AvenueVillage> list = new List<AvenueVillage>();
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
        public Utilisateur GetUtilisateur(int id)
        {
            Utilisateur utilisateur = null;

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

        private static Utilisateur getUtilisateur(IDataReader dr)
        {
            Utilisateur ut = new Utilisateur();
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
        public List<Utilisateur> GetUtilisateurs()
        {
            //if (con.State.ToString().Equals("Open")) { }
            //else con.Open();
            con.Close();
            con.Open();

            List<Utilisateur> list = new List<Utilisateur>();
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
        public CategorieUtilisateur GetCategorieUtilisateur(int id)
        {
            CategorieUtilisateur categorieUser = null;

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

        private static CategorieUtilisateur getCategorie(IDataReader dr)
        {
            CategorieUtilisateur cu = new CategorieUtilisateur();
            cu.Id = Convert.ToInt32(dr["id"]);
            cu.Designation = dr["designation"].ToString();
            cu.Groupe = Convert.ToString(dr["groupe"]);
            return cu;
        }
        /// <summary>
        /// Retourner toutes les Categorie des utilisateurs
        /// </summary>
        /// <returns>List des CategorieUtilisateur</returns>
        public List<CategorieUtilisateur> GetCategorieUtilisateurs()
        {
            List<CategorieUtilisateur> list = new List<CategorieUtilisateur>();

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
        private static Telephone getTel(IDataReader dr)
        {
            Telephone tel = new Telephone();
            tel.Id = Convert.ToInt32(dr["id"]);
            tel.Numero = dr["numero"].ToString();
            tel.Id_utilisateur = Convert.ToInt32(dr["id_utilisateur"]);
            return tel;
        }
        /// <summary>
        /// Retourner tous les numéros de téléphone des utilisateurs
        /// </summary>
        /// <returns>List des telephone</returns>
        public List<Telephone> GetTelephones(int value)
        {
            List<Telephone> list = new List<Telephone>();

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
        private static Carte getCarte(IDataReader dr)
        {
            Carte carte = new Carte();
            carte.Id = Convert.ToInt32(dr["id"]);
            carte.Id_personne = Convert.ToInt32(dr["id_personne"]);
            carte.Datelivraison = Convert.ToDateTime(dr["datelivraison"]);
            return carte;
        }
        /// <summary>
        /// Retourner tout les cartes
        /// </summary>
        /// <returns>List des cartes</returns>
        public List<Carte> GetCartes()
        {
            List<Carte> list = new List<Carte>();

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

        #region ErreurEnvoie(operation sur l'Envoie d'un message avec erreur)
        private static ErreurEnvoie getErreurEnvoie(IDataReader dr)
        {
            ErreurEnvoie erreurSend = new ErreurEnvoie();
            erreurSend.Id = Convert.ToInt32(dr["id"]);
            erreurSend.Expediteur = dr["expediteur"].ToString();
            erreurSend.Message = dr["message"].ToString();
            erreurSend.Date_envoie = Convert.ToDateTime(dr["date_envoie"]);
            erreurSend.Erreur = dr["erreur"].ToString();
            return erreurSend;
        }
        /// <summary>
        /// retourne tous les erreurs d'envoie
        /// </summary>
        /// <returns></returns>
        public List<ErreurEnvoie> getErreurEnvoies()
        {
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            List<ErreurEnvoie> list = new List<ErreurEnvoie>();
            IDbCommand cmdGetEnvErr = con.CreateCommand();
            cmdGetEnvErr.CommandText = "select *from erreurEnvoie";
            IDataReader dr = cmdGetEnvErr.ExecuteReader();
            while (dr.Read())
            {
                list.Add(getErreurEnvoie(dr));
            }
            dr.Close();
            cmdGetEnvErr.Dispose();
            con.Close();
            return list;
        }
        #endregion

        #region Gestion de l'authentification de l'utilisateur devant envoyer des data par des SMS
        /// <summary>
        /// Vérifie que la personne dont le numéro est passé en paramètre est un recensseur ou non
        /// et retourne vrai ou faux selon les cas
        /// </summary>
        /// <param name="numeroTel"></param>
        /// <returns>Booleen</returns>
        public bool isRecenseur(string numeroTel)
        {
            bool ok = false;
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();
            IDbCommand cmdRecenseur = con.CreateCommand();
            cmdRecenseur.CommandText = @"select groupe from categorieUtilisateur 
            inner join utilisateur on categorieUtilisateur.id=utilisateur.id_categorieUtilisateur
            inner join telephone on utilisateur.id=telephone.id_utilisateur where telephone.numero=@numero";
            IDbDataParameter paramNumTel = cmdRecenseur.CreateParameter();
            paramNumTel.ParameterName = "@numero";
            paramNumTel.Value = numeroTel;
            cmdRecenseur.Parameters.Add(paramNumTel);

            IDataReader rd = cmdRecenseur.ExecuteReader();
            if (rd.Read())
            {
                if ((Convert.ToString(rd["groupe"])).Equals("RECENSEUR")) ok = true;
            }
            return ok;
        }

        public bool AuthentificateUserToSensSMS(string msgRecu)
        {
            bool ok = false;
            DateTime dateOperation;
            string NumTel = "";
            //On recupere les trois valeurs provenant du string passe en argument (toutes les valeurs
            //separee par le symbole |, donc splitees)

            string[] tbvalueMessage = msgRecu.Split(new char[] { '|' });

            //Puis on affecte les valeurs
            NumTel = tbvalueMessage[1];
            dateOperation = Convert.ToDateTime(tbvalueMessage[2]);

            //Affichage de test des valeurs recuperees
            Console.Out.WriteLine("Message complet Authentificate :" + tbvalueMessage[0]);
            Console.Out.WriteLine("numeroExpediteur Authentificate: " + NumTel);
            Console.Out.WriteLine("dateOperation Authentificate   :" + dateOperation);
            try
            {
                //Recuperation des valeurs a inserer dans le first indice 0 du table tbvalueMessage, donc encore
                //spliter ce tableau par le separateur qui sera une virgule 
                //validateur(ici lettre c ou C),nomuser,motpass
                string[] valueToInsert = tbvalueMessage[0].Split(new char[] { ',' });

                string loginUser = valueToInsert[1];
                string passWord = valueToInsert[2];

                #region Connexion de l'agent recensseur
                if (valueToInsert[0].ToLower().Equals("c"))
                {
                    bool okConnect = false;
                    ok = false;
                    //Si la taille du tableau est superieure a 3m on renvoi un message d'erreur
                    if (valueToInsert.Length > 3)
                    {
                        okConnect = false;
                        SMS.Instance.SendOneSMS("Echec de l'authentification, le format du message est incorrects !!", NumTel);
                        //Console.WriteLine("=================Echec de l'authentification, le format du message est incorrects !!");
                    }
                    else
                    {
                        foreach (string str in verifieLoginUserSMS(loginUser, passWord))
                        {
                            if (str == "")
                            {
                                okConnect = false;
                                SMS.Instance.SendOneSMS("Echec de l'authentification, nom d'utilisateur et mot de passe incorrects !!", NumTel);
                                //Console.WriteLine("=================Echec de l'authentification, nom d'utilisateur et mot de passe incorrects !!");
                                break;
                            }
                            else if (str == null)
                            {
                                okConnect = false;
                                SMS.Instance.SendOneSMS("Echec de l'authentification, veuillez contactez l'Administrateur !!", NumTel);
                                //Console.WriteLine("=================Echec de l'authentification, veuillez contactez l'Administrateur !!");
                                break;
                            }
                            else
                            {
                                okConnect = true;
                                break;
                            }
                        }
                    }

                    //Verification si le user est decede ou non ou s'il n'est pas un recenseur
                    if (okConnect)
                    {
                        //Si la personne n'est pas un recenseur
                        if (!verifieLoginUserSMS(loginUser, passWord)[2].Equals("RECENSEUR"))
                        {
                            SMS.Instance.SendOneSMS("Cette personne n'est pas autorisée à utiliser ce service !!", NumTel);
                            //Console.WriteLine("Cette personne n'est pas autorisée à utiliser ce service !!");
                        }
                        else if (GetStatutAgent(Convert.ToInt32(verifieLoginUserSMS(loginUser, passWord)[3])))
                        {
                            //======================La personne est maintenant connectee et n'est pas decede donc elle peut do something

                            try
                            {
                                int idUser = 0;
                                bool findUser = false;

                                if (con.State.ToString().Equals("Open")) { }
                                else con.Open();

                                IDbCommand cmdHeure1 = con.CreateCommand();
                                int tempDebut = DateTime.Now.Hour;
                                int delais = tempDebut + 3;//Trois heure maximum pour rester connecte

                                //On verifie si la personne s'etait authentifie avant et si c'est le cas on fait
                                //un update sinon un insert
                                cmdHeure1.CommandText = "SELECT id FROM parametreAgentSMS WHERE nomuser=@nomuser AND motpass=@motpass";

                                IDataParameter paramNomuser = cmdHeure1.CreateParameter();
                                paramNomuser.ParameterName = "@nomuser";
                                paramNomuser.Value = loginUser;
                                IDataParameter paramMotpass = cmdHeure1.CreateParameter();
                                paramMotpass.ParameterName = "@motpass";
                                paramMotpass.Value = passWord;
                                cmdHeure1.Parameters.Add(paramNomuser);
                                cmdHeure1.Parameters.Add(paramMotpass);

                                IDataReader dr = cmdHeure1.ExecuteReader();

                                if (dr.Read())
                                {
                                    idUser = Convert.ToInt32(dr["id"]);
                                    findUser = true;
                                }
                                dr.Close();
                                cmdHeure1.Dispose();

                                if (findUser)
                                {
                                    //Utilisateur trouve, on fait un UPDATE du temps (Heure) de debut et de fin
                                    IDbCommand cmdHeure2 = con.CreateCommand();
                                    cmdHeure2.CommandText = "UPDATE parametreAgentSMS SET temp_debut=@temp_debut,delais=@delais,numeroTel=@numeroTel WHERE id=@id";

                                    IDataParameter paramTemp_debut = cmdHeure2.CreateParameter();
                                    paramTemp_debut.ParameterName = "@temp_debut";
                                    paramTemp_debut.Value = tempDebut;
                                    IDataParameter paramDelais = cmdHeure2.CreateParameter();
                                    paramDelais.ParameterName = "@delais";
                                    paramDelais.Value = delais;
                                    IDataParameter paramNumeroTel = cmdHeure2.CreateParameter();
                                    paramNumeroTel.ParameterName = "@numeroTel";
                                    paramNumeroTel.Value = NumTel;
                                    IDataParameter paramId = cmdHeure2.CreateParameter();
                                    paramId.ParameterName = "@id";
                                    paramId.Value = idUser;

                                    cmdHeure2.Parameters.Add(paramTemp_debut);
                                    cmdHeure2.Parameters.Add(paramDelais);
                                    cmdHeure2.Parameters.Add(paramNumeroTel);
                                    cmdHeure2.Parameters.Add(paramId);

                                    cmdHeure2.ExecuteNonQuery();
                                    cmdHeure2.Dispose();
                                }
                                else
                                {
                                    int goodId = 0;
                                    //Generation de l'id
                                    IDbCommand cmdRenewID = con.CreateCommand();
                                    cmdRenewID.CommandText = "SELECT MAX(id) AS maxid FROM parametreAgentSMS";

                                    IDataReader dtr = cmdRenewID.ExecuteReader();
                                    if (dtr.Read())
                                    {
                                        if (dtr["maxid"] == DBNull.Value) goodId = 1;
                                        else goodId = Convert.ToInt32(dtr["maxid"]) + 1;
                                    }
                                    dtr.Close();
                                    cmdRenewID.Dispose();
                                    //========================================================================

                                    //Utilisateur non trouve, on fait un INSERT du temps (Heure) de debut et delais
                                    IDbCommand cmdHeure3 = con.CreateCommand();
                                    cmdHeure3.CommandText = "INSERT INTO parametreAgentSMS(id,nomuser,motpass,temp_debut,delais,numeroTel) " +
                                    "VALUES(@id,@nomuser,@motpass,@temp_debut,@delais,@numeroTel)";

                                    IDataParameter paramIdUserSendSMS = cmdHeure3.CreateParameter();
                                    paramIdUserSendSMS.ParameterName = "@id";
                                    paramIdUserSendSMS.Value = goodId;
                                    IDataParameter paramNomuser1 = cmdHeure3.CreateParameter();
                                    paramNomuser1.ParameterName = "@nomuser";
                                    paramNomuser1.Value = loginUser;
                                    IDataParameter paramMotpass1 = cmdHeure3.CreateParameter();
                                    paramMotpass1.ParameterName = "@motpass";
                                    paramMotpass1.Value = passWord;
                                    IDataParameter paramTemp_debut = cmdHeure3.CreateParameter();
                                    paramTemp_debut.ParameterName = "@temp_debut";
                                    paramTemp_debut.Value = tempDebut;
                                    IDataParameter paramDelais = cmdHeure3.CreateParameter();
                                    paramDelais.ParameterName = "@delais";
                                    paramDelais.Value = delais;
                                    IDataParameter paramNumeroTel = cmdHeure3.CreateParameter();
                                    paramNumeroTel.ParameterName = "@numeroTel";
                                    paramNumeroTel.Value = NumTel;
                                    cmdHeure3.Parameters.Add(paramIdUserSendSMS);
                                    cmdHeure3.Parameters.Add(paramNomuser1);
                                    cmdHeure3.Parameters.Add(paramMotpass1);
                                    cmdHeure3.Parameters.Add(paramTemp_debut);
                                    cmdHeure3.Parameters.Add(paramDelais);
                                    cmdHeure3.Parameters.Add(paramNumeroTel);

                                    cmdHeure3.ExecuteNonQuery();
                                    cmdHeure3.Dispose();
                                }
                                SMS.Instance.SendOneSMS("Connexion réussie", NumTel);
                                //Console.WriteLine("Connexion réussie");

                                con.Close();
                            }
                            catch (Exception) { }
                        }
                        else SMS.Instance.SendOneSMS("Cette personne est déjà décédée et ne peut faire aucune opération !!", NumTel);
                        //Console.WriteLine("Cette personne est déjà décédée et ne peut faire aucune opération !!");
                    }
                }
#endregion

                ///////////////Debut Ajout
                #region Si la personne veut juste inserer ou modifier des donnees et pas se connecter
                else if (valueToInsert[0].ToLower().Equals("i") || valueToInsert[0].ToLower().Equals("m") || valueToInsert[0].ToLower().Equals("d"))
                {
                    //Alors on commence a verifier que le delais d'authentification du user utilisant le 
                    //numero de Tel d'envoi n'a pas expire
                    if (con.State.ToString().Equals("Open")) { }
                    else con.Open();
                    IDbCommand cmdVerifeDelaisUser = con.CreateCommand();
                    cmdVerifeDelaisUser.CommandText = "SELECT delais AS delais FROM parametreAgentSMS WHERE numeroTel=@numeroTel";

                    IDataParameter paramNumeroTel = cmdVerifeDelaisUser.CreateParameter();
                    paramNumeroTel.ParameterName = "@numeroTel";
                    paramNumeroTel.Value = NumTel;

                    cmdVerifeDelaisUser.Parameters.Add(paramNumeroTel);
                    IDataReader rd = cmdVerifeDelaisUser.ExecuteReader();
                    if (rd.Read()) 
                    {
                        if (DateTime.Now.Hour < Convert.ToInt32(rd["delais"]))
                        {
                            ok = true;
                        }
                        else
                        {
                            ok = false;
                            SMS.Instance.SendOneSMS("Votre delais d'authentification a exipé, veuillez vous réconnectez à nouveau au système !!", NumTel);
                        }
                    }
                    rd.Close();
                    cmdVerifeDelaisUser.Dispose();
                    con.Close();
                }
                #endregion
                ///////////////Fin Ajout
                else SMS.Instance.SendOneSMS("La synthaxe du message est incorrecte !!", NumTel);
                //Console.WriteLine("Cette personne est déjà décédée et ne peut faire aucune opération !!");
            }
            catch (IndexOutOfRangeException)
            {
                SMS.Instance.SendOneSMS("Echec de l'authentification, le format du message est incorrects !!", NumTel);
                //Console.WriteLine("=================Echec de l'authentification, le format du message est incorrects !!");
            }
            return ok;
        }
        #endregion

        #region METHODE PERMETTANT DE VERIFIER QUE LE NUMERO D'IDENTIFICATION NATIONAL PASSE EN PARAMETRE EXISTE
        public bool VerifieExistanceNumNational(string numeroNational)
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

        #region ENREGISTREMENT D'UN OU PLUSIEURS SMS DANS LA BD
        //Enregistrement d'un sms envoye à un destinataire dans la base de donnes
        /// <summary>
        /// Permet d'enregistrer un message envoyé à un destinataire dans la base de données
        /// </summary>
        /// <param name="obj">Object</param>
        public void SaveOneSMS(object obj)
        {
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdSaveOneSMS = null;

            if (obj is EnvoieSMS)
            {
                cmdSaveOneSMS = con.CreateCommand();
                cmdSaveOneSMS.CommandText = @"insert into envoie(id,numerotelephone,message_envoye,dateenvoie)
                values(@id,@numerotelephone,@message_envoye,@dateenvoie)";

                IDataParameter paramId = cmdSaveOneSMS.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = ((EnvoieSMS)obj).Id;
                IDataParameter paramNumerotelephone = cmdSaveOneSMS.CreateParameter();
                paramNumerotelephone.ParameterName = "@numerotelephone";
                paramNumerotelephone.Value = ((EnvoieSMS)obj).Destinataire;
                IDataParameter paramMessage_envoye = cmdSaveOneSMS.CreateParameter();
                paramMessage_envoye.ParameterName = "@message_envoye";
                paramMessage_envoye.Value = ((EnvoieSMS)obj).MessageEnvoye;
                IDataParameter paramDateenvoie = cmdSaveOneSMS.CreateParameter();
                paramDateenvoie.ParameterName = "@dateenvoie";
                paramDateenvoie.Value = ((EnvoieSMS)obj).DateEnvoie;
                cmdSaveOneSMS.Parameters.Add(paramId);
                cmdSaveOneSMS.Parameters.Add(paramNumerotelephone);
                cmdSaveOneSMS.Parameters.Add(paramMessage_envoye);
                cmdSaveOneSMS.Parameters.Add(paramDateenvoie);
            }
            else if (obj is EnvoieMsgAgent)
            {
                cmdSaveOneSMS = con.CreateCommand();
                cmdSaveOneSMS.CommandText = "insert into envoieMsgAgent(message_envoye,dateenvoie) " +
                "values(@message_envoye,@dateenvoie)";

                IDataParameter paramMessage_envoye = cmdSaveOneSMS.CreateParameter();
                paramMessage_envoye.ParameterName = "@message_envoye";
                paramMessage_envoye.Value = ((EnvoieMsgAgent)obj).Message_envoye;
                IDataParameter paramDateenvoie = cmdSaveOneSMS.CreateParameter();
                paramDateenvoie.ParameterName = "@dateenvoie";
                paramDateenvoie.Value = ((EnvoieMsgAgent)obj).Dateenvoie;
                cmdSaveOneSMS.Parameters.Add(paramMessage_envoye);
                cmdSaveOneSMS.Parameters.Add(paramDateenvoie);
            }
            cmdSaveOneSMS.ExecuteNonQuery();
            cmdSaveOneSMS.Dispose();
            con.Close();
        }

        //Enregistrement d'un sms envoye à plusieurs destinataire dans la base de donnes
        /// <summary>
        /// Permet d'enregistrer un message envoyé à un destinataire dans la base de données
        /// </summary>
        /// <param name="obj">Object</param>
        public void SaveManySMS(object obj, List<string> liste)
        {
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();
            string[] values;
            int goodId = 0;

            IDbCommand cmdSaveManySMS = null;
            EnvoieSMS envsms = new EnvoieSMS();

            //Recuperation dernier Id pour la table
            IDbCommand cmdRenewID = con.CreateCommand();
            cmdRenewID.CommandText = "SELECT MAX(id) AS maxid FROM envoie";

            IDataReader dr = cmdRenewID.ExecuteReader();
            if (dr.Read())
            {
                if (dr["maxid"] == DBNull.Value) goodId = 1;
                else goodId = Convert.ToInt32(dr["maxid"]) + 1;
            }
            dr.Close();
            cmdRenewID.Dispose();

            //Insertion du SMS envoyé dans la BD

            foreach (string str in liste)
            {
                values = str.Split(new char[] { '|' });

                envsms.Id = goodId;
                envsms.Destinataire = values[0];
                envsms.MessageEnvoye = values[1];

                cmdSaveManySMS = con.CreateCommand();
                cmdSaveManySMS.CommandText = "insert into envoie(id,numerotelephone,message_envoye,dateenvoie) " +
                "values(@id,@numerotelephone,@message_envoye,@dateenvoie)";

                IDataParameter paramId = cmdSaveManySMS.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = envsms.Id;
                IDataParameter paramNumerotelephone = cmdSaveManySMS.CreateParameter();
                paramNumerotelephone.ParameterName = "@numerotelephone";
                paramNumerotelephone.Value = envsms.Destinataire;
                IDataParameter paramMessage_envoye = cmdSaveManySMS.CreateParameter();
                paramMessage_envoye.ParameterName = "@message_envoye";
                paramMessage_envoye.Value = envsms.MessageEnvoye;
                IDataParameter paramDateenvoie = cmdSaveManySMS.CreateParameter();
                paramDateenvoie.ParameterName = "@dateenvoie";
                paramDateenvoie.Value = ((EnvoieSMS)obj).DateEnvoie;
                cmdSaveManySMS.Parameters.Add(paramId);
                cmdSaveManySMS.Parameters.Add(paramNumerotelephone);
                cmdSaveManySMS.Parameters.Add(paramMessage_envoye);
                cmdSaveManySMS.Parameters.Add(paramDateenvoie);
                goodId++;
                cmdSaveManySMS.ExecuteNonQuery();
                cmdSaveManySMS.Dispose();
            }
            con.Close();
        }
        #endregion

        #region Recuperation du statut de l'agent qui effectue les operations (Recensseur ou Agent de commune)
        private bool GetStatutAgent(int id)
        {
            bool status = true;
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdGetStatu = con.CreateCommand();
            cmdGetStatu.CommandText = "SELECT datedeces FROM personne WHERE id=@id";

            IDataParameter paramId = cmdGetStatu.CreateParameter();
            paramId.ParameterName = "@id";
            paramId.Value = id;
            cmdGetStatu.Parameters.Add(paramId);

            IDataReader dr = cmdGetStatu.ExecuteReader();

            if (dr.Read())
            {
                if (dr["datedeces"].Equals(DBNull.Value)) status = true;
                else status = false;
            }
            dr.Close();
            cmdGetStatu.Dispose();
            con.Close();
            return status;
        }
        #endregion

        #region CREATION DE LA TABLE QUI SAVE LES PARAM DU USER CONNECTE DEVANT SEND SMS
        /// <summary>
        /// Permet de créer la table qui conserve les utilisateur connectés au système pour enoyer des SMS
        /// </summary>
        public void CreateTableTempUser()
        {
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();
            IDbCommand cmdCreateTb = con.CreateCommand();
            cmdCreateTb.CommandText =
                  @"create table parametreAgentSMS
                    (
	                    id integer,
	                    nomuser varchar(50) not null,
	                    motpass varchar(50) not null,
	                    temp_debut integer not null,
	                    delais integer not null,
	                    numeroTel varchar(14) not null,
	                    constraint pk_parametreAgentSMS primary key(id),
	                    constraint uk_numeroTelAgentRecensseur unique(numeroTel)
                    )";
            cmdCreateTb.ExecuteNonQuery();
            cmdCreateTb.Dispose();
            con.Close();
        }
        #endregion

        #region SUPPRESSION DE LA TABLE QUI SAVE LES PARAM DU USER CONNECTE DEVANT SEND SMS
        /// <summary>
        /// Permet de supprimer la table qui conserve le delai de connection de chaque Agent connecté au syatème
        /// </summary>
        public void DropTableTempUser()
        {
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdCreateTb = con.CreateCommand();
            cmdCreateTb.CommandText = "drop table if exists parametreAgentSMS";
            cmdCreateTb.ExecuteNonQuery();
            cmdCreateTb.Dispose();
            con.Close();
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
        /// <param name="cheminAcces">String chemin d'acces Bd</param>
        /// <param name="lecteur">string</param>
        /// <param name="versionPostDreSQL">string</param>
        /// <returns>string</returns>
        public string BackupLocalDataBase(string cheminAcces,string lecteur, string versionPostDreSQL)
        {
            string requete = "",strMsgPath = "";
            if (string.IsNullOrEmpty(cheminAcces))
            {
                throw new Exception("Le chemin d'accès pour la sauvegarde de la base des données est invalide !!");
            }
            else
            {
                //Bd SQLServer
                lecteur = null;
                versionPostDreSQL = null;
                requete = "USE master " +
                          "BACKUP DATABASE " + con.Database + " " +
                          "TO DISK = N'" + cheminAcces + "' WITH NOFORMAT," +
                          "NOINIT,NAME = N'" + con.Database + "_Complete_BackUpBase'";
                this.ExecuteOneQyery(@requete);
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
        public string RestoreDataBase(string cheminAcces,string lecteur,string versionPostDreSQL)
        {
            string requete = "", strMsgPath = null;
            if (string.IsNullOrEmpty(cheminAcces))
            {
                throw new Exception("Le chemin d'accès pour la restoration de de la base des données est invalide !!");
            }
            else
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
                return strMsgPath;
            }
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
            if (Con.GetType().ToString().Equals("Npgsql.NpgsqlConnection")) sgbd = 1;
            else if (Con.GetType().ToString().Equals("MySql.Data.MySqlClient.MySqlConnection")) sgbd = 2;
            else if (Con.GetType().ToString().Equals("System.Data.SqlClient.SqlConnection")) sgbd = 3;
            else if (Con.GetType().ToString().Equals("System.Data.OleDb.OleDbConnection")) sgbd = 4;
            return sgbd;
        }
        #endregion

        #region EXECUTION DU BACKUP DISTANT
        /// <summary>
        /// Appel de l'exécution de la sauvegarde distante via l'interface cliente
        /// </summary>
        public void ExecuteBackupDistant()
        {
            bool provinceExiste = false;
            int idProvince = 0;
            List<string> listeReq = new List<string>();
            List<string> listeReqUpdate = new List<string>();
            List<byte[]> listPhoto = new List<byte[]>();
            List<byte[]> listPhotoUpdate = new List<byte[]>();
            //On verifie d'abord que la table parametre contient des parametres valides
            if (con.State.ToString().Equals("Open")) { }
            else con.Open();

            IDbCommand cmdVerifieParamProvClient = null, cmdRecupeQuery = null, cmdDeleteValueReq = null, cmdRecupeQueryUpdate = null, cmdDeleteValueReqUpdate = null;

            cmdVerifieParamProvClient = con.CreateCommand();
            cmdVerifieParamProvClient.CommandText = "SELECT id_province FROM parametreProvince";
            IDataReader dr1 = cmdVerifieParamProvClient.ExecuteReader();

            if (dr1.Read())
            {
                idProvince = Convert.ToInt32(dr1["id_province"]);
                provinceExiste = true;
            }

            dr1.Close();
            cmdVerifieParamProvClient.Dispose();

            if (provinceExiste)
            {
                //On recupere les requetes qu'on devra exécuter et on les remplis dans une liste
                //=================== Pour Insertion =====================
                cmdRecupeQuery = con.CreateCommand();
                cmdRecupeQuery.CommandText = "SELECT requete,photo FROM recupQuery ORDER BY id ASC";
                IDataReader dr2 = cmdRecupeQuery.ExecuteReader();
                while (dr2.Read())
                {
                    listeReq.Add(Convert.ToString(dr2["requete"]));
                    listPhoto.Add(ObjectToByteArray(dr2["photo"]));
                }
                dr2.Close();
                cmdRecupeQuery.Dispose();

                //=================== Pour Update =====================
                cmdRecupeQueryUpdate = con.CreateCommand();
                cmdRecupeQueryUpdate.CommandText = "SELECT requete,photo FROM recupQueryUpdate ORDER BY id ASC";
                IDataReader dr3 = cmdRecupeQueryUpdate.ExecuteReader();
                while (dr3.Read())
                {
                    listeReqUpdate.Add(Convert.ToString(dr3["requete"]));
                    listPhotoUpdate.Add(ObjectToByteArray(dr3["photo"]));
                }
                dr3.Close();
                cmdRecupeQueryUpdate.Dispose();
                if (listeReq.Count == 0 && listeReqUpdate.Count == 0) throw new Exception("Il n'ya rien à sauvegarder svp !!");

                else
                    //Appel de l'execution proprement dite de la sauvegarde ou insertion en back
                    Factory1.Instance.executeBackupFromClient(idProvince, listeReq, listPhoto, listeReqUpdate, listPhotoUpdate);

                //On supprime le contenu de la table contenant les requetes pour recevoir des nouvelles valeurs

                cmdDeleteValueReq = con.CreateCommand();
                cmdDeleteValueReq.CommandText = "DELETE FROM recupQuery";
                cmdDeleteValueReq.ExecuteNonQuery();
                cmdDeleteValueReq.Dispose();

                //On recupere les requetes qu'on devra exécuter et on les remplis dans une liste
                //=================== Pour Update =====================
                //On supprime le contenu de la table contenant les requetes pour recevoir des nouvelles valeurs

                cmdDeleteValueReqUpdate = con.CreateCommand();
                cmdDeleteValueReqUpdate.CommandText = "DELETE FROM recupQueryUpdate";
                cmdDeleteValueReqUpdate.ExecuteNonQuery();
                cmdDeleteValueReqUpdate.Dispose();
            }
            else throw new Exception("Impossible d'éffectuer la sauvegarde car les paramètres de la Povince sont invalides, veuillez contacter l'Administrateur du serveur distant");
            con.Close();
        }

        /// <summary>
        /// Permet de convertir un Objet en tqblequ des byte et le retourne
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null) return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
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
                strquery = @"SELECT parametreProvince.designation ,parametreProvince.superficie,(count(personne.id))/POW(parametreProvince.superficie,1) as densitePop
			    FROM parametreProvince,personne 
			    INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			    INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			    INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			    INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire 
			    GROUP BY parametreProvince.designation,parametreProvince.designation ,parametreProvince.superficie";
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
        public string fonctionCalculTauxNatalite(string nomTable, int anneCours,int id)
        {
            string dateJanvier = "1/1/" + anneCours.ToString();
            string dateDecembre = "31/12/" + anneCours.ToString();
            string strquery = "";
            if (nomTable.Equals("Province"))
            {
                strquery = @"SELECT DISTINCT parametreProvince.designation,parametreProvince.superficie,(CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)))/
                        (SELECT (SELECT (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM parametreProvince,personne 
	                        INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
	                        INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
	                        INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
	                        INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
	                        WHERE personne.dateNaissance <= '" + dateDecembre + @"'
	                        AND personne.dateDeces ISNULL )+
                        (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM parametreProvince,personne 
	                        INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
	                        INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
	                        INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
	                        INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
	                        WHERE personne.dateNaissance <= '" + dateJanvier + @"'
	                        AND personne.dateDeces ISNULL ))/2) as TauxNatalite,
                        (EXTRACT(YEAR FROM personne.dateNaissance)) as Annee FROM parametreProvince,personne 
                        INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
                        INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
                        INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
                        INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
                        WHERE (EXTRACT(YEAR FROM personne.dateNaissance))=" + anneCours + @"
                        AND personne.dateDeces ISNULL GROUP BY parametreProvince.designation,parametreProvince.superficie,(EXTRACT(YEAR FROM personne.dateNaissance))";
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
        public string fonctionCalculTauxMortalite(string nomTable, int anneCours,int id)
        {
            string dateJanvier = "1/1/" + anneCours.ToString();
            string dateDecembre = "1/1/" + anneCours.ToString();
            string strquery = "";
            if (nomTable.Equals("Province"))
            {
                strquery =
                @"SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2))/
		                (SELECT (SELECT (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM parametreProvince,personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			                INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			                WHERE personne.dateDeces <= '" + dateDecembre + @"')+
		                (SELECT DISTINCT CAST(COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS NUMERIC(15,2)) AS nombreNaissanceAnnee FROM parametreProvince,personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			                INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			                WHERE personne.dateDeces <= '" + dateJanvier + @"'))/2) 
	                AS TauxMortalite,parametreProvince.designation,parametreProvince.superficie,EXTRACT(YEAR FROM personne.dateDeces)as annee FROM parametreProvince,personne 
	                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
	                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
	                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
	                INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
	                WHERE (EXTRACT(YEAR FROM personne.dateDeces))=" + anneCours + @"
	                AND personne.dateDeces IS NOT NULL GROUP BY parametreProvince.designation,parametreProvince.superficie,EXTRACT(YEAR FROM personne.dateDeces)";
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
            if (obj is ParametreProvince)
            {
                strquery =
                @"SELECT((" + anneeFin + @"-" + anneeDebut + @")*
		            (SELECT SQRT(
			            (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeY FROM parametreProvince,personne 
				            INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
				            INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
				            INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
				            INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
				            WHERE personne.anneeSaved='"+anneeFin+@"')/

			            (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeX FROM parametreProvince,personne 
				            INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
				            INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
				            INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
				            INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
				            WHERE personne.anneeSaved='" + anneeDebut + @"')-1)))*100 
	                AS TauxCroissance,parametreProvince.designation from parametreProvince";
            }
            else if (obj is VilleTeritoire)
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

		                (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeX FROM parametreProvince,personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			                INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			                WHERE personne.anneeSaved='" + anneeDebut + @"')-1)))*100 
                    AS TauxCroissance,villeTerritoire.designation from villeTerritoire";
            }
            else if (obj is CommuneChefferieSecteur)
            {
                strquery =
                @"SELECT((" + anneeFin + @"-" + anneeDebut + @")*
	                (SELECT SQRT(
		                (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeY FROM personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			                WHERE personne.anneeSaved='" + anneeFin + @"' AND communeChefferieSecteur.id=" + ((CommuneChefferieSecteur)obj).Id + @")/

		                (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeX FROM parametreProvince,personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			                INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			                WHERE personne.anneeSaved='" + anneeDebut + @"' AND communeChefferieSecteur.id=" + ((CommuneChefferieSecteur)obj).Id + @")-1)))*100 
                    AS TauxCroissance,communeChefferieSecteur.designation from communeChefferieSecteur";
            }
            else if (obj is QuartierLocalite)
            {
                strquery =
                @"SELECT((" + anneeFin + @"-" + anneeDebut + @")*
	                (SELECT SQRT(
		                (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeY FROM personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			                WHERE personne.anneeSaved='" + anneeFin + @"' AND quartierLocalite.id=" + ((QuartierLocalite)obj).Id + @")/

		                (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeX FROM parametreProvince,personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			                WHERE personne.anneeSaved='" + anneeDebut + @"' AND quartierLocalite.id=" + ((QuartierLocalite)obj).Id + @")-1)))*100 
                    AS TauxCroissance,quartierLocalite.designation from quartierLocalite";
            }
            else if (obj is AvenueVillage)
            {
                strquery =
                @"SELECT((" + anneeFin + @"-" + anneeDebut + @")*
	                (SELECT SQRT(
		                (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeY FROM personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                WHERE personne.anneeSaved='" + anneeFin + @"' AND avenueVillage.id=" + ((AvenueVillage)obj).Id + @")/

		                (SELECT CAST(COUNT(personne.id) AS NUMERIC(15,2)) AS nombrePersonneAnneeX FROM parametreProvince,personne 
			                INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			                WHERE personne.anneeSaved='" + anneeDebut + @"' AND avenueVillage.id=" + ((AvenueVillage)obj).Id + @")-1)))*100 
                    AS TauxCroissance,avenueVillage.designation from avenueVillage";
            }
 
            return strquery;
        }
        #endregion
    }
}
