namespace PhotoShare.Client
{
    using Core;

    public class Application
    {
        public static void Main()
        {
            // This represents a solution for the PhotoShareSystem.
            // NOTE: solution is not 100% optimal and is not completely finished - there are some checks
            // which need to be made (like how much arguments you pass to a command or so).
            CommandDispatcher commandDispatcher = new CommandDispatcher();
            Engine engine = new Engine(commandDispatcher);
            engine.Run();
        }
    }
}
