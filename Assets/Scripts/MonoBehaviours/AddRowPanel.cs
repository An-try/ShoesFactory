using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShoesFactory
{
    public class AddRowPanel : Singleton<AddRowPanel>
    {
        [SerializeField] private GameObject _inputFieldPrefab = null;
        [SerializeField] private Transform _inputFieldsParent = null;
        [SerializeField] private bool _enableOnStart = true;

        private void Start()
        {
            SetActive(_enableOnStart);
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void UpdateUI(List<string> columnNames)
        {
            DeleteItems();
            List<string> columnNamesList = columnNames;
            columnNamesList.RemoveAt(0);
            AddInputFields(columnNamesList);
        }

        public void AddInputFields(List<string> columnNames)
        {
            for (int i = 0; i < columnNames.Count; i++)
            {
                Instantiate(_inputFieldPrefab, _inputFieldsParent).GetComponent<AddRowInputFieldUI>().Initialize(columnNames[i]);
            }
        }

        public void DeleteItems()
        {
            foreach (Transform ttransform in _inputFieldsParent)
            {
                Destroy(ttransform.gameObject);
            }
        }

        public void AddRow()
        {
            List<string> dataToInsert = new List<string>();

            for (int i = 0; i < _inputFieldsParent.childCount; i++)
            {
                dataToInsert.Add(_inputFieldsParent.GetChild(i).GetComponent<AddRowInputFieldUI>().Text);
            }

            SQL_AddRowController.Instance.ExecuteSQLRequest(ItemsOverviewController.Instance.CurrentTableNameInUse,
                                                            ItemsOverviewController.Instance.ColumnNames,
                                                            dataToInsert);
        }
    }
}
