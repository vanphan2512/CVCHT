﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace WebClientAuthen.SSOService.TokenAuthentication {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1098.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="TokenAuthenticationSoap", Namespace="http://tempuri.org/")]
    public partial class TokenAuthentication : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback HandShakeOperationCompleted;
        
        private System.Threading.SendOrPostCallback LoginOperationCompleted;
        
        private System.Threading.SendOrPostCallback LoginSysOperationCompleted;
        
        private System.Threading.SendOrPostCallback LogOutOperationCompleted;
        
        private System.Threading.SendOrPostCallback UpdateESignDataOperationCompleted;
        
        private System.Threading.SendOrPostCallback ChangePasswordOperationCompleted;
        
        private System.Threading.SendOrPostCallback TestUseServiceOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public TokenAuthentication() {
            this.Url = global::WebClientAuthen.Properties.Settings.Default.WebClientAuthen_SSOService_TokenAuthentication_TokenAuthentication;
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
        public event HandShakeCompletedEventHandler HandShakeCompleted;
        
        /// <remarks/>
        public event LoginCompletedEventHandler LoginCompleted;
        
        /// <remarks/>
        public event LoginSysCompletedEventHandler LoginSysCompleted;
        
        /// <remarks/>
        public event LogOutCompletedEventHandler LogOutCompleted;
        
        /// <remarks/>
        public event UpdateESignDataCompletedEventHandler UpdateESignDataCompleted;
        
        /// <remarks/>
        public event ChangePasswordCompletedEventHandler ChangePasswordCompleted;
        
        /// <remarks/>
        public event TestUseServiceCompletedEventHandler TestUseServiceCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/HandShake", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string HandShake(string strClientResponse) {
            object[] results = this.Invoke("HandShake", new object[] {
                        strClientResponse});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void HandShakeAsync(string strClientResponse) {
            this.HandShakeAsync(strClientResponse, null);
        }
        
        /// <remarks/>
        public void HandShakeAsync(string strClientResponse, object userState) {
            if ((this.HandShakeOperationCompleted == null)) {
                this.HandShakeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnHandShakeOperationCompleted);
            }
            this.InvokeAsync("HandShake", new object[] {
                        strClientResponse}, this.HandShakeOperationCompleted, userState);
        }
        
        private void OnHandShakeOperationCompleted(object arg) {
            if ((this.HandShakeCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.HandShakeCompleted(this, new HandShakeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Login", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public AuthenticateResult Login(string strData, string strGUID) {
            object[] results = this.Invoke("Login", new object[] {
                        strData,
                        strGUID});
            return ((AuthenticateResult)(results[0]));
        }
        
        /// <remarks/>
        public void LoginAsync(string strData, string strGUID) {
            this.LoginAsync(strData, strGUID, null);
        }
        
        /// <remarks/>
        public void LoginAsync(string strData, string strGUID, object userState) {
            if ((this.LoginOperationCompleted == null)) {
                this.LoginOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLoginOperationCompleted);
            }
            this.InvokeAsync("Login", new object[] {
                        strData,
                        strGUID}, this.LoginOperationCompleted, userState);
        }
        
        private void OnLoginOperationCompleted(object arg) {
            if ((this.LoginCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LoginCompleted(this, new LoginCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Dang_nhap_he_thong", RequestElementName="Dang_nhap_he_thong", RequestNamespace="http://tempuri.org/", ResponseElementName="Dang_nhap_he_thongResponse", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        [return: System.Xml.Serialization.XmlElementAttribute("Dang_nhap_he_thongResult")]
        public AuthenticateResult LoginSys(string strData, string strGUID, ref ResultMessage objResultMessage) {
            object[] results = this.Invoke("LoginSys", new object[] {
                        strData,
                        strGUID,
                        objResultMessage});
            objResultMessage = ((ResultMessage)(results[1]));
            return ((AuthenticateResult)(results[0]));
        }
        
        /// <remarks/>
        public void LoginSysAsync(string strData, string strGUID, ResultMessage objResultMessage) {
            this.LoginSysAsync(strData, strGUID, objResultMessage, null);
        }
        
        /// <remarks/>
        public void LoginSysAsync(string strData, string strGUID, ResultMessage objResultMessage, object userState) {
            if ((this.LoginSysOperationCompleted == null)) {
                this.LoginSysOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLoginSysOperationCompleted);
            }
            this.InvokeAsync("LoginSys", new object[] {
                        strData,
                        strGUID,
                        objResultMessage}, this.LoginSysOperationCompleted, userState);
        }
        
        private void OnLoginSysOperationCompleted(object arg) {
            if ((this.LoginSysCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LoginSysCompleted(this, new LoginSysCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/LogOut", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool LogOut(string strAuthenData, string strGUID) {
            object[] results = this.Invoke("LogOut", new object[] {
                        strAuthenData,
                        strGUID});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void LogOutAsync(string strAuthenData, string strGUID) {
            this.LogOutAsync(strAuthenData, strGUID, null);
        }
        
        /// <remarks/>
        public void LogOutAsync(string strAuthenData, string strGUID, object userState) {
            if ((this.LogOutOperationCompleted == null)) {
                this.LogOutOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLogOutOperationCompleted);
            }
            this.InvokeAsync("LogOut", new object[] {
                        strAuthenData,
                        strGUID}, this.LogOutOperationCompleted, userState);
        }
        
        private void OnLogOutOperationCompleted(object arg) {
            if ((this.LogOutCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LogOutCompleted(this, new LogOutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/UpdateESignData", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool UpdateESignData(string strAuthenData, string strGUID, string strESignData, ref ResultMessage objResultMessage) {
            object[] results = this.Invoke("UpdateESignData", new object[] {
                        strAuthenData,
                        strGUID,
                        strESignData,
                        objResultMessage});
            objResultMessage = ((ResultMessage)(results[1]));
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void UpdateESignDataAsync(string strAuthenData, string strGUID, string strESignData, ResultMessage objResultMessage) {
            this.UpdateESignDataAsync(strAuthenData, strGUID, strESignData, objResultMessage, null);
        }
        
        /// <remarks/>
        public void UpdateESignDataAsync(string strAuthenData, string strGUID, string strESignData, ResultMessage objResultMessage, object userState) {
            if ((this.UpdateESignDataOperationCompleted == null)) {
                this.UpdateESignDataOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateESignDataOperationCompleted);
            }
            this.InvokeAsync("UpdateESignData", new object[] {
                        strAuthenData,
                        strGUID,
                        strESignData,
                        objResultMessage}, this.UpdateESignDataOperationCompleted, userState);
        }
        
        private void OnUpdateESignDataOperationCompleted(object arg) {
            if ((this.UpdateESignDataCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateESignDataCompleted(this, new UpdateESignDataCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ChangePassword", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public ResultMessage ChangePassword(string strAuthenData, string strGUID, string strOldPassword, string strNewPassword, ref int intResult) {
            object[] results = this.Invoke("ChangePassword", new object[] {
                        strAuthenData,
                        strGUID,
                        strOldPassword,
                        strNewPassword,
                        intResult});
            intResult = ((int)(results[1]));
            return ((ResultMessage)(results[0]));
        }
        
        /// <remarks/>
        public void ChangePasswordAsync(string strAuthenData, string strGUID, string strOldPassword, string strNewPassword, int intResult) {
            this.ChangePasswordAsync(strAuthenData, strGUID, strOldPassword, strNewPassword, intResult, null);
        }
        
        /// <remarks/>
        public void ChangePasswordAsync(string strAuthenData, string strGUID, string strOldPassword, string strNewPassword, int intResult, object userState) {
            if ((this.ChangePasswordOperationCompleted == null)) {
                this.ChangePasswordOperationCompleted = new System.Threading.SendOrPostCallback(this.OnChangePasswordOperationCompleted);
            }
            this.InvokeAsync("ChangePassword", new object[] {
                        strAuthenData,
                        strGUID,
                        strOldPassword,
                        strNewPassword,
                        intResult}, this.ChangePasswordOperationCompleted, userState);
        }
        
        private void OnChangePasswordOperationCompleted(object arg) {
            if ((this.ChangePasswordCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ChangePasswordCompleted(this, new ChangePasswordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/TestUseService", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string TestUseService(string strData, string strAuthenData, string strGUID) {
            object[] results = this.Invoke("TestUseService", new object[] {
                        strData,
                        strAuthenData,
                        strGUID});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void TestUseServiceAsync(string strData, string strAuthenData, string strGUID) {
            this.TestUseServiceAsync(strData, strAuthenData, strGUID, null);
        }
        
        /// <remarks/>
        public void TestUseServiceAsync(string strData, string strAuthenData, string strGUID, object userState) {
            if ((this.TestUseServiceOperationCompleted == null)) {
                this.TestUseServiceOperationCompleted = new System.Threading.SendOrPostCallback(this.OnTestUseServiceOperationCompleted);
            }
            this.InvokeAsync("TestUseService", new object[] {
                        strData,
                        strAuthenData,
                        strGUID}, this.TestUseServiceOperationCompleted, userState);
        }
        
        private void OnTestUseServiceOperationCompleted(object arg) {
            if ((this.TestUseServiceCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.TestUseServiceCompleted(this, new TestUseServiceCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1098.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class AuthenticateResult {
        
        private bool isErrorField;
        
        private string errorMessageField;
        
        private string tokenStringField;
        
        private string userNameField;
        
        private string fullNameField;
        
        private string functionField;
        
        private string departmentField;
        
        private int departmentIDField;
        
        private string addressField;
        
        private int functionIDField;
        
        private string phoneNumberField;
        
        private int reviewLevelIDField;
        
        private string loginLogIDField;
        
        private int securityLevelIDField;
        
        private int positionIDField;
        
        private string positionNameField;
        
        private string defaultPictureURLField;
        
        private bool isUseETokenDeviceField;
        
        private string eTokenDeviceEmailField;
        
        private bool isCheckVersionField;
        
        private bool isUpdateVersionByFTPField;
        
        /// <remarks/>
        public bool IsError {
            get {
                return this.isErrorField;
            }
            set {
                this.isErrorField = value;
            }
        }
        
        /// <remarks/>
        public string ErrorMessage {
            get {
                return this.errorMessageField;
            }
            set {
                this.errorMessageField = value;
            }
        }
        
        /// <remarks/>
        public string TokenString {
            get {
                return this.tokenStringField;
            }
            set {
                this.tokenStringField = value;
            }
        }
        
        /// <remarks/>
        public string UserName {
            get {
                return this.userNameField;
            }
            set {
                this.userNameField = value;
            }
        }
        
        /// <remarks/>
        public string FullName {
            get {
                return this.fullNameField;
            }
            set {
                this.fullNameField = value;
            }
        }
        
        /// <remarks/>
        public string Function {
            get {
                return this.functionField;
            }
            set {
                this.functionField = value;
            }
        }
        
        /// <remarks/>
        public string Department {
            get {
                return this.departmentField;
            }
            set {
                this.departmentField = value;
            }
        }
        
        /// <remarks/>
        public int DepartmentID {
            get {
                return this.departmentIDField;
            }
            set {
                this.departmentIDField = value;
            }
        }
        
        /// <remarks/>
        public string Address {
            get {
                return this.addressField;
            }
            set {
                this.addressField = value;
            }
        }
        
        /// <remarks/>
        public int FunctionID {
            get {
                return this.functionIDField;
            }
            set {
                this.functionIDField = value;
            }
        }
        
        /// <remarks/>
        public string PhoneNumber {
            get {
                return this.phoneNumberField;
            }
            set {
                this.phoneNumberField = value;
            }
        }
        
        /// <remarks/>
        public int ReviewLevelID {
            get {
                return this.reviewLevelIDField;
            }
            set {
                this.reviewLevelIDField = value;
            }
        }
        
        /// <remarks/>
        public string LoginLogID {
            get {
                return this.loginLogIDField;
            }
            set {
                this.loginLogIDField = value;
            }
        }
        
        /// <remarks/>
        public int SecurityLevelID {
            get {
                return this.securityLevelIDField;
            }
            set {
                this.securityLevelIDField = value;
            }
        }
        
        /// <remarks/>
        public int PositionID {
            get {
                return this.positionIDField;
            }
            set {
                this.positionIDField = value;
            }
        }
        
        /// <remarks/>
        public string PositionName {
            get {
                return this.positionNameField;
            }
            set {
                this.positionNameField = value;
            }
        }
        
        /// <remarks/>
        public string DefaultPictureURL {
            get {
                return this.defaultPictureURLField;
            }
            set {
                this.defaultPictureURLField = value;
            }
        }
        
        /// <remarks/>
        public bool IsUseETokenDevice {
            get {
                return this.isUseETokenDeviceField;
            }
            set {
                this.isUseETokenDeviceField = value;
            }
        }
        
        /// <remarks/>
        public string ETokenDeviceEmail {
            get {
                return this.eTokenDeviceEmailField;
            }
            set {
                this.eTokenDeviceEmailField = value;
            }
        }
        
        /// <remarks/>
        public bool IsCheckVersion {
            get {
                return this.isCheckVersionField;
            }
            set {
                this.isCheckVersionField = value;
            }
        }
        
        /// <remarks/>
        public bool IsUpdateVersionByFTP {
            get {
                return this.isUpdateVersionByFTPField;
            }
            set {
                this.isUpdateVersionByFTPField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1098.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public partial class ResultMessage {
        
        private bool isErrorField;
        
        private ErrorTypes errorTypeField;
        
        private string messageField;
        
        private string messageDetailField;
        
        /// <remarks/>
        public bool IsError {
            get {
                return this.isErrorField;
            }
            set {
                this.isErrorField = value;
            }
        }
        
        /// <remarks/>
        public ErrorTypes ErrorType {
            get {
                return this.errorTypeField;
            }
            set {
                this.errorTypeField = value;
            }
        }
        
        /// <remarks/>
        public string Message {
            get {
                return this.messageField;
            }
            set {
                this.messageField = value;
            }
        }
        
        /// <remarks/>
        public string MessageDetail {
            get {
                return this.messageDetailField;
            }
            set {
                this.messageDetailField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.6.1098.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    public enum ErrorTypes {
        
        /// <remarks/>
        No_Error,
        
        /// <remarks/>
        LoadInfo,
        
        /// <remarks/>
        Insert,
        
        /// <remarks/>
        Update,
        
        /// <remarks/>
        Delete,
        
        /// <remarks/>
        SearchData,
        
        /// <remarks/>
        GetData,
        
        /// <remarks/>
        InvalidIV,
        
        /// <remarks/>
        TokenNotExist,
        
        /// <remarks/>
        TokenInvalid,
        
        /// <remarks/>
        CheckData,
        
        /// <remarks/>
        Others,
        
        /// <remarks/>
        UserDefine,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1098.0")]
    public delegate void HandShakeCompletedEventHandler(object sender, HandShakeCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1098.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class HandShakeCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal HandShakeCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1098.0")]
    public delegate void LoginCompletedEventHandler(object sender, LoginCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1098.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LoginCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LoginCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public AuthenticateResult Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((AuthenticateResult)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1098.0")]
    public delegate void LoginSysCompletedEventHandler(object sender, LoginSysCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1098.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LoginSysCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LoginSysCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public AuthenticateResult Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((AuthenticateResult)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public ResultMessage objResultMessage {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ResultMessage)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1098.0")]
    public delegate void LogOutCompletedEventHandler(object sender, LogOutCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1098.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LogOutCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LogOutCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1098.0")]
    public delegate void UpdateESignDataCompletedEventHandler(object sender, UpdateESignDataCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1098.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class UpdateESignDataCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal UpdateESignDataCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
        
        /// <remarks/>
        public ResultMessage objResultMessage {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ResultMessage)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1098.0")]
    public delegate void ChangePasswordCompletedEventHandler(object sender, ChangePasswordCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1098.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ChangePasswordCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ChangePasswordCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public ResultMessage Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((ResultMessage)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public int intResult {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1098.0")]
    public delegate void TestUseServiceCompletedEventHandler(object sender, TestUseServiceCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.6.1098.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class TestUseServiceCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal TestUseServiceCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
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
}

#pragma warning restore 1591