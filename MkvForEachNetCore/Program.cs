using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MkvForEachNetCore
{
    internal class Program
    {
        //cmd = r"C:/Users/steff/Downloads/mkvtoolnix-64bit-10.0.0\mkvmerge.exe --ui-language de --output 'D:\VID_20170403_085827.mkv' --language 0:und --compression 0:zlib --language 1:und 'D:\Temp\TestVideos\VID_20170403_085827.mp4' --track-order 0:0,0:1"
        public static readonly string[] wantedFileEndings = new string[]{".avi",".mp4"};


        private static void Main(string[] args)
        {
            string dir = null;
            if(args.Length > 0)
            {
                for(var index= 0; index < args.Length; index++)
                {
                    var arg = args[index];

                    if(arg == "-dir")
                    {
                        dir = args[index+1];
                    }

                }
            }

            var convertableFiles = GetConvertableFiles(dir);
            foreach(var convertableFile in convertableFiles)
            {
                var s = @"C:/Users/steff/Downloads/mkvtoolnix-64bit-10.0.0\mkvmerge.exe --ui-language de --output '"+convertableFile.TrimEnd(Path.GetExtension(convertableFile).ToCharArray())+".mkv' --language 0:und --compression 0:zlib --language 1:und '"+convertableFile+"' --track-order 0:0,0:1";
                var strCmdText = $@"/C {s}";
                Process.Start("Powershell.exe", strCmdText);
            }

           

            
        }

        public static List<string> GetConvertableFiles(string path)
        {
            var strings = new List<string>();
            if(!string.IsNullOrEmpty(path)&&Directory.Exists(path))
            {

                var files = Directory.GetFiles(path);
                foreach(var file in files)
                {
                    if (wantedFileEndings.Contains(Path.GetExtension(file)))
                    {
                        strings.Add(file);
                    }
                }

            }
            else
            {
                throw new DirectoryNotFoundException("Use -dir to set target directory!");
            }
            return strings;
        }
    }
}
