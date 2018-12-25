#region Copyright Notice
/*
 * gitter - VCS repository management tool
 * Copyright (C) 2013  Popovskiy Maxim Vladimirovitch <amgine.gitter@gmail.com>
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion

namespace gitter.Git.Gui.Controls
{
	using System;
	using System.ComponentModel;
	using System.Windows.Forms;

	using Resources = gitter.Git.Gui.Properties.Resources;

	[ToolboxItem(false)]
	public sealed class BranchMenu : ContextMenuStrip
	{
		public BranchMenu(BranchBase branch)
		{
			Verify.Argument.IsValidGitObject(branch, nameof(branch));

			Branch = branch;

			Items.Add(GuiItemFactory.GetViewReflogItem<ToolStripMenuItem>(Branch));
			Items.Add(GuiItemFactory.GetViewTreeItem<ToolStripMenuItem>(Branch));

			Items.Add(new ToolStripSeparator()); // interactive section

			Items.Add(GuiItemFactory.GetCheckoutRevisionItem<ToolStripMenuItem>(Branch, "{0} '{1}'"));
			Items.Add(GuiItemFactory.GetResetHeadHereItem<ToolStripMenuItem>(Branch));
			Items.Add(GuiItemFactory.GetRebaseHeadHereItem<ToolStripMenuItem>(Branch));
			Items.Add(GuiItemFactory.GetMergeItem<ToolStripMenuItem>(Branch));
			if(!branch.IsRemote)
			{
				Items.Add(GuiItemFactory.GetRenameBranchItem<ToolStripMenuItem>((Branch)Branch, "{0}"));
			}
			Items.Add(GuiItemFactory.GetRemoveBranchItem<ToolStripMenuItem>(Branch));
			if(!branch.IsRemote)
			{
				lock(branch.Repository.Remotes.SyncRoot)
				{
					if(branch.Repository.Remotes.Count != 0)
					{
						Items.Add(new ToolStripSeparator());
						var pushTo = new ToolStripMenuItem(Resources.StrPushTo, CachedResources.Bitmaps["ImgPush"]);
						foreach(var remote in branch.Repository.Remotes)
						{
							pushTo.DropDownItems.Add(GuiItemFactory.GetPushBranchToRemoteItem<ToolStripMenuItem>((Branch)branch, remote));
						}
						Items.Add(pushTo);
					}
				}
			}
			else
			{

			}
			Items.Add(new ToolStripSeparator()); // copy to clipboard section

			var item = new ToolStripMenuItem(Resources.StrCopyToClipboard);
			item.DropDownItems.Add(GuiItemFactory.GetCopyToClipboardItem<ToolStripMenuItem>(Resources.StrName, Branch.Name));
			item.DropDownItems.Add(GuiItemFactory.GetCopyToClipboardItem<ToolStripMenuItem>(Resources.StrFullName, Branch.FullName));
			item.DropDownItems.Add(GuiItemFactory.GetCopyHashToClipboardItem<ToolStripMenuItem>(Resources.StrPosition, Branch.Revision.HashString));

			Items.Add(item);

			Items.Add(new ToolStripSeparator());

			Items.Add(GuiItemFactory.GetCreateBranchItem<ToolStripMenuItem>(Branch));
			Items.Add(GuiItemFactory.GetCreateTagItem<ToolStripMenuItem>(Branch));
		}

		public BranchBase Branch { get; }
	}
}
