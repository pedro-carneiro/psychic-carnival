namespace ToggleApi.Tests.Unit.Converters
{
    using ToggleApi.Converters;
    using ToggleApi.Models.Entities;
    using ToggleApi.Models.Requests;
    using Xunit;

    public class ToggleRequestConverterTest
    {
        private readonly IConverter<ToggleRequest, Toggle> subject;

        public ToggleRequestConverterTest()
        {
            subject = new ToggleRequestConverter();
        }

        [Fact]
        public void shouldVerifyConversion()
        {
            long id = 10L;
            string toggleName = "someToggle";
            bool defaultValue = true;
            var convertee = new ToggleRequest()
            {
                Id = id,
                Name = toggleName,
                DefaultValue = defaultValue
            };
            
            var converted = subject.convert(convertee);

            Assert.Equal(id, converted.Id);
            Assert.Equal(toggleName, converted.Name);
            Assert.Equal(defaultValue, converted.DefaultValue);
        }
    }
}
