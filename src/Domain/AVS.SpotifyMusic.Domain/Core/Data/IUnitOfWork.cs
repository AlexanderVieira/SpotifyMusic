namespace AVS.SpotifyMusic.Domain.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}