﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JVNM
{
   public class AddUser : MiniSQLQuery
    {
         string user;
         string password;
        //añadir perfil de seguridad?
        string securityProfile;
        AddUser(string user, string password, string securityProfile) {
            this.user = user;
            this.password = password;
            //perfil de seguridad
            this.securityProfile = securityProfile;
        }

        public string Execute(Database database)
        {
            // return database.AddUser(user, password, securityProfile);
            throw new NotImplementedException();
        }
    }
}
