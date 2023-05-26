using FluentAssertions;
using Moq;
using Xunit;
using UseCases = FC.Codeflix.Catalog.Application.UseCases.Category.GetCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.GetCategory;

[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryTest
{
    private readonly GetCategoryTestFixture _fixture;

    public GetCategoryTest(GetCategoryTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = "")]
    [Trait("Application", "GetCategory - Use Cases")]
    public async Task GetCategory()
    {
        var repositoryMock = _fixture.GetCategoryRepository();
        var exampleCategory = _fixture.GetValidCategory();
        repositoryMock.Setup(x => x.Get(
            It.IsAny<Guid>(),It.IsAny<CancellationToken>())).ReturnsAsync(exampleCategory);
        
        var input = new UseCases.GetCategoryInput(exampleCategory.Id);
        var useCase = new UseCases.GetCategory(repositoryMock.Object);
        
        var output = await useCase.Handle(input, CancellationToken.None);
        
        repositoryMock.Verify(x => x.Get(
            It.IsAny<Guid>(),It.IsAny<CancellationToken>()), Times.Once());
        
        output.Should().NotBeNull();
        output.Name.Should().Be(exampleCategory.Name);
        output.Description.Should().Be(exampleCategory.Description);
        output.IsActive.Should().Be(exampleCategory.IsActive);
        output.Id.Should().Be(exampleCategory.Id);
        output.CreatedAt.Should().Be(exampleCategory.CreatedAt);
        
    }
}