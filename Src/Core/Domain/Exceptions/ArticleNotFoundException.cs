namespace Domain.Exceptions;

public class ArticleNotFoundException : Exception
{
    public ArticleNotFoundException() : base() { }
    public ArticleNotFoundException(string msg) : base(msg) { }
    public ArticleNotFoundException(string msg, Exception inner) : base(msg, inner) { }
}
