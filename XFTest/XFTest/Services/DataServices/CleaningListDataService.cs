using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XFTest.Dtos;
using XFTest.Models;
using XFTest.Services.DataMappingServices;

namespace XFTest.DataServices
{
	public class CleaningListDataService : IDataService<CleaningListJobItem>
	{
		private IDataService<CarFitClientDto> _carFitClientDtoDataService;

		private IEntityListMappingService<CarFitClientDto, CleaningListJobItem> _carFitClientListCleaningListMappingService;

		public CleaningListDataService(
			IDataService<CarFitClientDto> carFitClientDtoDataService,
			IEntityListMappingService<CarFitClientDto, CleaningListJobItem> carFitClientListCleaningListMappingService)
		{
			_carFitClientDtoDataService = carFitClientDtoDataService;
			_carFitClientListCleaningListMappingService = carFitClientListCleaningListMappingService;
		}

		public async Task<List<CleaningListJobItem>> FetchDataForEntityAsync()
		{
			var dataFromServer = await _carFitClientDtoDataService.FetchDataForEntityAsync();
			return _carFitClientListCleaningListMappingService.MapSourceToTarget(dataFromServer);
		}
	}
}
