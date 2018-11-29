using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class CheckableTable<T> where T:class
    {
        public List<CheckableRow<T>> Rows { get; set; }
    }
}
