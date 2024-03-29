﻿namespace _0_FrameWork.Application
{
    public class AuthViewModel
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string FullName { get; set; }
        public string  Username { get; set; }

        public AuthViewModel(long id, long roleId, string fullName, string username)
        {
            Id = id;
            RoleId = roleId;
            FullName = fullName;
            Username = username;
        }
    }
}