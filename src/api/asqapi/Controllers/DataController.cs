using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
//using RealTimeCharts_Server.DataStorage;
using asqapi.HubConfig;
using asqapi.TimerFeatures;
//using KafkaClient;

namespace asqapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private IHubContext<DataHub> _hub; 


        public DataController(IHubContext<DataHub> hub) 
        { 
            _hub = hub;
        }

        public IActionResult Get()
        { 
            //var timerManager = new TimerManager(() => _hub.Clients.All.SendAsync("transferdata", TestDataGenerator.GetData())); 
            
            return Ok(new { Message = "Request Completed" }); 
        }
    }
}