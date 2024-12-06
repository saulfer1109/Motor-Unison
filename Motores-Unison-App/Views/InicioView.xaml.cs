using System.Windows.Controls;
using Motores_Unison_Core.BaseDeDatos;
using Motores_Unison_Core.Servicios;
using Motores_Unison_Core.ViewModels;

namespace Motores_Unison_Core.Views;

public partial class InicioView : UserControl
{
    public InicioView()
    {
        InitializeComponent();
        var db = new DatosDB();
        db.Database.EnsureCreated();
        var servicio = new DatosServicio(db);
        DataContext = new InicioViewModel(servicio);
    }
}
