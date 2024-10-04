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
    [Route("PetAPI")]
    public class PetAPI : Controller
    {
        private readonly TestDBCntext _context;

        public PetAPI(TestDBCntext context)
        {
            _context = context;
        }

        
        [HttpGet]
        [Route("ListPet")]
        public async Task<string> ListPet()
        {
            string json = "";
            var data = await _context.Pet.ToListAsync();
            return data.ToJson();
        }

        
        
       
        [HttpPost]
        [Route("CreatePet")]
        public async Task<string> CreatePet([FromBody]string value)
        {
            var pet = value.parseTo<Pet>();

            string json = "Fallo";

            if (ModelState.IsValid)
            {
                _context.Add(pet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                json = "Guardado correctamente";
            }
            return json;
        }

        
        [HttpPost]
        [Route("EditPet")]
        public async Task<string> EditPet([FromBody]string value)
        {
            var pet = value.parseTo<Pet>();
            string json = "Fallo";

            if (id == null)
            {
                return json;
            }

            var petDB = await _context.Pet.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            

            if (pet!= null) {
                petDB.InputDatoValor = pet.InputDatoValor;
                petDB.FechaIngreso = pet.FechaIngreso;
                petDB.FechaIngreso = pet.FechaIngreso;

                await _context.Pet.Update(pet);
                return "Ok";
            }

            return json;
            
        }


        [HttpPost]
        [Route("DeletePet")]
        public async Task<string> DeletePet([FromBody]string value)
        {
            var id = value.parseTo<int>();

            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pet == null)
            {
                return NotFound();
            }
            await _context.Pet.Delete(m => m.Id == id);
            json = "Correcto";

            return json;
        }
        
    }
}
