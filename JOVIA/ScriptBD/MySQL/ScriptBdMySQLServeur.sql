--Creation du script de la base de données bdrecensement créé le 25/08/2012
--Private user

create database bdrecensement;

create table province
(
	id integer,
	designation varchar(50) not null,
	superficie integer,
	constraint pk_province primary key(id)
);

create table serveur
(
	id integer,
	id_province integer not null,
	designation varchar(50) not null,
	adresse_ip varchar(50) not null,
	constraint pk_serveur primary key(id),
	constraint fk_serveur_province foreign key(id_province) references province(id),
	constraint uk_serveur unique(designation)
);

create table villeTerritoire
(
	id integer,
	id_province integer not null,
	designation varchar(50) not null,
	superficie integer,
	constraint pk_villeTerritoire primary key(id),
	constraint fk_province_villeTerritoire foreign key(id_province) references province(id)
);

create table communeChefferieSecteur
(
	id integer,
	id_villeTerritoire integer not null,
	designation varchar(50) not null,
	superficie integer,
	constraint pk_communeChefferieSecteur primary key(id),
	constraint fk_villeTerritoire_communeChefferieSecteur foreign key(id_villeTerritoire) references villeTerritoire(id)
);

create table quartierLocalite
(
	id integer,
	id_communeChefferieSecteur integer not null,
	designation varchar(50) not null,
	superficie integer,
	constraint pk_quartierLocalite primary key(id),
	constraint fk_quartierLocalite_communeChefferieSecteur foreign key(id_communeChefferieSecteur) references communeChefferieSecteur(id)
);

create table avenueVillage
(
	id integer,
	id_quartierLocalite integer not null,
	designation varchar(50) not null,
	constraint pk_avenueVillage primary key(id),
	constraint fk_avenueVillage_quartierLocalite foreign key(id_quartierLocalite) references quartierLocalite(id)
);

create table personne
(
	id integer,
	id_avenueVillage integer not null,
	id_pere integer,
	id_mere integer,
	numeroNational varchar(12) not null,
	numero varchar(10),
	nom varchar(50) not null,
	postnom varchar(50),
	prenom varchar(30),
	sexe varchar(1) default 'M',
	etativil varchar(15) default 'CELIBATAIRE',
	datenaissance date,
	datedeces date,
	travail bool not null,
	nombreEnfant integer,
	niveauEtude varchar(15),
	constraint pk_personne primary key(id),
	constraint fk_personne_pere foreign key(id_pere) references personne(id),
	constraint fk_personne_mere foreign key(id_mere) references personne(id),
	constraint fk_personne_avenueVillage foreign key(id_avenueVillage) references avenueVillage(id),	
	constraint uk_personne unique(numeroNational,numero,nom,postnom,prenom,id_pere,id_mere,sexe,etativil,travail,datenaissance,datedeces,nombreEnfant,niveauEtude),
	constraint uk_numeroNational unique(numeroNational)
);

create table photo
(
	id integer,
	id_personne integer not null,
	photo blob,
	constraint pk_photo primary key(id),
	constraint fk_photo_personne foreign key(id_personne) references personne(id)
);

create table categorieUtilisateur
(
	id integer,
	designation varchar(50) not null,
	groupe varchar(20) not null,
	constraint pk_categorieUtilisateur primary key(id),
	constraint uk_designation unique(designation),
	constraint uk_groupe unique(groupe)
);

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

create table telephone
(
	id integer,
	numero varchar(14) not null,
	id_utilisateur integer not null,
	constraint pk_telpephone primary key(id),
	constraint fk_telephone_utilisateur foreign key(id_utilisateur) references utilisateur(id),
	constraint uk_telephone unique(numero)
);

create table carte
(
	id integer,
	id_personne integer not null,
	datelivraison date not null,
	constraint pk_carte primary key(id),
	constraint fk_carte_personne foreign key(id_personne) references personne(id),
	constraint uk_carte_personne unique(id_personne,datelivraison)
);
