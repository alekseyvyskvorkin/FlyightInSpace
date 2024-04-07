public interface IPoolable : ISpawnable
{
    public PoolContainer PoolManager { get; set; }
}
