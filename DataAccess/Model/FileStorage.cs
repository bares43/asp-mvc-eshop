using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Interfaces;

namespace DataAccess.Model
{
    public class FileStorage : IEntity
    {
        public int Id { get; set; }

        public List<File> Files { get; set; } 
    }
}
