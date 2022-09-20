using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_FrameWork.Application
{
    public interface IAuthHelper
    {
        void SignOut();
        bool IsAuthentcated();
        void SignIn(AuthViewModel account);
    }
}
