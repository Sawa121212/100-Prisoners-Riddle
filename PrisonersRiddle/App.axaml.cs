using System;
using System.Reflection;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Common.Core.Localization;
using ModuleA;
using BusinessLogic;
using BusinessLogic.Infrastructure.Services;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using PrisonersRiddle.Properties;
using PrisonersRiddle.Views;
using IResourceProvider = Common.Core.Localization.IResourceProvider;

namespace PrisonersRiddle{

public class App : PrismApplication
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        base.Initialize();
    }

    protected override Window CreateShell()
    {
        return Container.Resolve<ShellView>();
    }

    /// <summary>
    /// Регистрация служб приложения
    /// </summary>
    /// <param name="containerRegistry"></param>
    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry
            .RegisterSingleton<ILocalizer, Localizer>()
            .RegisterSingleton<IResourceProvider, ResourceProvider>(Assembly.GetExecutingAssembly().FullName)
            .RegisterSingleton<IMainService, MainService>()
            .RegisterSingleton<ISearchService, SearchService>()
            ;
        containerRegistry.RegisterSingleton<ShellView>();
    }

    /// <summary>
    /// Регистрация модулей приложения
    /// </summary>
    /// <param name="moduleCatalog"></param>
    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        moduleCatalog
            .AddModule<WelcomeModule>()
            .AddModule<BusinessLogicModule>();

        base.ConfigureModuleCatalog(moduleCatalog);
    }

    protected override void InitializeShell(IAvaloniaObject shell)
    {
        base.InitializeShell(shell);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = CreateShell();
            if (desktop.MainWindow != null)
            {
                desktop.MainWindow.Show();
            }
        }
    }

    /// <summary>
    /// ViewModel Locator. Мы работаем с View, а не с ViewModel!
    /// Ищем ViewModel для View в той же папке, где и View лежит.
    /// </summary>
    protected override void ConfigureViewModelLocator()
    {
        base.ConfigureViewModelLocator();

        ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
        {
            var viewName = viewType.FullName;
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;

            var viewModelName = string.Format(
                viewName != null && viewName.EndsWith("View", StringComparison.OrdinalIgnoreCase)
                    ? "{0}Model, {1}"
                    : "{0}ViewModel, {1}",
                viewName, viewAssemblyName);
            return Type.GetType(viewModelName);
        });

        ViewModelLocationProvider.SetDefaultViewModelFactory(type => Container.Resolve(type));
    }
} }