--Fonction pour villeTerritoire
CREATE FUNCTION fonctionRecupInsertVilleTerritoire () RETURNS TRIGGER AS $fonctionRecupInsertVilleTerritoire$
	BEGIN
		INSERT INTO recupQuery(requete) VALUES('insert into villeTerritoire(designation,superficie,id,id_province) values(''' || NEW.designation || ''',' || NEW.superficie || ',|villeTerritoire');
		RAISE INFO 'insert into villeTerritoire(designation,superficie,id,id_province) values(''%'',%,|villeTerritoire',NEW.designation,NEW.superficie;
		RETURN NULL;
	END	
	$fonctionRecupInsertVilleTerritoire$ LANGUAGE plpgsql;
	
--Trigger pour villeTerritoire	
CREATE TRIGGER recupInsertVilleTerritoire AFTER INSERT ON villeTerritoire	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupInsertVilleTerritoire();

--Fonction pour communeChefferieSecteur
CREATE FUNCTION fonctionRecupInsertCommuneChefferieSecteur () RETURNS TRIGGER AS $fonctionRecupInsertCommuneChefferieSecteur$
	BEGIN
		INSERT INTO recupQuery(requete) VALUES('insert into communeChefferieSecteur(id_villeTerritoire,designation,superficie,id) values(' || NEW.id_villeTerritoire || ',''' || NEW.designation || ''',' || NEW.superficie || ',|communeChefferieSecteur');
		RAISE INFO 'insert into communeChefferieSecteur(id_villeTerritoire,designation,superficie,id) values(%,''%'',%,|communeChefferieSecteur',NEW.id_villeTerritoire,NEW.designation,NEW.superficie;
		RETURN NULL;
	END	
	$fonctionRecupInsertCommuneChefferieSecteur$ LANGUAGE plpgsql;
	
--Trigger pour communeChefferieSecteur	
CREATE TRIGGER recupInsertCommuneChefferieSecteur AFTER INSERT ON communeChefferieSecteur	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupInsertCommuneChefferieSecteur();

--Fonction pour quartierLocalite
CREATE FUNCTION fonctionRecupInsertQuartierLocalite () RETURNS TRIGGER AS $fonctionRecupInsertQuartierLocalite$
	BEGIN
		INSERT INTO recupQuery(requete) VALUES('insert into quartierLocalite(id_communeChefferieSecteur,designation,superficie,id) values(' || NEW.id_communeChefferieSecteur || ',''' || NEW.designation || ''',' || NEW.superficie || ',|quartierLocalite');
		RAISE INFO 'insert into quartierLocalite(id_communeChefferieSecteur,designation,superficie,id) values(%,''%'',%,|quartierLocalite',NEW.id_communeChefferieSecteur,NEW.designation,NEW.superficie;
		RETURN NULL;
	END	
	$fonctionRecupInsertQuartierLocalite$ LANGUAGE plpgsql;
	
--Trigger pour quartierLocalite	
CREATE TRIGGER recupInsertQuartierLocalite AFTER INSERT ON quartierLocalite	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupInsertQuartierLocalite();    

--Fonction pour avenueVillage
CREATE FUNCTION fonctionRecupInsertAvenueVillage () RETURNS TRIGGER AS $fonctionRecupInsertAvenueVillage$
	BEGIN
		INSERT INTO recupQuery(requete) VALUES('insert into avenueVillage(id_quartierLocalite,designation,id) values(' || NEW.id_quartierLocalite || ',''' || NEW.designation || ''',|avenueVillage');
		RAISE INFO 'insert into avenueVillage(id_quartierLocalite,designation,id) values(%,''%'',|avenueVillage',NEW.id_quartierLocalite,NEW.designation;
		RETURN NULL;
	END	
	$fonctionRecupInsertAvenueVillage$ LANGUAGE plpgsql;
	
--Trigger pour avenueVillage	
CREATE TRIGGER recupInsertAvenueVillage AFTER INSERT ON avenueVillage	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupInsertAvenueVillage(); 

--Fonction pour personne
CREATE FUNCTION fonctionRecupInsertPersonne () RETURNS TRIGGER AS $fonctionRecupInsertPersonne$
	DECLARE
		idPere text;
		idMere text;
		numAv text;
		pNomPers text;
		prenomPers text;
		dNaiss text;
		dDeces text;
	BEGIN
		IF NEW.id_pere is NULL THEN
			idPere = 'NULL';
		ELSE idPere = NEW.id_pere;
		END IF;
		IF NEW.id_mere is NULL THEN
			idMere = 'NULL';
		ELSE idMere = NEW.id_mere;
		END IF;
		IF NEW.numero is NULL THEN
			numAv = 'NULL';
		ELSE numAv = NEW.numero;
		END IF;
		IF NEW.postnom is NULL THEN
			pNomPers = 'NULL';
		ELSE pNomPers = NEW.postnom;
		END IF;
		IF NEW.prenom is NULL THEN
			prenomPers = 'NULL';
		ELSE prenomPers = NEW.prenom;
		END IF;
		IF NEW.datenaissance is NULL THEN
			dNaiss = 'NULL';
		ELSE dNaiss = NEW.datenaissance;
		END IF;
		IF NEW.datedeces is NULL THEN
			dDeces = 'NULL';
		ELSE dDeces = NEW.datedeces;
		END IF;

		INSERT INTO recupQuery(requete) VALUES('insert into personne(id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,nombreEnfant,niveauEtude,id) 
		values('|| NEW.id_avenueVillage || ',''' || idPere || ''',''' || idMere || ''',''' || NEW.numeroNational || ''',''' || numAv || ''',''' || NEW.nom || ''',''' || pNomPers || 
		''',''' || prenomPers || ''',''' || NEW.sexe || ''',''' || NEW.etativil || ''',''' || dNaiss || ''',''' || dDeces || ''',' || NEW.travail || ',' || NEW.nombreEnfant || ',''' || NEW.niveauEtude || ''',|personne');
		
		RAISE INFO 'insert into personne(id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,nombreEnfant,niveauEtude,id) 
		values(%,%,%,''%'',''%'',''%'',''%'',''%'',''%'',''%'',''%'',''%'',''%'',%,''%'',|personne',NEW.id_avenueVillage,idPere,idMere,NEW.numeroNational,numAv,NEW.nom,pNomPers,prenomPers,NEW.sexe,NEW.etativil,dNaiss,dDeces,NEW.travail,NEW.nombreEnfant,NEW.niveauEtude;
		RETURN NULL;
	END	
	$fonctionRecupInsertPersonne$ LANGUAGE plpgsql;

--Trigger pour personne	
CREATE TRIGGER recupInsertPersonne AFTER INSERT ON personne	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupInsertPersonne();
    
--Fonction pour photo
CREATE FUNCTION fonctionRecupInsertPhoto () RETURNS TRIGGER AS $fonctionRecupInsertPhoto$
	DECLARE
		photoPers bytea;
	BEGIN
		IF NEW.photo is NULL THEN
			photoPers = 'NULL';
		ELSE photoPers = NEW.photo;
		END IF;
		
		INSERT INTO recupQuery(requete,photo) VALUES('insert into photo(id_personne,id,photo) values(,' || NEW.id_personne || ',|photo',photoPers);
		RAISE INFO 'insert into photo(id_personne,id,photo) values(%,|photo',NEW.id_personne;
		RETURN NULL;
	END	
	$fonctionRecupInsertPhoto$ LANGUAGE plpgsql;

--Trigger pour photo	
CREATE TRIGGER recupInsertPhoto AFTER INSERT ON photo	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupInsertPhoto();

--Fonction pour categorieUtilisateur
CREATE FUNCTION fonctionRecupInsertCategorieUtilisateur () RETURNS TRIGGER AS $fonctionRecupInsertCategorieUtilisateur$
	BEGIN
		INSERT INTO recupQuery(requete) VALUES('insert into categorieUtilisateur(designation,groupe,id) values(''' || NEW.designation || ''',''' || NEW.groupe || ''',|categorieUtilisateur');
		RAISE INFO 'insert into categorieUtilisateur(designation,groupe,id) values(''%'',''%'',|categorieUtilisateur',NEW.designation,NEW.groupe;
		RETURN NULL;
	END	
	$fonctionRecupInsertCategorieUtilisateur$ LANGUAGE plpgsql;
	
--Trigger pour categorieUtilisateur	
CREATE TRIGGER recupInsertCategorieUtilisateur AFTER INSERT ON categorieUtilisateur	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupInsertCategorieUtilisateur(); 

--Fonction pour utilisateur
CREATE FUNCTION fonctionRecupInsertUtilisateur () RETURNS TRIGGER AS $fonctionRecupInsertUtilisateur$
	BEGIN
		INSERT INTO recupQuery(requete) VALUES('insert into utilisateur(id_personne,id_categorieUtilisateur,activation,nomuser,motpass,id) values(' || NEW.id_personne || ',' || NEW.id_categorieUtilisateur || ',' || NEW.activation || ',''' || NEW.nomuser || ''',''' || NEW.motpass || ''',|utilisateur');
		RAISE INFO 'insert into utilisateur(id_personne,id_categorieUtilisateur,activation,nomuser,motpass,id) values(%,%,%,''%'',''%'',|utilisateur',NEW.id_personne,NEW.id_categorieUtilisateur,NEW.activation,NEW.nomuser,NEW.motpass;
		RETURN NULL;
	END	
	$fonctionRecupInsertUtilisateur$ LANGUAGE plpgsql;
	
--Trigger pour utilisateur	
CREATE TRIGGER recupInsertUtilisateur AFTER INSERT ON utilisateur	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupInsertUtilisateur();


--Fonction pour telephone
CREATE FUNCTION fonctionRecupInsertTelephone () RETURNS TRIGGER AS $fonctionRecupInsertTelephone$
	BEGIN
		INSERT INTO recupQuery(requete) VALUES('insert into telephone(numero,id_utilisateur,id) values(''' || NEW.numero || ''',' || NEW.id_utilisateur || ',|telephone');
		RAISE INFO 'insert into telephone(numero,id_utilisateur,id) values(''%'',%,|telephone',NEW.numero,NEW.id_utilisateur;
		RETURN NULL;
	END	
	$fonctionRecupInsertTelephone$ LANGUAGE plpgsql;
	
--Trigger pour telephone	
CREATE TRIGGER recupInsertTelephone AFTER INSERT ON telephone	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupInsertTelephone();

--Fonction pour carte
CREATE FUNCTION fonctionRecupInsertCarte () RETURNS TRIGGER AS $fonctionRecupInsertCarte$
	BEGIN
		INSERT INTO recupQuery(requete) VALUES('insert into carte(id_personne,datelivraison,id) values(' || NEW.id_personne || ',''' || NEW.datelivraison || ''',|carte');
		RAISE INFO 'insert into carte(id_personne,datelivraison,id) values(%,''%'',|carte',NEW.id_personne,NEW.datelivraison;
		RETURN NULL;
	END	
	$fonctionRecupInsertCarte$ LANGUAGE plpgsql;
	
--Trigger pour carte	
CREATE TRIGGER recupInsertCarte AFTER INSERT ON carte	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupInsertCarte();
    		

-- insert into villeTerritoire(id,designation,superficie) values(1,'NYIRAGONGO',23485);
-- insert into villeTerritoire(id,designation,superficie)values (2,'GOMA',18345);
-- insert into communeChefferieSecteur(id,id_villeTerritoire,designation,superficie)values (1,1,'LA KENYA',1398);
-- insert into quartierLocalite(id,id_communeChefferieSecteur,designation,superficie) values (1,1,'LUSHI',1886);
-- insert into avenueVillage(id,id_quartierLocalite,designation) values (1,1,'DES ANGES');
-- insert into personne (id,id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,niveauEtude,nombreEnfant) values (1,1,NULL,NULL,'nk11',1,'ISAMUNA','NKEMBO','JOSUE','M','CELIBATAIRE','23/11/1991',NULL,true,'D4',1);
-- insert into personne (id,id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,niveauEtude,nombreEnfant) values (2,1,NULL,NULL,'nk12',1,'ISAMUNA',NULL,NULL,'M','CELIBATAIRE','23/11/1991','12/12/2000',true,'D4',1);
-- insert into personne (id,id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,niveauEtude,nombreEnfant) values (3,1,NULL,NULL,'nk13',1,'MUBALAMA','JANVIER',NULL,'M','CELIBATAIRE',NULL,NULL,true,'D6',1);
-- insert into personne (id,id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,niveauEtude,nombreEnfant) values (4,1,NULL,NULL,'nk14',1,'MUBA','JEAN',NULL,'M','CELIBATAIRE',NULL,NULL,true,'D6',1);
-- insert into photo(id,id_personne,photo) values (1,1,null);
-- insert into carte(id,id_personne,datelivraison) values (1,1,'05/02/2013');
-- insert into categorieUtilisateur(id,designation,groupe) values (1,'Administrateur national','Administrateur');
-- insert into utilisateur(id,id_personne,id_categorieUtilisateur,activation,nomuser,motpass) values (1,1,1,FALSE,'josam','jos');
-- insert into telephone(id,numero,id_utilisateur) values (1,'0813870366',1);
-- --insert into parametreProvince(id,id_province,designation) values(1,1,'Nord Kivu')
-- 
-- select * from recupQuery
-- select * from personne;
-- select * from recupQuery;
-- select * from avenueVillage;

------insert into province(id,designation,superficie) values(1,'Nord kivu',560500)
------insert into province(id,designation,superficie) values(2,'Sud kivu',750500)

-- create table test(id integer,n varchar(10) not null,v bytea)
-- insert into test(id,n,v) values(1,'JOS','null')
-- select *from test
-- drop table test
--Trigger pour photo

----------------------------------------------------------- TRIGGER POUR MISE A JOUR ------------------------------------
--Fonction pour villeTerritoire
CREATE FUNCTION fonctionRecupVilleTerritoireUpdate () RETURNS TRIGGER AS $fonctionRecupVilleTerritoireUpdate$
	BEGIN 
		INSERT INTO recupQueryUpdate(requete) VALUES('' || NEW.id || ';' || NEW.designation || ';' || OLD.superficie || '|villeTerritoire');
		RAISE INFO '%;%;%|villeTerritoire',OLD.id,NEW.designation,NEW.superficie;
		RETURN NULL;
	END	
	$fonctionRecupVilleTerritoireUpdate$ LANGUAGE plpgsql;
	
--Trigger pour villeTerritoire	
CREATE TRIGGER recupVilleTerritoireUpdate AFTER UPDATE ON villeTerritoire	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupVilleTerritoireUpdate();

--Fonction pour communeChefferieSecteur
CREATE FUNCTION fonctionRecupCommuneChefferieSecteurUpdate () RETURNS TRIGGER AS $fonctionRecupCommuneChefferieSecteurUpdate$
	BEGIN
		INSERT INTO recupQueryUpdate(requete) VALUES('' || OLD.id || ';' || NEW.id_villeTerritoire || ';' || NEW.designation || ';' || NEW.superficie || '|communeChefferieSecteur');
		RAISE INFO '%;%;%;%|communeChefferieSecteur',OLD.id,NEW.id_villeTerritoire,NEW.designation,NEW.superficie;
		RETURN NULL;
	END	
	$fonctionRecupCommuneChefferieSecteurUpdate$ LANGUAGE plpgsql;
	
--Trigger pour communeChefferieSecteur	
CREATE TRIGGER recupCommuneChefferieSecteurUpdate AFTER UPDATE ON communeChefferieSecteur	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupCommuneChefferieSecteurUpdate();

--Fonction pour quartierLocalite
CREATE FUNCTION fonctionRecupQuartierLocaliteUpdate () RETURNS TRIGGER AS $fonctionRecupQuartierLocaliteUpdate$
	BEGIN
		INSERT INTO recupQueryUpdate(requete) VALUES('' || OLD.id || ';' || NEW.id_communeChefferieSecteur || ';' || NEW.designation || ';' || NEW.superficie || '|quartierLocalite');
		RAISE INFO '%;%;%;%|quartierLocalite',OLD.id,NEW.id_communeChefferieSecteur,NEW.designation,NEW.superficie;
		RETURN NULL;
	END	
	$fonctionRecupQuartierLocaliteUpdate$ LANGUAGE plpgsql;
	
--Trigger pour quartierLocalite	
CREATE TRIGGER recupQuartierLocaliteUpdate AFTER UPDATE ON quartierLocalite	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupQuartierLocaliteUpdate();    

--Fonction pour avenueVillage
CREATE FUNCTION fonctionRecupAvenueVillageUpdate () RETURNS TRIGGER AS $fonctionRecupAvenueVillageUpdate$
	BEGIN
		INSERT INTO recupQueryUpdate(requete) VALUES('' || OLD.id || ';' || NEW.id_quartierLocalite || ';' || NEW.designation || '|avenueVillage');
		RAISE INFO '%;%;%|avenueVillage',OLD.id,NEW.id_quartierLocalite,NEW.designation;
		RETURN NULL;
	END	
	$fonctionRecupAvenueVillageUpdate$ LANGUAGE plpgsql;
	
--Trigger pour avenueVillage	
CREATE TRIGGER recupAvenueVillageUpdate AFTER UPDATE ON avenueVillage	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupAvenueVillageUpdate(); 

--Fonction pour personne
CREATE FUNCTION fonctionRecupPersonneUpdate () RETURNS TRIGGER AS $fonctionRecupPersonneUpdate$
	DECLARE
		idPere text;
		idMere text;
		numAv text;
		pNomPers text;
		prenomPers text;
		dNaiss text;
		dDeces text;
	BEGIN
		IF NEW.id_pere is NULL THEN
			idPere = 'NULL';
		ELSE idPere = NEW.id_pere;
		END IF;
		IF NEW.id_mere is NULL THEN
			idMere = 'NULL';
		ELSE idMere = NEW.id_mere;
		END IF;
		IF NEW.numero is NULL THEN
			numAv = 'NULL';
		ELSE numAv = NEW.numero;
		END IF;
		IF NEW.postnom is NULL THEN
			pNomPers = 'NULL';
		ELSE pNomPers = NEW.postnom;
		END IF;
		IF NEW.prenom is NULL THEN
			prenomPers = 'NULL';
		ELSE prenomPers = NEW.prenom;
		END IF;
		IF NEW.datenaissance is NULL THEN
			dNaiss = 'NULL';
		ELSE dNaiss = NEW.datenaissance;
		END IF;
		IF NEW.datedeces is NULL THEN
			dDeces = 'NULL';
		ELSE dDeces = NEW.datedeces;
		END IF;

		INSERT INTO recupQueryUpdate(requete) VALUES('' || OLD.id || ';' || NEW.id_avenueVillage || ';' || idPere || ';' || idMere || ';' || NEW.numeroNational || ';' || numAv || ';' || NEW.nom || ';' || pNomPers || 
		';' || prenomPers || ';' || NEW.sexe || ';' || NEW.etativil || ';' || dNaiss || ';' || dDeces || ';' || NEW.travail || ';' || NEW.nombreEnfant || ';' || NEW.niveauEtude || '|personne');
		RAISE INFO '%;%;%;%;''%'';''%'';''%'';''%'';''%'';''%'';''%'';''%'';''%'';''%'';%;''%''|personne',OLD.id,NEW.id_avenueVillage,idPere,idMere,NEW.numeroNational,numAv,NEW.nom,pNomPers,prenomPers,NEW.sexe,NEW.etativil,dNaiss,dDeces,NEW.travail,NEW.nombreEnfant,NEW.niveauEtude;
		RETURN NULL;
	END	
	$fonctionRecupPersonneUpdate$ LANGUAGE plpgsql;

--Trigger pour personne	
CREATE TRIGGER recupPersonne AFTER UPDATE ON personne	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupPersonneUpdate();
    
--Fonction pour photo
CREATE FUNCTION fonctionRecupPhotoUpdate () RETURNS TRIGGER AS $fonctionRecupPhotoUpdate$
	DECLARE
		photoPers bytea;
	BEGIN
		IF NEW.photo is NULL THEN
			photoPers = 'NULL';
		ELSE photoPers = NEW.photo;
		END IF;

		INSERT INTO recupQueryUpdate(requete) VALUES('' || OLD.id || ';' || NEW.id_personne || '|photo');
		RAISE INFO '%;%|photo',OLD.id,NEW.id_personne;
		RETURN NULL;
	END	
	$fonctionRecupPhotoUpdate$ LANGUAGE plpgsql;

--Trigger pour photo	
CREATE TRIGGER recupPhotoUpdate AFTER UPDATE ON photo	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupPhotoUpdate();

--Fonction pour categorieUtilisateur
CREATE FUNCTION fonctionRecupCategorieUtilisateurUpdate () RETURNS TRIGGER AS $fonctionRecupCategorieUtilisateurUpdate$
	BEGIN
		INSERT INTO recupQueryUpdate(requete) VALUES('' || OLD.id || ';' || NEW.designation || ';' || NEW.groupe || '|categorieUtilisateur');
		RAISE INFO '%;%;%|categorieUtilisateur',OLD.id,NEW.designation,NEW.groupe;
		RETURN NULL;
	END	
	$fonctionRecupCategorieUtilisateurUpdate$ LANGUAGE plpgsql;
	
--Trigger pour categorieUtilisateur	
CREATE TRIGGER recupCategorieUtilisateurUpdate AFTER UPDATE ON categorieUtilisateur	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupCategorieUtilisateurUpdate(); 

--Fonction pour utilisateur
CREATE FUNCTION fonctionRecupUtilisateurUpdate () RETURNS TRIGGER AS $fonctionRecupUtilisateurUpdate$  
	BEGIN
		INSERT INTO recupQueryUpdate(requete) VALUES('' || OLD.id || ';' || NEW.id_personne || ';' || NEW.id_categorieUtilisateur|| ';' || NEW.activation || ';' || NEW.nomuser || ';' || NEW.motpass || '|utilisateur');
		RAISE INFO '%;%;%;%;%;%|utilisateur',OLD.id,NEW.id_categorieUtilisateur,NEW.designation;
		RETURN NULL;
	END	
	$fonctionRecupUtilisateurUpdate$ LANGUAGE plpgsql;
	
--Trigger pour utilisateur	
CREATE TRIGGER recupUtilisateurUpdate AFTER UPDATE ON utilisateur	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupUtilisateurUpdate();


--Fonction pour telephone
CREATE FUNCTION fonctionRecupTelephoneUpdate () RETURNS TRIGGER AS $fonctionRecupTelephoneUpdate$ 
	BEGIN
		INSERT INTO recupQueryUpdate(requete) VALUES('' || OLD.id || ';' || NEW.id_utilisateur || ';' || NEW.numero|| '|telephone');
		RAISE INFO '%;%;%|telephone',OLD.id,NEW.id_utilisateur,NEW.numero;
		RETURN NULL;
	END	
	$fonctionRecupTelephoneUpdate$ LANGUAGE plpgsql;
	
--Trigger pour telephone	
CREATE TRIGGER recupTelephoneUpdate AFTER UPDATE ON telephone	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupTelephoneUpdate();

--Fonction pour carte
CREATE FUNCTION fonctionRecupCarteUpdate () RETURNS TRIGGER AS $fonctionRecupCarteUpdate$   
	BEGIN
		INSERT INTO recupQueryUpdate(requete) VALUES('' || OLD.id || ';' || NEW.id_personne || ';' || NEW.datelivraison|| '|carte');
		RAISE INFO '%;%;%|carte',OLD.id,NEW.id_personne,NEW.datelivraison;
		RETURN NULL;
	END	
	$fonctionRecupCarteUpdate$ LANGUAGE plpgsql;
	
--Trigger pour carte	
CREATE TRIGGER recupCarteUpdate AFTER UPDATE ON carte	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupCarteUpdate();
	
--Trigger pour réplication du serveur de standby - Basculement intelligent en serveur principal	
CREATE TRIGGER basculMasterStandby AFTER UPDATE ON utilisateur	
    FOR EACH ROW EXECUTE PROCEDURE fonctionRecupUtilisateurUpdate();


--Fonction pour telephone
CREATE FUNCTION executeBasculMasterStandby () RETURNS TRIGGER AS $executeBasculMasterStandby$ 
	BEGIN
		SMART;
	END	
	$executeBasculMasterStandby$ LANGUAGE plpgsql;

update villeTerritoire set designation='GOMA',superficie=25800 where id=1;
update communeChefferieSecteur set id_villeTerritoire=1,designation='GOMA',superficie=12500 where id=1;
update quartierLocalite set id_communeChefferieSecteur=1,designation='KATOYI',superficie=2500 where id=1;
update avenueVillage set id_quartierLocalite=1,designation='BUKONDE' where id=1;

update personne set id_avenueVillage=1,id_pere=null,id_mere=null,numeroNational='NK11',numero='99',nom='NKEMBO',
postnom='NKEMBO',prenom='JOSUE',sexe='M',etativil='CELIBATAIRE',datenaissance='1991-02-11',datedeces=null,
travail=TRUE,nombreEnfant=0,niveauEtude='LICENCIE' where id=1;

update personne set id_avenueVillage=1,id_pere=null,id_mere=null,numeroNational='NK11',numero='99',nom='NKEMBO',
postnom=null,prenom=null,sexe='M',etativil='CELIBATAIRE',datenaissance=null,datedeces=null,
travail='0',nombreEnfant=1,niveauEtude='LICENCIE' where id=1;

update photo set id_personne=1,photo=null where id=1;
update categorieUtilisateur set designation='',groupe='' where id=1;
update utilisateur set id_personne=1,id_categorieUtilisateur=1,activation=TRUE,nomuser='josam',motpass='jos',groupe='' where id=1;
update telephone set id_utilisateur=1,numero='' where id=1;
update carte set id_personne=1,datelivraison='' where id=1;

