﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3053
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 2.0.50727.3053.
// 
#pragma warning disable 1591

namespace OpenDental.WebHostSynch {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
	using OpenDentBusiness;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WebHostSynchSoap", Namespace="http://opendental.com/")]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(TableBase))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RelatedEnd))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(StructuralObject))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(webforms_sheetfield[]))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EntityKeyMember[]))]
    public partial class WebHostSynch : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SetPreferencesOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetSheetFieldDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetSheetDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteSheetDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback CheckRegistrationKeyOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetDentalOfficeIDOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetWebFormAddressOperationCompleted;
        
        private System.Threading.SendOrPostCallback ReadSheetDefOperationCompleted;
        
        private System.Threading.SendOrPostCallback SheetsInternalMethOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WebHostSynch() {
            this.Url = global::OpenDental.Properties.Settings.Default.OpenDental_WebHostSynch_WebHostSynch;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event SetPreferencesCompletedEventHandler SetPreferencesCompleted;
        
        /// <remarks/>
        public event GetSheetFieldDataCompletedEventHandler GetSheetFieldDataCompleted;
        
        /// <remarks/>
        public event GetSheetDataCompletedEventHandler GetSheetDataCompleted;
        
        /// <remarks/>
        public event DeleteSheetDataCompletedEventHandler DeleteSheetDataCompleted;
        
        /// <remarks/>
        public event CheckRegistrationKeyCompletedEventHandler CheckRegistrationKeyCompleted;
        
        /// <remarks/>
        public event GetDentalOfficeIDCompletedEventHandler GetDentalOfficeIDCompleted;
        
        /// <remarks/>
        public event GetWebFormAddressCompletedEventHandler GetWebFormAddressCompleted;
        
        /// <remarks/>
        public event ReadSheetDefCompletedEventHandler ReadSheetDefCompleted;
        
        /// <remarks/>
        public event SheetsInternalMethCompletedEventHandler SheetsInternalMethCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://opendental.com/SetPreferences", RequestNamespace="http://opendental.com/", ResponseNamespace="http://opendental.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool SetPreferences(string RegistrationKey, int ColorBorder, string Heading1, string Heading2) {
            object[] results = this.Invoke("SetPreferences", new object[] {
                        RegistrationKey,
                        ColorBorder,
                        Heading1,
                        Heading2});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void SetPreferencesAsync(string RegistrationKey, int ColorBorder, string Heading1, string Heading2) {
            this.SetPreferencesAsync(RegistrationKey, ColorBorder, Heading1, Heading2, null);
        }
        
        /// <remarks/>
        public void SetPreferencesAsync(string RegistrationKey, int ColorBorder, string Heading1, string Heading2, object userState) {
            if ((this.SetPreferencesOperationCompleted == null)) {
                this.SetPreferencesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSetPreferencesOperationCompleted);
            }
            this.InvokeAsync("SetPreferences", new object[] {
                        RegistrationKey,
                        ColorBorder,
                        Heading1,
                        Heading2}, this.SetPreferencesOperationCompleted, userState);
        }
        
