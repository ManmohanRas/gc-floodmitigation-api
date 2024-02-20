//---------------------------------------------- System Namespaces ----------------------------------------------//
//===============================================================================================================//
global using System;
global using System.Collections.Generic;
global using System.Data;
global using System.Globalization;
global using System.IO;
global using System.Linq;
global using System.Net;
global using System.Net.Http.Headers;
global using System.Text;
global using System.Threading.Tasks;

global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.Options;

//---------------------------------------------- Package Namespaces ----------------------------------------------//
//================================================================================================================//
global using AutoMapper;
global using FluentValidation;
global using MediatR;
global using Newtonsoft.Json;
global using Polly;

//---------------------------------------------- PresTrust.FloodMitigation.API Namespaces ----------------------------------------------//
//======================================================================================================================================//

//---------------------------------------------- PresTrust.FloodMitigation.Application Namespaces ----------------------------------------------//
//==============================================================================================================================================//
global using PresTrust.FloodMitigation.Application.ApiExceptions;
global using PresTrust.FloodMitigation.Application.Http;
global using PresTrust.FloodMitigation.Application.Services.EmailApi;
global using PresTrust.FloodMitigation.Application.Services.IdentityApi;
global using PresTrust.FloodMitigation.Application.CommonViewModels;
global using Microsoft.Extensions.Caching.Memory;
global using System.Text.RegularExpressions;

//---------------------------------------------- PresTrust.FloodMitigation.Domain Namespaces ----------------------------------------------//
//=========================================================================================================================================//
global using PresTrust.FloodMitigation.Domain.Configurations;
global using static PresTrust.FloodMitigation.Domain.Constants.FloodMitigationDomainConstants;
global using PresTrust.FloodMitigation.Domain.Entities;
global using PresTrust.FloodMitigation.Domain.Enums;
global using PresTrust.FloodMitigation.Domain.Utils;

//---------------------------------------------- PresTrust.FloodMitigation.Infrastructure Namespaces ----------------------------------------------//
//=================================================================================================================================================//
global using PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;
global using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;

