using Asteroids.AsteroidsGenerator.Builder;
using Pools;

namespace Asteroids.AsteroidsGenerator.Pool
{
    public class AsteroidsPoolProvider : IAsteroidsProvider
    {
        private readonly AsteroidsBuilder _asteroidsBuilder;
        private readonly Pool<Asteroid> _pool;

        public AsteroidsPoolProvider(AsteroidsBuilder asteroidsBuilder)
        {
            _asteroidsBuilder = asteroidsBuilder;
            _pool = new Pool<Asteroid>();
        }

        public Asteroid GetAsteroid()
        {
            return _pool.HasInactiveObjects() ? GetInactiveAsteroid() : CreateNewAsteroid();
        }

        private Asteroid GetInactiveAsteroid()
        {
            var asteroid = _pool.GetInactiveObject();

            _asteroidsBuilder.InitializeAsteroid(asteroid);

            return asteroid;
        }

        private Asteroid CreateNewAsteroid()
        {
            var asteroid = _asteroidsBuilder.BuildAsteroid();

            _pool.Add(asteroid);
            
            return asteroid;
        }
    }
}