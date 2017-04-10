namespace PhotoShare.Client.Core
{
    using System;
    using System.Linq;

    using Commands;
    using Service;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters)
        {
            string commandName = commandParameters[0];
            commandParameters = commandParameters.Skip(1).ToArray();
            string result = string.Empty;

            UserService userService = new UserService();
            TownService townService = new TownService();
            TagService tagService = new TagService();
            AlbumService albumService = new AlbumService();
            PictureService pictureService = new PictureService();

            switch (commandName)
            {
                case "RegisterUser":
                    RegisterUserCommand registerUser = new RegisterUserCommand(userService);
                    result = registerUser.Execute(commandParameters);
                    break;
                case "AddTown":
                    AddTownCommand addTown = new AddTownCommand(townService);
                    result = addTown.Execute(commandParameters);
                    break;
                case "ModifyUser":
                    ModifyUserCommand modifyUser = new ModifyUserCommand(userService, townService);
                    result = modifyUser.Execute(commandParameters);
                    break;
                case "DeleteUser":
                    DeleteUserCommand deleteUser = new DeleteUserCommand(userService);
                    result = deleteUser.Execute(commandParameters);
                    break;
                case "AddTag":
                    AddTagCommand addTag = new AddTagCommand(tagService);
                    result = addTag.Execute(commandParameters);
                    break;
                case "CreateAlbum":
                    CreateAlbumCommand createAlbum = new CreateAlbumCommand(userService, albumService, tagService);
                    result = createAlbum.Execute(commandParameters);
                    break;
                case "AddTagTo":
                    AddTagToCommand addTagTo = new AddTagToCommand(tagService, albumService);
                    result = addTagTo.Execute(commandParameters);
                    break;
                case "MakeFriends":
                    MakeFriendsCommand makeFriends = new MakeFriendsCommand(userService);
                    result = makeFriends.Execute(commandParameters);
                    break;
                case "ListFriends":
                    ListFriendsCommand listFriends = new ListFriendsCommand(userService);
                    result = listFriends.Execute(commandParameters);
                    break;
                case "ShareAlbum":
                    ShareAlbumCommand shareAlbum = new ShareAlbumCommand(userService, albumService);
                    result = shareAlbum.Execute(commandParameters);
                    break;
                case "UploadPicture":
                    UploadPictureCommand uploadPicture = new UploadPictureCommand(albumService, pictureService);
                    result = uploadPicture.Execute(commandParameters);
                    break;
                case "Login":
                    LoginCommand loginCommand = new LoginCommand();
                    result = loginCommand.Execute(commandParameters);
                    break;
                case "Logout":
                    LogoutCommand logout = new LogoutCommand();
                    result = logout.Execute(commandParameters);
                    break;
                case "Exit":
                    ExitCommand exit = new ExitCommand();
                    exit.Execute();
                    break;
                default:
                    throw new InvalidOperationException($"Command {commandName} not valid!");
            }

            return result;
        }
    }
}
