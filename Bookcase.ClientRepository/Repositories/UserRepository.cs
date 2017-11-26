using Bookcase.ClientRepository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookcase.ClientRepository.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.Web;

namespace Bookcase.ClientRepository.Repositories
{
    public class UserRepository : IUserRepository
    {

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            IEnumerable<User> user = null;
            using (var _client = new HttpClient())
            {
                _client.BaseAddress = new Uri("http://localhost:49869/api/");

                var responseTask = await _client.GetAsync("user");
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<IEnumerable<User>>();
                    user = readTask.Result;
                }
                else if (responseTask.StatusCode == HttpStatusCode.NotFound)
                {
                    return Enumerable.Empty<User>();
                }
                else
                {
                    throw new Exception($"{responseTask.StatusCode}");
                }
            }
            return user;
        }

        public async Task<IEnumerable<User>> GetUserByNameAsync(string name)
        {
            IEnumerable<User> user = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49869/api/");

                var responseTask = await client.GetAsync("user/" + name);
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<IEnumerable<User>>();
                    user = readTask.Result;
                }
                else if (responseTask.StatusCode == HttpStatusCode.NotFound)
                {
                    return Enumerable.Empty<User>();
                }
                else
                {
                    throw new Exception($"{responseTask.StatusCode}");
                }
            }
            return user;
        }

        public async Task<User> GetUserByIdAsync(Guid? id)
        {
            User _user = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49869/api/");

                HttpResponseMessage response = await client.GetAsync("user?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var users = JsonConvert.DeserializeObject<List<User>>(data);
                    foreach (User user in users)
                        if (user.UserId == id)
                            _user = user;
                }
                else
                {
                    throw new Exception($"{response.StatusCode}");
                }
            }
            return _user;
        }

        public async Task AddUserAsync(User user)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:49869/api/user");
                var response = await client.PostAsJsonAsync<User>("user", user);
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpException($"{response.StatusCode}");

                }
              
            }
        }

        public async Task EditAsync(User user)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:49869/api/user");
                var response = await client.PutAsJsonAsync("user/" + user.UserId, user);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{response.StatusCode}");

                }
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:49869/api/");
                var response = await client.DeleteAsync("user/" + id.ToString());
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{response.StatusCode}");

                }
            }
        }
    }
}


