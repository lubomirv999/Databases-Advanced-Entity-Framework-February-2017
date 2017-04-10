namespace PhotoShare.Service
{
    using System.Linq;

    using Data;
    using Models;

    public class PictureService
    {
        public void AddPicture(string albumName, string pictureTitle, string pictureFilePath)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Album album = context.Albums.SingleOrDefault(a => a.Name == albumName);
                if (album != null)
                {
                    Picture picture = new Picture();
                    picture.Title = pictureTitle;
                    picture.Path = pictureFilePath;
                    picture.Albums.Add(album);

                    context.Pictures.Add(picture);
                    context.SaveChanges();
                }
            }
        }
    }
}
