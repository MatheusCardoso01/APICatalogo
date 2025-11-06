using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImageUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Coca-Cola Diet', 'Refrigerante de Cola 350 ml', 5.45, 'cocacola.jpg', 50, now(), 1)");

            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImageUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Fanta', 'Refrigerante de Guaraná 2 l', 8.45, 'fanta-guaraná.jpg', 25, now(), 1)");

            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImageUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Cachorro Quente', 'Salgado', 15.00, 'cachorroquente.jpg', 0, now(), 2)");

            mb.Sql("Insert into Produtos(Nome, Descricao, Preco, ImageUrl, Estoque, DataCadastro, CategoriaId) " +
                "Values('Pudim de Leite Condensado', 'Pudim', 25.00, 'pudim-leite-condensado.jpg', 6, now(), 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Produtos");
            mb.Sql("alter table Produtos auto_increment = 1");
        }
    }
}
