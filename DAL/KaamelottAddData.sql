/*
Clients : 
				  -Arthur Pendragon
				  -Lancelot du Lac
				  -Père Blaise
				  -Dame du Lac
				  -Merlin Le druide
				  -Léodagan Carmélide
				  -Yvain le chevalier au Lion
				  -Perceval de Galles
				  -Karadoc de Vannes		  
				  -Bohort de Gaunes
				  -Elias de Kelliwic'h			  
*/
INSERT INTO CLIENT (LOCID, CLINOM, CLIPRENOM, CLITELEPHONE, CLIMAIL, CLIADRESSE, CLIPASSWORD)
VALUES					  (	 '', '', '','','','' 	);
INSERT INTO CLIENT (LOCID, CLINOM, CLIPRENOM, CLITELEPHONE, CLIMAIL, CLIADRESSE, CLIPASSWORD)
VALUES					  (2, 'Pendragon', 'Arthur', '078 485 42 00' , 'sanglier.cornouaille@dies.irae'	, 'Route du throne 1', '4200');
INSERT INTO CLIENT (LOCID, CLINOM, CLIPRENOM, CLITELEPHONE, CLIMAIL, CLIADRESSE, CLIPASSWORD)
VALUES					  (2, 'Du Lac', 'Lancelot', '078 485 19 22','chevalier.errant@dies.irae','Rue du bosquet perdu 4','1922' 	);
INSERT INTO CLIENT (LOCID, CLINOM, CLIPRENOM, CLITELEPHONE, CLIMAIL, CLIADRESSE, CLIPASSWORD)
VALUES					  (2, 'Blaise', 'Père', '078 485 66 69','quinte.juste@dies.irae','Chapelle du chateau 3','6669' 	);
INSERT INTO CLIENT (LOCID, CLINOM, CLIPRENOM, CLITELEPHONE, CLIMAIL, CLIADRESSE, CLIPASSWORD)
VALUES					  (2, 'Du Lac', 'Dame', '078 485 21 19','invisible.aux.mortels@dies.irae','Chemin astral 2','2119' 	);
INSERT INTO CLIENT (LOCID, CLINOM, CLIPRENOM, CLITELEPHONE, CLIMAIL, CLIADRESSE, CLIPASSWORD)
VALUES					  (3, 'Druide', 'Merlin', '078 480 25 00','coco.asticot@dies.irae','Rue du bosquet 6','2500' 	);
INSERT INTO CLIENT (LOCID, CLINOM, CLIPRENOM, CLITELEPHONE, CLIMAIL, CLIADRESSE, CLIPASSWORD)
VALUES					  (5, 'Carmélide', 'Léodagan', '078 550 30 24','chevalier.sanguinaire@dies.irae','Route du fort 18','3024' 	);
INSERT INTO CLIENT (LOCID, CLINOM, CLIPRENOM, CLITELEPHONE, CLIMAIL, CLIADRESSE, CLIPASSWORD)
VALUES					  (5, 'Chevalier au Lion', 'Yvain', '078 550 78 43','petit.pedestre@dies.irae','Chemin du marais 24','7843' 	);
INSERT INTO CLIENT (LOCID, CLINOM, CLIPRENOM, CLITELEPHONE, CLIMAIL, CLIADRESSE, CLIPASSWORD)
VALUES					  (6, 'Le Gallois', 'Perceval', '078 370 65 97','pas.faux@dies.irae','Chemin du vieux 14','6597' 	);
INSERT INTO CLIENT (LOCID, CLINOM, CLIPRENOM, CLITELEPHONE, CLIMAIL, CLIADRESSE, CLIPASSWORD)
VALUES					  (8, 'De Vannes', 'Karadoc', '078 125 08 74','semi.croustillant@dies.irae','Route de la file indienne 7','0874' 	);
INSERT INTO CLIENT (LOCID, CLINOM, CLIPRENOM, CLITELEPHONE, CLIMAIL, CLIADRESSE, CLIPASSWORD)
VALUES					  (2, 'De Gaunes', 'Bohort', '078 485 55 91','mecreants@dieas.irae','Route du throne 4','5591' 	);
INSERT INTO CLIENT (LOCID, CLINOM, CLIPRENOM, CLITELEPHONE, CLIMAIL, CLIADRESSE, CLIPASSWORD)
VALUES					  (3, 'De Kelliwich', 'Elias', '078 480 66 60','enchanteur.du.nord@dies.irae','Rue des boulots 55','6660' 	);



