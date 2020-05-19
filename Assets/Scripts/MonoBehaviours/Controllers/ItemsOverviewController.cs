using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace ShoesFactory
{
    public class ItemsOverviewController : PanelUI<ItemsOverviewController>
    {
        [SerializeField] private string _currentTableNameInUse = "";
        [SerializeField] private List<string> _columnNames = null;
        [SerializeField] private CriteriasContent _criteriasContent = null;
        [SerializeField] private ItemsContent _itemsContent = null;
        [SerializeField] private TextMeshProUGUI _currentCategoryNameComponent = null;

        public string CurrentTableNameInUse => _currentTableNameInUse;
        public List<string> ColumnNames => _columnNames;
        public ISQL_BasicController CurrentUsedSQLController { get; private set; } = null;

        public void OutputColumnsAndRows(string tableName, List<string> columnNames, List<string> result, ISQL_BasicController sqlController)
        {
            ClearOutputWindow();

            CurrentUsedSQLController = sqlController;
            _currentTableNameInUse = tableName;
            _columnNames = columnNames;
            _currentCategoryNameComponent.text = tableName;
            _criteriasContent.AddColumns(columnNames);
            _itemsContent.AddItems(tableName, result, columnNames.Count);
            AddRowPanel.Instance.SetActive(true);
            AddRowPanel.Instance.UpdateUI(columnNames);
        }

        public void OutputColumns(string tableName, List<string> columnNames, ISQL_BasicController sqlController)
        {
            ClearOutputWindow();

            CurrentUsedSQLController = sqlController;
            _currentTableNameInUse = tableName;
            _columnNames = columnNames;
            _currentCategoryNameComponent.text = tableName;
            _criteriasContent.AddColumns(columnNames);
            AddRowPanel.Instance.SetActive(true);
            AddRowPanel.Instance.UpdateUI(columnNames);
        }

        public void ClearOutputWindow()
        {
            CurrentUsedSQLController = null;
            _currentTableNameInUse = "";
            _columnNames = null;
            _currentCategoryNameComponent.text = "";

            _criteriasContent.DeleteColumns();
            _itemsContent.DeleteItems();
            AddRowPanel.Instance.SetActive(false);
            AddRowPanel.Instance.DeleteItems();
        }



















        //[SerializeField] private Transform _itemsContent = null;
        //[SerializeField] private GameObject _itemUIPrefab = null;
        //[SerializeField] private TextMeshProUGUI _currentCategoryNameText = null;
        //private bool _isInventoryModeForSelectingItems = false;



        //#region Buttons

        //[Header("Buttons")]
        ////[SerializeField] private Button _allCategoryButton = null;
        //[SerializeField] private Button _skinStocksCategoryButton = null;
        //[SerializeField] private Button _shoesCategoryButton = null;
        //[SerializeField] private Button _driversCategoryButton = null;
        //[SerializeField] private Button _skinShopsCategoryButton = null;
        //[SerializeField] private Button _citiesCategoryButton = null;
        //[SerializeField] private Button _skinOrdersCategoryButton = null;
        //[SerializeField] private Button _completedDeliveriesCategoryButton = null;

        //[SerializeField] private Button _closePanelButton = null;

        //private void OnEnable()
        //{
        //    SubscribeEvents();
        //}

        //private void SubscribeEvents()
        //{
        //    //_allCategoryButton.onClick.AddListener(delegate { ShowItemsByType(); });
        //    _skinStocksCategoryButton.onClick.AddListener(delegate { ShowItemsByType(MenuCategoriesTypes.SkinStocks); });
        //    _shoesCategoryButton.onClick.AddListener(delegate { ShowItemsByType(MenuCategoriesTypes.Shoes); });
        //    _driversCategoryButton.onClick.AddListener(delegate { ShowItemsByType(MenuCategoriesTypes.Drivers); });
        //    _skinShopsCategoryButton.onClick.AddListener(delegate { ShowItemsByType(MenuCategoriesTypes.SkinShops); });
        //    _citiesCategoryButton.onClick.AddListener(delegate { ShowItemsByType(MenuCategoriesTypes.Cities); });
        //    _skinOrdersCategoryButton.onClick.AddListener(delegate { ShowItemsByType(MenuCategoriesTypes.SkinOrders); });
        //    _completedDeliveriesCategoryButton.onClick.AddListener(delegate { ShowItemsByType(MenuCategoriesTypes.CompletedDeliveries); });

        //    _closePanelButton.onClick.AddListener(ClosePanelWithAnimation);
        //}

        //private void OnDisable()
        //{
        //    UnsubscribeEvents();
        //}

        //private void UnsubscribeEvents()
        //{
        //    //_allCategoryButton.onClick.RemoveListener(delegate { ShowItemsByType(); });
        //    _skinStocksCategoryButton.onClick.RemoveListener(delegate { ShowItemsByType(MenuCategoriesTypes.SkinStocks); });
        //    _shoesCategoryButton.onClick.RemoveListener(delegate { ShowItemsByType(MenuCategoriesTypes.Shoes); });
        //    _driversCategoryButton.onClick.RemoveListener(delegate { ShowItemsByType(MenuCategoriesTypes.Drivers); });
        //    _skinShopsCategoryButton.onClick.RemoveListener(delegate { ShowItemsByType(MenuCategoriesTypes.SkinShops); });
        //    _citiesCategoryButton.onClick.RemoveListener(delegate { ShowItemsByType(MenuCategoriesTypes.Cities); });
        //    _skinOrdersCategoryButton.onClick.RemoveListener(delegate { ShowItemsByType(MenuCategoriesTypes.SkinOrders); });
        //    _completedDeliveriesCategoryButton.onClick.RemoveListener(delegate { ShowItemsByType(MenuCategoriesTypes.CompletedDeliveries); });

        //    _closePanelButton.onClick.RemoveListener(ClosePanelWithAnimation);
        //}

        //#endregion



        //private protected override void Init()
        //{
        //    if (_panelEnabledOnStart)
        //    {
        //        OpenPanelInstantly(false);
        //    }
        //    else
        //    {
        //        ClosePanelInstantly();
        //    }
        //}

        ///// <summary>
        ///// //TODO ... isInventoryModeForSelectingItems: true if the user should select some item Pass nothing to show all items in the inventory.
        ///// </summary>
        ///// <param name="isInventoryModeForSelectingItems"></param>
        ///// <param name="itemTypeSorting"></param>
        //public void OpenPanelWithAnimation(bool isInventoryModeForSelectingItems, MenuCategoriesTypes itemTypeSorting = (MenuCategoriesTypes)999)
        //{
        //    _isInventoryModeForSelectingItems = isInventoryModeForSelectingItems;

        //    ShowItemsByType(itemTypeSorting);
        //    OpenPanelWithAnimation();
        //}

        ///// <summary>
        ///// //TODO ... isInventoryModeForSelectingItems: true if the user should select some item Pass nothing to show all items in the inventory.
        ///// </summary>
        ///// <param name="isInventoryModeForSelectingItems"></param>
        ///// <param name="itemTypeSorting"></param>
        //public void OpenPanelInstantly(bool isInventoryModeForSelectingItems, MenuCategoriesTypes itemTypeSorting = (MenuCategoriesTypes)999)
        //{
        //    _isInventoryModeForSelectingItems = isInventoryModeForSelectingItems;

        //    ShowItemsByType(itemTypeSorting);
        //    OpenPanelInstantly();
        //}

        //public void ShowItemsByType(MenuCategoriesTypes itemType = (MenuCategoriesTypes)999)
        //{
        //    DeleteAllItemsInMenu();
        //    InstantiateItemsInMenu(itemType);

        //    if (Enum.IsDefined(typeof(MenuCategoriesTypes), itemType))
        //    {
        //        _currentCategoryNameText.text = itemType.ToString();
        //    }
        //    else
        //    {
        //        _currentCategoryNameText.text = "All items";
        //    }
        //}

        ///// <summary>
        ///// Pass nothing to show all items in the inventory.
        ///// </summary>
        ///// <param name="itemType"></param>
        ///// <returns></returns>
        //private void InstantiateItemsInMenu(MenuCategoriesTypes itemType)
        //{
        //    //PlayerData playerData = DataManager.Instance.LoadPlayerData();

        //    //if (Enum.IsDefined(typeof(MenuCategoriesTypes), itemType))
        //    //{
        //    //    if (itemType == MenuCategoriesTypes.Ship)
        //    //    {
        //    //        for (int i = 0; i < playerData.ShipsInInventory.Count; i++)
        //    //        {
        //    //            Instantiate(_itemUIPrefab, _itemsContent).GetComponent<InventoryItemUI>().SetInventoryItem(playerData.ShipsInInventory[i]);
        //    //        }
        //    //    }
        //    //    else
        //    //    {
        //    //        for (int i = 0; i < playerData.ItemsInInventory.Count; i++)
        //    //        {
        //    //            if (playerData.ItemsInInventory[i].ItemType == itemType)
        //    //            {
        //    //                Instantiate(_itemUIPrefab, _itemsContent).GetComponent<InventoryItemUI>().SetInventoryItem(playerData.ItemsInInventory[i]);
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    for (int i = 0; i < playerData.ItemsInInventory.Count; i++)
        //    //    {
        //    //        Instantiate(_itemUIPrefab, _itemsContent).GetComponent<InventoryItemUI>().SetInventoryItem(playerData.ItemsInInventory[i]);
        //    //    }

        //    //    for (int i = 0; i < playerData.ShipsInInventory.Count; i++)
        //    //    {
        //    //        Instantiate(_itemUIPrefab, _itemsContent).GetComponent<InventoryItemUI>().SetInventoryItem(playerData.ShipsInInventory[i]);
        //    //    }
        //    //}
        //}

        //private void DeleteAllItemsInMenu()
        //{
        //    foreach (Transform inventoryItemUI in _itemsContent)
        //    {
        //        Destroy(inventoryItemUI.gameObject);
        //    }
        //}
    }
}
