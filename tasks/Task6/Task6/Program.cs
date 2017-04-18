using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;

namespace Task6

{

    public interface BaseData
    {
        string Title
        {
            get;
        }

        string Media
        {
            get;
        }

        int Tonality
        {
            get;
        }

    }

    public class Article : BaseData
    {
        private double _sitePrice;

        public double SitePrice
        {
            get
            {
                return _sitePrice;
            }

            set
            {
                if (value < 0) throw new Exception("Site price must not be negative.");
                _sitePrice = value;
            }
        }

        public Article(string title, string media, double size, double sitePrice, int tonality)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title must not be empty.", nameof(title));
            if (string.IsNullOrWhiteSpace(media)) throw new ArgumentException("Media must not be empty.", nameof(media));
            if (size < 0) throw new ArgumentOutOfRangeException("Size must not be negative");
            if (tonality < -1 || tonality > 1) throw new ArgumentOutOfRangeException("Tonality must be positive (1), negative (-1) or neutral (0)");

            Title = title;
            Media = media;
            Size = size;
            SitePrice = sitePrice;
            Tonality = tonality;
            CommercialValue = CalcCommercialValue(sitePrice, size);

        }

        public string Title
        {
            get;
        }

        public string Media
        {
            get;
        }

        public double Size
        {
            get;
        }

        public int Tonality
        {
            get;
        }

        public double CommercialValue
        {
            get;
        }

        public double CalcCommercialValue(double SitePrice, double size)
        {
            return SitePrice * size;
        }


    }

    public class Posting : BaseData
    {

        public Posting(string title, string media, string username, int tonality)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title must not be empty.", nameof(title));
            if (string.IsNullOrWhiteSpace(media)) throw new ArgumentException("Media must not be empty.", nameof(media));
            if (tonality < -1 || tonality > 1) throw new ArgumentOutOfRangeException("Tonality must be positive (1), negative (-1) or neutral (0)");
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username must not be empty.", nameof(username));


            Title = title;
            Media = media;
            Username = username;
            Tonality = tonality;
        }

        public string Title
        {
            get;
        }

        public string Media
        {
            get;
        }

        public int Tonality
        {
            get;
        }

        public string Username
        {
            get;
        }

    }

    public static class PushExampleWithSubject
    {
        public static void Run()
        {
            var source = new Subject<int>();

            source
                .Sample(TimeSpan.FromSeconds(1.0))
                .Subscribe(x => Console.WriteLine($"received {x}"))
                ;

            var t = new Thread(() =>
            {
                var i = 0;
                while (true)
                {
                    Thread.Sleep(250);
                    source.OnNext(i);
                    Console.WriteLine($"sent {i}");
                    i++;
                }
            });
            t.Start();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                var mentioned = new BaseData[]
                {
                    new Article("Leberkas Pepi eröffnet am Wiener Hauptbahnhof", "Heute", 0.25, 23827, 1),
                    new Posting("Endlich guter Leberkäse in Wien #Leberkas Pepi", "twitter.com", "LeberkasLover", 1),
                    new Posting("Hier stinkt's!!1! Danke #Leberkas Pepi :-(", "twitter.com", "nofun", -1)

                };

                foreach (var y in mentioned)
                {
                    Console.WriteLine($"{y.Media}: {y.Title} mit Tonalität {y.Tonality}");
                }


                Console.WriteLine(JsonConvert.SerializeObject(mentioned, Formatting.Indented));

                var settings = new JsonSerializerSettings() { Formatting = Formatting.Indented, TypeNameHandling = TypeNameHandling.Auto };
                Console.WriteLine(JsonConvert.SerializeObject(mentioned, settings));

                var text = JsonConvert.SerializeObject(mentioned, settings);
                var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var filename = Path.Combine(desktop, "mentioned.json");
                File.WriteAllText(filename, text);

                var textFromFile = File.ReadAllText(filename);
                var itemsFromFile = JsonConvert.DeserializeObject<BaseData[]>(textFromFile, settings);
                foreach (var y in itemsFromFile)
                {
                    Console.WriteLine($"{y.Media}: {y.Title} mit Tonalität {y.Tonality}");
                }

                PushExampleWithSubject.Run();

            }
            catch (Exception e)
            {
                Console.WriteLine("Fehler! " + e.Message);
                return;
            }
        }
    }
}
