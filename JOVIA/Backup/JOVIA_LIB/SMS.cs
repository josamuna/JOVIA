using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO.Ports;
using GsmComm.GsmCommunication;
using GsmComm.PduConverter;
using MySql.Data.MySqlClient;
using Npgsql;

namespace JOVIA_LIB
{
    public class SMS
    {
        private static SMS staticFact;
        private static GsmCommMain comm;
        private string messageRecu = "";

        #region Initialisation de factory pour les SMS
        public static SMS Instance
        {
            get
            {
                if (staticFact == null)
                    staticFact = new SMS();
                return staticFact;
            }
        }
        #endregion

        #region Recuperation des tous les ports serie disponibles
        /// <summary>
        /// Permet d'avoir une liste contenant tous les ports series disponibles
        /// </summary>
        /// <returns>Liste des ports</returns>
        public List<string> GetAllports()
        {
            try
            {
                List<string> liste = new List<string>();

                foreach (string portSerie in SerialPort.GetPortNames())
                    liste.Add(portSerie);
                return liste;
            }
            catch (Exception)
            {
                throw new Exception("Impossible de charger tous les ports series");
            }
        }
        #endregion

        #region Ouverture de la connection au port choisit
        /// <summary>
        /// Permet l'ouverture de la connection au port passé en premier paramètre, avec comme vitesse 
        /// du port le second paramètre et comme troisième paramètre le temps de reponse avant ré - connection
        /// </summary>
        /// <param name="port">Numéro de port</param>
        /// <param name="baud">Vitesse de transfère du port</param>
        /// <param name="timeout">Temps de réponse avant réconnection</param>
        public void OpenConnection(int port, int baud, int timeout)
        {
            comm = new GsmCommMain(port, baud, timeout);
            int inc = 0;
            try
            {
                comm.Open();
                //Console.WriteLine("IdentifyDevice : " + comm.IdentifyDevice(),ToString());
                //int i=0;
                //foreach(string str in comm.GetSupportedCharacterSets())
                //{
                //    Console.WriteLine("Char "+ i + " = " + str);
                //        i++;
                //}
                //Console.WriteLine("comm.IsOpen().ToString() : " + comm.IsOpen().ToString());
                //Console.WriteLine("comm.IsConnected().ToString() : " + comm.IsConnected().ToString());
                while (!comm.IsConnected())
                {
                    if (inc > 0)
                    {
                        comm.Close();
                        return;  
                    }
                    inc++;
                    throw new Exception("");
                }

                comm.Close();
            }
            catch (Exception)
            {
                throw new Exception("Echec de l'ouverture du port choisi");
            }
        }
        #endregion

        #region Recuperation du statut de la connection au modem
        /// <summary>
        /// Permet de retourner le statut de la connection au modem
        /// True =>Si elle est ouverte et False dans le cas contraire
        /// </summary>
        /// <returns></returns>
        public bool GetStatusConnectionModem()
        {
            if (comm.IsOpen()) return true;
            else return false;
        }
        #endregion

        #region Fermeture de la connection
        public void CloseConnection()
        {
            if (comm != null || comm.IsOpen() || comm.IsConnected()) comm.Close();
            else { }
        }
        #endregion

        #region Gestion de la reception d'un SMS
        private string GetMsgSaved(int value)
        {
            //value = 0 pour SIM et 1 pour Phone
            string SmsRecup = "";
            if (value == 0) SmsRecup = PhoneStorageType.Sim;
            else if (value == 1) SmsRecup = PhoneStorageType.Phone;

            if (SmsRecup.Length == 0)
                throw new Exception("Type de message inconnu");
            return SmsRecup;
        }

        /// <summary>
        /// Permet d'appeler l'instentiation d'un objet gérant l'écoute de la réception d'un SMS
        /// </summary>
        /// <param name="port">Port du modem</param>
        /// <param name="baud">Vitesse de transmission</param>
        /// <param name="timeout">Temps de réponse</param>
        public void InstanceEcouteurReceptionSMS()
        {
            comm.MessageReceived += new MessageReceivedEventHandler(comm_MessageReceived);
            if (comm.IsOpen()) { }
            else comm.Open();
            Console.WriteLine("JE SUIS ECOUTE INSTANCIE");
        }

        /// <summary>
        /// Permet la réception d'un SMS qu'il retourne dans un string permettant sa recupération
        /// </summary>
        /// <returns>String du message</returns>
        public string ReceiveSMS()
        {
            string strSms = "";
            
            DecodedShortMessage[] message = comm.ReadMessages(PhoneMessageStatus.ReceivedUnread, GetMsgSaved(0));

            foreach (DecodedShortMessage msg in message)
            {
                strSms = AffichageMessage(msg.Data);
            }

            //MemoryStatus mst = new MemoryStatus();
            string emplacement = GetMsgSaved(0);
            //On libere de l'espace dans la SIM en supprimant tous les SMS
            if (comm.GetMessageMemoryStatus(emplacement).Used == 15) this.deleteAllSMS();
            else { }
            return strSms;
        }

