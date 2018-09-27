--Trigger villeTerritoire
CREATE TRIGGER recupInsertVilleTerritoire AFTER INSERT ON villeTerritoire	
    FOR EACH ROW 
	DECLARE
		@requeteAff varchar(6000)
	BEGIN
		INSERT INTO recupQuery(requete) VALUES('insert into villeTerritoire(designation,superficie,id,id_province) values(''' + NEW.designation + ''',' + NEW.superficie + ',|villeTerritoire');
		SET @requeteAff='insert into villeTerritoire(designation,superficie,id,id_province) values(''' + NEW.designation + ''',' + NEW.superficie + ',|villeTerritoire';
		SELECT @requeteAff;
	END;



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
	BEGIN

		INSERT INTO recupQuery(requete) VALUES('insert into personne(id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,nombreEnfant,niveauEtude,id) 
		values('|| NEW.id_avenueVillage || ',''' || IFNULL(NEW.id_pere,'NULL') || ''',''' || IFNULL(NEW.id_mere,'NULL') || ''',''' || NEW.numeroNational || ''',''' || IFNULL(NEW.numero,'NULL') || ''',''' || NEW.nom || ''',''' || IFNULL(NEW.postnom,'NULL') || 
		''',''' || IFNULL(NEW.prenom,'NULL') || ''',''' || NEW.sexe || ''',''' || NEW.etativil || ''',''' || IFNULL(NEW.datenaissance,'NULL') || ''',''' || IFNULL(NEW.datedeces,'NULL') || ''',' || NEW.travail || ',' || NEW.nombreEnfant || ',''' || NEW.niveauEtude || ''',|personne');
		
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
	BEGIN
		INSERT INTO recupQuery(requete) VALUES('insert into photo(id_personne,photo,id) values(' || NEW.id_personne || ',''' || IFNULL(NEW.photo,'NULL') || ''',|photo');
		RAISE INFO 'insert into photo(id_personne,photo,id) values(%,''%'',|photo',NEW.id_personne,photoPers;
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
		

insert into villeTerritoire(id,designation,superficie) values(1,'NYIRAGONGO',23485);
insert into villeTerritoire(id,designation,superficie)values (2,'GOMA',18345);
insert into communeChefferieSecteur(id,id_villeTerritoire,designation,superficie)values (1,1,'LA KENYA',1398);
insert into quartierLocalite(id,id_communeChefferieSecteur,designation,superficie) values (1,1,'LUSHI',1886);
insert into avenueVillage(id,id_quartierLocalite,designation) values (1,1,'DES ANGES');
insert into personne (id,id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,niveauEtude,nombreEnfant) values (1,1,NULL,NULL,'nk11',1,'ISAMUNA','NKEMBO','JOSUE','M','CELIBATAIRE','23/11/1991',NULL,true,'D4',1);
insert into personne (id,id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,niveauEtude,nombreEnfant) values (2,1,NULL,NULL,'nk12',1,'ISAMUNA',NULL,NULL,'M','CELIBATAIRE','23/11/1991','12/12/2000',true,'D4',1);
insert into personne (id,id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,niveauEtude,nombreEnfant) values (3,1,NULL,NULL,'nk13',1,'MUBALAMA','JANVIER',NULL,'M','CELIBATAIRE',NULL,NULL,true,'D6',1);
insert into personne (id,id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,niveauEtude,nombreEnfant) values (4,1,NULL,NULL,'nk14',1,'MUBA','JEAN',NULL,'M','CELIBATAIRE',NULL,NULL,true,'D6',1);
insert into photo(id,id_personne,photo) values (1,1,null);
insert into carte(id,id_personne,datelivraison) values (1,1,'05/02/2013');
insert into categorieUtilisateur(id,designation,groupe) values (1,'Administrateur national','Administrateur');
insert into utilisateur(id,id_personne,id_categorieUtilisateur,activation,nomuser,motpass) values (1,1,1,FALSE,'josam','jos');
insert into telephone(id,numero,id_utilisateur) values (1,'0813870366',1);
--insert into parametreProvince(id,id_province,designation) values(1,1,'Nord Kivu')

select * from recupQuery
select * from personne;
select * from recupQuery;
select * from avenueVillage;
create table test(id int,n bytea) 
insert into test(id,n) values(1,'ferifrehfjhrjkherjhjre')
select *from test

------insert into province(id,designation,superficie) values(1,'Nord kivu',560500)
------insert into province(id,designation,superficie) values(2,'Sud kivu',750500)
