using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Bookcase.Core.Domain;
using Bookcase.Core.Repositories;
using Bookcase.Infrastructure.BookcaseDbContext;
using System.Data;

namespace Bookcase.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookcaseContext _db;

        public UserRepository(BookcaseContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {

            var user = _db.User.AsNoTracking();
            return await Task.FromResult(user);

        }

        public async Task<User> GetAsync(Guid id)
        {

            return await Task.FromResult(_db.User.SingleOrDefault(x => x.UserId == id));


        }

        public async Task<User> GetByEmailAsync(string email)
        {

            return await Task.FromResult(_db.User.SingleOrDefault(x =>
                            x.Email == email));

        }

        public async Task<IEnumerable<User>> GetByNameAsync(string name = "")
        {


            var user = _db.User.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                user = user.Where(x => x.Name.ToLowerInvariant()
                    .Contains(name.ToLowerInvariant()));
            }
            return await Task.FromResult(user);

        }
        public async Task AddAsync(User user)
        {
            _db.User.Add(user);
            await _db.SaveChangesAsync();

        }

        public async Task UpdateAsync(User user)
        {
            _db.Entry(user).State = EntityState.Modified;
            await _db.SaveChangesAsync();

        }

        public async Task DeleteAsync(User user)
        {

            _db.User.Remove(user);
            await _db.SaveChangesAsync();
        }
    }
}
