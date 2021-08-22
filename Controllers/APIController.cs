using FieldLevel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Caching.Memory;

namespace FieldLevel.Controllers
{
    public class APIController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private IMemoryCache _cache;

        public APIController(ILogger<HomeController> logger, IConfiguration configuration, IMemoryCache memoryCache)
        {
            _logger = logger;
            _configuration = configuration;
            _cache = memoryCache;
        }

        public JsonResult GetRecentPosts()
        {
            List<UserPost> postList = FieldLevel.BusinessLogic.GetMostRecentUserPosts(_configuration, _cache);

            return Json(postList);
        }
    }
}
