using Microsoft.AspNetCore.Mvc;
using InventoryTracker.Models;
using System.Collections.Generic;
using System.Linq;

namespace InventoryTracker.Controllers
{
  public class GamesController : Controller
  {
    private readonly InventoryTrackerContext _db;

    public GamesController(InventoryTrackerContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Game> model = _db.Games.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
     return View();
    }

    [HttpPost]
    public ActionResult Create(Game game)
    {
      _db.Games.Add(game);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }

}

// namespace ToDoList.Controllers
// {
//   public class ItemsController : Controller
//   {

//     [HttpGet("/categories/{categoryId}/items/new")]
//     public ActionResult New(int categoryId)
//     {
//       Category category = Category.Find(categoryId);
//       return View(category);
//     }

//     [HttpPost("/items/delete")]
//     public ActionResult DeleteAll()
//     {
//       Item.ClearAll();
//       return View();
//     }

//     [HttpGet("/categories/{categoryId}/items/{itemId}")]
//     public ActionResult Show(int categoryId, int itemId)
//     {
//       Item item = Item.Find(itemId);
//       Category category = Category.Find(categoryId);
//       Dictionary<string, object> model = new Dictionary<string, object>();
//       model.Add("item", item);
//       model.Add("category", category);
//       return View(model);
//     }
//   }
// }


