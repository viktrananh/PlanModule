using DevComponents.Tree;
using DevComponents.Tree.Display;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLAN_MODULE.DTO.Sales
{
    internal class RedNodeRenderer : NodeSystemRenderer
    {
		public override void DrawNodeBackground(NodeRendererEventArgs e)
		{
			// We'll reuse some functionality from ElementStyle implementation so we get background as defined by style
			GraphicsPath path = ElementStyleDisplay.GetBackgroundPath(e.Style, e.NodeBounds);

			if (e.Node.IsMouseOver)
			{
				using (SolidBrush brush = new SolidBrush(Color.Orange))
					e.Graphics.FillPath(brush, path);
			}
			else
			{
				using (SolidBrush brush = new SolidBrush(Color.Red))
					e.Graphics.FillPath(brush, path);
			}

			// Allow any events to fire, no need to call if you base.DrawNodeBackground is called
			base.OnRenderNodeBackground(e);
		}
	}
}
