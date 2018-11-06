using Common.Helpers;
using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebDemo.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IRepository _repository = DependencyResolver.Current.GetService<IRepository>();
        protected readonly CacheFactory _cacheFactory = new CacheFactory();
    }
}