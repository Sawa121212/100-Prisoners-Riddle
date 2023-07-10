using Avalonia.Controls;
using BusinessLogic.Infrastructure.Services;
using Common.Core;
using Common.Resources.Circles;
using DataDomain;
using Prism.Commands;
using Prism.Regions;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Threading;
using BusinessLogic.Infrastructure.Interfaces;

namespace BusinessLogic.Views
{
    /// <summary>
    /// Окно игры
    /// </summary>
    public partial class MainViewModel : ViewModelBase, INavigationAware
    {
        private readonly IMainService _mainService;
        private ObservableCollection<Game> _games;
        private Game _selectedGame;
        private ItemCircle _testM;
        private int _gamesCount;
        private int _prisonersCount;
        private bool _isBusy;

        public MainViewModel(IRegionManager regionManager, IMainService mainService)
        {
            _regionManager = regionManager;
            _mainService = mainService;
            _games = _mainService.Games;
            _gamesCount = 1;
            _prisonersCount = 100;

            AddNewGameCommand = ReactiveCommand.Create(OnAddNewGame);
            ShowInformationCommand = new DelegateCommand(OnShowInformation);
            ShowSettingsCommand = new DelegateCommand(OnShowSettings);
            ShowAboutCommand = new DelegateCommand(OnShowAbout);
            //bool? x = new Signal<bool?>();

            SelectedCircleItemChangedCommand = new DelegateCommand(OnSelectedCircleItemChanged);
        }

        private void OnSelectedCircleItemChanged()
        {
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
        /// Выбранная игра
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