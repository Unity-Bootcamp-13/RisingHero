using JetBrains.Annotations;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    [SerializeField] private GameObject clearWindow;  // 클리어 UI 게임오브젝트
    [SerializeField] private GameObject defeatWindow; // 패배 UI 게임오브젝트
    [SerializeField] private GameObject bossClearWindow; // 보스 클리어 UI 게임오브젝트
    [SerializeField] private GameObject allClearWindow; // 모든 스테이지 클리어

    private ISaveService saveService;
    private IStageSceneLoader stageSceneLoader;
    private PlayerSaveData saveData;

    public void Initialize(ISaveService saveService, IStageSceneLoader stageSceneLoader)
    {
        this.saveService = saveService;
        this.stageSceneLoader = stageSceneLoader;
    }


    private void Start()
    {
        if (clearWindow != null)
            clearWindow.SetActive(false);

        if (defeatWindow != null)
            defeatWindow.SetActive(false);

        if (bossClearWindow != null)
            bossClearWindow.SetActive(false);

        if (allClearWindow != null)
            allClearWindow.SetActive(false);

        saveData = saveService.Load();
    }

    public void ShowClearWindow()
    {
        if (clearWindow != null)
            clearWindow.SetActive(true);
    }


    public void HideClearWindow()
    {
        if (clearWindow != null)
            clearWindow.SetActive(false);
    }

    public void ShowClearBossWindow()
    {
        bossClearWindow.SetActive(true);
    }

    public void HideClearBossWindow()
    {
        bossClearWindow.SetActive(false);
    }

    public void ShowClearAllStageWindow()
    {
        allClearWindow.SetActive(true);
    }

    public void ShowDefeatWindow()
    {
        if (defeatWindow != null)
            defeatWindow.SetActive(true);
    }

    public void HideDefeatWindow()
    {
        if (defeatWindow != null)
            defeatWindow.SetActive(false);
    }

    public void OnClickCurrentNormalStage()
    {
        // 다음 스테이지로 넘어가는 로직을 여기에 작성
        Debug.Log("일반 스테이지로 넘어갑니다.");
        HideClearWindow();
        stageSceneLoader.LoadStage(saveData.currentStage, saveData.topStage);

    }

    public void OnClickNextEliteStage()
    {
        // 엘리트 스테이지로 넘어가는 로직을 여기에 작성
        Debug.Log("엘리트 스테이지로 넘어갑니다.");
        saveData = saveService.Load();
        saveData.currentStage = saveData.topStage + 1; // 엘리트 스테이지는 최상위 스테이지 다음 단계
        saveService.Save(saveData); // 스테이지 변경 사항 저장
        HideClearWindow();
        stageSceneLoader.LoadStage(saveData.currentStage , saveData.topStage);
    }

    public void OnClickNextChapterNormalStage()
    {
        // 보스에서 다음 챕터 노말 스테이지로 이동 (ex 19 -> 21)
        Debug.Log("다음 챕터 일반 스테이지로 넘어갑니다.");
        saveData = saveService.Load();
        saveData.currentStage += 2;
        saveService.Save(saveData);
        HideClearBossWindow();
        stageSceneLoader.LoadStage(saveData.currentStage, saveData.topStage);
    }

    public void OnClickRetry()
    {
        // 재시작 로직을 여기에 작성
        Debug.Log("스테이지를 재시작합니다.");
        HideDefeatWindow();
        stageSceneLoader.LoadStage(saveData.currentStage, saveData.topStage);
    }

    public void onClickPreviousStage()
    {
        // 이전 스테이지로 돌아가는 로직을 여기에 작성
        Debug.Log("이전 스테이지로 돌아갑니다.");
        saveData = saveService.Load();

        saveData.currentStage -= 1; // 현재 스테이지를 이전 단계로 설정
        saveService.Save(saveData); // 스테이지 변경 사항 저장

        HideDefeatWindow();
        // Hide AllClearWindow(); 필요한가?
        stageSceneLoader.LoadStage(saveData.currentStage, saveData.topStage);

    }
}