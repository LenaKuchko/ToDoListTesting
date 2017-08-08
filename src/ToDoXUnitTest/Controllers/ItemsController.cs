using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoXUnitTest.Models;
using Microsoft.EntityFrameworkCore;
using ToDoXUnitTest.Models.Repositories;

namespace ToDoList.Controllers
{
    public class ItemsController : Controller
    {
        private IItemRepository itemRepo;

        public ItemsController(IItemRepository thisRepo = null)
        {
            if (thisRepo == null)
            {
                this.itemRepo = new EFItemRepository();
            }
            else
            {
                this.itemRepo = thisRepo;
            }
        }


        public ViewResult Index()
        {
            return View(itemRepo.Items.ToList());
        }

        public IActionResult Details(int id)
        {
            Item thisItem = itemRepo.Items.FirstOrDefault(x => x.ItemId == id);
            return View(thisItem);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            itemRepo.Save(item);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Item thisItem = itemRepo.Items.FirstOrDefault(x => x.ItemId == id);
            return View(thisItem);
        }

        [HttpPost]
        public IActionResult Edit(Item item)
        {
            itemRepo.Edit(item);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Item thisItem = itemRepo.Items.FirstOrDefault(x => x.ItemId == id);
            return View(thisItem);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            Item thisItem = itemRepo.Items.FirstOrDefault(x => x.ItemId == id);
            itemRepo.Remove(thisItem);
            return RedirectToAction("Index");
        }
    }
}