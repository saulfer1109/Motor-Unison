using System.Collections.ObjectModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Motores_Unison_Core.Modelos;
using Motores_Unison_Core.Servicios;
using System.Windows.Threading;

namespace Motores_Unison_App.ViewModels;

public class InicioViewModel : BaseViewModel
{
    private readonly DatosServicio _datosServicio;
    private ObservableCollection<ISeries> _series;
    private ObservableCollection<Datos> _datosList;
    private readonly DispatcherTimer _timer;

    public InicioViewModel(DatosServicio datosServicio)
    {
        _datosServicio = datosServicio;
        _series = new ObservableCollection<ISeries>();
        _datosList = new ObservableCollection<Datos>();

        // Configurar el timer para actualizar cada segundo
        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _timer.Tick += Timer_Tick;
        _timer.Start();

        CargarDatosAsync();
    }

    private async void Timer_Tick(object? sender, EventArgs e)
    {
        await CargarDatosAsync();
    }

    public ObservableCollection<ISeries> Series
    {
        get => _series;
        set => SetField(ref _series, value);
    }

    public ObservableCollection<Datos> DatosList
    {
        get => _datosList;
        set => SetField(ref _datosList, value);
    }

    private async Task CargarDatosAsync()
    {
        try
        {
            var datos = await _datosServicio.ObtenerTodosDatos();
            DatosList = new ObservableCollection<Datos>(datos.OrderBy(d => d.Tiempo).TakeLast(50));
            ActualizarGrafica();
        }
        catch (Exception ex)
        {
            // Manejar el error según sea necesario
            System.Diagnostics.Debug.WriteLine($"Error al cargar datos: {ex.Message}");
        }
    }

    private void ActualizarGrafica()
    {
        var series = new ObservableCollection<ISeries>
        {
            new LineSeries<int>
            {
                Name = "Acelerómetro X",
                Values = DatosList.Select(d => d.acc_x).ToArray(),
                Stroke = new SolidColorPaint(SKColors.Red) { StrokeThickness = 2 },
                GeometrySize = 0
            },
            new LineSeries<int>
            {
                Name = "Acelerómetro Y",
                Values = DatosList.Select(d => d.acc_y).ToArray(),
                Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 2 },
                GeometrySize = 0
            },
            new LineSeries<int>
            {
                Name = "Acelerómetro Z",
                Values = DatosList.Select(d => d.acc_z).ToArray(),
                Stroke = new SolidColorPaint(SKColors.Green) { StrokeThickness = 2 },
                GeometrySize = 0
            }
        };

        Series = series;
    }
}
