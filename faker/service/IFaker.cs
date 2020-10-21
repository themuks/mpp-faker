namespace faker.entity
{
    public interface IFaker
    {
        int MaxCircularDepth { get; set; }
        T Create<T>();
    }
}