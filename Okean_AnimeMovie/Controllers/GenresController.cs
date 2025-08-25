using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Okean_AnimeMovie.Core.Entities;
using Okean_AnimeMovie.Core.Interfaces;

namespace Okean_AnimeMovie.Controllers;

[Authorize(Roles = "Admin")]
public class GenresController : Controller
{
    private readonly IGenericRepository<Genre> _genreRepository;

    public GenresController(IGenericRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    // GET: /Genres
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var genres = await _genreRepository.GetAllAsync();
        return View(genres.OrderBy(g => g.Name));
    }

    // GET: /Genres/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View(new Genre());
    }

    // POST: /Genres/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Genre model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _genreRepository.AddAsync(model);
        TempData["Success"] = "Genre created successfully.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Genres/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var genre = await _genreRepository.GetByIdAsync(id);
        if (genre == null)
        {
            return NotFound();
        }
        return View(genre);
    }

    // POST: /Genres/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Genre model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        await _genreRepository.UpdateAsync(model);
        TempData["Success"] = "Genre updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    // POST: /Genres/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var genre = await _genreRepository.GetByIdAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        await _genreRepository.DeleteAsync(genre);
        TempData["Success"] = "Genre deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
