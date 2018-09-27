-------------------ETATS DE SORTIE-------------------
--Fonction qui calcule la Densite de la population (Nombre des habitants par Km carre) et la retourne sous forme d'un nombre decimal a trois chiffre
--apres la vigule et prend 2 parametre dont le nom de la table concernee et un entier 0->Provincial et 1->National
CREATE FUNCTION fonctionCalculDensitePopulation (nomTable text,entierCategorie integer) RETURNS numeric(10,3) AS $fonctionCalculDensitePopulation$
	DECLARE 
		resultCalcul numeric(10,3);
	BEGIN	
		IF (nomTable='province' AND entierCategorie=0) THEN--Niveau provincial
			resultCalcul=(SELECT ((count(personne.id))/POW((SELECT parametreProvince.superficie FROM parametreProvince WHERE parametreProvince.designation=(SELECT valuesTemp.designation_province FROM valuesTemp)),1)) as densitePop
			FROM parametreProvince,personne 
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire GROUP BY parametreProvince.designation);
		ELSEIF (nomTable='province' AND entierCategorie=1) THEN--Niveau National
			resultCalcul=(SELECT ((COUNT(personne.id))/POW((SELECT province.superficie FROM province WHERE province.designation=(SELECT valuesTemp.designation_province FROM valuesTemp)),1)) as densitePop
			FROM personne 
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			INNER JOIN province ON province.id=villeTerritoire.id_province
			WHERE province.designation=(SELECT designation_province FROM valuesTemp));
		ELSEIF (nomTable='' AND entierCategorie=1) THEN--Niveau National mais pour tous le pays
			resultCalcul=(SELECT ((COUNT(personne.id))/POW((SELECT province.superficie FROM province WHERE province.designation=(SELECT valuesTemp.designation_province FROM valuesTemp)),1)) as densitePop
			FROM personne 
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			INNER JOIN province ON province.id=villeTerritoire.id_province);
		ELSEIF (nomTable='villeTerritoire') THEN
			resultCalcul=(SELECT ((COUNT(personne.id))/POW((SELECT villeTerritoire.superficie FROM villeTerritoire WHERE villeTerritoire.designation=(SELECT valuesTemp.designation_villeTerritoire FROM valuesTemp)),1)) as densitePop
			FROM personne 
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			WHERE villeTerritoire.designation=(SELECT designation_villeTerritoire FROM valuesTemp));
		ELSEIF (nomTable='communeChefferieSecteur') THEN
			resultCalcul=(SELECT ((COUNT(personne.id))/POW((SELECT communeChefferieSecteur.superficie FROM communeChefferieSecteur WHERE communeChefferieSecteur.designation=(SELECT valuesTemp.designation_communeChefferieSecteur FROM valuesTemp)),1)) as densitePop
			FROM personne 
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			WHERE communeChefferieSecteur.designation=(SELECT designation_communeChefferieSecteur FROM valuesTemp));
			RAISE INFO 'nomTable = ''%'' et resultCalcul = %',nomTable,resultCalcul;
		ELSEIF (nomTable='quartierLocalite') THEN
			resultCalcul=(SELECT ((COUNT(personne.id))/POW((SELECT quartierLocalite.superficie FROM quartierLocalite WHERE quartierLocalite.designation=(SELECT valuesTemp.designation_quartierLocalite FROM valuesTemp)),1)) as densitePop
			FROM personne 
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			WHERE quartierLocalite.designation=(SELECT designation_quartierLocalite FROM valuesTemp));
		END IF;
		RAISE INFO 'nomTable = ''%'' et resultCalcul = %',nomTable,resultCalcul;
		UPDATE valuesTemp SET tableName='' || nomTable;
		RETURN resultCalcul;
	END	
	$fonctionCalculDensitePopulation$ LANGUAGE plpgsql;

