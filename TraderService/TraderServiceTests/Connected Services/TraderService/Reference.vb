﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace TraderService
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0"),  _
     System.ServiceModel.ServiceContractAttribute(ConfigurationName:="TraderService.ITraderService")>  _
    Public Interface ITraderService
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ITraderService/GetInvestmentList", ReplyAction:="http://tempuri.org/ITraderService/GetInvestmentListResponse")>  _
        Function GetInvestmentList(ByVal userId As Long) As System.Data.DataSet
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ITraderService/GetInvestmentList", ReplyAction:="http://tempuri.org/ITraderService/GetInvestmentListResponse")>  _
        Function GetInvestmentListAsync(ByVal userId As Long) As System.Threading.Tasks.Task(Of System.Data.DataSet)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ITraderService/GetCostBasisPerShare", ReplyAction:="http://tempuri.org/ITraderService/GetCostBasisPerShareResponse")>  _
        Function GetCostBasisPerShare(ByVal userId As Long) As Decimal
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ITraderService/GetCostBasisPerShare", ReplyAction:="http://tempuri.org/ITraderService/GetCostBasisPerShareResponse")>  _
        Function GetCostBasisPerShareAsync(ByVal userId As Long) As System.Threading.Tasks.Task(Of Decimal)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ITraderService/GetCurrentValue", ReplyAction:="http://tempuri.org/ITraderService/GetCurrentValueResponse")>  _
        Function GetCurrentValue(ByVal userId As Long) As Decimal
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ITraderService/GetCurrentValue", ReplyAction:="http://tempuri.org/ITraderService/GetCurrentValueResponse")>  _
        Function GetCurrentValueAsync(ByVal userId As Long) As System.Threading.Tasks.Task(Of Decimal)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ITraderService/GetCurrentPrice", ReplyAction:="http://tempuri.org/ITraderService/GetCurrentPriceResponse")>  _
        Function GetCurrentPrice(ByVal userId As Long) As Decimal
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ITraderService/GetCurrentPrice", ReplyAction:="http://tempuri.org/ITraderService/GetCurrentPriceResponse")>  _
        Function GetCurrentPriceAsync(ByVal userId As Long) As System.Threading.Tasks.Task(Of Decimal)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ITraderService/GetInvestmentTerm", ReplyAction:="http://tempuri.org/ITraderService/GetInvestmentTermResponse")>  _
        Function GetInvestmentTerm(ByVal userId As Long) As Short
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ITraderService/GetInvestmentTerm", ReplyAction:="http://tempuri.org/ITraderService/GetInvestmentTermResponse")>  _
        Function GetInvestmentTermAsync(ByVal userId As Long) As System.Threading.Tasks.Task(Of Short)
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ITraderService/GetGainLoss", ReplyAction:="http://tempuri.org/ITraderService/GetGainLossResponse")>  _
        Function GetGainLoss(ByVal userId As Long) As Decimal
        
        <System.ServiceModel.OperationContractAttribute(Action:="http://tempuri.org/ITraderService/GetGainLoss", ReplyAction:="http://tempuri.org/ITraderService/GetGainLossResponse")>  _
        Function GetGainLossAsync(ByVal userId As Long) As System.Threading.Tasks.Task(Of Decimal)
    End Interface
    
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Public Interface ITraderServiceChannel
        Inherits TraderService.ITraderService, System.ServiceModel.IClientChannel
    End Interface
    
    <System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")>  _
    Partial Public Class TraderServiceClient
        Inherits System.ServiceModel.ClientBase(Of TraderService.ITraderService)
        Implements TraderService.ITraderService
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String)
            MyBase.New(endpointConfigurationName)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(endpointConfigurationName, remoteAddress)
        End Sub
        
        Public Sub New(ByVal binding As System.ServiceModel.Channels.Binding, ByVal remoteAddress As System.ServiceModel.EndpointAddress)
            MyBase.New(binding, remoteAddress)
        End Sub
        
        Public Function GetInvestmentList(ByVal userId As Long) As System.Data.DataSet Implements TraderService.ITraderService.GetInvestmentList
            Return MyBase.Channel.GetInvestmentList(userId)
        End Function
        
        Public Function GetInvestmentListAsync(ByVal userId As Long) As System.Threading.Tasks.Task(Of System.Data.DataSet) Implements TraderService.ITraderService.GetInvestmentListAsync
            Return MyBase.Channel.GetInvestmentListAsync(userId)
        End Function
        
        Public Function GetCostBasisPerShare(ByVal userId As Long) As Decimal Implements TraderService.ITraderService.GetCostBasisPerShare
            Return MyBase.Channel.GetCostBasisPerShare(userId)
        End Function
        
        Public Function GetCostBasisPerShareAsync(ByVal userId As Long) As System.Threading.Tasks.Task(Of Decimal) Implements TraderService.ITraderService.GetCostBasisPerShareAsync
            Return MyBase.Channel.GetCostBasisPerShareAsync(userId)
        End Function
        
        Public Function GetCurrentValue(ByVal userId As Long) As Decimal Implements TraderService.ITraderService.GetCurrentValue
            Return MyBase.Channel.GetCurrentValue(userId)
        End Function
        
        Public Function GetCurrentValueAsync(ByVal userId As Long) As System.Threading.Tasks.Task(Of Decimal) Implements TraderService.ITraderService.GetCurrentValueAsync
            Return MyBase.Channel.GetCurrentValueAsync(userId)
        End Function
        
        Public Function GetCurrentPrice(ByVal userId As Long) As Decimal Implements TraderService.ITraderService.GetCurrentPrice
            Return MyBase.Channel.GetCurrentPrice(userId)
        End Function
        
        Public Function GetCurrentPriceAsync(ByVal userId As Long) As System.Threading.Tasks.Task(Of Decimal) Implements TraderService.ITraderService.GetCurrentPriceAsync
            Return MyBase.Channel.GetCurrentPriceAsync(userId)
        End Function
        
        Public Function GetInvestmentTerm(ByVal userId As Long) As Short Implements TraderService.ITraderService.GetInvestmentTerm
            Return MyBase.Channel.GetInvestmentTerm(userId)
        End Function
        
        Public Function GetInvestmentTermAsync(ByVal userId As Long) As System.Threading.Tasks.Task(Of Short) Implements TraderService.ITraderService.GetInvestmentTermAsync
            Return MyBase.Channel.GetInvestmentTermAsync(userId)
        End Function
        
        Public Function GetGainLoss(ByVal userId As Long) As Decimal Implements TraderService.ITraderService.GetGainLoss
            Return MyBase.Channel.GetGainLoss(userId)
        End Function
        
        Public Function GetGainLossAsync(ByVal userId As Long) As System.Threading.Tasks.Task(Of Decimal) Implements TraderService.ITraderService.GetGainLossAsync
            Return MyBase.Channel.GetGainLossAsync(userId)
        End Function
    End Class
End Namespace
