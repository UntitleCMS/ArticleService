namespace Application.Common.Repositories;

public interface ITagRepository
{ 
    Task<IList<KeyValuePair<string,int>>> GetTop(int n=10);
    Task<IList<string>> Serch(string tag, int n=10);
}

