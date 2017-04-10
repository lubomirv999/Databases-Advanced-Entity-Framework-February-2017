﻿namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Service;

    public class RegisterUserCommand
    {
        private readonly UserService userService;

        public RegisterUserCommand(UserService userService)
        {
            this.userService = userService;
        }

        // RegisterUser <username> <password> <repeat-password> <email>
        public string Execute(string[] data)
        {
            string username = data[0];
            string password = data[1];
            string repeatPassword = data[2];
            string email = data[3];

            if (this.userService.IsExistingByUsername(username))
            {
                throw new InvalidOperationException($"Username {username} is already taken! ");
            }

            if (password != repeatPassword)
            {
                throw new ArgumentException("Passwords do not match!");
            }

            this.userService.Add(username, password, email);

            return "User " + username + " was registered successfully!";
        }
    }
}
