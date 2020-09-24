namespace NHibernateSample.Util.Helpers
{
    public class ConfigurationHelper
    {
        // TODO: Transfer connection strings to the Azure Key Vault.
        public static string ConnectionString =>
        "Server=localhost;Port=5432;Database=db_nhibernatesample;User Id=postgres;Password=12345678";

        // TODO: Move to the config file.
        public static string EntitiesNamespace =>
            "NHibernateSample.Entity";

        // TODO: Move to the config file.
        public static string EntitiesMapsNamespace =>
            "NHibernateSample.EntityMap";

        // TODO: Move to the config file.
        public static string DatabaseSchema =>
            "sample";

        // TODO: Move to the config file.
        public static string ScriptFilesDirectory =>
            @"C:\NHibernateSampleScripts\";

        // TODO: Move to the config file.
        public static string SublimeTextPath =>
            @"C:\Program Files\Sublime Text 3\sublime_text.exe";

        // TODO: Move to the config file.
        public static string NotepadPath =>
            "notepad.exe";
    }
}
