--Creation du script de la base de données bdrecensementserveur créé le 25/08/2012
--Private user

create database bdrecensementServeur
go
use bdrecensementServeur
go

--table province 

create table province
(
	id int,
	designation varchar(50) not null,
	superficie int,
	constraint pk_province primary key(id)
)
go

--table serveur 

create table serveur
(
	id int,
	id_province integer not null,
	designation varchar(50) not null,
	adresse_ip varchar(50) not null,
	constraint pk_serveur primary key(id),
	constraint fk_serveur_province foreign key(id_province) references province(id),
	constraint uk_serveur unique(designation)
)
go

--table villeTerritoire

create table villeTerritoire
(
	id int,
	id_province int not null,
	designation varchar(50) not null,
	superficie int,
	constraint pk_villeTerritoire primary key(id),
	constraint fk_province_villeTerritoire foreign key(id_province) references province(id)
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

--table personne 23503	-> erreur de cle etrangere

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
	id_personne int not null,--id,id_personne,photo
	photo varbinary(max),
	constraint pk_photo primary key(id),
	constraint fk_photo_personne foreign key(id_personne) references personne(id)
)
go

--table categorieUtilisateur

create table categorieUtilisateur
(
	id int,
	designation varchar(50) not null,
	groupe varchar(20) not null,
	constraint pk_categorieUtilisateur primary key(id),
	constraint uk_designation unique(designation)
)
go

--table utilisateur

create table utilisateur
(
	id int,
	id_personne int not null,
	id_categorieUtilisateur int not null,
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