/*
	Localité : 
				  -Kaamelott (Logres)
				  -Rome
				  -Orcanie
				  -Carmélide
				  -Pays de Galles
				  -Armorique
				  -Vannes
				  -Aquitaine
 */
INSERT INTO LOCALITE (LOCNOM,     LOCNPA)
VALUES					      ('', 	''	);
INSERT INTO LOCALITE (LOCNOM,     LOCNPA)
VALUES					      ('Kaamelott', 	'485'	);
INSERT INTO LOCALITE (LOCNOM,     LOCNPA)
VALUES					      ('Logres', 		'480'	);
INSERT INTO LOCALITE (LOCNOM,     LOCNPA)
VALUES					      ('Orcanie', 		'780'	);
INSERT INTO LOCALITE (LOCNOM,     LOCNPA)
VALUES					      ('Carmélide', 	'550'	);
INSERT INTO LOCALITE (LOCNOM,     LOCNPA)
VALUES					      ('Pays de Galles', 	'370'	);
INSERT INTO LOCALITE (LOCNOM,     LOCNPA)
VALUES					      ('Armorique', 	'1200'	);
INSERT INTO LOCALITE (LOCNOM,     LOCNPA)
VALUES					      ('Vannes', 		'1250'	);
INSERT INTO LOCALITE (LOCNOM,     LOCNPA)
VALUES					      ('Aquitaine', 	'1400'	);
INSERT INTO LOCALITE (LOCNOM,     LOCNPA)
VALUES					      ('Rome',			'117'	);

/*
Staff :	 
				  -Venec le Bandit
				  -Kadoc
				  -Aconia
				  -Sven le Viking
				  -Demetra
				  -Loth d'Orcanie
*/
INSERT INTO STAFF (STANOM, STAPRENOM, STATELEPHONE, STAMAIL, STAPASSWORD)
VALUES				    ('','','','','');
INSERT INTO STAFF (STANOM, STAPRENOM, STATELEPHONE, STAMAIL, STAPASSWORD)
VALUES				    ('Le Bandit','Venec','078 485 33 37','esclaves.pascher@dies.irae','3337'	);
INSERT INTO STAFF (STANOM, STAPRENOM, STATELEPHONE, STAMAIL, STAPASSWORD)
VALUES				    ('Le Caillou','Kadoc','078 125 09 96','la.pouette@dies.irae','0996');
INSERT INTO STAFF (STANOM, STAPRENOM, STATELEPHONE, STAMAIL, STAPASSWORD)
VALUES				    ('De la Villa','Aconia','078 117 38 44','fidele.romaine@dies.irae','3844');
INSERT INTO STAFF (STANOM, STAPRENOM, STATELEPHONE, STAMAIL, STAPASSWORD)
VALUES				    ('Le Viking','Sven','078 780 87 65','valhalla.awayts@dies.irae','8765');
INSERT INTO STAFF (STANOM, STAPRENOM, STATELEPHONE, STAMAIL, STAPASSWORD)
VALUES				    ('Du Château','Demetra','078 485 29 64','favorite@dies.irae','2964');
INSERT INTO STAFF (STANOM, STAPRENOM, STATELEPHONE, STAMAIL, STAPASSWORD)
VALUES				    ('Orcanie','Loth','078 780 77 54','traitre@dies.irae','7754');

-- IMPOSSIBLE A SUPPRIMER PERCEVAL DU STAFF
DELETE FROM STAFF  WHERE STAID = 1;

