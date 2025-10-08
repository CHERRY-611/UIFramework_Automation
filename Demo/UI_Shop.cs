using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/// 상점 UI 로직
/// UI_Popup을 상속받아 프레임워크 구조(UI_Base, UIManager 등)에 맞게 동작.
/// ScriptableObject(ShopItemSO)를 기반으로 상점 항목을 로드하고,
/// 코인, 구매, 선택 상태를 UI에 반영.
public class UI_Shop : UI_Popup
{
    [Header("Shop Data")]
    [SerializeField] private ShopItemSO[] _shopItems;
    [SerializeField] private Sprite _defaultCustomSprite;

    [Header("UI References")]
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private GameObject[] _shopItemPanels;
    [SerializeField] private ShopTemplate[] _shopTemplates;
    [SerializeField] private Button[] _purchaseButtons;
    [SerializeField] private Button[] _selectButtons;

    private int _totalCoin;
    private int _currentItemID = -1;
    private int _selectedItemIndex = -1;

    private enum Buttons
    {
        Lobby,
    }

    private void Start()
    {
        InitializeUI();
    }

    /// 초기화 전체 프로세스
    private void InitializeUI()
    {
        Init();                    // Base Init
        SetupButtons();             // 버튼 바인딩
        InitializeShopPanels();     // UI 및 데이터 로드
        RefreshShopState();         // 구매 가능 상태 갱신
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        GetButton((int)Buttons.Lobby).gameObject.BindEvent(OnClickGoToCustom);
    }

    /// 상점 패널 초기화 및 데이터 로드
    private void InitializeShopPanels()
    {
        for (int i = 0; i < _shopItems.Length; i++)
            _shopItemPanels[i].SetActive(true);

        _currentItemID = GameManager.Instance.CurrentShopItemID;
        _totalCoin = GameManager.Instance.TotalCoin;
        _coinText.text = _totalCoin.ToString();

        LoadShopItems();
    }

    /// 버튼에 이벤트 바인딩
    private void SetupButtons()
    {
        // 구매/선택 버튼은 인스펙터에서 직접 바인딩되어 있음
        // 필요 시 동적 바인딩 추가 가능
    }

    /// 커스텀 씬으로 이동
    private void OnClickGoToCustom(PointerEventData _)
    {
        _sceneLoader.LoadScene("Custom");
    }

    /// 각 상점 아이템의 상태(구매/선택 가능 여부) 갱신
    private void RefreshShopState()
    {
        for (int i = 0; i < _shopItems.Length; i++)
        {
            var item = _shopItems[i];
            var template = _shopTemplates[i];

            bool canPurchase = _totalCoin >= item.BaseCost;
            bool isPurchased = GameManager.Instance.PurchaseShopItemIDList.Contains(item.ItemID);
            bool isSelected = _currentItemID == item.ItemID;

            _purchaseButtons[i].interactable = canPurchase;
            _selectButtons[i].interactable = isPurchased;

            template.BlockOkayBtn.SetActive(!canPurchase);
            template.PurchasedUI.SetActive(isPurchased);
            template.SelectedUI.SetActive(isSelected);
        }
    }

    /// 상점 항목 UI 표시 갱신
    private void LoadShopItems()
    {
        for (int i = 0; i < _shopItems.Length; i++)
        {
            var item = _shopItems[i];
            var template = _shopTemplates[i];

            template.TitleText.text = item.Title;
            template.CostText.text = item.BaseCost.ToString();
            template.ItemSprite.sprite = item.ItemSprite;
        }
    }

    /// 구매 버튼 클릭 시 팝업 표시
    public void OnClickPurchase(int index)
    {
        _selectedItemIndex = index;

        var popup = UIManager.Instance.ShowPopupUI<UI_ShopPurchasePopUp>();
        popup.ItemNameTxt.text = _shopItems[_selectedItemIndex].Title;
        popup.UIShop = this;
    }

    /// 실제 구매 수행 (팝업에서 호출)
    public void PurchaseSelectedItem()
    {
        var item = _shopItems[_selectedItemIndex];

        _totalCoin -= item.BaseCost;
        GameManager.Instance.PurchaseShopItem(_totalCoin, item.ItemID);

        _coinText.text = _totalCoin.ToString();
        _selectButtons[_selectedItemIndex].interactable = true;

        RefreshShopState();
    }

    /// 아이템 선택 (or 해제)
    public void OnClickSelect(int index)
    {
        var item = _shopItems[index];

        // 선택 해제
        if (_currentItemID == item.ItemID)
        {
            _currentItemID = -1;
            GameManager.Instance.CurrentShopItemSprite = _defaultCustomSprite;
        }
        else // 새 아이템 선택
        {
            _currentItemID = item.ItemID;
            GameManager.Instance.CurrentShopItemSprite = item.ItemSprite;
        }

        GameManager.Instance.CurrentShopItemID = _currentItemID;
        GameManager.Instance.SaveCustomData();

        RefreshShopState();
    }
}
