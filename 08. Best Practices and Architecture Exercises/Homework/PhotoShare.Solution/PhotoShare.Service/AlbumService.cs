namespace PhotoShare.Service
{
    using System.Collections.Generic;
    using System.Linq;

    using Data;

    using Models;

    public class AlbumService
    {
        public bool IsAlbumExisting(string albumName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Albums.Any(a => a.Name == albumName);
            }
        }

        public Album GetById(int id)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Albums.Find(id);
            }
        }

        public bool IsUserOwnerOfAlbum(string username, string albumName)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Album album = context.Albums
                    .Include("AlbumRoles")
                    .Include("AlbumRoles.User")
                    .SingleOrDefault(a => a.Name == albumName);

                if (album == null)
                {
                    return false;
                }

                return album.AlbumRoles.Any(ar => ar.User.Username == username && ar.Role == Role.Owner);
            }
        }

        public void AddAlbum(string username, string albumName, Color color, string[] tagsToInclude)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                Album album = new Album();
                album.Name = albumName;
                album.BackgroundColor = color;

                User user = context.Users.SingleOrDefault(u => u.Username == username);
                AlbumRole albumRole = new AlbumRole();
                albumRole.Album = album;
                albumRole.User = user;
                albumRole.Role = Role.Owner;
                album.AlbumRoles.Add(albumRole);

                List<Tag> tags = tagsToInclude.Select(t => context.Tags.SingleOrDefault(tag => tag.Name == t)).ToList();
                album.Tags = tags;
                context.Albums.Add(album);
                context.SaveChanges();
            }
        }

        public void ShareAlbum(string username, int albumId, Role role)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                User userToAdd = context.Users.SingleOrDefault(u => u.Username == username);
                Album album = context.Albums.Find(albumId);

                if (userToAdd != null && album != null)
                {
                    AlbumRole albumRole = new AlbumRole();
                    albumRole.User = userToAdd;
                    albumRole.Album = album;
                    albumRole.Role = role;

                    context.AlbumRoles.Add(albumRole);
                    context.SaveChanges();
                }
            }
        }
    }
}
