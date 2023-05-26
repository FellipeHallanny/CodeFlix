using System;
using FC.Codeflix.Catalog.Application.Interfaces;
using FC.Codeflix.Catalog.Domain.Repository;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codeflix.Catalog.Application.UseCases.Category.CreateCategory
{
    public class CreateCategory : ICreateCategory
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _unitofWork = unitOfWork;
            _categoryRepository = categoryRepository;
        }

        public async Task<CreateCategoryOutput> Handle(CreateCategoryInput input, CancellationToken cancellationToken)
        {
            var category = new DomainEntity.Category(input.Name,input.Description, input.IsActive);

            await _categoryRepository.Insert(category, cancellationToken); ;
            await _unitofWork.Commit(cancellationToken);

            return CreateCategoryOutput.FromCategory(category);

        }
    }
}

