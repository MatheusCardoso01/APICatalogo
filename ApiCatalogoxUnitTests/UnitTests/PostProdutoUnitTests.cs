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

public class PostProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
{
    private readonly ProdutosController _controller;

    public PostProdutoUnitTests(ProdutosUnitTestController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.logger, controller.mapper);
    }

    [Fact]
    public async Task PostProduto_Return_CreatedResult()
    {
        //Arrange
        var newProduto = new ProdutoDTO();

        newProduto.Nome = "Fanta";
        newProduto.Descricao = "Refrigerante de Guaraná";
        newProduto.Preco = (Decimal) 10.00;
        newProduto.ImageUrl = "refri.png";
        newProduto.CategoriaId = 1;

        //Act
        var data = await _controller.PostAsync(newProduto);

        //Assert
        data.Result.Should().BeOfType<CreatedAtRouteResult>()
            .Which.StatusCode.Should().Be(201);
    }

    [Fact]
    public async Task PostProduto_Return_BadRequest()
    {
        //Arrange
        var newProduto = new ProdutoDTO();

        newProduto = null;

        //Act
        var data = await _controller.PostAsync(newProduto);

        //Assert
        data.Result.Should().BeOfType<BadRequestObjectResult>()
            .Which.StatusCode.Should().Be(400);
    }
}
