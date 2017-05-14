using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.ServiceLayer
{
    public interface IService
    {
        string Register(string username, string password, string email);
        string Login(String username, String password);
        string EditPlayer(string username, string type, string newValue);
    }
}
