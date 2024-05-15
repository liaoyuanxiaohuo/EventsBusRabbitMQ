using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Contract
{
    public class EventbusOptions
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        public static Action<IServiceProvider, Exception, byte[]>? ReceiveExceptionEvent;
    }
}
