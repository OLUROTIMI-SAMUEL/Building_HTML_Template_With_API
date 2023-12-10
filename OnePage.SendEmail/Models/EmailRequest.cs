namespace OnePage.SendEmail.Models
{
    public class EmailRequest
    {
        public string MeetingScheduled { get; set; }
        public string NewPeopleToMeet { get; set; }
        public string PeopleToMeet { get; set; }

        public string ToAddress { get; set; }
    }
}
