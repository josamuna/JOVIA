--Triggers pour villeTerritoire
create trigger recupInsertVilleTerritoire on villeTerritoire after insert
as
	declare @designation varchar(50),@superficie varchar(20),@requete varchar(6000)
	begin
		select @designation=designation,@superficie=superficie from inserted
		set @requete='insert into villeTerritoire(designation,superficie,id,id_province) values(''' + @designation + ''',' + @superficie + ',|villeTerritoire'
		print 'Requete = ' + @requete
		insert into recupQuery(requete) values(@requete)
	end	
go

--Triggers pour communeChefferieSecteur
create trigger recupInsertCommuneChefferieSecteur on communeChefferieSecteur after insert
as
	declare @id_villeTerritoire varchar(10),@designation varchar(50),@superficie varchar(20),@requete varchar(6000)
	begin
		select @id_villeTerritoire=id_villeTerritoire,@designation=designation,@superficie=superficie from inserted
		set @requete='insert into communeChefferieSecteur(id_villeTerritoire,designation,superficie,id) values(' + @id_villeTerritoire + ',''' + @designation + ''',' + @superficie + ',|communeChefferieSecteur'
		print 'Requete = ' + @requete
		insert into recupQuery(requete) values(@requete)
	end	
go

--Triggers pour quartierLocalite
create trigger recupInsertQuartierLocalite on quartierLocalite after insert
as
	declare @id_communeChefferieSecteur varchar(10),@designation varchar(50),@superficie varchar(20),@requete varchar(6000)
	begin
		select @id_communeChefferieSecteur=id_communeChefferieSecteur,@designation=designation,@superficie=superficie from inserted
		set @requete='insert into quartierLocalite(id_communeChefferieSecteur,designation,superficie,id) values(' + @id_communeChefferieSecteur + ',''' + @designation + ''',' + @superficie + ',|quartierLocalite'
		print 'Requete = ' + @requete
		insert into recupQuery(requete) values(@requete)
	end	
go

--Triggers pour avenueVillage
create trigger recupInsertAvenueVillage on avenueVillage after insert
as
	declare @id_quartierLocalite varchar(10),@designation varchar(50),@requete varchar(6000)
	begin
		select @id_quartierLocalite=id_quartierLocalite,@designation=designation from inserted
		set @requete='insert into avenueVillage(id_quartierLocalite,designation,id) values(' + @id_quartierLocalite + ',''' + @designation + ''',|avenueVillage'
		print 'Requete = ' + @requete
		insert into recupQuery(requete) values(@requete)
	end	
go

--Triggers pour personne
create trigger recupInsertPersonne on personne after insert
as
	declare @id_avenueVillage varchar(10),@id_pere varchar(10),@id_mere varchar(10),@numeroNational varchar(12),@numero varchar(10),
	@nom varchar(50),@postnom varchar(50),@prenom varchar(30),@sexe varchar(1),@etativil varchar(15),@datenaissance varchar(20),@datedeces varchar(20),
	@travail varchar(5),@nombreEnfant varchar(3),@niveauEtude varchar(15),@num varchar(2),@requete varchar(6000)
	begin
		select @id_avenueVillage=id_avenueVillage,@id_pere=id_pere,@id_mere=id_mere,@numeroNational=numeroNational,@numero=numero,@nom=nom,
		@postnom=postnom,@prenom=prenom,@sexe=sexe,@etativil=etativil,@datenaissance=datenaissance,@datedeces=datedeces,@travail=travail,
		@nombreEnfant=nombreEnfant,@niveauEtude=niveauEtude from inserted
		
		set @requete='insert into personne(id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,nombreEnfant,niveauEtude,id) 
		values('+ @id_avenueVillage + ',' + isnull(@id_pere,'null') + ',' + isnull(@id_mere,'null') + ',''' + @numeroNational + ''',' + isnull(@numero,'null') + ',''' + @nom + ''',''' + isnull(@postnom,'null') + 
		''',''' + isnull(@prenom,'null') + ''',''' + @sexe + ''',''' + @etativil + ''',''' + isnull(@datenaissance,'null') + ''',''' + isnull(@datedeces,'null') + ''',' + @travail + ',' + @nombreEnfant + ',''' + @niveauEtude + ''',|personne'
		
		print 'Requete = ' + @requete
		insert into recupQuery(requete) values(@requete) 
	end	
go

--Triggers pour photo 
create trigger recupInsertPhoto on photo after insert
as
	declare @id_personne varchar(10),@photo varchar(100),@requete varchar(6000)
	begin
		select @id_personne=id_personne,@photo=photo from inserted
		set @requete='insert into photo(id_personne,id,photo) values(,' + @id_personne + ',|photo'
		print 'Requete = ' + @requete
		insert into recupQuery(requete) values(@requete)
	end	
go

--Triggers pour categorieUtilisateur
create trigger recupInsertCategorieUtilisateur on categorieUtilisateur after insert
as
	declare @designation varchar(50),@groupe varchar(20),@requete varchar(6000)
	begin
		select @designation=designation,@groupe=groupe from inserted
		set @requete='insert into categorieUtilisateur(designation,groupe,id) values(''' + @designation + ''',''' + @groupe + ''',|categorieUtilisateur'
		print 'Requete = ' + @requete
		insert into recupQuery(requete) values(@requete)
	end	
go 

--Triggers pour utilisateur
create trigger recupInsertUtilisateur on utilisateur after insert
as
	declare @id_personne varchar(10),@id_categorieUtilisateur varchar(10),@activation varchar(6),@nomuser varchar(50),@motpass varchar(50),@requete varchar(6000)
	begin
		select @id_personne=id_personne,@id_categorieUtilisateur=id_categorieUtilisateur,@activation=activation,@nomuser=nomuser,@motpass=motpass from inserted
		set @requete='insert into utilisateur(id_personne,id_categorieUtilisateur,activation,nomuser,motpass,id) values(' + @id_personne + ',' + @id_categorieUtilisateur + ',' + @activation + ',''' + @nomuser + ''',''' + @motpass + ''',|utilisateur'
		print 'Requete = ' + @requete
		insert into recupQuery(requete) values(@requete)
	end	
go

--Triggers pour telephone
create trigger recupInserTelephone on telephone after insert
as
	declare @numero varchar(14),@id_utilisateur varchar(14),@requete varchar(6000)
	begin
		select @numero=numero,@id_utilisateur=id_utilisateur from inserted
		set @requete='insert into telephone(numero,id_utilisateur,id) values(''' + @numero + ''',' + @id_utilisateur + ',|telephone'
		print 'Requete = ' + @requete
		insert into recupQuery(requete) values(@requete)
	end	
go

--Triggers pour carte
create trigger recupInserCarte on carte after insert
as
	declare @id_personne varchar(10),@datelivraison varchar(20),@requete varchar(6000)
	begin
		select @id_personne=id_personne,@datelivraison=datelivraison from inserted
		set @requete='insert into carte(id_personne,datelivraison,id) values(' + @id_personne + ',''' + @datelivraison + ''',|carte'
		print 'Requete = ' + @requete
		insert into recupQuery(requete) values(@requete)
	end	
go

--------Triggers pour envoie
------create trigger recupInsertEnvoie on envoie after insert
------as
------	declare @numerotelephone varchar(14),@message_envoye varchar(200),@dateenvoie varchar(20),@requete varchar(6000)
------	begin
------		select @numerotelephone=numerotelephone,@message_envoye=message_envoye,@dateenvoie=dateenvoie from inserted
------		set @requete='insert into envoie(numerotelephone,message_envoye,dateenvoie,id) values(' + @numerotelephone + ',' + @message_envoye + 
------		',' + @dateenvoie + ',|carte'
------		print 'Requete = ' + @requete
------		insert into recupQuery(requete) values(@requete)
------	end	
------go
------
--------Triggers pour envoieMsgAgent
------create trigger recupInsertEnvoieMsgAgent on envoieMsgAgent after insert
------as
------	declare @message_envoye varchar(200),@dateenvoie varchar(20),@requete varchar(6000)
------	begin
------		select @message_envoye=message_envoye,@dateenvoie=dateenvoie from inserted
------		set @requete='insert into envoieMsgAgent(message_envoye,dateenvoie,id) values(' + @message_envoye + ',' + @dateenvoie + ',|envoieMsgAgent'
------		print 'Requete = ' + @requete
------		insert into recupQuery(requete) values(@requete)
------	end	
------go
------
--------Triggers pour erreurEnvoie
------create trigger recupInsertErreurEnvoie on erreurEnvoie after insert
------as
------	declare @expediteur varchar(14),@message varchar(200),@date_envoie varchar(20),@erreur varchar(5000),@requete varchar(6000)
------	begin
------		select @expediteur=expediteur,@message=message,@date_envoie=date_envoie,@erreur=erreur from inserted
------		set @requete='insert into erreurEnvoie(expediteur,message,date_envoie,erreur,id) values(' + @expediteur + ',' + @message + 
------		',' + @date_envoie + ',' + @erreur + ',|erreurEnvoie'
------		print 'Requete = ' + @requete
------		insert into recupQuery(requete) values(@requete)
------	end	
------go

--Suppression triggers
drop trigger recupInsertVilleTerritoire
drop trigger recupInsertCommuneChefferieSecteur
drop trigger recupInsertQuartierLocalite
drop trigger recupInsertAvenueVillage
drop trigger recupInsertPersonne
drop trigger recupInsertPhoto 
drop trigger recupInserCarte
drop trigger recupInsertCategorieUtilisateur
drop trigger recupInsertUtilisateur
drop trigger recupInserTelephone
------drop trigger recupInsertErreurEnvoie
------drop trigger recupInsertEnvoieMsgAgent
------drop trigger recupInsertEnvoie

--Insertions de test
--insert into villeTerritoire(id,designation,superficie) values(1,'NYIRAGONGO',23485)
--insert into villeTerritoire(id,designation,superficie)values (2,'GOMA',18345)
--insert into communeChefferieSecteur(id,id_villeTerritoire,designation,superficie)values (1,1,'LA KENYA',1398)
--insert into quartierLocalite(id,id_communeChefferieSecteur,designation,superficie) values (1,1,'LUSHI',1886)
--insert into avenueVillage(id,id_quartierLocalite,designation) values (1,1,'DES ANGES')
--insert into personne (id,id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,niveauEtude,nombreEnfant) values (1,1,null,null,'nk11',1,'ISAMUNA','NKEMBO','JOSUE','M','CELIBATAIRE','23/11/1991',null,1,'D4',1)
--insert into personne (id,id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,niveauEtude,nombreEnfant) values (2,1,null,null,'nk12',1,'ISAMUNA',null,null,'M','CELIBATAIRE','23/11/1991','12/12/2000',1,'D4',1)
--insert into personne (id,id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,niveauEtude,nombreEnfant) values (3,1,null,null,'nk13',1,'MUBALAMA','JANVIER',null,'M','CELIBATAIRE',null,null,1,'D6',1)
--insert into personne (id,id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,niveauEtude,nombreEnfant) values (4,1,null,null,'nk14',1,'MUBA','JEAN',null,'M','CELIBATAIRE',null,null,1,'D6',1)
--insert into personne (id,id_avenueVillage,id_pere,id_mere,numeroNational,numero,nom,postnom,prenom,sexe,etativil,datenaissance,datedeces,travail,niveauEtude,nombreEnfant) values (5,1,null,null,'nk15',1,'YAMBA','KALOTA',null,'M','CELIBATAIRE','22/05/2000',null,1,'D4',1)
--insert into photo(id,id_personne,photo) values (1,1,null)
--insert into carte(id,id_personne,datelivraison) values (1,1,'05/02.2013')
--insert into categorieUtilisateur(id,designation,groupe) values (1,'Administrateur national','Administrateur')
--insert into utilisateur(id,id_personne,id_categorieUtilisateur,activation,nomuser,motpass) values (1,1,1,1,'josam','jos')
--insert into telephone(id,numero,id_utilisateur) values (1,'0813870366',1)

------insert into envoie(id,numerotelephone,message_envoye,dateenvoie) values (1,'0813870366','Erreur lors de l''enregistrement','05/02.2013')
------insert into erreurEnvoie(id,expediteur,message,date_envoie,erreur) values (1,'0813870366','Insertion','05/02/2013','Impossible d''inserer un enregistrement en doublons, violation de la contrainte unique')
------insert into envoieMsgAgent(id,message_envoye,dateenvoie) values (1,'Dieu est bon ','05/02.2013')
--insert into parametreProvince(id,id_province,designation) values(1,1,'Nord Kivu')
select * from personne
select * from recupQuery
select * from avenueVillage
delete from personne

------drop trigger recupInsertProvince
------
------insert into province(id,designation,superficie) values(1,'Nord kivu',560500)
------insert into province(id,designation,superficie) values(1,'Sud kivu',750500)
------@requete= CONVERT(varchar(1000),'insert into province(designation,superficie) values(' + CONVERT(varchar(50),@designation)+','+@superficie+')')

--Initiakise auto-incremente
--DBCC CHECKIDENT ('province', RESEED, 1)

--================================================================ TRIGGER MODIFICATION ===============
--Triggers pour villeTerritoire
create trigger recupVilleTerritoireUpdate on villeTerritoire after update
as
	declare @id varchar(10),@designation varchar(50),@superficie varchar(20),@requete varchar(1000)
	begin
		select @id=id from deleted
		select @designation=designation,@superficie=superficie from inserted
		set @requete= @id + ';' + @designation + ';' + @superficie + '|villeTerritoire'
		print 'Requete = ' + @requete
		insert into recupQueryUpdate(requete) values(@requete)
	end	
go

--Triggers pour communeChefferieSecteur
create trigger recupCommuneChefferieSecteurUpdate on communeChefferieSecteur after update
as
	declare @id varchar(10),@id_villeTerritoire varchar(10),@designation varchar(50),@superficie varchar(20),@requete varchar(1000)
	begin
		select @id=id from deleted
		select @id_villeTerritoire=id_villeTerritoire,@designation=designation,@superficie=superficie from inserted
		set @requete= @id + ';' + @id_villeTerritoire + ';' + @designation + ';' + @superficie + '|communeChefferieSecteur'
		print 'Requete = ' + @requete
		insert into recupQueryUpdate(requete) values(@requete)
	end	
go

--Triggers pour quartierLocalite
create trigger recupQuartierLocaliteUpdate on quartierLocalite after update
as
	declare @id varchar(10),@id_communeChefferieSecteur varchar(10),@designation varchar(50),@superficie varchar(20),@requete varchar(1000)
	begin
		select @id=id from deleted
		select @id_communeChefferieSecteur=id_communeChefferieSecteur,@designation=designation,@superficie=superficie from inserted
		set @requete= @id + ';' + @id_communeChefferieSecteur + ';' + @designation + ';' + @superficie + '|quartierLocalite'
		print 'Requete = ' + @requete
		insert into recupQueryUpdate(requete) values(@requete)
	end	
go

--Triggers pour avenueVillage
create trigger recupAvenueVillageUpdate on avenueVillage after update
as
	declare @id varchar(10),@id_quartierLocalite varchar(10),@designation varchar(50),@requete varchar(1000)
	begin
		select @id=id from deleted
		select @id_quartierLocalite=id_quartierLocalite,@designation=designation from inserted
		set @requete= @id + ';' + @id_quartierLocalite + ';' + @designation + '|avenueVillage'
		print 'Requete = ' + @requete
		insert into recupQueryUpdate(requete) values(@requete)
	end	
go

--Triggers pour personne
create trigger recupPersonneUpdate on personne after update
as
	declare @id varchar(10),@id_avenueVillage varchar(10),@id_pere varchar(10),@id_mere varchar(10),@numeroNational varchar(12),@numero varchar(10),
	@nom varchar(50),@postnom varchar(50),@prenom varchar(30),@sexe varchar(1),@etativil varchar(15),@datenaissance varchar(20),@datedeces varchar(20),
	@travail varchar(5),@nombreEnfant varchar(3),@niveauEtude varchar(15),@num varchar(2),@requete varchar(1000)
	begin
		select @id=id from deleted
		select @id_avenueVillage=id_avenueVillage,@id_pere=id_pere,@id_mere=id_mere,@numeroNational=numeroNational,@numero=numero,@nom=nom,
		@postnom=postnom,@prenom=prenom,@sexe=sexe,@etativil=etativil,@datenaissance=datenaissance,@datedeces=datedeces,@travail=travail,
		@nombreEnfant=nombreEnfant,@niveauEtude=niveauEtude from inserted
		
		set @requete= @id + ';' + @id_avenueVillage + ';' + isnull(@id_pere,'null') + ';' + isnull(@id_mere,'null') + ';' + @numeroNational + ';' + isnull(@numero,'null') + ';' + @nom + ';' + isnull(@postnom,'null') + 
		';' + isnull(@prenom,'null') + ';' + @sexe + ';' + @etativil + ';' + isnull(@datenaissance,'null') + ';' + isnull(@datedeces,'null') + ';' + @travail + ';' + @nombreEnfant + ';' + @niveauEtude + '|personne'
		
		print 'Requete = ' + @requete
		insert into recupQueryUpdate(requete) values(@requete) 
	end	
go

--Triggers pour photo 
create trigger recupPhotoUpdate on photo after update
as
	declare @id varchar(10),@id_personne varchar(10),@photo varchar(100),@requete varchar(1000)
	begin
		select @id=id from deleted
		select @id_personne=id_personne,@photo=photo from inserted
		set @requete= @id + ';' + @id_personne + '|photo'
		print 'Requete = ' + @requete
		insert into recupQueryUpdate(requete) values(@requete)
	end	
go

--Triggers pour categorieUtilisateur
create trigger recupCategorieUtilisateurUpdate on categorieUtilisateur after update
as
	declare @id varchar(10),@designation varchar(50),@groupe varchar(20),@requete varchar(1000)
	begin
		select @id=id from deleted
		select @designation=designation,@groupe=groupe from inserted
		set @requete= @id + ';' + @designation + ';' + @groupe + '|categorieUtilisateur'
		print 'Requete = ' + @requete
		insert into recupQueryUpdate(requete) values(@requete)
	end	
go 

--Triggers pour utilisateur
create trigger recupUtilisateurUpdate on utilisateur after update
as
	declare @id varchar(10),@id_personne varchar(10),@id_categorieUtilisateur varchar(10),@activation varchar(6),@nomuser varchar(50),@motpass varchar(50),@requete varchar(1000)
	begin
		select @id=id from deleted
		select @id_personne=id_personne,@id_categorieUtilisateur=id_categorieUtilisateur,@activation=activation,@nomuser=nomuser,@motpass=motpass from inserted
		set @requete= @id + ';' + @id_personne + ';' + @id_categorieUtilisateur + ';' + @activation + ';' + @nomuser + ';' + @motpass + '|utilisateur'
		print 'Requete = ' + @requete
		insert into recupQueryUpdate(requete) values(@requete)
	end	
go

--Triggers pour telephone
create trigger recupTelephoneUpdate on telephone after update
as
	declare @id varchar(10),@numero varchar(14),@id_utilisateur varchar(14),@requete varchar(1000)
	begin
		select @id=id from deleted
		select @numero=numero,@id_utilisateur=id_utilisateur from inserted
		set @requete= @id + ';' + @id_utilisateur + ';' + @numero + '|telephone'
		print 'Requete = ' + @requete
		insert into recupQueryUpdate(requete) values(@requete)
	end	
go

--Triggers pour carte
create trigger recupCarteUpdate on carte after update
as
	declare @id varchar(10),@id_personne varchar(10),@datelivraison varchar(20),@requete varchar(1000)
	begin
		select @id=id from deleted
		select @id_personne=id_personne,@datelivraison=datelivraison from inserted
		set @requete= @id + ';' + @id_personne + ';' + @datelivraison + '|carte'
		print 'Requete = ' + @requete
		insert into recupQueryUpdate(requete) values(@requete)
	end	
go

update villeTerritoire set designation='GOMA',superficie=25800 where id=1
update communeChefferieSecteur set id_villeTerritoire=1,designation='GOMA',superficie=12500 where id=1
update quartierLocalite set id_communeChefferieSecteur=1,designation='KATOYI',superficie=2500 where id=1
update avenueVillage set id_quartierLocalite=1,designation='BUKONDE' where id=1

update personne set id_avenueVillage=1,id_pere=null,id_mere=null,numeroNational='NK11',numero='99',nom='NKEMBO',
postnom='NKEMBO',prenom='JOSUE',sexe='M',etativil='CELIBATAIRE',datenaissance='1991-02-11',datedeces=null,
travail=1,nombreEnfant=0,niveauEtude='LICENCIE' where id=1

update personne set id_avenueVillage=1,id_pere=null,id_mere=null,numeroNational='NK11',numero='99',nom='NKEMBO',
postnom=null,prenom=null,sexe='M',etativil='CELIBATAIRE',datenaissance=null,datedeces=null,
travail='1',nombreEnfant=1,niveauEtude='LICENCIE' where id=1

update photo set id_personne=1,photo=null where id=1
update categorieUtilisateur set designation='Administrateur national',groupe='Administrateur' where id=1
update utilisateur set id_personne=1,id_categorieUtilisateur=1,activation=1,nomuser='josam',motpass='jos' where id=1
update telephone set id_utilisateur=1,numero='0813870366' where id=1
update carte set id_personne=1,datelivraison='2013-02-11' where id=1


