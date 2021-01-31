using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace FirstSchool.Models
{
    public class EscuelaContext : DbContext
    {
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Evaluacion> Evaluaciones { get; set; }
        public EscuelaContext(DbContextOptions<EscuelaContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var escuela = new Escuela();
            escuela.Since = 2005;
            escuela.Id = Guid.NewGuid().ToString();
            escuela.Nombre = "Gabriel School";
            escuela.Dirección = "Avenida de los de aca";
            escuela.Ciudad = "Bogotá";
            escuela.Pais = "Colombia";
            escuela.TipoEscuela = TiposEscuela.Secundaria;

            //Cargar Cursos de la escuela
            var cursos = CargarCursos(escuela);
            //X c/curso cargar Asignaturas
            var asignaturas = CargarAsignaturas(cursos);
            //X c/curso cargar Alumnos
            var alumnos = CargarAlumnos(cursos);

            modelBuilder.Entity<Escuela>().HasData(escuela);
            modelBuilder.Entity<Curso>().HasData(cursos.ToArray());
            modelBuilder.Entity<Asignatura>().HasData(asignaturas.ToArray());
            modelBuilder.Entity<Alumno>().HasData(alumnos.ToArray());
        }

        private List<Alumno> CargarAlumnos(List<Curso> cursos)
        {
            var listaAlumnos = new List<Alumno>();
            Random rnd = new Random();
            foreach (var curso in cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                var templist = GenerarAlumnosAlAzar(curso, cantRandom);
                listaAlumnos.AddRange(templist);
            }
            return listaAlumnos;
        }

        private static List<Asignatura> CargarAsignaturas(List<Curso> cursos)
        {
            var listaCompleta = new List<Asignatura>();
            foreach (var curso in cursos)
            {
                var tempList = new List<Asignatura>
                {
                    new Asignatura { Id = Guid.NewGuid().ToString(), Nombre = "Matematicas", CursoId = curso.Id },
                    new Asignatura { Id = Guid.NewGuid().ToString(), Nombre = "Programacion", CursoId = curso.Id },
                    new Asignatura { Id = Guid.NewGuid().ToString(), Nombre = "Idiomas", CursoId = curso.Id },
                    new Asignatura { Id = Guid.NewGuid().ToString(), Nombre = "Ciencia", CursoId = curso.Id },
                    new Asignatura { Id = Guid.NewGuid().ToString(), Nombre = "Recocha", CursoId = curso.Id }
                };
                listaCompleta.AddRange(tempList);
                //curso.Asignaturas = tempList;
            }

            return listaCompleta;
        }

        private static List<Curso> CargarCursos(Escuela escuela)
        {
            return new List<Curso>(){
                new Curso(){Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "101", Jornada = TiposJornada.Mañana },
                new Curso(){Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "201", Jornada = TiposJornada.Mañana },
                new Curso(){Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "301", Jornada = TiposJornada.Tarde },
                new Curso(){Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "401", Jornada = TiposJornada.Tarde },
                new Curso(){Id = Guid.NewGuid().ToString(), EscuelaId = escuela.Id, Nombre = "501", Jornada = TiposJornada.Tarde },
            };
        }

        private List<Alumno> GenerarAlumnosAlAzar(Curso curso, int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Gabriel" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno { Id = Guid.NewGuid().ToString(), Nombre = $"{n1} {n2} {a1}", CursoId = curso.Id };

            return listaAlumnos.OrderBy((al) => al.Id).Take(cantidad).ToList();
        }
        
    }
}