/*
Restaurant : 
				  -La Taverne
				  -Donjon de Fearmac
				  -Couleurs Burgondes
				  -Ketchatar's Pub
				  -Tintingel's Cider
				  -Rôtisserie Excalibur
*/
INSERT INTO RESTAURANT (LOCID, RESNOM, RESADRESSE	)
VALUES								('');
INSERT INTO RESTAURANT (LOCID, RESNOM, RESADRESSE	)
VALUES								(3,'La Taverne','Chemin du château 45'	);
INSERT INTO RESTAURANT (LOCID, RESNOM, RESADRESSE	)
VALUES								(2,'Donjon de Fearmac','Rue du fumier 7'	);
INSERT INTO RESTAURANT (LOCID, RESNOM, RESADRESSE	)
VALUES								(7,'Couleurs Burgondes','Rue de la plage 12'	);
INSERT INTO RESTAURANT (LOCID, RESNOM, RESADRESSE	)
VALUES								(6,'Ketchatars Pub','Chemin des irlandais 34'	);
INSERT INTO RESTAURANT (LOCID, RESNOM, RESADRESSE	)
VALUES								(4,'Tintingels Cider','Rue de la belle-mêre 56'	);
INSERT INTO RESTAURANT (LOCID, RESNOM, RESADRESSE	)
VALUES								(3,'Rôtisserie Excalibur','Chemin du rocher 3'	);


/*
Plat : 
				  -Le croque monsieur
				  -Coupe du Graal
				  -Pierre incandescente
				  -Picade de roquette
				  -Jarret de porc rôti au miel
				  -Sang du christ

				  -Fumier du crépuscule 			
				  -Soupe aux orties
				  -Filets de harengs marinés
				  -Côtelettes de proc, poires et panais au four
				  -Clafoutis rustique aux fruits rouges
				  -Raclette 

				  -Grand aïoli de morue
				  -Cretonnée de pois ou de fèves nouvelles
				  -Maqueraux fumés et pommes de terre à la moutarde
				  -Pain fouace
				  -Tarte aux pommes à l'ancienne
				  -Hydromel

				  -Gaufres au fromage
				  -Potiron au Gratin
				  -Venaison de sanglier
				  -Porridge aux fruits
				  -Hypocras au vin blanc
				  -Hypocras au vin rouge
				  
				  -Chausson de pommes / figues / raisins
				  -Poires au sirop parfumées de canelle et de gingembre
				  -Compote de pommes aux amandes
				  -Rissoles pommes, figues, raisins, noix, épices
				  -Cidre brut des plaines
				  -Cidre des îles

				  -Porc aigre-doux au gingembre
				  -Poulet sauté à la corianre et au cumin
				  -Poulet grillé au lait d'amande
				  -Fondue Vacherin
				  -Fondue Gruyère
				  -Fondue mixte à la noix de muscade				  
*/
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   ('');

