using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using System;
namespace ClubWebsite.Infrastructure
{
    public static class MVCControllerHelper
    {
        public static JsonResult CustomJson(this Controller controller, object? data)
        {
            return controller.Json(data == null ? "" : data, new JsonSerializerSettings());
        }
    }
}
