using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Threading;
using Common.Core;
using Common.Core.Extensions;
using Common.Core.Localization;
using Prism.Commands;
using Prism.Regions;
using Avalonia.Media;
using Material.Styles.Themes;
using Material.Styles.Themes.Base;
using ReactiveUI;
using System.Diagnostics;

namespace BusinessLogic.Views.Pages
{
    public class AboutViewModel : ViewModelBase, INavigationAware
    {
        private readonly ILocalizer _localizer;
        private readonly IRegionManager _regionManager;
        private LanguagesEnum _currentAppCultureInfo;
        private LanguagesEnum _appCultureInfo;
        private BaseThemeMode _currentThemeMode;
        private BaseThemeMode _themeMode;
        private PaletteHelper _paletteHelper;
        private IRegionNavigationJournal _journal;
        private bool _initialized;

        public AboutViewModel(ILocalizer localizer)
        {
            _localizer = localizer;
            ChangeLanguageCommand = new DelegateCommand(async () => await OnChangeLanguage());
            ChangeMaterialUiThemeCommand = new DelegateCommand(OnChangeMaterialUiTheme);
            MoveBackCommand = new DelegateCommand(OnGoBack);
            OpenGitHubLinkCommand = new DelegateCommand(OnOpenProjectRepoLink);
            OpenAvaloniaCommand = new DelegateCommand(OnOpenAvaloniaLink);
            OpenPrismAvaloniaCommand = new DelegateCommand(OnOpenPrismAvaloniaLink);
            OpenMaterialAvaloniaCommand = new DelegateCommand(OnOpenMaterialAvaloniaLink);

            _appCultureInfo = System.Globalization.CultureInfo.CurrentUICulture.ToString().ToEnum<LanguagesEnum>();
            _currentAppCultureInfo = _appCultureInfo;
            GetTheme();
            _initialized = true;
        }


        private void GetTheme()
        {
            _paletteHelper = new PaletteHelper();
            ITheme theme = _paletteHelper.GetTheme();
            _themeMode = theme.Background.Equals(Colors.Black) ? BaseThemeMode.Dark : BaseThemeMode.Light;
            _currentThemeMode = _themeMode;
        }

        /// <summary>
        /// Поменять язык
        /// </summary>
        private async Task OnChangeLanguage()
        {
            if (!_currentAppCultureInfo.Equals(_appCultureInfo) || !_initialized)
            {
                // lang
                await Dispatcher.UIThread.InvokeAsync(
                    () => { OnChangeCulture(CultureInfo); },
                    DispatcherPriority.SystemIdle);
                _currentAppCultureInfo = _appCultureInfo;
            }
        }

        private void OnChangeCulture(LanguagesEnum languagesEnum)
        {
            var lang = languagesEnum.ToString();
            Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(lang);
            _localizer.EditLn(lang);
        }

        /// <summary>
        /// Поменять тему
        /// </summary>
        private void OnChangeMaterialUiTheme()
        {
            if (!_currentThemeMode.Equals(_themeMode))
            {
                //Retrieve the app's existing theme
                ITheme theme = _paletteHelper.GetTheme();

                //Change the base theme
                switch (_themeMode)
                {
                    case BaseThemeMode.Inherit:
                    case BaseThemeMode.Light:
                        theme.SetBaseTheme(BaseThemeMode.Light.GetBaseTheme());
                        break;
                    case BaseThemeMode.Dark:
                        theme.SetBaseTheme(BaseThemeMode.Dark.GetBaseTheme());
                        break;
                    default:
                        break;
                }

                //Change the app's current theme
                _paletteHelper.SetTheme(theme);
                _currentThemeMode = _themeMode;
            }
        }

        private void OnOpenProjectRepoLink() =>
            OpenBrowserForVisitSite("https://github.com/Sawa121212/100-Prisoners-Riddle");

        private void OnOpenAvaloniaLink() =>
            OpenBrowserForVisitSite("https://avaloniaui.net/");

        private void OnOpenMaterialAvaloniaLink() =>
            OpenBrowserForVisitSite("https://github.com/AvaloniaUtils/material.avalonia");

        private void OnOpenPrismAvaloniaLink() =>
            OpenBrowserForVisitSite("https://github.com/AvaloniaCommunity/Prism.Avalonia");

        private void OpenBrowserForVisitSite(string link)
        {
            var param = new ProcessStartInfo
            {
                FileName = link,
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(param);
        }

        private void OnGoBack()
        {
            _journal.GoBack();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _journal = navigationContext.NavigationService.Journal;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        /// <summary>
        /// Application Language / Язык приложения
        /// </summary>
        public LanguagesEnum CultureInfo
        {
            get => _appCultureInfo;
            set => this.RaiseAndSetIfChanged(ref _appCultureInfo, value);
        }

        /// <summary>
        /// Тема приложения
        /// </summary>
        public BaseThemeMode ThemeMode
        {
            get => _themeMode;
            set => this.RaiseAndSetIfChanged(ref _themeMode, value);
        }

        public ICommand MoveBackCommand { get; }
        public ICommand ChangeLanguageCommand { get; }
        public ICommand ChangeMaterialUiThemeCommand { get; }
        public ICommand OpenGitHubLinkCommand { get; }
        public ICommand OpenAvaloniaCommand { get; }
        public ICommand OpenPrismAvaloniaCommand { get; }
        public ICommand OpenMaterialAvaloniaCommand { get; }

    }
}