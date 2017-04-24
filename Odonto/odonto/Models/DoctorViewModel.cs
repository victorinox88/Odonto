using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace odonto.Models
{
    public class DoctorViewModel: BaseViewModel
    {
        public DoctorViewModel() { }

        public DoctorViewModel(DOCTOR model)
        {
            this.ID = model.ID;
            this.NOMBRE = model.NOMBRE;
            this.SEGUNDO_NOMBRE = model.SEGUNDO_NOMBRE;
            this.APELLIDO = model.APELLIDO;
            this.SEGUNDO_APELLIDO = model.SEGUNDO_APELLIDO;
            this.LOGIN = model.LOGIN;
            this.PASSWORD = model.PASSWORD;
            this.ID_ROL = model.ID_ROL;
            this.correo = model.EMAIL;
            if (model.RELACION_TELEFONO != null && model.RELACION_TELEFONO.Count != 0)
            {
                foreach (RELACION_TELEFONO rt in model.RELACION_TELEFONO)
                {
                    switch (rt.TELEFONO.TIPOCHECK)
                    {
                        case "Telefono de Oficina":
                            this.TELEOFICINA = rt.TELEFONO.NUMERO;
                            break;
                        case "Telefono de Casa":
                            this.TELECASA = rt.TELEFONO.NUMERO;
                            break;
                        case "Telefono Celular":
                            this.TELECELULAR = rt.TELEFONO.NUMERO;
                            break;
                    }
                }
            }
           
        }

        public long ID { get; set; }
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
        [DisplayName("Contraseña")]
        public string PASSWORD { get; set; }
        [DisplayName("Rol")]
        public long ID_ROL { get; set; }
        [DisplayName("Login")]
        public string LOGIN { get; set; }
        [DisplayName("Seguros")]
        public long[] aseguradora { get; set; }
        public List<SelectListItem> listaInstituto { get; set; }
        [DisplayName("Instituto")]
        public long instituto { get; set; }

        /// <summary>
        /// Contrato
        /// </summary>
        public long ID_CONTRATO { get; set; }
        public Nullable<long> ID_INSTITUCION { get; set; }
        [DisplayName("Seguros")]
        public long ID_ASEGURADORA { get; set; }
        public bool ESTATUS_CONTRATO { get; set; }
        public List<DoctorViewModel> listaContratos { get; set; }
    }
}