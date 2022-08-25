using Asteroids.Movement.Direction;
using MainPlayer;
using UnityEngine;

namespace Ufos.Movement.Direction
{
    public sealed class UfoDirectionProvider : IDirectionProvider
    {
        private Vector2 _direction;

        public UfoDirectionProvider(Player player)
        {
            player.PlayerChangedPosition += ChangeDirection;
        }

        private void ChangeDirection(Vector2 position)
        {
            _direction = position;
        }

        public Vector2 GetDirection()
        {
            return _direction;
        }
    }
}