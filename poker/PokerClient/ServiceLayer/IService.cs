using ClientPoker.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPoker.ServiceLayer
{
    public interface IService
    {
        void Login(string player);
    }
}
