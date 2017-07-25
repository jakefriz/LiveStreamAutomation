using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;

namespace LiveStreamAutomation
{
    public class UiController : ApiController
    {
        //// GET api/values 
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5 
        public string Get(int id)
        {
            switch (id)
            {
                case (0): // Normal Lower Third Operation
                    MainWindow.root._uiChouice = UIChoice.LowerThird;
                    break;
                case (1): // Nothing
                    MainWindow.root._uiChouice = UIChoice.None;
                    break;
                case (2): // Team order for current pool
                    MainWindow.root._uiChouice = UIChoice.PlayOrder;
                    break;
                case (3):  // Results thus for for current pool
                    MainWindow.root._uiChouice = UIChoice.ResultsOrder;
                    break;
                case (4):  // Player Details
                    MainWindow.root._uiChouice = UIChoice.PlayerDetail;
                    break;
                default:
                    break;
            }
            MainWindow.root.WriteNames(null);
            return "Smiles and Rainbows";
        }

        // POST api/values 
        //public void Post([FromBody]string value)
        //{
        //    Team t = JsonConvert.DeserializeObject<Team>(value);
        //    MainWindow.root.WriteNames(t);
        //}

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