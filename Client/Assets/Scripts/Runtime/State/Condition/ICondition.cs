public interface ICondition
{

}
public interface ICondition<TReturn> : ICondition
{
    public TReturn GetCondition();
}

public interface ICondition<TReturn, TTarget> : ICondition<TReturn>
{
    public TReturn GetCondition();

}