/********************************************************************************
* Copyright (c) {2019 - 2024} Contributors to the Eclipse Foundation
*
* See the NOTICE file(s) distributed with this work for additional
* information regarding copyright ownership.
*
* This program and the accompanying materials are made available under the
* terms of the Apache License Version 2.0 which is available at
* https://www.apache.org/licenses/LICENSE-2.0
*
* SPDX-License-Identifier: Apache-2.0
********************************************************************************/

/*
 * DotAAS Part 2 | HTTP/REST | Asset Administration Shell Registry Service Specification
 *
 * The Full Profile of the Asset Administration Shell Registry Service Specification as part of the [Specification of the Asset Administration Shell: Part 2](http://industrialdigitaltwin.org/en/content-hub).   Publisher: Industrial Digital Twin Association (IDTA) 2023
 *
 * OpenAPI spec version: V3.0.1_SSP-001
 * Contact: info@idtwin.org
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using IO.Swagger.Registry.Lib.V3.Filters;
using System.Collections.Generic;
using Microsoft.OpenApi.Any;

namespace IO.Swagger;

using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Models;
using Registry.Lib.V3.Models;

/// <summary>
/// Startup
/// </summary>
public class Startup
{
    private readonly IWebHostEnvironment _hostingEnv;

    private IConfiguration Configuration { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="env"></param>
    /// <param name="configuration"></param>
    public Startup(IWebHostEnvironment env, IConfiguration configuration)
    {
        _hostingEnv   = env;
        Configuration = configuration;
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
        // Add framework services.
        services
            .AddMvc(options =>
                    {
                        // Remove System.Text.Json formatters
                        options.InputFormatters.RemoveType<SystemTextJsonInputFormatter>();
                        options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
                    })
            .AddJsonOptions(options =>
                            {
                                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                                options.JsonSerializerOptions.ReferenceHandler       = ReferenceHandler.Preserve;
                            })
            .AddXmlSerializerFormatters();

        services.Configure<JsonOptions>(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
                                                                       {
                                                                           options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                                                                           options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
                                                                       });

        services.Configure<JsonOptions>(options =>
                                        {
                                            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                                            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                                        });
        services
            .AddSwaggerGen(c =>
                           {
                               c.SwaggerDoc("V3.0.1_SSP-001",
                                            new OpenApiInfo
                                            {
                                                Version     = "V3.0.1_SSP-001",
                                                Title       = "DotAAS Part 2 | HTTP/REST | Asset Administration Shell Registry Service Specification",
                                                Description = "DotAAS Part 2 | HTTP/REST | Asset Administration Shell Registry Service Specification (ASP.NET Core 3.1)",
                                                Contact = new OpenApiContact()
                                                          {
                                                              Name  = "Industrial Digital Twin Association (IDTA)",
                                                              Url   = new Uri("https://github.com/swagger-api/swagger-codegen"),
                                                              Email = "info@idtwin.org"
                                                          },
                                                TermsOfService = new Uri("")
                                            });
                               c.CustomSchemaIds(type => type.FullName);
                               c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{_hostingEnv.ApplicationName}.xml");

                               // Include DataAnnotation attributes on Controller Action parameters as Swagger validation rules (e.g. required, pattern, ..)
                               // Use [ValidateModelState] on Actions to actually validate it in C# as well!
                               c.OperationFilter<GeneratePathParamsValidationFilter>();

                               c.MapType<MessageTypeEnum>(() => new OpenApiSchema
                                                                {
                                                                    Type = "string",
                                                                    Enum = Enum.GetNames(typeof(MessageTypeEnum))
                                                                               .Select(enumName => new OpenApiString(enumName))
                                                                               .Cast<IOpenApiAny>()
                                                                               .ToList()
                                                                });
                           });
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="loggerFactory"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        app.UseRouting();

        //TODO: Uncomment this if you need wwwroot folder
        // app.UseStaticFiles();

        app.UseAuthorization();

        app.UseSwagger();
        app.UseSwaggerUI(c =>
                         {
                             //TODO: Either use the SwaggerGen generated Swagger contract (generated from C# classes)
                             c.SwaggerEndpoint("/swagger/V3.0.1_SSP-001/swagger.json", "DotAAS Part 2 | HTTP/REST | Asset Administration Shell Registry Service Specification");

                             //TODO: Or alternatively use the original Swagger contract that's included in the static files
                             // c.SwaggerEndpoint("/swagger-original.json", "DotAAS Part 2 | HTTP/REST | Asset Administration Shell Registry Service Specification Original");
                         });

        //TODO: Use Https Redirection
        // app.UseHttpsRedirection();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            //TODO: Enable production exception handling (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/error-handling)
            app.UseExceptionHandler("/Error");

            app.UseHsts();
        }
    }
}