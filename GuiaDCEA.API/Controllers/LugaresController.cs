using GuiaDCEA.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuiaDCEA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LugaresController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public LugaresController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult<int>> RegistrarLugar(Lugar lugar)
        {
            context.Add(lugar);
            await context.SaveChangesAsync();
            return lugar.Id;
        }

        [HttpGet]
        public async Task<ActionResult<List<Lugar>>> ObtenerLugares()
        {
            return await context.Lugares.ToListAsync();
        }
    }
}
