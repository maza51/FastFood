using FastFood.Application.Interfaces.Repository;
using FastFood.Domain.Entities;
using FastFood.Infrastructure.EntityFramework.Repositories.Base;

namespace FastFood.Infrastructure.EntityFramework.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}