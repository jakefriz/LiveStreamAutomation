using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;

namespace LiveStreamAutomation
{
	public class TeamListController : ApiController
	{
		//// GET api/values 
		//public IEnumerable<string> Get()
		//{
		//    return new string[] { "value1", "value2" };
		//}

		//// GET api/values/5 
		//public string Get(int id)
		//{
		//    return "value";
		//}

		// POST api/values 
		public void Post(System.Net.Http.HttpRequestMessage httpMessage)
		{
			string value = httpMessage.Content.ReadAsStringAsync().Result;
			MainWindow.root._teamList = JsonConvert.DeserializeObject<TeamList>(value);
		}

		//// PUT api/values/5 
		//public void Put(int id, [FromBody]string value)
		//{
		//}

		//// DELETE api/values/5 
		//public void Delete(int id)
		//{
		//}
	}
}