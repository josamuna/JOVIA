insert into valuesTemp(tableName,designation_province,designation_villeTerritoire,designation_communeChefferieSecteur,designation_quartierLocalite,anneeNaissance,anneeDeces)
values ('quartierLocalite','NORD KIVU','GOMA','NYABANYIRA','KATOYI',2010,2010)

insert into province(designation,superficie)
values('nk',2000)

insert into villeTerritoire(id_province,designation,superficie)
values(45,'kik',1500)

insert into personne(id_avenueVillage,nom,postnom,prenom,nomPere,nomMere,sexe,etativil,travail)
values(4,'ISAMUNA','NKEMBO','JOSUE','ISAMUNA','JOSEE','M','CELIBATAIRE',TRUE)

SELECT * FROM recensseur avenueVillage personne 

alter table personne 
add column id_avenueVillage integer not null;
alter table personne 
add constraint fk_personne_avenueVillage foreign key(id_avenueVillage) references avenueVillage(id);
SELECT * FROM personne
DELETE FROM PERSONNE WHERE id=2
SELECT * FROM photo
update photo set id=1
alter table personne 
add constraint uk_nom_personne_parents unique(nom,postnom,prenom,nomPere,nomMere);

alter table recensseur
add constraint uk_recensseur_tel unique(numerotelephone);

alter table recensseur 
add constraint uk_recensseur_user unique(nomuser);
--table telephone

SELECT c.id AS idCatUser,c.designation AS designationCat,c.groupe AS groupe,u.id_personne AS id_personne
                FROM categorieUtilisateur AS c INNER JOIN utilisateur AS u ON c.id=u.id_categorieUtilisateur WHERE nomuser='josam' AND motpass='jos'

alter table personne 
drop constraint fk_personne_mere
SELECT nomuser,motpass FROM recensseurAgentcommune WHERE nomuser='josamm' AND motpass='josbs'
SELECT categorieagent FROM recensseurAgentcommune WHERE nomuser='josam' AND motpass='jos'
SELECT *FROM personne order by id WHERE nommere='kubata';
SELECT *FROM erreurEnvoie
SELECT *FROM naissance;
SELECT *FROM province;
SELECT *FROM serveur;
SELECT *FROM communeChefferieSecteur;
SELECT *FROM quartierLocalite;
SELECT *FROM valuesTemp;
SELECT id FROM personne
SELECT * FROM province ORDER BY id ASC
SELECT ceil(sqrt(superficie)) as sqrt FROM villeTerritoire where id=2
SELECT sqrt(superficie) as sqrt FROM villeTerritoire where id=2
SELECT round(sqrt(superficie)) as sqrt FROM villeTerritoire where id=2




---

SELECT communeChefferieSecteur.designation AS desiProv,((count(personne.id))/(SELECT parametreProvince.superficie FROM parametreProvince WHERE parametreProvince.designation=(SELECT valuesTemp.designation_province FROM valuesTemp))) as densitePop
			FROM communeChefferieSecteur,personne group by communeChefferieSecteur.designation

			
SELECT parametreProvince.designation AS desiProv,((count(personne.id))/(SELECT parametreProvince.superficie FROM parametreProvince WHERE parametreProvince.designation=(SELECT valuesTemp.designation_province FROM valuesTemp))) as densitePop
			FROM parametreProvince,personne group by parametreProvince.designation

(SELECT villeTerritoire.superficie FROM villeTerritoire WHERE villeTerritoire.designation=(SELECT valuesTemp.designation_villeTerritoire FROM valuesTemp))
SELECT *FROM categorieUtilisateur
SELECT *FROM utilisateur
SELECT *FROM parametreAgentSMS
SELECT *FROM utilisateur order by id asc
SELECT *FROM telephone
SELECT * FROM categorieutilisateur
drop table categorieutilisateur cascade

