﻿namespace Spark.Sitecore.Seo.Controls
{
	/// <summary>
	/// A Rendering that sets the status of the response to "404: Not Found".
	/// </summary>
	// ReSharper disable RedundantNameQualifier
	public class HttpResponseNotFound : Spark.WebForms.Controls.HttpResponseNotFound
	// ReSharper restore RedundantNameQualifier
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="HttpResponseNotFound"/> class.
		/// </summary>
		public HttpResponseNotFound()
		{
			this.StatusCode = 404;
			this.StatusDescription = "Page not found";
		}

		/// <summary>
		/// Overrides the default control output to ensure we don't 404 a Page Editor
		/// or Preview session.
		/// </summary>
		/// <param name="output">
		/// The output.
		/// </param>
		protected override void Render(System.Web.UI.HtmlTextWriter output)
		{
			if (global::Sitecore.Context.PageMode.IsNormal)
			{
				base.Render(output);
			}
		}
	}
}
