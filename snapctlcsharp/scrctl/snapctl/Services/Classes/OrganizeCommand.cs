public class OrganizeCommand : ICommand
{
    public void Execute(string args)
    {
        string screenshotPath=args;
        CreateDirectory(screenshotPath,"person");

        Console.WriteLine(args);
    }
    public void CreateDirectory(string path,string dirName)
    {
        string newDir=path+"/"+dirName;
        if (Directory.Exists(newDir))
        {
            Console.WriteLine("Directory "+dirName+" already exists");
            return;
        }
        DirectoryInfo di=Directory.CreateDirectory(newDir);
        if (!Directory.Exists(newDir))
        {
            Console.WriteLine("Directory does not exists");
            return;
        }
        Console.WriteLine("Successfully created Directory "+dirName+" !!");

        
    }

}