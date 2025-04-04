using System;

namespace api.DTOs.UNPHUClient;

public class StudentCareerDto
{
    public int IdCarrera { get; set; }
    public string? Carrera { get; set; }
    public int IdPensum { get; set; }
    public string? CodigoPensum { get; set; }
    public bool PensumPrimario { get; set; }
}
