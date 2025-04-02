-- 1. Sélectionner tous les utilisateurs triés par nom en ordre décroissant
SELECT * FROM Utilisateur
ORDER BY Nom DESC;

-- 2. Sélectionner toutes les commandes triées par date de livraison (la plus récente en premier)
SELECT * FROM Commande
ORDER BY Date_Heure_Livraison DESC;

-- 3. Sélectionner toutes les livraisons triées par date de livraison décroissante
SELECT * FROM Livraison
ORDER BY Date_Livraison DESC;

-- 4. Sélectionner tous les plats triés par prix décroissant
SELECT * FROM Plat
ORDER BY Prix DESC;

-- 5. Sélectionner toutes les évaluations triées par note décroissante
SELECT * FROM Evaluation
ORDER BY Note DESC;