using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ImageGenerateEngine
{
	class GenerateExecution
	{
		static void Main()
		{
			var watch = System.Diagnostics.Stopwatch.StartNew();
			try
			{
				CheckForStructure();
				GenerateEngine.CreateDirectory(Config.Default.GenerateRoot);
				Console.WriteLine(Config.Default.RotationAngleIncrement);
				List<string> totalFilesList = new List<string>();
				string[] subdirectoryEntries = Directory.GetDirectories(Config.Default.Root);
				foreach (var dir in subdirectoryEntries)
				{
					string[] fileArray = Directory.GetFiles(dir, "*.png");
					totalFilesList.AddRange(fileArray);
				}
				totalFilesList.OrderBy(model => model);
				List<IEnumerable<string>> listOfLists = new List<IEnumerable<string>>();
				for (int i = 0; i < totalFilesList.Count(); i += 20)
				{
					listOfLists.Add(totalFilesList.Skip(i).Take(20));
				}
				Parallel.ForEach(listOfLists, obj =>
				{
					foreach (var item in obj)
					{
						GenerateEngine.CreateDirectory(item.Replace(Config.Default.Root, Config.Default.GenerateRoot).Replace(".png", ""));
						GenerateEngine.GeneratePNG(item);
					}
				});
				watch.Stop();
				Console.WriteLine($"Total Execution Time: {ConsoleUtility.ConvertToTime(TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds))}.");
			}
			catch (Exception)
			{
				Console.WriteLine("An error happend when attemp to generate picture. Please check the cofig file again.");
				watch.Stop();
				Console.WriteLine($"Total Execution Time: {ConsoleUtility.ConvertToTime(TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds))}.");
			}
		}

		static void CheckForStructure()
		{
			try
			{
				//Get all subdirectories
				List<string> totalFilesList = new List<string>();
				string[] subdirectoryEntries = Directory.GetDirectories(Config.Default.Root);
			}
			catch (DirectoryNotFoundException)
			{
				Console.WriteLine("Cannot found the directory " + Config.Default.Root + ". Please make that sure you provide correct path.");
				return;
			}
		}
	}
}
