using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class DpradoProgramacionNcapasContext : DbContext
{
    public DpradoProgramacionNcapasContext()
    {
    }

    public DpradoProgramacionNcapasContext(DbContextOptions<DpradoProgramacionNcapasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Colonium> Colonia { get; set; }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<HistorialPermiso> HistorialPermisos { get; set; }

    public virtual DbSet<Municipio> Municipios { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<StatusPermiso> StatusPermisos { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VwEmpleado> VwEmpleados { get; set; }

    public virtual DbSet<VwUserProfile> VwUserProfiles { get; set; }

    public virtual DbSet<VwUsuario> VwUsuarios { get; set; }

    public virtual DbSet<DTOs.LoginInfo> LoginInfo { get; set; }
    public virtual DbSet<DTOs.GetBoss> GetBosses { get; set; }
    public virtual DbSet<DTOs.GetEmailByIdPermiso> GetEmailByIdPermisos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<DTOs.LoginInfo>(entity =>
        {
            entity.HasNoKey();
        }
          );

        modelBuilder.Entity<DTOs.GetBoss>(entity =>
        {
            entity.HasNoKey();
        }
          );

        modelBuilder.Entity<DTOs.GetEmailByIdPermiso>(entity =>
        {
            entity.HasNoKey();
        }
          );

        modelBuilder.Entity<Colonium>(entity =>
        {
            entity.HasKey(e => e.IdColonia).HasName("PK__Colonia__A1580F66BFD61DDD");

            entity.Property(e => e.IdColonia).ValueGeneratedNever();
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdMunicipioNavigation).WithMany(p => p.Colonia)
                .HasForeignKey(d => d.IdMunicipio)
                .HasConstraintName("FK__Colonia__IdMunic__2A4B4B5E");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__787A433D7A39F183");

            entity.ToTable("Departamento");

            entity.Property(e => e.IdDepartamento).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.HasKey(e => e.IdDireccion).HasName("PK__Direccio__1F8E0C76F6983156");

            entity.ToTable("Direccion");

            entity.Property(e => e.Calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroExterior)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NumeroInterior)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdColoniaNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.IdColonia)
                .HasConstraintName("FK__Direccion__IdCol__35BCFE0A");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Direccions)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Direccion__IdUsu__36B12243");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9E1B6A15F3");

            entity.ToTable("Empleado");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Curp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nss)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NSS");
            entity.Property(e => e.Rfc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RFC");
            entity.Property(e => e.SalarioBase).HasColumnType("money");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK__Empleado__IdDepa__5EBF139D");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__Estado__FBB0EDC1F6AAE634");

            entity.ToTable("Estado");

            entity.Property(e => e.IdEstado).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HistorialPermiso>(entity =>
        {
            entity.HasKey(e => e.IdHistorialPermiso).HasName("PK__Historia__0000E4BF3B23DA73");

            entity.ToTable("HistorialPermiso");

            entity.Property(e => e.Observaciones)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.AprovoRechazoNavigation).WithMany(p => p.HistorialPermisos)
                .HasForeignKey(d => d.AprovoRechazo)
                .HasConstraintName("FK__Historial__Aprov__7E37BEF6");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.HistorialPermisos)
                .HasForeignKey(d => d.IdPermiso)
                .HasConstraintName("FK__Historial__IdPer__7C4F7684");

            entity.HasOne(d => d.IdStatusPermisoNavigation).WithMany(p => p.HistorialPermisos)
                .HasForeignKey(d => d.IdStatusPermiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Historial__IdSta__7D439ABD");
        });

        modelBuilder.Entity<Municipio>(entity =>
        {
            entity.HasKey(e => e.IdMunicipio).HasName("PK__Municipi__61005978A5F33F2D");

            entity.ToTable("Municipio");

            entity.Property(e => e.IdMunicipio).ValueGeneratedNever();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Municipios)
                .HasForeignKey(d => d.IdEstado)
                .HasConstraintName("FK__Municipio__IdEst__276EDEB3");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK__Permiso__0D626EC86BBF0723");

            entity.ToTable("Permiso");

            entity.Property(e => e.Motivo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdAutorizadorNavigation).WithMany(p => p.PermisoIdAutorizadorNavigations)
                .HasForeignKey(d => d.IdAutorizador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Permiso__IdAutor__797309D9");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.PermisoIdEmpleadoNavigations)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Permiso__IdEmple__778AC167");

            entity.HasOne(d => d.IdStatusPermisoNavigation).WithMany(p => p.Permisos)
                .HasForeignKey(d => d.IdStatusPermiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Permiso__IdStatu__787EE5A0");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584C2EE226C4");

            entity.ToTable("Rol");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusPermiso>(entity =>
        {
            entity.HasKey(e => e.IdStatusPermiso).HasName("PK__StatusPe__D8526C0774A89F0F");

            entity.ToTable("StatusPermiso");

            entity.Property(e => e.IdStatusPermiso).ValueGeneratedNever();
            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.IdUserProfile).HasName("PK__UserProf__2CAC89ABE08BDE83");

            entity.ToTable("UserProfile");

            entity.HasIndex(e => e.Email, "UQ_Email").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UserName)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK__UserProfi__IdEmp__06CD04F7");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__UserProfi__IdRol__07C12930");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97F0946C6F");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.UserName, "UQ__Usuario__C9F2845673910DA9").IsUnique();

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Curp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK_Usuario_Rol");
        });

        modelBuilder.Entity<VwEmpleado>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwEmpleado");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Curp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nss)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NSS");
            entity.Property(e => e.Rfc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("RFC");
            entity.Property(e => e.SalarioBase).HasColumnType("money");
        });

        modelBuilder.Entity<VwUserProfile>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwUserProfile");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.RolType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwUsuario>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwUsuario");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Calle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Curp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FechaNacimiento)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.NumeroExterior)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.NumeroInterior)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Sexo)
                .HasMaxLength(2)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
