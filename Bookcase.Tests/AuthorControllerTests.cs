using Bookcase.Api.Controllers;
using Bookcase.Infrastructure.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Bookcase.Tests
{
    [TestClass]
   public class AuthorControllerTests
    {
      
        [TestMethod]
        public async Task Get_Should_Invoke_GetAllAuthorAsync_On_Author_Service()
        {
            //Arrange
            var authorServiceMock = new Mock<IAuthorService>();

            var authorController = new AuthorController(authorServiceMock.Object);

            //Act
            await authorController.Get();

            //Assert
            authorServiceMock.Verify(x => x.GetAllAuthorsAsync(), Times.Once());
        }
        
    }
}
