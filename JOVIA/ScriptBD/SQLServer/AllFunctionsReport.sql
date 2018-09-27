-------------------ETATS DE SORTIE-------------------
--Fonction qui calcule la Densite de la population et la retourne sous forme d'un nombre decimal a trois chiffre
--apres la vigule et prend 2 parametres dont le nom de la table concernee et un entier 0->Provincial et 1->National
CREATE PROCEDURE fonctionCalculDensitePopulation (@nomTable varchar(50),@entierCategorie integer) 
AS
	BEGIN
		DECLARE @resultCalcul numeric(10,3) 	
		IF (@nomTable='province' AND @entierCategorie=0) --Niveau provincial
			BEGIN
				SET @resultCalcul=(SELECT ((COUNT(personne.id))/SQRT((SELECT parametreProvince.superficie FROM parametreProvince WHERE parametreProvince.designation=(SELECT valuesTemp.designation_province FROM valuesTemp)))) as densitePop
				FROM parametreProvince,personne 
				INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
				INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
				INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
				INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire GROUP BY parametreProvince.designation)
			END
		ELSE IF (@nomTable='province' AND @entierCategorie=1) --Niveau National
			BEGIN
				SET @resultCalcul=(SELECT ((COUNT(personne.id))/SQRT((SELECT province.superficie FROM province WHERE province.designation=(SELECT valuesTemp.designation_province FROM valuesTemp)))) as densitePop
				FROM personne 
				INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
				INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
				INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
				INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
				INNER JOIN province ON province.id=villeTerritoire.id_province
				WHERE province.designation=(SELECT designation_province FROM valuesTemp))
			END
		ELSE IF (@nomTable='villeTerritoire') 
			BEGIN
				SET @resultCalcul=(SELECT ((count(personne.id))/SQRT((SELECT villeTerritoire.superficie FROM villeTerritoire WHERE villeTerritoire.designation=(SELECT valuesTemp.designation_villeTerritoire FROM valuesTemp)))) as densitePop
				FROM personne 
				INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
				INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
				INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
				INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
				WHERE villeTerritoire.designation=(SELECT designation_villeTerritoire FROM valuesTemp))
			END
		ELSE IF (@nomTable='communeChefferieSecteur') 
			BEGIN
				SET @resultCalcul=(SELECT ((COUNT(personne.id))/SQRT((SELECT communeChefferieSecteur.superficie FROM communeChefferieSecteur WHERE communeChefferieSecteur.designation=(SELECT valuesTemp.designation_communeChefferieSecteur FROM valuesTemp)))) as densitePop
				FROM personne 
				INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
				INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
				INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
				INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
				WHERE communeChefferieSecteur.designation=(SELECT designation_communeChefferieSecteur FROM valuesTemp))
			END
		ELSE IF (@nomTable='quartierLocalite') 
			BEGIN
				SET @resultCalcul=(SELECT ((COUNT(personne.id))/SQRT((SELECT quartierLocalite.superficie FROM quartierLocalite WHERE quartierLocalite.designation=(SELECT valuesTemp.designation_quartierLocalite FROM valuesTemp)))) as densitePop
				FROM personne 
				INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
				INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
				INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
				INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
				WHERE quartierLocalite.designation=(SELECT designation_quartierLocalite FROM valuesTemp))
			END
		PRINT 'nomTable = ' + @nomTable + ' et @resultCalcul = ' + CONVERT(varchar(20),@resultCalcul)
		UPDATE valuesTemp SET tableName='' + @nomTable
		RETURN @resultCalcul
	END	

