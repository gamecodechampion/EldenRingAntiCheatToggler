using System.Collections;
using System.IO;


class Program
{
    static string[] validFiles = new string[2];
    static string fileName = "eldenringpath.txt";
    static string filePath;
    private static string currentPath;
    
    
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
            int option = Convert.ToInt32(Console.ReadLine());
            
            
            if (option == 1)
            {
                if (!ValidateFiles())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The contents of your eldenringpath.txt file seems to be invalid.");
                    Console.Write("Please re-enter the path that your Elden Ring files are contained in, you will have to restart the program upon doing this -> ");
                    filePath = Console.ReadLine();
                    File.WriteAllText(fileName, filePath);
                }
                else
                {
                    EnableAntiCheat();
                }
            }

            else if (option == 2)
            {
                if (!ValidateFiles())
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("The contents of your eldenringpath.txt file seem to be invalid.");
                    Console.Write("Please re-enter the path that your Elden Ring files are contained in, you will have to restart the program upon doing this -> ");
                    filePath = Console.ReadLine();
                    File.WriteAllText(fileName, filePath);
                }
                else
                {
                    DisableAntiCheat();
                }
            }
        }
    }


    static bool ValidateFiles()
    {
        try
        {

            currentPath = File.ReadAllText(fileName);

            string[] allFiles = Directory.GetFiles(currentPath);

            bool hasStartProtected = allFiles.Any(f => Path.GetFileName(f) == "start_protected_game.exe");
            bool hasEldenRing = allFiles.Any(f => Path.GetFileName(f) == "eldenring.exe");
            bool hasStartProtectedOriginal =
                allFiles.Any(f => Path.GetFileName(f) == "start_protected_game_original.exe");

            bool enabledState = hasStartProtected && hasEldenRing;
            bool disabledState = hasStartProtected && hasStartProtectedOriginal;

            return enabledState || disabledState;
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Validation failed: {e.Message}");
            return false;
        }
    }


    static void EnableAntiCheat()
    {
        currentPath = File.ReadAllText(fileName);
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
            MessageBox.Show("Your anti cheat is already enabled!", "Notice", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
        }
    }
    

    static void DisableAntiCheat()
    {
        currentPath = File.ReadAllText(fileName);
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
            MessageBox.Show("Your anti cheat is already disabled!", "Notice", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
        }
    }
}