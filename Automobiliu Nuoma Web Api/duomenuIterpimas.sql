-- Įterpti duomenis į Automobiliai lentelę
INSERT INTO Automobiliai (Id, Pavadinimas, Metai, NuomosKaina) VALUES
(1, 'Toyota Corolla', 2020, 30.00),
(2, 'Volkswagen Golf', 2019, 35.00),
(3, 'Tesla Model 3', 2021, 50.00);

-- Įterpti duomenis į NaftosAutomobiliai lentelę
INSERT INTO NaftosAutomobiliai (Id, VariklioTuris, DegaluTipas, CO2Ismetimas) VALUES
(1, 1.8, 'Benzinas', 120),
(2, 2.0, 'Dyzelinas', 140);

-- Įterpti duomenis į ElektriniaiAutomobiliai lentelę
INSERT INTO ElektriniaiAutomobiliai (Id, BaterijosTalpa, MaxNuvaziuojamasAtstumas, IkrovimoLaikas) VALUES
(3, 75, 500, 1.5);

-- Įterpti duomenis į Klientai lentelę
INSERT INTO Klientai (Id, Vardas, Pavarde, ElPastas, Telefonas) VALUES
(1, 'Jonas', 'Jonaitis', 'jonas@example.com', '123456789'),
(2, 'Petras', 'Petraitis', 'petras@example.com', '987654321');

-- Įterpti duomenis į Darbuotojai lentelę
INSERT INTO Darbuotojai (Id, Vardas, Pavarde, Pareigos) VALUES
(1, 'Andrius', 'Andriulaitis', 'Administratorius'),
(2, 'Eglė', 'Eglutė', 'Vadybininkas');

-- Įterpti duomenis į NuomosUzsakymai lentelę
INSERT INTO NuomosUzsakymai (Id, KlientasId, DarbuotojasId, AutomobilisId, PradziosData, PabaigosData, Kaina) VALUES
(1, 1, 1, 1, '2024-01-01', '2024-01-05', 150.00),
(2, 2, 2, 3, '2024-02-15', '2024-02-20', 250.00);
