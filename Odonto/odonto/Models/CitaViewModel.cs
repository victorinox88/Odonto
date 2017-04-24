using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace odonto.Models
{
    public class CitaViewModel: BaseViewModel
    {
        public CitaViewModel() { }

        public CitaViewModel(CITA model)
        {
            this.ID = model.ID;
            this.ID_DOCTOR = model.ID_DOCTOR;
            this.ID_PACIENTE = model.ID_PACIENTE;
            this.FECHA = model.FECHA.ToString("dd-MM-yyyy");
            this.ESTATUS = model.ESTATUS;
            this.nombrePaciente = model.PACIENTE.NOMBRE + " " + model.PACIENTE.APELLIDO;
            this.nombreDoctor = model.DOCTOR.NOMBRE + " " + model.DOCTOR.APELLIDO;
            if (model.HISTORIALs != null && model.HISTORIALs.Count != 0)
            {
                this.historial = new List<paciente_historial>();
                foreach (HISTORIAL his in model.HISTORIALs)
                {
                    this.historial.Add(new paciente_historial(his));
                }
            }
        }

        public long ID { get; set; }
        public long ID_DOCTOR { get; set; }
        public long ID_PACIENTE { get; set; }
        public string FECHA { get; set; }
        [DisplayName("Estatus")]
        public bool ESTATUS { get; set; }
        public string nombrePaciente { get; set; }
        public string nombreDoctor { get; set; }
        public Nullable<System.DateTime> FECHA_MODIFICACION { get; set; }
        public ICollection<paciente_historial> historial { get; set; }
    }
}