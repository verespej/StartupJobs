﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;

namespace StartupJobsParser
{
    public class SjpSkytapScraper : SjpScraper
    {
        private Uri _defaultUri = new Uri("http://www.skytap.com/company/careers/?/about-us/careers/index.php");
        public override string CompanyName { get { return "Skytap"; } }
        public override Uri DefaultUri
        {
            get { return _defaultUri; }
        }

        public SjpSkytapScraper(string storageDirPath, ISjpIndex index)
            : base(storageDirPath, index)
        {
        }

        protected override IEnumerable<JobDescription> GetJds(Uri uri)
        {
            HtmlDocument doc = SjpUtils.GetHtmlDoc(uri);
            foreach (HtmlNode jdLink in doc.DocumentNode.SelectNodes("//a[starts-with(@href, 'http://www.skytap.com/company/careers/')]"))
            {
                yield return GetSkytapJd(new Uri(jdLink.Attributes["href"].Value));
            }
        }

        private JobDescription GetSkytapJd(Uri uri)
        {
            HtmlDocument doc = SjpUtils.GetHtmlDoc(uri);
            HtmlNode jdNode = doc.DocumentNode.SelectSingleNode("//div[@id='content']/div/div[@class='column']");

            HtmlNode titleNode = jdNode.SelectSingleNode("h1");
            string title = titleNode.InnerText;

            titleNode.ParentNode.RemoveChild(titleNode, false);
            string description = jdNode.InnerHtml;

            return new JobDescription()
            {
                SourceUri = uri.AbsoluteUri,
                Company = CompanyName,
                Title = WebUtility.HtmlDecode(title).Trim(),
                Location = "Seattle, WA",
                FullTextDescription = WebUtility.HtmlDecode(description).Trim(),
                FullHtmlDescription = description
            };
        }
    }
}