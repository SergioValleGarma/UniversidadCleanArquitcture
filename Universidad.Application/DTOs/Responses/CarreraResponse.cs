// Universidad.Application/DTOs/Responses/CarreraResponse.cs
namespace Universidad.Application.DTOs.Responses;

public class CarreraResponse
{
    public int CarreraId { get; set; }
    public int FacultadId { get; set; }
    public string FacultadNombre { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }
    public int DuracionSemestres { get; set; }
    public string TituloOtorgado { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; }
    public bool Activo { get; set; }

    public CarreraResponse() { }

    public CarreraResponse(int carreraId, int facultadId, string facultadNombre,
                          string nombre, string? descripcion, int duracionSemestres,
                          string tituloOtorgado, DateTime fechaRegistro, bool activo)
    {
        CarreraId = carreraId;
        FacultadId = facultadId;
        FacultadNombre = facultadNombre;
        Nombre = nombre;
        Descripcion = descripcion;
        DuracionSemestres = duracionSemestres;
        TituloOtorgado = tituloOtorgado;
        FechaRegistro = fechaRegistro;
        Activo = activo;
    }
}