using FC.Codeflix.Catalog.Application.UseCases.Category.Common;
using FC.Codeflix.Catalog.Domain.Repository;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.ListCategories;

public class ListCategories : IListCategories
{
    private readonly ICategoryRepository _categoryRepository;
    
    public ListCategories(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public async Task<ListCategoriesOutput> Handle(ListCategoriesInput request, CancellationToken cancellationToken)
    {
        var searchOutput = await _categoryRepository.Search(
            new (
                page: request.Page,
                perPage: request.PerPage,
                search: request.Search,
                orderBy: request.Sort,
                order: request.Dir
            ),
            cancellationToken
        );
        
        return new ListCategoriesOutput(
            page: searchOutput.CurrentPage,
            perPage: searchOutput.PerPage,
            total: searchOutput.Total,
            items: searchOutput.Items.Select(CategoryModelOutput.FromCategory).ToList() //Transforming the list of Category to CategoryModelOutput
        );
    }
}