--Fonction qui calcule le taux de natalite (Nombre des naissances vivantes l'an divise par la population moyenne l'an) 
--et la retourne sous forme d'un nombre decimal a trois chiffres apres la vigule et prend 5 parametres un string representant dont le nom de la table 
--concernee, un entier representant  la population au 1er Janvier de l'annee concernee, un entier representant la population au 31 Decembre de l'annee concernee,
--un entier representant l'annee pour laquelle on veut connaitre le taux ainsi qu'un entier representant la niveau National ou Provincial
-- 0->Provincial et 1->National
CREATE FUNCTION fonctionCalculTauxNatalite (nomTable text,popAu1Janvier real,popAu31Decembre real,anneCours real,entierCategorie real) RETURNS numeric(10,3) AS $fonctionCalculTauxNatalite$
	DECLARE 
		resultCalcul numeric(10,3);
		nombreNaissanceVivantesAnnee numeric(10,3);
	BEGIN	
		UPDATE valuesTemp SET anneeNaissance=anneCours;
		
		IF (nomTable='province' AND entierCategorie=0) THEN--Niveau provincial
			nombreNaissanceVivantesAnnee=(SELECT DISTINCT COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS nombreNaissanceAnnee FROM parametreProvince,personne 
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			WHERE (EXTRACT(YEAR FROM personne.dateNaissance))=(SELECT anneeNaissance FROM valuesTemp)
			AND parametreProvince.designation=(SELECT designation_province FROM valuesTemp) AND personne.dateDeces ISNULL GROUP BY parametreProvince.designation);
		ELSEIF (nomTable='province' AND entierCategorie=1) THEN--Niveau National
			nombreNaissanceVivantesAnnee=(SELECT DISTINCT COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS nombreNaissanceAnnee FROM personne 
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			INNER JOIN province ON province.id=villeTerritoire.id_province
			WHERE (EXTRACT(YEAR FROM personne.dateNaissance)=(SELECT anneeNaissance FROM valuesTemp)
			AND province.designation=(SELECT designation_province FROM valuesTemp)) AND personne.dateDeces ISNULL);
		ELSEIF (nomTable='' AND entierCategorie=1) THEN--Niveau National mais tous le pays
			nombreNaissanceVivantesAnnee=(SELECT DISTINCT COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS nombreNaissanceAnnee FROM personne 
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			INNER JOIN province ON province.id=villeTerritoire.id_province
			WHERE (EXTRACT(YEAR FROM personne.dateNaissance)=(SELECT anneeNaissance FROM valuesTemp)AND personne.dateDeces ISNULL));
		ELSEIF (nomTable='villeTerritoire') THEN
			nombreNaissanceVivantesAnnee=(SELECT DISTINCT COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS nombreNaissanceAnnee FROM personne  
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			WHERE (EXTRACT(YEAR FROM personne.dateNaissance)=(SELECT anneeNaissance FROM valuesTemp) 
			AND villeTerritoire.designation=(SELECT designation_villeTerritoire FROM valuesTemp)) AND personne.dateDeces ISNULL) ;
		ELSEIF (nomTable='communeChefferieSecteur') THEN
			nombreNaissanceVivantesAnnee=(SELECT DISTINCT COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS nombreNaissanceAnnee FROM personne 
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			WHERE (EXTRACT(YEAR FROM personne.dateNaissance)=(SELECT anneeNaissance FROM valuesTemp)
			AND communeChefferieSecteur.designation=(SELECT designation_communeChefferieSecteur FROM valuesTemp)) AND personne.dateDeces ISNULL);
		ELSEIF (nomTable='quartierLocalite') THEN
			nombreNaissanceVivantesAnnee=(SELECT DISTINCT COUNT(EXTRACT(YEAR FROM personne.dateNaissance)) AS nombreNaissanceAnnee FROM personne  
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			WHERE (EXTRACT(YEAR FROM personne.dateNaissance)=(SELECT anneeNaissance FROM valuesTemp)
			AND quartierLocalite.designation=(SELECT designation_quartierLocalite FROM valuesTemp)) AND personne.dateDeces ISNULL);
		END IF;
		UPDATE valuesTemp SET tableName='' || nomTable;
		resultCalcul=nombreNaissanceVivantesAnnee/((popAu1Janvier + popAu31Decembre)/2);
		RAISE INFO 'nombreNaissanceVivantesAnnee = % et resultCalcul = %',nombreNaissanceVivantesAnnee,resultCalcul;
		RETURN resultCalcul;
	END	
	$fonctionCalculTauxNatalite$ LANGUAGE plpgsql;

