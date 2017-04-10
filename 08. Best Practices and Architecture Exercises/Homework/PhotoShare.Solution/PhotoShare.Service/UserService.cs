namespace PhotoShare.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using Data;
    using Models;

    public class UserService
    {
        public virtual void Add(string username, string password, string email)
        {
            User user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false,
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now
            };

            using (PhotoShareContext context = new PhotoShareContext())
            {
                // TODO: Check for username.
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public bool IsExistingByUsername(string username)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                return context.Users.Any(u => u.Username == username);
            }
        }

        public User GetUserByUsername(string username)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                User user = context.Users.AsNoTracking().SingleOrDefault(u => u.Username == username);
                return user;
            }
        }

        public void UpdateUser(User updatedUser)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                User userToUpdate = context.Users
                    .Include("BornTown")
                    .Include("CurrentTown")
                    .SingleOrDefault(u => u.Id == updatedUser.Id);

                if (userToUpdate != null)
                {
                    if (updatedUser.BornTown != null &&
                        (userToUpdate.BornTown == null || updatedUser.BornTown.Id != userToUpdate.BornTown.Id))
                    {
                        userToUpdate.BornTown = context.Towns.Find(updatedUser.BornTown.Id);
                    }

                    if (updatedUser.CurrentTown != null && 
                        (userToUpdate.CurrentTown == null || updatedUser.CurrentTown.Id != userToUpdate.CurrentTown.Id))
                    {
                        userToUpdate.CurrentTown = context.Towns.Find(updatedUser.CurrentTown.Id);
                    }

                    if (updatedUser.Password != userToUpdate.Password)
                    {
                        userToUpdate.Password = updatedUser.Password;
                    }

                    context.Entry(userToUpdate).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
        }

        public void Remove(string username)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                User user = context.Users.SingleOrDefault(u => u.Username == username);

                if (user != null)
                {
                    user.IsDeleted = true;
                    context.SaveChanges();
                }
            }
        }

        public bool AreFriends(string requesterUsername, string friendUsername)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                User requester = context.Users.Include("Friends").SingleOrDefault(u => u.Username == requesterUsername);

                if (requester == null)
                {
                    return false;
                }

                return requester.Friends.Any(f => f.Username == friendUsername);
            }
        }

        public void MakeFriends(string requesterUsername, string friendUsername)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                User requester = context.Users.SingleOrDefault(u => u.Username == requesterUsername);

                User friend = context.Users.SingleOrDefault(u => u.Username == friendUsername);

                if (requester != null && friend != null)
                {
                    requester.Friends.Add(friend);
                    context.SaveChanges();
                }
            }
        }

        public IEnumerable<User> GetFriends(string username)
        {
            using (PhotoShareContext context = new PhotoShareContext())
            {
                User user = context.Users.Include("Friends").SingleOrDefault(u => u.Username == username);

                if (user == null)
                {
                    return new List<User>();
                }

                return user.Friends.ToList();
            }
        }
    }
}
