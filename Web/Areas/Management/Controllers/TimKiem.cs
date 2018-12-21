using Common.Helpers;
using Entities.Enums;
using Entities.Models;
using Entities.Models.SystemManage;
using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Management.Filters;
using Web.Areas.Management.Helpers;

namespace Web.Areas.Management.Controllers
{
    [RouteArea("Management", AreaPrefix = "quan-ly")]
    public class TimKiemController : BaseController
    {
        public const string CName = "TimKiem";
        public const string CRoute = "tim-kiem";
        public const string CText = "Tìm kiếm";
        public const ModuleEnum CModule = ModuleEnum.TimKiem;

        public IGenericRepository<dmDonVi> GetRespository()
        {
            return _repository.GetRepository<dmDonVi>();
        }

        #region Thống kê theo đối tượng
        [Route(CRoute + "-can-bo", Name = CName + "_Index")]
        [ValidationPermission(Action = ActionEnum.Read, Module = CModule)]
        public async Task<ActionResult> Index()
        {
            List<dmDonVi> dv = _repository.GetRepository<dmDonVi>().GetAll(
                o =>
                o.CapDV == 1 &&
                (LaCapCty
                || (IdDonVi != null && CapQuanLy == "ĐV" && o.Id == IdDonVi)
                )
            ).ToList();

            ViewBag.DonVis = dv.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            return View();
        }
        [Route(CRoute + "-sinh-vien", Name = CName + "_IndexSV")]
        [ValidationPermission(Action = ActionEnum.Read, Module = CModule)]
        public async Task<ActionResult> IndexSV()
        {
            List<dmDonVi> dv = _repository.GetRepository<dmDonVi>().GetAll(
                o =>
                o.CapDV == 1 &&
                (LaCapCty
                || (IdDonVi != null && CapQuanLy == "ĐV" && o.Id == IdDonVi)
                )
            ).ToList();

            ViewBag.DonVis = dv.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });
            return View();
        }
      
        #endregion
    }
}