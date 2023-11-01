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
global using System.Reflection;
global using System.Text;
global using System.Threading.Tasks;

global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.OpenApi.Models;

//---------------------------------------------- Package Namespaces ----------------------------------------------//
//================================================================================================================//
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using FluentValidation.Results;
global using MediatR;
global using Serilog;
global using Serilog.Context;
global using Serilog.Events;
global using Serilog.Sinks.MSSqlServer;
global using Polly;
global using Polly.Extensions.Http;

//---------------------------------------------- PresTrust.FloodMitigation.API Namespaces ----------------------------------------------//
//======================================================================================================================================//
global using PresTrust.FloodMitigation.API.Contracts;
global using PresTrust.FloodMitigation.API.DependencyInjection;
global using PresTrust.FloodMitigation.API.Extensions;
global using PresTrust.FloodMitigation.API.Infrastructure.Behaviours;
global using PresTrust.FloodMitigation.API.Infrastructure.Filters;

//---------------------------------------------- PresTrust.FloodMitigation.Application Namespaces ----------------------------------------------//
//==============================================================================================================================================//
global using PresTrust.FloodMitigation.Application;
global using PresTrust.FloodMitigation.Application.ApiExceptions;
global using PresTrust.FloodMitigation.Application.Commands;
global using PresTrust.FloodMitigation.Application.Queries;
global using PresTrust.FloodMitigation.Application.Services.EmailApi;
global using PresTrust.FloodMitigation.Application.Services.IdentityApi;
global using PresTrust.FloodMitigation.Application.CommonViewModels;
global using PresTrust.FloodMitigation.Application.BackgroundJobs;

//---------------------------------------------- PresTrust.FloodMitigation.Domain Namespaces ----------------------------------------------//
//=========================================================================================================================================//
global using PresTrust.FloodMitigation.Domain.Configurations;
global using PresTrust.FloodMitigation.Domain.Constants;
global using static PresTrust.FloodMitigation.Domain.Constants.FloodMitigationDomainConstants;

//---------------------------------------------- PresTrust.FloodMitigation.Infrastructure Namespaces ----------------------------------------------//
//=================================================================================================================================================//
global using PresTrust.FloodMitigation.Infrastructure.SqlServerDb;
global using PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Contracts;
global using PresTrust.FloodMitigation.Infrastructure.SqlServerDb.Repositories;

