using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using OrganizationManagement.Models;
using OrganizationManagement.Repositories;
using OrganizationManagement.Services;
using System.Text.Json;

namespace OrganizationManagement.Controllers;

public class EmployeeController(EmployeeRepository repository, IMapper mapper) : Controller
{
    private readonly EmployeeRepository _repository = repository;
    private readonly IMapper _mapper = mapper;

    /// <summary>
    /// Displays a list of all employees.
    /// </summary>
    /// <returns>The view displaying the list of employees.</returns>
    public IActionResult Index()
    {
        var employees = _repository.GetAll();
        var employeeDetails = _mapper.Map<IEnumerable<EmployeeDetails>>(employees);
        return View(employeeDetails);
    }

    /// <summary>
    /// Displays the details of a specific employee by their ID.
    /// </summary>
    /// <param name="id">The ID of the employee to display.</param>
    /// <returns>The view displaying the employee details, or a 404 Not Found if the employee does not exist.</returns>
    public IActionResult Details(int id)
    {
        var employee = _repository.GetById(id);
        if (employee == null)
        {
            return NotFound();
        }
        var employeeDetails = _mapper.Map<EmployeeDetails>(employee);
        return View(employeeDetails);
    }

    /// <summary>
    /// Displays the view for creating a new employee.
    /// </summary>
    /// <returns>The view for creating a new employee.</returns>
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    /// Handles the creation of a new employee.
    /// </summary>
    /// <param name="employee">The employee data to create.</param>
    /// <returns>Redirects to the Index view if successful, otherwise returns the Create view with validation errors.</returns>
    [HttpPost]
    public IActionResult Create([ModelBinder]Employee employee)
    {
        if (ModelState.IsValid)
        {
            _repository.Add(employee);
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }

    /// <summary>
    /// Displays the view for editing an existing employee.
    /// </summary>
    /// <param name="id">The ID of the employee to edit.</param>
    /// <returns>The view for editing the employee, or a 404 Not Found if the employee does not exist.</returns>
    public IActionResult Edit(int id)
    {
        var employee = _repository.GetById(id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    /// <summary>
    /// Handles the update of an existing employee.
    /// </summary>
    /// <param name="id">The ID of the employee to update.</param>
    /// <param name="employee">The updated employee data.</param>
    /// <returns>Redirects to the Index view if successful, otherwise returns the Edit view with validation errors.</returns>
    [HttpPost]
    public IActionResult Edit(Employee employee)
    {
        if (ModelState.IsValid)
        {
            _repository.Update(employee);
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }

    /// <summary>
    /// Displays the view for deleting an existing employee.
    /// </summary>
    /// <param name="id">The ID of the employee to delete.</param>
    /// <returns>The view for deleting the employee, or a 404 Not Found if the employee does not exist.</returns>
    public IActionResult Delete(int id)
    {
        var employee = _repository.GetById(id);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    /// <summary>
    /// Handles the deletion of an existing employee.
    /// </summary>
    /// <param name="id">The ID of the employee to delete.</param>
    /// <returns>Redirects to the Index view after deletion.</returns>
    [HttpPost]
    public IActionResult DeleteConfirmed(int id)
    {
        _repository.Delete(id);
        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// Returns a list of employees in JSON format.
    /// </summary>
    /// <returns>The JSON representation of the employee list.</returns>
    [HttpGet("api/employees/json")]
    public IActionResult GetEmployeesAsJson()
    {
        var employees = _repository.GetAll();
        var employeeDetails = _mapper.Map<IEnumerable<EmployeeDetails>>(employees);
        var json = SerializationHelper.SerializeToJson(employeeDetails);
        return Content(json, "application/json");
    }

    /// <summary>
    /// Returns a list of employees in XML format.
    /// </summary>
    /// <returns>The XML representation of the employee list.</returns>
    [HttpGet("api/employees/xml")]
    public IActionResult GetEmployeesAsXml()
    {
        var employees = _repository.GetAll();
        var employeeDetails = _mapper.Map<List<EmployeeDetails>>(employees); // Change to List
        var xml = SerializationHelper.SerializeToXml(employeeDetails);
        return Content(xml, "application/xml");
    }

    /// <summary>
    /// Creates a new employee from JSON data.
    /// </summary>
    /// <param name="json">The JSON representation of the employee data.</param>
    /// <returns>Ok if the employee is successfully created, otherwise a BadRequest with an error message.</returns>
    [HttpPost("api/employees/json")]
    public IActionResult PostEmployeeFromJson([FromBody] string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return BadRequest("Empty or null JSON data");
        }

        try
        {
            var employeeDetails = SerializationHelper.DeserializeFromJson<EmployeeDetails>(json);

            if (employeeDetails == null)
            {
                return BadRequest("Failed to deserialize JSON data");
            }

            // Optionally: Process or save the deserialized data
            var employee = _mapper.Map<Employee>(employeeDetails);
            _repository.Add(employee);

            return Ok();
        }
        catch (JsonException ex)
        {
            // Handle JSON deserialization errors
            return BadRequest($"Invalid JSON format: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates new employees from XML data.
    /// </summary>
    /// <param name="xml">The XML representation of the employee data.</param>
    /// <returns>Ok if the employees are successfully created, otherwise a BadRequest with an error message.</returns>
    [HttpPost("api/employees/xml")]
    public IActionResult PostEmployeesFromXml([FromBody] string xml)
    {
        var employeeDetails = SerializationHelper.DeserializeFromXml<IEnumerable<EmployeeDetails>>(xml);
        if (employeeDetails != null)
        {
            // Optionally: Process or save the deserialized data
            foreach (var detail in employeeDetails)
            {
                var employee = _mapper.Map<Employee>(detail);
                _repository.Add(employee);
            }
            return Ok();
        }
        return BadRequest("Invalid XML data");
    }
}


