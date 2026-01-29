using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
public class Program
{
    public static void Main(string[] args)
    {
        string screenshotPath=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"Screenshots");
        if (!Directory.Exists(screenshotPath)){
            Directory.CreateDirectory(screenshotPath);
            Console.WriteLine("Folder Created at : "+screenshotPath);
        }
        else Console.WriteLine("Folder Exists at : "+screenshotPath);
        Console.WriteLine("Access Done to the Screenshots Folder");
        if(args.Length>0)
        {
            if (args[1] == "organize")
            {
                
            }
        }
        
    }
    

}