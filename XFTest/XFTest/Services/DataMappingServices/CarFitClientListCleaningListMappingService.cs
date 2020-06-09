﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using XFTest.Dtos;
using XFTest.Models;
using XFTest.Util;

namespace XFTest.Services.DataMappingServices
{
	public class CarFitClientListCleaningListMappingService : IEntityListMappingService<CarFitClientDto, CleaningListJobItem>
	{
		private DataConvertHelper _dataConvertHelper;

		public CarFitClientListCleaningListMappingService(
			DataConvertHelper dataConvertHelper)
		{
			_dataConvertHelper = dataConvertHelper;
		}

		/**
		 * These mapping operations can be done using nuget packages like https://www.nuget.org/packages/automapper/.
		 * But, as per this link https://mallibone.com/post/xamarin-automapper it introduce some problems in iOS project.
		 * Thus, did this manually for this excercise.
		 */
		public List<CleaningListJobItem> MapSourceToTarget(List<CarFitClientDto> inputData)
		{
			List<CleaningListJobItem> cleaningTaskList = new List<CleaningListJobItem>();

			foreach (var carFitClientDto in inputData)
			{
				foreach (var task in carFitClientDto.Tasks)
				{
					CleaningListJobItem jobItem = new CleaningListJobItem()
					{
						ClientName = _dataConvertHelper.BuildClientName(carFitClientDto.HouseOwnerFirstName, carFitClientDto.HouseOwnerLastName),
						ClientAddressInformation = _dataConvertHelper.BuildClientAddressInfo(carFitClientDto.HouseOwnerAddress, carFitClientDto.HouseOwnerCity, carFitClientDto.HouseOwnerZip),
						JobDescription = task.Title,
						JobStartTime = carFitClientDto.StartTimeUtc.DateTime,
						ExpectedTimeSlotOfAvailablity = carFitClientDto.ExpectedTime,
						ExpectedTimeAllocatedForJob = task.TimesInMinutes,
						ExpectedTimeAllocatedInfo = _dataConvertHelper.BuildExpectedTimeInfo(task.TimesInMinutes),
						ClientLocationLatitude = carFitClientDto.HouseOwnerLatitude,
						ClientLocationLongitude = carFitClientDto.HouseOwnerLongitude,
						JobStatus = carFitClientDto.VisitState,
						JobBackgroundColor = _dataConvertHelper.FetchBackgroundForState(carFitClientDto.VisitState)
					};

					cleaningTaskList.Add(jobItem);
				}
			}

			cleaningTaskList = PostProcessDataList(cleaningTaskList);

			return cleaningTaskList;
		}

		
		private List<CleaningListJobItem> PostProcessDataList(List<CleaningListJobItem> inputList)
		{
			for (int i = 0; i < inputList.Count; i++)
			{
				CleaningListJobItem currentJob = inputList[i];

				if (i == 0)
				{
					currentJob.DistanceInformation = _dataConvertHelper.BuildDistanceInfo(0);
					currentJob.TimeInformationOnJob = _dataConvertHelper.BuildTimeInformation(currentJob.JobStartTime, currentJob.ExpectedTimeSlotOfAvailablity);
						
				}
				else
				{
					CleaningListJobItem previousJob = inputList[i - 1];

					if (currentJob.JobStartTime.ToShortDateString().Equals(previousJob.JobStartTime.ToShortDateString()))
					{
						currentJob.DistanceInformation = _dataConvertHelper.BuildDistanceInfo(_dataConvertHelper.CalculateDistance(
						currentJob.ClientLocationLatitude,
						currentJob.ClientLocationLongitude,
						previousJob.ClientLocationLatitude,
						previousJob.ClientLocationLongitude));

						currentJob.JobStartTime = previousJob.JobStartTime.AddMinutes(previousJob.ExpectedTimeAllocatedForJob);
						currentJob.TimeInformationOnJob = _dataConvertHelper.BuildTimeInformation(currentJob.JobStartTime, currentJob.ExpectedTimeSlotOfAvailablity);
					}
					else 
					{
						currentJob.DistanceInformation = _dataConvertHelper.BuildDistanceInfo(0);
						currentJob.TimeInformationOnJob = _dataConvertHelper.BuildTimeInformation(currentJob.JobStartTime, currentJob.ExpectedTimeSlotOfAvailablity);
					}
				}

				// To add space after the cap in the status. Length of InProgress job status was too long for the UI element, thus removed
				// currentJob.JobStatus = Regex.Replace(currentJob.JobStatus, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
			}

			return inputList;
		}
	}
}
