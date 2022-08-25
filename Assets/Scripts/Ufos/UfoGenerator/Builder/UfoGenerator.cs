using System;
using Common;
using Ufos.UfoGenerator.Pool;
using UpdatesSystem;

namespace Ufos.UfoGenerator.Builder
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
            _timer.TimIsUp -= SpawnUfo;
            _timer.Clean();
        }
    }
}