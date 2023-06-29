﻿using System.Resources;

using BusinessLogic.Properties;
using BusinessLogic.Views;
using BusinessLogic.Views.Pages;
using Common.Core.Localization;
using Common.Core.Regions;
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
            containerRegistry.RegisterForNavigation<GamesView>();
            containerRegistry.RegisterForNavigation<InfoView>();
            containerRegistry.RegisterForNavigation<SettingsView>();
            containerRegistry.RegisterForNavigation<AboutView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Добавим ресурс Локализации в "коллекцию ресурсов локализации"
            containerProvider.Resolve<ILocalizer>().AddResourceManager(new ResourceManager(typeof(Language)));

            // Зарегистрировать View к региону.Теперь при запуске ПО View будет привязано сразу
            _regionManager.RequestNavigate(RegionNameService.ContentRegionName, nameof(GamesView));
        }
    }
}