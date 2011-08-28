using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Mod03_ChelasMovies.DomainModel
{
    public class User
    {
        [HiddenInput]
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        public MembershipUser MembershipUser { get; set; }

        public string UserName { get { return MembershipUser.UserName; } }
    }
}
