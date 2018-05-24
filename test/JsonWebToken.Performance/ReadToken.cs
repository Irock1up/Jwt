﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Running;
using Jose;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;

namespace JsonWebToken.Performance
{
    [Config(typeof(DefaultCoreConfig))]
    public class ReadToken
    {
        private static readonly IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
        private static readonly IJsonSerializer serializer = new JsonNetSerializer();
        private static readonly IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
        private static readonly IDateTimeProvider dateTimeProvider = new UtcDateTimeProvider();
        public static readonly IJwtEncoder JwtDotNetEncoder = new JwtEncoder(algorithm, serializer, urlEncoder);
        public static readonly JwtDecoder JwtDotNetDecoder = new JwtDecoder(serializer, new JwtValidator(serializer, dateTimeProvider), urlEncoder);

        private static readonly SymmetricJwk SymmetricKey = new SymmetricJwk
        {
            Use = "sig",
            Kid = "kid-hs256",
            K = "GdaXeVyiJwKmz5LFhcbcng",
            Alg = "HS256"
        };
        public static readonly JsonWebTokenReader Reader = new JsonWebTokenReader();
        private static readonly TokenValidationParameters validationParameters = TokenValidationParameters.NoValidation;
        public static readonly JwtSecurityTokenHandler Handler = new JwtSecurityTokenHandler() { MaximumTokenSizeInBytes = 4 * 1024 * 1024 };

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(GetTokens))]
        public void Jwt(string token)
        {
            var result = Reader.TryReadToken(Tokens.ValidTokens[token], validationParameters);
            if (!result.Succedeed)
            {
                throw new Exception();
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(GetTokens))]
        public void Wilson(string token)
        {
            var result = Handler.ReadJwtToken(Tokens.ValidTokens[token]);
            if (result == null)
            {
                throw new Exception();
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(GetTokens))]
        public void JoseDotNet(string token)
        {
            // unable to read the token without signature validation
            var value = Jose.JWT.Decode(Tokens.ValidTokens[token], SymmetricKey.RawK);
            if (value == null)
            {
                throw new Exception();
            }
        }

        [Benchmark]
        [ArgumentsSource(nameof(GetTokens))]
        public void JwtDotNet(string token)
        {
            var value = JwtDotNetDecoder.Decode(Tokens.ValidTokens[token]);
            if (value == null)
            {
                throw new Exception();
            }
        }

        public IEnumerable<object[]> GetTokens()
        {
            yield return new[] { "empty" };
            yield return new[] { "small" };
            yield return new[] { "medium" };
            yield return new[] { "big" };
        }
    }
}
