using TMPro;
using UnityEngine;
using UpdatesSystem;

namespace Installers
{
    public class EndGameView : MonoBehaviour, IClean
    {
        [SerializeField] private TMP_Text _currentScore;
        [SerializeField] private CustomButton _restartButton;

        private NewGameOperation _newGameOperation;
        private Score _score;
        
        public void Initialize(Score score, NewGameOperation newGameOperation)
        {
            _currentScore.text = score.CurrentScore.ToString();
            _restartButton.Pressed += RestartGame;
            _newGameOperation = newGameOperation;
        }

        private void RestartGame()
        {
            _newGameOperation.Execute();
        }
        
        public void Clean()
        {
            _restartButton.Pressed -= RestartGame;
        }
    }
}