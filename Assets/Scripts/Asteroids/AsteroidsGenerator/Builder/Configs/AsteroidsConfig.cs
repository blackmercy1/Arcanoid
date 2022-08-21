using Asteroids.Stats;
using Asteroids.Stats.Decorators;
using UnityEngine;

namespace Asteroids.AsteroidsGenerator.Builder.Configs
{
    [CreateAssetMenu(menuName = "AsteroidsConfig")]
    public sealed class AsteroidsConfig : ScriptableObject, IAsteroidsStatsProvider
    {
        [SerializeField] private AsteroidsStats _ballStats;
        [SerializeField] private Asteroid _prefab;

        public AsteroidsStats Stats => _ballStats;
        public Asteroid Prefab => _prefab;
    }
}