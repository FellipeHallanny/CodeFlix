using FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;

namespace FC.Codeflix.Catalog.UnitTests.Application.UpdateCategory;

public class UpdateCategoryTestDataGenerator
{
    public static IEnumerable<Object[]> GetCategoriesToUpdate(int time = 10)
    {
        var fixture = new UpdateCategoryTestFixture();
        for(var indice = 0; indice < time; indice++)
        {
            var exempleCategory = fixture.GetExampleCategory();
            var exempleInput = fixture.GetValidInput(exempleCategory.Id);
            
            yield return new object[] { exempleCategory, exempleInput };
        }
    }
}