        //Ecouteur de la reception d'un SMS
        public void comm_MessageReceived(object sender, GsmComm.GsmCommunication.MessageReceivedEventArgs e)
        {
            try
            {
                messageRecu = ReceiveSMS();
                //if (string.IsNullOrEmpty(messageRecu)) messageRecu = ReceiveSMSPhone();
                Console.WriteLine("JE SUIS MAINTENANT ECOUTE");
                if (Factory.Instance.AuthentificateUserToSensSMS(messageRecu)) this.DoData(messageRecu);
                else { }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur => " + ex.Message);
            }
        }

        #endregion

        #region Suppression d'un ou des tous les SMS
        /// <summary>
        /// Permet de supprimer un SMS en passant en paramètre l'index du message
        /// </summary>
        /// <param name="index">Entier</param>
        private void deleteSMS(int index)
        {
            string emplacement = GetMsgSaved(0);
            // Suppresion du message a un index specifie dans son emplacement de stockage
            comm.DeleteMessage(index, emplacement);
        }
        /// <summary>
        /// Permet de supprimer tous les messages enregistrés
        /// </summary>
        public void deleteAllSMS()
        {
            string emplacement = GetMsgSaved(0);
            // Suppresion des tous les mesages stockes
            comm.DeleteMessages(DeleteScope.All, emplacement);
        }
        #endregion

        #region Affichage du message suivant les cas
        private string AffichageMessage(SmsPdu pdu)
        {
            string valueAdd = "";

            if (pdu is SmsDeliverPdu)
            {
                // Message Recu qui se suit: Message reçu, Expediteur et date de réception
                SmsDeliverPdu data = (SmsDeliverPdu)pdu;
                valueAdd = data.UserDataText + "|" + data.OriginatingAddress + "|" + data.SCTimestamp.ToString();
            }

            return valueAdd;
        }
        #endregion

        #region Gestion de l'envoie d'un SMS à un seul destinataire
        /// <summary>
        /// Permet l'envoie d'un SMS à un seul destinataire en passant en paramètre  
        /// le message à envoyé ainsi que le N°Tél du destinataire
        /// </summary>
        /// <param name="message">Message à envoyer</param>
        /// <param name="destinataires">N° du destinataire</param>
        public void SendOneSMS(string message, string destinataire)
        {
            SmsSubmitPdu pdu;

            pdu = new SmsSubmitPdu(message, destinataire, "");
            if (comm.IsOpen()) { }
            else comm.Open();
            comm.SendMessage(pdu);
            //comm.Close();
        }
        #endregion

        #region Gestion de l'envoie d'un SMS à plusieurs destinataires
        /// <summary>
        /// Permet l'envoie d'un SMS à plusieurs destinataires en passant en paramètre le message à envoyer  
        /// et toutes les adresses des destinataires (N° de Tél) séparés par des point virgules
        /// </summary>
        /// <param name="message">Message à envoyer</param>
        /// <param name="destinataires">N° du destinataire</param>
        public List<string> SendManySMS(string message, string destinataires)
        {
            SmsSubmitPdu pdu;
            string[] tbDestinataires;
            
            List<string> dataToSave = new List<string>();//Liste qui permettra de contenir successivement 
                                                        //chaque enregistrement a inserer

            tbDestinataires = destinataires.Split(new char[] { ';' });

            if (comm.IsOpen()) { }
            else comm.Open();

            for (int i = 0; i < tbDestinataires.Length; i++)
            {
                pdu = new SmsSubmitPdu(message, tbDestinataires[i], "");
                comm.SendMessage(pdu);

                //Recuperation des valeurs et ajout dans la liste     
                dataToSave.Add(tbDestinataires[i] + "|" + message);
            }
            return dataToSave;
        }
        #endregion

        #region Recupération du numéro de port en ométant la désignation COM lors de la sélection de ce dernier
        /// <summary>
        /// Permet de retourner le numéro de port sous forme quasi entière en ométant la désignation COM
        /// et retourne un entier tout en prennant en paramètre le string contenant les deux valeur concaténées
        /// Ex: COM2 -> 2
        /// </summary>
        /// <param name="valeur"></param>
        /// <returns></returns>
        public int RecupNumeroPort(string valeur)
        {
            try
            {
                int numPort = 0;
                numPort = Convert.ToInt32(valeur.Substring(3, valeur.Length - 3));
                return numPort;
            }
            catch (Exception)
            {
                throw new Exception("Erreur lors de la récupération du numéro de port");
            }
        }
        #endregion

        #region Chargement des différentes vitesses de ports
        /// <summary>
        /// Chargement du Baut Rate ou vitesse de transfère du port du modem qui retourne un tableau des entiers
        /// </summary>
        /// <returns>Tableau des entiers</returns>
        public int[] LoadBaudPorts()
        {
            int[] baud = new int[] { 300, 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200, 460800, 921600 };
            return baud;
        }
        #endregion

        #region Chargement des différentes valeurs pour le Bit des données
        /// <summary>
        /// Chargement des valeurs pour le bit des données du modem et qui retourne un tableau des entiers
        /// </summary>
        /// <returns>Tableau des entiers</returns>
        public int[] LoadDataBit()
        {
            int[] dataBit = new int[] { 4, 5, 6, 7, 8 };
            return dataBit;
        }
        #endregion

