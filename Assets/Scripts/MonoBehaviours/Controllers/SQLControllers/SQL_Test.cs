using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShoesFactory
{
    public class SQL_Test : SQL_BasicController<SQL_Test>, ISQL_BasicController
    {
        private protected override IEnumerator SQLCoroutine(string tableName)
        {
            _isExecutingSQLCoroutine = true;

            WWWForm wwwForm = DatabaseSettings.Instance.CreateBasicWWWForm();
            wwwForm.AddField("table_name", tableName);

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
            //else if (sqlConnect.text.Contains(_noResultsCode))
            //{
            //    Debug.Log(sqlConnect.text);
            //}
            else
            {
                //_onSQLExecutedEvent?.Invoke(sqlConnect.text, _columnNames);
                Debug.Log(sqlConnect.text);
                //ItemsOverviewController.Instance.CurrentTableNameInUse = "drivers";
                Debug.Log("Operation successful");
            }

            _isExecutingSQLCoroutine = false;

            yield break;
        }
    }
}
