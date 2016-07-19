using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class ProjectAndTaskCountViewModel
    {
        public int ID { get; set; } // it won't let me create a view out of this view model unless I give the model a fucking ID!
        public string ProjectName { get; set; }
        public int TaskCount { get; set; }
    }
}