using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public partial class MenuItem
    {
        public string Title { get; set; }
        public int MenuId { get; set; }
        public string Icon { get; set; }
        public string ControllerName { get; set; }
        public bool Disabled { get; set; }
        public string RouteName { get; set; }
        public Enums.ModuleEnum Module { get; set; }
        public string Color { get; set; }
        public List<Enums.ActionEnum> Actions { get; set; }
        public int OrdinalNumber { get; set; }
        public MenuGroup Group{get;set;}

    }
}
