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
    public class AuthorRepository : IAuthorRepository
    {
        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            IEnumerable<Author> author = null;
            using (var _client = new HttpClient())
            {
                _client.BaseAddress = new Uri("http://localhost:49869/api/");

                var responseTask = await _client.GetAsync("author");
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<IEnumerable<Author>>();
                    author = readTask.Result;
                }
                else if (responseTask.StatusCode == HttpStatusCode.NotFound)
                {
                    return Enumerable.Empty<Author>();
                } 
                else
                {
                    throw new HttpException($"{responseTask.StatusCode}");
                }
            }
            return author;
        }

        public async Task<IEnumerable<Author>> GetAuthorByNameAsync(string name)
        {
            IEnumerable<Author> author = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49869/api/");

                var responseTask = await client.GetAsync("author/" + name);
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<IEnumerable<Author>>();
                    author = readTask.Result;
                }
                else if (responseTask.StatusCode == HttpStatusCode.NotFound)
                {
                    return Enumerable.Empty<Author>();
                }
                else
                {
                    
                    throw new HttpException($"{responseTask.StatusCode}");
                }
            }
            return author;
        }

        public async Task<Author> GetAuthorByIdAsync(Guid? id)
        {
            Author _author = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49869/api/");

                HttpResponseMessage response = await client.GetAsync("author?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var authors = JsonConvert.DeserializeObject<List<Author>>(data);
                    foreach (Author author in authors)
                        if (author.AuthorId == id)
                            _author = author;
                }
                else
                {
                    throw new Exception($"{response.StatusCode}");
                }
            }
            return _author;
        }

        public async Task AddAsync(Author author)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49869/api/");
                var response = await client.PostAsJsonAsync<Author>("author", author);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{response.StatusCode}");

                }
            }
        }

        public async Task EditAsync(Author author)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:49869/api/author");
                var response = await client.PutAsJsonAsync("author/" + author.AuthorId, author);

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
                var response = await client.DeleteAsync("author/" + id.ToString());
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{response.StatusCode}");
                }
            }
        }
    }
}
