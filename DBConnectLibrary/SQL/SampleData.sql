INSERT INTO Utilisateur (ID, Nom, Prénom, Téléphone, Mail, Rue, Numero_Rue, Ville, Code_Postal, Station_Plus_Proche) VALUES
(1, 'Varenne Welnovski', 'Vladimir', 768799734, 'varenne.vw@gmail.com', 'Rue Poulet', '33', 'Paris', 75018, 73),
(2, 'Cogo', 'Sacha', 600000002, 'scogo@gmail.com', 'PH', '11', 'Paris', 75017, 46),
(3, 'Lestrat', 'Damien', 600000003, 'dlestrat@gmail.com', 'PH', '11', 'Paris', 75017, 46),
(4, 'Peytavin', 'Gabriel', 600000004, 'ilovebryan@gmail.com', 'PH', '36', 'Paris', 75017, 321),
(5, 'Pautras', 'Thomas', 600000005, 'tpautras@gmail.com', 'PH', '22', 'Paris', 75013, 329),
(6, 'Belloc', 'Arthur', 600000006, 'abelloc@gmail.com', 'PH', '6', 'Paris', 75007, 187),
(7, 'Bellegarde', 'Gaspar', 600000007, 'gbellegarde@gmail.com', 'PH', '33', 'Paris', 75018, 73),
(8, 'Bossuet', 'Felix', 600000008, 'fbossuet@gmail.com', 'PH', '42', 'Paris', 75004, 13),
(9, 'Dougui', 'Malik', 600000009, 'mdoughi@gmail.com', 'PH', '9', 'Paris', 75018, 320);

INSERT INTO Cuisinier (ID_cuisinier, ID) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);

INSERT INTO Client_ (ID_client, Entreprise, ID) VALUES
(1, FALSE, 6),
(2, FALSE, 7),
(3, FALSE, 8),
(4, FALSE, 9);

INSERT INTO Commande (ID_Commande, Prix_Commande, Nombre_Portions, Date_Heure_Livraison, ID_client, ID_cuisinier) VALUES
(1, 45.50, 2, '2025-03-05 12:00:00', 1, 1),
(2, 32.00, 1, '2025-03-06 13:00:00', 2, 2),
(3, 58.75, 3, '2025-03-07 18:30:00', 3, 3),
(4, 27.90, 2, '2025-03-08 12:45:00', 4, 4);

INSERT INTO Livraison (ID_Livraison, station_client, station_cuisinier, Date_Livraison, est_livre, ID_Commande, ID_Client) VALUES
(1, '16, Rue du Moulin, Paris', '1, Rue de la Paix, Paris', '2025-03-05 13:00:00', false, 1, 1),
(2, '17, Boulevard des Fêtes, Lille', '2, Avenue des Champs, Paris', '2025-03-06 14:00:00', false,2, 1),
(3, '18, Allée des Roses, Strasbourg', '3, Boulevard Saint-Germain, Paris', '2025-03-07 19:00:00', false,3, 1),
(4, '19, Impasse du Soleil, Nantes', '4, Rue Victor Hugo, Lyon', '2025-03-08 13:00:00', false,4, 1);

INSERT INTO Recette (ID_Recette, Nom, Régime_alimentaire) VALUES
(1, 'Recette 1', 'Omnivore'),
(2, 'Recette 2', 'Omnivore'),
(3, 'Recette 3', 'Omnivore'),
(4, 'Recette 4', 'Omnivore'),
(5, 'Recette 5', 'Omnivore');

INSERT INTO Nationalité (Pays) VALUES
('France'),
('Italie'),
('Espagne'),
('Allemagne'),
('Royaume-Uni'),
('Etats-Unis'),
('Canada'),
('Japon'),
('Chine'),
('Bresil'),
('Inde'),
('Australie'),
('Mexique'),
('Russie'),
('Suede');

INSERT INTO Ingrédient (Nom) VALUES
('Tomate'),
('Fromage'),
('Poulet'),
('Pâtes'),
('Oeuf'),
('Farine'),
('Lait'),
('Boeuf'),
('Carotte'),
('Pommes de terre'),
('Oignon'),
('Ail'),
('Basilic'),
('Thym'),
('Laurier'),
('Sel'),
('Poivre'),
('Huile d''olive'),
('Champignon');

INSERT INTO Evaluation (ID_client, Note, commentaire, jour, ID_Commande) VALUES
(1, 4, 'Très bon', '2025-03-06', 1),
(2, 5, 'Excellent service', '2025-03-07', 2),
(3, 3, 'Moyen', '2025-03-08', 3),
(4, 4, 'Bon rapport qualité/prix', '2025-03-09', 4);

INSERT INTO Plat (ID_Plat, Nom, Quantité, Prix, Date_Fabrication, Date_Péremption, Nombre_Portions_Total, Plat_Du_Jour, ID_Recette, ID_cuisinier) VALUES
(1, 'Plat 1', 10, 12.50,  '2025-03-03 10:00:00', '2025-03-10 10:00:00', 5, TRUE, 1, 1),
(2, 'Plat 2', 8, 10.00, '2025-03-03 11:00:00', '2025-03-10 11:00:00', 4, FALSE, 2, 2),
(3, 'Plat 3', 12, 15.75, '2025-03-03 12:00:00', '2025-03-10 12:00:00', 6, TRUE, 3, 3),
(4, 'Plat 4', 9, 11.00,  '2025-03-03 13:00:00', '2025-03-10 13:00:00', 4, FALSE, 4, 4),
(5, 'Plat 5', 15, 18.20,  '2025-03-03 14:00:00', '2025-03-10 14:00:00', 7, TRUE, 5, 5);

INSERT INTO est_commandé (ID_Plat, ID_Commande) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4);

INSERT INTO Vient (ID_Plat, Pays) VALUES
(1, 'France'),
(2, 'Italie'),
(3, 'Espagne'),
(4, 'Allemagne'),
(5, 'Royaume-Uni');

INSERT INTO contient (ID_Recette, Nom) VALUES
(1, 'Tomate'),
(2, 'Fromage'),
(3, 'Poulet'),
(4, 'Boeuf'),
(5, 'Carotte');
