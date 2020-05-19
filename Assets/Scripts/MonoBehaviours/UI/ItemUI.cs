using UnityEngine;

namespace ShoesFactory
{
    public class ItemUI : MonoBehaviour
    {
        [SerializeField] private GameObject _itemColumnUIPrefab = null;
        [SerializeField] private Transform _layoutGroup = null;

        private string _tableName;

        private int _rowId
        {
            get
            {
                int id = -999;

                if (int.TryParse(_layoutGroup.GetChild(0).GetComponent<ItemColumnUI>().Text, out int result))
                {
                    id = result;
                }
                else
                {
                    Debug.LogError("[ItemUI] : Cannot get row id.");
                }

                return id;
            }
        }

        public void Initialize(string tableName, string[] values)
        {
            _tableName = tableName;
            AddColumns(values);
        }

        public void DeleteRow()
        {
            SQL_DeleteRowController.Instance.ExecuteSQLRequest(_tableName, _rowId, gameObject);
        }

        private void AddColumns(string[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                Instantiate(_itemColumnUIPrefab, _layoutGroup).GetComponent<ItemColumnUI>().SetText(values[i], i != 0);
            }
        }
    }
}
