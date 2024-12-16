public interface ISerializer
{
    public void Serialize();
}

public interface ISerializer<T>
{
    public void Serialize(T param);
}