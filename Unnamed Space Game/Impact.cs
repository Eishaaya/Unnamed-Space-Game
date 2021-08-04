using System;
using System.Collections.Generic;
using System.Text;

namespace Unnamed_Space_Game
{
    class Impact
    {
        bool happening;
        Impact (bool happened = false)
        {
            happening = happened;
        }
        public static implicit operator bool (Impact impact) => impact.happening;
        public static implicit operator Impact (bool boolean) => new Impact(boolean);
        
    }
}
