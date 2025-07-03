
using UnityEngine;
using UnityEngine.UI;

public interface IStageState
{
    void SetButtonState();
    void HandleClick(int stageId);
}

public class ActiveStageState : IStageState
{
    private Button button;
    private UserStageData userStageData;
    private IStageService stageService;

    public ActiveStageState(Button button, UserStageData userStageData, IStageService stageService)
    {
        this.button = button;
        this.userStageData = userStageData;
        this.stageService = stageService;
    }

    public void SetButtonState()
    {
        button.interactable = true;
        Debug.Log("활성화");
      //button.GetComponentInChildren<Text>().text = userStageData.stage_name;  
    }

    public void HandleClick(int stageId)
    {

        stageService.MoveToStage(stageId);
    }
}

public class InActiveStageState : IStageState
{
    private Button button;
    private UserStageData userStageData;
    private IStageService stageService;

    public InActiveStageState(Button button)
    {
        this.button = button;

    }

    public void SetButtonState()
    {
        button.interactable = false;
        Debug.Log("비활성화");
        //button.GetComponentInChildren<Text>().text = userStageData.stage_name;  
    }

    public void HandleClick(int stageId)
    {

        Debug.Log("비활성화된 스테이지는 클릭할 수 없습니다."); //솔직히 아무 로직 없음
    }
}
