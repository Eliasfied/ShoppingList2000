using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.EventHandlers
{
    public interface IEventHandler<TEvent>
    {
        void Handle(TEvent tEvent);
    }
}
