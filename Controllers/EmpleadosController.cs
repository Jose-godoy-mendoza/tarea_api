
using Microsoft.AspNetCore.Mvc; 
using System.Threading.Tasks;
using System.Linq;
using clase_api.Models;

namespace clase_api.Controllers
{
    [Route("api/[controller]")]

    public class EmpleadosController:Controller
    {
        private Conexion dbConexion;
        public EmpleadosController()
        {
            dbConexion=Conectar.Create();
        }


       [HttpGet]
       public ActionResult GetActionResult()
       {
           //var ver_empleado= select_empleado();
          var e_empleado = from e in dbConexion.empleados join p in dbConexion.puestos on e.id_puesto equals p.id_puesto select new 
           {
                id_empleado=e.id_empleado,
                codigo=  e.codigo,
                nombres = e.nombres,
                apellidos = e.apellidos,
                direccion = e.direccion,
                telefono = e.telefono,
                fecha_nacimiento = e.fecha_nacimiento,
                //puesto = p.puesto
                puesto = p.puesto
           };
           return Ok(e_empleado);
       }
       /* [HttpGet]
        public ActionResult GetActionResult()
        {
            return Ok(dbConexion.empleados.ToArray());
        }*/

        [HttpGet("{id}")]
        public async Task<ActionResult> GetActionResult(int id)
        {
            var empleados=await dbConexion.empleados.FindAsync(id);
            if(empleados!=null)
            {
                var e_empleado = from e in dbConexion.empleados join p in dbConexion.puestos on e.id_puesto equals p.id_puesto where e.id_empleado == id select new 
                /*var e_empleado = await from e in dbConexion.empleados join p in dbConexion.puestos on e.id_puesto equals p.id_puesto where e.id_empleado = dbConexion.empleados.FindAsync(id) select new */
                {
                    id_empleado=e.id_empleado,
                    codigo=  e.codigo,
                    nombres = e.nombres,
                    apellidos = e.apellidos,
                    direccion = e.direccion,
                    telefono = e.telefono,
                    fecha_nacimiento = e.fecha_nacimiento,
                    puesto = p.puesto
                };
                return Ok(e_empleado);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBodyAttribute] Empleados empleados)
        {
            if(ModelState.IsValid)
            {
                dbConexion.empleados.Add(empleados);
                await dbConexion.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<ActionResult> Put( [FromBodyAttribute] Empleados empleados)
        {
            var e_emp= dbConexion.empleados.SingleOrDefault(emp => emp.id_empleado ==empleados.id_empleado);
            if(e_emp !=null && ModelState.IsValid)
            {
                dbConexion.Entry(e_emp).CurrentValues.SetValues(empleados);
                await dbConexion.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var empleado=dbConexion.empleados.SingleOrDefault(e => e.id_empleado ==id);
            if(empleado!=null)
            {
                dbConexion.empleados.Remove(empleado);
                await dbConexion.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}