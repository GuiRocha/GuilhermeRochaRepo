using GuilhermeRocha.Infrastructure;
using GuilhermeRocha.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuilhermeRocha.API.BusinessLogic
{
    public class UsersBO
    {
        public static async Task<IEnumerable<User>> GetAllUsers(GuilhermeContext guilhermeContext)
        {
            return await guilhermeContext.User.AsNoTracking().ToListAsync();
        }

        public static async Task<User> GetUserById(GuilhermeContext guilhermeContext, Guid id)
        {
            return await guilhermeContext.User.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public static async Task<User> GetUserByEmail(GuilhermeContext guilhermeContext, string email)
        {
            return await guilhermeContext.User.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
        }

        public static async Task<bool> InsertUser(GuilhermeContext guilhermeContext, User user)
        {
            try
            {
                if(await guilhermeContext.User.AsNoTracking().CountAsync(x => x.Email == user.Email) > 0)
                {
                    return false;
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                await guilhermeContext.User.AddAsync(user);
                await guilhermeContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<bool> UpdateUser(GuilhermeContext guilhermeContext, User user)
        {
            try
            {
                guilhermeContext.User.Update(user);
                await guilhermeContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<bool> DeleteUserById(GuilhermeContext guilhermeContext, Guid id)
        {
            try
            {
                var userToRemove = await guilhermeContext.User.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

                if (userToRemove == null)
                {
                    return false;
                }

                guilhermeContext.User.Remove(userToRemove);
                await guilhermeContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<bool> DeleteUserByEmail(GuilhermeContext guilhermeContext, string email)
        {
            try
            {
                var userToRemove = await guilhermeContext.User.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);

                if (userToRemove == null)
                {
                    return false;
                }

                guilhermeContext.User.Remove(userToRemove);
                await guilhermeContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
