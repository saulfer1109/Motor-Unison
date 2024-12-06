using Microsoft.EntityFrameworkCore;
using Motores_Unison_Core.BaseDeDatos;
using Motores_Unison_Core.Modelos;

namespace Motores_Unison_Core.Servicios;

public class DatosServicio
{
    private readonly DatosDB _context;

    public DatosServicio(DatosDB context)
    {
        _context = context;
    }

    public async Task<List<Datos>> ObtenerTodosDatos()
    {
        return await _context.Datos.ToListAsync();
    }

    public async Task<List<Datos>> ObtenerDatosPorTipo(string tipo)
    {
        return await _context.Datos
            .Where(d => d.Tipo == tipo)
            .ToListAsync();
    }

    public async Task<Datos?> ObtenerDatoPorId(Guid id)
    {
        return await _context.Datos.FindAsync(id);
    }

    public async Task GuardarDatos(Datos datos)
    {
        if (datos.Id == Guid.Empty)
        {
            datos.Id = Guid.NewGuid();
            await _context.Datos.AddAsync(datos);
        }
        else
        {
            _context.Datos.Update(datos);
        }
        await _context.SaveChangesAsync();
    }

    public async Task EliminarDatos(Guid id)
    {
        var datos = await _context.Datos.FindAsync(id);
        if (datos != null)
        {
            _context.Datos.Remove(datos);
            await _context.SaveChangesAsync();
        }
    }
}
