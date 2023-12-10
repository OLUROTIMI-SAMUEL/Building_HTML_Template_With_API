using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Net.Mail;
using System.Net;
using OnePage.SendEmail.Models;
using Newtonsoft.Json;

namespace OnePage.SendEmail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimpleEmailServicesController : ControllerBase
    {

        [HttpGet("mail-structure")]
        public IActionResult GetMailStructure()
        {
            try
            {
                // Call the three API endpoints and aggregate the data
                var meetingScheduled = FetchDataFromApi("meeting-scheduled");
                var newPeopleToMeet = FetchDataFromApi("new-people-to-meet");
                var peopleToMeet = FetchDataFromApi("people-to-meet");

                // Create an object with the aggregated data
                var result = new
                {
                    meetingScheduled,
                    newPeopleToMeet,
                    peopleToMeet
                };

                // Return the data in the response
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error fetching data: {ex.Message}");
            }
        }

        private List<string> FetchDataFromApi(string endpoint)
        {
            try
            {
                var response = new WebClient().DownloadString($"https://localhost:7032/api/meeting/{endpoint}");
                var data = JsonConvert.DeserializeObject<List<string>>(response);
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching data for {endpoint}: {ex.Message}");
                return new List<string>();
            }
        }

    }
}

