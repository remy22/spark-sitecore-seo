﻿namespace Spark.Sitecore.Seo.Sitemaps
{
	using System;
	using System.Web;

	using global::Sitecore.Data.Items;
	using global::Sitecore.Links;
	using global::Sitecore.Sites;

	/// <summary>
	/// Represents a candidate element for the sitemap.xml file.
	/// </summary>
	public class DefaultSitemapNode : SitemapNode<Item>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultSitemapNode"/> class.
		/// </summary>
		/// <param name="item">The item to be considered for a node.</param>
		public DefaultSitemapNode(Item item)
			: base(item)
		{
			// shut up Stylecop
		}

		/// <summary>
		/// Determines whether the item object (which is assumed to be a page) is to be represented
		/// in navigation presented on page (ex: a sitemap or breadcrumbs).
		/// </summary>
		/// <param name="item">An object representing a Sitecore Item.</param>
		/// <returns><c>true</c> if the object is intended to be represented in various page navigation scenarios.</returns>
		protected override bool CheckIsListedInNavigation(Item item)
		{
			return item.Visualization.Layout != null;
		}

		/// <summary>
		/// Determines whether the item object represents a web page on the site and therefore
		/// has presentation details.
		/// </summary>
		/// <param name="item">An object representing a Sitecore Item.</param>
		/// <returns><c>true</c> if the object is a page with presentation details given the current context.</returns>
		protected override bool CheckIsPage(Item item)
		{
			if (item.Visualization.Layout != null)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Determines whether the item object (which is assumed to be a page) is to be indexed
		/// by search engines.
		/// </summary>
		/// <param name="item">An object representing a Sitecore Item.</param>
		/// <returns><c>true</c> if the object's presentation is intended to be crawled by search engines.</returns>
		protected override bool CheckShouldIndex(Item item)
		{
			if (item.Visualization.Layout != null)
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Determines the full URL (hostname &amp; language definition) of the item object (which is assumed to be a page).
		/// </summary>
		/// <param name="item">An object representing a Sitecore Item.</param>
		/// <returns>The absolute URL to the object, including protocol.</returns>
		protected override string ResolveAbsoluteUrl(Item item)
		{
			var options = LinkManager.GetDefaultUrlOptions();
			options.AlwaysIncludeServerUrl = true;
			var context = HttpContext.Current;
			options.Site = SiteContextFactory.GetSiteContext(context.Request.Url.Host, context.Request.Url.LocalPath, context.Request.Url.Port);

			if (item.Language.CultureInfo.Name.Equals(options.Site.Language, StringComparison.OrdinalIgnoreCase))
			{
				options.LanguageEmbedding = LanguageEmbedding.Never;
			}
			else
			{
				options.LanguageEmbedding = LanguageEmbedding.AsNeeded;
			}

			return LinkManager.GetItemUrl(item, options);
		}

		/// <summary>
		/// Determines the anticipated content/presentation change frequency of the item object (which is assumed to be a page).
		/// </summary>
		/// <param name="item">An object representing a Sitecore Item.</param>
		/// <returns>The change frequency.</returns>
		protected override ChangeFrequency ResolveChangeFrequency(Item item)
		{
			return ChangeFrequency.Monthly;
		}

		/// <summary>
		/// Determines the crawling priority of the item object (which is assumed to be a page) using a scale
		/// from 0.0 to 1.0 where 1.0 is the highest priority and 0.0 is the lowest priority. 
		/// 0.5 is the expected neutral value.
		/// </summary>
		/// <param name="item">An object representing a Sitecore Item.</param>
		/// <returns>The numeric prioritization value.</returns>
		protected override decimal ResolvePriority(Item item)
		{
			return 0.5M;
		}

		/// <summary>
		/// Determines the last date that the item object was modified.
		/// </summary>
		/// <param name="item">An object representing a Sitecore Item.</param>
		/// <returns>The last date of modification.</returns>
		protected override DateTime ResolveUpdatedDate(Item item)
		{
			return item.Statistics.Updated;
		}

		/// <summary>
		/// Required by the base SitemapNode class, but not used in this implementation.
		/// </summary>
		/// <param name="item">The Item to convert.</param>
		/// <returns>The same item.</returns>
		protected override Item Convert(Item item)
		{
			return item;
		}
	}
}
