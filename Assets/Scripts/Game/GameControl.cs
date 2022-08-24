using Asteroids;
using Asteroids.AsteroidsGenerator;
using Asteroids.AsteroidsGenerator.Pool;
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
        private readonly UfoGenerator _ufoGenerator;

        public GameControl(Player player, GameArea gameArea, GameUpdates gameUpdates, FixedGameUpdates fixedGameUpdates,
            AsteroidGenerator asteroidGenerator, UfoGenerator ufoGenerator)
        {
            _player = player;
            _gameArea = gameArea;
            _gameUpdates = gameUpdates;
            _fixedGameUpdates = fixedGameUpdates;
            _asteroidGenerator = asteroidGenerator;
            _ufoGenerator = ufoGenerator;

            _player.Died += EndGame;
        }

        public void Start()
        {
            _asteroidGenerator.Spawned += OnAsteroidSpawned;
            _ufoGenerator.Spawned += OnUfoSpawned;
            
            _gameUpdates.ResumeUpdate();
            _fixedGameUpdates.ResumeUpdate();
        }

        private void OnUfoSpawned(Ufo ufo)
        {
            _fixedGameUpdates.AddToUpdateList(ufo);
        }

        private void OnAsteroidSpawned(Asteroid asteroid)
        {
            _fixedGameUpdates.AddToUpdateList(asteroid);
        }

        private void EndGame()
        {
            _gameUpdates.StopUpdate();
            _fixedGameUpdates.StopUpdate();
            //остановить инпут
        }

        public void Clean()
        {
            _asteroidGenerator.Spawned -= OnAsteroidSpawned;
            _ufoGenerator.Spawned += OnUfoSpawned;
        }
    }
}