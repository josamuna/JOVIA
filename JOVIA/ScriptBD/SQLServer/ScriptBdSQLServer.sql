--Creation du script de la base de données bdrecensement créé le 25/08/2012
--Private user

create database bdrecensement
go
use bdrecensement
go

--table villeTerritoire

create table villeTerritoire
(
	id int,
	designation varchar(50) not null,
	superficie int,
	constraint pk_villeTerritoire primary key(id)
)
go

--table communeChefferieSecteur

create table communeChefferieSecteur
(
	id int,
	id_villeTerritoire int not null,
	designation varchar(50) not null,
	superficie int,
	constraint pk_communeChefferieSecteur primary key(id),
	constraint fk_villeTerritoire_communeChefferieSecteur foreign key(id_villeTerritoire) references villeTerritoire(id)
)
go

--table quartierLocalite

create table quartierLocalite
(
	id int,
	id_communeChefferieSecteur int not null,
	designation varchar(50) not null,
	superficie int,
	constraint pk_quartierLocalite primary key(id),
	constraint fk_quartierLocalite_communeChefferieSecteur foreign key(id_communeChefferieSecteur) references communeChefferieSecteur(id)
)
go

--table avenueVillage

create table avenueVillage
(
	id int,
	id_quartierLocalite int not null,
	designation varchar(50) not null,
	constraint pk_avenueVillage primary key(id),
	constraint fk_avenueVillage_quartierLocalite foreign key(id_quartierLocalite) references quartierLocalite(id)
)
go

--table personne

create table personne
(
	id int,
	id_avenueVillage int not null,
	id_pere int,--id_pere,id_mere
	id_mere int,
	numeroNational varchar(12) not null,--numeroNational,numero,nombreEnfant,niveauEtude
	numero varchar(10),
	nom varchar(50) not null,--id_avenueVillage,id_pere,id_mere,numero,nom,postnom,prenom,sexe,etativil,travail,datenaissance,datedeces,nombreEnfant,niveauEtude
	postnom varchar(50),
	prenom varchar(30),
	sexe varchar(1) default 'M',
	etativil varchar(15) default 'CELIBATAIRE',
	datenaissance smalldatetime,
	datedeces smalldatetime,
	travail bit not null,
	nombreEnfant int,
	niveauEtude varchar(15),
	constraint pk_personne primary key(id),
	constraint fk_personne_pere foreign key(id_pere) references personne(id),
	constraint fk_personne_mere foreign key(id_mere) references personne(id),
	constraint fk_personne_avenueVillage foreign key(id_avenueVillage) references avenueVillage(id),	
	constraint uk_personne unique(numeroNational,numero,nom,postnom,prenom,id_pere,id_mere,sexe,etativil,travail,datenaissance,datedeces,nombreEnfant,niveauEtude),
	constraint uk_numeroNational unique(numeroNational)
)
go

--table photo

create table photo
(
	id int,
	id_personne int not null,
	photo varbinary(max),
	constraint pk_photo primary key(id),
	constraint fk_photo_personne foreign key(id_personne) references personne(id)
)
go

create table categorieUtilisateur
(
	id integer,
	designation varchar(50) not null,
	groupe varchar(20) not null,
	constraint pk_categorieUtilisateur primary key(id),
	constraint uk_designation unique(designation),
	--constraint uk_groupe unique(groupe)
)
go

--table utilisateur

create table utilisateur
(
	id integer,
	id_personne integer not null,
	id_categorieUtilisateur integer not null,
	activation bit default 0,
	nomuser varchar(50) not null,
	motpass varchar(50) not null,
	constraint pk_utilisateur primary key(id),
	constraint fk_utilisateur_personne foreign key(id_personne) references personne(id),
	constraint fk_utilisateur_categorieUtilisateur foreign key(id_categorieUtilisateur) references categorieUtilisateur(id),
	constraint uk_utilisateur_user unique(nomuser)
)
go

--table telephone

create table telephone
(
	id integer,
	numero varchar(14) not null,
	id_utilisateur integer not null,
	constraint pk_telpephone primary key(id),
	constraint fk_telephone_utilisateur foreign key(id_utilisateur) references utilisateur(id),
	constraint uk_telephone unique(numero)
)
go

--table carte

create table carte
(
	id int,
	id_personne int not null,
	datelivraison smalldatetime not null,
	constraint pk_carte primary key(id),
	constraint fk_carte_personne foreign key(id_personne) references personne(id),
	constraint uk_carte_personne unique(id_personne,datelivraison)
)
go

--table envoie

create table envoie
(
	id int,
	numerotelephone varchar(14) not null,
	message_envoye varchar(200)not null,
	dateenvoie smalldatetime not null,
	constraint pk_envoie primary key(id)
)
go

--table envoieMsgAgent

create table envoieMsgAgent
(
	id int,
	message_envoye varchar(200)not null,
	dateenvoie smalldatetime not null,
	constraint pk_envoieMsgAgent primary key(id)
)
go

--table erreurEnvoie

create table erreurEnvoie
(
	id int,
	expediteur varchar(14) not null,
	message varchar(200),
	date_envoie smalldatetime not null,
	erreur varchar(5000),
	constraint pk_erreurEnvoie primary key(id)
)
go

--table parametreAgentSMS

create table parametreAgentSMS
(
	id int,
	nomuser varchar(50) not null,
	motpass varchar(50) not null,
	temp_debut int not null,
	delais int not null,
	constraint pk_parametreAgentSMS primary key(id)
)
go

--table recupQuery

create table recupQuery
(
	id int identity,
	requete varchar(6000) not null,
	photo varbinary(max),
	constraint pk_recupQuery primary key(id)
)
go

--table recupQueryUpdate

create table recupQueryUpdate
(
	id int identity,
	requete varchar(1000) not null,
	photo varbinary(max),
	constraint pk_recupQueryUpdate primary key(id)
)
go

--table parametreProvince

create table parametreProvince
(
	id integer,
	id_province integer not null,
	designation varchar(50) not null,
	superficie integer,
	constraint pk_parametreProvince primary key(id)
)
go

--table valuesTemp

create table valuesTemp
(
	id integer identity,
	tableName varchar(50) not null,
	designation_province varchar(50),
	designation_villeTerritoire varchar(50),
	designation_communeChefferieSecteur varchar(50),
	designation_quartierLocalite varchar(50),
	constraint pk_valuesTemp primary key(id)
)
go