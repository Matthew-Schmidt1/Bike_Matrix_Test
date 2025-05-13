using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using BikeMatrixModels;
using Newtonsoft.Json;
using BikeMatrixTest.Interfaces;

namespace BikeMatrixTest.Functions
{
    public class BikeCRUD
    {
        private readonly ILogger<BikeCRUD> _logger;
        private readonly IBikeServices bikeServices;
        public BikeCRUD(ILogger<BikeCRUD> logger)
        {
            _logger = logger;
        }

        [Function("CreateBike")]
        [OpenApiOperation(operationId: "CreateBike", Description = "Creates a Bike")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Bikes), Description = "")]
        [OpenApiRequestBody(contentType: "Json", bodyType:typeof(Bikes))]
        public async Task<IActionResult> CreateBikeAsync([HttpTrigger(AuthorizationLevel.Function,"post", Route = "bike")] HttpRequest req,string userID)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Bikes? bike = JsonConvert.DeserializeObject<Bikes>(requestBody);
           
            if (bike == null)
                return new BadRequestObjectResult("Invalid request body");
            await bikeServices.createBikeAsync(bike);
            return new OkObjectResult("Welcome to Azure Functions!");
        }


        [Function("GetBike")]
        [OpenApiOperation(operationId: "GetBike", Description = "Gets a Bike for the Given USERID")]
        [OpenApiParameter(name: "BikeID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Bikeid  of the Bike")]
        public IActionResult GetBike([HttpTrigger(AuthorizationLevel.Function, "get", Route = "bike/{BikeID}")] HttpRequest req,string UserID, string BikeID)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }

        [Function("UpdateBike")]
        [OpenApiOperation(operationId: "UpdateBike", Description = "Gets a Bike for the Given USERID")]
        [OpenApiParameter(name: "BikeID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Bikeid  of the Bike")]
        [OpenApiRequestBody(contentType: "Json", bodyType: typeof(Bikes))]
        public async Task<IActionResult> UpdateBikeAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "bike/{BikeID}")] HttpRequest req, string UserID,string BikeID)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Bikes? user = JsonConvert.DeserializeObject<Bikes>(requestBody);
            return new OkObjectResult("Welcome to Azure Functions!");
        }

        [Function("DeleteBike")]
        [OpenApiOperation(operationId: "DeleteBike", Description = "Gets a Bike for the Given USERID")]
        [OpenApiParameter(name: "BikeID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Bikeid  of the Bike")]
        public IActionResult DeleteBike([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "bike/{BikeID}")] HttpRequest req, string UserID, string BikeID)
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
        public IActionResult GetAllBike([HttpTrigger(AuthorizationLevel.Function, "get", Route = "/bike")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }

        
    }
}
