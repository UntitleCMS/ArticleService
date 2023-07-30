using Xunit;
using Moq;
using Application.Common.Interfaces.Repositoris;
using Domain.Entity;
using Application.Common.Extentions;

namespace Application.Posts.Query.Tests
{
    public class GetPostQueryHandelerTests
    {
        [Fact]
        public void HandleTest()
        {
            // Arrange
            var post = new Post()
            {
                ID = Guid.NewGuid(),
                Author = Guid.NewGuid(),
                Title = "Test",
            };

            var mockService = new Mock<IRepository<Post,Guid>>();
            mockService.Setup(service => service.Find(It.IsAny<Guid>()))
                .Returns(post);

            var requestMock = new Mock<GetPostDetailQuery>();
            requestMock.Setup(req => req.Id)
                .Returns(Guid.Empty.ToBase64Url());

            var classToTest = new GetPostDetailQueryHandeler(mockService.Object);

            // Act
            var result = classToTest.Handle(requestMock.Object, default).Result;

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(post.ID, result.Data!.Id.ToGuid());
            Assert.Equal(post.Author, result.Data!.AuthorId.ToGuid());
            Assert.Equal(post.Title, result.Data.Title);
        }
    }
}