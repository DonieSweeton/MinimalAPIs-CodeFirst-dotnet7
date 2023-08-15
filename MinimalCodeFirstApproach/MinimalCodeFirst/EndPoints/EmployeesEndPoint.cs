using Microsoft.EntityFrameworkCore;
using MinimalCodeFirst.Data;
using MinimalCodeFirst.Models;

namespace MiniDB0731.EndPoints
{
    public static class EmployeesEndpointClass
    {
        public static void EmployeesEndPoint(this WebApplication app)
        {
            app.MapGet("/employees", List);
            app.MapGet("/employees/{employee_id}", Get);
            app.MapPost("/employees", Create);
            app.MapPut("/employees/{employee_id}", Update);
            app.MapDelete("/employees/{employee_id}", Delete);
        }

        public static async Task<IResult> List(CodeContext db)
        {
            var result = await db.Employees.Where(e => !e.IsDeleted).ToListAsync();
            return Results.Ok(result);
        }

        public static async Task<IResult> Get(CodeContext db, int employee_id)
        {
            var employee = await db.Employees
                .Where(e => !e.IsDeleted)
                .FirstOrDefaultAsync(e => e.EmployeeId == employee_id);

            if (employee != null)
            {
                return Results.Ok(employee);
            }
            else
            {
                return Results.NotFound("Employee not found.");
            }
        }

        public static async Task<IResult> Create(CodeContext db, string name, int age, string createdBy)
        {
            var newEmployee = new Employee
            {
                Name = name,
                Age = age,
                CreatedBy = createdBy,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            db.Employees.Add(newEmployee);
            await db.SaveChangesAsync();

            return Results.Created($"/employees/{newEmployee.EmployeeId}", newEmployee);
        }

        public static async Task<IResult> Update(CodeContext db, int employee_id, Employee updateEmployee)
        {
            var employee = await db.Employees.FindAsync(employee_id);

            if (employee == null)
            {
                return Results.NotFound("Employee not found.");
            }

            employee.Name = updateEmployee.Name;
            employee.Age = updateEmployee.Age;
            employee.ModifiedBy = updateEmployee.ModifiedBy;
            employee.ModifiedDate = DateTime.UtcNow;

            await db.SaveChangesAsync();

            return Results.Ok("Employee has been updated.");
        }

        public static async Task<IResult> Delete(CodeContext db, int employee_id)
        {
            var employee = await db.Employees.FindAsync(employee_id);

            if (employee != null)
            {
                employee.IsDeleted = true;

                await db.SaveChangesAsync();

                return Results.Ok("Employee has been deleted.");
            }
            else
            {
                return Results.NotFound("Employee not found.");
            }
        }
    }
}
