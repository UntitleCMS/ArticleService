namespace Application.Common.Repositories;

public interface IArticlesNameRepository
{
    IList<KeyValuePair<Guid, string>> Top(int n = 10);
}
