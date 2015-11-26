using Horizon.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Horizon.Web.Identity
{
    public class CustomUserStore : IUserRoleStore<DummyUser>, IUserStore<DummyUser>, IUserPasswordStore<DummyUser>, IDisposable
    {
        private Horizon.Web.DAL.HorizonContext db = new DAL.HorizonContext();

        public System.Threading.Tasks.Task<DummyUser> FindByNameAsync(string userName)
        {
            DummyUser user = null;
            var result = db.User.Where(item => item.Name == userName).FirstOrDefault();

            if (result != null)
            {
                user = new DummyUser { UserName = result.Name, Email = result.Email, Id = result.Id.ToString(), PasswordHash = result.Password };
            }


            return Task.FromResult<DummyUser>(user);
        }

        public System.Threading.Tasks.Task CreateAsync(DummyUser user)
        {
            Role default_role = db.Role.Where(Role => Role.Name.Equals("Guest")).FirstOrDefault();

            var result = db.User.Add(new User
            {
                Name = user.UserName,
                Email = user.Email,
                Roles = new List<Role> { default_role },
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                IsActive = false,
                IsLocked = false,
                Password = user.PasswordHash,
                UserLogin = null,
                Profile = new Profile { Id = Guid.NewGuid() }
            });

            db.SaveChanges();

            return Task.FromResult<bool>(result != null);
        }
        public Task<string> GetPasswordHashAsync(DummyUser user)
        {
            return Task.FromResult<string>(user.PasswordHash.ToString());
        }
        public Task SetPasswordHashAsync(DummyUser user, string passwordHash)
        {
            return Task.FromResult<string>(user.PasswordHash = passwordHash);
        }

        #region Not implemented methods
        public System.Threading.Tasks.Task DeleteAsync(DummyUser user)
        {
            throw new NotImplementedException();
        }

        public string GetUserIdByName(string username)
        {
            var result = db.User.Where(m => m.Name.ToString().Equals(username)).FirstOrDefault().Id;
            return result.ToString();
        }

        public System.Threading.Tasks.Task<DummyUser> FindByIdAsync(string userId)
        {
            var result = db.User.Where(m => m.Id.ToString().Equals(userId)).FirstOrDefault();
            DummyUser user = new DummyUser
            {
                Id = result.Id.ToString(),
                PasswordHash = result.Password,
                Email = result.Email,
                UserName = result.Name
            };

            return Task.FromResult<DummyUser>(user);
        }
        public System.Threading.Tasks.Task UpdateAsync(DummyUser user)
        {
            throw new NotImplementedException();
        }
        public Task<bool> HasPasswordAsync(DummyUser user)
        {
            throw new NotImplementedException();
        }
        
        ~CustomUserStore()
        {
            Dispose(false);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
            }
        }


        #endregion

        public Task AddToRoleAsync(DummyUser user, string roleName)
        {
            throw new NotImplementedException();
        }
        

        public Task<IList<string>> GetRolesAsync(DummyUser user)
        {
            List<string> result;
            if ( user != null)
            {
                result = new List<string>();

                foreach ( var role in db.User.Where(u=> u.Id.ToString() == user.Id).SingleOrDefault().Roles)
                {
                    result.Add(role.Name);
                }

                return Task.FromResult<IList<string>>(result);
            }
            else
            {
                return null;
            }
        }

        public IList<string> GetRoles(DummyUser user)
        {
            List<string> result;
            if (user != null)
            {
                result = new List<string>();

                foreach (var role in db.User.Where(u => u.Id.ToString() == user.Id).SingleOrDefault().Roles)
                {
                    result.Add(role.Name);
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public Task<bool> IsInRoleAsync(DummyUser user, string roleName)
        {
            return Task.FromResult<bool>(db.User.Where(u => u.Roles.Where(r => r.Name.Equals(roleName)).SingleOrDefault() != null).ToList().Count() > 0);
        }

        public Task RemoveFromRoleAsync(DummyUser user, string roleName)
        {
            throw new NotImplementedException();
        }
    }
}