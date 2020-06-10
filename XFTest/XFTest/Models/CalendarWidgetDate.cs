using System;

namespace XFTest.Models
{
	/// <summary>
	/// The entity to represent the date related information in the Calendar Widget.
	/// </summary>
	public class CalendarWidgetDate
	{
		private static readonly string _defaultBgColor = "#25A87B";

		private static readonly string _selectedBgColor = "#368268";

		public CalendarWidgetDate()
		{
			ColorCode = _defaultBgColor;
		}

		public DateTime DateTimeData { get; set; }

		public string DateRepresentation { get; set; }

		public string DayRepresentation { get; set; }

		public string ColorCode { get; set; }

		/// <summary>
		/// Returns the default color code used to display <see cref="CalendarWidgetDate"/> in UI
		/// </summary>
		/// <returns>The hex code for the default color</returns>
		public string GetDefaultBgColorCode()
		{
			return _defaultBgColor;
		}

		/// <summary>
		/// Returns the color code used to display when user selects a <see cref="CalendarWidgetDate"/> in UI
		/// </summary>
		/// <returns>The hex code for the selected color</returns>
		public string GetSelectedBgColorCode()
		{
			return _selectedBgColor;
		}
	}
}
