using Avalonia.Controls;
using BusinessLogic.Infrastructure.Services;
using Common.Core;
using Common.Resources.Circles;
using DataDomain;
using Prism.Commands;
using Prism.Regions;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace BusinessLogic.Views
{
    /// <summary>
    /// Окно игры
    /// </summary>
    public partial class GamesViewModel : ViewModelBase, INavigationAware
    {
        private readonly IMainService _mainService;
        private ObservableCollection<Game> _games;
        private Game _selectedGame;
        private ItemCircle _testM;
        private int _prisonersCount;

        public GamesViewModel(IRegionManager regionManager, IMainService mainService)
        {
            _regionManager = regionManager;
            _mainService = mainService;
            _games = _mainService.Games;
            _prisonersCount = 100;

            AddNewGameCommand = ReactiveCommand.Create(OnAddNewGame);
            ShowInformationCommand = new DelegateCommand(OnShowInformation);
            ShowSettingsCommand = new DelegateCommand(OnShowSettings);
            ShowAboutCommand = new DelegateCommand(OnShowAbout);

            TestM = new ItemCircle();
            var b = new Button() { Content = "Top 20 Left 40" };
            TestM.ItemsSource.Add(new CircleSubject() { Width = 120, Height = 50, Content = b, Y = 20, X = 40 });
        }

        /// <summary>
        /// Добавить новую игру
        /// </summary>
        private void OnAddNewGame()
        {
            _mainService.StartNewGame(_prisonersCount);
        }

        /// <summary>
        /// Количество заключенных в игре
        /// </summary>
        public int PrisonersCount
        {
            get => _prisonersCount;
            set => this.RaiseAndSetIfChanged(ref _prisonersCount, value);
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
        /// 
        /// </summary>
        public ItemCircle TestM
        {
            get => _testM;
            set => this.RaiseAndSetIfChanged(ref _testM, value);
        }

        public ICommand AddNewGameCommand { get; }
    }
}