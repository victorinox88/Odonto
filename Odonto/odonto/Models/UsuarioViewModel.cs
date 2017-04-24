using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace odonto.Models
{
    public class UsuarioViewModel: BaseViewModel
    {
        public long Id { get; set; }
        [DisplayName("Username")]
        public string Login { get; set; }
        [DisplayName("Contrasena")]
        public string Password { get; set; }
        [DisplayName("Usuario")]
        public long IdDoctor { get; set; }
        [DisplayName("Rol")]
        public long IdRol { get; set; }
    }
}