using Newtonsoft.Json;
using Server.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Map
{
    
    public class Map
    {
        private List<DtoTile> _tileDatas;
        public List<DtoTile> Tiles => _tileDatas;

        public Map(List<DtoTile> tileDatas) {
            _tileDatas = tileDatas;
        }

    }
}
