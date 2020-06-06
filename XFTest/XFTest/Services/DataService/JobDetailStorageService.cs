using System;
using System.Collections.Generic;
using System.Text;
using XFTest.Dtos;
using XFTest.Models;

namespace XFTest.Services
{
	internal class JobDetailStorageService
	{
		// Marked as static to make the list behave as a DB table.
		private static List<CarWashTask> _carWashTasks;

		internal JobDetailStorageService()
		{
			_carWashTasks = new List<CarWashTask>();
		}

		internal List<CarWashTask> PopulateContextualData(List<CarFitClientDto> dataFromServer)
		{
			foreach (var carFitClientDto in dataFromServer)
			{
				foreach (var task in carFitClientDto.Tasks)
				{
					CarWashVisit carWashVisitData = new CarWashVisit()
					{ 
						ClientFirstName = carFitClientDto.HouseOwnerFirstName,
						ClientLastName = carFitClientDto.HouseOwnerLastName,
						ClientContactNo = carFitClientDto.HouseOwnerMobilePhone,
						ClientAddress = carFitClientDto.HouseOwnerAddress,
						ClientCity = carFitClientDto.HouseOwnerCity,
						ClientZipCode = carFitClientDto.HouseOwnerZip,
						ClientLocationLatitude = carFitClientDto.HouseOwnerLatitude,
						ClientLocationLongitude = carFitClientDto.HouseOwnerLongitude,
						StartDateTime = carFitClientDto.StartTimeUtc.DateTime,
						EndDateTime = carFitClientDto.EndTimeUtc.DateTime,
						ExpectedCompletionTime = carFitClientDto.ExpectedTime,
						JobState = carFitClientDto.VisitState
					};

					CarWashTask carWashTask = new CarWashTask() 
					{ 
						TaskId = task.TaskId,
						TaskDetail = task.Title,
						PlannedTimeSpan = task.TimesInMinutes,
						Price = task.Price,
						CarWashVisit = carWashVisitData
					};

					_carWashTasks.Add(carWashTask);
				}
			}

			return _carWashTasks;
		}

	}
}
