using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.EventDispatcher
{
    public interface IEventDispatcher
    {
        void Dispatch<TEvent>(TEvent tEvent);
    }
}
