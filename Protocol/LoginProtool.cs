using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
    public class LoginProtool
    {
        public const int LOGIN_CREQ = 0;//登录
        public const int LOGIN_BRQ = 1;

        public const int REG_CREQ = 2;//注册
        public const int REG_BRQ = 3;
    }

[Serializable]
public class Login_Result_Protool
{
    public const int AccountNotExit = -4;
    public const int PasswordError= -3;
    public const int UserIsOnLine = -2;
    public const int LoginSuccess = 0;
}
