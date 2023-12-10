using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnePage.SendEmail.Resources;
using System.Collections;
using System.Reflection;
using System.Resources;

namespace OnePage.SendEmail.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly ResourceManager _resourceManager;

        public MeetingController()
        {
           
            _resourceManager = new ResourceManager("OnePage.SendEmail.Resources.MeetingSheduled", Assembly.GetExecutingAssembly());
        }

        [HttpGet("meeting-scheduled")]
        public ActionResult<IEnumerable<KeyValuePair<string, string>>> GetMeetingScheduled()
        {
            return Ok(FilterUndesiredValues(GetResourceData("MeetingSheduled")));
        }

        [HttpGet("new-people-to-meet")]
        public ActionResult<IEnumerable<KeyValuePair<string, string>>> GetNewPeopleToMeet()
        {
            return Ok(FilterUndesiredValues(GetResourceData("NewPeopleToMet")));
        }

        [HttpGet("people-to-meet")]
        public ActionResult<IEnumerable<KeyValuePair<string, string>>> GetPeopleToMeet()
        {
            return Ok(FilterUndesiredValues(GetResourceData("PeopleToMet")));
        }

        private IEnumerable<KeyValuePair<string, string>> GetResourceData(string resourceName)
        {
            var resourceType = Type.GetType($"OnePage.SendEmail.Resources.{resourceName}, OnePage.SendEmail");
            var resourceProperties = resourceType?.GetProperties();

            if (resourceProperties == null)
            {
                return Enumerable.Empty<KeyValuePair<string, string>>();
            }

            return resourceProperties
                .Select(property => new KeyValuePair<string, string>(property.Name, property.GetValue(_resourceManager)?.ToString()))
                .ToList();
        }

        
        private IEnumerable<KeyValuePair<string, string>> FilterUndesiredValues(IEnumerable<KeyValuePair<string, string>> data)
        {
            return data.Where(item =>
                !string.Equals(item.Value, "System.Resources.ResourceManager") &&
                !string.IsNullOrWhiteSpace(item.Value) &&  
                item.Key != "Culture"
            );
        }
    }

}



