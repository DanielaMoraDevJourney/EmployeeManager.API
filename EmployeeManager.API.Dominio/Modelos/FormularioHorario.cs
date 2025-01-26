namespace EmployeeManager.API.EmployeeManager.API.Dominio.Modelos
{
    public class FormularioHorario
    {
        public int Id { get; set; }
        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }
        public DateTime Fecha { get; set; }
        public bool Llenado { get; set; } // True si el formulario fue llenado
    }

}
