
CREATE TABLE Perfil (
    IdPerfil INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Descricao VARCHAR(20) NOT NULL
);


INSERT INTO Perfil (Descricao) VALUES ('Admim');
INSERT INTO Perfil (Descricao) VALUES ('Garcom');
INSERT INTO Perfil (Descricao) VALUES ('Cozinha');
INSERT INTO Perfil (Descricao) VALUES ('Copa');


CREATE TABLE Usuario (
    Email VARCHAR(255) NOT NULL PRIMARY KEY,
    Senha VARCHAR(128) NOT NULL,
    Nome VARCHAR(255) NOT NULL,
    IdPerfil INT NOT NULL,
    FOREIGN KEY (IdPerfil) REFERENCES Perfil(IdPerfil)
);


INSERT INTO Usuario (Email, Senha, Nome, IdPerfil) 
VALUES ('admin@example.com', 
'fa3c1cdee866e8b57b644e55aa85ad1f001ea14471da9d41cdd3195e5613f4b8b6fff905e7f1afb3954a3e182e92c52497e41decf5718b51a09bfadf52e77f20', 
'Admin User', 
1);
INSERT INTO Usuario (Email, Senha, Nome, IdPerfil) 
VALUES ('garcom@example.com', 
'47bba1a540dca13dd8ad5edae5f425103a0748a2e4e08920f4965bd9a9beeac6169a93a27c64991af1416af6a10417d5afed6479a41f9af1d59bdc3acc760da4',
'Garçom User',
2);
INSERT INTO Usuario (Email, Senha, Nome, IdPerfil) 
VALUES ('cozinha@example.com',
'ccc0b075bad6425a7f285b234940da249a2a01ed983877c57d1bc56ea82a2fc8dc112d359f8ff3b444109dbc69b5e9332423ba1119151aa81f1eef09d082c4aa',
'Cozinha User',
3);
INSERT INTO Usuario (Email, Senha, Nome, IdPerfil) 
VALUES ('copa@example.com', 
'd0801a78dc2f9d42d0156fce5ec909e2e84c16ff489c675a7d7339ed42ac5871dba7ee84b4312bf26f349d2662bcc74e91c8ad972bf98a1c6334b96166cb4c25',
'Copa User',
4);

SELECT * FROM Usuario;

SELECT Email, Nome, IdPerfil FROM Usuario WHERE Email = 'admin@example.com' AND Senha = 'fa3c1cdee866e8b57b644e55aa85ad1f001ea14471da9d41cdd3195e5613f4b8b6fff905e7f1afb3954a3e182e92c52497e41decf5718b51a09bfadf52e77f20';

CREATE TABLE Usuario (
    Email VARCHAR(255) NOT NULL PRIMARY KEY,
    Senha VARCHAR(128) NOT NULL,
    Nome VARCHAR(255) NOT NULL,
    IdPerfil INT NOT NULL,
    FOREIGN KEY (IdPerfil) REFERENCES Perfil(IdPerfil)
);

SELECT Email, Nome, IdPerfil 
FROM Usuario 
WHERE Email = 'admin@example.com' 
AND Senha = 'fa3c1cdee866e8b57b644e55aa85ad1f001ea14471da9d41cdd3195e5613f4b8b6fff905e7f1afb3954a3e182e92c52497e41decf5718b51a09bfadf52e77f20';


SELECT Email, Senha FROM Usuario;

SELECT * FROM Usuario u 
join Perfil p on u.IdPerfil = p.IdPerfil 
