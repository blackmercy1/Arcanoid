using System;
using Asteroids.AsteroidsGenerator.Pool;
using Common;
using UpdatesSystem;

namespace Asteroids.AsteroidsGenerator
{
    public sealed class UfoGenerator : IClean
    {
        public event Action<Ufo> Spawned;

        private readonly IUfoPlacer _ufoPlacer;
        private readonly IUfoProvider _ufoProvider;
        private readonly RandomLoopTimer _timer; 
        
        public UfoGenerator(IUfoPlacer ufoPlacer, IUfoProvider ufoProvider, RandomLoopTimer timer)
        {
            _ufoPlacer = ufoPlacer;
            _ufoProvider = ufoProvider;
            
            _timer = timer;
            _timer.TimIsUp += SpawnUfo;
            _timer.Resume();
        }

        private void SpawnUfo()
        {
            var ufo = _ufoProvider.GetUfo();
            _ufoPlacer.PlaceUfo(ufo);
            
            Spawned?.Invoke(ufo);
        }

        public void Clean()
        {
            throw new NotImplementedException();
        }
    }
}