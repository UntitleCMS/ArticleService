using Application.Common.Interfaces;
using Application.Common.Mediator;
using Domain.Entites;

namespace Application.Events.NewArticle;

public record NewArticleEvent(PostEntity post) : IRequestWraped<string>;
