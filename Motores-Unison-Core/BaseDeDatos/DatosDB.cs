using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Motores_Unison_Core.BaseDeDatos;
using Motores_Unison_Core.BaseDeDatos.Modelos;

public class DatosDB : DbContext
{
    private const string nombreBaseDeDatos = "Datos.db";

    public DbSet<Datos> Datos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var directorio=AppContext.BaseDirectory+ "/_data";
       
       if(!Directory.Exists(directorio)) Directory.CreateDirectory(directorio);

       optionsBuilder.Sqlite($"Filename={directorio}/{nombreBaseDeDatos}", op=> op.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName));

       base.OnConfiguring(optionsBuilder);
       
    }
    
}