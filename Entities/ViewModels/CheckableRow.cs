using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class CheckableRow<T> where T : class
    {
        public bool Checked { get; set; }
        public T Value { get; set; }
        public long STT { get; set; }
    }
}
