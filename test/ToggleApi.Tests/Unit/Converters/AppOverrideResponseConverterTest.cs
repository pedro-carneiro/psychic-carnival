namespace ToggleApi.Tests.Unit.Converters
{
    using ToggleApi.Converters;
    using ToggleApi.Models.Entities;
    using ToggleApi.Models.Responses;
    using Xunit;

    public class AppOverrideResponseConverterTest
    {
        private readonly IConverter<AppOverride, AppOverrideResponse> subject;

        public AppOverrideResponseConverterTest()
        {
            subject = new AppOverrideResponseConverter();
        }

        [Fact]
        public void shouldVerifyConversion()
        {
            long toggleId = 20L;
            string toggleName = "toggleName";
            bool value = false;
            var convertee = new AppOverride()
            {
                ToggleId = toggleId,
                ToggleName = toggleName,
                Value = value
            };

            var converted = subject.convert(convertee);

            Assert.Equal(toggleId, converted.ToggleId);
            Assert.Equal(toggleName, converted.ToggleName);
            Assert.Equal(value, converted.Value);
        }
    }
}
