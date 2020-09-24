# NHibernateSample
 
This sample project demonstrates the usage of NHibernate with .NET Core.

The main point of this sample is to implement the following reusable methods on NHibernateManager.cs

- Initialize(ShowSql showSql) 
- CreateSchema() 
- UpdateSchema()
- GenerateScriptFile(string scriptsDirectoryPath, bool openScriptFileAfterCreation)
- DropSchema()
