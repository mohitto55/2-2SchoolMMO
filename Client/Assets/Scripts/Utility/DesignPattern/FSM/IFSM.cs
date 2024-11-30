public interface IFSM
{

}
public interface IFSM<T> : IFSM
{
    public void Enter(T target);
    public void Idle(T target);
    public void FixedIdle(T target);
    public void Exit(T target);
}