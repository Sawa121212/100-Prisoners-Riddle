using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Controls;
using BusinessLogic.Infrastructure.Services;
using Common.Core;
using Common.Resources.Circles;
using DataDomain;
using ReactiveUI;

namespace BusinessLogic.Views
{

    public class GamesViewModel : ViewModelBase
    {
        private readonly IMainService _mainService;
        private ObservableCollection<Game> _games;
        private Game _selectedGame;
        private ItemCircle _testM;

        public GamesViewModel(IMainService mainService)
        {
            _mainService = mainService;
            _games = _mainService.Games;
            AddNewGameCommand = ReactiveCommand.Create(OnAddNewGame);

            TestM = new ItemCircle();
            var b = new Button() { Content = "Top 20 Left 40" };
            TestM.ItemsSource.Add(new CircleSubject() { Width = 120, Height = 50, Content = b, Y = 20, X = 40 });
        }

        /// <summary>
        /// Добавить новую игру
        /// </summary>
        private void OnAddNewGame()
        {
            _mainService.StartNewGame();
        }

        /// <summary>
        /// Список игр
        /// </summary>
        public ObservableCollection<Game> Games
        {
            get => _games;
            set => this.RaiseAndSetIfChanged(ref _games, value);
        }

        /// <summary>
        /// Выбранная игра
        /// </summary>
        public Game SelectedGame
        {
            get => _selectedGame;
            set => this.RaiseAndSetIfChanged(ref _selectedGame, value);
        }

        /// <summary>
        /// Выбранная игра
        /// </summary>
        public ItemCircle TestM
        {
            get => _testM;
            set => this.RaiseAndSetIfChanged(ref _testM, value);
        }

        public ICommand AddNewGameCommand { get; }
    }
}