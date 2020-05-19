using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShoesFactory
{
    public class SQL_DeleteRowController : SQL_BasicController<SQL_DeleteRowController>, ISQL_BasicController
    {
        [SerializeField] private string _deleteRowErrorCode = "delete_row_error_code";

        public void ExecuteSQLRequest(string tableName, int rowIdToDelete, GameObject rowGameObjectToDestroy)
        {
            if (_isExecutingSQLCoroutine)
            {
                return;
            }

            KillSQLCoroutine();
            _sqlCoroutine = StartCoroutine(SQLCoroutine(tableName, rowIdToDelete, rowGameObjectToDestroy));
        }

        private IEnumerator SQLCoroutine(string tableName, int rowIdToDelete, GameObject rowGameObjectToDestroy)
        {
            _isExecutingSQLCoroutine = true;

            WWWForm wwwForm = DatabaseSettings.Instance.CreateBasicWWWForm();
            wwwForm.AddField("table_name", tableName);
            wwwForm.AddField("row_id", rowIdToDelete);
            wwwForm.AddField("delete_row_error_code", _deleteRowErrorCode);

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
            else if (sqlConnect.text.Contains(_deleteRowErrorCode))
            {
                Debug.LogFormat(_deleteRowErrorCode);
            }
            else
            {
                Destroy(rowGameObjectToDestroy);
                Debug.Log("Operation successful");
            }

            _isExecutingSQLCoroutine = false;

            yield break;
        }
    }
}
