using Microsoft.SemanticKernel;
using Plugins.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugins.KernelPlugins;

public class EmployeePlugin
{
    private Employee empObj;

    public EmployeePlugin(Employee employee)
    {
        empObj = employee;
    }

    // get all employee
    [KernelFunction("GetAllEmployee"),Description("A function to get all the employee details as a json string") ]
    [return:Description("Returns all the employee details in string format.")]
    public string GetAllEmployee()
    {
        StringBuilder sb= new ();
        foreach (var emp in empObj.GetAllEmployees())
        {
            sb.Append(emp.ToString());
        }
        return sb.ToString();
    }

    // update work status
    [KernelFunction("UpdateEmployeeWorkStatus"), Description("A function to update employee work status based on emp id")]
    [return: Description("Returns true or false depends on updation")]
    public bool UpdateEmployeeWorkStatus([Description("Employee ID to be identify employee for workstatus updation")]int EmpID,[Description("Empoyee work status to be updated")] bool workStatus)
    {
        return empObj.UpdateEmployeeWorkingStatus(EmpID, workStatus);
    }

    // update work location

    // add employee

}