/*  Restaurant : La Taverne
				  -Le croque monsieur
				  -Coupe du Graal
				  -Pierre incandescente
				  -Picade de roquette
				  -Jarret de porc rôti au miel
				  -Sang du christ
*/
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX,PLATDESCRIPTION )
VALUES				   (2,'Le croque monsieur', 4.50, 'Plat national du royaume de Logres.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION)
VALUES				   (2, 'Picard de roquette', 7.00, 'Fameuse salade du royaume de Logres, préparé avec amour du tavernier.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION)
VALUES				   (2, 'Pierre incadesscente', 18.00, 'La légende raconte que ce serait le Graal. Viande cuite & servie sur une pierre incadescente.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION)
VALUES				   (2, 'Jarret de porc rôti au miel', 24.00, 'Excellent choix pour les grands faims, accompagné par un verre de Graal pour digerér le tout.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION)
VALUES				   (2,'Coupe du Graal', 6.00,'Un verre de plus pour achever la quête ! ');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (2,'Sang du christ', 5.00, 'Un pinot noir, les chevaliers de Kaamelott le recomandent ardement ! ');

/*Restaurant : Donjon de Fearmac
				  -Fumier du crépuscule 			
				  -Soupe aux orties
				  -Filets de harengs marinés
				  -Côtelettes de proc, poires et panais au four
				  -Clafoutis rustique aux fruits rouges
				  -Raclette 
*/
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (3,'Fumier du crépuscule', 14.00, 'Plat surprise du jour ! Reveil en pleine nuit garantie !');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (3,'Soupe aux orties', 9.00, 'Chez Fearmac, on aime les produits locaux. Et encore plus ceux qui poussent dans les prisons du château !');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (3,'Filet de harengs marinés', 16.00, 'Spécialité de la prison. Poisson frais du jour, donne une panache incroyable à la voiy pour crier àplein poumons en pleine nuit.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (3,'Côtelettes de porc aux poires et panais au four', 23.00, 'Un plat succulent, capable de faire patienter au moins 22 ans un prisonnier avant de pousser une gueulée. ' );
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (3,'Clafoutis aux fruits rouges', 4.00, 'Une PART de clafoutis préparé avec amour par dame Séli. Peut aussi servir à réparer les murs du château.' );
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (3,'Raclette au feu de bois', 13.00, 'Chez Fearmac, on sait préparer le meilleur plat du royaume !');

/*Restaurant : Couleurs Burgondes
				  -Grand aïoli de morue
				  -Tarte aux pommes à l'ancienne
				  -Maqueraux fumés et pommes de terre à la moutarde
				  -Pain fouace
*/
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (4,'Grand aïoli de morue',23.00,'Plat traditionnel des burgondes, très epicé ! ');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (4,'Maqueraux fumés et pommes de terre à la moutarde', 18.00, 'Poisson préparé au thym et romarin pour epicer la chair, couvert par des rondelles de citron.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (4,'Pain fouace',3.00,'Pain cuit au four, accompagné de sauce Tzaziki.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (4,'Tarte aux pommes à lancienne', 4.00,'Tarte préparé avec les pommes gala de Tintingel');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (4,'Breuvage de jouvence', 6.00, 'Boisson tonfiant le corps et puissant aphrodisiaque.');

/* Restaurant : Ketchatars Pub
				  -Potiron au Gratin
				  -Venaison de sanglier
				  -Hypocras au vin blanc
				  -Hypocras au vin rouge
				  -Gaufres au fromage
				  -Porridge aux fruits
*/
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (5,'Potiron au gratin', 24.00, 'Spécialité irlandaise, a cosommer sans modération.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (5,'Venaisons de sanglier', 28.00,'Sanglier de la chasse, respectueux des traditions irlandaises.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (5,'Hypocrasse au vin blanc', 3.00,'Breuvage à base de vin fortement sucré au miel, épices royales, fermenté et filtré aux mains de maitres irlandais.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (5,'Hypocrasse au vin rouge',3.30,'Breuvage à base de vin fortement sucré au miel, épices royales, fermenté et filtré aux mains de maitres irlandais.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (5,'Gaufres au fromage',4.00,'Excellent accompagnant pour adoucir le gôut sucrés de nos breuvages.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (5,'Porridge aux fruits de saison', 2.00, 'Porridge au lait et à la canelle, accompagné par des fruits de saison et arrosée au sirop');

/* Restaurant : Tintingels Cider
				  -Chausson de pommes / figues / raisins
				  -Poires au sirop parfumées de canelle et de gingembre
				  -Compote de pommes aux amandes
				  -Rissoles pommes, figues, raisins, noix, épices
				  -Cidre brut des plaines
				  -Cidre des îles
*/
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (6,'Chausson aux pommes, figues et raisins', 6.00,'Excellent choix pour un déjeuner.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (6,'Poires au sirop', 4.10,'Poires au sirop parfumées de canelle et de gingembre.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (6,'Compote de pommes aux amandes',6.00,'Plat préparé avec une technique secrète de grand-mère.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (6,'Rissoles au hachis et champignons', 7.00, 'Préparé selon la recette trouvé au fond du tresor caché au nord du continent.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (6,'Cidre brut des plaines', 3.00,'Cidre préparé avec les fameuses pommes de tintingel.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (6,'Cidre brut des îles', 3.60,'Cidre préparé avec les pommes des îles Storror.');

/* Restaurant : Rôtisserie Excalibur
				  -Porc aigre-doux au gingembre
				  -Poulet sauté à la corianre et au cumin
				  -Poulet grillé au lait d'amande
				  -Fondue Vacherin
				  -Fondue Gruyère
				  -Fondue mixte à la noix de muscade		
*/
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (7,'Porc aigre-doux au gingembre', 17.00, 'Porc rôti à la broche, servi dans un pain burgonde.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (7,'Poulet sauté au coriandre et au cumin', 19.00,'Poulet très épicé pour les faims de loup !');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (7,'Poulet grillé au lait d''amandes', 18.00, 'Poulet tendre, une recette copieuse mais efficace.');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (7,'Fondue Vacherin', 25.00,'Plat exotique venant d''Helvetie');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (7,'Fondue Gruyère', 25.00,'Plat exotique venant d''Helvetie');
INSERT INTO PLAT (RESID, PLATNOM, PLATPRIX, PLATDESCRIPTION )
VALUES				   (7,'Fondue mixte à la noix de muscade',22.00, 'Fondue moité-moité parrfait pour les indécis !');


/* 
	Clients : 
				  -Arthur Pendragon
				  -Léodagan Carmélide
				  -Perceval de Galles
				  -Karadoc de Vannes
				  -Lancelot du Lac
				  -Bohort de Gaunes
				  -Yvain le chevalier au Lion
				  -Père Blaise
				  -Merlin Le druide
				  -Elias de Kelliwic'h
				  -Dame du Lac
				
	Staff :	 
				  -Venec le Bandit
				  -Kadoc
				  -Aconia
				  -Sven le Viking
				  -Demetra
				  -Loth d'Orcanie

	Restaurant : 
				  -La Taverne
				  -Donjon de Fearmac
				  -Couleurs Burgondes
				  -Ketchatar's Pub
				  -Tintingel's Cider
				  -Rôtisserie Excalibur
				  -

	Localité : 
				  -Kaamelott (Logres)
				  -Rome
				  -Orcanie
				  -Carmélide
				  -Pays de Galles
				  -Armorique
				  -Vannes
				  -Aquitaine

	Plat : 
				  -Le croque monsieur
				  -Coupe du Graal
				  -Pierre incandescente
				  -Picade de roquette
				  -Fumier du crépuscule 
				  -Jarret de porc rôti au miel
							
				  -Soupe aux orties
				  -Filets de harengs marinés
				  -Côtelettes de proc, poires et panais au four
				  -Clafoutis rustique aux fruits rouges
				  -Grand aïoli de morue
				  -Tarte aux pommes à l'ancienne
				  -Maqueraux fumés et pommes de terre à la moutarde
				  -Pain fouace

				  -Gaufres au fromage
				  -Cretonnée de pois ou de fèves nouvelles
				  -Potiron au Gratin
				  -Venaison de sanglier
				  -Porridge aux fruits
				  -Hypocras au vin blanc
				  -Hypocras au vin rouge
				  -Hydromel
				  
				  -Chausson de pommes / figues / raisins
				  -Poires au sirop parfumées de canelle et de gingembre
				  -Compote de pommes aux amandes
				  -Rissoles pommes, figues, raisins, noix, épices

				  -Porc aigre-doux au gingembre
				  -Poulet sauté à la corianre et au cumin
				  -Poulet grillé au lait d'amande

				  -Fondue Vacherin
				  -Fondue Gruyère
				  -Fondue mixte à la noix de muscade
				  -Raclette 
				  -Sang du christ
			  
*/