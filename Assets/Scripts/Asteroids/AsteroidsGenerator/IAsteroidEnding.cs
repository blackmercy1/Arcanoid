using System;
using UnityEngine;

namespace Asteroids.AsteroidsGenerator
{
    public interface IAsteroidEnding
    {
        event Action<Vector2> GetEndPosition;
    }
}