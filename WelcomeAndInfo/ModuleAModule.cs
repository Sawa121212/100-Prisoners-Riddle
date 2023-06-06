using System.Resources;
using Common.Core.Localization;
using Common.Core.Regions;
using ModuleA.Properties;
using ModuleA.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleA
{
    /// <summary>
    /// Модуль A
    /// </summary>
    public class WelcomeModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public WelcomeModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // регистрируем View для навигации по Регионам
            containerRegistry.RegisterForNavigation<WelcomeView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Добавим ресурс Локализации в "коллекцию ресурсов локализации"
            containerProvider.Resolve<ILocalizer>().AddResourceManager(new ResourceManager(typeof(Language)));

            // Зарегистрировать View к региону. Теперь при запуске ПО View будет привязано сразу
            _regionManager.RegisterViewWithRegion(RegionNameService.ContentRegionName, typeof(WelcomeView));
        }
    }
}