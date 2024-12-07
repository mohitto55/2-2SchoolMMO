using System.Collections.Generic;

public interface IState<TTarget>
{
    public void Enter(TTarget target);
    public void Idle(TTarget target);
    public void FixedIdle(TTarget target);
    public void Exit(TTarget target);
}
