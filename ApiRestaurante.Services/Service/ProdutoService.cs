public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _repositorio;

    public void InserirProduto(Produto produto)
    {
        if (produto == null)
            throw new ArgumentNullException(nameof(produto));

        try
        {
            ((Contexto)_repositorio).AbrirConexao();
            // Validar se já existe produto com mesmo nome
            var produtoExistente = _repositorio.ObterProdutoPorNome(produto.NomeProduto);
            if (produtoExistente != null)
            {
                throw new Exception($"Já existe um produto com o nome: {produto.NomeProduto}");
            }
            _repositorio.InserirProduto(produto);
        }
        finally
        {
            ((Contexto)_repositorio).FecharConexao();
        }
    }

    public void AtualizarProduto(Produto produto)
    {
        if (produto == null)
            throw new ArgumentNullException(nameof(produto));

        try
        {
            ((Contexto)_repositorio).AbrirConexao();
            // Validar se produto existe
            if (!_repositorio.ProdutoExiste(produto.IdProduto))
            {
                throw new Exception($"Produto de ID: {produto.IdProduto} não encontrado");
            }
            // Validar se novo nome não conflita com outro produto
            var produtoExistente = _repositorio.ObterProdutoPorNome(produto.NomeProduto);
            if (produtoExistente != null && produtoExistente.IdProduto != produto.IdProduto)
            {
                throw new Exception($"Já existe outro produto com o nome: {produto.NomeProduto}");
            }
            _repositorio.AtualizarProduto(produto);
        }
        finally
        {
            ((Contexto)_repositorio).FecharConexao();
        }
    }

    public Produto ObterProdutoPorId(int idProduto)
    {
        try
        {
            ((Contexto)_repositorio).AbrirConexao();
            var produto = _repositorio.ObterProdutoPorId(idProduto);
            if (produto == null)
                throw new Exception($"Produto de ID: {idProduto} não encontrado");
            return produto;
        }
        finally
        {
            ((Contexto)_repositorio).FecharConexao();
        }
    }
}