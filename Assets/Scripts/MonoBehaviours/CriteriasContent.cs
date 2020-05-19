using System.Collections.Generic;
using UnityEngine;

namespace ShoesFactory
{
    public class CriteriasContent : Singleton<CriteriasContent>
    {
        [SerializeField] private GameObject _critetiaColumnUIPrefab = null;

        public void Initialize(List<string> values)
        {
            DeleteColumns();
            AddColumns(values);
        }

        public void AddColumns(List<string> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                Instantiate(_critetiaColumnUIPrefab, transform).GetComponent<CriteriaColumnUI>().SetText(values[i]);
            }
        }

        public void DeleteColumns()
        {
            foreach (Transform ttransform in transform)
            {
                Destroy(ttransform.gameObject);
            }
        }
    }
}
