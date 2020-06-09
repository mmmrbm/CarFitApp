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
using Prism.Events;
using System.Linq;

namespace XFTest.ViewModels
{
	public class CleaningListViewModel : BindableBase, INotifyPropertyChanged
    {
		#region Private Members
		private IList<CleaningListJobItem> source;

        private IDataFetchService<CleaningListJobItem> _cleaningListDataService;

        private IDialogService _dialogService;

        private INavigationService _navigationService;

        private IEventAggregator _eventAggregator;
		#endregion

		#region Properties
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

        private ObservableCollection<CleaningListJobItem> _cleaningTasks;

        public ObservableCollection<CleaningListJobItem> CleaningTasks
        {
            get { return _cleaningTasks; }
            set { SetProperty(ref _cleaningTasks, value); }
        }
		#endregion

		#region Commands
		public DelegateCommand RefreshCommand { get; set; }

        public DelegateCommand ShowCalendarCommand { get; set; }

        public DelegateCommand HideCalendarCommand { get; set; }
        #endregion

        /// <summary>
        /// Constructs <see cref="CleaningListViewModel"/>
        /// </summary>
        /// <param name="dialogService">Instance of <see cref="IDialogService"/></param>
        /// <param name="navigationService">Instance of <see cref="INavigationService"/></param>
        /// <param name="eventAggregator">Instance of <see cref="IEventAggregator"/></param>
        /// <param name="cleaningListDataService">Instance of <see cref="IDataFetchService"/> of type <see cref="CleaningListJobItem"/></param>
        public CleaningListViewModel( 
            IDialogService dialogService, 
            INavigationService navigationService,
            IEventAggregator eventAggregator,
            IDataFetchService<CleaningListJobItem> cleaningListDataService)
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

        #region Actions
        /// <summary>
        /// Responsible to populate data in to the collection which will be displayed in app.
        /// </summary>
        private void PopulateCleaningTaskList()
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

        /// <summary>
        /// Responsible to filter out the data based on the selected date by user and display the filtered out data list.
        /// </summary>
        private void ModifyListOnUserSelectedDate()
        {
            IsRefreshing = true;
            var filteredDataSource =  source.Where(jobItem => jobItem.JobStartTime.Date.ToShortDateString().Equals(UserSelectedDate.Date.ToShortDateString())).ToList();
            CleaningTasks = new ObservableCollection<CleaningListJobItem>(filteredDataSource);
            IsRefreshing = false;
        }

        /// <summary>
        /// Responsible to subscribe to the event which will be raised when user changes the date of interest
        /// </summary>
        private void SubscribeToDateSelectedEvent()
        {
            SubscriptionToken subscriptionToken = _eventAggregator.
                                                        GetEvent<PubSubEvent<DateTime>>().
                                                        Subscribe((details) =>
                                                            {
                                                                this.UserSelectedDate = details;
                                                            });
        }

        /// <summary>
        /// Responsible to manage logic to hide the calendar widget and display the header section
        /// </summary>
		private void HideCalendarCommandHandler()
		{
            ShouldCalanderVisible = false;
            ShouldTitleSectionVisible = true;
        }

        /// <summary>
        /// Responsible to manage logic to display the calendar widget and hide the header section
        /// </summary>
		private void ShowCalendarCommandHandler()
		{
            ShouldCalanderVisible = true;
            ShouldTitleSectionVisible = false;
        }

        /// <summary>
        /// Respoisble to manage logic to handle refresh activity of user and re-populate data.
        /// </summary>
        /**
         * Refresh command handling logic is based on the elegant solution provided at
         * https://devblogs.microsoft.com/xamarin/refreshview-xamarin-forms/
         */
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
                JobBackgroundColor = "#FF6347",
                JobStartTime = DateTime.Now
            };

            source.Add(dummyJobItem);
            ModifyListOnUserSelectedDate();

            IsRefreshing = false;
        }
		#endregion
	}
}
