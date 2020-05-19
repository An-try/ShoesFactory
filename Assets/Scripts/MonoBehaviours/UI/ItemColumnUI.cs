using UnityEngine;
using TMPro;

namespace ShoesFactory
{
    public class ItemColumnUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputFieldComponent = null;
        [SerializeField] private TextMeshProUGUI _placeholderTextComponent = null;

        public string Text => _inputFieldComponent.text;

        public void SetText(string text, bool isInteractable)
        {
            _inputFieldComponent.interactable = isInteractable;
            _inputFieldComponent.text = text;
            _placeholderTextComponent.text = "Enter " + CriteriasContent.Instance.transform.GetChild(transform.GetSiblingIndex()).GetComponent<CriteriaColumnUI>().Text;
        }

        public void UpdateRow(string newData)
        {
            if (int.TryParse(transform.parent.GetChild(0).GetComponent<ItemColumnUI>().Text, out int rowId))
            {
                SQL_UpdateTableController.Instance.ExecuteSQLRequest(ItemsOverviewController.Instance.CurrentTableNameInUse,
                                                                 ItemsOverviewController.Instance.ColumnNames[transform.GetSiblingIndex() - 1],
                                                                 newData,
                                                                 rowId);
            }
            else
            {
                Debug.LogError("[ItemColumnUI] : Cannot get row id.");
            }
        }
    }
}
