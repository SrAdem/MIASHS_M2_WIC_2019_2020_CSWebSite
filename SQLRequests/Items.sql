INSERT INTO Item (id_item, name, description, available, price, picture)
VALUES (1, 'Tabouret haut', 'Un tabouret pas mal haut ...', 1, 10, '000001.jpg'),
(2, 'Tabouret noir + pied', 'Un tabouret noir et un endroit où posser ses pieds', 1, 15, '000002.jpg'),
(3, 'Tabouret moche', 'Un tabouret petit et moche', 1, 50, '000003.jpg');


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
(10,'Assiettes'),

(11,'Machine'),
(12,'Meuble'),
(13,'Cuisine');

INSERT INTO CategorysLink (id_category, id_subcategory)
VALUES (11,1),
(11,2),
(11,3),
(11,4),
(12,5),
(12,6),
(12,7),
(12,8),
(13,9),
(13,10);

INSERT INTO ItemCategory (id_item, id_category)
VALUES (1,5),
(2,5),
(3,5);