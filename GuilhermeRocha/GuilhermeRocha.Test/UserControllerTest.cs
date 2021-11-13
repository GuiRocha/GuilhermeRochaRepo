using Xunit;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using GuilhermeRocha.Infrastructure;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using GuilhermeRocha.Infrastructure.Entities;
using System.Text.Json;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace GuilhermeRocha.Test
{
    [TestCaseOrderer("GuilhermeRocha.Test.PriorityOrderer", "GuilhermeRocha.Test")]
    public class UserControllerTest
    {
        private readonly HttpClient _client;

        public UserControllerTest()
        {
            var server = new TestServer(
                            new WebHostBuilder()
                                .UseStartup<Startup>());

            _client = server.CreateClient();
        }

        // InsertUser_True
        [Fact, TestPriority(1)]
        public async Task Test_1()
        {
            //Arranje
            var userToInsert = new User()
            {
                FirstName = "FirstName",
                Surname = "Surname",
                Email = "email@email.com",
                Password = "Password"
            };

            var json = new StringContent(
                            System.Text.Json.JsonSerializer.Serialize(userToInsert),
                            Encoding.UTF8,
                            Application.Json);

            // Act
            var response = await _client.PostAsync("/api/Users/createuser", json);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("true", response.Content.ReadAsStringAsync().Result);
        }

        // InsertUser_False
        [Fact, TestPriority(2)]
        public async Task Test_2()
        {
            //Arranje
            var userToInsert = new User()
            {
                FirstName = "FirstName",
                Surname = "Surname",
                Email = "email@email.com",
                Password = "Password"
            };

            var json = new StringContent(
                            System.Text.Json.JsonSerializer.Serialize(userToInsert),
                            Encoding.UTF8,
                            Application.Json);

            // Act
            var response = await _client.PostAsync("/api/Users/createuser", json);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("false", response.Content.ReadAsStringAsync().Result);
        }

        // ListAllUsers
        [Fact, TestPriority(3)]
        public async Task Test_3()
        {
            // Act
            var response = await _client.GetAsync("/api/Users/getusers");

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // UserByEmail
        [Fact, TestPriority(4)]
        public async Task Test_4()
        {
            // Act
            var response = await _client.GetAsync("/api/Users/getuserbyemail/email@email.com");

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        // UpdateUser_True
        [Fact, TestPriority(5)]
        public async Task Test_5()
        {
            //Arranje
            var responseGetUser = await _client.GetAsync("/api/Users/getusers");
            responseGetUser.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, responseGetUser.StatusCode);

            User userFound = JsonConvert.DeserializeObject<List<User>>(responseGetUser.Content.ReadAsStringAsync().Result).First();

            var userToUpdate = new User()
            {
                Id = userFound.Id,
                FirstName = "Guilherme",
                Surname = "Rocha",
                Email = "email@email.com",
                Password = "Password"
            };

            var json = new StringContent(
                            System.Text.Json.JsonSerializer.Serialize(userToUpdate),
                            Encoding.UTF8,
                            Application.Json);

            // Act
            var response = await _client.PutAsync("/api/Users/updateuser", json);

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("true", response.Content.ReadAsStringAsync().Result);
        }

        // UpdateUser_False
        [Fact, TestPriority(6)]
        public async Task Test_6()
        {
            User userToUpdate = null;

            var json = new StringContent(
                            System.Text.Json.JsonSerializer.Serialize(userToUpdate),
                            Encoding.UTF8,
                            Application.Json);

            // Act
            var response = await _client.PutAsync("/api/Users/updateuser", json);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        // DeleteUserByEmail
        [Fact, TestPriority(7)]
        public async Task Test_7()
        {
            // Act
            var response = await _client.DeleteAsync("/api/Users/deleteuserbyemail/email@email.com");

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("true", response.Content.ReadAsStringAsync().Result);
        }
    }
}
