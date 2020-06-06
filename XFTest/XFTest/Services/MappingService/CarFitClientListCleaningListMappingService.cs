using ImTools;
using System;
using System.Collections.Generic;
using System.Text;
using XFTest.Dtos;
using XFTest.Models;

namespace XFTest.Services.MappingService
{
	public class CarFitClientListCleaningListMappingService : IEntityListMappingService<CarFitClientDto, CleaningList>
	{
		private static string _space = " ";
		private static string _comma = ",";

		/**
		 * These mapping operations can be done using packages like https://www.nuget.org/packages/automapper/.
		 * But, as per this link https://mallibone.com/post/xamarin-automapper it introduce some problems in iOS project.
		 * Thus, did this manually for this test.
		 */
		public List<CleaningList> MapSourceToTarget(List<CarFitClientDto> inputData)
		{
			List<CleaningList> cleaningTaskList = new List<CleaningList>();

			foreach (var carFitClientDto in inputData)
			{
				foreach (var task in carFitClientDto.Tasks)
				{
					CleaningList jobItem = new CleaningList()
					{ 
						ClientName = BuildClientName(carFitClientDto.HouseOwnerFirstName, carFitClientDto.HouseOwnerLastName),
						ClientAddressInformation = BuildClientAddressInfo(carFitClientDto.HouseOwnerAddress, carFitClientDto.HouseOwnerCity, carFitClientDto.HouseOwnerZip),
						JobDescription = task.Title,
						TimeInformationOnJob = BuildTimeInformation(carFitClientDto.StartTimeUtc.DateTime, carFitClientDto.ExpectedTime),
						ExpectedTimeAllocatedForJob = BuildExpectedTimeInfo(task.TimesInMinutes),
						DistanceInformation = BuildDistanceInfo(carFitClientDto.HouseOwnerLatitude, carFitClientDto.HouseOwnerLongitude),
						JobStatus = carFitClientDto.VisitState
					};

					cleaningTaskList.Add(jobItem);
				}
			}

			return cleaningTaskList;
		}

		private string BuildClientName(string firstName, string lastName)
		{
			StringBuilder resultingStringBuilder = new StringBuilder(firstName);
			resultingStringBuilder.Append(_space);
			resultingStringBuilder.Append(lastName);

			return resultingStringBuilder.ToString();
		}

		private string BuildClientAddressInfo(string address, string city, string zipCode)
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

		private string BuildTimeInformation(DateTime startTime, string expectedTime)
		{
			StringBuilder resultingStringBuilder = new StringBuilder(startTime.ToString("hh:mm"));

			if (expectedTime != null)
			{
				resultingStringBuilder.Append(" / ");
				resultingStringBuilder.Append(expectedTime.Replace('/', '-'));
			}

			return resultingStringBuilder.ToString();
		}

		private string BuildExpectedTimeInfo(int timeInMin)
		{
			StringBuilder resultingStringBuilder = new StringBuilder(timeInMin.ToString());
			resultingStringBuilder.Append(_space);
			resultingStringBuilder.Append("min");

			return resultingStringBuilder.ToString();
		}

		private string BuildDistanceInfo(double latitude, double longitude)
		{
			double distance = CalculateDistance(latitude, longitude);

			StringBuilder resultingStringBuilder = new StringBuilder(distance.ToString());
			resultingStringBuilder.Append(_space);
			resultingStringBuilder.Append("km");

			return resultingStringBuilder.ToString();
		}

		private double CalculateDistance(double latitude, double longitude)
		{
			return 5.2;
		}
	}
}
