using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GQLService.GraphQL
{
    public class Mutation
    {
        public async Task<string> AddTest(string test)
        {
            await Task.Delay(0);
            return $"Hello {test}";
        }
    }
}