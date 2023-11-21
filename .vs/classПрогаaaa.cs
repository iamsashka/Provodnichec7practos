namespace Проводник2_0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string? currentDir = "";
            while (currentDir != null)
            {
                if (currentDir.Length == 0)
                {
                    currentDir = Explorer.GetDriver();
                }
                else
                {
                    currentDir = Explorer.GetDir(currentDir);
                }
            }
        }
    }
}
