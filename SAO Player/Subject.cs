using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAO_Player
{
    public interface Subject
    {
        void attach(SAO_Widget saoWidget);
        void detach(SAO_Widget saoWidget);
       
    }
}
