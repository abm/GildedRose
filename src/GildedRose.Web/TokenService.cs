using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GildedRose
{
    public class TokenService : ITokenService
    {
        private readonly Dictionary<string, Customer> tokens;

        public TokenService(Dictionary<string, Customer> tokens)
        {
            this.tokens = tokens;
        }

        public Task<Customer> Authenticate(string token)
        {
            return Task.FromResult(tokens.ContainsKey(token)
                ? tokens[token]
                : null
            );
        }
    }
}