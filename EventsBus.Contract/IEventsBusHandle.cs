using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsBus.Contract
{
    public interface IEventsBusHandle<in TEto> where TEto : IEvent
    {
        /// <summary>
        /// 接收处理逻辑
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        Task HandleAsync(TEto eventData);
    }
}
