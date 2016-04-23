using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshop.Class
{
    public class Paginator
    {
        private int _currentPage;

        public int CurrentPage
        {
            get
            {
                if (_currentPage < 1) return 1;
                if (_currentPage > TotalPages) return TotalPages;
                return _currentPage;
            }
            set { _currentPage = value; }
        }

        public int ItemsPerPage { get; set; } = 25;
        public int TotalItems { get; set; }

        public int TotalPages => (int) Math.Ceiling((double)TotalItems / ItemsPerPage);
        public int Skip => Math.Max((CurrentPage - 1)*ItemsPerPage,0);

        private Dictionary<string, string> urlParams;

        public Dictionary<string, string> UrlParams => urlParams ?? (urlParams = new Dictionary<string, string>());
    }
}