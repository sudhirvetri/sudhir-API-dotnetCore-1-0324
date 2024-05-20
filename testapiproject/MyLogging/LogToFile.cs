namespace testapiproject.MyLogging
{
    public class LogToFile : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Log to File");
            //write your own logic to save the log to file. 
        }
    }
}