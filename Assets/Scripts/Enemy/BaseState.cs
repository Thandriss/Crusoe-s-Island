public abstract class BaseState
{
    //
    //
    public Enemy enemy;
    public StateMachine state;

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}