namespace GD.Interfaces
{
    public interface IPoolable
    {
        void OnGetFromPool();
        void OnReturnToPool();
    }
}