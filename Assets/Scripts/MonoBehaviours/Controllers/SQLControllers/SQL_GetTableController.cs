using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShoesFactory
{
    public class SQL_GetTableController : SQL_BasicController<SQL_GetTableController>, ISQL_BasicController
    {
        [SerializeField] private string _noResultsCode = "no_results_code";

        private protected override IEnumerator SQLCoroutine(string tableName)
        {
            _isExecutingSQLCoroutine = true;

            WWWForm wwwForm = DatabaseSettings.Instance.CreateBasicWWWForm();
            wwwForm.AddField("table_name", tableName);
            wwwForm.AddField("no_results_code", _noResultsCode);

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
            else if (sqlConnect.text.Contains(_noResultsCode))
            {
                Debug.LogFormat(sqlConnect.text);
                List<string> result = sqlConnect.text.Split('\t').ToList();
                List<string> columnNames = GetColumnNames(ref result);

                _outputColumnsEvent?.Invoke(tableName, columnNames, this);
                Debug.LogFormat(_noResultsCode);
            }
            else
            {
                Debug.LogFormat(sqlConnect.text);
                List<string> result = sqlConnect.text.Split('\t').ToList();
                List<string> columnNames = GetColumnNames(ref result);

                _outputColumnsAndRowsEvent?.Invoke(tableName, columnNames, result, this);
                Debug.Log("Operation successful");
            }

            _isExecutingSQLCoroutine = false;

            yield break;
        }
    }
}
