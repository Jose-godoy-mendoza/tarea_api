using Microsoft.EntityFrameworkCore;

namespace clase_api.Models
{
    class Conexion : DbContext
    {
        public Conexion(DbContextOptions<Conexion> options) : base (options){}
        public DbSet<Clientes> clientes {get;set;}
        public DbSet<Puestos> puestos {get;set;}
        public DbSet<Empleados> empleados {get;set;}

    }
    class Conectar
    {
        private const string CadenaConexion =" server=localhost;port=3309;database=db_empresa;userid=root;pwd=root" ;
        public static Conexion Create()
        {
            var constructor = new DbContextOptionsBuilder<Conexion>();
            constructor.UseMySQL(CadenaConexion);
            var cn= new Conexion(constructor.Options);
            return cn;
        }
    }
}
