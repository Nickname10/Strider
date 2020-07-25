﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Authorization.RegistrationService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RegistrationService.IRegistration")]
    public interface IRegistration {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistration/Register", ReplyAction="http://tempuri.org/IRegistration/RegisterResponse")]
        bool Register(string email, string nickname, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRegistration/Register", ReplyAction="http://tempuri.org/IRegistration/RegisterResponse")]
        System.Threading.Tasks.Task<bool> RegisterAsync(string email, string nickname, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRegistrationChannel : Authorization.RegistrationService.IRegistration, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RegistrationClient : System.ServiceModel.ClientBase<Authorization.RegistrationService.IRegistration>, Authorization.RegistrationService.IRegistration {
        
        public RegistrationClient() {
        }
        
        public RegistrationClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RegistrationClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RegistrationClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RegistrationClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Register(string email, string nickname, string password) {
            return base.Channel.Register(email, nickname, password);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterAsync(string email, string nickname, string password) {
            return base.Channel.RegisterAsync(email, nickname, password);
        }
    }
}
