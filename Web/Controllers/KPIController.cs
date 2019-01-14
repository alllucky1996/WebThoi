using Entities.Models;
using Entities.Models.SystemManage;
using Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Handler;

namespace Web.Controllers
{
  public class KPIController : Controller
  {
	protected static readonly IRepository _repository = DependencyResolver.Current.GetService<IRepository>();
	// GET: KPI
	public ActionResult Index()
    {
			int account_id = 1; 
	    var user = (from ac in _repository.GetRepository<Account>().GetAll()
							    join dv in _repository.GetRepository<DM_DonVi>().GetAll() on ac.IdDonVi equals dv.Id
							    join cap in _repository.GetRepository<CapQuanLy>().GetAll() on ac.IDCapQuanLy equals cap.Id
							    where ac.Id == account_id
							    select new
							    {
								    user_name = ac.FullName,
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
				join w in _repository.GetRepository<Weight>().GetAll() on uk.IdWeight equals w.id
				where ac.Id == account_id
		    select new
		    {
			    kpi_code = kpi.Code,
			    kpi_name = kpi.Name,
			    kpi_id = kpi.Id,
			    kpi_id_parent = kpi.IdCha,
					kpi_unit = kpi.Unit.Name,
					kpi_weight = w.code
				});
	    var list = new List<Handler.obj_kpi>();
	    obj_kpi obj;
	    foreach (var value in Kpi)
	    {
		    obj = new obj_kpi(value.kpi_id.ToString(), value.kpi_id_parent.ToString(), value.kpi_code, value.kpi_name, value.kpi_weight);
		    list.Add(obj);
	    }
	    ViewData["count_kpi_parent"] = Kpi.Where(x => x.kpi_id_parent == null).Count();
	    ViewData["lst_kpi"] = list;
	    list = new List<obj_kpi>();
	    foreach(var value in Kpi.Where(x => x.kpi_id_parent == null))
	    {
		    obj = new obj_kpi(value.kpi_id.ToString(), value.kpi_id_parent.ToString(), value.kpi_code, value.kpi_name, value.kpi_weight);
		    list.Add(obj);
	    }
	    ViewData["lst_kpi_parent"] = list;
	    return View();
    }
		public static int flag = 0;
    public static int GetItemTree(long id)
    {
        var gr = _repository.GetRepository<dmKPI>().GetAll().Where(x => x.IdCha == id);
        foreach (var item in gr)
        {
						Debug.WriteLine(item.Id + item.Name);
            GetItemTree(item.Id);
        }
   
				flag += gr.Count();
				return flag;
    }

		 public static int GetItemTree_1(long id)
		 {
					var gr = _repository.GetRepository<dmKPI>().GetAll().Where(x => x.IdCha == id);
					foreach (var item in gr)
					{
										GetItemTree_1(item.Id);
					}

					
					return gr.Count();
		 }

		 public static int get_all_chau(long id) {
							 int row = 0;
							 foreach (var value in _repository.GetRepository<dmKPI>().GetAll().Where(x => x.IdCha == id))
							 {
										foreach (var item in _repository.GetRepository<dmKPI>().GetAll().Where(x => x.IdCha == value.Id))
										{
												 foreach (var item_1 in _repository.GetRepository<dmKPI>().GetAll().Where(x => x.IdCha == item.Id))
												 {
															row++;
												 }
										}
							 }
							 return row;
					}
					public static float sum = 1;
		 public static float sum_kpi(long? id_children) 
		 {
					var gr = (from uk in _repository.GetRepository<User_KPI>().GetAll()
										join kpi in _repository.GetRepository<dmKPI>().GetAll() on uk.IdKPI equals kpi.Id
										join w in _repository.GetRepository<Weight>().GetAll() on uk.IdWeight equals w.id
										where (kpi.Id == id_children)
										select new { 
										kpi_id = kpi.Id,
										kpi_id_parent = kpi.IdCha,
										kpi_weight = w.code
										});
					foreach(var item in gr) {
							 if (item.kpi_weight != null)
							 {
										sum = sum * (item.kpi_weight % 100);
							 }
							 sum_kpi(item.kpi_id_parent);
					}
					return sum;

		 }
	 }
}