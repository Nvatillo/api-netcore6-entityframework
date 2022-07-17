using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.Entity;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    public class SuperHeroController : Controller
    {

        private List<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero{
                    Id = 1,
                    Name = "Spider Man",
                    FirstName = "Peter",
                    LastName = "Parker",
                    Place = "New York City"
                },
                new SuperHero{
                    Id = 2,
                    Name = "Iron Man",
                    FirstName = "Tony",
                    LastName = "Stark",
                    Place = "Long Island"
                }
            };
        private readonly DataContext _dataContext;

        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetOnlyOne(int id)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");
            
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero([FromBody] SuperHero hero)
        {
            _dataContext.SuperHeroes.Add(hero);
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<SuperHero>> UpdateHero([FromBody] SuperHero request)
        {
            var hero =await _dataContext.SuperHeroes.FindAsync(request.Id);
            if (hero == null)
                return BadRequest("Hero not found");

            hero.Name = request.Name;
            hero.FirstName =  request.FirstName;
            hero.LastName = request.LastName;   
            hero.Place = request.Place;
            await _dataContext.SaveChangesAsync();

            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");

            _dataContext.SuperHeroes.Remove(hero);
            await _dataContext.SaveChangesAsync();  

            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }


    }
}
