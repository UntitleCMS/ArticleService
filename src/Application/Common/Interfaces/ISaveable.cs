namespace Application.Common.Interfaces;

public interface ISaveable
{
    void SaveChanges();
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
