using System.IO;


class Program
{
    static string[] validFiles = new string[2];
    static string fileName = "eldenringpath.txt";
    static string filePath;
    
    
    static void Main(string[] args)
    {
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Hello! Welcome to GCC\'s Elden Ring anti cheat toggle!\n");
        
        
        if (!File.Exists(fileName))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("It seems you do not have a file containing the path for your Elden Ring files\n\n=========================================\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Please enter the path your elden ring files are contained in and then restart this program -> ");
            filePath = Console.ReadLine();
            File.WriteAllText(fileName, filePath);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Would you like to enable or disable Elden Ring's anti cheat? (1 for on, 2 for off) -> ");
            string option = Console.ReadLine();

            if (option.Equals("1"))
            {
                EnableAntiCheat();
            }

            else if (option.Equals("2"))
            {
                DisableAntiCheat();
            }
        }
    }


    static void EnableAntiCheat()
    {
        string currentPath = File.ReadAllText(fileName);
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Scanning files in path....");
        string[] allFiles = Directory.GetFiles(currentPath);
        for (int i = 0; i < allFiles.Length; i++)
        {
            if (allFiles[i].Contains("start_protected_game_original.exe"))
            {
                validFiles[1] = allFiles[i];
            } else if (allFiles[i].Contains("start_protected_game.exe"))
            {
                validFiles[0] = allFiles[i];
            }
        }
                
        Thread.Sleep(500);
                
        Console.WriteLine("Renaming target files...");

        if (validFiles[0] != null && validFiles[1] != null)
        {
            string antiCheatExecutable = validFiles[1];
            string eldenRingExecutable = validFiles[0];


            string antiCheatRenamed = Path.Combine(Path.GetDirectoryName(antiCheatExecutable),
                        "start_protected_game.exe");
            string eldenRingRename = Path.Combine(Path.GetDirectoryName(eldenRingExecutable),
                        "eldenring.exe");
                    
            File.Move(eldenRingExecutable, eldenRingRename);
            Console.WriteLine($"Renamed: {eldenRingExecutable} -> {eldenRingRename}");
            
            File.Move(antiCheatExecutable, antiCheatRenamed);
            Console.WriteLine($"Renamed: {antiCheatExecutable} -> {antiCheatRenamed}");
            
                    
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Anti cheat has been successfully enabled!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Couldn't find the required files to rename!");
        }
    }
    

    static void DisableAntiCheat()
    {
        string currentPath = File.ReadAllText(fileName);
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Scanning files in path....");
        string[] allFiles = Directory.GetFiles(currentPath);
        for (int i = 0; i < allFiles.Length; i++)
        {
            if (allFiles[i].Contains("start_protected_game.exe"))
            {
                validFiles[0] = allFiles[i];
            } else if (allFiles[i].Contains("eldenring.exe"))
            {
                validFiles[1] = allFiles[i];
            }
        }
                
        Thread.Sleep(500);
                
        Console.WriteLine("Renaming target files...");

        if (validFiles[0] != null && validFiles[1] != null)
        {
            string antiCheatExecutable = validFiles[0];
            string eldenRingExecutable = validFiles[1];


            string antiCheatRenamed = Path.Combine(Path.GetDirectoryName(antiCheatExecutable),
                        "start_protected_game_original.exe");
            string eldenRingRename = Path.Combine(Path.GetDirectoryName(eldenRingExecutable),
                        "start_protected_game.exe");
                    
            File.Move(antiCheatExecutable, antiCheatRenamed);
            Console.WriteLine($"Renamed: {antiCheatExecutable} -> {antiCheatRenamed}");
                    
            File.Move(eldenRingExecutable, eldenRingRename);
            Console.WriteLine($"Renamed: {eldenRingExecutable} -> {eldenRingRename}");
                    
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Anti cheat has been successfully disabled!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press any key to exit!");
            Console.ReadKey();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Couldn't find the required files to rename!");
        }
    }
}