namespace PhotoShare.Client.Core.Commands
{
    using System;

    using PhotoShare.Client.Utilities;

    using Service;

    public class AddTagToCommand
    {
        private TagService tagService;

        private AlbumService albumService;

        public AddTagToCommand(TagService tagService, AlbumService albumService)
        {
            this.tagService = tagService;
            this.albumService = albumService;
        }

        // AddTagTo <albumName> <tag>
        public string Execute(string[] data)
        {
            string albumName = data[0];
            string tagName = TagUtilities.ValidateOrTransform(data[1]);

            if (!this.albumService.IsAlbumExisting(albumName) || !this.tagService.IsTagExisting(tagName))
            {
                throw new ArgumentException("Either tag or album do not exist!");
            }

            if (!SecurityService.IsAuthenticated())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            if (!this.albumService.IsUserOwnerOfAlbum(SecurityService.GetCurrentUser().Username, albumName))
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            this.tagService.AddTagTo(albumName, tagName);

            return $"Tag {tagName} added to {albumName}!";
        }
    }
}
