using NHibernate.Tool.hbm2ddl;
using NHibernateSample.Util.Helpers;
using System;
using System.Diagnostics;
using System.IO;

namespace NHibernateSample.OrmConfig
{
    public static class ScriptFileBuilder
    {
        public static void GenerateScriptsFile(NHibernate.Cfg.Configuration config, string directoryPath, bool openScriptsFileAfterCreation)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var fileName = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.sql";
            var fileFullName = $"{directoryPath}{fileName}";

            var schemaExport = new SchemaExport(config);
            schemaExport.SetDelimiter(";");
            schemaExport.SetOutputFile(fileFullName);
            schemaExport.Execute(false, false, false);

            if (openScriptsFileAfterCreation)
            {
                Process.Start(
                    File.Exists(ConfigurationHelper.SublimeTextPath)
                        ? ConfigurationHelper.SublimeTextPath
                        : ConfigurationHelper.NotepadPath, fileFullName);
            }
        }
    }
}
