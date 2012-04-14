﻿namespace gitter.Git.AccessLayer.CLI
{
	using System;
	using System.Collections.Generic;

	/// <summary>Add file contents to the index.</summary>
	public sealed class AddCommand : Command
	{
		/// <summary>Don't actually add the file(s), just show if they exist.</summary>
		public static CommandArgument DryRun()
		{
			return CommandArgument.DryRun();
		}

		/// <summary>Be verbose.</summary>
		public static CommandArgument Verbose()
		{
			return CommandArgument.Verbose();
		}

		/// <summary>Allow adding otherwise ignored files.</summary>
		public static CommandArgument Force()
		{
			return new CommandArgument("--force");
		}

		/// <summary>
		///	Add modified contents in the working tree interactively to the index. Optional path arguments
		///	may be supplied to limit operation to a subset of the working tree. See “Interactive mode” for details.
		/// </summary>
		public static CommandArgument Interactive()
		{
			return CommandArgument.Interactive();
		}

		/// <summary>
		/// Similar to Interactive mode but the initial command loop is bypassed and the patch subcommand
		/// is invoked using each of the specified filepatterns before exiting.
		/// </summary>
		public static CommandArgument Patch()
		{
			return new CommandArgument("--patch");
		}

		/// <summary>
		///	Open the diff vs. the index in an editor and let the user edit it. After the editor was closed,
		///	adjust the hunk headers and apply the patch to the index. 
		/// </summary>
		public static CommandArgument Edit()
		{
			return new CommandArgument("--edit");
		}

		/// <summary>
		///	Update only files that git already knows about, staging modified content for commit and marking
		///	deleted files for removal. This is similar to what "git commit -a" does in preparation for making a
		///	commit, except that the update is limited to paths specified on the command line. If no paths are
		///	specified, all tracked files in the current directory and its subdirectories are updated.
		/// </summary>
		public static CommandArgument Update()
		{
			return new CommandArgument("--update");
		}

		/// <summary>
		///	Update files that git already knows about (same as --update) and add all untracked files that are
		///	not ignored by .gitignore  mechanism.
		/// </summary>
		public static CommandArgument All()
		{
			return new CommandArgument("--all");
		}

		/// <summary>
		///	Record only the fact that the path will be added later. An entry for the path is placed in the
		///	index with no content. This is useful for, among other things, showing the unstaged content of such
		///	files with git diff and committing them with git commit -a.
		/// </summary>
		public static CommandArgument IntentToAdd()
		{
			return new CommandArgument("--intent-to-add");
		}

		/// <summary>Don't add the file(s), but only refresh their stat() information in the index.</summary>
		public static CommandArgument Refresh()
		{
			return new CommandArgument("--refresh");
		}

		/// <summary>
		///	If some files could not be added because of errors indexing them, do not abort the operation,
		///	but continue adding the others. The command shall still exit with non-zero status.
		/// </summary>
		public static CommandArgument IgnoreErrors()
		{
			return new CommandArgument("--ignore-errors");
		}

		/// <summary>
		///	This option can be used to separate command-line options from the list of files,
		///	(useful when filenames might be mistaken for command-line options).
		/// </summary>
		public static CommandArgument NoMoreOptions()
		{
			return new CommandArgument("--");
		}

		public AddCommand()
			: base("add")
		{
		}

		public AddCommand(params CommandArgument[] args)
			: base("add", args)
		{
		}

		public AddCommand(IList<CommandArgument> args)
			: base("add", args)
		{
		}
	}
}
