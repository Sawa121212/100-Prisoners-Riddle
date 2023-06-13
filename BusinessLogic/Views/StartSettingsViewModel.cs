using System.Windows.Input;
using Avalonia.Utilities;
using BusinessLogic.Infrastructure.Services;
using Common.Core;
using Common.Core.Regions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ReactiveUI;

namespace BusinessLogic.Views
{
    public class StartSettingsViewModel : ViewModelBase
    {
        private readonly IMainService _mainService;
        private readonly IRegionManager _regionManager;
        private int _prisonersCount;

        public StartSettingsViewModel(IMainService mainService, IRegionManager regionManager)
        {
            _mainService = mainService;
            _regionManager = regionManager;
            StartCommand = new DelegateCommand(OnStart);
            BackCommand = new DelegateCommand(OnBack);

            PrisonersCount = 100;
        }

        private void OnStart()
        {
            _mainService.StartNewGame(_prisonersCount);
            _regionManager.RequestNavigate(RegionNameService.ContentRegionName, nameof(GamesView));
        }
        
        private void OnBack()
        {
            _regionManager.RequestNavigate(RegionNameService.ContentRegionName, "WelcomeView");
        }

        public int PrisonersCount
        {
            get => _prisonersCount;
            set =>  this.RaiseAndSetIfChanged(ref _prisonersCount, value);
        }

        public ICommand StartCommand { get; }
        public ICommand BackCommand { get; }
    }
}