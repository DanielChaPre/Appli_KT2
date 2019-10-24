//------------------------------------------------------------------------------
// <generado automáticamente>
//     Este código fue generado por una herramienta.
//     //
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </generado automáticamente>
//------------------------------------------------------------------------------

namespace ServiceReference1
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UsuarioClass", Namespace="http://schemas.datacontract.org/2004/07/WebServiceAppli_KT.Modelo")]
    public partial class UsuarioClass : object
    {
        
        private string contraseniaField;
        
        private int cveUsuarioField;
        
        private string estatusField;
        
        private System.DateTime fechaRegistroField;
        
        private string nombreUsuarioField;
        
        private ServiceReference1.PersonaClass personaField;
        
        private string rolField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string contrasenia
        {
            get
            {
                return this.contraseniaField;
            }
            set
            {
                this.contraseniaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int cveUsuario
        {
            get
            {
                return this.cveUsuarioField;
            }
            set
            {
                this.cveUsuarioField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string estatus
        {
            get
            {
                return this.estatusField;
            }
            set
            {
                this.estatusField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime fechaRegistro
        {
            get
            {
                return this.fechaRegistroField;
            }
            set
            {
                this.fechaRegistroField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nombreUsuario
        {
            get
            {
                return this.nombreUsuarioField;
            }
            set
            {
                this.nombreUsuarioField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ServiceReference1.PersonaClass persona
        {
            get
            {
                return this.personaField;
            }
            set
            {
                this.personaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string rol
        {
            get
            {
                return this.rolField;
            }
            set
            {
                this.rolField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PersonaClass", Namespace="http://schemas.datacontract.org/2004/07/WebServiceAppli_KT.Modelo")]
    public partial class PersonaClass : object
    {
        
        private string apellidoMaternoField;
        
        private string apellidoPaternoField;
        
        private string coloniaField;
        
        private string correoElectronicoField;
        
        private string curpField;
        
        private int cvePersonaField;
        
        private int estadoCivilField;
        
        private System.DateTime fechaNacimientoField;
        
        private System.DateTime fechaRegistroField;
        
        private string municipioField;
        
        private string nacionalidadField;
        
        private string nombreField;
        
        private string numeroTelefonoField;
        
        private string perfilField;
        
        private string rfcField;
        
        private string sexoField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string apellidoMaterno
        {
            get
            {
                return this.apellidoMaternoField;
            }
            set
            {
                this.apellidoMaternoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string apellidoPaterno
        {
            get
            {
                return this.apellidoPaternoField;
            }
            set
            {
                this.apellidoPaternoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string colonia
        {
            get
            {
                return this.coloniaField;
            }
            set
            {
                this.coloniaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string correoElectronico
        {
            get
            {
                return this.correoElectronicoField;
            }
            set
            {
                this.correoElectronicoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string curp
        {
            get
            {
                return this.curpField;
            }
            set
            {
                this.curpField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int cvePersona
        {
            get
            {
                return this.cvePersonaField;
            }
            set
            {
                this.cvePersonaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int estadoCivil
        {
            get
            {
                return this.estadoCivilField;
            }
            set
            {
                this.estadoCivilField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime fechaNacimiento
        {
            get
            {
                return this.fechaNacimientoField;
            }
            set
            {
                this.fechaNacimientoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime fechaRegistro
        {
            get
            {
                return this.fechaRegistroField;
            }
            set
            {
                this.fechaRegistroField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string municipio
        {
            get
            {
                return this.municipioField;
            }
            set
            {
                this.municipioField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nacionalidad
        {
            get
            {
                return this.nacionalidadField;
            }
            set
            {
                this.nacionalidadField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string numeroTelefono
        {
            get
            {
                return this.numeroTelefonoField;
            }
            set
            {
                this.numeroTelefonoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string perfil
        {
            get
            {
                return this.perfilField;
            }
            set
            {
                this.perfilField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string rfc
        {
            get
            {
                return this.rfcField;
            }
            set
            {
                this.rfcField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string sexo
        {
            get
            {
                return this.sexoField;
            }
            set
            {
                this.sexoField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="NotificacionClass", Namespace="http://schemas.datacontract.org/2004/07/WebServiceAppli_KT.Modelo")]
    public partial class NotificacionClass : object
    {
        
        private string audienciaField;
        
        private string categorizacionField;
        
        private string colorSemaforizacionField;
        
        private int cveNotificacionesField;
        
        private ServiceReference1.MediosEnvioClass medioDifusionField;
        
        private string responsableField;
        
        private string textoField;
        
        private string tipoNotificacionField;
        
        private string tituloField;
        
        private string urlField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string audiencia
        {
            get
            {
                return this.audienciaField;
            }
            set
            {
                this.audienciaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string categorizacion
        {
            get
            {
                return this.categorizacionField;
            }
            set
            {
                this.categorizacionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string colorSemaforizacion
        {
            get
            {
                return this.colorSemaforizacionField;
            }
            set
            {
                this.colorSemaforizacionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int cveNotificaciones
        {
            get
            {
                return this.cveNotificacionesField;
            }
            set
            {
                this.cveNotificacionesField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ServiceReference1.MediosEnvioClass medioDifusion
        {
            get
            {
                return this.medioDifusionField;
            }
            set
            {
                this.medioDifusionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string responsable
        {
            get
            {
                return this.responsableField;
            }
            set
            {
                this.responsableField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string texto
        {
            get
            {
                return this.textoField;
            }
            set
            {
                this.textoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string tipoNotificacion
        {
            get
            {
                return this.tipoNotificacionField;
            }
            set
            {
                this.tipoNotificacionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string titulo
        {
            get
            {
                return this.tituloField;
            }
            set
            {
                this.tituloField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string url
        {
            get
            {
                return this.urlField;
            }
            set
            {
                this.urlField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MediosEnvioClass", Namespace="http://schemas.datacontract.org/2004/07/WebServiceAppli_KT.Modelo")]
    public partial class MediosEnvioClass : object
    {
        
        private int cveMedioEnvioField;
        
        private string estatusField;
        
        private string nombreField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int cveMedioEnvio
        {
            get
            {
                return this.cveMedioEnvioField;
            }
            set
            {
                this.cveMedioEnvioField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string estatus
        {
            get
            {
                return this.estatusField;
            }
            set
            {
                this.estatusField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nombre
        {
            get
            {
                return this.nombreField;
            }
            set
            {
                this.nombreField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PlantelesESClass", Namespace="http://schemas.datacontract.org/2004/07/WebServiceAppli_KT.Modelo")]
    public partial class PlantelesESClass : object
    {
        
        private string OPDField;
        
        private int activoField;
        
        private string claveInstitucionField;
        
        private string clavePlantelField;
        
        private ServiceReference1.DetallePlantelClass detallePlantelField;
        
        private int idPlantelesESField;
        
        private string municipioField;
        
        private string nivelAgrupadoField;
        
        private string nombreInstitucionEsField;
        
        private string nombrePlantelESField;
        
        private string sostenimientoField;
        
        private string subsistemaField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OPD
        {
            get
            {
                return this.OPDField;
            }
            set
            {
                this.OPDField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int activo
        {
            get
            {
                return this.activoField;
            }
            set
            {
                this.activoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string claveInstitucion
        {
            get
            {
                return this.claveInstitucionField;
            }
            set
            {
                this.claveInstitucionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string clavePlantel
        {
            get
            {
                return this.clavePlantelField;
            }
            set
            {
                this.clavePlantelField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ServiceReference1.DetallePlantelClass detallePlantel
        {
            get
            {
                return this.detallePlantelField;
            }
            set
            {
                this.detallePlantelField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idPlantelesES
        {
            get
            {
                return this.idPlantelesESField;
            }
            set
            {
                this.idPlantelesESField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string municipio
        {
            get
            {
                return this.municipioField;
            }
            set
            {
                this.municipioField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nivelAgrupado
        {
            get
            {
                return this.nivelAgrupadoField;
            }
            set
            {
                this.nivelAgrupadoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nombreInstitucionEs
        {
            get
            {
                return this.nombreInstitucionEsField;
            }
            set
            {
                this.nombreInstitucionEsField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nombrePlantelES
        {
            get
            {
                return this.nombrePlantelESField;
            }
            set
            {
                this.nombrePlantelESField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string sostenimiento
        {
            get
            {
                return this.sostenimientoField;
            }
            set
            {
                this.sostenimientoField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string subsistema
        {
            get
            {
                return this.subsistemaField;
            }
            set
            {
                this.subsistemaField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DetallePlantelClass", Namespace="http://schemas.datacontract.org/2004/07/WebServiceAppli_KT.Modelo")]
    public partial class DetallePlantelClass : object
    {
        
        private string costosField;
        
        private int cveDetallePlantelField;
        
        private System.DateTime fechasField;
        
        private string imagenPlantelField;
        
        private string latitudField;
        
        private string logoPlantelField;
        
        private string longitudField;
        
        private string nivelEstudioField;
        
        private string requisitosField;
        
        private string reseñaField;
        
        private string ubicacionField;
        
        private string urlVinculacionField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string costos
        {
            get
            {
                return this.costosField;
            }
            set
            {
                this.costosField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int cveDetallePlantel
        {
            get
            {
                return this.cveDetallePlantelField;
            }
            set
            {
                this.cveDetallePlantelField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime fechas
        {
            get
            {
                return this.fechasField;
            }
            set
            {
                this.fechasField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string imagenPlantel
        {
            get
            {
                return this.imagenPlantelField;
            }
            set
            {
                this.imagenPlantelField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string latitud
        {
            get
            {
                return this.latitudField;
            }
            set
            {
                this.latitudField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string logoPlantel
        {
            get
            {
                return this.logoPlantelField;
            }
            set
            {
                this.logoPlantelField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string longitud
        {
            get
            {
                return this.longitudField;
            }
            set
            {
                this.longitudField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nivelEstudio
        {
            get
            {
                return this.nivelEstudioField;
            }
            set
            {
                this.nivelEstudioField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string requisitos
        {
            get
            {
                return this.requisitosField;
            }
            set
            {
                this.requisitosField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string reseña
        {
            get
            {
                return this.reseñaField;
            }
            set
            {
                this.reseñaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ubicacion
        {
            get
            {
                return this.ubicacionField;
            }
            set
            {
                this.ubicacionField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string urlVinculacion
        {
            get
            {
                return this.urlVinculacionField;
            }
            set
            {
                this.urlVinculacionField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CarrerasESClass", Namespace="http://schemas.datacontract.org/2004/07/WebServiceAppli_KT.Modelo")]
    public partial class CarrerasESClass : object
    {
        
        private int activaField;
        
        private string campoAmplio2016Field;
        
        private string campoAmplioAnteriorField;
        
        private string campoEspecifico2016Field;
        
        private string campoEspecificoAnteriorField;
        
        private string claveCarreraField;
        
        private int idCarreraESField;
        
        private string nivelField;
        
        private string nombreCarreraESField;
        
        private ServiceReference1.PlantelesESClass plantelESField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int activa
        {
            get
            {
                return this.activaField;
            }
            set
            {
                this.activaField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string campoAmplio2016
        {
            get
            {
                return this.campoAmplio2016Field;
            }
            set
            {
                this.campoAmplio2016Field = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string campoAmplioAnterior
        {
            get
            {
                return this.campoAmplioAnteriorField;
            }
            set
            {
                this.campoAmplioAnteriorField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string campoEspecifico2016
        {
            get
            {
                return this.campoEspecifico2016Field;
            }
            set
            {
                this.campoEspecifico2016Field = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string campoEspecificoAnterior
        {
            get
            {
                return this.campoEspecificoAnteriorField;
            }
            set
            {
                this.campoEspecificoAnteriorField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string claveCarrera
        {
            get
            {
                return this.claveCarreraField;
            }
            set
            {
                this.claveCarreraField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int idCarreraES
        {
            get
            {
                return this.idCarreraESField;
            }
            set
            {
                this.idCarreraESField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nivel
        {
            get
            {
                return this.nivelField;
            }
            set
            {
                this.nivelField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string nombreCarreraES
        {
            get
            {
                return this.nombreCarreraESField;
            }
            set
            {
                this.nombreCarreraESField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public ServiceReference1.PlantelesESClass plantelES
        {
            get
            {
                return this.plantelESField;
            }
            set
            {
                this.plantelESField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/IniciarSesion", ReplyAction="http://tempuri.org/IService1/IniciarSesionResponse")]
        System.Threading.Tasks.Task<bool> IniciarSesionAsync(string user, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CrearCuenta", ReplyAction="http://tempuri.org/IService1/CrearCuentaResponse")]
        System.Threading.Tasks.Task<bool> CrearCuentaAsync(ServiceReference1.UsuarioClass usuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/CrearPerfil", ReplyAction="http://tempuri.org/IService1/CrearPerfilResponse")]
        System.Threading.Tasks.Task<bool> CrearPerfilAsync(ServiceReference1.UsuarioClass usuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ModificarPerfil", ReplyAction="http://tempuri.org/IService1/ModificarPerfilResponse")]
        System.Threading.Tasks.Task<bool> ModificarPerfilAsync(ServiceReference1.UsuarioClass usuario);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/EliminarPerfil", ReplyAction="http://tempuri.org/IService1/EliminarPerfilResponse")]
        System.Threading.Tasks.Task<bool> EliminarPerfilAsync(int cveUsuario, int cvePersona);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/ConsultarPerfil", ReplyAction="http://tempuri.org/IService1/ConsultarPerfilResponse")]
        System.Threading.Tasks.Task<ServiceReference1.UsuarioClass> ConsultarPerfilAsync(string user, string pass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/consultar", ReplyAction="http://tempuri.org/IService1/consultarResponse")]
        System.Threading.Tasks.Task<ServiceReference1.NotificacionClass> consultarAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/obtenerPlanteles", ReplyAction="http://tempuri.org/IService1/obtenerPlantelesResponse")]
        System.Threading.Tasks.Task<ServiceReference1.PlantelesESClass[]> obtenerPlantelesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/obtenerCarreras", ReplyAction="http://tempuri.org/IService1/obtenerCarrerasResponse")]
        System.Threading.Tasks.Task<ServiceReference1.CarrerasESClass[]> obtenerCarrerasAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public interface IService1Channel : ServiceReference1.IService1, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public partial class Service1Client : System.ServiceModel.ClientBase<ServiceReference1.IService1>, ServiceReference1.IService1
    {
        
    /// <summary>
    /// Implemente este método parcial para configurar el punto de conexión de servicio.
    /// </summary>
    /// <param name="serviceEndpoint">El punto de conexión para configurar</param>
    /// <param name="clientCredentials">Credenciales de cliente</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public Service1Client() : 
                base(Service1Client.GetDefaultBinding(), Service1Client.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IService1.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1Client(EndpointConfiguration endpointConfiguration) : 
                base(Service1Client.GetBindingForEndpoint(endpointConfiguration), Service1Client.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1Client(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(Service1Client.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1Client(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(Service1Client.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<bool> IniciarSesionAsync(string user, string pass)
        {
            return base.Channel.IniciarSesionAsync(user, pass);
        }
        
        public System.Threading.Tasks.Task<bool> CrearCuentaAsync(ServiceReference1.UsuarioClass usuario)
        {
            return base.Channel.CrearCuentaAsync(usuario);
        }
        
        public System.Threading.Tasks.Task<bool> CrearPerfilAsync(ServiceReference1.UsuarioClass usuario)
        {
            return base.Channel.CrearPerfilAsync(usuario);
        }
        
        public System.Threading.Tasks.Task<bool> ModificarPerfilAsync(ServiceReference1.UsuarioClass usuario)
        {
            return base.Channel.ModificarPerfilAsync(usuario);
        }
        
        public System.Threading.Tasks.Task<bool> EliminarPerfilAsync(int cveUsuario, int cvePersona)
        {
            return base.Channel.EliminarPerfilAsync(cveUsuario, cvePersona);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.UsuarioClass> ConsultarPerfilAsync(string user, string pass)
        {
            return base.Channel.ConsultarPerfilAsync(user, pass);
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.NotificacionClass> consultarAsync()
        {
            return base.Channel.consultarAsync();
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.PlantelesESClass[]> obtenerPlantelesAsync()
        {
            return base.Channel.obtenerPlantelesAsync();
        }
        
        public System.Threading.Tasks.Task<ServiceReference1.CarrerasESClass[]> obtenerCarrerasAsync()
        {
            return base.Channel.obtenerCarrerasAsync();
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IService1))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IService1))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:50681/Service1.svc");
            }
            throw new System.InvalidOperationException(string.Format("No se pudo encontrar un punto de conexión con el nombre \"{0}\".", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return Service1Client.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IService1);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return Service1Client.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IService1);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_IService1,
        }
    }
}
