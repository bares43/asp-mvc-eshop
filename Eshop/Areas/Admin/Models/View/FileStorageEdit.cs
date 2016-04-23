using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess.Model;

namespace Eshop.Areas.Admin.Models.View
{
    public class FileStorageEdit
    {
        public FileStorage FileStorage { get; set; }

        public List<File> Files { get; set; } 
    }
}