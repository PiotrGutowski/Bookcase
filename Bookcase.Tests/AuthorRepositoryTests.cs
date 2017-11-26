using Bookcase.Infrastructure.BookcaseDbContext;
using Bookcase.Infrastructure.Repositories;
using Bookcase.Core.Repositories;
using Effort;
using System.Threading.Tasks;
using Bookcase.Core.Domain;
using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Bookcase.Tests
{
    [TestClass]
    public class AuthorRepositoryTests
    {
        private BookcaseContext _context;
        private IAuthorRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            var connection = DbConnectionFactory.CreateTransient();
            _context = new BookcaseContext(connection);
            _context.Database.CreateIfNotExists();
            _repository = new AuthorRepository(_context);
        }

        [TestMethod]
        public async Task Insert_Author_ToDatabase()
        {

            //Arrange
            var author = new Author(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), "Adam", "Mickiewicz");

            // Act
            await _repository.AddAsync(author);

            // Assert
            _context.Author.Should().HaveCount(1);
            _context.Author.First().Should().NotBeNull();
            _context.Author.First().LastName.Should().Be("Mickiewicz");
        }

        [TestMethod]
        public async Task GetAuthor_With_Non_ExistingId_ReturnsNull()
        {
            // Arrange
            Guid nonExistingId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019a");

            // Act
            var author= await _repository.GetAuthorByIdAsync(nonExistingId);

            // Assert
            author.Should().BeNull();
        }

        [TestMethod]
        public async Task Browse_With_Name_ReturnsAuthor()
        {

            // Arrange
            var author = new Author(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), "Adam", "Mickiewicz");
            await _repository.AddAsync(author);
            author = new Author(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019a"), "Henryk", "Sienkiewicz");
            await _repository.AddAsync(author);
            // Act
            var retrievedAuthor = await _repository.BrowseAsync("Mi");

            // Assert
            retrievedAuthor.Select(c => c.LastName).Should().Equal("Mickiewicz");
            retrievedAuthor.Select(c => c.LastName).Should().NotEqual("Sienkiewicz");
        }

        [TestMethod]
        public async Task GetAll_ReturnsAuthor()
        {

            // Arrange
            var author = new Author(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), "Adam", "Mickiewicz");
            await _repository.AddAsync(author);
            author = new Author(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019a"), "Henryk", "Sienkiewicz");
            await _repository.AddAsync(author);
            // Act
            var retrievedAuthor = await _repository.GetAllAuthorAsync();
            // Assert
            retrievedAuthor.Should().NotBeEmpty().And.HaveCount(2);
            

        }
        [TestMethod]
        public async Task GetAuthor_With_ExistingId_ReturnsAuthor()
        {
            // Arrange
            var author = new Author(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), "Adam", "Mickiewicz");
            await _repository.AddAsync(author);

            // Act
            var retrievedAuthor = await _repository.GetAuthorByIdAsync(author.AuthorId);

            // Assert
            author.FirstName.Should().Be(retrievedAuthor.FirstName);
            author.LastName.Should().Be(retrievedAuthor.LastName);
            author.AuthorId.Should().Be(retrievedAuthor.AuthorId);
        }
        [TestMethod]
        public async Task GetAuthor_With_ExistingName_ReturnsAuthor()
        {
            // Arrange
            var author = new Author(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), "Adam", "Mickiewicz");
            await _repository.AddAsync(author);

            // Act
            var retrievedAuthor = await _repository.GetAuthorByNameAsync(author.LastName);

            // Assert
            author.FirstName.Should().Be(retrievedAuthor.FirstName);
            author.LastName.Should().Be(retrievedAuthor.LastName);
            author.AuthorId.Should().Be(retrievedAuthor.AuthorId);
        }

        [TestMethod]
        public async Task Edit_Author_InDatabase()
        {
            // Arrange
            var author = new Author(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), "Adam", "Mickiewicz");
            await _repository.AddAsync(author);

            // Act
            var retrievedAuthor = await _repository.GetAuthorByIdAsync(author.AuthorId);
            retrievedAuthor.SetFirstName("Henryk");
            retrievedAuthor.SetLastName("Sienkiewicz");
            await _repository.EditAsync(retrievedAuthor);
            // Assert
            author.FirstName.Should().Be(retrievedAuthor.FirstName);
            author.LastName.Should().Be(retrievedAuthor.LastName);
            author.AuthorId.Should().Be(retrievedAuthor.AuthorId);
        }

        [TestMethod]
        public async Task Delete_Author_InDatabase()
        {
            // Arrange
            var author = new Author(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), "Adam", "Mickiewicz");
            await _repository.AddAsync(author);

            // Act
            var retrievedAuthor = await _repository.GetAuthorByIdAsync(author.AuthorId);
            await _repository.DeleteAsync(retrievedAuthor);
            retrievedAuthor = await _repository.GetAuthorByIdAsync(author.AuthorId);
            // Assert
            retrievedAuthor.Should().BeNull();
        }


     
    }


    
}