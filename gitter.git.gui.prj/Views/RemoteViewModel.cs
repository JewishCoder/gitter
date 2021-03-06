﻿#region Copyright Notice
/*
 * gitter - VCS repository management tool
 * Copyright (C) 2014  Popovskiy Maxim Vladimirovitch <amgine.gitter@gmail.com>
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

namespace gitter.Git.Gui.Views
{
	public class RemoteViewModel
	{
		#region .ctor

		public RemoteViewModel(Remote remote)
		{
			Remote = remote;
		}

		#endregion

		#region Properties

		public Remote Remote { get; }

		#endregion

		#region Methods

		public override int GetHashCode()
		{
			return Remote != null ? Remote.GetHashCode() : 0;
		}

		public override bool Equals(object obj)
		{
			if(obj == null)
			{
				return false;
			}
			var other = obj as RemoteViewModel;
			if(other == null)
			{
				return false;
			}
			return object.Equals(Remote, other.Remote);
		}

		#endregion
	}
}
