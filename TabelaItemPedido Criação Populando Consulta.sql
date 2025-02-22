CREATE TABLE ItemPedido (
    IdItemPedido INT PRIMARY KEY AUTO_INCREMENT,
    PedidoId INT NOT NULL,
    ProdutoId INT NOT NULL,
    Quantidade INT NOT NULL,
    FOREIGN KEY (PedidoId) REFERENCES Pedido(IdPedido) ON DELETE CASCADE,
    FOREIGN KEY (ProdutoId) REFERENCES Produto(IdProduto)
);

DROP TABLE ItemPedido;

SELECT * FROM ItemPedido;