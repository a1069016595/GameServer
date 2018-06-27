using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceNetFrameWork;
using AceNetFrameWork.ace;
using AceNetFrameWork.ace.auto;
namespace LolServer
{
    public interface HandlerInterface
    {
          void ClientConnect(UserToken token);
       

          void Messagereceive( UserToken token, SocketModel message);


          void ClientClose(UserToken token, string error);
       
    }
}
