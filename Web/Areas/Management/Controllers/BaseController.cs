using Common.Helpers;
using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Management.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IRepository _repository = DependencyResolver.Current.GetService<IRepository>();
        protected readonly CacheFactory _cacheFactory = new CacheFactory();
        protected readonly long AccountId;
    }
}