using MainPlayer.PlayerSettings;
using UnityEngine;

namespace Installers
{
    public class UIInstaller : MonoBehaviour
    {
        [SerializeField] private StatisticsView _statisticsView;
        [SerializeField] private PlayerSettingsConfig _playerSettingsConfig;
        
        private Statistics _statistics;
        
        public void Initialize(Statistics statistics)
        {
            _statisticsView.Initialize(statistics, _playerSettingsConfig);
        }
    }
}