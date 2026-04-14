using Avalonia;
using Avalonia.Controls.Documents;
using Avalonia.Threading;
using Common.Core;
using Common.Core.Extensions;
using Common.Core.Localization;
using Material.Styles.Themes;
using Material.Styles.Themes.Base;
using Prism.Commands;
using Prism.Regions;
using ReactiveUI;
using System.Windows.Input;

namespace BusinessLogic.Views.Pages;

public class SettingsViewModel : ViewModelBase, INavigationAware
{
    private readonly ILocalizer _localizer;
    private LanguagesEnum _currentAppCultureInfo;
    private LanguagesEnum _appCultureInfo;
    private IRegionNavigationJournal _journal;
    private bool _initialized;

    public SettingsViewModel(ILocalizer localizer)
    {
        _localizer = localizer;
        ChangeLanguageCommand = new DelegateCommand(async () => await OnChangeLanguage());
        ChangeMaterialUiThemeCommand = new DelegateCommand<object>(OnChangeMaterialUiTheme);
        MoveBackCommand = new DelegateCommand(OnGoBack);

        // culture
        _appCultureInfo = System.Globalization.CultureInfo.CurrentUICulture.ToString().ToEnum<LanguagesEnum>();
        _currentAppCultureInfo = _appCultureInfo;

        // theme
        ThemeMode = Application.Current!.LocateMaterialTheme<MaterialThemeBase>();

        _initialized = true;
    }

    private void GetTheme()
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
                () =>
                {
                    OnChangeCulture(CultureInfo);
                },
                DispatcherPriority.SystemIdle);
            _currentAppCultureInfo = _appCultureInfo;
        }
    }

    private void OnChangeCulture(LanguagesEnum languagesEnum)
    {
        string lang = languagesEnum.ToString();
        Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(lang);
        _localizer.EditLn(lang);
    }

    /// <summary>
    /// Поменять тему
    /// </summary>
    private void OnChangeMaterialUiTheme(object value)
    {
        if (value is not BaseThemeMode themeMode)
        {
            return;
        }

        if (themeMode.Equals(ThemeMode.BaseTheme))
        {
            return;
        }

        ThemeMode.BaseTheme = themeMode;
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
    public MaterialThemeBase ThemeMode
    {
        get => _themeMode;
        set => this.RaiseAndSetIfChanged(ref _themeMode, value);
    }
    private MaterialThemeBase _themeMode;

    public ICommand MoveBackCommand { get; }

    public ICommand ChangeLanguageCommand { get; }

    public ICommand ChangeMaterialUiThemeCommand { get; }
}