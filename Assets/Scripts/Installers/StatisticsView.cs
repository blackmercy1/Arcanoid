using System.Globalization;
using MainPlayer.PlayerSettings;
using TMPro;
using UnityEngine;
using UpdatesSystem;

namespace Installers
{
    public sealed class StatisticsView : MonoBehaviour, IClean
    {
        [SerializeField] private TMP_Text _laserAmmo;
        [SerializeField] private TMP_Text _rotationAngle;
        [SerializeField] private TMP_Text _movementSpeed;
        [SerializeField] private TMP_Text _timeToReloadLaser;
        
        [SerializeField] private TMP_Text _playerCoordinateX;
        [SerializeField] private TMP_Text _playerCoordinateY;

        private PlayerSettingsConfig _playerSettingsConfig;
        private Statistics _statistics;
        
        public void Initialize(Statistics statistics, PlayerSettingsConfig playerSettingsConfig)
        {
            _statistics = statistics;
            _playerSettingsConfig = playerSettingsConfig;
            
            _statistics.LaserAmmoChanged += StatisticsOnLaserAmmoChanged;
            _statistics.RotationChanged += StatisticsOnRotationChanged;
            _statistics.PositionChanged += StatisticsOnPositionChanged;
            _statistics.SpeedChanged += StatisticsOnSpeedChanged;
            _statistics.TimeToReloadLaserChanged += StatisticsOnTimeToReloadLaserChanged;
        }

        private void StatisticsOnTimeToReloadLaserChanged(float reloadTime)
        {
            _timeToReloadLaser.text = Mathf.Round(reloadTime).ToString(CultureInfo.InvariantCulture);
        }

        private void StatisticsOnSpeedChanged(Vector2 speed)
        {
            var speedRound = Mathf.Sqrt((speed.x * speed.x) + (speed.y * speed.y));
            _movementSpeed.text = Mathf.Round(speedRound * 1000).ToString(CultureInfo.InvariantCulture);
        }

        private void StatisticsOnPositionChanged(Vector2 position)
        {
            _playerCoordinateY.text = Mathf.RoundToInt(position.y).ToString(CultureInfo.InvariantCulture);
            _playerCoordinateX.text = Mathf.RoundToInt(position.x).ToString(CultureInfo.InvariantCulture);
        }

        private void StatisticsOnRotationChanged(Quaternion rotation)
        {
            _rotationAngle.text = Mathf.RoundToInt(rotation.eulerAngles.z).ToString(CultureInfo.InvariantCulture);
        }
        
        private void StatisticsOnLaserAmmoChanged(int laserAmmo)
        {
            _laserAmmo.text = laserAmmo + "/" + _playerSettingsConfig.MaxLaserAmmo.ToString(CultureInfo.InvariantCulture);
        }

        void IClean.Clean()
        {
            _statistics.LaserAmmoChanged -= StatisticsOnLaserAmmoChanged;
            _statistics.RotationChanged -= StatisticsOnRotationChanged;
            _statistics.PositionChanged -= StatisticsOnPositionChanged;
            _statistics.SpeedChanged -= StatisticsOnSpeedChanged;
            _statistics.TimeToReloadLaserChanged -= StatisticsOnTimeToReloadLaserChanged;
        }
    }
}