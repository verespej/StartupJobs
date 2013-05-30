﻿using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;

namespace StartupJobsParser
{
    public class SjpQumuloScraper : SjpScraper
    {
        private Uri _defaultUri = new Uri("http://www.qumulo.com/#/qumulo_jobs_eng");
        public override string CompanyName { get { return "Qumulo"; } }
        public override Uri DefaultUri
        {
            get { return _defaultUri; }
        }

        public SjpQumuloScraper(string storageDirPath, ISjpIndex index)
            : base(storageDirPath, index)
        {
        }

        protected override IEnumerable<JobDescription> GetJds(Uri uri)
        {
            HtmlDocument doc = SjpUtils.GetHtmlDoc(uri);
            foreach (HtmlNode jdNode in doc.DocumentNode.SelectNodes("//div[@data-position]"))
            {
                yield return GetSkytapJd(jdNode, uri);
            }
        }

        private JobDescription GetSkytapJd(HtmlNode jdNode, Uri uri)
        {
            string title = jdNode.Attributes["data-position"].Value;
            string description = jdNode.SelectSingleNode("div[@class='jdbg']/div[@class='body']/span").InnerHtml;

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