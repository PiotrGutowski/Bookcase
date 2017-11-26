using Bookcase.ClientRepository.IRepositories;
using Bookcase.ClientRepository.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bookcase.ClientRepository.Repositories
{
    public class BorrowedBooksRepository : IBorrowedBooksRepository
    {
        public async Task<IEnumerable<BorrowedBooks>> GetAllBorrowedBooksAsync()
        {
            IEnumerable<BorrowedBooks> borrowedBooks = null;
            using (var _client = new HttpClient())
            {
                _client.BaseAddress = new Uri("http://localhost:49869/api/");

                var responseTask = await _client.GetAsync("borrowedBooks");
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<IEnumerable<BorrowedBooks>>();
                    borrowedBooks = readTask.Result;
                }
                else if (responseTask.StatusCode == HttpStatusCode.NotFound)
                {
                    return Enumerable.Empty<BorrowedBooks>();
                }
                else
                {
                    throw new Exception($"{responseTask.StatusCode}");
                }
            }
            return borrowedBooks;
        }

        public async Task<IEnumerable<BorrowedBooks>> GetBorrowedBooksByNameAsync(string name)
        {
            IEnumerable<BorrowedBooks> borrowedBooks = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49869/api/");

                var responseTask = await client.GetAsync("borrowedBooks/" + name);
                if (responseTask.IsSuccessStatusCode)
                {
                    var readTask = responseTask.Content.ReadAsAsync<IEnumerable<BorrowedBooks>>();
                    borrowedBooks = readTask.Result;
                }
                else if (responseTask.StatusCode == HttpStatusCode.NotFound)
                {
                    return Enumerable.Empty<BorrowedBooks>();
                }
                else
                {
                    throw new Exception($"{responseTask.StatusCode}");
                }
            }
            return borrowedBooks;
        }

        public async Task AddAsync(BorrowedBooks borrowedBooks)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:49869/api/");
                var response = await client.PostAsJsonAsync<BorrowedBooks>("BorrowedBooks", borrowedBooks);
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{response.StatusCode}");

                }
            }
        }

        public async Task<BorrowedBooks> GetBorrowedBooksByIdAsync(Guid? id)
        {
            BorrowedBooks _borrowedBooks = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49869/api/");

                HttpResponseMessage response = await client.GetAsync("borrowedBooks?id=" + id);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var AllBorrowedBookss = JsonConvert.DeserializeObject<List<BorrowedBooks>>(data);
                    foreach (BorrowedBooks borrowedBooks in AllBorrowedBookss)
                        if (borrowedBooks.BorrowedBooksId == id)
                            _borrowedBooks = borrowedBooks;
                }
                else
                {
                    throw new Exception($"{response.StatusCode}");
                }
            }
            return _borrowedBooks;
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("http://localhost:49869/api/");
                var response = await client.DeleteAsync("borrowedBooks/" + id.ToString());
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{response.StatusCode}");

                }
            }
        }
    }
}
