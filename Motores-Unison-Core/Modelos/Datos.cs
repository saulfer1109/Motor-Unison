namespace Motores_Unison_Core.Modelos;

public class Datos
{
    public Guid Id { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public long Tiempo { get; set; } = string.Empty;
    public int acc_x { get; set; }
    public int acc_y { get; set; }
    public int acc_z { get; set; }
    public int gyro_x { get; set; }
    public int gyro_y { get; set; }
    public int gyro_z { get; set; }
}