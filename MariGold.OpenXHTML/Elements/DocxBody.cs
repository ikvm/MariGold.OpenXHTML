﻿namespace MariGold.OpenXHTML
{
	using System;
	using MariGold.HtmlParser;
	using DocumentFormat.OpenXml;
	using DocumentFormat.OpenXml.Wordprocessing;
	
	internal sealed class DocxBody : DocxElement
	{
		private OpenXmlElement body;
		
		private void ProcessBody(HtmlNode node)
		{
			while (node != null)
			{
				if (node.IsText)
				{
					AppendToParagraphWithRun(node, body, new Text(node.InnerHtml));
				}
				else
				{
					ProcessChild(node, body);
				}
				
				node = node.Next;
			}
		}
		
		public DocxBody(IOpenXmlContext context)
			: base(context)
		{
		}
		
		internal override bool CanConvert(HtmlNode node)
		{
			return string.Compare(node.Tag, "body", true) == 0;
		}
		
		internal override void Process(HtmlNode node, OpenXmlElement parent)
		{
			body = context.Document.AppendChild(new Body());
			
			//If the node is body tag, find the first children to process
			if (CanConvert(node))
			{
				if (!node.HasChildren)
				{
					//Nothing to process. Just return from here.
					return;
				}
				
				node = node.Children[0];
			}
			
			ProcessBody(node);
		}
	}
}