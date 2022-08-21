using System;

namespace UpdatesSystem
{
    public interface IFixedUpdate
    {
        void FixedGameUpdate(float fixedDeltaTime);
        
        event Action<IFixedUpdate> UpdateFixedRemoveRequested;
    }
}