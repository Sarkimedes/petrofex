﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PetrofexSystem.PricesServer.Client.FuelSupplyService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FuelPriceQuote", Namespace="http://schemas.datacontract.org/2004/07/PetroFexHQ.Services")]
    [System.SerializableAttribute()]
    public partial class FuelPriceQuote : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime QuoteDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private PetrofexSystem.PricesServer.Client.FuelSupplyService.FuelPrice[] QuotePricesField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid QuoteReferenceField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime QuoteDate {
            get {
                return this.QuoteDateField;
            }
            set {
                if ((this.QuoteDateField.Equals(value) != true)) {
                    this.QuoteDateField = value;
                    this.RaisePropertyChanged("QuoteDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public PetrofexSystem.PricesServer.Client.FuelSupplyService.FuelPrice[] QuotePrices {
            get {
                return this.QuotePricesField;
            }
            set {
                if ((object.ReferenceEquals(this.QuotePricesField, value) != true)) {
                    this.QuotePricesField = value;
                    this.RaisePropertyChanged("QuotePrices");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid QuoteReference {
            get {
                return this.QuoteReferenceField;
            }
            set {
                if ((this.QuoteReferenceField.Equals(value) != true)) {
                    this.QuoteReferenceField = value;
                    this.RaisePropertyChanged("QuoteReference");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FuelPrice", Namespace="http://schemas.datacontract.org/2004/07/PetroFexHQ.Services")]
    [System.SerializableAttribute()]
    public partial class FuelPrice : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double PriceField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Price {
            get {
                return this.PriceField;
            }
            set {
                if ((this.PriceField.Equals(value) != true)) {
                    this.PriceField = value;
                    this.RaisePropertyChanged("Price");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FuelPurchaseOrder", Namespace="http://schemas.datacontract.org/2004/07/PetroFexHQ.Services")]
    [System.SerializableAttribute()]
    public partial class FuelPurchaseOrder : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FuelTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OrderReferenceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double QuantityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid QuoteReferenceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int StationIdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FuelType {
            get {
                return this.FuelTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.FuelTypeField, value) != true)) {
                    this.FuelTypeField = value;
                    this.RaisePropertyChanged("FuelType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OrderReference {
            get {
                return this.OrderReferenceField;
            }
            set {
                if ((object.ReferenceEquals(this.OrderReferenceField, value) != true)) {
                    this.OrderReferenceField = value;
                    this.RaisePropertyChanged("OrderReference");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Quantity {
            get {
                return this.QuantityField;
            }
            set {
                if ((this.QuantityField.Equals(value) != true)) {
                    this.QuantityField = value;
                    this.RaisePropertyChanged("Quantity");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid QuoteReference {
            get {
                return this.QuoteReferenceField;
            }
            set {
                if ((this.QuoteReferenceField.Equals(value) != true)) {
                    this.QuoteReferenceField = value;
                    this.RaisePropertyChanged("QuoteReference");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int StationId {
            get {
                return this.StationIdField;
            }
            set {
                if ((this.StationIdField.Equals(value) != true)) {
                    this.StationIdField = value;
                    this.RaisePropertyChanged("StationId");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="FuelPurchaseConfirmation", Namespace="http://schemas.datacontract.org/2004/07/PetroFexHQ.Services")]
    [System.SerializableAttribute()]
    public partial class FuelPurchaseConfirmation : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FuelTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OrderReferenceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double QuantityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double TotalPriceField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FuelType {
            get {
                return this.FuelTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.FuelTypeField, value) != true)) {
                    this.FuelTypeField = value;
                    this.RaisePropertyChanged("FuelType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OrderReference {
            get {
                return this.OrderReferenceField;
            }
            set {
                if ((object.ReferenceEquals(this.OrderReferenceField, value) != true)) {
                    this.OrderReferenceField = value;
                    this.RaisePropertyChanged("OrderReference");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Quantity {
            get {
                return this.QuantityField;
            }
            set {
                if ((this.QuantityField.Equals(value) != true)) {
                    this.QuantityField = value;
                    this.RaisePropertyChanged("Quantity");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double TotalPrice {
            get {
                return this.TotalPriceField;
            }
            set {
                if ((this.TotalPriceField.Equals(value) != true)) {
                    this.TotalPriceField = value;
                    this.RaisePropertyChanged("TotalPrice");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FuelSupplyService.IFuelSupplyService")]
    public interface IFuelSupplyService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFuelSupplyService/GetFuelPrices", ReplyAction="http://tempuri.org/IFuelSupplyService/GetFuelPricesResponse")]
        PetrofexSystem.PricesServer.Client.FuelSupplyService.FuelPriceQuote GetFuelPrices(int StationId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFuelSupplyService/PurchaseFuel", ReplyAction="http://tempuri.org/IFuelSupplyService/PurchaseFuelResponse")]
        PetrofexSystem.PricesServer.Client.FuelSupplyService.FuelPurchaseConfirmation PurchaseFuel(PetrofexSystem.PricesServer.Client.FuelSupplyService.FuelPurchaseOrder purchaseOrder);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFuelSupplyServiceChannel : PetrofexSystem.PricesServer.Client.FuelSupplyService.IFuelSupplyService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FuelSupplyServiceClient : System.ServiceModel.ClientBase<PetrofexSystem.PricesServer.Client.FuelSupplyService.IFuelSupplyService>, PetrofexSystem.PricesServer.Client.FuelSupplyService.IFuelSupplyService {
        
        public FuelSupplyServiceClient() {
        }
        
        public FuelSupplyServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FuelSupplyServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FuelSupplyServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FuelSupplyServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public PetrofexSystem.PricesServer.Client.FuelSupplyService.FuelPriceQuote GetFuelPrices(int StationId) {
            return base.Channel.GetFuelPrices(StationId);
        }
        
        public PetrofexSystem.PricesServer.Client.FuelSupplyService.FuelPurchaseConfirmation PurchaseFuel(PetrofexSystem.PricesServer.Client.FuelSupplyService.FuelPurchaseOrder purchaseOrder) {
            return base.Channel.PurchaseFuel(purchaseOrder);
        }
    }
}