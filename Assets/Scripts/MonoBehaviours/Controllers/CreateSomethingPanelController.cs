using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ShoesFactory
{
    public class CreateSomethingPanelController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _firstNameInputField = null;
        [SerializeField] private TMP_InputField _secondNameInputField = null;
        [SerializeField] private TMP_InputField _ageInputField = null;
        [SerializeField] private TMP_InputField _salaryInputField = null;
        [SerializeField] private TMP_InputField _experienceInYearsInputField = null;
        [SerializeField] private Button _createEmployeeButton = null;

        private string _driverInsertFailedCode = "driver_insert_failed_code";

        private string _addDriverFileName = "add_driver.php";

        private Coroutine _addDriverCoroutine = null;

        private bool _isAddingDriver = false;

        private void OnEnable()
        {
            _createEmployeeButton.onClick.AddListener(AddEmployee);
        }

        private void OnDisable()
        {
            _createEmployeeButton.onClick.RemoveListener(AddEmployee);
        }

        public void AddEmployee()
        {
            if (_isAddingDriver)
            {
                return;
            }

            KillCoroutine(ref _addDriverCoroutine);
            _addDriverCoroutine = StartCoroutine(AddDriverCoroutine());
        }

        private IEnumerator AddDriverCoroutine()
        {
            _isAddingDriver = true;

            WWWForm wwwForm = DatabaseSettings.Instance.CreateBasicWWWForm();
            wwwForm.AddField("driver_insert_failed_code", _driverInsertFailedCode);

            wwwForm.AddField("first_name", _firstNameInputField.text);
            wwwForm.AddField("last_name", _secondNameInputField.text);
            wwwForm.AddField("age", _ageInputField.text);
            wwwForm.AddField("salary", _salaryInputField.text);
            wwwForm.AddField("experience_in_years", _experienceInYearsInputField.text);

            WWW sqlConnect = new WWW(DatabaseSettings.Instance.FolderLinkWithDatabaseAccessFiles + _addDriverFileName, wwwForm);
            yield return sqlConnect;

            if (!string.IsNullOrEmpty(sqlConnect.error))
            {
                Debug.LogError(sqlConnect.error);
                _isAddingDriver = false;
                yield break;
            }

            if (sqlConnect.text.Contains(DatabaseSettings.Instance.ConnectionFailedErrorCode) ||
                sqlConnect.text.Contains(_driverInsertFailedCode))
            {
                Debug.LogError(sqlConnect.text);
            }
            else
            {
                Debug.Log("Operation successful");
            }

            _isAddingDriver = false;
            yield break;
        }

        private void KillCoroutine(ref Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}
