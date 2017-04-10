namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using System.Text;

    using Models;
    using Service;

    public class ListFriendsCommand
    {
        private UserService userService;

        public ListFriendsCommand(UserService userService)
        {
            this.userService = userService;
        }

        // PrintFriendsList <username>
        public string Execute(string[] data)
        {
            string username = data[0];

            if (!this.userService.IsExistingByUsername(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }

            var friends = this.userService.GetFriends(username);

            if (friends.Count() == 0)
            {
                return "No friends for this user. :(";
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Friends:");
            foreach (User friend in friends)
            {
                sb.AppendLine($"-{friend.Username}");
            }

            return sb.ToString().Trim();
        }
    }
}
