using Asteroids.Stats;
using Asteroids.Stats.Decorators;
using UnityEngine;

namespace Asteroids.AsteroidsGenerator.Builder.Configs
{
    [CreateAssetMenu(menuName = "SmallAsteroidsConfig")]
    public sealed class SmallAsteroidsConfig : ScriptableObject, IAsteroidsStatsProvider
    {
        [SerializeField] private AsteroidsStats _ballStats;
        [SerializeField] private Asteroid _prefab;
        [SerializeField] private int _destroyedParts;

        public int DestroyedParts => _destroyedParts;
        public AsteroidsStats Stats => _ballStats;
        public Asteroid Prefab => _prefab;
    }
}