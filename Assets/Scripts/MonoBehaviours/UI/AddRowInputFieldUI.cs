using UnityEngine;
using TMPro;


namespace ShoesFactory
{
    public class AddRowInputFieldUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _inputFieldComponent = null;
        [SerializeField] private TextMeshProUGUI _placeholderTextComponent = null;

        public string Text => _inputFieldComponent.text;

        public void Initialize(string columnName)
        {
            _placeholderTextComponent.text = "Enter " + columnName;
        }
    }
}
