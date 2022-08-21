using System;
using UpdatesSystem;

namespace Common
{
    public class StopWatch : IClean, IUpdate
    {
        public event Action<IUpdate> UpdateRemoveRequested;
    
        public float PassedTimeInSeconds { get; private set; }
    
        public void GameUpdate(float deltaTime)
        {
            PassedTimeInSeconds += deltaTime;
        }

        public void Clean()
        {
            UpdateRemoveRequested?.Invoke(this);
        }
    }
}