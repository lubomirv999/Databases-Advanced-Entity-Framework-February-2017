namespace PhotoShare.Client.Core.Commands
{
    using System;

    using PhotoShare.Service;

    public class DeleteUserCommand
    {
        private UserService userService;

        public DeleteUserCommand(UserService userService)
        {
            this.userService = userService;
        }

        // DeleteUser <username>
        public string Execute(string[] data)
        {
            string username = data[0];

            if (!this.userService.IsExistingByUsername(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }

            if (!SecurityService.IsAuthenticated() || SecurityService.GetCurrentUser().Username != username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            // TODO: Case when user is already removed.
            this.userService.Remove(username);

            return $"User {username} was deleted successfully!";
        }
    }
}
