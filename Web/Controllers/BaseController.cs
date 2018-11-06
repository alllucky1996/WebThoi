using Interface;
using System.Web.Mvc;
using Common.Helpers;
namespace Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IRepository _repository = DependencyResolver.Current.GetService<IRepository>();
        protected readonly CacheFactory _cacheFactory = new CacheFactory();
    }
}