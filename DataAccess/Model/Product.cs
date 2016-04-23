using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataAccess.Interfaces;

namespace DataAccess.Model
{
    public class Product : IEntity
    {
        public enum STOCK_STATUS
        {
            [Display(Name="Dostupné")]
            [Description("Dostupné")]
            Available,
            [Display(Name = "Nedostupné")]
            [Description("Nedostupné")]
            Unavailable,
            [Display(Name = "Dostupné během pár dní")]
            [Description("Dostupné během pár dní")]
            StockWithinAFewDays,
            [Display(Name = "Dostupné během týdne")]
            [Description("Dostupné během týdne")]
            StockWithinAWeek,
            [Display(Name = "Dostupné během měsíce")]
            [Description("Dostupné během měsíce")]
            StockWithinAMonth
        }

        public int Id { get; set; }

        [DisplayName("Název")]
        public string Name { get; set; }

        [DisplayName("Krátký popis")]
        public string Description { get; set; }

        [DisplayName("Dlouhý popis")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string FullDescription { get; set; }

        [DisplayName("Cena")]
        public double Price { get; set; }

        [DisplayName("Viditelný")]
        public bool Visible { get; set; }

        [DisplayName("Zvýrazněný")]
        public bool Highlighted { get; set; }

        public int? GalleryId { get; set; }
        public virtual Gallery Gallery { get; set; }

        public int? FileStorageId { get; set; }
        public virtual FileStorage FileStorage { get; set; }

        [DisplayName("Počet ks skladem")]
        public int StockQuantity { get; set; }

        [DisplayName("Stav skladu")]
        public STOCK_STATUS StockStatus { get; set; }

        [DisplayName("Kategorie")]
        public virtual List<Category> Categories { get; set; } 

        public virtual string StockStatusText()
        {
            string status = null;
            switch (StockStatus)
            {
                case STOCK_STATUS.Available:
                    status = "Dostupné ihned";
                break;
                case STOCK_STATUS.StockWithinAFewDays:
                    status = "Dostupné během několika dní";
                break;
                case STOCK_STATUS.StockWithinAWeek:
                    status = "Dostupné během týdne";
                break;
                case STOCK_STATUS.StockWithinAMonth:
                    status = "Dostupné do 30 dní";
                break;
                case STOCK_STATUS.Unavailable:
                    status = "Momentálně nedostupné";
                break;
            }
            return status;
        }
    }
}
