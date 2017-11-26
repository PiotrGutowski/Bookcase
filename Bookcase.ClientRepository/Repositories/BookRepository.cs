using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Bookcase.ClientRepository.Models;
using Bookcase.ClientRepository.IRepositories;
using Newtonsoft.Json;
using System.Net;

namespace Bookcase.ClientRepository.Repositories
{
    public class BookRepository : IBookRepository
    {

        public async Task<IEnumerable<Book>> GetAllBookAsync()
        {
            IEnumerable<Book> book = null;
            using (var _client = new HttpClient())
            {
                _client.BaseAddress = new Uri("http://localhost:49869/api/");
                var responseTask = await _client.GetAsync("book");

                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<IEnumerable<Book>>();
                    book = readTask.Result;
                }
                else if (responseTask.StatusCode == HttpStatusCode.NotFound)
                {
                    return Enumerable.Empty<Book>();
                }
                else
                {
                    throw new Exception($"{responseTask.StatusCode}");
                }
            }
            return book;
        }

        public async Task<IEnumerable<Book>> GetBookByNameAsync(string name)
        {
            IEnumerable<Book> book = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49869/api/");

                var responseTask = await client.GetAsync("book/" + name);
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<IEnumerable<Book>>();
                    book = readTask.Result;
                }
                else if (responseTask.StatusCode == HttpStatusCode.NotFound)
                {
                    return Enumerable.Empty<Book>();
                }
                else
                {
                    throw new Exception($"{responseTask.StatusCode}");
                }
            }
            return book;
        }

        public async Task AddAsync(Book book)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49869/api/book");
                var response = await client.PostAsJsonAsync<Book>("book", book);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{response.StatusCode}");

                }
            }
        }

        public async Task<Book> GetBookByIdAsync(Guid? id)
        {
            Book _book = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49869/api/");

                HttpResponseMessage response = await client.GetAsync("book?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var books = JsonConvert.DeserializeObject<List<Book>>(data);
                    foreach (Book book in books)
                        if (book.BookId == id)
                            _book = book;
                }
                else
                {
                    throw new Exception($"{response.StatusCode}");
                }
            }
            return _book;
        }

        public async Task EditAsync(Book book)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:49869/api/book");
                var response = await client.PutAsJsonAsync("Book/" + book.BookId, book);
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
                var response = await client.DeleteAsync("book/" + id.ToString());
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{response.StatusCode}");

                }

            }
        }
    }
}
