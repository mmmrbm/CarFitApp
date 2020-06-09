using System;
using System.Text;
using XFTest.Services.LocationServices;

namespace XFTest.Util
{
	/// <summary>
	/// A helper class which holds logic to manipulate data of <see cref="CarFitClientDto"/> to
	/// populate data items of <see cref="CleaningListJobItem"/>
	/// </summary>
	public class CarFitClientCleaningListJobItemDataConvertHelper
	{
		private static readonly string _space = " ";

		private static readonly string _comma = ",";

		private readonly IDistanceCalculationService _distanceCalculationService;

		/// <summary>
		/// Constructs <see cref="CarFitClientCleaningListJobItemDataConvertHelper"/>
		/// </summary>
		/// <param name="distanceCalculationService">An implmentation of <see cref="IDistanceCalculationService"/></param>
		public CarFitClientCleaningListJobItemDataConvertHelper(IDistanceCalculationService distanceCalculationService)
		{
			_distanceCalculationService = distanceCalculationService;
		}

		/// <summary>
		/// Responsible to build the name of the client to be displayed in the app.
		/// </summary>
		/// <param name="firstName">First name of the client</param>
		/// <param name="lastName">Last name of the client</param>
		/// <returns></returns>
		public string BuildClientName(string firstName, string lastName)
		{
			StringBuilder resultingStringBuilder = new StringBuilder(firstName);
			resultingStringBuilder.Append(_space);
			resultingStringBuilder.Append(lastName);

			return resultingStringBuilder.ToString();
		}

		/// <summary>
		/// Responsible to build the address of the client to be displayed in the app.
		/// </summary>
		/// <param name="address">Address line of the client</param>
		/// <param name="city">City of the client</param>
		/// <param name="zipCode">Zip Code of the city of the client</param>
		/// <returns></returns>
		public string BuildClientAddressInfo(string address, string city, string zipCode)
		{
			StringBuilder resultingStringBuilder = new StringBuilder(address);
			resultingStringBuilder.Append(_space);
			resultingStringBuilder.Append(_comma);
			resultingStringBuilder.Append(city);
			resultingStringBuilder.Append(_space);
			resultingStringBuilder.Append(_comma);
			resultingStringBuilder.Append(zipCode);

			return resultingStringBuilder.ToString();
		}

		/// <summary>
		/// Responsible to build the time related information of the job item to be displayed in the app.
		/// </summary>
		/// <param name="startTime">Start time of the job</param>
		/// <param name="expectedTime">Expected time of arrival for the job</param>
		/// <returns>Formatted string for time information to be displayed in the app</returns>
		public string BuildTimeInformation(DateTime startTime, string expectedTime)
		{
			StringBuilder resultingStringBuilder = new StringBuilder(startTime.ToString("hh:mm"));

			if (expectedTime != null)
			{
				resultingStringBuilder.Append(" / ");
				resultingStringBuilder.Append(expectedTime.Replace('/', '-'));
			}

			return resultingStringBuilder.ToString();
		}

		/// <summary>
		/// Responsible to build the expected time related information of the job item to be displayed in the app.
		/// </summary>
		/// <param name="timeInMin">The time planned for the job</param>
		/// <returns>Formatted string on expected time information to be displayed in the app</returns>
		public string BuildExpectedTimeInfo(int timeInMin)
		{
			StringBuilder resultingStringBuilder = new StringBuilder(timeInMin.ToString());
			resultingStringBuilder.Append(_space);
			resultingStringBuilder.Append("min");

			return resultingStringBuilder.ToString();
		}

		/// <summary>
		/// Responsible to set the color to denote the status of the job.
		/// </summary>
		/// <param name="jobState">State of the job</param>
		/// <returns>Color in hexcode for the job</returns>
		public string FetchBackgroundForState(string jobState)
		{
			if (jobState == "ToDo")
			{
				return "#4E77D6";
			}
			else if (jobState == "InProgress")
			{
				return "#F5C709";
			}
			else if (jobState == "Done")
			{
				return "#25A87B";
			}
			else if (jobState == "Rejected")
			{
				return "#EF6565";
			}
			else
			{
				return "#000000";
			}
		}

		/// <summary>
		/// Responsible to calculate the distance between job locations.
		/// </summary>
		/// <param name="currentJobLatitude">Latitude of current job location</param>
		/// <param name="currentJobLongitude">Longitude of current job location</param>
		/// <param name="previousJobLatitude">Latitude of previous job location</param>
		/// <param name="previousJobLongitude">Longitude of previous job location</param>
		/// <returns>The distance between the current and previous job locations.</returns>
		public double CalculateDistance(
			double currentJobLatitude, 
			double currentJobLongitude,
			double previousJobLatitude,
			double previousJobLongitude)
		{
			return _distanceCalculationService.CalculateDistanceByLangLat(
				currentJobLatitude,
				currentJobLongitude,
				previousJobLatitude,
				previousJobLongitude,
				'K');
		}

		/// <summary>
		/// Responsible to build the distance information to be displayed in the app.
		/// </summary>
		/// <param name="distance">Distance between to locations</param>
		/// <returns>Formatted string on distance info to be displayed in the app</returns>
		public string BuildDistanceInfo(double distance)
		{
			StringBuilder resultingStringBuilder = new StringBuilder(distance.ToString());
			resultingStringBuilder.Append(_space);
			resultingStringBuilder.Append("km");

			return resultingStringBuilder.ToString();
		}
	}
}
