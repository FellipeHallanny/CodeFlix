using Bogus;

namespace FC.Codeflix.Catalog.IntegrationTests.Base;

public class BaseFixture
{
    public Faker Faker { get; set; }

    protected BaseFixture()
        => Faker = new Faker("pt_BR");
}