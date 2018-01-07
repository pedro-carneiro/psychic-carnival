namespace ToggleApi.Models.Requests
{
    public class ToggleRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool DefaultValue { get; set; }
    }
}
