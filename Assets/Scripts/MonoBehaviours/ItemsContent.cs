using System.Collections.Generic;
using UnityEngine;

namespace ShoesFactory
{
    public class ItemsContent : MonoBehaviour
    {
        [SerializeField] private GameObject _itemUIPrefab = null;

        public void UpdateUI(string tableName, List<string> result, int columnAmount)
        {
            DeleteItems();
            AddItems(tableName, result, columnAmount);
        }

        public void AddItems(string tableName, List<string> result, int columnAmount)
        {
            int itemsUIAmount = result.Count / columnAmount;
            int resultCounter = 0;

            for (int i = 0; i < itemsUIAmount; i++)
            {
                string[] itemUIValues = new string[columnAmount];
                for (int j = 0; j < columnAmount; j++)
                {
                    itemUIValues[j] = result[resultCounter];
                    resultCounter++;
                }

                Instantiate(_itemUIPrefab, transform).GetComponent<ItemUI>().Initialize(tableName, itemUIValues);
            }
        }

        public void DeleteItems()
        {
            foreach (Transform ttransform in transform)
            {
                Destroy(ttransform.gameObject);
            }
        }
    }
}
