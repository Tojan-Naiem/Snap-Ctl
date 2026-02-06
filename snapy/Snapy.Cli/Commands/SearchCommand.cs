
public class SearchCommand : ISnapyCommand
{
        public void Execute(string[] args)
    {
        if (args.Length !=2)
        {
            Console.WriteLine("Use the required structure!");
            return ;
        }
        string searchData=args[1];
      
        SqliteRepository.SearchTextFromDBS(searchData);

    }

}