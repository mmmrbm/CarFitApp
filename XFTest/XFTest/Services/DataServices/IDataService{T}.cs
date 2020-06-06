using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XFTest.DataServices
{
	public interface IDataService<T>
	{
		Task<List<T>> FetchDataForEntityAsync();
	}
}
