using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace odonto.Models
{
    public class PacienteViewModel: BaseViewModel
    {
        public PacienteViewModel()
        {
            listaEnfermedad = new List<SelectListItem>();
        }

        public PacienteViewModel(PACIENTE model)
        {
            this.ID = model.ID;
            this.FECHA_NACIMIENTO = model.FECHA_NACIMIENTO.ToString();
            this.NOMBRE = model.NOMBRE;
            this.SEGUNDO_NOMBRE = model.SEGUNDO_NOMBRE;
            this.APELLIDO = model.APELLIDO;
            this.SEGUNDO_APELLIDO = model.SEGUNDO_APELLIDO;
            this.FECHA_INGRESO = model.FECHA_INGRESO.ToString();
            this.ID_PACIENTE = model.ID_PACIENTE;
            this.SEXO = model.SEXO;
            if (model.RELACION_TELEFONO != null && model.RELACION_TELEFONO.Count!=0)
            {
                foreach(RELACION_TELEFONO rt in model.RELACION_TELEFONO)
                {
                    switch(rt.TELEFONO.TIPOCHECK)
                    {
                        case "Telefono de Oficina": this.TELEOFICINA = rt.TELEFONO.NUMERO;
                            break;
                        case "Telefono de Casa": this.TELECASA = rt.TELEFONO.NUMERO;
                            break;
                        case "Telefono Celular": this.TELECELULAR = rt.TELEFONO.NUMERO;
                            break;
                    }
                }
            }
            this.Enfermedades = model.PACIENTE_ENFERMEDAD.ToList();
            this.familiar = model.PACIENTE2;
            listaEnfermedad = new List<SelectListItem>();
            this.hist = model.HISTORIALs.ToList();
            this.cedula = model.CEDULA;
            if (model.PACIENTE_ASEGURADORA != null && model.PACIENTE_ASEGURADORA.Count != 0)
            {
                int cont = 0;
                this.seguro = new long[model.PACIENTE_ASEGURADORA.Count]; 
                foreach (PACIENTE_ASEGURADORA pas in model.PACIENTE_ASEGURADORA)
                {
                    this.seguro[cont] = pas.ID_ASEGURADORA;
                    cont++;
                }
                paciente_asegu = model.PACIENTE_ASEGURADORA;
            }
            /*if (this.hist != null && this.hist.Count != 0)
            {
                this.historial = new List<paciente_historial>();
                foreach (HISTORIAL hi in this.hist)
                {
                    this.historial.Add(new paciente_historial(hi));
                }
            }*/
        }

        public long ID { get; set; }
        [DisplayName("Tratamiento")]
        public long Id_TRATAMIENTO { get; set; }
        [DisplayName("Primer Nombre")]
        public string NOMBRE { get; set; }
        [DisplayName("Segundo Nombre")]
        public string SEGUNDO_NOMBRE { get; set; }
        [DisplayName("Primer Apellido")]
        public string APELLIDO { get; set; }
        [DisplayName("Segundo Apellido")]
        public string SEGUNDO_APELLIDO { get; set; }
        [DisplayName("Familiar")]
        public Nullable<long> ID_PACIENTE { get; set; }
        [DisplayName("Correo Electronico")]
        public string correo { get; set; }
        [DisplayName("Tipo de Enfermedad")]
        public string tipo { get; set; }
        public string tipoModal { get; set; }
        [DisplayName("Enfermedad")]
        public long enfermedad { get; set; }
        public List<SelectListItem> listaEnfermedad { get; set; }
        public List<PACIENTE_ENFERMEDAD> Enfermedades { get; set; }
        [DisplayName("Familiar")]
        public PACIENTE familiar { get; set; }
        [DisplayName("Descripcion")]
        public string descripcion { get; set; }
        //public DIENTE diente { get; set; }
        public string svg { get; set; }
        public List<paciente_historial> historial { get; set; }
        public List<HISTORIAL> hist { get; set; }
        public CITA cita { get; set; }
        [DisplayName("Cédula")]
        public string cedula { get; set; }
        [DisplayName("Seguro")]
        public long[] seguro { get; set; }
        [DisplayName("Cobertura")]
        public decimal cobertura { get; set;}
        public ICollection<PACIENTE_ASEGURADORA> paciente_asegu { get; set; }
    }

    public class paciente_telefono
    {
        public long ID { get; set;}
        public Nullable<long> ID_PACIENTE { get; set; }
        public Nullable<long> ID_DOCTOR { get; set; }
        public Nullable<long> ID_GERENTE { get; set; }
        public Nullable<long> ID_SECRETARIA { get; set; }
        public long ID_TELEFONO { get; set; }
        public TELEFONO telefono
        {
            get
            {
                return CargarTelefono(ID_TELEFONO);
            }
            set
            {
                this.telefono = value;
            }
        }


        public TELEFONO CargarTelefono(long idtelefono)
        {
            NEOODONTOEntities db = new NEOODONTOEntities();
            return db.TELEFONOes.Find(idtelefono);
        }
    }

    public class paciente_historial
    {
        private NEOODONTOEntities db = new NEOODONTOEntities();

        public paciente_historial() { }

        public paciente_historial(HISTORIAL model)
        {
            this.id = model.ID;
            this.id_paciente = model.ID_PACIENTE;
            this.id_cita = model.ID_CITA;
            this.diente = model.DIENTE;
            this.svg_doctor = model.SVG_DOCTOR;
            this.svg_modal = model.SVG_MODAL;
            this.svg_paciente = model.SVG_PACIENTE;

            if (model.DETALLE_HISTORIAL != null && model.DETALLE_HISTORIAL.Count != 0)
            {
                this.listaDetalle = new List<Detalle>();
                foreach(DETALLE_HISTORIAL de in model.DETALLE_HISTORIAL)
                {
                    this.listaDetalle.Add(new Detalle(de));
                }
            }
        }

        public long id { get; set; }
        public long id_paciente { get; set; }
        public long? id_cita { get; set; }
        public string diente { get; set; }
        public string svg_doctor { get; set; }
        public string svg_paciente { get; set; }
        public string svg_modal { get; set; }
        public List<Detalle> listaDetalle { get; set; }

    }

    public class Detalle
    {
        public Detalle() { }

        public Detalle(DETALLE_HISTORIAL model)
        {
            this.id = model.ID;
            this.id_trata_diente = model.ID_TRATA_DIENTE;
            this.id_historial = model.ID_HISTORIAL;
            this.descripcion = model.DESCRIPCION;
            this.posicion = model.POSICION;
            this.fecha = model.FECHA.ToString("dd-MM-yyyy");
            this.id_tratamiento = model.TRATA_DIENTE.ID_TRATAMIENTO;
            this.color = model.TRATA_DIENTE.TRATAMIENTO.COLOR;
            this.diente = model.TRATA_DIENTE.DIENTE.NOMBRE;
            this.nombreTratamiento = model.TRATA_DIENTE.TRATAMIENTO.NOMBRE;
            this.monto = model.TRATA_DIENTE.TRATAMIENTO.MONTO;
        }

        public long id { get; set; }
        public long id_tratamiento { get; set; }
        public long id_trata_diente { get; set; }
        public long id_historial { get; set; }
        public string fecha { get; set; }
        public string descripcion { get; set; }
        public string color { get; set; }
        public Nullable<int> posicion { get; set; }
        public string diente { get; set; }
        public string nombreTratamiento { get; set; }
        public decimal? monto { get; set; }
    }

}