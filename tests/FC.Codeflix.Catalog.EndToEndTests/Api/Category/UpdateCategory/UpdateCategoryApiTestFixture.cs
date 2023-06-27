using FC.Codeflix.Catalog.Application.UseCases.Category.UpdateCategory;
using FC.Codeflix.Catalog.EndToEndTests.Api.Category.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FC.Codeflix.Catalog.EndToEndTests.Api.Category.UpdateCategory
{
    [CollectionDefinition(nameof(UpdateCategoryApiTestFixture))]
    public class UpdateCategoryApiTestCollection : ICollectionFixture<UpdateCategoryApiTestFixture>
    {
    }

    public class UpdateCategoryApiTestFixture : CategoryBaseFixture
    {
        public UpdateCategoryInput GetExampleInput(Guid? id = null) => new UpdateCategoryInput(
                    id ?? Guid.NewGuid(),
                    GetValidCategoryName(),
                    GetValidCategoryDescription(),
                    getRandomBoolean()
                );
    }
}
