using GuiaDCEA.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GuiaDCEA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotografiasController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public FotografiasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost("fotografia")]
        public async Task<ActionResult<int>> GuardarFotografia(Fotografia fotografia)
        {
            context.Add(fotografia);
            //context.Fotografias.Add(fotografia);
            await context.SaveChangesAsync();
            return fotografia.Id;
        }

        [HttpPost("fotografialugar")]
        public async Task<ActionResult<int>> GuardarFotografiaLugar(int fotografiaId, int lugarId)
        {
            FotografiaLugar fotografiaLugar = new FotografiaLugar();
            fotografiaLugar.LugarId = lugarId;
            fotografiaLugar.FotografiaId = fotografiaId;

            context.Add(fotografiaLugar);

            await context.SaveChangesAsync();
            return fotografiaLugar.Id;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Fotografia>>> ObtenerFotorgrafias(int id)
        {
            List<int> lista = await context.FotografiasLugares.Where(f => f.LugarId == id).Select(f=> f.FotografiaId).ToListAsync();

            List<Fotografia> fotografias = new List<Fotografia>();

            foreach(var item in lista)
            {
                var fotografia = await context.Fotografias.Where(f => f.Id == item).FirstOrDefaultAsync();
                fotografias.Add(fotografia);
            }
            
            return fotografias;
        }
    }
}
