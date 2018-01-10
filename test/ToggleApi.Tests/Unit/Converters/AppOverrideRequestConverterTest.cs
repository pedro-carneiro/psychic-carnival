namespace ToggleApi.Tests.Unit.Converters
{
    using ToggleApi.Converters;
    using ToggleApi.Models.Entities;
    using ToggleApi.Models.Requests;
    using Xunit;

    public class AppOverrideRequestConverterTest
    {
        private readonly IConverter<AppOverrideRequest, AppOverride> subject;

        public AppOverrideRequestConverterTest()
        {
            subject = new AppOverrideRequestConverter();
        }

        [Fact]
        public void shouldVerifyConversion()
        {
            long toggleId = 10L;
            string toggleName = "someToggle";
            string application = "someApp";
            bool value = true;
            var convertee = new AppOverrideRequest()
            {
                ToggleId = toggleId,
                ToggleName = toggleName,
                Application = application,
                Value = value
            };
            
            var converted = subject.convert(convertee);

            Assert.Equal(toggleId, converted.ToggleId);
            Assert.Equal(toggleName, converted.ToggleName);
            Assert.Equal(application, converted.Application);
            Assert.Equal(value, converted.Value);
        }
    }
}
