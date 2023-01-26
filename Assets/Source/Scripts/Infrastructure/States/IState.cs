namespace Source.Infrastructure.States
{
    public interface IState : IExitableState
    {
        public void Enter();
        public void Enter(int level);
    }
}