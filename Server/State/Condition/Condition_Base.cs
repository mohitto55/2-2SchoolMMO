
public abstract class Condition_Base<TReturn, TTarget> : ICondition<TReturn, TTarget>
{
    protected TTarget _target;

    public abstract TReturn GetCondition();

    public Condition_Base(TTarget target)
    {
        _target = target;
    }
}