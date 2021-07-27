using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteMeMonogameTest
{
    public interface IMovable
    {
        MoveComponent Movement { get; set; }
    }
}
