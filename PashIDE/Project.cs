using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashIDE
{
    public class Project
    {
        public string Name, WorkingDirectory;
        public Project(ProjectPreload preload)
        {
            Name = preload.Name;
            WorkingDirectory = preload.WorkingDirectory;
        }
    }
}
