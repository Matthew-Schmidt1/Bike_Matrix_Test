using BikeMatrixModels;
using BikeMatrixTest.Exceptions;
using BikeMatrixTest.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Net;

namespace BikeMatrixTest.Functions
{
    public class BikeCRUD
    {
        private readonly ILogger<BikeCRUD> _logger;
        private readonly IBikeServices _bikeServices;
        public BikeCRUD(ILogger<BikeCRUD> logger, IBikeServices bikeServices)
        {
            _logger = logger;
            _bikeServices = bikeServices;
        }

        [Function("CreateBike")]
        [OpenApiOperation(operationId: "CreateBike", Description = "Creates a Bike")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Bikes), Description = "")]
        [OpenApiRequestBody(contentType: "Json", bodyType: typeof(Bikes))]
        [OpenApiResponseWithoutBody(HttpStatusCode.Created, Description = "The Bike has been Created.")]
        [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest, Description = "Any Validation Errors.")]
        public async Task<IActionResult> CreateBikeAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "bike")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Bikes? bike = JsonConvert.DeserializeObject<Bikes>(requestBody);
           
            if (bike == null)
                return new BadRequestObjectResult("Invalid request body");
            try
            {
                await _bikeServices.createBikeAsync(bike);
                return new CreatedResult();
            }
            catch (BikeMatirxValidationExceptions ex)
            {
                return new BadRequestObjectResult(ex.Errors);
            }
        }


        [Function("GetBike")]
        [OpenApiOperation(operationId: "GetBike", Description = "Gets a Bike for the Given USERID")]
        [OpenApiParameter(name: "BikeID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Bikeid  of the Bike")]
        [OpenApiResponseWithoutBody(HttpStatusCode.OK, Description = "Returns the request bike")]
        [OpenApiResponseWithoutBody(HttpStatusCode.NotFound, Description = "Entry not found")]
        public async Task<IActionResult> GetBikeAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "bike/{BikeID}")] HttpRequest req, string BikeID)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            int ValidateBikeId = int.Parse(BikeID);
            if (ValidateBikeId > 0)
            {
                var bike = await _bikeServices.GetBikeAsync(ValidateBikeId);
                if (bike != null)
                {
                    return new OkObjectResult(bike);
                }
                else
                {
                    return new NotFoundResult();
                }
            }

            return new BadRequestObjectResult("Invalid Bike");
        }

        [Function("UpdateBike")]
        [OpenApiOperation(operationId: "UpdateBike", Description = "Gets a Bike for the Given USERID")]
        [OpenApiParameter(name: "BikeID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Bikeid  of the Bike")]
        [OpenApiRequestBody(contentType: "Json", bodyType: typeof(Bikes))]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "json", typeof(Bikes), Description = "The Updated Bike")]
        public async Task<IActionResult> UpdateBikeAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "bike/{BikeID}")] HttpRequest req, string BikeID)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Bikes? bike = JsonConvert.DeserializeObject<Bikes>(requestBody);
            if (bike == null)
                return new BadRequestObjectResult("Invalid request body");
            try
            {
                var updatedBike = await _bikeServices.UpdateBikeAsync(bike);
                return new OkObjectResult(updatedBike);
            }
            catch (BikeMatirxValidationExceptions ex)
            {
                return new BadRequestObjectResult(ex.Errors);
            }

        }

        [Function("DeleteBike")]
        [OpenApiOperation(operationId: "DeleteBike", Description = "Gets a Bike for the Given USERID")]
        [OpenApiParameter(name: "BikeID", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Bikeid  of the Bike")]
        [OpenApiResponseWithoutBody(HttpStatusCode.NoContent, Description = "Successfull deltion of the entry")]
        [OpenApiResponseWithoutBody(HttpStatusCode.NotFound, Description = "Entry not found")]
        public async Task<IActionResult> DeleteBikeAsync([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "bike/{BikeID}")] HttpRequest req, string BikeID)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Bikes? bike = JsonConvert.DeserializeObject<Bikes>(requestBody);

            int ValidateBikeId = int.Parse(BikeID);
            if (ValidateBikeId > 0)
            {
                if (await _bikeServices.DeleteBikeAsync(ValidateBikeId))
                {
                    return new NoContentResult();
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            return new OkObjectResult("Welcome to Azure Functions!");
        }

        [Function("GetAllBike")]
        [OpenApiOperation(operationId: "GetAllBike", Description = "Creates a Bike for the Given USERID")]
        public async Task<IActionResult> GetAllBikeAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "bike")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            return new OkObjectResult(await _bikeServices.getAllBikesAsync());
        }


    }
}