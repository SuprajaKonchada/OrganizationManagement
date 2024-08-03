using AutoMapper;
using OrganizationManagement.Data;
using OrganizationManagement.Models;
using OrganizationManagement.Services;

namespace OrganizationManagement.Repositories;

public class EmployeeRepository(AppDbContext context, IMapper mapper)
{
    private readonly AppDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Retrieves all <see cref="Employee"/> entities from the database.
    /// </summary>
    /// <returns>An <see cref="IEnumerable{Employee}"/> containing all employees.</returns>
    public IEnumerable<Employee> GetAll()
    {
        return [.. _context.Employees];
    }

    /// <summary>
    /// Retrieves an <see cref="Employee"/> entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the employee to retrieve.</param>
    /// <returns>The <see cref="Employee"/> entity if found; otherwise, <c>null</c>.</returns>
    public Employee? GetById(int id)
    {
        return _context.Employees.Find(id);
    }

    /// <summary>
    /// Retrieves detailed information about an <see cref="Employee"/> entity by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the employee whose details to retrieve.</param>
    /// <returns>An <see cref="EmployeeDetails"/> object containing the employee's details if found; otherwise, <c>null</c>.</returns>
    public EmployeeDetails? GetDetailsById(int id)
    {
        var employee = _context.Employees.Find(id);
        return employee != null ? _mapper.Map<EmployeeDetails>(employee) : null;
    }


    /// <summary>
    /// Adds a new <see cref="Employee"/> entity to the database.
    /// </summary>
    /// <param name="employee">The <see cref="Employee"/> entity to add.</param>
    public void Add(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
    }

    /// <summary>
    /// Updates an existing <see cref="Employee"/> entity in the database.
    /// </summary>
    /// <param name="employee">The updated <see cref="Employee"/> entity.</param>
    public void Update(Employee employee)
    {
        var existing = _context.Employees.Find(employee.Id);
        if (existing != null)
        {
            foreach (var property in typeof(Employee).GetProperties())
            {
                if (property.CanWrite)
                {
                    var value = property.GetValue(employee);
                    if (value != null)
                    {
                        ReflectionHelper.SetPropertyValue(existing, property.Name, value);
                    }

                }
            }
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Deletes an <see cref="Employee"/> entity from the database by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="Employee"/> entity to delete.</param>
    public void Delete(int id)
    {
        var employee = _context.Employees.Find(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
        }
    }
}

