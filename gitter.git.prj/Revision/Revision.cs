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

namespace gitter.Git
{
	using System;
	using System.Collections.Generic;

	using gitter.Git.AccessLayer;

	public sealed partial class Revision : GitObject, IRevisionPointer
	{
		#region Data

		private Hash _treeHash;
		private User _author;

		#endregion

		#region .ctor

		internal Revision(Repository repository, Hash hash)
			: base(repository)
		{
			Parents    = new RevisionParentsCollection();
			References = new RevisionReferencesCollection();
			Hash       = hash;
			HashString = hash.ToString();
		}

		#endregion

		public void Load()
		{
			var revisionData = Repository.Accessor.QueryRevision.Invoke(
				new QueryRevisionParameters(Hash));
			ObjectFactories.UpdateRevision(this, revisionData);
		}

		#region Properties

		public RevisionParentsCollection Parents { get; }

		public RevisionReferencesCollection References { get; }

		public bool IsCurrent => Repository.Head.Revision == this;

		public bool IsLoaded { get; set; }

		#endregion

		#region Commit Attributes

		public Hash Hash { get; }

		public string HashString { get; }

		public Hash TreeHash
		{
			get { return _treeHash; }
			internal set
			{
				if(!IsLoaded)
				{
					_treeHash = value;
					TreeHashString = value.ToString();
				}
				else
				{
					if(_treeHash != value)
					{
						_treeHash = value;
						TreeHashString = value.ToString();
					}
				}
			}
		}

		public string TreeHashString { get; private set; }

		public User Author
		{
			get { return _author; }
			internal set
			{
				if(!IsLoaded)
				{
					_author = value;
				}
				else
				{
					if(_author != value)
					{
						_author = value;
					}
				}
			}
		}

		public DateTime AuthorDate { get; internal set; }

		public User Committer { get; internal set; }

		public DateTime CommitDate { get; internal set; }

		public string Subject { get; internal set; }

		public string Body { get; internal set; }

		#endregion

		#region IRevisionPointer Members

		ReferenceType IRevisionPointer.Type => ReferenceType.Revision;

		string IRevisionPointer.Pointer => HashString;

		string IRevisionPointer.FullName => HashString;

		bool IRevisionPointer.IsDeleted => false;

		Revision IRevisionPointer.Dereference() => this;

		#endregion

		public override string ToString()
		{
			return string.Format("{0}: {1}", Hash.ToString(7), Subject);
		}
	}
}
