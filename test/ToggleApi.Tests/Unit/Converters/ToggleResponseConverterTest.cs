namespace ToggleApi.Tests.Unit.Converters
{
    using ToggleApi.Converters;
    using ToggleApi.Models.Entities;
    using ToggleApi.Models.Responses;
    using Xunit;

    public class ToggleResponseConverterTest
    {
        private readonly IConverter<Toggle, ToggleResponse> subject;

        public ToggleResponseConverterTest()
        {
            subject = new ToggleResponseConverter();
        }

        [Fact]
        public void shouldVerifyConversion()
        {
            long id = 20L;
            string name = "toggleName";
            bool defaultValue = false;
            var convertee = new Toggle()
            {
                Id = id,
                Name = name,
                DefaultValue = defaultValue
            };

            var converted = subject.convert(convertee);

            Assert.Equal(id, converted.Id);
            Assert.Equal(name, converted.Name);
            Assert.Equal(defaultValue, converted.DefaultValue);
        }
    }
}
