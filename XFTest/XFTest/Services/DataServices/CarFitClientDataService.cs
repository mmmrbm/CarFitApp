using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using XFTest.Dtos;
using XFTest.Views;

namespace XFTest.DataServices
{
	/**
	 * Class is marked as internal. All invocations should be handled via <see cref="CleaningListDataService"/>
	 * Data reads can use complex and low level methods. Idea is to hide the complexity via a mediator.
	 */
	internal class CarFitClientDataService : IDataService<CarFitClientDto>
	{
		/**
		 * Below code is based on the solutions and information provided on below discussions.
		 * https://stackoverflow.com/questions/60878223/xamarin-forms-read-local-json-file-and-display-in-picker
		 * https://stackoverflow.com/questions/38762368/embedded-resource-in-net-core-libraries
		 */
		public async Task<List<CarFitClientDto>> FetchDataForEntityAsync()
		{
			CarFitResponseDto response = null;
			List<CarFitClientDto> carFitClients = null;

			var assembly = typeof(MainPage).GetTypeInfo().Assembly;

			foreach (var resource in assembly.GetManifestResourceNames())
			{
				if (resource.Contains("XF-Test-Json"))
				{
					Stream stream = assembly.GetManifestResourceStream(resource);

					using (var reader = new StreamReader(stream))
					{
						string data = await reader.ReadToEndAsync();
						response = JsonConvert.DeserializeObject<CarFitResponseDto>(data);

						if (response == null)
						{
							throw new JsonException("Couldn't parse JSON response from Server.");
						}

						carFitClients = response.Clients;
					}
				}
			}

			return carFitClients;
		}
	}
}
