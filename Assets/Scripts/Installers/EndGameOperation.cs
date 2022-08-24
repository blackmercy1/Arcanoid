using System.Collections.Generic;
using UpdatesSystem;

namespace Installers
{
    public class EndGameOperation
    {
        private readonly List<IClean> _cleanUps;
        private readonly Score _score;
        private readonly UI _ui;

        public EndGameOperation(List<IClean> cleanUps, Score score, UI ui)
        {
            _cleanUps = cleanUps;
            _score = score;
            _ui = ui;
        }

        public void Execute()
        {
            foreach (var cleanUp in _cleanUps)
            {
                cleanUp.Clean();
            }

            DisplayResults();
        }

        private void DisplayResults()
        {
            _ui.ShowEndView(_score);
        }
    }
}