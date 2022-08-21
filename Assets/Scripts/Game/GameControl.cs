using Asteroids;
using Asteroids.AsteroidsGenerator;
using GameAreas;
using MainPlayer;
using UpdatesSystem;

namespace Game
{
    public class GameControl : IClean
    {
        private readonly Player _player;
        private readonly GameArea _gameArea;
        private readonly GameUpdates _gameUpdates;
        private readonly FixedGameUpdates _fixedGameUpdates;
        private readonly AsteroidGenerator _asteroidGenerator;

        public GameControl(Player player, GameArea gameArea, GameUpdates gameUpdates, FixedGameUpdates fixedGameUpdates,
            AsteroidGenerator asteroidGenerator)
        {
            _player = player;
            _gameArea = gameArea;
            _gameUpdates = gameUpdates;
            _fixedGameUpdates = fixedGameUpdates;
            _asteroidGenerator = asteroidGenerator;

            _player.Died += EndGame;
        }

        public void Start()
        {
            _asteroidGenerator.Spawned += OnSpawned;
            
            _gameUpdates.ResumeUpdate();
            _fixedGameUpdates.ResumeUpdate();
        }

        private void OnSpawned(Asteroid asteroid)
        {
            _fixedGameUpdates.AddToUpdateList(asteroid);
        }

        private void EndGame()
        {
            _gameUpdates.StopUpdate();
            _fixedGameUpdates.StopUpdate();
        }

        public void Clean()
        {
            _asteroidGenerator.Spawned -= OnSpawned;
        }
    }
}