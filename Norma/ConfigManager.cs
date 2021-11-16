global using Norma.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Norma
{
	[Export(SingleInstance = true)]	
	public sealed class ConfigManager
	{
		public KeyValueConfigurationCollection Settings { get; }

		public ConfigManager()
		{
            try
            {
				configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			}
            catch (ConfigurationErrorsException e)
            {
				Console.Error.WriteLine(e.Message);
                return;
            }
			Settings = configFile.AppSettings.Settings;
			if (Settings["AssisName"] is null)
			{
				Settings.Add("AssisName", "Norma Lawns");
			}

			configFile.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
		}

		public ConfigManager(KeyValueConfigurationCollection settings)
		{
			Settings = settings;
			if (Settings["AssisName"] is null)
			{
				Settings.Add("AssisName", "Norma Lawns");
			}
		}

		private Configuration configFile;

	}
}
