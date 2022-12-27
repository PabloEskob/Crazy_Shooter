using System;
using System.Collections.Generic;

namespace Source.Infrastructure
{
    public class FinishLevelState : IPayloadState<IReadOnlyList<int>>
    {
        public void Enter(IReadOnlyList<int> payload)
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}