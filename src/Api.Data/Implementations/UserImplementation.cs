using System.Threading.Tasks;
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Implementations
{
    public class UserImplementation : BaseRepository<UserEntity>, IUserRepository
    {
        private DbSet<UserEntity> _dbSet;

        public UserImplementation(MyContext context): base (context)
        {
            _dbSet = context.Set<UserEntity>();
        }

        public async Task<UserEntity> FindByLogin(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

    }
}