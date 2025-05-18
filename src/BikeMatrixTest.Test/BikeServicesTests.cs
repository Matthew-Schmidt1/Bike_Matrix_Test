using BikeMatrixModels;
using BikeMatrixTest.Exceptions;
using BikeMatrixTest.Interfaces;
using BikeMatrixTest.Services;
using Dapper;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;

namespace BikeMatrixTest.Test
{
    [TestClass]
    public sealed class BikeServicesTests
    {
        [TestMethod]
        public async Task NotValidEmailAsync()
        {
            // Arrange
            BikeServices servicesUnderTest = GetService();

            try
            {
                // Act
                await servicesUnderTest.createBikeAsync(new Bikes { Brand = "testOne", EmailAddress = "thisIsNot A Email Address", Model = "mod", YearOfManufactor = 2018 });
                Assert.Fail("Should not reach this code");
            }
            catch (BikeMatirxValidationExceptions ex)
            {
                Assert.IsNotNull(ex.Errors);
                Assert.IsTrue(ex.Errors[0].Contains("EmailAddress"));
            }
        }

        [TestMethod]
        public async Task TestForSqlInjection()
        {
            // Arrange
            BikeServices servicesUnderTest = GetService();

            try
            {
                // Act
                await servicesUnderTest.createBikeAsync(new Bikes { Brand = "SELECT * FROM Users WHERE Username = ' ' OR 1=1 -- '", EmailAddress = "Example@example.com", Model = "mod", YearOfManufactor = 2018 });
                Assert.Fail("Should not reach this code");
            }
            catch (BikeMatirxValidationExceptions ex)
            {
                Assert.IsNotNull(ex.Errors);
                Assert.IsTrue(ex.Errors[0].Contains("Potential SQL injection detected"));
            }
        }

        private static BikeServices GetService()
        {
            // Arrange
            var fakesqlFactory = A.Fake<ISqlConnectionFactory>();
            var fakeConnection = A.Fake<IDbConnection>();
            var fakeCommand = A.Fake<IDbCommand>();
            var fakeReader = A.Fake<IDataReader>();

            // Setup command behavior
            A.CallTo(() => fakeConnection.CreateCommand()).Returns(fakeCommand);

            var inMemorySettings = new Dictionary<string, string>
                {
                    { "LocalDbConnection", " " },
                    { "AppSettings:Timeout", "30" }
                };
            var fakeConfiguration = new ConfigurationBuilder()
                        .AddInMemoryCollection(inMemorySettings)
                    .Build();

            A.CallTo(() => fakesqlFactory.GetSqlConnection(A<string>.Ignored)).Returns(fakeConnection);

            BikeServices servicesUnderTest = new BikeServices(fakeConfiguration, A.Fake<ILogger<BikeServices>>(), fakesqlFactory);
            return servicesUnderTest;
        }

        [TestMethod]
        public async Task InvalidModel()
        {
            // Arrange
            BikeServices servicesUnderTest = GetService();

            try
            {
                // Act
                await servicesUnderTest.createBikeAsync(new Bikes { Brand = "testOne", EmailAddress = "Example@example.com", Model = " ", YearOfManufactor = 2018 });
                Assert.Fail("Should not reach this code");
            }
            catch (BikeMatirxValidationExceptions ex)
            {
                Assert.IsNotNull(ex.Errors);
                Assert.IsTrue(ex.Errors[0].Contains("required."));
                Assert.IsTrue(ex.Errors[0].Contains("Model"));
            }
        }

        [TestMethod]
        public async Task InvalidBrand()
        {
            // Arrange
            BikeServices servicesUnderTest = GetService();

            try
            {
                // Act
                await servicesUnderTest.createBikeAsync(new Bikes { Brand = " ", EmailAddress = "Example@example.com", Model = "BMX", YearOfManufactor = 2018 });
                Assert.Fail("Should not reach this code");
            }
            catch (BikeMatirxValidationExceptions ex)
            {
                Assert.IsNotNull(ex.Errors);
                Assert.IsTrue(ex.Errors[0].Contains("required."));
                Assert.IsTrue(ex.Errors[0].Contains("Brand"));
            }
        }

        [TestMethod]
        public async Task InvalidYear()
        {
            // Arrange
            BikeServices servicesUnderTest = GetService();

            try
            {
                // Act
                await servicesUnderTest.createBikeAsync(new Bikes { Brand = "BMX", EmailAddress = "Example@example.com", Model = "BMX", YearOfManufactor = 1799 });
                Assert.Fail("Should not reach this code");
            }
            catch (BikeMatirxValidationExceptions ex)
            {
                Assert.IsNotNull(ex.Errors);
                Assert.IsTrue(ex.Errors[0].Contains("YearOfManufactor must be at least 1800"));
            }
        }

      
        
    }
}

