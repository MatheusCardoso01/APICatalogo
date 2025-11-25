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

public class GetProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
{
    private readonly ProdutosController _controller;

    public GetProdutoUnitTests(ProdutosUnitTestController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.logger, controller.mapper);
    }

    [Fact]
    public async Task GetProdutoById_OkResult()
    {
        //Arrange
        var prodId = 2;

        //Act
        var data = await _controller.GetAsync(prodId);

        //Assert (xUnit)
        //var okResult = Assert.IsType<OkObjectResult>(data.Result);
        //Assert.Equal(200, okResult.StatusCode);

        //Assert (fluentassertions)
        data.Result.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task GetProdutoById_Return_NotFound()
    {
        //Arrange
        var prodId = 999;

        //Act
        var data = await _controller.GetAsync(prodId);

        //Assert
        data.Result.Should().BeOfType<NotFoundObjectResult>()
            .Which.StatusCode.Should().Be(404);
    }

    [Fact]
    public async Task GetProdutoById_Return_BadRequest()
    {
        //Arrange
        var prodId = -1;

        //Act
        var data = await _controller.GetAsync(prodId);

        //Assert
        data.Result.Should().BeOfType<BadRequestObjectResult>()
            .Which.StatusCode.Should().Be(400);
    }

    [Fact]
    public async Task Getprodutos_Return_ListOfProdutoDTO()
    {
        //Arrange não tem

        //Act
        var data = await _controller.GetAllAsync();

        //Assert
        data.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeAssignableTo<IEnumerable<ProdutoDTO>>().And.NotBeNull();
    }

    [Fact]
    public async Task GetProdutos_Return_NotFound()
    { 
        //Arrange não tem

        //Act
        var data = await _controller.GetAllAsync();

        //Assert
        data.Result.Should().BeOfType<NotFoundObjectResult>(); // somente se entidade produto estiver vazia
    }
}
