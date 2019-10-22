        protected virtual void AddDocumentation(IServiceCollection services)
        {
            HostConfiguration configuration = services.ResolveConfiguration<HostConfiguration>();

            // Swashbuckle
            //// Register the Swagger generator, defining 1 or more Swagger documents
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("LithiumServiceDocumentation", new Info
            //    {
            //        Version = configuration.Information.Version,
            //        Title = configuration.Information.HostTitle,
            //        Description = configuration.Information.ProductName,
            //        TermsOfService = new Uri("https://pt.primaverabss.com/pt/site/termos-de-utilizacao/").ToString(),
            //        Contact = new Contact
            //        {
            //            Name = configuration.Information.Company,
            //            Url = new Uri("https://pt.primaverabss.com/pt/").ToString()
            //        },
            //        License = new License
            //        {
            //            Name = configuration.Information.Company + " " + configuration.Information.Copyright,
            //            Url = new Uri("https://pt.primaverabss.com/pt/").ToString()
            //        }
            //    });

            //    // Set the comments path for the Swagger JSON and UI.
            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    c.IncludeXmlComments(xmlPath);

            //    var security = new Dictionary<string, IEnumerable<string>>
            //    {
            //        {"Bearer", new string[] { }},
            //    };

            //    c.AddSecurityDefinition(
            //        "Bearer",
            //        new ApiKeyScheme
            //        {
            //            In = "header",
            //            Description = "Copie 'Bearer ' + token'",
            //            Name = "Authorization",
            //            Type = "apiKey"
            //        });

            //    c.AddSecurityRequirement(security);
            //});

            // NSwag
            // Register the Swagger services
            services.AddSwaggerDocument(config =>
            {
                config.FlattenInheritanceHierarchy = true;
                config.PostProcess = document =>
                {
                    document.Info.Version = configuration.Information.Version;
                    document.Info.Title = configuration.Information.HostTitle;
                    document.Info.Description = configuration.Information.ProductName;
                    document.Info.TermsOfService = new Uri("https://pt.primaverabss.com/pt/site/termos-de-utilizacao/").ToString();
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = configuration.Information.Company,
                        Email = string.Empty,
                        Url = new Uri("https://pt.primaverabss.com/pt/").ToString()
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = configuration.Information.Company + " " + configuration.Information.Copyright,
                        Url = new Uri("https://pt.primaverabss.com/pt/").ToString()
                    };
                };
                //config.DocumentProcessors.Add(new SecurityDefinitionAppender("JWT Token",
                //    new OpenApiSecurityScheme
                //    {
                //        Type = OpenApiSecuritySchemeType.ApiKey,
                //        Name = "Authorization",
                //        Description = "Copy 'Bearer ' + valid JWT token into field",
                //        In = OpenApiSecurityApiKeyLocation.Header
                // }));

                config.AddSecurity("bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    Description = "Lithium Sample API",
                    Flow = OpenApiOAuth2Flow.Implicit, //Application,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            Scopes = new Dictionary<string, string>
                                {
                                    { "lithium-sample", "Access lithium-sample API." }
                                },
                            AuthorizationUrl = $"{configuration.IdentityServerBaseUri.Trim()}/connect/authorize",
                            TokenUrl = $"{configuration.IdentityServerBaseUri.Trim()}/connect/token",
                            RefreshUrl = $"{configuration.IdentityServerBaseUri.Trim()}/connect/token"
                        },
                    }
                });

                config.OperationProcessors.Add(
                     new AspNetCoreOperationSecurityScopeProcessor("bearer"));
            });


        }

        protected virtual void UseDocumentation(IApplicationBuilder app)
        {
            // Swashbuckle
            //// Enable middleware to serve generated Swagger as a JSON endpoint.
            //app.UseSwagger();

            //// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            //// specifying the Swagger JSON endpoint.
            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/LithiumServiceDocumentation/swagger.json", "Lithium API");
            //    c.RoutePrefix = string.Empty;
            //});

            // NSwag
            // Register the Swagger generator and the Swagger UI middlewares
            // Add Swagger 2.0 document serving middleware
            app.UseOpenApi();
            app.UseSwaggerUi3(settings =>
            {
                settings.DocExpansion = "list";
                settings.OAuth2Client = new OAuth2ClientSettings
                {
                    ClientId = "lithium-sample-implicit",
                    ClientSecret = "A8vYHgFsy3DnASwBUGwtsgabYtEnUodj8uDdiy8wmPZV0XdU2YVDrtHrpVUEAOKh4PlkYfGgLjyf+1ioZh+DIw== "
                };

                settings.OAuth2Client.AdditionalQueryStringParameters.Add("redirect_uri", "http://localhost:4200");
            });
        }