create table valuesTemp
(
	id serial8,
	designation_province varchar(50),
	designation_villeTerritoire varchar(50),
	designation_communeChefferieSecteur varchar(50),
	designation_quartierLocalite varchar(50),
	designation_teritoire varchar(50),
	constraint pk_valuesTemp primary key(id)
);
alter table categorieUtilisateur 
drop constraint uk_niveau


INSERT INTO parametreAgentSMS(nomuser,motpass,temp_debut,delais) 
VALUES('vk','via',14,17)

                                insert into parametreAgentSMS(nomuser,motpass,temp_debut,delais)
                                values('vianney k','vk',16,19)

UPDATE parametreAgentSMS SET temp_debut=19,delais=23 WHERE id=1
SELECT *FROM parametreAgentSMS
SELECT *FROM personne
SELECT id FROM personne WHERE numeroNational='SK9'
update personne set datedeces=null WHERE id=5
SELECT motpass FROM utilisateur WHERE nomuser='jos'

update categorieUtilisateur set designation='Administrateur', niveau=0 WHERE id=1
update categorieUtilisateur set designation='Recensseur', niveau=1 WHERE id=2
update categorieUtilisateur set designation='Agent de commune', niveau=2 WHERE id=3

SELECT c.id AS idCatUser,c.designation AS designationCat,c.niveau AS levelUser,u.id_personne AS id_personne FROM categorieUtilisateur AS c
INNER JOIN utilisateur AS u ON c.id=u.id_categorieUtilisateur
WHERE nomuser='josam' AND motpass='jos'

delete FROM recensseurAgentcommune;
SELECT *FROM recensseurAgentcommune;
insert into recensseurAgentcommune(id_personne,nomuser,motpass,numerotelephone,categorieagent)
values(38,'josam','jos','0991350532',2);
insert into naissance(id_personne,datenaissance)
values(6,'13/06/1980');
insert into recensseurAgentcommune(id_personne,nomuser,motpass,numerotelephone,categorieagent)
values(38,'josam','jos','0991350532',1);

insert into naissance(id_personne,datenaissance) values(9,'11/12/2012');

SELECT id FROM naissance WHERE id_personne=7;
delete FROM naissance WHERE id=34
delete FROM deces WHERE id=3
update naissance set datenaissance='15/05/1977' WHERE id=1;id_personne=7,
update naissance set datenaissance=null WHERE id=4;
SELECT *FROM erreurEnvoie  
  
SELECT *FROM deces
SELECT *FROM province
SELECT *FROM avenueVillage 
SELECT *FROM utilisateur
SELECT *FROM categorieutilisateur
SELECT * FROM recensseurAgentcommune  
SELECT *FROM personne WHERE id=2
SELECT *FROM personne WHERE id=3
SELECT datenaissance FROM naissance WHERE id_personne=2
SELECT d.id_personne AS id,r.categorieagent AS categorie FROM recensseurAgentcommune AS r LEFT OUTER JOIN deces AS d
ON r.id_personne=d.id_personne WHERE nomuser='josam' AND motpass='jos'

SELECT *FROM province
insert into province(designation,superficie)
values ('KATANGA',4560006)

insert into categorieutilisateur(designation,niveau)
values ('josam',1),('vk',2),('NDOLE',3)

insert into utilisateur(id_personne,id_categorieutilisateur,nomuser,motpass)
values (1,1,'josam','jos'),(2,2,'vianney k','vk'),(3,3,'Benja','ben')

SELECT *FROM villeTerritoire
insert into villeTerritoire(id_province,designation,superficie)
values (9,'KABALO',12345)

insert into villeTerritoire(id_province,designation,superficie)
values (null,'RUTHURU',756)

SELECT *FROM communeChefferieSecteur
insert into communeChefferieSecteur(id_villeTerritoire,designation,superficie)
values (8,'LA KENYA',1398)

SELECT *FROM quartierLocalite
insert into quartierLocalite(id_communeChefferieSecteur,designation,superficie)
values (8,'LUSHI',1886)

SELECT *FROM avenueVillage
insert into avenueVillage(id_quartierLocalite,designation)
values (8,'DES ANGES')

