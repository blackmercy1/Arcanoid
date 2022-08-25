using GameAreas;

namespace Ufos.UfoGenerator.Builder
{
    public sealed class UfoPlacer : IUfoPlacer
    {
        private readonly GameArea _gameArea;

        public UfoPlacer(GameArea gameArea)
        {
            _gameArea = gameArea;
        }

        public void PlaceUfo(Ufo ufo)
        {
            var transform = ufo.transform;

            var startPosition = _gameArea.GetRandomStartPosition();

            _gameArea.PlaceObject(transform, startPosition);
        }
    }
}