        #region Chargement des différentes valeurs pour le Bit de parité du Modem
        /// <summary>
        /// Chargement des valeurs pour le bit de parité du modem et qui retourne un tableau des strings
        /// </summary>
        /// <returns>Tableau des strings</returns>
        public string[] LoadParityBit()
        {
            string[] parityBit = new string[] { "Aucun", "Pair", "Impair", "Marque", "Espace" };
            return parityBit;
        }
        #endregion

        #region Chargement des différentes valeurs de timeout du Modem
        /// <summary>
        /// Chargement des valeurs pour le delais de connection du Modem et retourne un tableau des entiers
        /// </summary>
        /// <returns>Tableau des entiers</returns>
        public int[] LoadTimeOut()
        {
            int[] timeOut = new int[] { 150, 300, 600, 900, 1200, 1500, 1800, 2000 };
            return timeOut;
        }
        #endregion

        #region GESTION DE L'INSERTION DU SMS DANS LA BASE DES DONNEES
        void DoData(string valueString)
        {
            ErreurEnvoie errEnvoie = new ErreurEnvoie();
            Personne personne = new Personne();
            Photo photoPers = new Photo();
            DateTime dateOperation;
            string NumTel = "";
            //On recupere les trois valeurs provenant du string passe en argument (toutes les valeurs
            //separee par le symbole |, donc splitees)

            string[] tbvalueMessage = valueString.Split(new char[] { '|' });

            //Puis on affecte les valeurs
            NumTel = tbvalueMessage[1];
            dateOperation = Convert.ToDateTime(tbvalueMessage[2]);

            //Affichage de test des valeurs recuperees
            Console.Out.WriteLine("Message complet :" + tbvalueMessage[0]);
            Console.Out.WriteLine("numeroExpediteur: " + NumTel);
            Console.Out.WriteLine("dateOperation   :" + dateOperation);
            
            //Avant toutes operationm on verifie que la personne est reellement un Agent recensseur
            //Si oui on continuem sinon on retourne un message d'erreur

            if (Factory.Instance.isRecenseur(NumTel))
            {
                //Recuperation des valeurs a inserer dans le first indice 0 du table tbvalueMessage, donc encore
                //spliter ce tableau par le separateur qui sera une virgule 
                //id_avenueVillage,id_pere,id_mere,nom,postnom,prenom,sexe,etatcivil,travail,
                //numero,nombreEnfant,niveauEtude,datenaissance,nomuser,motpass
                //Le numero d'identification est attribue automatiquement
                string[] valueToInsert = tbvalueMessage[0].Split(new char[] { ',' });

                //Avant de faire une operation une insertion ou un update,on verifie que le delais de connection n'a pas expire
                bool doOperation = false;
                //try
                //{
                    Factory.Instance.Con.Close();
                    Factory.Instance.Con.Open();

                //On doit recalculer le delais parrapport a l'instant de l'envoie et virifier qu'il n'est pas expire
                int tempActuel = DateTime.Now.Hour;
                IDbCommand cmdRecalculateDelais = Factory.Instance.Con.CreateCommand();
                cmdRecalculateDelais.CommandText = @"SELECT parametreAgentSMS.delais AS delais,telephone.numero AS numero
                FROM utilisateur INNER JOIN telephone ON utilisateur.id = telephone.id_utilisateur INNER JOIN parametreAgentSMS
                ON utilisateur.nomuser = parametreAgentSMS.nomuser WHERE (numero = @numero)";

                IDataParameter paramNumero = cmdRecalculateDelais.CreateParameter();
                paramNumero.ParameterName = "@numero";
                paramNumero.Value = NumTel;
                cmdRecalculateDelais.Parameters.Add(paramNumero);

                IDataReader dr = cmdRecalculateDelais.ExecuteReader();

                if (dr.Read())
                {
                    if (tempActuel <= (Convert.ToInt32(dr["delais"]))) doOperation = true;
                    else doOperation = false;
                }
                else doOperation = false;

                dr.Dispose();
                cmdRecalculateDelais.Dispose();
                
                Factory.Instance.Con.Close();
                //}catch(Exception){}

                if (doOperation)
                {
                    //Autorisation de do des opertions car le delais de connection n'a pas encore expire

                    #region Insertion des données de la personne
                    if (valueToInsert[0].ToLower().Equals("i"))
                    {
                        try
                        {
                            //On sette les valeurs deja recuperes dans le tableau
                            //Respectivemet la lettre i pour insert,id_avenueVillage,NumeroNational du pere(converti en id_pere),NumeroNational de la mere(converti en id_mere,
                            //nom,postnom,prenom,sexe,etativil,travail,numero,nombreEnfant,niveauEtude,datenaissance
                            personne.Id_avenueVillage = Convert.ToInt32(valueToInsert[1]);
                            personne.Id = Factory.Instance.ReNewIdValue(personne);
                            int tempIdPere = (Factory.Instance.GetIdPersonne(valueToInsert[2].ToUpper())) == 0 ? Convert.ToInt32(null) : Factory.Instance.GetIdPersonne(valueToInsert[2].ToUpper());
                            int tempIdMere = (Factory.Instance.GetIdPersonne(valueToInsert[3].ToUpper())) == 0 ? Convert.ToInt32(null) : Factory.Instance.GetIdPersonne(valueToInsert[3].ToUpper());

                            if (tempIdPere == 0) personne.Id_pere = null;
                            else personne.Id_pere = tempIdPere;
                            if (tempIdMere == 0) personne.Id_mere = null;
                            else personne.Id_mere = tempIdMere;

                            personne.Nom = valueToInsert[4];
                            personne.Postnom = valueToInsert[5];
                            personne.Prenom = valueToInsert[6];
                            personne.Sexe = valueToInsert[7].ToUpper();
                            personne.EtatCivile = valueToInsert[8].ToUpper();
                            if (valueToInsert[9].EndsWith("0")) personne.Travail = false;
                            else if (valueToInsert[9].EndsWith("1")) personne.Travail = true;
                            else if (valueToInsert[9].ToLower().EndsWith("true")) personne.Travail = true;
                            else if (valueToInsert[9].ToLower().EndsWith("false")) personne.Travail = false;
                            personne.Numero = valueToInsert[10];
                            personne.NombreEnfant = Convert.ToInt32(valueToInsert[11]);
                            personne.Niveau = valueToInsert[12].ToUpper();
                            personne.NumeroNational = Factory.Instance.generateNumIdNational(personne.Id_avenueVillage);
                            if (valueToInsert[13].Equals("")) personne.Datenaissance = null;
                            else personne.Datenaissance = Convert.ToDateTime(valueToInsert[13]);
                            personne.Datedeces = null;

                            //Affichage de test des data to insert
                            Console.Out.WriteLine("personne.Id_avenueVillage :" + personne.Id_avenueVillage);
                            Console.Out.WriteLine("personne.Id_pere          :" + personne.Id_pere);
                            Console.Out.WriteLine("personne.Id_mere          :" + personne.Id_mere);
                            Console.Out.WriteLine("personne.Nom              :" + personne.Nom);
                            Console.Out.WriteLine("personne.Postnom          :" + personne.Postnom);
                            Console.Out.WriteLine("personne.Prenom           :" + personne.Prenom);
                            Console.Out.WriteLine("personne.Sexe             :" + personne.Sexe);
                            Console.Out.WriteLine("personne.EtatCivile       :" + personne.EtatCivile);
                            Console.Out.WriteLine("personne.Travail          :" + personne.Travail);
                            Console.Out.WriteLine("personne.Numero           :" + personne.Numero);
                            Console.Out.WriteLine("personne.NombreEnfant     :" + personne.NombreEnfant);
                            Console.Out.WriteLine("personne.Niveau           :" + personne.Niveau);
                            Console.Out.WriteLine("personne.NumeroNational   :" + personne.NumeroNational);
                            Console.Out.WriteLine("personne.Datenaissance    :" + personne.Datenaissance);
                            Console.Out.WriteLine("personne.Datedeces        :" + personne.Datedeces);

                            //Insertion des data dans la database
                            personne.Enregistrer();

                            //Apres l'insertion de la personne, on insere sa photo
                            photoPers.Id_personne = Convert.ToInt32(personne.Id);
                            photoPers.Id = Factory.Instance.ReNewIdValue(photoPers);
                            photoPers.PhotoPersonne = null;
                            photoPers.Enregistrer();
                            this.SendOneSMS("Enregistrement éffectué", NumTel);
                            //Console.WriteLine("Enregistrement éffectué");
                        }
                        catch (NpgsqlException e1)
                        {
                            //Console.Out.WriteLine("Erreur lors de l'insertion des données de la personne 1!!");

                            try
                            {
                                //Insertion du message errone dans la table erreurEnvoie
                                errEnvoie.Id = Factory.Instance.ReNewIdValue(errEnvoie);
                                errEnvoie.Expediteur = NumTel;
                                errEnvoie.Message = tbvalueMessage[0];
                                errEnvoie.Date_envoie = dateOperation;
                                errEnvoie.Erreur = e1.Message;

                                errEnvoie.Enregistrer();
                            }
                            catch (Exception e3)
                            {
                                throw new Exception(e3.Message);
                            }
                            this.SendOneSMS("Erreur lors de l'insertion des données de la personne,veuillez vérifier la syntaxe ainsi que les données du sms svp !!", NumTel);
                            //Console.WriteLine("Erreur lors de l'insertion des données de la personne,veuillez vérifier la syntaxe ainsi que les données du sms svp !!");
                        }
                        catch (SqlException e11)
                        {
                            try
                            {
                                //Insertion du message errone dans la table erreurEnvoie
                                errEnvoie.Id = Factory.Instance.ReNewIdValue(errEnvoie);
                                errEnvoie.Expediteur = NumTel;
                                errEnvoie.Message = tbvalueMessage[0];
                                errEnvoie.Date_envoie = dateOperation;
                                errEnvoie.Erreur = e11.Message;

                                errEnvoie.Enregistrer();
                            }
                            catch (Exception e31)
                            {
                                throw new Exception(e31.Message);
                            }
                            this.SendOneSMS("Erreur lors de l'insertion des données de la personne,veuillez vérifier la syntaxe ainsi que les données du sms svp !!", NumTel);
                            //Console.WriteLine("Erreur lors de l'insertion des données de la personne,veuillez vérifier la syntaxe ainsi que les données du sms svp !!");
                        }
                        catch (OleDbException e12)
                        {
                            try
                            {
                                //Insertion du message errone dans la table erreurEnvoie
                                errEnvoie.Id = Factory.Instance.ReNewIdValue(errEnvoie);
                                errEnvoie.Expediteur = NumTel;
                                errEnvoie.Message = tbvalueMessage[0];
                                errEnvoie.Date_envoie = dateOperation;
                                errEnvoie.Erreur = e12.Message;

                                errEnvoie.Enregistrer();
                            }
                            catch (Exception e32)
                            {
                                throw new Exception(e32.Message);
                            }
                            this.SendOneSMS("Erreur lors de l'insertion des données de la personne,veuillez vérifier la syntaxe ainsi que les données du sms svp !!", NumTel);
                            //Console.WriteLine("Erreur lors de l'insertion des données de la personne,veuillez vérifier la syntaxe ainsi que les données du sms svp !!");
                        }
                        catch (MySqlException e13)
                        {
                            try
                            {
                                //Insertion du message errone dans la table erreurEnvoie
                                errEnvoie.Id = Factory.Instance.ReNewIdValue(errEnvoie);
                                errEnvoie.Expediteur = NumTel;
                                errEnvoie.Message = tbvalueMessage[0];
                                errEnvoie.Date_envoie = dateOperation;
                                errEnvoie.Erreur = e13.Message;

                                errEnvoie.Enregistrer();
                            }
                            catch (Exception e33)
                            {
                                throw new Exception(e33.Message);
                            }
                            this.SendOneSMS("Erreur lors de l'insertion des données de la personne,veuillez vérifier la syntaxe ainsi que les données du sms svp !!", NumTel);
                            //Console.WriteLine("Erreur lors de l'insertion des données de la personne,veuillez vérifier la syntaxe ainsi que les données du sms svp !!");
                        }
                        catch (Exception e2)
                        {
                            //Console.Out.WriteLine("Erreur lors de l'insertion des données de la personne 2 !!");

                            try
                            {
                                //Insertion du message errone dans la table erreurEnvoie
                                errEnvoie.Id = Factory.Instance.ReNewIdValue(errEnvoie);
                                errEnvoie.Expediteur = NumTel;
                                errEnvoie.Message = tbvalueMessage[0];
                                errEnvoie.Date_envoie = dateOperation;
                                errEnvoie.Erreur = e2.Message;

                                errEnvoie.Enregistrer();
                            }
                            catch (Exception e3)
                            {
                                throw new Exception(e3.Message);
                            }
                            //Si bad data, on renvoi un mesage a l'expediteur ici le recensseur
                            //Console.WriteLine("Le format des données envoyées concernant la personne est invalide !!");
                            this.SendOneSMS("Le format des données envoyées concernant la personne est invalide !!", NumTel);
                        }
                    }
                    #endregion

                    #region Modification des données de la personne
                    else if (valueToInsert[0].ToLower().Equals("m"))
                    {
                        try
                        {
                            //On sette les valeurs deja recuperes dans le tableau
                            //Respectivemet la lettre m pour modifier ou update,id_avenueVillage,NumeroNational du pere(converti en id_pere),NumeroNational de la mere(converti en id_mere,
                            //nom,postnom,prenom,sexe,etativil,travail,numero,nombreEnfant,niveauEtude,datenaissance,numeroNational
                            personne.Id_avenueVillage = Convert.ToInt32(valueToInsert[1]);
                            int tempIdPere = (Factory.Instance.GetIdPersonne(valueToInsert[2].ToUpper())) == 0 ? Convert.ToInt32(null) : Factory.Instance.GetIdPersonne(valueToInsert[2].ToUpper());
                            int tempIdMere = (Factory.Instance.GetIdPersonne(valueToInsert[3].ToUpper())) == 0 ? Convert.ToInt32(null) : Factory.Instance.GetIdPersonne(valueToInsert[3].ToUpper());

                            if (tempIdPere == 0) personne.Id_pere = null;
                            else personne.Id_pere = tempIdPere;
                            if (tempIdMere == 0) personne.Id_mere = null;
                            else personne.Id_mere = tempIdMere;

                            personne.Nom = valueToInsert[4];
                            personne.Postnom = valueToInsert[5];
                            personne.Prenom = valueToInsert[6];
                            personne.Sexe = valueToInsert[7].ToUpper();
                            personne.EtatCivile = valueToInsert[8].ToUpper();
                            if (valueToInsert[9].EndsWith("0")) personne.Travail = false;
                            else if (valueToInsert[9].EndsWith("1")) personne.Travail = true;
                            else if (valueToInsert[9].ToLower().EndsWith("true")) personne.Travail = true;
                            else if (valueToInsert[9].ToLower().EndsWith("false")) personne.Travail = false;
                            personne.Numero = valueToInsert[10];
                            personne.NombreEnfant = Convert.ToInt32(valueToInsert[11]);
                            personne.Niveau = valueToInsert[12].ToUpper();
                            //personne.NumeroNational = Factory.Instance.generateNumIdNational(personne.Id_avenueVillage);
                            if (valueToInsert[13].Equals("")) personne.Datenaissance = null;
                            else personne.Datenaissance = Convert.ToDateTime(valueToInsert[13]);
                            personne.Datedeces = null;

                            //On sette l'Id de la personne connaissant son numeroNational
                            personne.Id = Factory.Instance.GetIdPersonne(valueToInsert[14].ToUpper());

                            //Affichage de test des data to insert
                            Console.Out.WriteLine("personne.Id_avenueVillage :" + personne.Id_avenueVillage);
                            Console.Out.WriteLine("personne.Id_pere          :" + personne.Id_pere);
                            Console.Out.WriteLine("personne.Id_mere          :" + personne.Id_mere);
                            Console.Out.WriteLine("personne.Nom              :" + personne.Nom);
                            Console.Out.WriteLine("personne.Postnom          :" + personne.Postnom);
                            Console.Out.WriteLine("personne.Prenom           :" + personne.Prenom);
                            Console.Out.WriteLine("personne.Sexe             :" + personne.Sexe);
                            Console.Out.WriteLine("personne.EtatCivile       :" + personne.EtatCivile);
                            Console.Out.WriteLine("personne.Travail          :" + personne.Travail);
                            Console.Out.WriteLine("personne.Numero           :" + personne.Numero);
                            Console.Out.WriteLine("personne.NombreEnfant     :" + personne.NombreEnfant);
                            Console.Out.WriteLine("personne.Niveau           :" + personne.Niveau);
                            Console.Out.WriteLine("personne.NumeroNational   :" + valueToInsert[14].ToUpper());
                            Console.Out.WriteLine("personne.Datenaissance    :" + personne.Datenaissance);
                            Console.Out.WriteLine("personne.Datedeces        :" + personne.Datedeces);

                            //Modification des data dans la database
                            personne.Modifier();
                            this.SendOneSMS("Modification éffectuée", NumTel);
                            //Console.WriteLine("Modification éffectuée");
                        }
                        catch (NpgsqlException e1)
                        {
                            //Console.Out.WriteLine("Erreur lors de l'insertion des données de la personne 1!!");

                            try
                            {
                                //Insertion du message errone dans la table erreurEnvoie
                                errEnvoie.Id = Factory.Instance.ReNewIdValue(errEnvoie);
                                errEnvoie.Expediteur = NumTel;
                                errEnvoie.Message = tbvalueMessage[0];
                                errEnvoie.Date_envoie = dateOperation;
                                errEnvoie.Erreur = e1.Message;

                                errEnvoie.Enregistrer();
                            }
                            catch (Exception e3)
                            {
                                throw new Exception(e3.Message);
                            }
                            this.SendOneSMS("Erreur lors de la modification des données de la personne,veuillez vérifier la syntaxe ainsi que les données du sms svp !!", NumTel);
                            //Console.WriteLine("Erreur lors de la modification des données de la personne,veuillez vérifier la syntaxe ainsi que les données du sms svp !!");
                        }
                        catch (SqlException e11)
                        {
                            try
                            {
                                //Insertion du message errone dans la table erreurEnvoie
                                errEnvoie.Id = Factory.Instance.ReNewIdValue(errEnvoie);
                                errEnvoie.Expediteur = NumTel;
                                errEnvoie.Message = tbvalueMessage[0];
                                errEnvoie.Date_envoie = dateOperation;
                                errEnvoie.Erreur = e11.Message;

                                errEnvoie.Enregistrer();
                            }
                            catch (Exception e31)
                            {
                                throw new Exception(e31.Message);
                            }
                            this.SendOneSMS("Erreur lors de la modification des données de la personne,veuillez vérifier la syntaxe ainsi que les données du sms svp !!", NumTel);
                            //Console.WriteLine("Erreur lors de la modification des données de la personne,veuillez vérifier la syntaxe ainsi que les données du sms svp !!");
                        }
                        catch (OleDbException e12)
                        {
                            try
                            {
                                //Insertion du message errone dans la table erreurEnvoie
                                errEnvoie.Id = Factory.Instance.ReNewIdValue(errEnvoie);
                                errEnvoie.Expediteur = NumTel;
                                errEnvoie.Message = tbvalueMessage[0];
                                errEnvoie.Date_envoie = dateOperation;
                                errEnvoie.Erreur = e12.Message;

                                errEnvoie.Enregistrer();
                            }
                            catch (Exception e32)
                            {
                                throw new Exception(e32.Message);
                            }
                            this.SendOneSMS("Erreur lors de la modification des données de la personne,veuillez vérifier la syntaxe ainsi que les données du sms svp !!", NumTel);
                            //Console.WriteLine("Erreur lors de la modification des données de la personne,veuillez vérifier la syntaxe ainsi que les données du sms svp !!");
                        }
                        catch (MySqlException e13)
                        {
                            try
                            {
                                //Insertion du message errone dans la table erreurEnvoie
                                errEnvoie.Id = Factory.Instance.ReNewIdValue(errEnvoie);
                                errEnvoie.Expediteur = NumTel;
                                errEnvoie.Message = tbvalueMessage[0];
                                errEnvoie.Date_envoie = dateOperation;
                                errEnvoie.Erreur = e13.Message;

                                errEnvoie.Enregistrer();
                            }
                            catch (Exception e33)
                            {
                                throw new Exception(e33.Message);
                            }
                            this.SendOneSMS("Erreur lors de la modification des données de la personne,veuillez vérifier la syntaxe ainsi que les données du sms svp !!", NumTel);
                            //Console.WriteLine("Erreur lors de la modification des données de la personne,veuillez vérifier la syntaxe ainsi que les données du sms svp !!");
                        }
                        catch (Exception e2)
                        {
                            //Console.Out.WriteLine("Erreur lors de l'insertion des données de la personne 2 !!");

                            try
                            {
                                //Insertion du message errone dans la table erreurEnvoie
                                errEnvoie.Id = Factory.Instance.ReNewIdValue(errEnvoie);
                                errEnvoie.Expediteur = NumTel;
                                errEnvoie.Message = tbvalueMessage[0];
                                errEnvoie.Date_envoie = dateOperation;
                                errEnvoie.Erreur = e2.Message;

                                errEnvoie.Enregistrer();
                            }
                            catch (Exception e3)
                            {
                                throw new Exception(e3.Message);
                            }
                            //Si bad data, on renvoi un mesage a l'expediteur ici le recensseur
                            //Console.WriteLine("Le format des données envoyées concernant la personne est invalide !!");
                            this.SendOneSMS("Le format des données envoyées concernant la personne est invalide !!", NumTel);
                        }
                    }
                    #endregion

                    #region Insertion ou mis à jour pour la personne décédée
                    else if (valueToInsert[0].ToLower().Equals("d"))
                    {
                        try
                        {
                            //On sette les valeurs deja recuperes dans le tableau
                            //Respectivemet la lettre d pour décedé,NumeroNational de la personne,datedeces
                            personne.NumeroNational = valueToInsert[1].ToUpper();
                            if (valueToInsert[2].Equals("")) personne.Datedeces = null;
                            else personne.Datedeces = Convert.ToDateTime(valueToInsert[2]);

                            if (personne.Datedeces == null)
                            {
                                this.SendOneSMS("La date de decès ne peut être vide !!", NumTel);
                                //Console.WriteLine("La date de decès ne peut être vide !!");
                            }
                            else
                            {
                                //On verifie que la date de deces est superieure a la date de naissance et dans le
                                //cas contraire on ne fait aucune insertion
                                if (Factory.Instance.GetDateNaissance(personne.NumeroNational) != null && personne.Datedeces != null)
                                {
                                    if (Factory.Instance.GetDateNaissance(personne.NumeroNational) > personne.Datedeces)
                                    {
                                        try
                                        {
                                            //Insertion du message errone dans la table erreurEnvoie
                                            errEnvoie.Id = Factory.Instance.ReNewIdValue(errEnvoie);
                                            errEnvoie.Expediteur = NumTel;
                                            errEnvoie.Message = tbvalueMessage[0];
                                            errEnvoie.Date_envoie = dateOperation;
                                            errEnvoie.Erreur = "La date de decès ne peut être inférieure à la date de naissance de la personne";

                                            errEnvoie.Enregistrer();
                                        }
                                        catch (Exception e3)
                                        {
                                            throw new Exception(e3.Message);
                                        }
                                        this.SendOneSMS("La date de decès ne peut être inférieure à la date de naissance de la personne !!", NumTel);
                                        //Console.WriteLine("La date de decès ne peut être inférieure à la date de naissance de la personne !!");
                                    }
                                    else
                                    {
                                        //On verifie que le numero national exist reelement et dans le cas contraire,pas update 
                                        //et on retourne un message d'ereur

                                        if (Factory.Instance.VerifieExistanceNumNational(personne.NumeroNational))
                                        {
                                            //Affichage de test des data to insert
                                            Console.Out.WriteLine("personne.NumeroNational :" + personne.NumeroNational);
                                            Console.Out.WriteLine("personne.Datedeces      :" + personne.Datedeces);
                                            //On insere tel quel la date de deces
                                            //Insertion des data dans la database
                                            personne.ModifierDeces();
                                            this.SendOneSMS("Date de décès mise à jourr avec succès !!", NumTel);
                                        }
                                        else this.SendOneSMS("Le numéro d'identification National de la personne n'existe pas !!", NumTel);
                                            //Console.WriteLine("Le numéro d'identification National de la personne n'existe pas !!");
                                    }
                                }
                                else if ((Factory.Instance.GetDateNaissance(personne.NumeroNational) == null && personne.Datedeces != null))
                                {
                                    //On verifie que le numero national exist reelement et dans le cas contraire,pas update 
                                    //et on retourne un message d'ereur

                                    if (Factory.Instance.VerifieExistanceNumNational(personne.NumeroNational))
                                    {
                                        //Affichage de test des data to insert
                                        Console.Out.WriteLine("personne.NumeroNational :" + personne.NumeroNational);
                                        Console.Out.WriteLine("personne.Datedeces      :" + personne.Datedeces);
                                        //On insere tel quel la date de deces
                                        //Insertion des data dans la database
                                        personne.ModifierDeces();
                                        this.SendOneSMS("Date de décès mise à jourr avec succès !!", NumTel);
                                    }
                                    else this.SendOneSMS("Le numéro d'identification National de la personne n'existe pas !!", NumTel);
                                        //Console.WriteLine("Le numéro d'identification National de la personne n'existe pas !!");
                                }
                            }
                        }
                        catch (NpgsqlException e1)
                        {
                            try
                            {
                                //Insertion du message errone dans la table erreurEnvoie
                                errEnvoie.Id = Factory.Instance.ReNewIdValue(errEnvoie);
                                errEnvoie.Expediteur = NumTel;
                                errEnvoie.Message = tbvalueMessage[0];
                                errEnvoie.Date_envoie = dateOperation;
                                errEnvoie.Erreur = e1.Message;

                                errEnvoie.Enregistrer();
                            }
                            catch (Exception e3)
                            {
                                throw new Exception(e3.Message);
                            }
                            this.SendOneSMS("Erreur lors de l'enregistrement du decès, veuillez verifier la syntaxe du sms !!", NumTel);
                            //Console.WriteLine("Erreur lors de l'enregistrement du decès, veuillez verifier la syntaxe du sms !!");
                        }
                        catch (SqlException e11)
                        {
                            try
                            {
                                //Insertion du message errone dans la table erreurEnvoie
                                errEnvoie.Id = Factory.Instance.ReNewIdValue(errEnvoie);
                                errEnvoie.Expediteur = NumTel;
                                errEnvoie.Message = tbvalueMessage[0];
                                errEnvoie.Date_envoie = dateOperation;
                                errEnvoie.Erreur = e11.Message;

                                errEnvoie.Enregistrer();
                            }
                            catch (Exception e31)
                            {
                                throw new Exception(e31.Message);
                            }
                            this.SendOneSMS("Erreur lors de l'enregistrement du decès, veuillez verifier la syntaxe du sms !!", NumTel);
                            //Console.WriteLine("Erreur lors de l'enregistrement du decès, veuillez verifier la syntaxe du sms !!");
                        }
                        catch (OleDbException e12)
                        {
                            try
                            {
                                //Insertion du message errone dans la table erreurEnvoie
                                errEnvoie.Id = Factory.Instance.ReNewIdValue(errEnvoie);
                                errEnvoie.Expediteur = NumTel;
                                errEnvoie.Message = tbvalueMessage[0];
                                errEnvoie.Date_envoie = dateOperation;
                                errEnvoie.Erreur = e12.Message;

                                errEnvoie.Enregistrer();
                            }
                            catch (Exception e32)
                            {
                                throw new Exception(e32.Message);
                            }
                            this.SendOneSMS("Erreur lors de l'enregistrement du decès, veuillez verifier la syntaxe du sms !!", NumTel);
                            //Console.WriteLine("Erreur lors de l'enregistrement du decès, veuillez verifier la syntaxe du sms !!");
                        }
                        catch (MySqlException e13)
                        {
                            try
                            {
                                //Insertion du message errone dans la table erreurEnvoie
                                errEnvoie.Id = Factory.Instance.ReNewIdValue(errEnvoie);
                                errEnvoie.Expediteur = NumTel;
                                errEnvoie.Message = tbvalueMessage[0];
                                errEnvoie.Date_envoie = dateOperation;
                                errEnvoie.Erreur = e13.Message;

                                errEnvoie.Enregistrer();
                            }
                            catch (Exception e33)
                            {
                                throw new Exception(e33.Message);
                            }
                            this.SendOneSMS("Erreur lors de l'enregistrement du decès, veuillez verifier la syntaxe du sms !!", NumTel);
                            //Console.WriteLine("Erreur lors de l'enregistrement du decès, veuillez verifier la syntaxe du sms !!");
                        }
                        catch (Exception e2)
                        {
                            try
                            {
                                //Insertion du message errone dans la table erreurEnvoie
                                errEnvoie.Id = Factory.Instance.ReNewIdValue(errEnvoie);
                                errEnvoie.Expediteur = NumTel;
                                errEnvoie.Message = tbvalueMessage[0];
                                errEnvoie.Date_envoie = dateOperation;
                                errEnvoie.Erreur = e2.Message;

                                errEnvoie.Enregistrer();
                            }
                            catch (Exception e3)
                            {
                                throw new Exception(e3.Message);
                            }

                            this.SendOneSMS("Le format des données envoyées concernant le decès de personnes est invalides !!", NumTel);
                            //Console.WriteLine("Le format des données envoyées concernant le decès de personnes est invalides !!");
                        }
                    }
                    #endregion

                    else
                    {
                        this.SendOneSMS("Le format des données du message est incorrect", NumTel);
                        //Console.WriteLine("Le format des données du message est incorrect");
                    }
                }
                else this.SendOneSMS("Le delais de votre connexion a expiré, veuillez vous reconnecté svp !!", NumTel);
                    //Console.WriteLine("Le delais de votre connexion a expiré, veuillez vous reconnecté svp !!");
            }
            else this.SendOneSMS("Cette personne n'est pas autorisée à utiliser ce service !!", NumTel);
                //Console.WriteLine("Cette personne n'est pas autorisée à utiliser ce service !!");
        }
        #endregion
    }
}
