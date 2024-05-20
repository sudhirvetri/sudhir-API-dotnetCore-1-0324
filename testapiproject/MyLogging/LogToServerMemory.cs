namespace testapiproject.MyLogging
{
    public class LogToServerMemory : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Log to Server Memory");
            //write your own logic to save the log to Server Memory. 
        }
    }
}