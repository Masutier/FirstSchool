using System;


namespace FirstSchool.Models
{
    public class Evaluacion : ObjetoEscuelaBase
    {
        public Alumno Alumno { get; set; }
        public String AlumnoId { get; set; }
        public Asignatura Asignatura  { get; set; }
        public String AsignaturaId { get; set; }
        public float Nota { get; set; }

        public override string ToString()
        {
            return $"{Nota}, {Alumno.Nombre}, {Asignatura.Nombre}";
        }
    }
}
