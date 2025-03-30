DROP DATABASE IF EXISTS livin_paris_bdd;
CREATE DATABASE IF NOT EXISTS livin_paris_bdd;
USE livin_paris_bdd;


CREATE TABLE Utilisateur(
   ID INT AUTO_INCREMENT,
   Nom VARCHAR(50) NOT NULL,
   Prénom VARCHAR(50) NOT NULL,
   Téléphone INT,
   Mail VARCHAR(50),
   Rue VARCHAR(50),
   Numero_Rue VARCHAR(50),
   Ville VARCHAR(50),
   Code_Postal INT,
   Station_Plus_Proche VARCHAR(50),
   PRIMARY KEY(ID)
);

CREATE TABLE Cuisinier(
   ID_cuisinier INT AUTO_INCREMENT,
   ID INT NOT NULL,
   PRIMARY KEY(ID_cuisinier),
   UNIQUE(ID),
   FOREIGN KEY(ID) REFERENCES Utilisateur(ID)
);

CREATE TABLE Client_(
   ID_client INT AUTO_INCREMENT,
   Entreprise BOOL,
   ID INT NOT NULL,
   PRIMARY KEY(ID_client),
   UNIQUE(ID),
   FOREIGN KEY(ID) REFERENCES Utilisateur(ID)
);

CREATE TABLE Commande(
   ID_Commande INT,
   Prix_Commande DECIMAL(15,2),
   Nombre_Portions INT,
   Date_Heure_Livraison DATETIME,
   ID_client INT NOT NULL,
   PRIMARY KEY(ID_Commande),
   FOREIGN KEY(ID_client) REFERENCES Client_(ID_client)
);

CREATE TABLE Livraison(
   ID_Livraison INT,
   adresse_client TEXT,
   adresse_cuisinier TEXT,
   Date_Livraison DATETIME,
   ID_Commande INT NOT NULL,
   PRIMARY KEY(ID_Livraison),
   FOREIGN KEY(ID_Commande) REFERENCES Commande(ID_Commande)
);

CREATE TABLE Recette(
   ID_Recette INT,
   Nom VARCHAR(50),
   PRIMARY KEY(ID_Recette)
);

CREATE TABLE Nationalité(
   Pays VARCHAR(50),
   PRIMARY KEY(Pays)
);

CREATE TABLE Ingrédient(
   Nom VARCHAR(50),
   PRIMARY KEY(Nom)
);

CREATE TABLE Evaluation(
   ID_client INT,
   Note INT,
   commentaire VARCHAR(50),
   jour DATE,
   ID_Commande INT NOT NULL,
   PRIMARY KEY(ID_client),
   FOREIGN KEY(ID_Commande) REFERENCES Commande(ID_Commande)
);

CREATE TABLE Plat(
   ID_Plat INT,
   Nom VARCHAR(50) NOT NULL,
   Quantité INT,
   Prix DECIMAL(15,2),
   Régime_alimentaire VARCHAR(50),
   Date_Fabrication DATETIME,
   Date_Péremption DATETIME,
   Nombre_Portions_Total INT,
   Plat_Du_Jour BOOL,
   ID_Recette INT NOT NULL,
   ID_cuisinier INT NOT NULL,
   PRIMARY KEY(ID_Plat),
   FOREIGN KEY(ID_Recette) REFERENCES Recette(ID_Recette),
   FOREIGN KEY(ID_cuisinier) REFERENCES Cuisinier(ID_cuisinier)
);

CREATE TABLE est_commandé(
   ID_Plat INT,
   ID_Commande INT,
   PRIMARY KEY(ID_Plat, ID_Commande),
   FOREIGN KEY(ID_Plat) REFERENCES Plat(ID_Plat),
   FOREIGN KEY(ID_Commande) REFERENCES Commande(ID_Commande)
);

CREATE TABLE Vient(
   ID_Plat INT,
   Pays VARCHAR(50),
   PRIMARY KEY(ID_Plat, Pays),
   FOREIGN KEY(ID_Plat) REFERENCES Plat(ID_Plat),
   FOREIGN KEY(Pays) REFERENCES Nationalité(Pays)
);

CREATE TABLE contient(
   ID_Recette INT,
   Nom VARCHAR(50),
   PRIMARY KEY(ID_Recette, Nom),
   FOREIGN KEY(ID_Recette) REFERENCES Recette(ID_Recette),
   FOREIGN KEY(Nom) REFERENCES Ingrédient(Nom)
);
