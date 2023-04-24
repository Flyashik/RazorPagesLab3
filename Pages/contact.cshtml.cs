using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            ContactForm contact = new ContactForm();
            contact.FirstName = Request.Form["first_name"];
            contact.LastName = Request.Form["last_name"];
            contact.Email = Request.Form["email"];
            contact.Phone = Request.Form["phone"];
            contact.SelectService = Request.Form["select_service"];
            contact.SelectPrice = Request.Form["select_price"];
            contact.Comments = Request.Form["comments"];
            cw.WriteInCSV(contact);

            Response.StatusCode = 200;

            return Content("Привет! Что там с телеграмом?");
        }
    }
}
