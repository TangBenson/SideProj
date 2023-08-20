using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace LoginGrpcService.Services;

public class EmployeeService : Employee.EmployeeBase
{
    private readonly ILogger<EmployeeService> logger;

    public EmployeeService(ILogger<EmployeeService> logger)
    {
        this.logger = logger;
    }

    public override Task<EmployeeModel> GetEmployee(
        EmployeeRequest request, 
        ServerCallContext context)
    {
        // Emulate get an employee.
        var employee = new EmployeeModel
        {
            Id = 1,
            Name = "Johnny",
            EmployeeType = EmployeeType.FirstLevel,
            PhoneNumbers = { new EmployeeModel.Types.PhoneNumber { Value = "0912345678" } },
            ModifiedTime = "ccgg"
        };

        return Task.FromResult(employee);
    }

    public override async Task GetAllEmployees(
        EmployeeRequest request, 
        IServerStreamWriter<EmployeeModel> responseStream, 
        ServerCallContext context)
    {
        // Emulate get all employees.
        var allEmployees = new List<EmployeeModel>
        {
            new EmployeeModel
            {
                Id = 1,
                Name = "Johnny",
                EmployeeType = EmployeeType.FirstLevel,
                PhoneNumbers = { new EmployeeModel.Types.PhoneNumber { Value = "0912345678" },
                                 new EmployeeModel.Types.PhoneNumber { Value = "0928250058" } },
                ModifiedTime = "ccgg"
            },
            new EmployeeModel
            {
                Id = 2,
                Name = "Mary",
                EmployeeType = EmployeeType.SecondLevel,
                PhoneNumbers = { new EmployeeModel.Types.PhoneNumber { Value = "0923456789" } },
                ModifiedTime = "ccgg"
            },
            new EmployeeModel
            {
                Id = 3,
                Name = "Tom",
                EmployeeType = EmployeeType.LastLevel,
                PhoneNumbers = { new EmployeeModel.Types.PhoneNumber { Value = "0934567890" } },
                ModifiedTime = "ccgg"
            }
        };

        foreach (var employee in allEmployees)
        {
            await responseStream.WriteAsync(employee);
        }
    }









    public override Task<EmployeeAddedResult> AddEmployee(EmployeeModel request, ServerCallContext context)
    {
        request.ModifiedTime = "ccgg";

        // ... Do add an employee.

        return Task.FromResult(new EmployeeAddedResult { IsSuccess = true });
    }

    public override async Task<EmployeeAddedResult> AddEmployees(IAsyncStreamReader<EmployeeModel> requestStream, ServerCallContext context)
    {
        var employees = new List<EmployeeModel>();

        while (await requestStream.MoveNext())
        {
            employees.Add(requestStream.Current);
        }

        // ... Do batch add employees.

        return new EmployeeAddedResult { IsSuccess = true };
    }
}
