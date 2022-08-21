using System.Collections.Generic;
using UnityEngine;

namespace UpdatesSystem
{
    public class FixedGameUpdates : MonoBehaviour, IClean
    {
        private List<IFixedUpdate> _fixedUpdates;
        private bool _isStopped = false;

        private void Awake()
        {
            _fixedUpdates = new List<IFixedUpdate>();
        }

        public void AddToUpdateList(IFixedUpdate gameUpdate)
        {
            _fixedUpdates.Add(gameUpdate);
            gameUpdate.UpdateFixedRemoveRequested += OnUpdateRemoveRequested;
        }

        private void OnUpdateRemoveRequested(IFixedUpdate gameUpdate)
        {
            gameUpdate.UpdateFixedRemoveRequested -= OnUpdateRemoveRequested;
            RemoveFromUpdateList(gameUpdate);
        }

        private void RemoveFromUpdateList(IFixedUpdate gameUpdate)
        {
            var index = _fixedUpdates.FindIndex(s => s == gameUpdate);
            var lastIndex = _fixedUpdates.Count - 1;
            _fixedUpdates[index] = _fixedUpdates[lastIndex];
            _fixedUpdates.RemoveAt(lastIndex);
        }

        private void FixedUpdate()
        {
            if (_isStopped) 
                return;

            for (var i = 0; i < _fixedUpdates.Count; i++)
            {
                _fixedUpdates[i].FixedGameUpdate(Time.fixedDeltaTime);
            }
        }

        public void StopUpdate()
        {
            _isStopped = true;
        }

        public void ResumeUpdate()
        {
            _isStopped = false;
        }
    
        public void Clean()
        {
            _fixedUpdates.ForEach(gameUpdate =>
            {
                gameUpdate.UpdateFixedRemoveRequested -= OnUpdateRemoveRequested;
            });
        }
    }
}