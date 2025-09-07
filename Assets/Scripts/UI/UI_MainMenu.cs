using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenu : UI_Base
{
    const string BUTTON_NAME__BUTTON_START = "Button_Start";
    const string BUTTON_NAME__BUTTON_SETTINGS = "Button_Settings";
    const string BUTTON_NAME__BUTTON_EXIT = "Button_Exit";



    protected override void onClick(string btnName)
    {
        EventCenter.Instance.EventTrigger(EventType.Event_ButtonClick);

        switch (btnName)
        {
            default:
                break;
            case BUTTON_NAME__BUTTON_START:
                UIMgr.Instance.HideUI(UIType.UI_MainMenu);
                ScenesMgr.Instance.LoadScene(SceneType.Scene_GameScene);
                break;
            case BUTTON_NAME__BUTTON_SETTINGS:
                // UIMgr.Instance.ShowUI<UI_Settings>(UIType.UI_Settings);
                break;
            case BUTTON_NAME__BUTTON_EXIT:
                Application.Quit();
                break;
        }
    }
}