--Fonction qui calcule le taux de mortalite (Nombre des deces l'an divise par la population moyenne l'an) 
--et la retourne sous forme d'un nombre decimal a trois chiffres apres la vigule et prend 5 parametres un string representant dont le nom de la table 
--concernee, un entier representant  la population au 1er Janvier de l'annee concernee, un entier representant la population au 31 Decembre de l'annee concernee,
--un entier representant l'annee pour laquelle on veut connaitre le taux ainsi qu'un entier representant la niveau National ou Provincial
-- 0->Provincial et 1->National
CREATE FUNCTION fonctionCalculTauxMortalite (nomTable text,popAu1Janvier real,popAu31Decembre real,anneCours real,entierCategorie real) RETURNS numeric(10,3) AS $fonctionCalculTauxMortalite$
	DECLARE 
		resultCalcul numeric(10,3);
		nombreDecesAnnee numeric(10,3);
	BEGIN	
		UPDATE valuesTemp SET anneeNaissance=anneCours;

		IF (nomTable='province' AND entierCategorie=0) THEN--Niveau provincial
			nombreDecesAnnee=(SELECT DISTINCT COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS nombreDecesAnnee FROM parametreProvince,personne 
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			WHERE (EXTRACT(YEAR FROM personne.dateDeces))=(SELECT anneeDeces FROM valuesTemp)
			AND parametreProvince.designation=(SELECT designation_province FROM valuesTemp) AND personne.dateDeces IS NOT NULL GROUP BY parametreProvince.designation);
		ELSEIF (nomTable='province' AND entierCategorie=1) THEN--Niveau National
			nombreDecesAnnee=(SELECT DISTINCT COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS nombreDecesAnnee FROM personne 
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			INNER JOIN province ON province.id=villeTerritoire.id_province
			WHERE (EXTRACT(YEAR FROM personne.dateDeces)=(SELECT anneeDeces FROM valuesTemp)
			AND province.designation=(SELECT designation_province FROM valuesTemp)) AND personne.dateDeces IS NOT NULL);
		ELSEIF (nomTable='' AND entierCategorie=1) THEN--Niveau National mais pour toute la population du pays
			nombreDecesAnnee=(SELECT DISTINCT COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS nombreDecesAnnee FROM personne 
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			INNER JOIN province ON province.id=villeTerritoire.id_province
			WHERE (EXTRACT(YEAR FROM personne.dateDeces)=(SELECT anneeDeces FROM valuesTemp) AND personne.dateDeces IS NOT NULL));
		ELSEIF (nomTable='villeTerritoire') THEN
			nombreDecesAnnee=(SELECT DISTINCT COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS nombreDecesAnnee FROM personne  
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			WHERE (EXTRACT(YEAR FROM personne.dateDeces)=(SELECT anneeDeces FROM valuesTemp) 
			AND villeTerritoire.designation=(SELECT designation_villeTerritoire FROM valuesTemp)) AND personne.dateDeces IS NOT NULL);
		ELSEIF (nomTable='communeChefferieSecteur') THEN
			nombreDecesAnnee=(SELECT DISTINCT COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS nombreDecesAnnee FROM personne 
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			WHERE (EXTRACT(YEAR FROM personne.dateDeces)=(SELECT anneeDeces FROM valuesTemp)
			AND communeChefferieSecteur.designation=(SELECT designation_communeChefferieSecteur FROM valuesTemp)) AND personne.dateDeces IS NOT NULL);
		ELSEIF (nomTable='quartierLocalite') THEN
			nombreDecesAnnee=(SELECT DISTINCT COUNT(EXTRACT(YEAR FROM personne.dateDeces)) AS nombreDecesAnnee FROM personne  
			INNER JOIN avenueVillage ON avenueVillage.id=personne.id_avenueVillage 
			INNER JOIN quartierLocalite ON quartierLocalite.id=avenueVillage.id_quartierLocalite
			INNER JOIN communeChefferieSecteur ON communeChefferieSecteur.id=quartierLocalite.id_communeChefferieSecteur
			INNER JOIN villeTerritoire ON villeTerritoire.id=communeChefferieSecteur.id_villeTerritoire
			WHERE (EXTRACT(YEAR FROM personne.dateDeces)=(SELECT anneeDeces FROM valuesTemp)
			AND quartierLocalite.designation=(SELECT designation_quartierLocalite FROM valuesTemp)) AND personne.dateDeces IS NOT NULL);
		END IF;
		UPDATE valuesTemp SET tableName='' || nomTable;
		resultCalcul=nombreDecesAnnee/((popAu1Janvier + popAu31Decembre)/2);
		RAISE INFO 'nombreDecesAnnee = % et resultCalcul = %',nombreDecesAnnee,resultCalcul;
		RETURN resultCalcul;
	END	
	$fonctionCalculTauxMortalite$ LANGUAGE plpgsql;

