namespace ToggleApi.Models.Requests
{
    public class AppOverrideRequest
    {
        public long ToggleId { get; set; }
        public string ToggleName { get; set; }
        public string Application { get; set; }
        public bool Value { get; set; }
    }
}
