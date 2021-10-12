using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using clase_api.Models;

namespace clase_api.Controllers
{
    [Route("api/[controller]")]

    public class PuestosController : Controller
    {
        private Conexion dbConexion;
        public PuestosController()
        {
            dbConexion=Conectar.Create();
        }
        [HttpGet]
        public ActionResult GetActionResult()
        {
            return Ok(dbConexion.puestos.ToArray());
        }

        [HttpGet("{id_puesto}")]
        public async Task <ActionResult> GetActionResult (int id_puesto)
        {
            var puestos =await dbConexion.puestos.FindAsync(id_puesto);
            if(puestos!=null)
            {
                return Ok(puestos);
            }
            else{
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBodyAttribute] Puestos puestos)
        {
            if(ModelState.IsValid)
            {
                dbConexion.puestos.Add(puestos);
                await dbConexion.SaveChangesAsync();
                return Ok();
            }
            else{
                return BadRequest();
            }
        }

        public async Task<ActionResult> Put([FromBodyAttribute] Puestos puestos)
        {
            var p_puestos = dbConexion.puestos.SingleOrDefault(p =>p.id_puesto == puestos.id_puesto);
            if(p_puestos != null && ModelState.IsValid)
            {
                dbConexion.Entry(p_puestos).CurrentValues.SetValues(puestos);
                await dbConexion.SaveChangesAsync();
                return Ok();
            }
            else{
                return BadRequest();
            }
        }

        [HttpDelete("{id_puesto}")]
        public async Task<ActionResult> Delete(int id_puesto)
        {
            var puestos = dbConexion.puestos.SingleOrDefault(p => p.id_puesto == id_puesto);
            if(puestos !=null)
            {
                dbConexion.puestos.Remove(puestos);
                await dbConexion.SaveChangesAsync();
                return Ok();
            }
            else{
                return BadRequest();
            }
        }
        
    }
}