using UnityEngine;
using TMPro;

namespace ShoesFactory
{
    public class CriteriaColumnUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textComponent = null;

        public string Text => _textComponent.text;

        public void SetText(string text)
        {
            _textComponent.text = text;
        }
    }
}
