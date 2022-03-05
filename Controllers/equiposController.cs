using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using _2019PA603WACRUD.Models;


namespace _2019PA603WACRUD.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext _context;

        public equiposController(equiposContext miContexto)
        {
            this._context = miContexto;
        }
        [HttpGet]
        [Route("api/equipos/")]
        public IActionResult Get()
        {
            try
            {
                var listadoEquipo = (from e in _context.equipos
                                     join m in _context.marcas on e.marca_id equals m.id_marcas
                                     join te in _context.tipo_equipo on e.tipo_equipo_id equals te.id_tipo_equipo
                                     join s in _context.estado_equipos on e.estado_equipo_id equals s.id_estados_equipo
                                     select new
                                     {
                                         e.id_equipos,
                                         e.nombre,
                                         descripcion_equipo = e.descripcion,
                                         e.marca_id,
                                         m.nombre_marcas,
                                         e.tipo_equipo_id,
                                         tipo_descripcion = te.descripcion,
                                         e.estado_equipo_id,
                                         estado_descripcion = s.descripcion,
                                         detalle = "Marca: " + m.nombre_marcas + " Tipo: " + te.descripcion
                                     }).OrderBy(te => te.tipo_equipo_id);
                if (listadoEquipo.Count() > 0)
                {
                    return Ok(listadoEquipo);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/equipos/{idEquipo}")]
        public IActionResult Get(int idEquipo)
        {
            try
            {
                var xEquipo = (from e in _context.equipos
                               join m in _context.marcas on e.marca_id equals m.id_marcas
                               join te in _context.tipo_equipo on e.tipo_equipo_id equals te.id_tipo_equipo
                               join s in _context.estado_equipos on e.estado_equipo_id equals s.id_estados_equipo
                               where e.id_equipos == idEquipo
                               select new
                               {
                                   e.id_equipos,
                                   e.nombre,
                                   descripcion_equipo = e.descripcion,
                                   e.marca_id,
                                   m.nombre_marcas,
                                   e.tipo_equipo_id,
                                   tipo_descripcion = te.descripcion,
                                   e.estado_equipo_id,
                                   estado_descripcion = s.descripcion,
                                   detalle = "Marca: " + m.nombre_marcas + "Tipo: " + te.descripcion
                               }).FirstOrDefault();
                if (xEquipo != null)
                {
                    return Ok(xEquipo);
                }
                return NotFound();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/equipos")]
        public IActionResult guardarEquipo([FromBody] equipos equipoNuevo)
        {
            try
            {
                _context.equipos.Add(equipoNuevo);
                _context.SaveChanges();
                return Ok(equipoNuevo);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("api/equipos")]
        public IActionResult updateEquipo([FromBody] equipos equipoAModificar)
        {
            //Para actualizar un registro, se obtiene el registro original de la base de datos
            equipos equipoExiste = (from e in _context.equipos
                                    where e.id_equipos == equipoAModificar.id_equipos
                                    select e).FirstOrDefault();
            if (equipoExiste is null)
            {
                // Si no existe el registro retornar un NO ENCONTRADO
                return NotFound();
            }

            //Si se encuentra el registro, se alteran los campos a modificar
            equipoExiste.nombre = equipoAModificar.nombre;
            equipoExiste.descripcion = equipoAModificar.descripcion;

            //Se envia el objeto a la base de datos.
            _context.Entry(equipoExiste).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(equipoExiste);
        }
    }
}
