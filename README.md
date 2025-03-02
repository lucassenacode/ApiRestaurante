# Prova de Conceito - Sistema para Restaurante

## Objetivo
Desenvolver um sistema para restaurante que permita gerenciar pedidos de maneira eficiente.

## Requisitos do Sistema
### Funcionalidades Principais
1. **Cadastro de Pedidos**
   - O usuário pode selecionar produtos que compõem o cardápio.
   - Definir a quantidade, mesa e nome do solicitante.
2. **Exibição para Setores**
   - A "Cozinha" deve visualizar apenas os pedidos de pratos.
   - A "Copa" deve visualizar apenas os pedidos de bebidas.
3. **Atualização de Status**
   - Cada setor pode alterar o status do pedido (Em preparo, Pronto, Entregue).
4. **Autenticação de Usuários**
   - O usuário e senha para acessar o sistema.
5. **Histórico de Pedidos**
   - Listagem dos pedidos finalizados.

## Requisitos Técnicos
### Frontend
- Framework: Bootstrap para estilização e responsividade (ou outra de sua escolha).

### Backend
- Tecnologia: C#.
- Banco de Dados: Livre escolha, desde que documentado como executar.

### Diferenciais (Opcional)
- Uso do AJAX.

---

## Controle de Acesso e Roles

### [AllowAnonymous] (Acesso Público)
- **POST** `/restaurante/login` - Endpoint de login para gerar token JWT.
- **GET** `/restaurante/produto` - Listagem pública de produtos para visualização do cardápio.

### [Admin User 1] (Administrador)
- Todos os endpoints da API

### [Garçom User 2] (Garçom)
- **GET /restaurante/pedidos**
- **GET /restaurante/pedido/{id}**
- **POST /restaurante/pedido**
- **PUT /restaurante/pedido/{id}**
- **PUT /restaurante/pedido/{id}/status**
- **GET /restaurante/produto**
- **GET /restaurante/produto/{id}**

### [Cozinha User 3] (Cozinha)
- **GET /restaurante/pedidos/cozinha**
- **PUT /restaurante/pedido/{id}/status**

### [Copa User 4] (Copa)
- **GET /restaurante/pedidos/copa**
- **PUT /restaurante/pedido/{id}/status**


---

## Endpoints da API
### Autenticação
- **POST** `/restaurante/login` - Autenticação e geração de token JWT.

### Pedidos
- **GET** `/restaurante/pedidos` - Listar todos os pedidos.
- **GET** `/restaurante/pedido/{id}` - Obter detalhes de um pedido.
- **POST** `/restaurante/pedido` - Criar um novo pedido.
- **PUT** `/restaurante/pedido/{id}` - Atualizar um pedido.
- **DELETE** `/restaurante/pedido/{id}` - Excluir um pedido.
- **PUT** `/restaurante/pedido/{id}/status` - Atualizar status do pedido.
- **GET** `/restaurante/pedidos/copa` - Listar pedidos da Copa.
- **GET** `/restaurante/pedidos/cozinha` - Listar pedidos da Cozinha.
- **GET** `/restaurante/pedidos/historico` - Listar pedidos finalizados.

### Itens do Pedido
- **GET** `/restaurante/itemPedido/{pedidoId}` - Listar itens de um pedido.
- **POST** `/restaurante/itemPedido` - Adicionar item ao pedido.
- **PUT** `/restaurante/itemPedido/{idItemPedido}` - Atualizar um item do pedido.
- **DELETE** `/restaurante/itemPedido/{idItemPedido}` - Remover um item do pedido.

### Produtos
- **GET** `/restaurante/produto` - Listar produtos.
- **GET** `/restaurante/produto/{id}` - Obter detalhes de um produto.
- **POST** `/restaurante/produto` - Criar um novo produto.
- **PUT** `/restaurante/produto/{id}` - Atualizar um produto.
- **DELETE** `/restaurante/produto/{id}` - Remover um produto.

### ## Perfis de Usuário

Usuários cadastrados para testes:

```json
{
  "Email": "admin@example.com",
  "Senha": "senha123",
  "Nome": "Admin User 1"
}

{
  "Email": "garcom@example.com",
  "Senha": "senha456",
  "Nome": "Garçom User 2"
}

{
  "Email": "cozinha@example.com",
  "Senha": "senha789",
  "Nome": "Cozinha User 3"
}

{
  "Email": "copa@example.com",
  "Senha": "senha101",
  "Nome": "Copa User 4"
}
```


## Configuração do Banco de Dados

```json
"AllowedHosts": "*",
"ConnectionStrings": {
  "MySqlConnection": "Server=sql10.freesqldatabase.com;Database=sql10764721;User Id=sql10764721;Password=hqnHlsPqeQ;Port=3306;"
}
```

(O banco acima expirou, então um novo deve ser configurado.)

## Modelo do Banco de Dados

```sql
CREATE TABLE Produto (
	IdProduto INT AUTO_INCREMENT PRIMARY KEY,
    NomeProduto VARCHAR(100) NOT NULL,       
    Preco DECIMAL(15, 2) NOT NULL,           
    TipoProduto ENUM('Comida', 'Bebida') NOT NULL 
);

CREATE TABLE Pedido (
    IdPedido INT PRIMARY KEY AUTO_INCREMENT,
    NomeCliente VARCHAR(255) NOT NULL,
    NumeroMesa INT NOT NULL,
    Status ENUM('Novo', 'EmAndamento', 'Pronto', 'Entregue') NOT NULL DEFAULT 'Novo',
    CriadoEm TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE ItemPedido ( 
    IdItemPedido INT PRIMARY KEY AUTO_INCREMENT,
    IdPedido INT NOT NULL,
    IdProduto INT NOT NULL,
    Quantidade INT NOT NULL,
    FOREIGN KEY (IdPedido) REFERENCES Pedido(IdPedido) ON DELETE CASCADE,
    FOREIGN KEY (IdProduto) REFERENCES Produto(IdProduto)
);

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
```

#### Pedido
```json
{
  "idPedido": 1,
  "nomeCliente": "João Silva",
  "numeroMesa": 5,
  "itens": [
    { "idProduto": 1, "quantidade": 2 }
  ],
  "status": 1,
  "criadoEm": "2024-03-01T12:00:00Z"
}
```

#### Produto
```json
{
  "IdProduto": 1,
  "NomeProduto": "Hambúrguer",
  "Preco": 25.90,
  "TipoProduto": 1
}
```

#### Status do Pedido
| Código | Status    |
|--------|----------|
| 1      | Novo     |
| 2      | Em Preparo |
| 3      | Pronto   |
| 4      | Entregue |

#### Tipo de Produto
| Código | Tipo      |
|--------|----------|
| 1      | Prato    |
| 2      | Bebida   |

### Segurança
- O sistema utiliza **JWT** para autenticação.
- O token deve ser enviado no cabeçalho `Authorization: Bearer {seu_token}` para acessar endpoints protegidos.

---

## Como Executar o Projeto
1. **Configurar o Banco de Dados:**
   - Criar a base de dados conforme as instruções do projeto.
2. **Rodar o Backend:**
   - Abrir a solução no Visual Studio.
   - Configurar a `appsettings.json` conforme o banco de dados.
   - Rodar o projeto (`dotnet run` ou via IDE).
3. **Rodar o Frontend:**
   - Abrir o projeto frontend.
   - Iniciar um servidor local (`Live Server` ou equivalente) do login.html
   
---


