using System.Collections.Generic;

namespace XFTest.Services.DataMappingServices
{
	/// <summary>
	/// Contract needs to be fulfilled by entities which will be providing entity data mapping services.
	/// Ideally, the logic of a concrete implementation should convert a list of Source entities 
	/// to a list of Target entities.
	/// </summary>
	/// <typeparam name="Source">Source entity type.</typeparam>
	/// <typeparam name="Target">Target entity type.</typeparam>
	public interface IEntityListMappingService<Source, Target>
	{
		/// <summary>
		/// Maps data from Source entity to data of Target entity which transforms source entities 
		/// to target entities.
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		List<Target> MapSourceToTarget(List<Source> inputData);
	}
}
