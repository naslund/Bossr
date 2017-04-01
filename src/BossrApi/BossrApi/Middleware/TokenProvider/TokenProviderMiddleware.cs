﻿using System;
using System.Net;
using System.Threading.Tasks;
using BossrApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using BossrApi.Models.Responses;
using BossrApi.Repositories.UserRepository;
using BossrApi.Services.Security.PasswordValidatorService;
using BossrApi.Services.ResponseWriter;

namespace BossrApi.Middleware.TokenProvider
{
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate next;
        private readonly TokenProviderOptions options;
        private readonly IResponseWriter responseWriter;
        private readonly IUserRepository userRepository;
        private readonly IPasswordValidatorService passwordValidatorService;
        private readonly ITokenGenerator tokenGenerator;

        public TokenProviderMiddleware(
            RequestDelegate next,
            IOptions<TokenProviderOptions> options,
            IResponseWriter responseWriter,
            IUserRepository userRepository,
            IPasswordValidatorService passwordValidatorService,
            ITokenGenerator tokenGenerator)
        {
            this.next = next;
            this.options = options.Value;
            this.responseWriter = responseWriter;
            this.userRepository = userRepository;
            this.passwordValidatorService = passwordValidatorService;
            this.tokenGenerator = tokenGenerator;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Equals(options.Path, StringComparison.Ordinal))
            {
                await next.Invoke(context);
                return;
            }

            if (!context.Request.Method.Equals("POST") || !context.Request.HasFormContentType)
            {
                var response = new MessageResponse { Message = "Tokens must be provided using POST and with the Content-Type: application/x-www-form-urlencode." };
                await responseWriter.WriteResponseAsync(context, HttpStatusCode.BadRequest, response);
                return;
            }

            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];

            var user = await userRepository.ReadAsync(username);
            if (!passwordValidatorService.IsPasswordValid(user, password))
            {
                var response = new MessageResponse { Message = "Invalid username or password." };
                await responseWriter.WriteResponseAsync(context, HttpStatusCode.BadRequest, response);
                return;
            }

            var token = tokenGenerator.GenerateToken(options, user);
            await responseWriter.WriteResponseAsync(context, HttpStatusCode.OK, token);
        }
    }
}
