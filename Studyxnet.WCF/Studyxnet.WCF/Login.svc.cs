using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Studyxnet.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Login" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Login.svc or Login.svc.cs at the Solution Explorer and start debugging.
    public class Login : ILogin
    {
        public bool Logar(string usuario, string senha)
        {
            string senhaBanco = string.Format("@{0}{1}01", usuario.Substring(0, 1).ToUpper(), usuario.Substring(1));

            if (senha == senhaBanco)
            {
                return true;
            }

            return false;
        }
    }
}
