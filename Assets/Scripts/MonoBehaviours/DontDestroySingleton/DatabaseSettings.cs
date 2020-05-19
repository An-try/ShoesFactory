using UnityEngine;

namespace ShoesFactory
{
    public class DatabaseSettings : DontDestroySingleton<DatabaseSettings>
    {
        [SerializeField] private string _folderLinkWithDatabaseAccessFiles = "http://localhost/shoes_factory/";
        [SerializeField] private string _hostName = "localhost";
        [SerializeField] private string _username = "root";
        [SerializeField] private string _password = "20002000";
        [SerializeField] private string _databaseName = "shoes_factory";
        [SerializeField] private string _port = "3306";
        [SerializeField] private string _connectionFailedErrorCode = "connection_failed_error_code";

        public string FolderLinkWithDatabaseAccessFiles => _folderLinkWithDatabaseAccessFiles;
        public string ConnectionFailedErrorCode => _connectionFailedErrorCode;

        public WWWForm CreateBasicWWWForm()
        {
            WWWForm wwwForm = new WWWForm();

            wwwForm.AddField("host_name", _hostName);
            wwwForm.AddField("username", _username);
            wwwForm.AddField("password", _password);
            wwwForm.AddField("database_name", _databaseName);
            wwwForm.AddField("port", _port);
            wwwForm.AddField("connection_failed_error_code", _connectionFailedErrorCode);

            return wwwForm;
        }
    }
}
