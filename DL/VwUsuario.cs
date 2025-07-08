﻿using System;
using System.Collections.Generic;

namespace DL;

public partial class VwUsuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Sexo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Celular { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string Curp { get; set; } = null!;

    public string? Rol { get; set; }

    public int IdRol { get; set; }

    public string? Calle { get; set; }

    public string? NumeroInterior { get; set; }

    public string? NumeroExterior { get; set; }

    public byte[]? Imagen { get; set; }
}
