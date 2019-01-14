using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Handler
{
	public class obj_kpi
	{
		public string ID;
		public string IdParent;
		public string Code;
		public string Name;
		public int Weight;
    public obj_kpi(string id, string idParent, string code, string name, int weight)
		{
			ID = id;
			IdParent = idParent;
			Code = code;
			Name = name;
			Weight = weight;
		}
	}
}