#if !NET5_0_OR_GREATER && !NETCOREAPP3_1_OR_GREATER
// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices;
// This is needed to support 'init' properties in .NET Standard 2.0. Records use init
public static class IsExternalInit;
#endif