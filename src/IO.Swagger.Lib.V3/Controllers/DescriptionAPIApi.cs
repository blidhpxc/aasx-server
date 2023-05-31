/*
 * DotAAS Part 2 | HTTP/REST | Asset Administration Shell Repository Service Specification
 *
 * The Full Profile of the Asset Administration Shell Repository Service Specification as part of Specification of the Asset Administration Shell: Part 2. Publisher: Industrial Digital Twin Association (IDTA) April 2023
 *
 * OpenAPI spec version: V3.0_SSP-001
 * Contact: info@idtwin.org
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using IO.Swagger.Attributes;
using IO.Swagger.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace IO.Swagger.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class DescriptionAPIApiController : ControllerBase
    {
        /// <summary>
        /// Returns the self-describing information of a network resource (ServiceDescription)
        /// </summary>
        /// <response code="200">Requested Description</response>
        /// <response code="401">Unauthorized, e.g. the server refused the authorization attempt.</response>
        /// <response code="403">Forbidden</response>
        [HttpGet]
        [Route("/description")]
        [ValidateModelState]
        [SwaggerOperation("GetDescription")]
        //[SwaggerResponse(statusCode: 200, type: typeof(ServiceDescription), description: "Requested Description")]
        [SwaggerResponse(statusCode: 200, type: typeof(object), description: "Requested Description")]
        [SwaggerResponse(statusCode: 401, type: typeof(Result), description: "Unauthorized, e.g. the server refused the authorization attempt.")]
        [SwaggerResponse(statusCode: 403, type: typeof(Result), description: "Forbidden")]
        public virtual IActionResult GetDescription()
        {
            return new ObjectResult(null);
        }
    }
}