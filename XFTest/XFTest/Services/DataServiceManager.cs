using System;
using System.Collections.Generic;
using System.Text;
using XFTest.Models;

namespace XFTest.Services
{
	/**
	 * Acts as a mediator to data services sub-system.
	 */
	public class DataServiceManager
	{
		public List<CarWashTask> OrchestrateContextualDataPopulation()
		{
			return new JobDetailStorageService().PopulateContextualData(new FetchDataService().FetchClientData()); 
		}
	}
}
