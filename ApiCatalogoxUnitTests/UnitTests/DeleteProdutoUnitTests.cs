using APICatalogo.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApiCatalogoxUnitTests.UnitTests;

public class DeleteProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
{
    private readonly ProdutosController _controller;

    public DeleteProdutoUnitTests(ProdutosUnitTestController controller)
    {
        _controller = new ProdutosController(controller.repository, controller.logger, controller.mapper);
    }

    [Fact]
    public async Task DeleteProdutoById_Return_OkResult()
    {
        //Arrange
        var prodId = 14;

        //Act
        var data = await _controller.DeleteAsync(prodId);

        //Assert
        data.Result.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task DeleteProdutoById_Return_NotFound()
    {
        //Arrange
        var prodId = 13;

        //Act
        var data = await _controller.DeleteAsync(prodId);

        //Assert
        data.Result.Should().BeOfType<NotFoundObjectResult>()
            .Which.StatusCode.Should().Be(404);
    }
}
