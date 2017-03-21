using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Task4
{
    [TestFixture]

    class Test
    {
        [Test]
        public void CanCreateArticle()
        {
            var x = new Article("Leberkas Pepi eröffnet am Wiener Hauptbahnhof", "Heute", 0.25, 23827, 1);

            Assert.IsTrue(x.Title == "Leberkas Pepi eröffnet am Wiener Hauptbahnhof");
            Assert.IsTrue(x.Media == "Heute");
            Assert.IsTrue(x.Size == 0.25);
            Assert.IsTrue(x.SitePrice == 23827);
            Assert.IsTrue(x.Tonality == 1);
        }

        [Test]
        public void CannotCreateArticleWithEmptyTitle1()
        {
            Assert.Catch(() =>
            {
                var x = new Article(null, "Heute", 0.25, 23827, 1);
            });
        }

        [Test]
        public void CannotCreateArticleWithEmptyTitle2()
        {
            Assert.Catch(() =>
            {
                var x = new Article("", "Heute", 0.25, 23827, 1);
            });
        }

        [Test]
        public void CannotCreateArticleWithEmptyMedia1()
        {
            Assert.Catch(() =>
            {
                var x = new Article("Leberkas Pepi eröffnet am Wiener Hauptbahnhof", null, 0.25, 23827, 1);
            });
        }

        [Test]
        public void CannotCreateArticleWithEmptyMedia2()
        {
            Assert.Catch(() =>
            {
                var x = new Article("Leberkas Pepi eröffnet am Wiener Hauptbahnhof", "", 0.25, 23827, 1);
            });
        }

        [Test]
        public void CannotCreateArticleWithNegativeSize()
        {
            Assert.Catch(() =>
            {
                var x = new Article("Leberkas Pepi eröffnet am Wiener Hauptbahnhof", "Heute", -1, 23827, 1);
            });
        }

        [Test]
        public void CannotCreateArticleWithNegativeSitePrice()
        {
            Assert.Catch(() =>
            {
                var x = new Article("Leberkas Pepi eröffnet am Wiener Hauptbahnhof", "Heute", 0.25, -1, 1);
            });
        }

        [Test]
        public void CanCalculateCommercialValueWithPrice()
        {
            var x = new Article("Leberkas Pepi eröffnet am Wiener Hauptbahnhof", "Heute", 0.25, 23827, 1);
            x.CalcCommercialValue(23827, 0.25);

            Assert.IsTrue(x.SitePrice == 23827);
            Assert.IsTrue(x.Size == 0.25);
        }
    }
}
