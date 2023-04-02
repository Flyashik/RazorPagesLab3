using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace RazorPagesLab3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public interface IService<T>
    {
        IEnumerable<T> getContent(string filename);
    }

    public class Service<T> : IService<T>
    {
        public IEnumerable<T> getContent(string filename)
        {
            var streamReader = new StreamReader(filename);

            string json = streamReader.ReadToEnd();
            return JsonSerializer.Deserialize<T[]>(json) ?? new T[] { };
        }
    }

    [Serializable]
    public class Product
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
    }

    //[Serializable]
    //public class 
}
