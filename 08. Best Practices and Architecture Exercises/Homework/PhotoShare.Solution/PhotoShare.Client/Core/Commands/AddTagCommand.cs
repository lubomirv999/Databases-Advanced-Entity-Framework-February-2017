namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;

    using PhotoShare.Service;

    using Utilities;

    public class AddTagCommand
    {
        private TagService tagService;

        public AddTagCommand(TagService tagService)
        {
            this.tagService = tagService;
        }

        // AddTag <tag>
        public string Execute(string[] data)
        {
            string tag = TagUtilities.ValidateOrTransform(data[0]);

            if (this.tagService.IsTagExisting(tag))
            {
                throw new ArgumentException($"Tag {tag} exists!");
            }

            if (!SecurityService.IsAuthenticated())
            {
                throw new InvalidOperationException("Invalid credentials!");
            }

            this.tagService.AddTag(tag);

            return $"Tag {tag} was added successfully!";
        }
    }
}
