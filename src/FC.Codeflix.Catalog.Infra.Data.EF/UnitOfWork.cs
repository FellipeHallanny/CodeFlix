using FC.Codeflix.Catalog.Application.Interfaces;

namespace FC.Codeflix.Catalog.Infra.Data.EF;

public class UnitOfWork : IUnitOfWork
{
    private readonly CodeflixCatalogDbContext _dbContext;
    
    public UnitOfWork(CodeflixCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task Commit(CancellationToken cancellationToken)
        => _dbContext.SaveChangesAsync(cancellationToken);

    public Task Rollback(CancellationToken cancellationToken)
        => Task.CompletedTask;
}