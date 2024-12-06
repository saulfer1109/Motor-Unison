using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Motores_Unison_Core.Modelos;
using System.IO;

namespace Motores_Unison_Core.BaseDeDatos;

public class DatosDB : DbContext
{
    private const string nombreBaseDeDatos = "database.sqlite";
    private readonly string _databasePath;

    public DatosDB()
    {
        var directorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "BaseDeDatos");
        _databasePath = Path.Combine(directorio, nombreBaseDeDatos);
    }

    public DbSet<Datos> Datos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={_databasePath}");
    }

    public async Task AsegurarBaseDeDatosCreada()
    {
        await Database.EnsureCreatedAsync();
    }
}