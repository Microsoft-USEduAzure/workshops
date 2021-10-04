#r "Newtonsoft.Json"

using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

public static async Task<IActionResult> Run(HttpRequest req, ILogger log)
{
    string category = "GREEN";

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    log.LogInformation(string.Format("The sentiment score received is '{0}'.", requestBody));

    double score = Convert.ToDouble(requestBody);

    if(score < .5)
    {
        category = "RED";
    }
    else if (score < .6) 
    {
        category = "YELLOW";
    }

    return requestBody != null
        ? (ActionResult)new OkObjectResult(category)
        : new BadRequestObjectResult("Please pass a value on the query string or in the request body");
}