using Game.Operations;
using UnityEngine;
using UpdatesSystem;
namespace Game.UI
{
    public class UI : IClean
    {
        private readonly Canvas _canvas;
        private readonly EndGameView _endGameView;
        private readonly Camera _camera;

        public UI(Canvas canvas, EndGameView endGameView, Camera camera)
        {
            _canvas = canvas;
            _endGameView = endGameView;
            _camera = camera;

            _canvas.worldCamera = camera;
        }

        public void ShowEndView(Score score)
        {
            var newGameOperation = new NewGameOperation();
            _endGameView.Initialize(score, newGameOperation);
            _endGameView.gameObject.SetActive(true);
        }

        void IClean.Clean()
        {
            
        }
    }
}