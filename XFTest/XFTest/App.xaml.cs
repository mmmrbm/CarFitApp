﻿using Prism;
using Prism.Ioc;
using XFTest.ViewModels;
using XFTest.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFTest.DataServices;
using XFTest.Dtos;
using XFTest.Services.DataMappingServices;
using XFTest.Services.LocationServices;
using XFTest.Util;
using Prism.Plugin.Popups;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XFTest
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            containerRegistry.Register<IDataFetchService<XFTest.Models.CleaningListJobItem>, CleaningListDataService>();
            containerRegistry.Register<IDataFetchService<CarFitClientDto>, CarFitClientDataService>();
            containerRegistry.Register<IEntityListMappingService<CarFitClientDto, XFTest.Models.CleaningListJobItem>, CarFitClientListCleaningListMappingService>();

            containerRegistry.Register<IDistanceCalculationService, LongLatDistanceCalculationService>();

            containerRegistry.Register<CarFitClientCleaningListJobItemDataConvertHelper>();
        }
    }
}
