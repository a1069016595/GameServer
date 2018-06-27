using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LolServer;
using Protocol;
using AceNetFrameWork;
using LolServer.Biz;
using AceNetFrameWork.ace.auto;
using AceNetFrameWork.ace;

public class User
{
    public string userName;
    public string passWord;

    public  User (string _userName,string _password)
    {
        userName=_userName;
        passWord=_password;
    }
}

 public class LoginHandler :AbsOnceHandler, HandlerInterface
{
     IAccountBiz biz = BizFactory.accountBiz;

     private List<User> userList;
     public   LoginHandler()
     {
         userList = new List<User>();
         userList.Add(new User("a", "1"));
         userList.Add(new User("b", "2"));
     }

     //IAccountBiz accountBiz;
  //   IUserBiz userBiz;
    public void ClientClose(UserToken token, string error)
    {
        ExecutorPool.Instance.execute
            (
                delegate()
                {
                    biz.close(token);
                }
            );
    }

    public void Messagereceive(UserToken token, SocketModel message)
    {
        AccountInfoDTO dto = message.getMessage<AccountInfoDTO>();
        switch (message.command)
        {
            case LoginProtool.LOGIN_BRQ:
                login(token, dto);
                break;
            default:
                break;
        }

        //ExecutorPool.Instance.execute
        //   (
        //       delegate()
        //       {
        //           //RoomListDTO a = new RoomListDTO();


        //           //    RoomInfoDTO b = new RoomInfoDTO();
        //           //    b.roomID = "fw";
        //           //    b.roomName = "33";
        //           //    b.roomOwner = "哈";
        //           //    b.roomState = "正在准备";
        //           //    a.roomList[0] = b;
        //           //RoomListDTO a = new RoomListDTO();

        //           //a.roomList=new RoomInfoDTO[15];

        //           //RoomInfoDTO b = new RoomInfoDTO();
        //           //b.roomID = "fw";
        //           //b.roomName = "33";
        //           //b.roomOwner = "哈";
        //           //b.roomState = "正在准备";
        //           //for (int i = 0; i < 15; i++)
        //           //{
        //           //    a.roomList[i] = b;
        //           //}
        //           //write(token, MainProtocol.TYPE_GAMEHALL, GameHallProtocol.GAMEHALL_ROOMLIST, 0, a);

        //       }
        //   );
    }

    public void login(UserToken token, AccountInfoDTO value)
    {
        ExecutorPool.Instance.execute
            (
                delegate()
                {
                    write(token,TypeProtocol.TYPE_LOGIN_CREQ, 0,LoginProtool.LOGIN_CREQ, biz.login(token, value.account, "0"));
                }
            );
    }

    public void reg(UserToken token, AccountInfoDTO value)
    {

    }

    public void ClientConnect(UserToken token)
    {

    }
}

