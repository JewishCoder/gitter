﻿namespace gitter.Git
{
	using System;
	using System.Collections.Generic;

	using gitter.Framework;

	/// <summary>Describes an external merge tool used by 'git mergetool' command.</summary>
	public sealed class MergeTool : INamedObject
	{
		#region Data

		private readonly string _name;
		private readonly string _url;
		private readonly bool _supportsWin;
		private readonly bool _suuportsLinux;

		#endregion

		#region Static

		public static readonly MergeTool kdiff3;
		public static readonly MergeTool tkdiff;
		public static readonly MergeTool meld;
		public static readonly MergeTool xxdiff;
		public static readonly MergeTool emerge;
		public static readonly MergeTool vimdiff;
		public static readonly MergeTool gvimdiff;
		public static readonly MergeTool ecmerge;
		public static readonly MergeTool diffuse;
		public static readonly MergeTool tortoisemerge;
		public static readonly MergeTool opendiff;
		public static readonly MergeTool p4merge;
		public static readonly MergeTool araxis;

		private static readonly Dictionary<string, MergeTool> _tools;

		public static MergeTool GetByName(string name)
		{
			return _tools[name];
		}

		public static MergeTool GetCreateByName(string name)
		{
			MergeTool res;
			if(!_tools.TryGetValue(name, out res))
			{
				res = new MergeTool(name);
			}
			return res;
		}

		public static IEnumerable<MergeTool> KnownTools
		{
			get { return _tools.Values; }
		}

		public static int KnownToolsCount
		{
			get { return _tools.Count; }
		}

		#endregion

		#region .ctor

		static MergeTool()
		{
			_tools = new Dictionary<string, MergeTool>(13)
			{
				{ "kdiff3",			kdiff3			= new MergeTool("kdiff3",			@"http://sourceforge.net/projects/kdiff3/files/", true, true) },
				{ "tkdiff",			tkdiff			= new MergeTool("tkdiff",			@"http://sourceforge.net/projects/tkdiff/files/", true, true) },
				{ "meld",			meld			= new MergeTool("meld",				@"http://ftp.gnome.org/pub/gnome/sources/meld/", false, true) },
				{ "xxdiff",			xxdiff			= new MergeTool("xxdiff",			@"http://sourceforge.net/projects/xxdiff/files/", false, true) },
				{ "emerge",			emerge			= new MergeTool("emerge",			@"", false, true) },
				{ "vimdiff",		vimdiff			= new MergeTool("vimdiff",			@"", false, true) },
				{ "gvimdiff",		gvimdiff		= new MergeTool("gvimdiff",			@"", false, true) },
				{ "ecmerge",		ecmerge			= new MergeTool("ecmerge",			@"http://www.elliecomputing.com/Download/download_form.asp", true, true) },
				{ "diffuse",		diffuse			= new MergeTool("diffuse",			@"http://diffuse.sourceforge.net/download.html", true, true) },
				{ "tortoisemerge",	tortoisemerge	= new MergeTool("tortoisemerge",	@"http://tortoisesvn.net/downloads", true, false) },
				{ "opendiff",		opendiff		= new MergeTool("opendiff",			@"", false, true) },
				{ "p4merge",		p4merge			= new MergeTool("p4merge",			@"http://www.perforce.com/perforce/downloads/index.html", true, true) },
				{ "araxis",			araxis			= new MergeTool("araxis",			@"http://www.araxis.com/merge/index.html", true, false) },
			};
		}

		internal MergeTool(string name)
		{
			_name = name;
		}

		private MergeTool(string name, string url, bool supportsWin, bool supportsLinux)
		{
			_name = name;
			_url = url;
			_supportsWin = supportsWin;
			_suuportsLinux = supportsLinux;
		}

		#endregion

		public string Name
		{
			get { return _name; }
		}

		public string DownloadUrl
		{
			get { return _url; }
		}

		public bool SupportsWin
		{
			get { return _supportsWin; }
		}

		public bool SupportsLinux
		{
			get { return _suuportsLinux; }
		}

		/// <summary>Returns a <see cref="System.String"/> that represents this instance.</summary>
		/// <returns>A <see cref="System.String"/> that represents this instance.</returns>
		public override string ToString()
		{
			return _name;
		}
	}
}