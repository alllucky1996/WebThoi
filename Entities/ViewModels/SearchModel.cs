using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModels
{
    public class SearchModel<T> where T:class, new()
    {

        public int Count { get; set; }
        public string Description { get; set; }
       
    }
}
