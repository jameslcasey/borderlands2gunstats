using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Borderlands2Guns.Models;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace Borderlands2Guns.Controllers
{
    public class GunsController : Controller
    {
        private readonly Borderlands2GunsContext _context;

        public GunsController(Borderlands2GunsContext context)
        {
            //context.Database.ExecuteSqlCommand("exec UpdateRanks");
            _context = context;
        }

        [HttpGet]
        public string MetricRank(string metric, decimal value, int type)
        {
            var x = _context.Database.ExecuteSqlCommand("exec MetricRanks @metric, @value, @type", 
                new SqlParameter("@metric", metric),
                new SqlParameter("@value", value),
                new SqlParameter("@type", type));


            return JsonConvert.SerializeObject(x);
        }


        [HttpGet]
        public string GunNameSearch(string ss)
        {
            var guns = from g in _context.Guns orderby g.AllTypesDamageOnTargetRank select g;

            if (!String.IsNullOrEmpty(ss))
            {
                guns = guns
                    .Where(s => s.Name.Contains(ss))
                    .OrderBy(o => o.AllTypesDamageOnTargetRank);
            }

            return JsonConvert.SerializeObject(guns);
        }




        [HttpGet]
        public string GunCalcs(int damage, decimal accuracy,  decimal firerate, decimal reloadspeed, int magazinesize, decimal elementalDamagePerSecond, decimal chance)
        {
            Guns gun = new Guns() {
                Damage = damage,
                Accuracy = accuracy,
                FireRate = firerate,
                ReloadSpeed = reloadspeed,
                MagazineSize = magazinesize,
                ElementalDamagePerSecond = elementalDamagePerSecond,
                Chance = chance
            };
            return JsonConvert.SerializeObject(gun);
        }


        // GET: Guns
        public async Task<IActionResult> GunSearchResults(string ss)
        {

            var guns = from g in _context.Guns select g;

            if (!String.IsNullOrEmpty(ss))
            {
                guns = guns.Where(s => s.Name.Contains(ss));
            }

            return View(await guns.ToListAsync());
        }


        [HttpGet]
        public JsonResult Search(string ss)
        {
            var guns = from g in _context.Guns select g;

            if (!String.IsNullOrEmpty(ss))
            {
                guns = guns.Where(s => s.Name.Contains(ss));
            }

            return Json(guns.ToList());
        }

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    return View();
        //}

        //[HttpGet]
        //public JsonResult IndexData()
        //{
        //    var guns = from g in _context.Guns orderby g.DamageOnTarget descending select g;
        //    var result = new Dictionary<string, List<Guns>>
        //    {
        //        { "data", guns.ToList() }
        //    };
        //    return Json(result);
        //}



        // GET: Guns
        public async Task<IActionResult> Index(string ss)
        {

            var guns = from g in _context.Guns orderby g.DamageOnTarget descending select g;

            if (!String.IsNullOrEmpty(ss))
            {
                guns = guns.Where(s => s.Name.Contains(ss)).OrderByDescending(o => o.DamageOnTarget);
            }

            return View(await guns.ToListAsync());
        }

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    var guns = from g in _context.Guns orderby g.AllTypesDamageOnTargetRank select g;
        //    ViewData["guns"] = JsonConvert.SerializeObject(guns);
        //    return View();
        //}




        // GET: Guns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guns = await _context.Guns
                .SingleOrDefaultAsync(m => m.Id == id);
            if (guns == null)
            {
                return NotFound();
            }

            return View(guns);
        }

        // GET: Guns/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Level,Type,Brand,Damage,Accuracy,FireRate,ReloadSpeed,MagazineSize,DamageTimesFireRate,DamageTimesFireRateTimesMagazineSize,DamageTimesFireRateTimesMagazineSizePerReloadSpeed,DamageOnTarget,ElementalEffect,ElementalDamagePerSecond,Chance,ElementalDamagePerSecondTimesChance,ElementalDamageOnTargetTimesDamagePerSecondTimesChance")] Guns guns)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guns);
                await _context.SaveChangesAsync();
                _context.Database.ExecuteSqlCommand("exec UpdateRanks");
                return RedirectToAction(nameof(Create));
            }
            return View(guns);
        }

        // GET: Guns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guns = await _context.Guns.SingleOrDefaultAsync(m => m.Id == id);
            if (guns == null)
            {
                return NotFound();
            }
            return View(guns);
        }

        // POST: Guns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Level,Type,Brand,Damage,Accuracy,FireRate,ReloadSpeed,MagazineSize,DamageTimesFireRate,DamageTimesFireRateTimesMagazineSize,DamageTimesFireRateTimesMagazineSizePerReloadSpeed,DamageOnTarget,ElementalEffect,ElementalDamagePerSecond,Chance,ElementalDamagePerSecondTimesChance,ElementalDamageOnTargetTimesDamagePerSecondTimesChance")] Guns guns)
        {
            if (id != guns.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guns);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GunsExists(guns.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(guns);
        }

        // GET: Guns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guns = await _context.Guns
                .SingleOrDefaultAsync(m => m.Id == id);
            if (guns == null)
            {
                return NotFound();
            }

            return View(guns);
        }

        // POST: Guns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guns = await _context.Guns.SingleOrDefaultAsync(m => m.Id == id);
            _context.Guns.Remove(guns);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GunsExists(int id)
        {
            return _context.Guns.Any(e => e.Id == id);
        }


    }
}
