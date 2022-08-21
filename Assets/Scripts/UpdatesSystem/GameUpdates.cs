using System.Collections.Generic;
using UnityEngine;

namespace UpdatesSystem
{
    public sealed class GameUpdates : MonoBehaviour, IClean
    {
        private List<IUpdate> _updates;
        private bool _isStopped = false;

        private void Awake()
        {
            _updates = new List<IUpdate>();
        }

        public void AddToUpdateList(IUpdate gameUpdate)
        {
            _updates.Add(gameUpdate);
            gameUpdate.UpdateRemoveRequested += OnUpdateRemoveRequested;
        }

        private void OnUpdateRemoveRequested(IUpdate gameUpdate)
        {
            gameUpdate.UpdateRemoveRequested -= OnUpdateRemoveRequested;
            RemoveFromUpdateList(gameUpdate);
        }

        private void RemoveFromUpdateList(IUpdate gameUpdate)
        {
            var index = _updates.FindIndex(s => s == gameUpdate);
            var lastIndex = _updates.Count - 1;
            _updates[index] = _updates[lastIndex];
            _updates.RemoveAt(lastIndex);
        }

        private void Update()
        {
            if (_isStopped)
                return;

            for (var i = 0; i < _updates.Count; i++)
            {
                _updates[i].GameUpdate(Time.deltaTime);
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
            _updates.ForEach(gameUpdate => { gameUpdate.UpdateRemoveRequested -= OnUpdateRemoveRequested; });
        }
    }
}