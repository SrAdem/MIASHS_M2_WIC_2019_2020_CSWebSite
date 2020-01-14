INSERT INTO Item (id_item, name, description, available, price, picture)
VALUES (1, 'Tabouret haut', 'Un tabouret pas mal haut ...', 1, 10, '000001.jpg'),
(2, 'Tabouret noir + pied', 'Un tabouret noir et un endroit où posser ses pieds', 1, 15, '000002.jpg'),
(3, 'Tabouret moche', 'Un tabouret petit et moche', 1, 50, '000003.jpg'),
(4, 'poing noir', 'Un coup de poing de couleur noir', 1, 2.5, '1.png'),
(5, 'Alignement centré', '', 1, 0.75, 'center-align.png'),
(6, 'check', 'C est ok !', 1, 1.02, 'check.png'),
(7, 'nope', 'pas possible', 0, 1.02, 'clear.png'),
(8, 'ordinateur', 'un ordinateur standard', 1, 350, 'computer.png'),
(9, 'Ordinateur Hacké', 'Pc qui marche pas tres bien car il a était hack', 1, 300, 'computer(1).png'),
(10, 'Contrat', 'Un jolie contrat a signer', 1, 10, 'contract.png'),
(11, 'Compte Facebook', 'Le compte facebook de quelqu un que j ai hacké', 1, 12, 'facebook.png'),
(12, 'drapeau de la france', 'Oui madame !', 1, 20, 'france.png'),
(13, 'Service de hack', 'Je vend mes services de hacker', 1, 5000, 'hacker.png'),
(14, 'une clé', 'Belle clé trouvé près des poubelles', 1, 2.5, 'key.png'),
(15, 'Thor', 'Figurine du héro Thor', 1, 23.75, 'superhero(1).png'),
(16, 'Hulk', 'Figurine du héro Hulk', 1, 23.75, 'superhero(2).png'),
(17, 'Flash', 'Figurine du héro Flash', 1, 23.75, 'superhero(3).png'),
(18, 'Wolverine', 'Figurine du héro Wolverine', 1, 23.75, 'superhero.png');

/*Les sous catégories*/
INSERT INTO Category (id_category,title)
VALUES (1,'Télé'),
(2,'Ordinateur'),
(3,'Aspirateur'),
(4,'Machine à laver'),

(5,'Tabouret'),
(6,'Table'),
(7,'Bureau'),
(8,'Chaise'),

(9,'Couvert'),
(10,'Assiettes');

INSERT INTO MasterCategory (id_mastercategory, title)
VALUES
(0,'Machine'),
(1,'Meuble'),
(2,'Cuisine');

INSERT INTO CategoriesLinks (id_mastercategory, id_subcategory)
VALUES (0,1),
(0,2),
(0,3),
(0,4),
(1,5),
(1,6),
(1,7),
(1,8),
(2,9),
(2,10);

INSERT INTO ItemCategory (id_item, id_category)
VALUES (1,5),
(2,5),
(3,5);

INSERT INTO Users (first_name, last_name, email, pass_word, superUser)
VALUES ('Adem', 'Gurbuz', 'adem@gur.buz', 'adem', 1),
('Nico', 'Gau', 'nico@gau.pastis', 'nico', 1),
('Sadok', 'Ben', 'sadok@ben.fredj', 0),
('Jimmy','Avae', 'Ymmij@eava.etouais', 0);