        private void OnSetPreferencesOperationCompleted(object arg) {
            if ((this.SetPreferencesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SetPreferencesCompleted(this, new SetPreferencesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://opendental.com/GetSheetFieldData", RequestNamespace="http://opendental.com/", ResponseNamespace="http://opendental.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public webforms_sheetfield[] GetSheetFieldData(string RegistrationKey) {
            object[] results = this.Invoke("GetSheetFieldData", new object[] {
                        RegistrationKey});
            return ((webforms_sheetfield[])(results[0]));
        }
        
        /// <remarks/>
        public void GetSheetFieldDataAsync(string RegistrationKey) {
            this.GetSheetFieldDataAsync(RegistrationKey, null);
        }
        
        /// <remarks/>
        public void GetSheetFieldDataAsync(string RegistrationKey, object userState) {
            if ((this.GetSheetFieldDataOperationCompleted == null)) {
                this.GetSheetFieldDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetSheetFieldDataOperationCompleted);
            }
            this.InvokeAsync("GetSheetFieldData", new object[] {
                        RegistrationKey}, this.GetSheetFieldDataOperationCompleted, userState);
        }
        
        private void OnGetSheetFieldDataOperationCompleted(object arg) {
            if ((this.GetSheetFieldDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetSheetFieldDataCompleted(this, new GetSheetFieldDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://opendental.com/GetSheetData", RequestNamespace="http://opendental.com/", ResponseNamespace="http://opendental.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public webforms_sheet[] GetSheetData(string RegistrationKey) {
            object[] results = this.Invoke("GetSheetData", new object[] {
                        RegistrationKey});
            return ((webforms_sheet[])(results[0]));
        }
        
        /// <remarks/>
        public void GetSheetDataAsync(string RegistrationKey) {
            this.GetSheetDataAsync(RegistrationKey, null);
        }
        
        /// <remarks/>
        public void GetSheetDataAsync(string RegistrationKey, object userState) {
            if ((this.GetSheetDataOperationCompleted == null)) {
                this.GetSheetDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetSheetDataOperationCompleted);
            }
            this.InvokeAsync("GetSheetData", new object[] {
                        RegistrationKey}, this.GetSheetDataOperationCompleted, userState);
        }
        
        private void OnGetSheetDataOperationCompleted(object arg) {
            if ((this.GetSheetDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetSheetDataCompleted(this, new GetSheetDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://opendental.com/DeleteSheetData", RequestNamespace="http://opendental.com/", ResponseNamespace="http://opendental.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void DeleteSheetData(string RegistrationKey, long[] SheetsForDeletion) {
            this.Invoke("DeleteSheetData", new object[] {
                        RegistrationKey,
                        SheetsForDeletion});
        }
        
        /// <remarks/>
        public void DeleteSheetDataAsync(string RegistrationKey, long[] SheetsForDeletion) {
            this.DeleteSheetDataAsync(RegistrationKey, SheetsForDeletion, null);
        }
        
        /// <remarks/>
        public void DeleteSheetDataAsync(string RegistrationKey, long[] SheetsForDeletion, object userState) {
            if ((this.DeleteSheetDataOperationCompleted == null)) {
                this.DeleteSheetDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteSheetDataOperationCompleted);
            }
            this.InvokeAsync("DeleteSheetData", new object[] {
                        RegistrationKey,
                        SheetsForDeletion}, this.DeleteSheetDataOperationCompleted, userState);
        }
        
        private void OnDeleteSheetDataOperationCompleted(object arg) {
            if ((this.DeleteSheetDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteSheetDataCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://opendental.com/CheckRegistrationKey", RequestNamespace="http://opendental.com/", ResponseNamespace="http://opendental.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool CheckRegistrationKey(string RegistrationKeyFromDentalOffice) {
            object[] results = this.Invoke("CheckRegistrationKey", new object[] {
                        RegistrationKeyFromDentalOffice});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void CheckRegistrationKeyAsync(string RegistrationKeyFromDentalOffice) {
            this.CheckRegistrationKeyAsync(RegistrationKeyFromDentalOffice, null);
        }
        
        /// <remarks/>
        public void CheckRegistrationKeyAsync(string RegistrationKeyFromDentalOffice, object userState) {
            if ((this.CheckRegistrationKeyOperationCompleted == null)) {
                this.CheckRegistrationKeyOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckRegistrationKeyOperationCompleted);
            }
            this.InvokeAsync("CheckRegistrationKey", new object[] {
                        RegistrationKeyFromDentalOffice}, this.CheckRegistrationKeyOperationCompleted, userState);
        }
        
        private void OnCheckRegistrationKeyOperationCompleted(object arg) {
            if ((this.CheckRegistrationKeyCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckRegistrationKeyCompleted(this, new CheckRegistrationKeyCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://opendental.com/GetDentalOfficeID", RequestNamespace="http://opendental.com/", ResponseNamespace="http://opendental.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public long GetDentalOfficeID(string RegistrationKeyFromDentalOffice) {
            object[] results = this.Invoke("GetDentalOfficeID", new object[] {
                        RegistrationKeyFromDentalOffice});
            return ((long)(results[0]));
        }
        
        /// <remarks/>
        public void GetDentalOfficeIDAsync(string RegistrationKeyFromDentalOffice) {
            this.GetDentalOfficeIDAsync(RegistrationKeyFromDentalOffice, null);
        }
        
        /// <remarks/>
        public void GetDentalOfficeIDAsync(string RegistrationKeyFromDentalOffice, object userState) {
            if ((this.GetDentalOfficeIDOperationCompleted == null)) {
                this.GetDentalOfficeIDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetDentalOfficeIDOperationCompleted);
            }
            this.InvokeAsync("GetDentalOfficeID", new object[] {
                        RegistrationKeyFromDentalOffice}, this.GetDentalOfficeIDOperationCompleted, userState);
        }
        
        private void OnGetDentalOfficeIDOperationCompleted(object arg) {
            if ((this.GetDentalOfficeIDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetDentalOfficeIDCompleted(this, new GetDentalOfficeIDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://opendental.com/GetWebFormAddress", RequestNamespace="http://opendental.com/", ResponseNamespace="http://opendental.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetWebFormAddress(string RegistrationKeyFromDentalOffice) {
            object[] results = this.Invoke("GetWebFormAddress", new object[] {
                        RegistrationKeyFromDentalOffice});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetWebFormAddressAsync(string RegistrationKeyFromDentalOffice) {
            this.GetWebFormAddressAsync(RegistrationKeyFromDentalOffice, null);
        }
        
        /// <remarks/>
        public void GetWebFormAddressAsync(string RegistrationKeyFromDentalOffice, object userState) {
            if ((this.GetWebFormAddressOperationCompleted == null)) {
                this.GetWebFormAddressOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetWebFormAddressOperationCompleted);
            }
            this.InvokeAsync("GetWebFormAddress", new object[] {
                        RegistrationKeyFromDentalOffice}, this.GetWebFormAddressOperationCompleted, userState);
        }
        
        private void OnGetWebFormAddressOperationCompleted(object arg) {
            if ((this.GetWebFormAddressCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetWebFormAddressCompleted(this, new GetWebFormAddressCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://opendental.com/ReadSheetDef", RequestNamespace="http://opendental.com/", ResponseNamespace="http://opendental.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void ReadSheetDef(SheetDef sheetDef) {
            this.Invoke("ReadSheetDef", new object[] {
                        sheetDef});
        }
        
        /// <remarks/>
        public void ReadSheetDefAsync(SheetDef sheetDef) {
            this.ReadSheetDefAsync(sheetDef, null);
        }
        
        /// <remarks/>
        public void ReadSheetDefAsync(SheetDef sheetDef, object userState) {
            if ((this.ReadSheetDefOperationCompleted == null)) {
                this.ReadSheetDefOperationCompleted = new System.Threading.SendOrPostCallback(this.OnReadSheetDefOperationCompleted);
            }
            this.InvokeAsync("ReadSheetDef", new object[] {
                        sheetDef}, this.ReadSheetDefOperationCompleted, userState);
        }
        
        private void OnReadSheetDefOperationCompleted(object arg) {
            if ((this.ReadSheetDefCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ReadSheetDefCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://opendental.com/SheetsInternalMeth", RequestNamespace="http://opendental.com/", ResponseNamespace="http://opendental.com/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void SheetsInternalMeth(SheetsInternal sheetInt) {
            this.Invoke("SheetsInternalMeth", new object[] {
                        sheetInt});
        }
        
        /// <remarks/>
        public void SheetsInternalMethAsync(SheetsInternal sheetInt) {
            this.SheetsInternalMethAsync(sheetInt, null);
        }
        
        /// <remarks/>
        public void SheetsInternalMethAsync(SheetsInternal sheetInt, object userState) {
            if ((this.SheetsInternalMethOperationCompleted == null)) {
                this.SheetsInternalMethOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSheetsInternalMethOperationCompleted);
            }
            this.InvokeAsync("SheetsInternalMeth", new object[] {
                        sheetInt}, this.SheetsInternalMethOperationCompleted, userState);
        }
        
        private void OnSheetsInternalMethOperationCompleted(object arg) {
            if ((this.SheetsInternalMethCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SheetsInternalMethCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3053")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://opendental.com/")]
    public partial class webforms_sheetfield : EntityObject {
        
        private string fieldNameField;
        
        private string fieldValueField;
        
        private long sheetFieldIDField;
        
        private EntityReferenceOfwebforms_sheet webforms_sheetReferenceField;
        
        /// <remarks/>
        public string FieldName {
            get {
                return this.fieldNameField;
            }
            set {
                this.fieldNameField = value;
            }
        }
        
        /// <remarks/>
        public string FieldValue {
            get {
                return this.fieldValueField;
            }
            set {
                this.fieldValueField = value;
            }
        }
        
        /// <remarks/>
        public long SheetFieldID {
            get {
                return this.sheetFieldIDField;
            }
            set {
                this.sheetFieldIDField = value;
            }
        }
        
        /// <remarks/>
        public EntityReferenceOfwebforms_sheet webforms_sheetReference {
            get {
                return this.webforms_sheetReferenceField;
            }
            set {
                this.webforms_sheetReferenceField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3053")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://opendental.com/")]
    public partial class EntityReferenceOfwebforms_sheet : EntityReference {
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EntityReferenceOfwebforms_preference))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EntityReferenceOfwebforms_sheet))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3053")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://opendental.com/")]
    public abstract partial class EntityReference : RelatedEnd {
        
        private EntityKey entityKeyField;
        
        /// <remarks/>
        public EntityKey EntityKey {
            get {
                return this.entityKeyField;
            }
            set {
                this.entityKeyField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3053")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://opendental.com/")]
    public partial class EntityKey {
        
        private string entitySetNameField;
        
        private string entityContainerNameField;
        
        private EntityKeyMember[] entityKeyValuesField;
        
        /// <remarks/>
        public string EntitySetName {
            get {
                return this.entitySetNameField;
            }
            set {
                this.entitySetNameField = value;
            }
        }
        
        /// <remarks/>
        public string EntityContainerName {
            get {
                return this.entityContainerNameField;
            }
            set {
                this.entityContainerNameField = value;
            }
        }
        
        /// <remarks/>
        public EntityKeyMember[] EntityKeyValues {
            get {
                return this.entityKeyValuesField;
            }
            set {
                this.entityKeyValuesField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3053")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://opendental.com/")]
    public partial class EntityKeyMember {
        
        private string keyField;
        
        private object valueField;
        
        /// <remarks/>
        public string Key {
            get {
                return this.keyField;
            }
            set {
                this.keyField = value;
            }
        }
        
        /// <remarks/>
        public object Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3053")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://opendental.com/")]
    public partial class SheetsInternal {
    }
    
        
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EntityReference))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EntityReferenceOfwebforms_preference))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EntityReferenceOfwebforms_sheet))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3053")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://opendental.com/")]
    public abstract partial class RelatedEnd {
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(EntityObject))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(webforms_sheet))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(webforms_sheetfield))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3053")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://opendental.com/")]
    public abstract partial class StructuralObject {
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(webforms_sheet))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(webforms_sheetfield))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3053")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://opendental.com/")]
    public abstract partial class EntityObject : StructuralObject {
        
