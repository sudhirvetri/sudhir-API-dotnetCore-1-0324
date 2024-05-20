namespace testapiproject.MyLogging
{
    public class LogtoDB : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Log to DB");
            //write your own logic to save the log to DB. 
        }
    }
}
