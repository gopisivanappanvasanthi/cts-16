using Sitecore.Data.Items;
using Sitecore.Shell.Framework.Commands;
using Sitecore.Web.UI.Sheer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cts.Feature.Common.ContentEditorCustom
{
    public class ChildCount : Command
    {
        public override void Execute(CommandContext context)
        {
            Item commandContextItem = context.Items[0];
            if(!(commandContextItem is null))
            {
                if (commandContextItem.HasChildren)
                {
                    SheerResponse.Alert($"The child item count for the selected item is {commandContextItem.GetChildren().Count}");
                }
                else
                {
                    SheerResponse.Alert($"There is no child item available for the selected item");
                }
            }
            
        }
    }
}