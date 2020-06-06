using System;
using System.Collections.Generic;
using System.Text;

namespace XFTest.Services.MappingService
{
	public interface IEntityListMappingService<Source, Target>
	{
		List<Target> MapSourceToTarget(List<Source> inputData);
	}
}
