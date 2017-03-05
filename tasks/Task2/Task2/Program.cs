using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2

{
    public class Article
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

        public Article(string title, string media, double size, double sitePrice)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title must not be empty.", nameof(title));
            if (string.IsNullOrWhiteSpace(media)) throw new ArgumentException("Media must not be empty.", nameof(media));
            if (size < 0) throw new ArgumentOutOfRangeException("Size must not be negative");

            Title = title;
            Media = media;
            Size = size;
            SitePrice = sitePrice;
            CommercialValue=CalcCommercialValue(sitePrice, size);

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

        public double CommercialValue
        {
            get;
        }
     
        public double CalcCommercialValue(double SitePrice, double size)
        {
            return SitePrice * size;
        }


    }


    class Program
    {
        static void Main(string[] args)
        {
            Article article1 = new Article("Wiener Melange", "Kronen Zeitung", 0.33, 32000.4);
            Article article2 = new Article("Proteste um Eisenbahnbrücke in Linz", "Oberösterreichische Nachrichten", 0.25, 19696);
            Article article3 = new Article("Polizeireporter", "Vorarlberger Nachrichten", 0.125, 10485);

            Console.WriteLine("Der Artikel {0} in der {1} nimmt {2} einer ganzen Seite ein.", article1.Title, article1.Media, article1.Size);
            Console.WriteLine("Der Werbewert für Artikel 1 beträgt: {0} Euro, für Artikel 2: {1} Euro und für Artikel 3: {2} Euro", article1.CommercialValue, article2. CommercialValue, article3.CommercialValue);
            

        }
    }
}
