using GenericRepository;
using NewsLetter.Domain.Entities;
using NewsLetter.Domain.Repositories;
using NewsLetter.Persistance.Context;

namespace NewsLetter.Persistance.Repositories;
internal sealed class SubscribeRepository : Repository<Subscribe, AppDbContext>, ISubscribeRepository
{
    public SubscribeRepository(AppDbContext context) : base(context) {}
}
