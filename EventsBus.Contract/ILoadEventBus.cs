using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsBus.Contract
{
    public interface ILoadEventBus
    {
        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEto"></typeparam>
        /// <param name="eto"></param>
        /// <returns></returns>
        Task PushAsync<TEto>(TEto eto) where TEto : class;
    }
}
