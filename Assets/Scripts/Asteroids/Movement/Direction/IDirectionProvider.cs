using UnityEngine;

namespace Asteroids.Movement.Direction
{
    public interface IDirectionProvider
    {
        Vector2 GetDirection();
    }
}