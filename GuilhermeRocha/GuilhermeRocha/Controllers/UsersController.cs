using GuilhermeRocha.API.BusinessLogic;
using GuilhermeRocha.Infrastructure;
using GuilhermeRocha.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuilhermeRocha.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly GuilhermeContext _guilhermeContext;

        public UsersController(GuilhermeContext guilhermeContext)
        {
            _guilhermeContext = guilhermeContext;
        }


        [HttpGet("getusers")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await UsersBO.GetAllUsers(_guilhermeContext);
        }

        [HttpGet("getuserbyid/{id}")]
        public async Task<User> Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            return await UsersBO.GetUserById(_guilhermeContext, id);
        }

        [HttpGet("getuserbyemail/{email}")]
        public async Task<User> Get(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return null;
            }

            return await UsersBO.GetUserByEmail(_guilhermeContext, email);
        }

        [HttpPost("createuser")]
        public async Task<bool> Post(User user)
        {
            if (user == null)
            {
                return false;
            }

            return await UsersBO.InsertUser(_guilhermeContext, user);
        }

        [HttpPut("updateuser")]
        public async Task<bool> Put(User user)
        {
            if (user == null)
            {
                return false;
            }

            return await UsersBO.UpdateUser(_guilhermeContext, user);
        }

        [HttpDelete("deleteuserbyid/{id}")]
        public async Task<bool> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }

            return await UsersBO.DeleteUserById(_guilhermeContext, id);
        }

        [HttpDelete("deleteuserbyemail/{email}")]
        public async Task<bool> Delete(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return false;
            }

            return await UsersBO.DeleteUserByEmail(_guilhermeContext, email);
        }
    }
}
