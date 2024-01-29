using Microsoft.AspNetCore.Mvc;
using InventoryTracker.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Controllers
{
  public class GamesController : Controller
  {
    private readonly InventoryTrackerContext _db;

    public GamesController(InventoryTrackerContext db)
    {
      _db = db;
    }

    // public ActionResult Index()
    // {
    //   List<Game> model = _db.Games.ToList();
    //   return View(model);
    // }



    // public async Task<IActionResult> Index(string searchQuery)
    // {
    //   List<Game> model = _db.Games.ToList();
    //   // IEnumerable<Game> searchResults = model.Where(game => game.GameName.ToLower().Contains(gameName.ToLower())).ToList<Game>();

    //   model = model.Where(game => game.GameName.Contains(searchQuery)).ToList<Game>();

    //   return View(await model.ToListAsync());


    // }

    public async Task<IActionResult> IndexSearchOld(string searchString)
    {
      if (_db == null)
      {
        return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
      }

      var games = _db.Games.AsQueryable();
      // from m in _db.Games
      //             select m;

      if (!String.IsNullOrEmpty(searchString))
      {
        games = games.Where(s => s.GameName!.Contains(searchString));
      }

      return View(await games.ToListAsync());

    }


    private async Task<List<Game>> SearchMethod(string searchQuery)
    {
      IQueryable<Game> gamesList = _db.Set<Game>();

      if (searchQuery != null)
      {
        return await gamesList?.Where(s => s.GameName.Contains(searchQuery)).ToListAsync();
      }
      else
      {
        return await gamesList.ToListAsync();
      }
    }

    public async Task<IActionResult> Index(string searchQuery)
    {
      List<Game> resultsList = await SearchMethod(searchQuery);
      return View(resultsList);
    }

    // [HttpPost]
    //     public async Task<IActionResult> Search(GameSearchViewModel viewModel)
    //     {
    //         if (ModelState.IsValid)
    //         {
    //             var searchResults = await _context.Games
    //                 .Where(g => g.Name.Contains(viewModel.SearchTerm))
    //                 .ToListAsync();

    //             return View("SearchResults", searchResults);
    //         }

    //         return View(viewModel);
    //     }






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

    public ActionResult Details(int id)
    {
      Game thisGame = _db.Games.FirstOrDefault(game => game.GameId == id);
      return View(thisGame);
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


