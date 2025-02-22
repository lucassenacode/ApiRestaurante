-- Criação Tabela Pedido
CREATE TABLE Pedido (
    IdPedido INT PRIMARY KEY AUTO_INCREMENT,
    NomeCliente VARCHAR(255) NOT NULL,
    NumeroMesa INT NOT NULL,
    Status ENUM('Novo', 'EmAndamento', 'Pronto', 'Entregue') NOT NULL DEFAULT 'Novo',
    CriadoEm TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
DROP TABLE Pedido;

INSERT INTO Pedido (NomeCliente, NumeroMesa) VALUES ('Fernanda Lima', 3);
INSERT INTO Pedido (NomeCliente, NumeroMesa, Status) VALUES ('Carlos Pereira', 7, 'EmAndamento');
INSERT INTO Pedido (NomeCliente, NumeroMesa, Status, CriadoEm) VALUES ('Isabela Souza', 1, 'Pronto', '2024-07-27 12:30:00');
INSERT INTO Pedido (NomeCliente, NumeroMesa) VALUES ('Rafael Oliveira', 12);
INSERT INTO Pedido (NomeCliente, NumeroMesa, Status) VALUES ('Juliana Santos', 9, 'EmAndamento');
INSERT INTO Pedido (NomeCliente, NumeroMesa, Status, CriadoEm) VALUES ('Lucas Rodrigues', 6, 'Entregue', '2024-07-27 19:45:00');
INSERT INTO Pedido (NomeCliente, NumeroMesa) VALUES ('Mariana Costa', 11);
INSERT INTO Pedido (NomeCliente, NumeroMesa, Status) VALUES ('Gustavo Almeida', 4, 'Novo');
INSERT INTO Pedido (NomeCliente, NumeroMesa, Status, CriadoEm) VALUES ('Camila Ferreira', 15, 'Pronto', '2024-07-28 08:15:00');
INSERT INTO Pedido (NomeCliente, NumeroMesa) VALUES ('Bruno Silva', 2);

SELECT * FROM Pedido;  