        private EntityKey entityKeyField;
        
        /// <remarks/>
        public EntityKey EntityKey {
            get {
                return this.entityKeyField;
            }
            set {
                this.entityKeyField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3053")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://opendental.com/")]
    public partial class webforms_sheet : EntityObject {
        
        private System.DateTime dateTimeSubmittedField;
        
        private long sheetIDField;
        
        private EntityReferenceOfwebforms_preference webforms_preferenceReferenceField;
        
        /// <remarks/>
        public System.DateTime DateTimeSubmitted {
            get {
                return this.dateTimeSubmittedField;
            }
            set {
                this.dateTimeSubmittedField = value;
            }
        }
        
        /// <remarks/>
        public long SheetID {
            get {
                return this.sheetIDField;
            }
            set {
                this.sheetIDField = value;
            }
        }
        
        /// <remarks/>
        public EntityReferenceOfwebforms_preference webforms_preferenceReference {
            get {
                return this.webforms_preferenceReferenceField;
            }
            set {
                this.webforms_preferenceReferenceField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3053")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://opendental.com/")]
    public partial class EntityReferenceOfwebforms_preference : EntityReference {
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void SetPreferencesCompletedEventHandler(object sender, SetPreferencesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SetPreferencesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SetPreferencesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetSheetFieldDataCompletedEventHandler(object sender, GetSheetFieldDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetSheetFieldDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetSheetFieldDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public webforms_sheetfield[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((webforms_sheetfield[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetSheetDataCompletedEventHandler(object sender, GetSheetDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetSheetDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetSheetDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public webforms_sheet[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((webforms_sheet[])(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void DeleteSheetDataCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void CheckRegistrationKeyCompletedEventHandler(object sender, CheckRegistrationKeyCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CheckRegistrationKeyCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CheckRegistrationKeyCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetDentalOfficeIDCompletedEventHandler(object sender, GetDentalOfficeIDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetDentalOfficeIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetDentalOfficeIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public long Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((long)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void GetWebFormAddressCompletedEventHandler(object sender, GetWebFormAddressCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetWebFormAddressCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetWebFormAddressCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void ReadSheetDefCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.3053")]
    public delegate void SheetsInternalMethCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591