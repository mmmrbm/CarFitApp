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

namespace XFTest.ViewModels
{
	public class CleaningListViewModel : BindableBase, INotifyPropertyChanged
    {
        private IList<CleaningListJobItem> source;

        private IDataService<CleaningListJobItem> _cleaningListDataService;

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

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }

        public CleaningListViewModel( 
            IDialogService dialogService, 
            INavigationService navigationService,
            IDataService<CleaningListJobItem> cleaningListDataService)
        {
            _cleaningListDataService = cleaningListDataService;
            RefreshCommand = new DelegateCommand(RefreshCommandHandler);

            PopulateCleaningTaskList();
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
            IsRefreshing = true;
            source = _cleaningListDataService.FetchDataForEntityAsync().Result;
            CleaningTasks = new ObservableCollection<CleaningListJobItem>(source);
            IsRefreshing = false;
        }
    }
}
