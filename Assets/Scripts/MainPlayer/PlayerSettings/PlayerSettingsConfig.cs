using MainPlayer.Stats;
using UnityEngine;

namespace MainPlayer.PlayerSettings
{
    [CreateAssetMenu(menuName = "Configs/PlayerConfig")]
    public sealed class PlayerSettingsConfig : ScriptableObject
    {
        [SerializeField] private PlayerStats _playerStats;
        [SerializeField] private GameObject _defaultTilePrefab;
        [SerializeField] private GameObject _laserTilePrefab;
        
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _maxLaserAmmo;
        
        public GameObject LaserTilePrefab => _laserTilePrefab;
        public GameObject DefaultTilePrefab => _defaultTilePrefab;
        public PlayerStats PlayerStats => _playerStats;

        public float MaxLaserAmmo => _maxLaserAmmo;
        public float MovementSpeed => _movementSpeed;
        public float RotationSpeed => _rotationSpeed;
    }
}