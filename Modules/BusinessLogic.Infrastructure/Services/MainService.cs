using System.Collections.ObjectModel;
using DataDomain;
using ReactiveUI;

namespace BusinessLogic.Infrastructure.Services
{
    public class MainService : ReactiveObject, IMainService
    {
        private readonly ISearchService _searchService;
        private int _maxSearchAttempt;
        private Game _game;
        private ObservableCollection<Game> _games;
        private int _prisonersCount;

        public MainService(ISearchService searchService)
        {
            _searchService = searchService;
            Games = new ObservableCollection<Game>();
        }


        /// <summary>
        /// Начать новую игру
        /// </summary>
        /// <param name="prisonersCount"></param>
        public void StartNewGame(int prisonersCount)
        {
            if (prisonersCount == 0)
                return;
            _prisonersCount = prisonersCount;
            _maxSearchAttempt = _prisonersCount / 2;

            StartNewGame();
        }

        public void StartNewGame()
        {
            if (_prisonersCount == 0)
                return;

            _game = new Game(_games.Count);
            _games.Add(_game);

            // Build
            _game.Build(_prisonersCount);

            // Search
            StartSearch();
        }

        /// <summary>
        /// Начать поиск
        /// </summary>
        public void StartSearch()
        {
            foreach (var prisoner in _game.Prisoners)
            {
                _searchService.ComeIntoTheRoom(prisoner, _game.Room, _maxSearchAttempt);
            }

            _game.CheckSuccess();
        }

        /// <summary>
        /// Текущая игра
        /// </summary>
        public Game Game
        {
            get => _game;
            private set => this.RaiseAndSetIfChanged(ref _game, value);
        }

        /// <summary>
        /// Список игр
        /// </summary>
        public ObservableCollection<Game> Games
        {
            get => _games;
            private set => this.RaiseAndSetIfChanged(ref _games, value);
        }
    }

    public interface IMainService
    {
        public Game Game { get; }
        public ObservableCollection<Game> Games { get; }

        public void StartNewGame(int prisonersCount);
        public void StartNewGame();
    }
}