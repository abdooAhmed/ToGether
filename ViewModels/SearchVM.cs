using GpProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GpProject.ViewModels
{
    public class SearchVM
    {
        public IList<Report> Reports { get; set; }
        public IList<User> Users { get; set; }
    }
}
