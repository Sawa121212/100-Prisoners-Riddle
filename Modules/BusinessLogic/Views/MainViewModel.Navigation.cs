using BusinessLogic.Views.Pages;
using BusinessLogic.Views.Report;
using Common.Core.Regions;
using Prism.Commands;
using Prism.Regions;

namespace BusinessLogic.Views
{

    public partial class MainViewModel
    {
        private readonly IRegionManager _regionManager;
        private IRegionNavigationJournal _journal;

        /// <summary>
        /// Показать информацию о задаче
        /// </summary>
        private void OnShowInformation()
        {
            _regionManager.RequestNavigate(RegionNameService.ContentRegionName, nameof(InfoView));
        }

        /// <summary>
        /// Показать настройки приложения
        /// </summary>
        private void OnShowSettings()
        {
            _regionManager.RequestNavigate(RegionNameService.ContentRegionName, nameof(SettingsView));
        }

        /// <summary>
        /// Показать информацию о приложении
        /// </summary>
        private void OnShowAbout()
        {
            _regionManager.RequestNavigate(RegionNameService.ContentRegionName, nameof(AboutView));
        }

        /// <summary>
        /// Показать отчет по играм
        /// </summary>
        private void OnShowReport()
        {
            _regionManager.RequestNavigate(RegionNameService.ContentRegionName, nameof(ReportView));
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
            ShowInformationCommand.RaiseCanExecuteChanged();
            ShowSettingsCommand.RaiseCanExecuteChanged();
            ShowAboutCommand.RaiseCanExecuteChanged();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public DelegateCommand ShowInformationCommand { get; }
        public DelegateCommand ShowSettingsCommand { get; }
        public DelegateCommand ShowAboutCommand { get; }
        public DelegateCommand ShowReportCommand { get; }
    }
}