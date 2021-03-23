using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PeopleAjax.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PeopleAjax.Data;

namespace PeopleAjax.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString =
               @"Data Source=.\sqlexpress;Initial Catalog=People;Integrated Security=true;";
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Person person)
        {
            var db = new PeopleDb(_connectionString);
            db.Add(person);
            return Json(person);
        }

        public IActionResult GetAll()
        {
            var db = new PeopleDb(_connectionString);
            List<Person> ppl = db.GetAll();
            return Json(ppl);
        }
        [HttpPost]
        public IActionResult Edit(Person person)
        {
            var db = new PeopleDb(_connectionString);
            db.Edit(person);
            return Json(person);
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var db = new PeopleDb(_connectionString);
            db.Delete(id);
            return Json(id);
        }
    }
}
