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

namespace gitter.Framework
{
	using System;
	using System.IO;

	using gitter.Framework.Configuration;
	using gitter.Framework.Services;

	public sealed class ConfigurationService : IDisposable
	{
		#region Constants

		private const string ConfigFileName = "gitter.xml";
		private const string AppFolderName = "gitter";

		#endregion

		#region Data

		private readonly string _configPath;
		private readonly string _configFileName;
		private Section _providersSection;

		#endregion

		#region .ctor & finalizer

		internal ConfigurationService()
		{
			var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			_configPath = Path.Combine(appData, AppFolderName);
			_configFileName = Path.Combine(_configPath, ConfigFileName);

			if(!Directory.Exists(_configPath))
			{
				try
				{
					Directory.CreateDirectory(_configPath);
				}
				catch(Exception exc)
				{
					if(exc.IsCritical())
					{
						throw;
					}
					LoggingService.Global.Error(exc);
				}
			}

			Configuration            = LoadConfig(ConfigFileName, "Configuration");
			RootSection              = Configuration.RootSection;
			GuiSection               = RootSection.GetCreateSection("Gui");
			GlobalSection            = RootSection.GetCreateSection("Global");
			ViewsSection             = RootSection.GetCreateSection("Tools");
			_providersSection        = RootSection.GetCreateSection("Providers");
			RepositoryManagerSection = RootSection.GetCreateSection("RepositoryManager");
		}

		~ConfigurationService()
		{
			Dispose(false);
		}

		#endregion

		public ConfigurationManager Configuration { get; }

		public Stream CreateFile(string fileName)
		{
			return new FileStream(Path.Combine(_configPath, fileName), FileMode.Create, FileAccess.Write, FileShare.None);
		}

		public Stream OpenFile(string fileName)
		{
			return new FileStream(Path.Combine(_configPath, fileName), FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		public bool FileExists(string fileName)
		{
			return File.Exists(Path.Combine(_configPath, fileName));
		}

		public Section RootSection { get; }

		public Section GuiSection { get; }

		public Section GlobalSection { get; }

		public Section ViewsSection { get; }

		public Section RepositoryManagerSection { get; }

		public Section GetSectionForProvider(IRepositoryProvider provider)
		{
			Verify.Argument.IsNotNull(provider, nameof(provider));

			return _providersSection.GetCreateSection(provider.Name);
		}

		public Section GetSectionForProvider(IRepositoryServiceProvider provider)
		{
			Verify.Argument.IsNotNull(provider, nameof(provider));

			return _providersSection.GetCreateSection(provider.Name);
		}

		public Section GetSectionForProviderGui(IRepositoryProvider provider)
		{
			Verify.Argument.IsNotNull(provider, nameof(provider));

			var section = _providersSection.GetCreateSection(provider.Name);
			return section.GetCreateSection("Gui");
		}

		public Section GetSectionForProviderGui(IRepositoryServiceProvider provider)
		{
			Verify.Argument.IsNotNull(provider, nameof(provider));

			var section = _providersSection.GetCreateSection(provider.Name);
			return section.GetCreateSection("Gui");
		}

		public void Save()
		{
			SaveConfig(ConfigFileName, Configuration);
		}

		private ConfigurationManager LoadConfig(string configFile, string configName)
		{
			ConfigurationManager config = null;
			if(FileExists(configFile))
			{
				try
				{
					using(var stream = OpenFile(configFile))
					{
						if(stream.Length != 0)
						{
							using(var adapter = new XmlAdapter(stream))
							{
								try
								{
									config = new ConfigurationManager(adapter);
								}
								catch(Exception exc)
								{
									if(exc.IsCritical())
									{
										throw;
									}
									LoggingService.Global.Error(exc);
								}
							}
						}
					}
				}
				catch(Exception exc)
				{
					if(exc.IsCritical())
					{
						throw;
					}
					LoggingService.Global.Error(exc);
				}
			}
			if(config == null)
			{
				config = new ConfigurationManager(configName);
			}
			return config;
		}

		private void SaveConfig(string configFile, ConfigurationManager config)
		{
			try
			{
				using(var stream = CreateFile(configFile))
				using(var adapter = new XmlAdapter(stream))
				{
					config.Save(adapter);
				}
			}
			catch(Exception exc)
			{
				if(exc.IsCritical())
				{
					throw;
				}
				LoggingService.Global.Error(exc);
			}
		}

		#region IDisposable

		public bool IsDisposed { get; private set; }

		private void Dispose(bool disposing)
		{
			if(disposing)
			{
			}
		}

		public void Dispose()
		{
			if(!IsDisposed)
			{
				GC.SuppressFinalize(this);
				Dispose(true);
				IsDisposed = true;
			}
		}

		#endregion
	}
}
