CREATE TABLE ItemPedido (
    IdItemPedido INT PRIMARY KEY AUTO_INCREMENT,
    IdPedido INT NOT NULL,
    IdProduto INT NOT NULL,
    Quantidade INT NOT NULL,
    FOREIGN KEY (IdPedido) REFERENCES Pedido(IdPedido) ON DELETE CASCADE,
    FOREIGN KEY (IdProduto) REFERENCES Produto(IdProduto)
);

DROP TABLE ItemPedido;

-- Pedido 1 (Fernanda Lima, Mesa 3)
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (1, 1, 1); -- Hambúrguer Clássico
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (1, 11, 1); -- Refrigerante Cola

-- Pedido 2 (Carlos Pereira, Mesa 7)
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (2, 2, 1); -- Pizza Margherita
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (2, 14, 1); -- Cerveja Artesanal

-- Pedido 3 (Isabela Souza, Mesa 1)
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (3, 4, 1); -- Lasanha à Bolonhesa
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (3, 12, 1); -- Suco de Laranja Natural

-- Pedido 4 (Rafael Oliveira, Mesa 12)
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (4, 6, 2); -- Frango Grelhado (2 unidades)
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (4, 13, 2); -- Água Mineral (2 unidades)

-- Pedido 5 (Juliana Santos, Mesa 9)
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (5, 8, 1); -- Risoto de Cogumelos
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (5, 17, 1); -- Vinho Tinto

-- Pedido 6 (Lucas Rodrigues, Mesa 6)
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (6, 10, 1); -- Feijoada
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (6, 15, 1); -- Chá Gelado

-- Pedido 7 (Mariana Costa, Mesa 11)
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (7, 3, 1); -- Batata Frita
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (7, 18, 1); -- Café Expresso

-- Pedido 8 (Gustavo Almeida, Mesa 4)
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (8, 5, 1); -- Salada Caesar
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (8, 16, 1); -- Caipirinha

-- Pedido 9 (Camila Ferreira, Mesa 15)
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (9, 7, 1); -- Strogonoff de Carne
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (9, 19, 1); -- Milkshake de Chocolate

-- Pedido 10 (Bruno Silva, Mesa 2)
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (10, 9, 1); -- Macarrão Carbonara
INSERT INTO ItemPedido (IdPedido, IdProduto, Quantidade) VALUES (10, 20, 1); -- Energético


SELECT * FROM ItemPedido;