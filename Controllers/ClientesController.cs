using ApiOracle19c.Data;
using ApiOracle19c.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiOracle19c.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly OracleContext _context;

        public ClientesController(OracleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            using var conexion = _context.GetConnection();

            string sql =
                @"SELECT
                    ID Id,
                    NOMBRE Nombre,
                    EMAIL Email,
                    TELEFONO Telefono,
                    FECHA_REGISTRO FechaRegistro
                  FROM CLIENTES_AC
                  ORDER BY ID";

            var datos = await conexion.QueryAsync<Cliente>(sql);

            return Ok(datos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            using var conexion = _context.GetConnection();

            string sql =
                @"SELECT
                    ID Id,
                    NOMBRE Nombre,
                    EMAIL Email,
                    TELEFONO Telefono,
                    FECHA_REGISTRO FechaRegistro
                  FROM CLIENTES_AC
                  WHERE ID = :Id";

            var cliente =
                await conexion.QueryFirstOrDefaultAsync<Cliente>(
                    sql,
                    new { Id = id }
                );

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Insertar(Cliente cliente)
        {
            using var conexion = _context.GetConnection();

            string sql =
                @"INSERT INTO CLIENTES_AC
                  (
                    NOMBRE,
                    EMAIL,
                    TELEFONO
                  )
                  VALUES
                  (
                    :Nombre,
                    :Email,
                    :Telefono
                  )";

            var filas =
                await conexion.ExecuteAsync(
                    sql,
                    new
                    {
                        cliente.Nombre,
                        cliente.Email,
                        cliente.Telefono
                    });

            return Ok(new
            {
                Mensaje = "Registro insertado",
                Filas = filas
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(
            int id,
            Cliente cliente)
        {
            using var conexion = _context.GetConnection();

            string sql =
                @"UPDATE CLIENTES_AC
                  SET
                    NOMBRE = :Nombre,
                    EMAIL = :Email,
                    TELEFONO = :Telefono
                  WHERE ID = :Id";

            var filas =
                await conexion.ExecuteAsync(
                    sql,
                    new
                    {
                        Id = id,
                        cliente.Nombre,
                        cliente.Email,
                        cliente.Telefono
                    });

            return Ok(new
            {
                Mensaje = "Registro actualizado",
                Filas = filas
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            using var conexion = _context.GetConnection();

            string sql =
                @"DELETE FROM CLIENTES_AC
                  WHERE ID = :Id";

            var filas =
                await conexion.ExecuteAsync(
                    sql,
                    new { Id = id });

            return Ok(new
            {
                Mensaje = "Registro eliminado",
                Filas = filas
            });
        }
    }
}