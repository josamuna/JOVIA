--Creation du script de la base de données bdrecensement créé le 25/08/2012
--Private user

create database bdrecensement;

--table villeTerritoire

create table villeTerritoire
(
	id integer,
	designation varchar(50) not null,
	superficie integer,
	constraint pk_villeTerritoire primary key(id)
);

--table communeChefferieSecteur

create table communeChefferieSecteur
(
	id integer,
	id_villeTerritoire integer not null,
	designation varchar(50) not null,
	superficie integer,
	constraint pk_communeChefferieSecteur primary key(id),
	constraint fk_villeTerritoire_communeChefferieSecteur foreign key(id_villeTerritoire) references villeTerritoire(id)
);

--table quartierLocalite

create table quartierLocalite
(
	id integer,
	id_communeChefferieSecteur integer not null,
	designation varchar(50) not null,
	superficie integer,
	constraint pk_quartierLocalite primary key(id),
	constraint fk_quartierLocalite_communeChefferieSecteur foreign key(id_communeChefferieSecteur) references communeChefferieSecteur(id)
);

--table avenueVillage

create table avenueVillage
(
	id integer,
	id_quartierLocalite integer not null,
	designation varchar(50) not null,
	constraint pk_avenueVillage primary key(id),
	constraint fk_avenueVillage_quartierLocalite foreign key(id_quartierLocalite) references quartierLocalite(id)
);

--table personne

create table personne
(
	id integer,
	id_avenueVillage integer not null,
	id_pere integer,--id_pere,id_mere
	id_mere integer,
	numeroNational varchar(12) not null,--numeroNational,numero,nombreEnfant,niveauEtude
	numero varchar(10),
	nom varchar(50) not null,--id,id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,nombreEnfant,niveauEtude
	postnom varchar(50),
	prenom varchar(30),
	sexe varchar(1) default 'M',
	etativil varchar(15) default 'CELIBATAIRE',
	datenaissance date,
	datedeces date,
	travail bool not null,
	nombreEnfant integer,
	niveauEtude varchar(15),
	anneeSaved integer,
	constraint pk_personne primary key(id),
	constraint fk_personne_pere foreign key(id_pere) references personne(id),
	constraint fk_personne_mere foreign key(id_mere) references personne(id),
	constraint fk_personne_avenueVillage foreign key(id_avenueVillage) references avenueVillage(id),	
	constraint uk_personne unique(numeroNational,numero,nom,postnom,prenom,id_pere,id_mere,sexe,etativil,travail,datenaissance,datedeces,nombreEnfant,niveauEtude),
	constraint uk_numeroNational unique(numeroNational)
);

--table photo

create table photo
(
	id integer,
	id_personne integer not null,--id,id_personne,photo
	photo bytea,
	constraint pk_photo primary key(id),
	constraint fk_photo_personne foreign key(id_personne) references personne(id)
);

--table categorieUtilisateur

create table categorieUtilisateur
(
	id integer,
	designation varchar(50) not null,
	groupe varchar(20) not null,
	constraint pk_categorieUtilisateur primary key(id),
	constraint uk_designation unique(designation)
);

--table utilisateur

create table utilisateur
(
	id integer,
	id_personne integer not null,
	id_categorieUtilisateur integer not null,
	activation bool default false,
	nomuser varchar(50) not null,
	motpass varchar(50) not null,
	constraint pk_utilisateur primary key(id),
	constraint fk_utilisateur_personne foreign key(id_personne) references personne(id),
	constraint fk_utilisateur_categorieUtilisateur foreign key(id_categorieUtilisateur) references categorieUtilisateur(id),
	constraint uk_utilisateur_user unique(nomuser)
);

--table telephone

create table telephone
(
	id integer,
	numero varchar(14) not null,
	id_utilisateur integer not null,
	constraint pk_telpephone primary key(id),
	constraint fk_telephone_utilisateur foreign key(id_utilisateur) references utilisateur(id),
	constraint uk_telephone unique(numero)
);

--table carte

create table carte
(
	id integer,
	id_personne integer not null,
	datelivraison date not null,
	constraint pk_carte primary key(id),
	constraint fk_carte_personne foreign key(id_personne) references personne(id),
	constraint uk_carte_personne unique(id_personne,datelivraison)
);

--table envoie

create table envoie
(
	id integer,
	numerotelephone varchar(14) not null,
	message_envoye varchar(200)not null,
	dateenvoie date not null,
	constraint pk_envoie primary key(id)
);

--table envoieMsgAgent

create table envoieMsgAgent
(
	id integer,
	message_envoye varchar(200)not null,
	dateenvoie date not null,
	constraint pk_envoieMsgAgent primary key(id)
);

--table erreurEnvoie

create table erreurEnvoie
(
	id integer,
	expediteur varchar(14) not null,
	message varchar(200),
	date_envoie date not null,
	erreur varchar(5000),
	constraint pk_erreurEnvoie primary key(id)
);

--table parametreAgentSMS

create table parametreAgentSMS
(
	id integer,
	nomuser varchar(50) not null,
	motpass varchar(50) not null,
	temp_debut integer not null,
	delais integer not null,
	numeroTel varchar(14) not null,
	constraint pk_parametreAgentSMS primary key(id),
	constraint uk_numeroTelAgentRecensseur unique(numeroTel)
);

--table recupQuery

create table recupQuery
(
	id serial8,
	requete varchar(6000) not null,
	photo bytea,
	constraint pk_recupQuery primary key(id)
);

--table recupQuery

create table recupQueryUpdate
(
	id serial8,
	requete varchar(1000) not null,
	photo bytea,
	constraint pk_recupQueryUpdate primary key(id)
);

--table parametreProvince

create table parametreProvince
(
	id integer,
	id_province integer not null,
	designation varchar(50) not null,
	superficie integer,
	constraint pk_parametreProvince primary key(id)
);

--table valuesTemp

-- create table valuesTemp
-- (
-- 	id serial8,
-- 	tableName varchar(50) not null,
-- 	designation_province varchar(50),
-- 	designation_villeTerritoire varchar(50),
-- 	designation_communeChefferieSecteur varchar(50),
-- 	designation_quartierLocalite varchar(50),
-- 	anneeNaissance integer,
-- 	anneeDeces integer,
-- 	constraint pk_valuesTemp primary key(id)
-- );