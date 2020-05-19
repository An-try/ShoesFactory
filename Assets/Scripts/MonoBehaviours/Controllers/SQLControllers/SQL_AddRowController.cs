using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShoesFactory
{
    public class SQL_AddRowController : SQL_BasicController<SQL_AddRowController>, ISQL_BasicController
    {
        [SerializeField] private string _insertRowErrorCode = "insert_row_error_code";

        public void ExecuteSQLRequest(string tableName, List<string> columnsNames, List<string> dataToInsert)
        {
            if (_isExecutingSQLCoroutine)
            {
                return;
            }

            //string columnNamesString = "";
            //for (int i = 1; i < columnsNames.Length; i++)
            //{
            //    columnNamesString += columnsNames[i] + "\t";
            //}
            //columnsNames.RemoveAt(0);

            KillSQLCoroutine();
            _sqlCoroutine = StartCoroutine(SQLCoroutine(tableName, columnsNames.Count, columnsNames, dataToInsert));
        }

        private IEnumerator SQLCoroutine(string tableName, int columnsAmount, List<string> columnsNames, List<string> dataToInsert)
        {
            _isExecutingSQLCoroutine = true;

            WWWForm wwwForm = DatabaseSettings.Instance.CreateBasicWWWForm();

            string command = "";

            int counter = 0;
            while (counter < dataToInsert.Count)
            {
                command = "INSERT INTO " + tableName + " (";

                for (int j = 0; j < columnsNames.Count; j++)
                {
                    if (j != columnsNames.Count - 1)
                    {
                        command += columnsNames[j] + ", ";
                    }
                    else
                    {
                        command += columnsNames[j] + ") VALUES ('";
                    }
                }

                for (int j = 0; j < columnsNames.Count; j++)
                {
                    if (j != columnsNames.Count - 1)
                    {
                        command += dataToInsert[counter] + "', '";
                    }
                    else
                    {
                        command += dataToInsert[counter] + "');";
                    }
                    counter++;
                }
            }

            wwwForm.AddField("command", command);
            wwwForm.AddField("insert_row_failed_code", _insertRowErrorCode);

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
            else if (sqlConnect.text.Contains(_insertRowErrorCode))
            {
                Debug.LogError(_insertRowErrorCode + " : " + sqlConnect.text);
            }
            else
            {
                ItemsOverviewController.Instance.CurrentUsedSQLController.ExecuteSQLRequest(tableName);
                Debug.Log("Operation successful");
            }

            _isExecutingSQLCoroutine = false;

            yield break;
        }
    }
}
