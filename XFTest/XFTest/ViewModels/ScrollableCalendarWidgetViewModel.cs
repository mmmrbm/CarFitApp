using ImTools;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using XFTest.Models;

namespace XFTest.ViewModels
{
	public class ScrollableCalendarWidgetViewModel : BindableBase, INotifyPropertyChanged
	{
		#region Private Members
		private List<CalendarWidgetDate> _dateInfoList;

		private IEventAggregator _eventAggregator;
		#endregion

		#region Properties

		private Boolean _isDateRefreshing;

		public Boolean IsDateRefreshing
		{
			get { return _isDateRefreshing; }
			set { SetProperty(ref _isDateRefreshing, value); }
		}

		private string _yearMonthInfo;

		public string YearMonthInfo
		{
			get { return _yearMonthInfo; }
			set { SetProperty(ref _yearMonthInfo, value); }
		}

		private DateTime _selectedDateOfInterest;

		public DateTime SelectedDateOfInterest
		{
			get { return _selectedDateOfInterest; }
			set
			{
				SetProperty(ref _selectedDateOfInterest, value);
				this._eventAggregator.GetEvent<PubSubEvent<DateTime>>().Publish(this.SelectedDateOfInterest);
			}
		}

		private DateTime _indicatorDateOfMonth;

		public DateTime IndicatorDateOfMonth
		{
			get { return _indicatorDateOfMonth; }
			set { SetProperty(ref _indicatorDateOfMonth, value); }
		}


		private ObservableCollection<CalendarWidgetDate> _dateList;

		public ObservableCollection<CalendarWidgetDate> DateList
		{
			get { return _dateList; }
			set { SetProperty(ref _dateList, value); }
		}
		#endregion

		#region Commands
		public DelegateCommand DateSelectCommand { get; set; }
		public DelegateCommand MoveWeekBackwardCommand { get; set; }
		public DelegateCommand MoveWeekForwardCommand { get; set; }
		#endregion

		/// <summary>
		/// Constructs <see cref="ScrollableCalendarWidgetViewModel"/>
		/// </summary>
		/// <param name="eventAggregator">An implementation of <see cref="IEventAggregator"/></param>
		public ScrollableCalendarWidgetViewModel(
			IEventAggregator eventAggregator)
		{
			_eventAggregator = eventAggregator;

			MoveWeekBackwardCommand = new DelegateCommand(MoveWeekBackwardCommandHandler);
			MoveWeekForwardCommand = new DelegateCommand(MoveWeekForwardCommandHandler);
			DateSelectCommand = new DelegateCommand(DateSelectCommandHandler);

			IndicatorDateOfMonth = DateTime.Now;
			SelectedDateOfInterest = DateTime.Now;
			SetupUiDateInfo();
		}

		#region Methods
		/// <summary>
		/// Responsible to handle the logic to be executed when user selects a date
		/// </summary>
		/// <param name="selectedDateInfo">Selected <see cref="CalendarWidgetDate"/> object</param>
		private void DateSelectCommandHandler()
		{
			//SelectedDateOfInterest = selectedDateInfo.DateTimeData;
			Console.WriteLine("Test");
		}

		/// <summary>
		/// Responsible to handle the logic to be executed when user tries to move back a month
		/// </summary>
		private void MoveWeekBackwardCommandHandler()
		{
			IndicatorDateOfMonth = IndicatorDateOfMonth.AddMonths(-1);
			SetupUiDateInfo();
		}

		/// <summary>
		/// Responsible to handle the logic to be executed when user tries to move forward a month
		/// </summary>
		private void MoveWeekForwardCommandHandler()
		{
			IndicatorDateOfMonth = IndicatorDateOfMonth.AddMonths(+1);
			SetupUiDateInfo();
		}

		/// <summary>
		/// Responsible to set up all data required for UI bindings
		/// </summary>
		private void SetupUiDateInfo()
		{
			IsDateRefreshing = true;
			BuildHeaderYearMonthInfo();
			SetupDateListForMonth();
			IsDateRefreshing = false;
		}

		/// <summary>
		/// Responsible to build and assign header information on Year and month
		/// </summary>
		private void BuildHeaderYearMonthInfo()
		{
			var year = IndicatorDateOfMonth.Year.ToString();
			var month = IndicatorDateOfMonth.ToString("MMMM", CultureInfo.InvariantCulture);

			StringBuilder yearMonthInfo = new StringBuilder(year);
			yearMonthInfo.Append(" / ");
			yearMonthInfo.Append(month);
			YearMonthInfo = yearMonthInfo.ToString();
		}

		/// <summary>
		/// Responsible to set up the dates of the month of interest as a list of <see cref="CalendarWidgetDate"/>
		/// </summary>
		private void SetupDateListForMonth()
		{
			var year = IndicatorDateOfMonth.Year.ToString();
			var month = IndicatorDateOfMonth.Month.ToString();
			_dateInfoList = new List<CalendarWidgetDate>();

			for (var date = new DateTime(int.Parse(year), int.Parse(month), 1); date.Month == int.Parse(month); date = date.AddDays(1))
			{
				CalendarWidgetDate calendarWidgetDate = new CalendarWidgetDate
				{ 
					DateTimeData = date,
					DateRepresentation = date.Day.ToString(),
					DayRepresentation = date.DayOfWeek.ToString().Substring(0, 3)
				};

				if (SelectedDateOfInterest.ToShortDateString().Equals(date.ToShortDateString()))
				{
					calendarWidgetDate.ColorCode = "#368268";
				}

				_dateInfoList.Add(calendarWidgetDate);

				DateList = new ObservableCollection<CalendarWidgetDate>(_dateInfoList);
			}
		}
		#endregion
	}
}
