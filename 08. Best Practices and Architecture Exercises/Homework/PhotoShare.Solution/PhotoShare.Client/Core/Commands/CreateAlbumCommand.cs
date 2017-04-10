namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using Models;

    using Service;

    using Utilities;

    public class CreateAlbumCommand
    {
        private UserService userService;

        private AlbumService albumService;

        private TagService tagService;

        public CreateAlbumCommand(UserService userService, AlbumService albumService, TagService tagService)
        {
            this.userService = userService;
            this.albumService = albumService;
            this.tagService = tagService;
        }

        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] data)
        {
            string username = data[0];

            if (!this.userService.IsExistingByUsername(username))
            {
                throw new ArgumentException($"User {username} not found!");
            }

            if (!SecurityService.IsAuthenticated())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            string albumName = data[1];

            if (SecurityService.GetCurrentUser().Username != username)
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            if (this.albumService.IsAlbumExisting(albumName))
            {
                throw new ArgumentException($"Album {albumName} exists!");
            }

            string backgroundColor = data[2];

            Color color = (Color)Enum.Parse(typeof(Color), backgroundColor);

            string[] tagsToInclude = data.Skip(3).Select(t => TagUtilities.ValidateOrTransform(t)).ToArray();

            if (tagsToInclude.Any(t => !this.tagService.IsTagExisting(t)))
            {
                throw new ArgumentException("Invalid tags!");
            }

            this.albumService.AddAlbum(username, albumName, color, tagsToInclude);
            
            return $"Album {albumName} successfully created!";
        }
    }
}
