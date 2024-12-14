public interface IFactory<T>
{
    public T Create();
}
public interface IFactory<TItem, TPram>
{
    public TItem Create(TPram param);
}