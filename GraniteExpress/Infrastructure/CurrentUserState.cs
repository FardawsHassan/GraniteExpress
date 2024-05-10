﻿namespace GraniteExpress.Infrastructure
{
    public class CurrentUserState
    {
        public string UserId { get; private set; }
        public string UserName { get; private set; }

        public void SetState(string userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }
    }
}