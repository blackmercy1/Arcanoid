using UnityEngine;

namespace Portals
{
    public interface ICollision
    {
        Vector2 GetPosition();
        void SetPosition(Vector2 position);
    }
}