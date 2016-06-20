using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication32.Models;
using People.Data;

namespace MvcApplication32.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var repository = new PeopleRepository(Properties.Settings.Default.ConStr);
            var viewModel = new IndexViewModel { People = repository.GetPeople() };
            if (TempData["message"] != null)
            {
                viewModel.Message = (string)TempData["message"];
            }
            return View(viewModel);
        }

        public ActionResult AddPerson()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPerson(Person person)
        {
            var repository = new PeopleRepository(Properties.Settings.Default.ConStr);
            repository.AddPerson(person);
            TempData["message"] = "New person added, id: " + person.Id;
            return Redirect("/home/index");
        }

        public ActionResult EditPerson(int personId)
        {
            var repository = new PeopleRepository(Properties.Settings.Default.ConStr);
            return View(new EditPersonViewModel { Person = repository.GetById(personId) });
        }

        [HttpPost]
        public ActionResult EditPerson(Person person)
        {
            var repository = new PeopleRepository(Properties.Settings.Default.ConStr);
            repository.UpdatePerson(person);
            TempData["message"] = "Person: " + person.Id + " updated!";
            return Redirect("/home/index");
        }

        [HttpPost]
        public void DeletePerson(int personId)
        {
            var repository = new PeopleRepository(Properties.Settings.Default.ConStr);
            repository.DeletePerson(personId);
        }
    }
}
