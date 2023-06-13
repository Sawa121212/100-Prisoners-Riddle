using System.Resources;
using BusinessLogic.Properties;
using BusinessLogic.Views;
using Common.Core.Localization;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace BusinessLogic
{
    /// <summary>
    /// Модуль B
    /// </summary>
    public class BusinessLogicModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public BusinessLogicModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Регистрируем View для навигации по Регионам
            containerRegistry.RegisterForNavigation<StartSettingsView>();
            containerRegistry.RegisterForNavigation<GamesView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Добавим ресурс Локализации в "коллекцию ресурсов локализации"
            containerProvider.Resolve<ILocalizer>().AddResourceManager(new ResourceManager(typeof(Language)));
        }
    }
}