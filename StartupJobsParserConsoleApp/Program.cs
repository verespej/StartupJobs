﻿using System;
using System.Collections.Generic;
using StartupJobsParser;
using System.IO;

namespace StartupJobsParserConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //string indexDirPath = Directory.GetCurrentDirectory() + "\\index\\";
            ISjpIndex index = new SjpLocalDiskIndex(Path.GetFullPath(".\\index\\"));

            List<ISjpScraper> scrapers = new List<ISjpScraper>();
            //scrapers.Add(new SjpApptioScraper(".\\data\\apptio\\", index));
            //scrapers.Add(new SjpRedfinScraper(".\\data\\redfin\\", index));
            //scrapers.Add(new SjpExtraHopScraper(".\\data\\extrahop\\", index));
            //scrapers.Add(new SjpIndochinoScraper(".\\data\\indochino\\", index));
            scrapers.Add(new SjpSmartsheetScraper(".\\data\\smartsheet\\", index));

            foreach (ISjpScraper scraper in scrapers)
            {
                scraper.Scrape();
            }

            //foreach (JobDescription jd in index.FindJds("java"))
            //{
            //    SjpLogger.Log("Found: " + jd.Company + " - " + jd.Title);
            //}

            Console.WriteLine("Done. Press <enter>.");
            Console.ReadLine();
        }
    }
}
