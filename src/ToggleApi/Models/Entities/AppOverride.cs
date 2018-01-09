namespace ToggleApi.Models.Entities
{
    public class AppOverride
    {
        public long Id { get; set; }
        public long ToggleId { get; set; }
        public string Application { get; set; }
        public bool Value { get; set; }
    }
}
