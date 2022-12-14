using System.Collections.Generic;
using Game.Operations;
using Game.UI;
using GameAreas;
using UnityEngine;
using UpdatesSystem;

namespace Game.Installers
{
    public sealed class GameInstaller : MonoBehaviour
    {
        [Header("Updates")]
        [SerializeField] private FixedGameUpdates _fixedGameUpdates;
        [SerializeField] private GameUpdates _gameUpdates;
        
        [SerializeField] private Camera _cameraPrefab;
        
        [Header("Installers")]
        [SerializeField] private CharacterInstaller _characterInstaller;
        [SerializeField] private AsteroidsGeneratorInstaller _asteroidsGeneratorInstaller;
        [SerializeField] private UIInstaller _uiInstaller;
        [SerializeField] private UfoGeneratorInstaller _ufoGeneratorInstaller;
        
        [Header("Spawn borders")]
        [SerializeField] private Transform _leftBorder;
        [SerializeField] private Transform _rightBorder;
        [SerializeField] private Transform _topBorder;
        [SerializeField] private Transform _downBorder;

        private void Start() => InstallGame();

        private void InstallGame()
        {
            var player = _characterInstaller.Initialize(_fixedGameUpdates, _gameUpdates);
            
            var laserStats = player.GetGunStatistics();
            var playerTransform = player.GetPlayerTransform();
            var statistics = new Statistics(laserStats, playerTransform, player);
            
            _gameUpdates.AddToUpdateList(statistics);
            _uiInstaller.Initialize(statistics);
            
            var gameArea = new GameArea(_cameraPrefab, _leftBorder, _rightBorder, _topBorder, _downBorder);
            var asteroidsGenerator = _asteroidsGeneratorInstaller.Install(gameArea, _gameUpdates);
            var ufoGenerator = _ufoGeneratorInstaller.Install(gameArea, _gameUpdates, player);

            var score = new Score(asteroidsGenerator, ufoGenerator, 0);
            var ui = _uiInstaller.Install(score, _cameraPrefab);

            var cleanUps = new List<IClean>
            {
                player, ui, _gameUpdates, _fixedGameUpdates, asteroidsGenerator, ufoGenerator, score
            };

            var endGameOperation = new EndGameOperation(cleanUps, score, ui);
            var game = new GameControl(player, gameArea, _gameUpdates, _fixedGameUpdates, asteroidsGenerator, 
                ufoGenerator, endGameOperation);
            game.Start();

            DestroySelf();
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}

