using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations;

/// <inheritdoc />
public partial class PopulaClientes : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder mb)
    {
        mb.Sql("INSERT INTO Clientes(Nome, Idade, Sexo) VALUES('Ana Souza', 28, 'Feminino')");
        mb.Sql("INSERT INTO Clientes(Nome, Idade, Sexo) VALUES('Bruno Lima', 35, 'Masculino')");
        mb.Sql("INSERT INTO Clientes(Nome, Idade, Sexo) VALUES('Carla Mendes', 22, 'Feminino')");
        mb.Sql("INSERT INTO Clientes(Nome, Idade, Sexo) VALUES('Diego Pereira', 41, 'Masculino')");
        mb.Sql("INSERT INTO Clientes(Nome, Idade, Sexo) VALUES('Eduarda Silva', 30, 'Feminino')");
        mb.Sql("INSERT INTO Clientes(Nome, Idade, Sexo) VALUES('Felipe Costa', 27, 'Masculino')");
        mb.Sql("INSERT INTO Clientes(Nome, Idade, Sexo) VALUES('Gabriela Torres', 33, 'Feminino')");
        mb.Sql("INSERT INTO Clientes(Nome, Idade, Sexo) VALUES('Henrique Almeida', 45, 'Masculino')");
        mb.Sql("INSERT INTO Clientes(Nome, Idade, Sexo) VALUES('Isabela Martins', 19, 'Feminino')");
        mb.Sql("INSERT INTO Clientes(Nome, Idade, Sexo) VALUES('João Batista', 38, 'Masculino')");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder mb)
    {
        mb.Sql("DELETE FROM Clientes");
        mb.Sql("ALTER TABLE Clientes AUTO_INCREMENT = 1");
    }
}