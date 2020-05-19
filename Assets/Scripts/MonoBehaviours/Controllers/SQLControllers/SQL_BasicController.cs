using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShoesFactory
{
    public abstract class SQL_BasicController<T> : Singleton<T> where T : MonoBehaviour, ISQL_BasicController
    {
        [SerializeField] private bool _isDebug = false;
        [SerializeField] private protected string _phpFileName = null;
        [SerializeField] private protected ItemsOverviewController _itemsOverviewController = null;

        private protected Coroutine _sqlCoroutine = null;
        private protected bool _isExecutingSQLCoroutine = false;

        private protected Action<string, List<string>, List<string>, ISQL_BasicController> _outputColumnsAndRowsEvent;
        private protected Action<string, List<string>, ISQL_BasicController> _outputColumnsEvent;

        private void OnEnable()
        {
            _outputColumnsAndRowsEvent += OutputColumnsAndRows;
            _outputColumnsEvent += OutputColumns;
        }

        private void OnDisable()
        {
            _outputColumnsAndRowsEvent -= OutputColumnsAndRows;
            _outputColumnsEvent -= OutputColumns;
        }

        private void Update()
        {
            if (_isDebug && Input.GetKeyDown(KeyCode.Space))
            {
                ExecuteSQLRequest("drivers");
            }
        }

        private void OutputColumnsAndRows(string tableName, List<string> columnNames, List<string> result, ISQL_BasicController sqlController)
        {
            _itemsOverviewController.OutputColumnsAndRows(tableName, columnNames, result, sqlController);
        }

        private void OutputColumns(string tableName, List<string> columnNames, ISQL_BasicController sqlController)
        {
            _itemsOverviewController.OutputColumns(tableName, columnNames, sqlController);
        }

        public void ExecuteSQLRequest(string tableName)
        {
            if (_isExecutingSQLCoroutine)
            {
                return;
            }

            KillSQLCoroutine();
            _sqlCoroutine = StartCoroutine(SQLCoroutine(tableName));
        }

        private protected virtual IEnumerator SQLCoroutine(string tableName)
        {
            yield break;
        }

        /// <summary>
        /// Return column names and delete them from the string.
        /// </summary>
        /// <param name="sqlConnect"></param>
        /// <param name=""></param>
        /// <returns></returns>
        private protected List<string> GetColumnNames(ref List<string> result)
        {
            List<string> columnNames = new List<string>();
            if (int.TryParse(result[0], out int columnsAmount) && result.Count > 0)
            {
                result.RemoveAt(0);

                for (int i = 0; i < columnsAmount; i++)
                {
                    columnNames.Add(result[0]);
                    result.RemoveAt(0);
                }
            }
            else
            {
                Debug.LogError("[SQL_BasicController] : Cannot get columns amount.");
            }

            return columnNames;
        }

        private protected void KillSQLCoroutine()
        {
            if (_sqlCoroutine != null)
            {
                StopCoroutine(_sqlCoroutine);
                _sqlCoroutine = null;
            }
        }
    }
}