--Fonction qui calcule le taux de natalite (Nombre des naissances vivantes l'an divise par la population moyenne l'an) 
--et la retourne sous forme d'un nombre decimal a trois chiffres apres la vigule et prend 5 parametres un string representant dont le nom de la table 
--concernee, un entier representant  la population au 1er Janvier de l'annee concernee, un entier representant la population au 31 Decembre de l'annee concernee,
--un entier representant l'annee pour laquelle on veut connaitre le taux ainsi qu'un entier representant la niveau National ou Provincial
-- 0->Provincial et 1->National
CREATE PROCEDURE fonctionCalculTauxNatalite (@nomTable text,@popAu1Janvier integer,@popAu31Decembre integer,@anneCours integer,@entierCategorie integer) 
AS
	BEGIN
		DECLARE 
			@resultCalcul numeric(10,3)
			@nombreNaissanceVivantesAnnee integer
		BEGIN	
			UPDATE valuesTemp SET anneeNaissance=@anneCours
			
			IF (@nomTable='province' AND @entierCategorie=0) --Niveau provincial
				BEGIN
					SET @nombreNaissanceVivantesAnnee=(SELECT DISTINCT COUNT(EXTRACT(YEAR FROM personne.datenaissance)) AS nombreNaissanceAnnee FROM parametreProvince,personne 
					INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
					INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
					INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
					INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
					WHERE (EXTRACT(YEAR FROM personne.datenaissance))=(SELECT anneeNaissance FROM valuesTemp)
					AND parametreProvince.designation=(SELECT designation_province FROM valuesTemp) AND personne.datedeces ISNULL GROUP BY parametreProvince.designation)
				END
			ELSE IF (@nomTable='province' AND @entierCategorie=1)--Niveau National
				BEGIN
					SET @nombreNaissanceVivantesAnnee=(SELECT DISTINCT COUNT(EXTRACT(YEAR FROM personne.datenaissance)) AS nombreNaissanceAnnee FROM personne 
					INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
					INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
					INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
					INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
					INNER JOIN province ON province.id=villeTerritoire.id_province
					WHERE (EXTRACT(YEAR FROM personne.datenaissance)=(SELECT anneeNaissance FROM valuesTemp)
					AND province.designation=(SELECT designation_province FROM valuesTemp)) AND personne.datedeces ISNULL);
				END
			ELSE IF (@nomTable='villeTerritoire')
				BEGIN SELECT YEAR(personne.datenaissance) FROM personne
					SET @nombreNaissanceVivantesAnnee=(SELECT DISTINCT COUNT(YEAR(personne.datenaissance)) AS nombreNaissanceAnnee FROM personne  
					INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
					INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
					INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
					INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
					WHERE ((YEAR(personne.datenaissance) FROM personne)=(SELECT anneeNaissance FROM valuesTemp) 
					AND villeTerritoire.designation=(SELECT designation_villeTerritoire FROM valuesTemp)) AND personne.datedeces ISNULL)
				END
			ELSE IF (nomTable='communeChefferieSecteur') 
				BEGIN
					@nombreNaissanceVivantesAnnee=(SELECT DISTINCT COUNT(EXTRACT(YEAR FROM personne.datenaissance)) AS nombreNaissanceAnnee FROM personne 
					INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
					INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
					INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
					INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
					WHERE (EXTRACT(YEAR FROM personne.datenaissance)=(SELECT anneeNaissance FROM valuesTemp)
					AND communeChefferieSecteur.designation=(SELECT designation_communeChefferieSecteur FROM valuesTemp)) AND personne.datedeces ISNULL);
				END
			ELSE IF (nomTable='quartierLocalite')
				BEGIN
					SET @nombreNaissanceVivantesAnnee=(SELECT DISTINCT COUNT(EXTRACT(YEAR FROM personne.datenaissance)) AS nombreNaissanceAnnee FROM personne  
					INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
					INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
					INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
					INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
					WHERE (EXTRACT(YEAR FROM personne.datenaissance)=(SELECT anneeNaissance FROM valuesTemp)
					AND quartierLocalite.designation=(SELECT designation_quartierLocalite FROM valuesTemp)) AND personne.datedeces ISNULL);
				END
			END IF
			UPDATE valuesTemp SET tableName=@nomTable;
			@resultCalcul=nombreNaissanceVivantesAnnee/((@popAu1Janvier + @popAu31Decembre)/2);
			PRINT 'nombreNaissanceVivantesAnnee = ' +  CONVERT(varchar(30),@nombreNaissanceVivantesAnnee) + ' et @resultCalcul = ' + CONVERT(varchar(20),@resultCalcul)
			RETURN resultCalcul;
	END	
	$fonctionCalculTauxNatalite$ LANGUAGE plpgsql;


--Suppression fonctions
drop procedure fonctionCalculDensitePopulation
drop procedure fonctionCalculTauxNatalite


--Test densite Population Cote provincial
EXEC fonctionCalculDensitePopulation 'province',0
EXEC fonctionCalculDensitePopulation 'villeTerritoire',0
EXEC fonctionCalculDensitePopulation 'communeChefferieSecteur',0
EXEC fonctionCalculDensitePopulation 'quartierLocalite',0

--Test densite Population Cote National 
EXEC fonctionCalculDensitePopulation 'province',1
EXEC fonctionCalculDensitePopulation 'villeTerritoire',1
EXEC fonctionCalculDensitePopulation 'communeChefferieSecteur',1
EXEC fonctionCalculDensitePopulation'quartierLocalite',1

------------------
--Test taux de natalite Cote provincial
EXEC fonctionCalculTauxNatalite('province',2,3,2010,0);
EXEC fonctionCalculTauxNatalite('villeTerritoire',2,3,2010,0);
EXEC fonctionCalculTauxNatalite('communeChefferieSecteur',2,3,2010,0);
EXEC fonctionCalculTauxNatalite('quartierLocalite',2,3,2010,0);

--Test taux de natalite Cote National
EXEC fonctionCalculTauxNatalite('province',2,3,2010,1);
EXEC fonctionCalculTauxNatalite('villeTerritoire',2,3,2010,1);
EXEC fonctionCalculTauxNatalite('communeChefferieSecteur',2,3,2010,1);
EXEC fonctionCalculTauxNatalite('quartierLocalite',2,3,2010,1);
