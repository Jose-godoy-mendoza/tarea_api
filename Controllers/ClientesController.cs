using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using clase_api.Models;

namespace clase_api.Controllers
{
    [Route("api/[controller]")]
    public class ClientesController : Controller
    {
        private Conexion dbConexion;
        public ClientesController()
        {
            dbConexion = Conectar.Create();
        }
        [HttpGet]
        public ActionResult GetActionResult()
        {
            return Ok(dbConexion.clientes.ToArray());
        }

        [HttpGet("{id}")]
        public async Task <ActionResult> GetActionResult(int id)
        {
            var clientes=await dbConexion.clientes.FindAsync(id);
            if(clientes !=null)
            {
                return Ok(clientes);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBodyAttribute] Clientes clientes)
        {
            if(ModelState.IsValid)
            {
                dbConexion.clientes.Add(clientes);
                await dbConexion.SaveChangesAsync();
                return Ok();
            }
            else{
                return BadRequest();
            }
        }

        public async Task<ActionResult> Put([FromBodyAttribute] Clientes clientes)
        {
            //update clientes set where id
            var v_clientes = dbConexion.clientes.SingleOrDefault( a => a.id_clientes == clientes.id_clientes);
            if( v_clientes !=null && ModelState.IsValid)
            {
                dbConexion.Entry(v_clientes).CurrentValues.SetValues(clientes);
                await dbConexion.SaveChangesAsync();
                return Ok();
            }
            else{
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var clientes = dbConexion.clientes.SingleOrDefault(a => a.id_clientes == id);
            if(clientes !=null)
            {
                dbConexion.clientes.Remove(clientes);
               await  dbConexion.SaveChangesAsync();
                return Ok();
            }
            else{
                return BadRequest();
            }
        }
    }
}
