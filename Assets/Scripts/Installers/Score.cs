using System;
using Asteroids;
using Asteroids.AsteroidsGenerator;
using UpdatesSystem;

namespace Installers
{
    public class Score : IClean
    {
        public event Action<int> Changed;

        public int CurrentScore { get; private set; }

        private readonly AsteroidGenerator _asteroidGenerator;
        private readonly int _currentScore;
        private readonly UfoGenerator _ufoGenerator;

        private IScoreProvider _asteroidScoreProvider;
        private IScoreProvider _ufoScoreProvider;
        
        public Score(AsteroidGenerator asteroidGenerator, UfoGenerator ufoGenerator, int currentScore)
        {
            _asteroidGenerator = asteroidGenerator;
            _ufoGenerator = ufoGenerator;
            _currentScore = currentScore;

            _asteroidGenerator.Spawned += OnAsteroidSpawned;
            _ufoGenerator.Spawned += OnUfoSpawned;
        }

        private void OnAsteroidSpawned(IScoreProvider scoreProvider)
        {
            _asteroidScoreProvider = scoreProvider;
            _asteroidScoreProvider.Scored += AddPoints;
        }

        private void AddPoints(int points)
        {
            CurrentScore += points;
            Changed?.Invoke(CurrentScore);
        }

        private void OnUfoSpawned(IScoreProvider scoreProvider)
        {
            _ufoScoreProvider = scoreProvider;
            _ufoScoreProvider.Scored += AddPoints;
        }

        public void Clean()
        {
            _ufoScoreProvider.Scored -= AddPoints;
            _asteroidScoreProvider.Scored -= AddPoints;
        }
    }
}