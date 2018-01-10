namespace ToggleApi.Tests.Unit.Services.AppOverrides
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using ToggleApi.Converters;
    using ToggleApi.Exceptions;
    using ToggleApi.Models.Entities;
    using ToggleApi.Models.Requests;
    using ToggleApi.Models.Responses;
    using ToggleApi.Services;
    using ToggleApi.Services.AppOverrides;
    using Xunit;

    public class AppOverrideCreateServiceTest
    {
        private readonly ApiDbContext context;
        private readonly Mock<IConverter<AppOverrideRequest, AppOverride>> appOverrideConverter;
        private readonly Mock<IConverter<AppOverride, AppOverrideResponse>> responseConverter;
        private readonly ICreateService<AppOverrideRequest, AppOverrideResponse> victim;

        public AppOverrideCreateServiceTest()
        {
            var db = new DbContextOptionsBuilder<ApiDbContext>();
            db.UseInMemoryDatabase("AppOverrideCreateServiceTest");
            context = new ApiDbContext(db.Options);
            appOverrideConverter = new Mock<IConverter<AppOverrideRequest, AppOverride>>();
            responseConverter = new Mock<IConverter<AppOverride, AppOverrideResponse>>();
            victim = new AppOverrideCreateService(context, appOverrideConverter.Object, responseConverter.Object);
        }

        [Fact]
        public void shouldThrowExceptionOnMissingToggle()
        {
            Assert.Throws<ResourceNotFoundException>(() => victim.Create(buildRequest(20L)));
        }

        [Fact]
        public void shouldCreateEntity()
        {
            // arrange
            var request = buildRequest(1L);
            
            var entity = buildEntity();
            appOverrideConverter.Setup(c => c.convert(It.IsAny<AppOverrideRequest>())).Returns(entity);

            var response = new AppOverrideResponse();
            responseConverter.Setup(c => c.convert(It.IsAny<AppOverride>())).Returns(response);

            context.Toggles.Add(new Toggle() { Id = request.ToggleId });
            context.SaveChanges();

            // act
            var actualResponse = victim.Create(request);

            // assert
            var actualEntity = context.AppOverrides.FirstOrDefault(t => t.Id == entity.Id);
            Assert.Equal(entity.ToggleId, actualEntity.ToggleId);
            Assert.Equal(entity.ToggleName, actualEntity.ToggleName);
            Assert.Equal(entity.Application, actualEntity.Application);
            Assert.Equal(entity.Value, actualEntity.Value);
            Assert.Equal(response, actualResponse);
        }

        private AppOverrideRequest buildRequest(long toggleId)
        {
            return new AppOverrideRequest()
            {
                ToggleId = toggleId,
                ToggleName = "toggle",
                Application = "app",
                Value = true
            };
        }

        private AppOverride buildEntity()
        {
            return new AppOverride()
            {
                Id = 1L,
                ToggleId = 1L,
                ToggleName = "toggle",
                Application = "app",
                Value = true
            };
        }
    }
}
