using Application.Common.Repositories;
using Application.Features.Article.Command.AddArticle;
using Domain.Entites;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace ApplicationTest.Features.Posts.Command;

public class AddPostCommandHandlerTests
{
    public AddArticleCommandHandler ClassUnderTest;
    public Guid mockID;

    public AddPostCommandHandlerTests()
    {
        mockID = Guid.NewGuid();

        var mockPostRepo = new Mock<IPostRepository>();
        mockPostRepo.Setup(m => m.Add(ref It.Ref<PostEntity>.IsAny))
            .Callback((ref PostEntity post) => { post.ID = mockID; })
            .Returns(Task.CompletedTask);

        ClassUnderTest = new(mockPostRepo.Object);
    }

    public static IEnumerable<object[]> AddPostCommandData()
    {
        yield return new object[] { new AddArticleCommand()
        {
            Title = "Title",
            Sub = "mock"
        }};
    }

    [Theory]
    [MemberData(nameof(AddPostCommandData))]
    public async Task HandleTest(AddArticleCommand command)
    {
        var res = await ClassUnderTest.Handle(command, default);

        Assert.NotNull(res);
        Assert.NotNull(res.Data);
        Assert.True(res.IsSuccess);
        Assert.Equal(mockID.ToString(), res.Data.ToString());
    }

    [Fact]
    public async Task HandleNoSubTest()
    {
        AddArticleCommand command = new()
        {
            Sub = ""
        };

        var res = await ClassUnderTest.Handle(command, default);

        Assert.False(res.IsSuccess);
        Assert.NotNull(res.Error);
        Assert.Equal(typeof(ValidationException), res.Error.GetType());
    }

    [Fact]
    public async Task HandleNoTitleTest()
    {
        AddArticleCommand command = new()
        {
            Title = ""
        };

        var res = await ClassUnderTest.Handle(command, default);

        Assert.False(res.IsSuccess);
        Assert.NotNull(res.Error);
        Assert.Equal(typeof(ValidationException), res.Error.GetType());
    }

}
