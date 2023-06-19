namespace FC.Codeflix.Catalog.EndToEndTests.Api.Category.CreateCategory;

public class CreateCategoryApiTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs()
    {
        var fixture = new CreateCategoryApiTestFixture();
        var invalidInputsList = new List<object[]>();
        var totalInvalidCases = 3;

        for (int index = 0; index < totalInvalidCases; index++)
        {
            var input = fixture.GetExempleInput();
            switch (index % totalInvalidCases)
            {
                case 0:
                    input.Name = fixture.GetInvalidNameTooShort();
                    invalidInputsList.Add(new object[] {
                        input,
                        "Name should be at least 3 characters long"
                    });
                    break;
                case 1:
                    input.Name = fixture.GetInvalidNameTooLong();
                    invalidInputsList.Add(new object[] {
                        input,
                        "Name should be less or equal 255 characters long"
                    });
                    break;
                case 2:
                    input.Description = fixture.GetInvalidDescriptionTooLongDescription();
                    invalidInputsList.Add(new object[] {
                        input,
                        "Description should be less or equal 10000 characters long"
                    });
                    break;
                default:
                    break;
            }
        }

        return invalidInputsList;
    }
}