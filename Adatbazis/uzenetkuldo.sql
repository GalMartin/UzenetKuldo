SET NAMES utf8mb4;
CREATE DATABASE IF NOT EXISTS uzenetkuldo CHARACTER SET utf8mb4 COLLATE utf8mb4_hungarian_ci;
USE uzenetkuldo;
CREATE TABLE IF NOT EXISTS uzenet
(
    Id INT NOT NULL AUTO_INCREMENT,
    Szoveg TEXT NOT NULL,
    `KüldesiIdo` DATETIME NOT NULL,
    UzenetTipus VARCHAR(8) NOT NULL,
    Telefon VARCHAR(16) NULL,
    Email VARCHAR(64) NULL,
    PRIMARY KEY (Id),
    CHECK (UzenetTipus IN ('SMS', 'Email'))
);
INSERT INTO uzenet (Id, Szoveg, `KüldesiIdo`, UzenetTipus, Telefon, Email) VALUES
(1, 'Kérem, ellenőrizze postafiókját! Sürgős küldeménye érkezett.', '2026-09-02 15:00:28', 'SMS', '+36305679841', NULL),
(2, 'Adategyeztetés céljából kérjük, keresse fel honlapunkat: https://www.ceghonlap.hu', '2026-08-27 19:05:38', 'Email', NULL, 'ugyfelcim1@mail.hu'),
(3, 'Vigyázat, csalók!!!\r\nIsmeretlenek a cégünk nevével visszaélve bizalmas információk (jelszavak, szerződésadatok) megadását kérhetik öntől teefonon vagy emailben. Felhívjuk figyelmét, hogy ilyen információt senkitől sem kérünk, ezért ne dőljön be a csalóknak!', '2026-09-13 10:07:19', 'SMS', '+36205009301', NULL),
(4, 'Vigyázat, csalók!!!\r\nIsmeretlenek a cégünk nevével visszaélve bizalmas információk (jelszavak, szerződésadatok) megadását kérhetik öntől teefonon vagy emailben. Felhívjuk figyelmét, hogy ilyen információt senkitől sem kérünk, ezért ne dőljön be a csalóknak!', '2026-09-13 10:07:19', 'Email', NULL, 'kiemeltugyfel@mail.com') 
ON DUPLICATE KEY UPDATE Id = VALUES(Id);
