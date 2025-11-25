using APICatalogo.Controllers;
using APICatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiCatalogoxUnitTests.UnitTests;

public class PutProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
{
    private readonly ProdutosController _controller;

    public PutProdutoUnitTests(ProdutosUnitTestController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.logger, controller.mapper);
    }

    [Fact]
    public async Task PutProduto_Return_OkResult()
    {
        //Arrange
        var prodId = 9;

        var produto = new ProdutoDTO();
        produto.ProdutoId = prodId;
        produto.Nome = "NovoNome";
        produto.Descricao = "Nova descrição";
        produto.Preco = 999.99M;
        produto.ImageUrl = "novo-produto.jpg";
        produto.CategoriaId = 1;

        //Act
        var data = await _controller.PutAsync(prodId, produto);

        //Assert
        data.Result.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task PutProduto_Return_BadRequest()
    {
        //Arrange
        var prodId = 9;

        var produto = new ProdutoDTO();
        produto.ProdutoId = 9999;
        produto.Nome = "NovoNome";
        produto.Descricao = "Nova descrição";
        produto.Preco = 999.99M;
        produto.ImageUrl = "novo-produto.jpg";
        produto.CategoriaId = 1;

        //Act
        var data = await _controller.PutAsync(prodId, produto);

        //Assert
        data.Result.Should().BeOfType<BadRequestObjectResult>()
            .Which.StatusCode.Should().Be(400);
    }
}
