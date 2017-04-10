namespace PhotoShare.Service
{
    using System;
    using System.Linq;

    using Data;
    using Models;

    public class TagService
    {
        public void AddTag(string tagLabel)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Tag tag = new Tag();
                tag.Name = tagLabel;

                context.Tags.Add(tag);
                context.SaveChanges();
            }
        }

        public bool IsTagExisting(string tagLabel)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Tags.Any(t => t.Name == tagLabel);
            }
        }

        public void AddTagTo(string albumName, string tagName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Album album = context.Albums.SingleOrDefault(a => a.Name == albumName);
                Tag tag = context.Tags.SingleOrDefault(t => t.Name == tagName);

                if (album != null && tag != null)
                {
                    tag.Albums.Add(album);
                    context.SaveChanges();
                }
            }
        }
    }
}
