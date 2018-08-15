using Ninject.Modules;
using BLL.Services;
using BLL.Interfaces;


namespace TaskTrackingAPI.Util
{
    public class TaskModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITaskService>().To<TaskService>();
            Bind<ISubTaskService>().To<SubTaskService>();
        }
    }
}
