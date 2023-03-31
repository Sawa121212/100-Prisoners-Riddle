using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Threading;
using Common.Core;
using Common.Core.Extensions;
using Common.Core.Localization;
using Prism.Commands;

namespace ModuleA.Views
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ILocalizer _localizer;
        private LanguagesEnum _currentAppCultureInfo;
        private LanguagesEnum _appCultureInfo;
        private bool _initialized;

        public MainViewModel(ILocalizer localizer)
        {
            _localizer = localizer;
            ChangeLanguageCommand = new DelegateCommand(async () => await OnChangeLanguage());
            GoCommand = new DelegateCommand( OnGo);
            AppCultureInfo = CultureInfo.CurrentUICulture.ToString().ToEnum<LanguagesEnum>();

            // Change the resource language forcibly during initialization
            // Изменим язык ресурсов принудительно при инициализации
            OnChangeLanguage();
            _initialized = true;
        }

        private void OnGo()
        {
            
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