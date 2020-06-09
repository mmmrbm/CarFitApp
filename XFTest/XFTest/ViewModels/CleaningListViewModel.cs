using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using XFTest.Models;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services.Dialogs;
using XFTest.DataServices;
using Prism.Commands;
using System;
using XFTest.Views;
using Xamarin.Forms;
using Prism.Events;
using System.Linq;

namespace XFTest.ViewModels
{
	public class CleaningListViewModel : BindableBase, INotifyPropertyChanged
    {
        private IList<CleaningListJobItem> source;

        private IDataService<CleaningListJobItem> _cleaningListDataService;

        private IDialogService _dialogService;

        private INavigationService _navigationService;

        private IEventAggregator _eventAggregator;

        private ObservableCollection<CleaningListJobItem> _cleaningTasks;

        public ObservableCollection<CleaningListJobItem> CleaningTasks
        {
            get { return _cleaningTasks; }
            set { SetProperty(ref _cleaningTasks, value); }
        }

        /**
         * Refresh command handling logic is based on the elegant solution provided at
         * https://devblogs.microsoft.com/xamarin/refreshview-xamarin-forms/
         */
        public DelegateCommand RefreshCommand { get; set; }

        public DelegateCommand ShowCalendarCommand { get; set; }

        public DelegateCommand HideCalendarCommand { get; set; }

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        private bool _shouldCalanderVisible;

        public bool ShouldCalanderVisible
        {
            get { return _shouldCalanderVisible; }
            set { SetProperty(ref _shouldCalanderVisible, value); }
        }

        private bool _shouldTitleSectionVisible;

        public bool ShouldTitleSectionVisible
        {
            get { return _shouldTitleSectionVisible; }
            set { SetProperty(ref _shouldTitleSectionVisible, value); }
        }

		private DateTime _userSelectedDate;

		public DateTime UserSelectedDate
        {
			get { return _userSelectedDate; }
			set 
            { 
                SetProperty(ref _userSelectedDate, value);
                ModifyListOnUserSelectedDate();
            }
		}


		public CleaningListViewModel( 
            IDialogService dialogService, 
            INavigationService navigationService,
            IEventAggregator eventAggregator,
            IDataService<CleaningListJobItem> cleaningListDataService)
        {
            _dialogService = dialogService;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            _cleaningListDataService = cleaningListDataService;

            RefreshCommand = new DelegateCommand(RefreshCommandHandler);
            ShowCalendarCommand = new DelegateCommand(ShowCalendarCommandHandler);
            HideCalendarCommand = new DelegateCommand(HideCalendarCommandHandler);

            ShouldTitleSectionVisible = true;

            SubscribeToDateSelectedEvent();
            PopulateCleaningTaskList();
        }

        private void ModifyListOnUserSelectedDate()
        {
            IsRefreshing = true;
            var filteredDataSource =  source.Where(jobItem => jobItem.JobStartTime.Date.ToShortDateString().Equals(UserSelectedDate.Date.ToShortDateString())).ToList();
            CleaningTasks = new ObservableCollection<CleaningListJobItem>(filteredDataSource);
            IsRefreshing = false;
        }

        private void LoadDataAsObserverableCollection(IList<CleaningListJobItem> dataSource)
        { 
            
        }

        private void SubscribeToDateSelectedEvent()
        {
            SubscriptionToken subscriptionToken = _eventAggregator.
                                                        GetEvent<PubSubEvent<DateTime>>().
                                                        Subscribe((details) =>
                                                            {
                                                                this.UserSelectedDate = details;
                                                            });
        }

		private void HideCalendarCommandHandler()
		{
            ShouldCalanderVisible = false;
            ShouldTitleSectionVisible = true;
        }

		private void ShowCalendarCommandHandler()
		{
            ShouldCalanderVisible = true;
            ShouldTitleSectionVisible = false;
        }

		private void RefreshCommandHandler()
		{
            if (IsRefreshing)
            {
                return;
            }

            // In production environment below method will be invoked
            // PopulateCleaningTaskList()
            // But, for this excercise, developed below logic to add a dummy object to list

            IsRefreshing = true;

            CleaningListJobItem dummyJobItem = new CleaningListJobItem()
            {
                ClientName = "Dummy Dummy",
                ClientAddressInformation = "Dummy Street, Dummy City, Dummy ZIP",
                DistanceInformation = "Dummy km",
                JobDescription = "Dummy Work",
                JobStatus = "Dummy",
                JobBackgroundColor = "#FF6347"
            };

            source.Add(dummyJobItem);
            CleaningTasks = new ObservableCollection<CleaningListJobItem>(source);

            IsRefreshing = false;
        }

		void PopulateCleaningTaskList()
        {
			try
			{
                IsRefreshing = true;
                source = _cleaningListDataService.FetchDataForEntityAsync().Result;
                UserSelectedDate = DateTime.Now;
                IsRefreshing = false;
            }
			catch (Exception ex)
			{
                CleaningTasks.Clear();
                App.Current.MainPage.DisplayAlert(
                    "Error", 
                    $@"An error occured while fetching data. Please contact your service administrator with below information. 
                    -------------------------------------------------   
                    {ex.Message}
                    -------------------------------------------------", 
                    "OK");
            }
        }
    }
}
