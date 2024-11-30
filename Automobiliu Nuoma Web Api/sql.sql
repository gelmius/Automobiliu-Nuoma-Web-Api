


CREATE TABLE Automobiliai (
    Id INT PRIMARY KEY,
    Pavadinimas NVARCHAR(100) NOT NULL,
    Metai INT NOT NULL,
    NuomosKaina DECIMAL(18, 2) NOT NULL
);

CREATE TABLE NaftosAutomobiliai (
    Id INT PRIMARY KEY,
    VariklioTuris DECIMAL(18, 2) NOT NULL,
    DegaluTipas NVARCHAR(50) NOT NULL,
    CO2Ismetimas DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (Id) REFERENCES Automobiliai(Id)
);

CREATE TABLE ElektriniaiAutomobiliai (
    Id INT PRIMARY KEY,
    BaterijosTalpa DECIMAL(18, 2) NOT NULL,
    MaxNuvaziuojamasAtstumas INT NOT NULL,
    IkrovimoLaikas DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (Id) REFERENCES Automobiliai(Id)
);

CREATE TABLE Darbuotojai (
    Id INT PRIMARY KEY,
    Vardas NVARCHAR(100) NOT NULL,
    Pavarde NVARCHAR(100) NOT NULL,
    Pareigos NVARCHAR(50) NOT NULL
);

CREATE TABLE Klientai (
    Id INT PRIMARY KEY,
    Vardas NVARCHAR(100) NOT NULL,
    Pavarde NVARCHAR(100) NOT NULL,
    ElPastas NVARCHAR(100) NOT NULL,
    Telefonas NVARCHAR(15) NOT NULL
);


CREATE TABLE NuomosUzsakymai (
    Id INT PRIMARY KEY,
    KlientasId INT NOT NULL,
    DarbuotojasId INT NOT NULL,
    AutomobilisId INT NOT NULL,
    PradziosData DATETIME NOT NULL,
    PabaigosData DATETIME NOT NULL,
    Kaina DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (KlientasId) REFERENCES Klientai(Id),
    FOREIGN KEY (DarbuotojasId) REFERENCES Darbuotojai(Id),
    FOREIGN KEY (AutomobilisId) REFERENCES Automobiliai(Id)
);
