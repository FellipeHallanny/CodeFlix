using System.Net;
using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using FC.Codeflix.Catalog.Application.UseCases.Category.GetCategory;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEndTests.Api.Category.GetCategoryById;

[Collection(nameof(GetCategoryApiTestFixture))]
public class GetCategoryApiTest
{
    private readonly GetCategoryApiTestFixture _fixture;
    
    public GetCategoryApiTest(GetCategoryApiTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(GetCategory))]
    [Trait("EndToEnd/API ", "Category/Get - Endpoints")]
    public async Task GetCategory()
    {
        var exampleCategoryList = _fixture.GetExampleCategoriesList(20);
        await _fixture.Persistence.InsertList(exampleCategoryList);
        var exampleCategory = exampleCategoryList[10];
        
        var (response, output) = await _fixture.ApiClient.Get<CategoryModelOutput>($"/Categories/{exampleCategory.Id}");
        
        response.Should().NotBeNull();
        response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
        output.Should().NotBeNull();
        output!.Id.Should().Be(exampleCategory.Id);
        output.Name.Should().Be(exampleCategory.Name);
        output.Description.Should().Be(exampleCategory.Description);
        output.IsActive.Should().Be(exampleCategory.IsActive);
        output.CreatedAt.Should().BeSameDateAs(exampleCategory.CreatedAt);
    }
}