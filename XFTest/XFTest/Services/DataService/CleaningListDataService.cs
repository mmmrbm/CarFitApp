using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XFTest.Dtos;
using XFTest.Models;
using XFTest.Services.MappingService;

namespace XFTest.Services
{
	public class CleaningListDataService : IDataService<CleaningList>
	{
		private IDataService<CarFitClientDto> _carFitClientDtoDataService;

		private IEntityListMappingService<CarFitClientDto, CleaningList> _carFitClientListCleaningListMappingService;

		public CleaningListDataService(
			IDataService<CarFitClientDto> carFitClientDtoDataService,
			IEntityListMappingService<CarFitClientDto, CleaningList> carFitClientListCleaningListMappingService)
		{
			_carFitClientDtoDataService = carFitClientDtoDataService;
			_carFitClientListCleaningListMappingService = carFitClientListCleaningListMappingService;
		}

		public async Task<List<CleaningList>> FetchDataForEntityAsync()
		{
			var dataFromServer = await _carFitClientDtoDataService.FetchDataForEntityAsync();
			return _carFitClientListCleaningListMappingService.MapSourceToTarget(dataFromServer);
		}
	}
}
