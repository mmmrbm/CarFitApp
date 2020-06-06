using System;
using System.Collections.Generic;
using System.Text;
using XFTest.Services.LocationServices;

namespace XFTest.Util
{
	public class DataConvertHelper
	{
		private static string _space = " ";

		private static string _comma = ",";

		private IDistanceCalculationService _distanceCalculationService;

		public DataConvertHelper(IDistanceCalculationService distanceCalculationService)
		{
			_distanceCalculationService = distanceCalculationService;
		}

		public string BuildClientName(string firstName, string lastName)
		{
			StringBuilder resultingStringBuilder = new StringBuilder(firstName);
			resultingStringBuilder.Append(_space);
			resultingStringBuilder.Append(lastName);

			return resultingStringBuilder.ToString();
		}

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

		public string BuildExpectedTimeInfo(int timeInMin)
		{
			StringBuilder resultingStringBuilder = new StringBuilder(timeInMin.ToString());
			resultingStringBuilder.Append(_space);
			resultingStringBuilder.Append("min");

			return resultingStringBuilder.ToString();
		}

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

		public string BuildDistanceInfo(double distance)
		{
			StringBuilder resultingStringBuilder = new StringBuilder(distance.ToString());
			resultingStringBuilder.Append(_space);
			resultingStringBuilder.Append("km");

			return resultingStringBuilder.ToString();
		}
	}
}
