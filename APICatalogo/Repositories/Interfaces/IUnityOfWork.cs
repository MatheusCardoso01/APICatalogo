namespace APICatalogo.Repositories.Interfaces;

public interface IUnityOfWork
{
    IProdutoRepository ProdutoRepository { get; }
    ICategoriaRepository CategoriaRepository { get; }
    IClienteRepository ClienteRepository { get; }   
    void Commit();
}
