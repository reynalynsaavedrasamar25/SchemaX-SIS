using System;
using System.Collections.Generic;
using System.Text;

namespace SchemaX.SIS.Helpers
{
    public static class Session
    {
        public static Models.AppUser? CurrentUser { get; set; }

        public static int CurrentUserId => CurrentUser?.UserId ?? throw new InvalidOperationException("No user is logged in.");
    }
}
