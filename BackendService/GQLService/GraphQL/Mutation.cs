using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GQLService.GraphQL.Test;

namespace GQLService.GraphQL
{
    public class Mutation
    {
        public async Task<TestPayload> AddTest(TestInput test)
        {
            await Task.Delay(0);
            return new TestPayload($"Hello {test.Test}", $" World");
        }

        public async Task<string> BookCar(string test)
        {
            await Task.Delay(0);
            return $"這是BookCar {test}";
        }

        public async Task<string> CancelCar(string test)
        {
            await Task.Delay(0);
            return $"這是CancelCar {test}";
        }
    }
}