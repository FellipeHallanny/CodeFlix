using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEndTests.Api.Category.DeleteCategory;

[Collection(nameof(DeleteCategoryApiTestFixture))]
public class DeleteCategoryApiTest
{
    private readonly DeleteCategoryApiTestFixture _fixture;
    
    public DeleteCategoryApiTest(DeleteCategoryApiTestFixture fixture)
    {
        _fixture = fixture;
    }
    
    [Fact(DisplayName = nameof(DeleteCategory))]
    [Trait("EndToEnd/API ", "Category/Delete - Endpoints")]
    public async Task DeleteCategory()
    {
        var exampleCategoryList = _fixture.GetExampleCategoriesList(20);
        await _fixture.Persistence.InsertList(exampleCategoryList);
        var exampleCategory = exampleCategoryList[10];
        
        var (response, output) = await _fixture.ApiClient.Delete<object>($"/Categories/{exampleCategory.Id}");
        
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status204NoContent);
        output.Should().BeNull();

        var persistenceCategory = await _fixture.Persistence.GetById(exampleCategory.Id);
        persistenceCategory.Should().BeNull();
    }
    
    [Fact(DisplayName = nameof(ErrorWhenNotFound))]
    [Trait("EndToEnd/API ", "Category/Delete - Endpoints")]
    public async Task ErrorWhenNotFound()
    {
        var exampleCategoryList = _fixture.GetExampleCategoriesList(20);
        await _fixture.Persistence.InsertList(exampleCategoryList);
        var randomGuid = Guid.NewGuid();
        
        var (response, output) = await _fixture.ApiClient.Delete<ProblemDetails>($"/Categories/{randomGuid}");
        
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status404NotFound);
        output.Should().NotBeNull();
        output!.Status.Should().Be(StatusCodes.Status404NotFound);
        output.Title.Should().Be("Not Found.");
        output.Detail.Should().Be($"Category '{randomGuid}' not found."); 
        output.Type.Should().Be("NotFound");
    }
}