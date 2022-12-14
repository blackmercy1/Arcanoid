using System.Collections.Generic;

namespace Pools
{
    public class Pool<T> : IPoolReturn where T : PooledObject
    {
        private readonly Queue<T> _inactiveObjects = new Queue<T>();

        public void Add(T obj)
        {
            obj.AssignPool(this);
        }

        public void ReturnToPool(PooledObject obj)
        {
            obj.Disable();
            _inactiveObjects.Enqueue((T) obj);
        }

        public bool HasInactiveObjects()
        {
            return _inactiveObjects.Count > 0;
        }

        public T GetInactiveObject()
        {
            var inactiveObject = _inactiveObjects.Dequeue();
            inactiveObject.Enable();

            return inactiveObject;
        }
    }
}