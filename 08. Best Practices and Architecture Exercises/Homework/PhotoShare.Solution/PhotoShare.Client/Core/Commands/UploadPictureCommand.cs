namespace PhotoShare.Client.Core.Commands
{
    using System;

    using Service;

    public class UploadPictureCommand
    {
        private AlbumService albumService;

        private PictureService pictureService;

        public UploadPictureCommand(AlbumService albumService, PictureService pictureService)
        {
            this.albumService = albumService;
            this.pictureService = pictureService;
        }

        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public string Execute(string[] data)
        {
            string albumName = data[0];
            string pictureTitle = data[1];
            string pictureFilePath = data[2];

            if (!this.albumService.IsAlbumExisting(albumName))
            {
                throw new ArgumentException($"Album {albumName} not found!");
            }

            if (!SecurityService.IsAuthenticated())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            if (!this.albumService.IsUserOwnerOfAlbum(SecurityService.GetCurrentUser().Username, albumName))
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            this.pictureService.AddPicture(albumName, pictureTitle, pictureFilePath);

            return $"Picture {pictureTitle} added to {albumName}!";
        }
    }
}
