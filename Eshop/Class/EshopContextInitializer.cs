using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using DataAccess;
using DataAccess.Class;
using DataAccess.Model;

namespace Eshop.Class
{
    public class EshopContextInitializer : CreateDatabaseIfNotExists<EshopContext>
    {
        protected override void Seed(EshopContext context)
        {
            var roleAdmin = context.Roles.Add(new Role()
            {
                Identificator = Role.ROLES.Administrator.ToString(),
                Description = "Admin"
            });

            context.Users.Add(new Administrator()
            {
                Name = "Jan",
                Surname = "Bareš",
                Email = "janbares43@gmail.com",
                ClearPassword = "heslo",
                ConfirmPassword = "heslo",
                Created = DateTime.Now,
                Roles = new List<Role>() { roleAdmin}
            });


            /*var user = context.Users.Add(new Customer()
            {
                Name = "Jan",
                Surname = "Bareš",
                Email = "bares@bares.cz",
                ClearPassword = "heslo",
                ConfirmPassword = "heslo",
                Created = DateTime.Now
            });

            context.Addresses.Add(new Address()
            {
                User = user,
                Name = "jan",
                Surname = "bareš",
                Street = "gen. kratochvíla",
                StreedCode = "1008",
                City = "červený kostelec",
                ZipCode = "54941",
                Created = DateTime.Now,
                Type = Address.AddressTypes.Billing
            });

            context.Addresses.Add(new Address()
            {
                User = user,
                Name = "jan",
                Surname = "bareš",
                Street = "gen. kratochvíla",
                StreedCode = "1008",
                City = "praha",
                ZipCode = "54941",
                Created = DateTime.Now,
                Type = Address.AddressTypes.Billing,
                Primary = true
            });

            context.Addresses.Add(new Address()
            {
                User = user,
                Name = "2jan",
                Surname = "bareš",
                Street = "gen. kratochvíla",
                StreedCode = "1008",
                City = "červený kostelec",
                ZipCode = "54941",
                Created = DateTime.Now,
                Type = Address.AddressTypes.Delivery
            });

            context.Addresses.Add(new Address()
            {
                User = user,
                Name = "2jan",
                Surname = "bareš",
                Street = "gen. kratochvíla",
                StreedCode = "1008",
                City = "praha",
                ZipCode = "54941",
                Created = DateTime.Now,
                Type = Address.AddressTypes.Delivery,
                Primary = true
            });

            context.Categories.Add(new Category()
            {
                Name = "Kategorie jedna",
                Description = "Popis kategorie",
                Products = new List<Product>()
                {
                  new Product()
                    {
                        Name = "Produkt dva",
                        Description = "Popis produktu",
                        FullDescription = "Dlhy popis produktu",
                        Price = 500,
                        Highlighted = false,
                        Visible = true,
                        StockQuantity = 10,
                        StockStatus = Product.STOCK_STATUS.Available
                    },
                    new Product()
                    {
                        Name = "Produkt tři",
                        Description = "Popis produktu",
                        FullDescription = "Dlhy popis produktu",
                        Price = 500,
                        Highlighted = true,
                        Visible = true,
                        StockQuantity = 10,
                        StockStatus = Product.STOCK_STATUS.Available
                    },
                    new Product()
                    {
                        Name = "Produkt čtyři",
                        Description = "Popis produktu",
                        FullDescription = "Dlhy popis produktu",
                        Price = 500,
                        Highlighted = false,
                        Visible = true,
                        StockQuantity = 10,
                        StockStatus = Product.STOCK_STATUS.Available
                    }
                },
                Subcategories = new List<Category>()
                {
                    new Category()
                    {
                        Name = "Podkategorie 1",
                        Description = "Popis podkategorie 1",
                        Products = new List<Product>()
                        {
                            new Product()
                            {
                                Name = "Produkt jedna",
                                Description = "Popis produktu",
                                FullDescription = "Dlhy popis produktu",
                                Price = 500,
                                Highlighted = true,
                                Visible = true,
                                StockQuantity = 10,
                                StockStatus = Product.STOCK_STATUS.Available
                            }
                        }
                    }
                }
            });*/

            context.SaveChanges();
        }
    }
}