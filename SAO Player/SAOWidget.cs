using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAO_Player
{
    /// <summary>
    /// SAO 组件接口
    /// </summary>
    public interface SAOWidget 
    {
        void onPlay();
        void onStop();
    }
}
