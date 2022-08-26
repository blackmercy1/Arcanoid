using Ufos.UfoGenerator.Stats;
using Ufos.UfoGenerator.Stats.Decorators;
using UnityEngine;

namespace Ufos.UfoGenerator.Builder.Configs
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