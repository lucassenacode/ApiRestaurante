-- Criação Tabela Produto
CREATE TABLE Produto (
	IdProduto INT AUTO_INCREMENT PRIMARY KEY,
    NomeProduto VARCHAR(100) NOT NULL,       
    Preco DECIMAL(15, 2) NOT NULL,           
    TipoProduto ENUM('Comida', 'Bebida') NOT NULL 
);

DROP TABLE Produto;

INSERT INTO Produto (NomeProduto, Preco, TipoProduto)
VALUES 
    ('Hambúrguer Clássico', 25.99, 'Comida'),
    ('Pizza Margherita', 45.50, 'Comida'),
    ('Batata Frita', 12.00, 'Comida'),
    ('Lasanha à Bolonhesa', 35.90, 'Comida'),
    ('Salada Caesar', 18.75, 'Comida'),
    ('Frango Grelhado', 28.50, 'Comida'),
    ('Strogonoff de Carne', 32.00, 'Comida'),
    ('Risoto de Cogumelos', 40.00, 'Comida'),
    ('Macarrão Carbonara', 30.00, 'Comida'),
    ('Feijoada', 50.00, 'Comida');

INSERT INTO Produto (NomeProduto, Preco, TipoProduto)
VALUES 
    ('Refrigerante Cola', 8.50, 'Bebida'),
    ('Suco de Laranja Natural', 12.00, 'Bebida'),
    ('Água Mineral', 5.00, 'Bebida'),
    ('Cerveja Artesanal', 15.00, 'Bebida'),
    ('Chá Gelado', 7.50, 'Bebida'),
    ('Caipirinha', 18.00, 'Bebida'),
    ('Vinho Tinto', 60.00, 'Bebida'),
    ('Café Expresso', 6.00, 'Bebida'),
    ('Milkshake de Chocolate', 14.00, 'Bebida'),
    ('Energético', 10.00, 'Bebida');
    
SELECT * FROM Produto;    