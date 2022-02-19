using HotChocolate.AspNetCore;
using HotChocolate.Execution;

using MoviesAPI.Extensions;

using System.IdentityModel.Tokens.Jwt;

namespace MoviesAPI.Interceptors
{
    public class RequestInterceptor : DefaultHttpRequestInterceptor
    {
        public override ValueTask OnCreateAsync(HttpContext context,
            IRequestExecutor requestExecutor,
            IQueryRequestBuilder requestBuilder,
            CancellationToken cancellationToken)
        {
            if (context.Request.Headers.ContainsKey(Statics.neo4jdatabase))
            {
                var db = context.Request.Headers[Statics.neo4jdatabase].FirstOrDefault();
                requestBuilder.SetProperty(Statics.neo4jdatabase, db);
            }

            if (context.Request.Headers.ContainsKey(Statics.Authorization))
            {
                var access_token = (context.Request.Headers[Statics.Authorization])
                    .ToString()
                    .Split("Bearer ")[1]
                    .Trim();


                ProcessJwt(access_token, requestBuilder);
            }

            return base.OnCreateAsync(context, requestExecutor, requestBuilder, cancellationToken);
        }

        private static void ProcessJwt(string token, IQueryRequestBuilder builder)
        {
            var payload = new JwtSecurityTokenHandler().ReadJwtToken(token);

            builder.SetProperty(nameof(payload.Issuer), payload.Issuer);
            builder.SetProperty("oid", payload.Claims.First(c => c.Type == "oid").Value);
            builder.SetProperty("uuid", payload.Claims.First(c => c.Type == "uuid").Value);
            builder.SetProperty("email", payload.Claims.First(c => c.Type == "Email").Value);
        }
    }


}
