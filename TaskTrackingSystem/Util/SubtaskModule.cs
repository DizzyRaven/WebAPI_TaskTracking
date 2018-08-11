using Ninject.Modules;
using BLL.Interfaces;
using BLL.Services;

namespace TaskTrackingSystem.Util
{
    public class SubtaskModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITaskService>().To<TaskService>();
        }

    }
}