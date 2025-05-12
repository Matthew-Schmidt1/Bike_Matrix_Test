using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace BikeMatrixTest.Functions
{
    public class BikeCRUD
    {
        private readonly ILogger<BikeCRUD> _logger;

        public BikeCRUD(ILogger<BikeCRUD> logger)
        {
            _logger = logger;
        }

        [Function("CreateBike")]
        [OpenApiOperation(operationId: "CreateBike", Description = "Creates a Bike for the Given USERID")]
        [OpenApiParameter(name: "UserID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Userid of the user that the bike is created for.")]
        //[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Bike),
        public IActionResult CreateBike([HttpTrigger(AuthorizationLevel.Function,"post", Route = "{UserID}/bike")] HttpRequest req,string userID)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }


        [Function("GetBike")]
        [OpenApiOperation(operationId: "GetBike", Description = "Gets a Bike for the Given USERID")]
        [OpenApiParameter(name: "UserID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Userid of the user")]
        [OpenApiParameter(name: "BikeID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Bikeid  of the Bike")]
        public IActionResult GetBike([HttpTrigger(AuthorizationLevel.Function, "get", Route = "{UserID}/bike/{BikeID}")] HttpRequest req,string UserID, string BikeID)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }

        [Function("UpdateBike")]
        [OpenApiOperation(operationId: "UpdateBike", Description = "Gets a Bike for the Given USERID")]
        [OpenApiParameter(name: "UserID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Userid of the user")]
        [OpenApiParameter(name: "BikeID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Bikeid  of the Bike")]
        public IActionResult UpdateBike([HttpTrigger(AuthorizationLevel.Function, "post", Route = "{UserID}/bike/{BikeID}")] HttpRequest req, string UserID,string BikeID)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }

        [Function("DeleteBike")]
        [OpenApiOperation(operationId: "DeleteBike", Description = "Gets a Bike for the Given USERID")]
        [OpenApiParameter(name: "UserID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Userid of the user")]
        [OpenApiParameter(name: "BikeID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Bikeid  of the Bike")]
        public IActionResult DeleteBike([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "{UserID}/bike/{BikeID}")] HttpRequest req, string UserID, string BikeID)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }

        [Function("DeleteBikeByID")]
        [OpenApiOperation(operationId: "DeleteBike", Description = "Gets a Bike for the Given USERID")]
        [OpenApiParameter(name: "BikeID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Bikeid  of the Bike")]
        public IActionResult DeleteBikeByID([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "bike/{BikeID}")] HttpRequest req, string UserID, string BikeID)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }

        [Function("GetAllBike")]
        [OpenApiOperation(operationId: "GetAllBike", Description = "Creates a Bike for the Given USERID")]
        [OpenApiParameter(name: "UserID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Userid of the user.")]
        public IActionResult GetAllBike([HttpTrigger(AuthorizationLevel.Function, "get", Route = "{UserID}/bike")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
