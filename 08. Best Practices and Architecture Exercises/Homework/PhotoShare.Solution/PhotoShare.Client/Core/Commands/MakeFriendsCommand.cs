namespace PhotoShare.Client.Core.Commands
{
    using System;

    using PhotoShare.Models;
    using PhotoShare.Service;

    public class MakeFriendsCommand
    {
        private UserService userService;

        public MakeFriendsCommand(UserService userService)
        {
            this.userService = userService;
        }

        // MakeFriends <username1> <username2>
        public string Execute(string[] data)
        {
            string requesterUsername = data[0];
            string friendUsername = data[1];

            if (!SecurityService.IsAuthenticated())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            if (!this.userService.IsExistingByUsername(requesterUsername))
            {
                throw new ArgumentException($"User {requesterUsername} not found!");
            }

            if (!this.userService.IsExistingByUsername(friendUsername))
            {
                throw new ArgumentException($"User {friendUsername} not found!");
            }

            if (SecurityService.GetCurrentUser().Username != requesterUsername)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }
            
            if (this.userService.AreFriends(requesterUsername, friendUsername))
            {
                throw new InvalidOperationException($"{friendUsername} is already a friend to {requesterUsername}!");
            }

            this.userService.MakeFriends(requesterUsername, friendUsername);

            return $"Friend {friendUsername} added to {requesterUsername}!";
        }
    }
}
