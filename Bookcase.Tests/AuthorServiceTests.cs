using AutoMapper;
using Bookcase.Core.Domain;
using Bookcase.Core.Repositories;
using Bookcase.Infrastructure.DTO;
using Bookcase.Infrastructure.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Bookcase.Tests
{
    [TestClass]
    public class AuthorServiceTests
    {
        [TestMethod]
        public async Task AddAuthorAsync_Should_Invoke_AddAsync_On_Author_Repository()
        {
            //Arrange
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var mapperMock = new Mock<IMapper>();
            var authorService = new AuthorService(authorRepositoryMock.Object, 
                mapperMock.Object);

            //Act
            await authorService.AddAuthorAsync(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), "Adam", "Mickiewicz");

            //Assert
            authorRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Author>()), Times.Once());
        }



        [TestMethod]
        public async Task GetAuthorByNameAsync_Should_Invoke_GetAuthorByNameAsync_On_Author_Repository()
        {
            // Arrange
            var author = new Author(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), "Adam", "Mickiewicz");
            var authorDto = new AuthorDto
            {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName

            };
            var authorRepositoryMock = new Mock<IAuthorRepository>(); ;
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<AuthorDto>(author)).Returns(authorDto);
            var authorService = new AuthorService(authorRepositoryMock.Object,
                mapperMock.Object);
           

            //Act
            var existingAccountDto = await authorService.GetAuthorByNameAsync(author.LastName);

            //Assert
            authorRepositoryMock.Verify(x => x.GetAuthorByNameAsync(author.LastName), Times.Once());
            authorDto.Should().NotBeNull();
            authorDto.LastName.ShouldBeEquivalentTo(author.LastName);
        }

        [TestMethod]
        public async Task EditAuthorAsync_Should_Invoke_EditAsync_On_Author_Repository()
        {
            // Arrange
            var author = new Author(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), "Adam", "Mickiewicz");
            var authorDto = new AuthorDto
            {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName

            };
            var authorRepositoryMock = new Mock<IAuthorRepository>(); ;
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<AuthorDto>(author)).Returns(authorDto);
            var authorService = new AuthorService(authorRepositoryMock.Object,
                mapperMock.Object);
            authorRepositoryMock.Setup(x => x.GetAuthorByIdAsync(author.AuthorId))
                .ReturnsAsync(author);

            //Act
            await authorService.EditAuthorAsync(author.AuthorId, "Henryk", "Sienkiewicz");

            //Assert
            authorRepositoryMock.Verify(x => x.EditAsync(It.IsAny<Author>()), Times.Once());

        }

        [TestMethod]
        public async Task DeleteAuthorAsync_Should_Invoke_GetAsync_On_Author_Repository()
        {
            // Arrange
            var author = new Author(new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D"), "Adam", "Mickiewicz");
            var authorDto = new AuthorDto
            {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName

            };
            var authorRepositoryMock = new Mock<IAuthorRepository>(); ;
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<AuthorDto>(author)).Returns(authorDto);
            var authorService = new AuthorService(authorRepositoryMock.Object,
                mapperMock.Object);
            
            //Act
             await authorService.DeleteAuthorAsync(author.AuthorId);

            //Assert
            authorRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Author>()), Times.Once());


        }
        [TestMethod]
        public async Task GetAllAuthorsAsync_Should_Invoke_GetAllAuthorAsync_On_Author_Repository()
        {
            //Arrange
            var authorRepositoryMock = new Mock<IAuthorRepository>();
            var mapperMock = new Mock<IMapper>();
            var authorService = new AuthorService(authorRepositoryMock.Object,
                mapperMock.Object);

            //Act
            await authorService.GetAllAuthorsAsync();

            //Assert
            authorRepositoryMock.Verify(x => x.GetAllAuthorAsync(), Times.Once());
        }
    }
}
