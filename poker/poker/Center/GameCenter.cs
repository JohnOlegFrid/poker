using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Center
{

    public class GameCenter
    {
        private List<League> leagues;
        
        public GameCenter(List<League> leagues)
        {
            this.leagues = leagues;
        }
    }
}
