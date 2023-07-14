using System.Collections.Generic;
using BusinessLogic.Infrastructure.Interfaces;
using DataDomain;
using ReactiveUI;

namespace BusinessLogic.Infrastructure.Services
{
    public class PrisonDirectorManager: ReactiveObject, IPrisonDirectorManager
    {
        private int _maxSearchAttempt;
        private Game _game;
        private List<Game> _games;
        private int _prisonersCount;

        public PrisonDirectorManager()
        {
            Games = new List<Game>();
        }

        /// <inheritdoc />
        public void StartNewGame(int prisonersCount)
        {
            if (prisonersCount == 0)
                return;
            _prisonersCount = prisonersCount;
            _maxSearchAttempt = _prisonersCount / 2;

            StartNewGame();
        }

        /// <inheritdoc />
        public void StartNewGame()
        {
            if (_prisonersCount == 0)
                return;

            _game = new Game(_games.Count + 1);

            _game.Play(_prisonersCount);

            _games.Add(_game);
        }

  

        /// <inheritdoc />
        public Game Game
        {
            get => _game;
            private set => this.RaiseAndSetIfChanged(ref _game, value);
        }

        /// <inheritdoc />
        public List<Game> Games
        {
            get => _games;
            private set => this.RaiseAndSetIfChanged(ref _games, value);
        }
    }
}