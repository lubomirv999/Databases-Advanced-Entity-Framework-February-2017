namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Models;

    using Service;

    public class ShareAlbumCommand
    {
        private UserService userService;

        private AlbumService albumService;

        public ShareAlbumCommand(UserService userService, AlbumService albumService)
        {
            this.userService = userService;
            this.albumService = albumService;
        }

        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        public string Execute(string[] data)
        {
            int albumId = int.Parse(data[0]);
            string username = data[1];
            string permission = data[2];

            if (!this.userService.IsExistingByUsername(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }

            Album album = this.albumService.GetById(albumId);
            if (album == null)
            {
                throw new ArgumentException($"Album {albumId} not found!");
            }

            if (!SecurityService.IsAuthenticated())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            if (!this.albumService.IsUserOwnerOfAlbum(SecurityService.GetCurrentUser().Username, album.Name))
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            Role role;
            bool isValidRole = Enum.TryParse(permission, out role);

            if (!isValidRole)
            {
                throw new ArgumentException("Permission must be either “Owner” or “Viewer”!");
            }

            this.albumService.ShareAlbum(username, albumId, role);

            return $"User {username} added to album {album.Name}({permission})";
        }
    }
}
