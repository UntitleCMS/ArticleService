namespace Domain.Exceptions;

public class ArticleNotFoundException : Exception
{
    public ArticleNotFoundException() : base("Article Not Found.") { }
    public ArticleNotFoundException(string msg) : base(msg) { }
    public ArticleNotFoundException(string msg, Exception inner) : base(msg, inner) { }
}
