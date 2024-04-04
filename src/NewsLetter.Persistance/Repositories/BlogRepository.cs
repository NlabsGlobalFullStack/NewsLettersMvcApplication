using GenericRepository;
using NewsLetter.Domain.Entities;
using NewsLetter.Domain.Repositories;
using NewsLetter.Persistance.Context;

namespace NewsLetter.Persistance.Repositories;
internal sealed class BlogRepository : Repository<Blog, AppDbContext>, IBlogRepository
{
    public BlogRepository(AppDbContext context) : base(context) {}
}
