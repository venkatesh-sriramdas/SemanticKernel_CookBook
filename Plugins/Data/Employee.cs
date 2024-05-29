using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.Data;

public class Employee
{
    internal int EmployeeID { get; set; }
    internal string? EmployeeName { get; set; }
    internal string? EmailID { get; set; }
    internal bool IsActive { get; set; }
    internal string? WorkLocation { get; set; }
    internal string DepartmentName { get; set; }
    internal static List<Employee> Employees { get; set; }

    public override string ToString()
    {
        return $"{{ \"EmployeeID\": {EmployeeID}, \"EmployeeName\": \"{EmployeeName}\", \"EmailID\": \"{EmailID}\", \"IsActive\": {IsActive}, \"WorkLocation\": \"{WorkLocation}\", \"DepartmentName\": \"{DepartmentName}\" }}";
    }
    static Employee()
    {
        Employees = new List<Employee>();
        // pre populate with dummy details for Employees property here
        Employees.AddRange([ new Employee { EmployeeID = 1, EmployeeName = "John Doe (NYC)", EmailID = "john.doe@example.com", IsActive = true, WorkLocation = "New York City", DepartmentName = "Engineering" },
            new Employee { EmployeeID = 2, EmployeeName = "Jane Smith (NYC)", EmailID = "jane.smith@example.com", IsActive = true, WorkLocation = "New York City", DepartmentName = "Marketing" },

            // Employees from Bangalore
            new Employee { EmployeeID = 3, EmployeeName = "Rajesh Kumar (Bangalore)", EmailID = "rajesh.kumar@example.com", IsActive = true, WorkLocation = "Bangalore", DepartmentName = "IT" },
            new Employee { EmployeeID = 4, EmployeeName = "Priya Patel (Bangalore)", EmailID = "priya.patel@example.com", IsActive = true, WorkLocation = "Bangalore", DepartmentName = "HR" },

            // Employees from Delhi
            new Employee { EmployeeID = 5, EmployeeName = "Amit Verma (Delhi)", EmailID = "amit.verma@example.com", IsActive = true, WorkLocation = "Delhi", DepartmentName = "Finance" },
            new Employee { EmployeeID = 6, EmployeeName = "Neha Gupta (Delhi)", EmailID = "neha.gupta@example.com", IsActive = true, WorkLocation = "Delhi", DepartmentName = "HR" },

            // Employees from Sydney
            new Employee { EmployeeID = 7, EmployeeName = "Michael Brown (Sydney)", EmailID = "michael.brown@example.com", IsActive = true, WorkLocation = "Sydney", DepartmentName = "Sales" },
            new Employee { EmployeeID = 8, EmployeeName = "Emily White (Sydney)", EmailID = "emily.white@example.com", IsActive = true, WorkLocation = "Sydney", DepartmentName = "Marketing" }]);
    }
    public List<Employee> GetAllEmployees()
    {
        return Employees;
    }
    public bool UpdateEmployeeWorkLocation(int employeeID,string workLocation) {
        var employee = Employees.FirstOrDefault(e => e.EmployeeID ==employeeID);
        if (employee != null)
        {
            employee.WorkLocation = workLocation;
            return true;
        }
        else return false;
    }

    public bool UpdateEmployeeWorkingStatus(int employeeID, bool workingStatus)
    {
        var employee = Employees.FirstOrDefault(e => e.EmployeeID == employeeID);
        if (employee != null)
        {
            employee.IsActive = workingStatus;
            return true;
        }
        else return false;
    }


}
