using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Threading;
using BusinessLogic.Views;
using Common.Core;
using Common.Core.Extensions;
using Common.Core.Localization;
using Common.Core.Regions;
using Prism.Commands;
using Prism.Regions;

namespace ModuleA.Views
{
    public class WelcomeViewModel : ViewModelBase
    {
        private readonly ILocalizer _localizer;
        private readonly IRegionManager _regionManager;
        private LanguagesEnum _currentAppCultureInfo;
        private LanguagesEnum _appCultureInfo;
        private bool _initialized;

        public WelcomeViewModel(ILocalizer localizer, IRegionManager regionManager)
        {
            _localizer = localizer;
            _regionManager = regionManager;
            ChangeLanguageCommand = new DelegateCommand(async () => await OnChangeLanguage());
            GoCommand = new DelegateCommand(OnGo);
            AppCultureInfo = CultureInfo.CurrentUICulture.ToString().ToEnum<LanguagesEnum>();

            // Change the resource language forcibly during initialization
            // Изменим язык ресурсов принудительно при инициализации
            OnChangeLanguage();
            _initialized = true;
        }

        private void OnGo()
        {
            _regionManager.RequestNavigate(RegionNameService.ContentRegionName, nameof(StartSettingsView));
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
                    () => { OnChangeCulture(AppCultureInfo); },
                    DispatcherPriority.SystemIdle);
                _currentAppCultureInfo = _appCultureInfo;
            }
        }

        private void OnChangeCulture(LanguagesEnum languagesEnum)
        {
            var lang = languagesEnum.ToString();
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
            _localizer.EditLn(lang);
        }

        /// <summary>
        /// Application Language / Язык приложения
        /// </summary>
        public LanguagesEnum AppCultureInfo
        {
            get => _appCultureInfo;
            set => SetProperty(ref _appCultureInfo, value);
        }

        public ICommand GoCommand { get; }
        public ICommand ChangeLanguageCommand { get; }
    }
}