SELECT *FROM communeChefferieSecteur
SELECT *FROM avenueVillage
SELECT designation FROM avenueVillage ORDER BY id ASC

SELECT    province.designation AS designation
FROM      avenueVillage AS av INNER JOIN
          quartierLocalite ON av.id_quartierLocalite = quartierLocalite.id INNER JOIN
          communeChefferieSecteur ON quartierLocalite.id_communeChefferieSecteur = communeChefferieSecteur.id INNER JOIN
          villeTerritoire ON communeChefferieSecteur.id_villeTerritoire = villeTerritoire.id INNER JOIN
          province ON villeTerritoire.id_province = province.id
WHERE av.id=1
SELECT *FROM personne WHERE numeroNational=''

SELECT nom,postnom,prenom,numeroNational FROM personne ORDER BY id ASC


insert into avenueVillage(id_quartierLocalite,designation)
values(4,'BUKONDE'),(6,'KUMATA')

update avenueVillage set id_quartierLocalite=6 WHERE id=3

SELECT id,id_quartierLocalite FROM avenueVillage WHERE id_quartierLocalite=4
AND designation='BUKONDE'


drop table deces


alter table personne
drop column  sexe;
alter table personne
drop column  travail;
alter table personne
drop column  photo;
alter table personne
drop constraint uk_nom_personne;

drop table deces cascade
add column sexe varchar(1) default 'M';
alter table personne
add column travail bool not null;
alter table personne
add column photo bytea;
alter table personne
add column nomPere varchar(200)not null;
alter table personne
add column nomMere varchar(200);
alter table personne
add constraint uk_nom_pere unique(nomPere);
alter table personne
add constraint uk_nom_mere unique(nomMere);
alter table personne
add constraint uk_nom_personne unique(nomPere,nomMere,nom,postnom,prenom);

create table test(id serial8)
insert into test(id) values(2);
SELECT *FROM test

create table test1(id serial8,nom varchar(10));
insert into test1(nom) values('Jean');
SELECT *FROM serveur;


drop table naissance cascade
alter table quartierLocalite
add constraint uk_quartierLocalite unique(id_communeChefferieSecteur,designation);
add constraint uk_communeChefferieSecteur unique(id_villeTerritoire,designation);
add constraint uk_deces_personne unique(id_personne,datedeces);
add column tr bool;
create table test(id serial8,tr varchar(15) not null,dt date)
insert into testJo(nom,tr) values('Good1',true);
insert into testJo(nom,tr) values('Good1',false);
SELECT *FROM testJo;

insert into testJo (nom)) values (UPPER('kasi'));
('paps'),('grec'),('cres'),('cele'),('celestin'),('jo'),('josam'),('via'),('josam'),
('kimasi'),('yeyes'),('nathali'),('nad'),('ruda'),('yannick'),('ergan'),('kassa'),('serge'),('sedjo'),
('jaman'),('niga'),('keneth'),('cedric'),('sifa'),('asifiwe'),('jida'),('jamanic'),('joamani'),('bisimwa');

SELECT photo FROM photo WHERE id=3
SELECT numero,designation FROM avenueVillage ORDER BY id ASC;
delete FROM testJo;
drop table deces cascade;
drop table personne cascade;
drop table naissance cascade
SELECT *FROM personne
SELECT *FROM utilisateur
SELECT id FROM utilisateur WHERE nomuser='josam'
SELECT *FROM recensseurAgentcommune;villeTerritoire WHERE id_personne=38;


SELECT ph.id AS id,ph.id_personne AS idPers,ph.photo AS photo FROM photo AS ph LEFT OUTER JOIN personne AS p
ON p.id=ph.id_personne WHERE p.nom='' AND p.postnom='' AND p.prenom=''

SELECT datenaissance FROM personne WHERE numeroNational=''

