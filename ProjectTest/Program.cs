using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Services;
using BLL.Infrastructure;
using BLL.DTO;
using BLL.Interfaces;
namespace ProjectTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                TaskService taskService = new TaskService(@"data source = (localdb)\MSSQLLocalDB; AttachDbFilename =| DataDirectory | \userdb.mdf; Integrated Security = True; ");
                taskService.AddTask(new TaskDTO() {Id=9, Name = "sdf",StartDate= DateTime.Now, DeadLine = DateTime.Now });
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
