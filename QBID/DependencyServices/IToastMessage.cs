using System;
using System.Collections.Generic;
using System.Text;

namespace QBid.DependencyServices
{
    public interface IToastMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
