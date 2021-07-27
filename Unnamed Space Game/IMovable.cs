using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unnamed_Space_Game
{
    public interface IMovable
    {
        MoveComponent Movement { get; set; }
    }
}
