using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace BikeMatrixTest.Functions
{
    public class UserCRUD
    {
        private readonly ILogger<UserCRUD> _logger;

        public UserCRUD(ILogger<UserCRUD> logger)
        {
            _logger = logger;
        }

        [Function("CreateUser")]
        public IActionResult CreateUser([HttpTrigger(AuthorizationLevel.Function, "post", Route = "user")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }

        [Function("GetUser")]
        [OpenApiOperation(operationId: "GetUser", Description = "Gets the Details of the requested user")]
        [OpenApiParameter(name: "UserID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Userid of the user.")]
        public IActionResult GetUser([HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/{userID}")] HttpRequest req,string userID)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }

        [Function("UpdateUser")]
        [OpenApiOperation(operationId: "UpdateUser", Description = "Updates the details of the current user.")]
        [OpenApiParameter(name: "UserID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Userid of the user.")]
        public IActionResult UpdateUser([HttpTrigger(AuthorizationLevel.Function, "post", Route = "user/{userID}")] HttpRequest req, string userID)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