--Fonction qui calcule le taux de croissance ((Y-X)((racine caree PY/PX)-1)).100 
--et la retourne sous forme d'un nombre decimal a trois chiffres apres la vigule et prend 5 parametres un entier representant dont le nom de la table 
--l'année de fin Y, un entier representant  l'année de debut X , un entier representant la population à l'année Y soit PY, un entier representant 
--la population à l'année X soit PX et un string représentant la table concernee ou le type de taux de croissqnce (Si c'est une commune ou etc.)
CREATE FUNCTION fonctionCalculTauxCroissance (nomTable text,anneeFin real,anneeDebut real,populationFin real,populationDebut real) RETURNS numeric(10,2) AS $fonctionCalculTauxCroissance$
	DECLARE 
		resultCalcul numeric(10,2);
	BEGIN	
		UPDATE valuesTemp SET tableName='' || nomTable;
		IF (nomTable='province' OR nomTable='villeTerritoire' OR nomTable='communeChefferieSecteur' OR nomTable='quartierLocalite' OR nomTable='') THEN
			IF(SQRT(((populationFin/populationDebut)))<0) THEN
				resultCalcul=0;
			ELSE
				resultCalcul=((anneeFin-anneeDebut)*(SQRT(((populationFin/populationDebut)))-1))*100;--((anneeFin-anneeDebut)*(SQRT(((populationFin/populationDebut)))-1))*100;--((anneeFin-anneeDebut)*(SQRT(((populationFin/populationDebut)))-1))*100;
			END IF;
		END IF;		
		RAISE INFO 'resultCalculTaux de croissance = %',resultCalcul;
		RETURN resultCalcul;
	END	
	$fonctionCalculTauxCroissance$ LANGUAGE plpgsql;
		
--Suppression fonctions
drop function fonctionCalculDensitePopulation(nomTable text,entierCategorie integer);
drop function fonctionCalculTauxNatalite(nomTable text,popAu1Janvier integer,popAu31Decembre integer,anneCours integer,entierCategorie integer);
drop function fonctionCalculTauxMortalite(nomTable text,popAu1Janvier integer,popAu31Decembre integer,anneCours integer,entierCategorie integer);
drop function fonctionCalculTauxCroissance(nomTable text,anneeFin real,anneeDebut real,populationFin real,populationDebut real);


--Test densite Population Cote provincial
SELECT fonctionCalculDensitePopulation('province',0);
SELECT fonctionCalculDensitePopulation('villeTerritoire',0);
SELECT fonctionCalculDensitePopulation('communeChefferieSecteur',0);
SELECT fonctionCalculDensitePopulation('quartierLocalite',0);

--Test densite Population Cote National
SELECT fonctionCalculDensitePopulation('province',1);
SELECT fonctionCalculDensitePopulation('villeTerritoire',1);
SELECT fonctionCalculDensitePopulation('communeChefferieSecteur',1);
SELECT fonctionCalculDensitePopulation('quartierLocalite',1);
SELECT fonctionCalculDensitePopulation('',1);

------------------
--Test taux de natalite Cote provincial
SELECT fonctionCalculTauxNatalite('province',2,3,2010,0);
SELECT fonctionCalculTauxNatalite('villeTerritoire',2,3,2010,0);
SELECT fonctionCalculTauxNatalite('communeChefferieSecteur',2,3,2010,0);
SELECT fonctionCalculTauxNatalite('quartierLocalite',2,3,2010,0);

--Test taux de natalite Cote National
SELECT fonctionCalculTauxNatalite('province',2,3,2010,1);
SELECT fonctionCalculTauxNatalite('villeTerritoire',2,3,2010,1);
SELECT fonctionCalculTauxNatalite('communeChefferieSecteur',2,3,2010,1);
SELECT fonctionCalculTauxNatalite('quartierLocalite',2,3,2010,1);
SELECT fonctionCalculTauxNatalite('',2,3,2010,1);

------------------
--Test taux de mortalite Cote provincial
SELECT fonctionCalculTauxMortalite('province',2,3,2011,0);
SELECT fonctionCalculTauxMortalite('villeTerritoire',2,3,2011,0);
SELECT fonctionCalculTauxMortalite('communeChefferieSecteur',2,3,2011,0);
SELECT fonctionCalculTauxMortalite('quartierLocalite',2,3,2011,0);

--Test taux de mortalite Cote National
SELECT fonctionCalculTauxMortalite('province',2,3,2011,1);
SELECT fonctionCalculTauxMortalite('villeTerritoire',2,3,2011,1);
SELECT fonctionCalculTauxMortalite('communeChefferieSecteur',2,3,2011,1);
SELECT fonctionCalculTauxMortalite('quartierLocalite',2,3,2011,1);
SELECT fonctionCalculTauxMortalite('',2,3,2011,1);

------------------
--Test taux de croissance
SELECT fonctionCalculTauxCroissance('province',2010,2007,4,2);
SELECT fonctionCalculTauxCroissance('villeTerritoire',2010,2007,4,2);
SELECT fonctionCalculTauxCroissance('communeChefferieSecteur',2010,2007,1800000,1500000);
SELECT fonctionCalculTauxCroissance('quartierLocalite',2010,2007,3,2);
