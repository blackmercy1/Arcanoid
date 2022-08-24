using Asteroids.AsteroidsGenerator.Pool;
using Asteroids.Stats;
using UnityEngine;

namespace Asteroids.AsteroidsGenerator.Builder.Configs
{
    [CreateAssetMenu(menuName = "UfoConfig")]
    public sealed class UfoConfig : ScriptableObject, IUfoStatsProvider
    {
        [SerializeField] private UfoStats _ufoStats;
        [SerializeField] private Ufo _ufoPrefab;

        public Ufo Ufo => _ufoPrefab;
        public UfoStats Stats => _ufoStats;
    }
}