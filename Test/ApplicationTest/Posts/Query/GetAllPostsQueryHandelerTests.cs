using Xunit;
using Application.Posts.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Application.Common.Interfaces.Repositoris;
using Domain.Entity;

namespace Application.Posts.Query.Tests
{
    public class GetAllPostsQueryHandelerTests
    {
        [Fact()]
        public void HandleTest()
        {
            // arrange
            var bfPosts = new List<Post>()
            {
                new Post { Title = "1" },
                new Post { Title = "2" },
                new Post { Title = "3" },
                new Post { Title = "4" },
                new Post { Title = "5" },
                new Post { Title = "6" },
            };

            var afPosts = new List<Post>()
            {
                new Post { Title = "1" },
                new Post { Title = "2" },
                new Post { Title = "3" },
                new Post { Title = "5" },
            };
            var mockRepositoryPageable = new Mock<IRepositoryPageable<Post, Guid>>();
             mockRepositoryPageable
                .Setup(r => r.GetBefore( It.IsAny<int>(), It.IsAny<Guid>() ) )
                .Returns(bfPosts);
             mockRepositoryPageable
                .Setup(r => r.GetAfter( It.IsAny<int>(), It.IsAny<Guid>() ) )
                .Returns(afPosts);

            var classToTest = new GetAllPostsQueryHandeler(mockRepositoryPageable.Object);

            var mockRequest = new Mock<GetAllPostsQuery>();
            mockRequest
                .Setup(r => r.Take)
                .Returns(20);
            mockRequest
                .Setup(r => r.RefPostId)
                .Returns(Guid.Empty);

            // act
            var result = classToTest.Handle(mockRequest.Object, default).Result!;

            // assert
            Assert.True(result.IsSuccess );
            Assert.Equal(bfPosts.Count(), result.Data!.Count());
            

        }
    }
}