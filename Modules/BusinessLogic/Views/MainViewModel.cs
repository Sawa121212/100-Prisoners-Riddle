using Common.Core;
using Common.Resources.Circles;
using DataDomain;
using Prism.Commands;
using Prism.Regions;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BusinessLogic.Events;
using BusinessLogic.Infrastructure.Interfaces;
using Prism.Events;

namespace BusinessLogic.Views
{
    /// <summary>
    /// Окно игры
    /// </summary>
    public partial class MainViewModel : ViewModelBase, INavigationAware
    {
        private readonly IPrisonDirectorManager _mainService;
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<Game> _games;
        private Game _selectedGame;
        private ItemCircle _testM;
        private int _gamesCount;
        private int _prisonersCount;
        private bool _isBusy;

        public MainViewModel(
            IRegionManager regionManager,
            IPrisonDirectorManager mainService,
            IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _mainService = mainService;
            _eventAggregator = eventAggregator;
            _games = new ObservableCollection<Game>(_mainService.Games);
            _gamesCount = 100;
            _prisonersCount = 100;

            AddNewGameCommand = ReactiveCommand.Create(OnAddNewGame);
            ShowInformationCommand = new DelegateCommand(OnShowInformation);
            ShowSettingsCommand = new DelegateCommand(OnShowSettings);
            ShowAboutCommand = new DelegateCommand(OnShowAbout);
            ShowReportCommand = new DelegateCommand(OnShowReport);
        }

        /// <summary>
        /// Добавить новую игру
        /// </summary>
        private async Task OnAddNewGame()
        {
            IsBusy = true;

            await Task.Run(() =>
            {
                for (int i = 0; i < _gamesCount; i++)
                {
                   _mainService.StartNewGame(_prisonersCount);
                }
            });

            Games = new ObservableCollection<Game>(_mainService.Games);

            _eventAggregator.GetEvent<UpdatePlotsEvent>().Publish();
            IsBusy = false;
        }

        /// <summary>
        /// Количество заключенных в игре
        /// </summary>
        public int GamesCount
        {
            get => _gamesCount;
            set => this.RaiseAndSetIfChanged(ref _gamesCount, value);
        }

        /// <summary>
        /// Количество игр
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
        /// Панель 
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set => this.RaiseAndSetIfChanged(ref _isBusy, value);
        }

        public ICommand AddNewGameCommand { get; }
        public ICommand SelectedCircleItemChangedCommand { get; }
    }
}