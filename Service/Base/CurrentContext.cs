using Model.Common;
using Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace Service.Base
{
    public class CurrentContext
    {
        public static void SetUser(ICurrentUser currentUser)
        {
            if (currentUser != null)
            {
                SessionHelper.Set("Current_User", currentUser, 20);
            }
        }

        public static ICurrentUser GetUser()
        {
            var currentUser = SessionHelper.Get("Current_User");

            return currentUser as ICurrentUser;
        }

        public static void ClearUser()
        {
            SessionHelper.Remove("Current_User");
        }
    }
}