using System.Collections;
using UnityEngine;

namespace ShoesFactory
{
    public class SQL_UpdateTableController : SQL_BasicController<SQL_UpdateTableController>, ISQL_BasicController
    {
        [SerializeField] private string _updateErrorCode = "update_error_code";

        public void ExecuteSQLRequest(string tableName, string columnName, string newData, int rowId)
        {
            if (_isExecutingSQLCoroutine)
            {
                return;
            }

            KillSQLCoroutine();
            _sqlCoroutine = StartCoroutine(SQLCoroutine(tableName, columnName, newData, rowId));
        }

        private IEnumerator SQLCoroutine(string tableName, string columnName, string newData, int rowId)
        {
            _isExecutingSQLCoroutine = true;

            WWWForm wwwForm = DatabaseSettings.Instance.CreateBasicWWWForm();
            wwwForm.AddField("table_name", tableName);
            wwwForm.AddField("column_name", columnName);
            wwwForm.AddField("new_data", newData);
            wwwForm.AddField("row_id", rowId);
            Debug.Log(tableName + " " + columnName + " " + newData + " " + rowId);
            wwwForm.AddField("update_error_code", _updateErrorCode);

            WWW sqlConnect = new WWW(DatabaseSettings.Instance.FolderLinkWithDatabaseAccessFiles + _phpFileName, wwwForm);
            yield return sqlConnect;

            if (!string.IsNullOrEmpty(sqlConnect.error))
            {
                Debug.LogError(sqlConnect.error);
                _isExecutingSQLCoroutine = false;
                yield break;
            }

            if (sqlConnect.text.Contains(DatabaseSettings.Instance.ConnectionFailedErrorCode))
            {
                Debug.LogError(sqlConnect.text);
            }
            else if (sqlConnect.text.Contains(_updateErrorCode))
            {
                Debug.LogError(_updateErrorCode);
                Debug.LogError(sqlConnect.text);
            }
            else
            {
                //_onSQLExecutedEvent?.Invoke(sqlConnect.text, _columnNames);
                Debug.Log("Operation successful");
            }

            _isExecutingSQLCoroutine = false;

            yield break;
        }
    }
}
