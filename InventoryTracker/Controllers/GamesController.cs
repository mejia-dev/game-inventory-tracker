using Microsoft.AspNetCore.Mvc;
using InventoryTracker.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    private async Task<List<Game>> SearchMethod(string searchQuery, string propertyName)
    {
      IQueryable<Game> gamesList = _db.Set<Game>();

      if (searchQuery != null)
      {
        switch (propertyName)
        {
          case "GameDescription":
            return await gamesList?.Where(s => s.GameDescription.Contains(searchQuery)).ToListAsync();
          default: 
            return await gamesList?.Where(s => s.GameName.Contains(searchQuery)).ToListAsync();
        }
        // return await gamesList?.Where(s => s.GameName.Contains(searchQuery)).ToListAsync();
      }
      else
      {
        return await gamesList.ToListAsync();
      }
    }

    public async Task<IActionResult> Index(string searchQuery, string propertyName)
    {
      List<Game> resultsList = await SearchMethod(searchQuery, propertyName);
      return View(resultsList);
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

    public ActionResult Details(int id)
    {
      Game thisGame = _db.Games.FirstOrDefault(game => game.GameId == id);
      return View(thisGame);
    }

    public ActionResult Edit(int id)
    {
      Game thisGame = _db.Games.FirstOrDefault(game => game.GameId == id);
      return View(thisGame);
    }

    [HttpPost]
    public ActionResult Edit(Game game)
    {
      _db.Games.Update(game);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Game thisGame = _db.Games.FirstOrDefault(game => game.GameId == id);
      return View(thisGame);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Game thisGame = _db.Games.FirstOrDefault(game => game.GameId == id);
      _db.Games.Remove(thisGame);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
