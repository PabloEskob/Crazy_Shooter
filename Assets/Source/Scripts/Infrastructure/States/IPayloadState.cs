using Source.Infrastructure.States;

namespace Source.Infrastructure
{
    public interface IPayloadState<TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }
}