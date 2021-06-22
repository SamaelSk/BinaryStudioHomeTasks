using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.Models
{
    class UserToTasks
    {
        public string UserName { get; set; }
         public List<ProjectTask> ProjectTasks { get; set; }
    }
}
