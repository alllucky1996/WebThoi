using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public partial class MenuGroup
    {
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; }
        public int GroupId { get; set; }
        public int Ordinal { get; set; }
        public List<MenuItem> Items { get; set; }
    }
}
