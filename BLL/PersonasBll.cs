using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RegistroPersonasExtra.DAL;
using RegistroPersonasExtra.Entidades;

namespace RegistroPersonasExtra.BLL
{
    public class PersonasBll
    {
        public static bool Guardar(Personas persona)
        {
            bool paso = false;
            Contexto contexto = new Contexto();

            try
            {
                if (contexto.Personas.Add(persona) != null)
                    paso = contexto.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }
    }
}
