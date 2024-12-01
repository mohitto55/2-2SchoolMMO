using System.Collections.Generic;

public abstract class State_Base<TTarget> : IState<TTarget>
{
    List<ITransition> _transitions = new List<ITransition>();

    public virtual void Enter(TTarget target)
    {
    }

    public virtual void Exit(TTarget target)
    {
    }

    public virtual void FixedIdle(TTarget target)
    {
    }

    public virtual void Idle(TTarget target)
    {
        TryTransition();
    }
    private void TryTransition()
    {
        foreach (var transition in _transitions)
        {
            transition.TryTransition();
        }
    }
    public State_Base<TTarget> AddTransition(ITransition transition)
    {
        _transitions.Add(transition);

        return this;
    }
}
