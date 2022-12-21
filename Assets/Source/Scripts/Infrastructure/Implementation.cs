namespace Source.Infrastructure
{
    public class Implementation<TService> where TService : IService
    {
        public static TService ServiceInstance;
    }
}