using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsBus.RabbitMQ
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EventsBusAttribute : Attribute
    {
        public readonly string Name;

        public EventsBusAttribute(string name)
        {
            Name = name;
        }
    }
}
