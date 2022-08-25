using Pools;
using Ufos.UfoGenerator.Builder;

namespace Ufos.UfoGenerator.Pool
{
    public sealed class UfoPoolProvider : IUfoProvider
    {
        private readonly UfoBuilder _ufoBuilder;
        private readonly Pool<Ufo> _ufoPool;

        public UfoPoolProvider(UfoBuilder ufoBuilder)
        {
            _ufoBuilder = ufoBuilder;
            _ufoPool = new Pool<Ufo>();
        }

        public Ufo GetUfo()
        {
            return _ufoPool.HasInactiveObjects() ? GetInactiveUfo() : CreateNewUfo();
        }
        
        private Ufo CreateNewUfo()
        {
            var ufo = _ufoBuilder.BuildUfo();

            _ufoPool.Add(ufo);

            return ufo;
        }
        
        private Ufo GetInactiveUfo()
        {
            var ufo = _ufoPool.GetInactiveObject();

            _ufoBuilder.InitializeUfo(ufo);

            return ufo;
        }
    }
}