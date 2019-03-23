using System;
using GF.DillyDally.Data.Tasks;

namespace GF.DillyDally.Data
{
    public sealed class DataInitializer
    {
        public void InitializeDataLayer(Action<Type, Type> serviceRegister)
        {
            serviceRegister(typeof(ITasksRepository), typeof(TasksRepository));
        }
    }
}