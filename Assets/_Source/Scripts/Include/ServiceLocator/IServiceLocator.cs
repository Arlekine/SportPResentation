namespace SquidGameVR.App
{
    public interface IServiceLocator
    {
        public T GetService<T>();
    }
}