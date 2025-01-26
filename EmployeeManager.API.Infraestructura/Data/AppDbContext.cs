using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeManager.API.EmployeeManager.API.Dominio.Modelos;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<EmployeeManager.API.EmployeeManager.API.Dominio.Modelos.Empleado> Empleados { get; set; } = default!;
    public DbSet<EmployeeManager.API.EmployeeManager.API.Dominio.Modelos.Horario> Horarios { get; set; }
    public DbSet<EmployeeManager.API.EmployeeManager.API.Dominio.Modelos.SolicitudVacaciones> SolicitudesVacaciones { get; set; }
    public DbSet<EmployeeManager.API.EmployeeManager.API.Dominio.Modelos.FormularioHorario> FormulariosHorarios { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de relaciones y restricciones

        // Relación Empleado -> Horarios (Uno a Muchos)
        modelBuilder.Entity<Horario>()
            .HasOne(h => h.Empleado)
            .WithMany(e => e.Horarios)
            .HasForeignKey(h => h.EmpleadoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relación Empleado -> SolicitudesVacaciones (Uno a Muchos)
        modelBuilder.Entity<SolicitudVacaciones>()
            .HasOne(s => s.Empleado)
            .WithMany(e => e.SolicitudesVacaciones)
            .HasForeignKey(s => s.EmpleadoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relación Empleado -> FormulariosHorarios (Uno a Muchos)
        modelBuilder.Entity<FormularioHorario>()
            .HasOne(f => f.Empleado)
            .WithMany()
            .HasForeignKey(f => f.EmpleadoId)
            .OnDelete(DeleteBehavior.Cascade);
    }


}
