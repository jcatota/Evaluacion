using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API.Context;
using API.Models;

namespace API.Controllers

    // No recuerdo completamente el código, sin embargo escribo la lógica que se debe usar en el API
{
    [ApiController]
    [Route("PersonaAPI")]
    public class PersonaAPI : Controller
    {
        private readonly TestDBCntext _context;

        public PersonaAPI(TestDBCntext context)
        {
            _context = context;
        }

        
        [HttpGet]
        [Route("ListPersona")]
        public async Task<string> ListPersona()
        {
            string json = "";
            var data = await _context.Persona.ToListAsync();
            return data.ToJson();
        }

        
        
       
        [HttpPost]
        [Route("CreatePersona")]
        public async Task<string> CreatePersona([FromBody]string value)
        {
            var persona = value.parseTo<Persona>();

            string json = "Fallo";

            if (ModelState.IsValid)
            {
                _context.Add(persona);
                await _context.SaveChangesAsync();
                json = "Guardado correctamente";
            }
            return json;
        }

        
        [HttpPost]
        [Route("EditPersona")]
        public async Task<string> EditPersona([FromBody]string value)
        {
            var persona = value.parseTo<Persona>();
            string json = "Fallo";

            if (id == null)
            {
                return json;
            }

            var personaDB = await _context.Persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
    }

            if (persona!= null) {
                personaDB.InputDatoValor = persona.InputDatoValor;
                personaDB.FechaIngreso = persona.FechaIngreso;
                personaDB.FechaIngreso = persona.FechaIngreso;

                await _context.Persona.Update(persona);
                return "Ok";
            }

            return json;
            
        }


        [HttpPost]
        [Route("DeletePersona")]
        public async Task<string> DeletePersona([FromBody]string value)
        {
            var id = value.parseTo<int>();

            if (id == null)
            {
                return NotFound();
            }

            var persona = await _context.Persona
                .FirstOrDefaultAsync(m => m.Id == id);
            if (persona == null)
            {
                return NotFound();
            }
            await _context.Persona.Delete(m => m.Id == id);
            json = "Correcto";

            return json;
        }
        
    }
}
