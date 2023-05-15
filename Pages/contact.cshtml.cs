using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RazorPagesLab3.Models;

namespace RazorPagesLab3.Pages
{
    [IgnoreAntiforgeryToken]
    public class contactModel : PageModel
    {
        private readonly ContactsWriter cw;

        public contactModel(ContactsWriter _cw)
        {
            this.cw = _cw;
        }

        public void OnGet() {}

        public IActionResult OnPost()
        {
            ContactForm contact = new ContactForm
            {
                FirstName = Request.Form["first_name"],
                LastName = Request.Form["last_name"],
                Email = Request.Form["email"],
                Phone = Request.Form["phone"],
                SelectService = Request.Form["select_service"],
                SelectPrice = Request.Form["select_price"],
                Comments = Request.Form["comments"]
            };
            cw.WriteInCSV(contact);

            Response.StatusCode = 200;

            insertToContacts(contact);

            return Content("Привет! Что там с телеграмом?");
        }

        public static void insertToContacts(ContactForm cnt)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(config.GetConnectionString("Default"))
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureCreated();
                context.Database.Migrate();

                context.Contacts.Add(cnt);
                context.SaveChanges();
            }
        }
    }
}
