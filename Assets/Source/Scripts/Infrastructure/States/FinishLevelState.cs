using System;
using System.Collections.Generic;

namespace Source.Infrastructure
{
    public class FinishLevelState : IPayloadState<IReadOnlyList<int>>, IPayloadState<string>
    {
        private GameStateMachine _gameStateMachine;
        private SceneLoader _sceneLoader;

        public FinishLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter(IReadOnlyList<int> payload)
        {
            throw new NotImplementedException();
        }

        public void Enter(string payload)
        {
          
        }

        public void Exit()
        {
            throw new NotImplementedException();
        }
    }
}