using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// 메인 씬 UI
/// UI_Scene을 상속받아, 씬 전환 및 팝업 호출을 담당.
/// 프레임워크의 Bind 시스템을 이용해 버튼 이벤트를 자동으로 연결.

public class UI_MainScene : UI_Scene
{
    private enum Buttons
    {
        Ranking,
        Setting,
        Start,
        Custom,
    }

    private void Start()
    {
        InitializeUI();
    }

    /// UI 초기화
    private void InitializeUI()
    {
        Init();
        SetupButtonEvents();
    }

    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
    }

    /// 각 버튼 이벤트 등록
    private void SetupButtonEvents()
    {
        GetButton((int)Buttons.Start).gameObject.BindEvent(OnClickStart);
        GetButton((int)Buttons.Custom).gameObject.BindEvent(OnClickCustom);
        GetButton((int)Buttons.Ranking).gameObject.BindEvent(OnClickRanking);
        GetButton((int)Buttons.Setting).gameObject.BindEvent(OnClickSetting);
    }


    /// 게임 시작 버튼 클릭 → LevelMenu 씬 전환
    private void OnClickStart(PointerEventData _)
    {
        _sceneLoader.LoadScene("LevelMenu");
        AudioManager.Instance.PlaySFX("UiClick");
    }


    /// 커스텀 버튼 클릭 → Custom 씬 전환
    private void OnClickCustom(PointerEventData _)
    {
        _sceneLoader.LoadScene("Custom", () =>
        {
            AudioManager.Instance.PlayBGM("Custom");
        });

        Time.timeScale = 1f;
        AudioManager.Instance.PlaySFX("UiClick");
    }


    /// 랭킹 버튼 클릭 → 구글 리더보드 열기
    private void OnClickRanking(PointerEventData _)
    {
        GooglePlayManager.Instance.ShowLeaderBoard();
        AudioManager.Instance.PlaySFX("UiClick");
    }


    /// 설정 버튼 클릭 → 설정 팝업 열기
    private void OnClickSetting(PointerEventData _)
    {
        UIManager.Instance.ShowPopupUI<UI_Settings>();
        AudioManager.Instance.PlaySFX("UiClick");
    }
}
