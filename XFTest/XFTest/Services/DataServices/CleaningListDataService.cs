using System.Collections.Generic;
using System.Threading.Tasks;
using XFTest.Dtos;
using XFTest.Models;
using XFTest.Services.DataMappingServices;

namespace XFTest.DataServices
{
	/// <summary>
	/// Entity responsible to manage data services for <see cref="CleaningListJobItem"/>
	/// </summary>
	public class CleaningListDataService : IDataFetchService<CleaningListJobItem>
	{
		private IDataFetchService<CarFitClientDto> _carFitClientDtoDataService;

		private IEntityListMappingService<CarFitClientDto, CleaningListJobItem> _carFitClientListCleaningListMappingService;

		/// <summary>
		/// Creates an instance of <see cref="CleaningListDataService"/>
		/// </summary>
		/// <param name="carFitClientDtoDataService"></param>
		/// <param name="carFitClientListCleaningListMappingService"></param>
		public CleaningListDataService(
			IDataFetchService<CarFitClientDto> carFitClientDtoDataService,
			IEntityListMappingService<CarFitClientDto, CleaningListJobItem> carFitClientListCleaningListMappingService)
		{
			_carFitClientDtoDataService = carFitClientDtoDataService;
			_carFitClientListCleaningListMappingService = carFitClientListCleaningListMappingService;
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public async Task<List<CleaningListJobItem>> FetchDataForEntityAsync()
		{
			var dataFromServer = await _carFitClientDtoDataService.FetchDataForEntityAsync();
			return _carFitClientListCleaningListMappingService.MapSourceToTarget(dataFromServer);
		}
	}
}
