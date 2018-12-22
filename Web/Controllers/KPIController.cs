using Entities.Models.SystemManage;
using Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Handler;

namespace Web.Controllers
{
  public class KPIController : Controller
  {
		protected readonly IRepository _repository = DependencyResolver.Current.GetService<IRepository>();
		// GET: KPI
		public ActionResult Index()
    {

			var user = (from ac in _repository.GetRepository<Account>().GetAll()
									join dv in _repository.GetRepository<dmDonVi>().GetAll() on ac.IdDonVi equals dv.Id
									join cap in _repository.GetRepository<CapQuanLy>().GetAll() on ac.IDCapQuanLy equals cap.Id
									where ac.Id == 1
									select new
									{
										user_name = ac.Name,
										user_phone = ac.PhoneNumber,
										user_cap = cap.Name,
										user_dv = dv.Name,
									}).First();
				ViewData["user_name"] = user.user_name;
				ViewData["user_cap"] = user.user_cap;
				ViewData["user_dv"] = user.user_dv;

			var Kpi = (
				from ac in _repository.GetRepository<Account>().GetAll()
				join uk in _repository.GetRepository<User_KPI>().GetAll() on ac.Id equals uk.IdUser
				join kpi in _repository.GetRepository<dmKPI>().GetAll() on uk.IdKPI equals kpi.Id
				//join u in _repository.GetRepository<unit>().GetAll() on kpi.IdUnit equals u.Id
				//join w in _repository.GetRepository<Weight>().GetAll() on kpi.IdWeight equals w.id
				where ac.Id == 1
				select new
				{
					kpi_code = kpi.Code,
					kpi_name = kpi.Name,
					kpi_id = kpi.Id,
					kpi_id_parent = kpi.IdCha,
					//kpi_unit = kpi.Unit.Name,
					//kpi_weight = kpi.Weight.code
				});
			var list = new List<Handler.obj_kpi>();
			obj_kpi obj;
			foreach (var value in Kpi)
			{
				obj = new obj_kpi(value.kpi_id.ToString(), value.kpi_id_parent.ToString(), value.kpi_code, value.kpi_name);
				list.Add(obj);
			}
			ViewData["count_kpi_parent"] = Kpi.Where(x => x.kpi_id_parent == null).Count();
			ViewData["lst_kpi"] = list;
			list = new List<obj_kpi>();
			foreach(var value in Kpi.Where(x => x.kpi_id_parent == null))
			{
				obj = new obj_kpi(value.kpi_id.ToString(), value.kpi_id_parent.ToString(), value.kpi_code, value.kpi_name);
				list.Add(obj);
			}
			ViewData["lst_kpi_parent"] = list;
			return View();
    }
  }
}