update recensseurAgentcommune set numerotelephone='0995576689' WHERE id=1
update naissance set id_personne=38 WHERE id=30;SELECT *FROM erreurEnvoie; ORDER BY id ASC;
SELECT nomuser,motpass FROM recensseur WHERE numerotelephone='0991350532';
SELECT numero,designation FROM avenueVillage ORDER BY id ASC;
SELECT id,designation FROM province;
SELECT id,designation,superficie FROM province WHERE designation='KATANGA' AND superficie='4568500';
SELECT id FROM province WHERE designation='Katanga' AND superficie='4568500';
SELECT designation,superficie FROM province ORDER BY designation ASC;

insert into communeChefferieSecteur (id_villeTerritoire,designation,superficie)
 values (1,'Ndosho',2000);

 insert into communeChefferieSecteur (id_villeTerritoire,designation,superficie)
 values (1,'Goma',2200);

  insert into quartierLocalite (id_communeChefferieSecteur,designation,superficie)
 values (1,'BUKONDE',2200);
 
 insert into recensseur (id_personne,nomuser,motpass)
 values (1,'Ndosho','java');

SELECT *FROM personne
SELECT *FROM photo
SELECT * FROM personne WHERE id=1
insert into personne (id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,nombreEnfant,niveauEtude)
values (2,null,null,'NK1',10,'ISAMUNA','NKEMBO','JOSUE','M','CELIBATAIRE','23/11/1991',null,true,0,'D4')
 

 SELECT MAX(id) AS maxid FROM recensseur;

truncate table personne cascade;
drop table personne cascade;
drop table photo cascade;
truncate table naissance cascade;
truncate table communeChefferieSecteur cascade;
truncate table quartierLocalite cascade;
truncate table villeTerritoire cascade;
truncate table province cascade;
truncate table avenueVillage cascade;
alter table utilisateur
add column activation bool default false

SELECT *FROM avenueVillage

delete FROM villeTerritoire WHERE id=8;
SELECT *FROM personne photo --WHERE id_personne=1
SELECT photo FROM photo WHERE id=1
update photo set photo=null WHERE id=1

SELECT MAX(id) AS maxid FROM personne;
SELECT MAX(id) AS maxid FROM avenueVillage;


insert into parametreProvince(id_province,designation) values(1,'Nord Kivu')

SELECT parametreAgentSMS.delais AS delais,telephone.numero AS numero 
FROM   utilisateur INNER JOIN telephone ON utilisateur.id = telephone.id_utilisateur INNER JOIN
       parametreAgentSMS ON utilisateur.nomuser = parametreAgentSMS.nomuser
WHERE (numero = '0990678028')

SELECT id,designation,superficie FROM villeTerritoire WHERE designation LIKE 'BENI%'

SELECT id, id_avenuevillage, id_pere, id_mere, id_serveur, numeronational, 
       numero, nom, postnom, prenom, sexe, etativil, datenaissance, 
       datedeces, travail, nombreenfant, niveauetude
  FROM personne;
  delete FROM personne
SELECT datedeces FROM personne
SELECT *FROM photo


SELECT p.id AS idPersPhoto FROM photo AS p INNER JOIN personne AS pers ON 
            pers.id=p.id_personne WHERE pers.id=1 and pers.numeroNational='NK11'

SELECT c.id AS idCatUser,c.designation AS designationCat,c.groupe AS groupe,u.id_personne AS id_personne
FROM categorieUtilisateur AS c INNER JOIN utilisateur AS u ON c.id=u.id_categorieUtilisateur WHERE nomuser=null AND motpass=null

--Taux de natalite
SELECT extract(year FROM timestamp '2001/02/16')
--Taux de mortalite
--Taux de croissance
alter table utilisateur
add column activation bool default false

create table test4
(
id int not null,
nom varchar(100),
pnom varchar(100),
sexe varchar(1)
);
drop table test4;
SELECT groupe FROM categorieUtilisateur 
INNER JOIN utilisateur ON categorieUtilisateur.id=utilisateur.id_categorieUtilisateur
INNER JOIN telephone ON utilisateur.id=telephone.id_utilisateur WHERE telephone.numero='0991350532'