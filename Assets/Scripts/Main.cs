using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Awake()
    {
        var inputMgr = InputMgr.Instance;
        var resMgr = ResMgr.Instance;
        var uIMgr = UIMgr.Instance;
        var scenesMgr = ScenesMgr.Instance;
        var musicMgr = MusicMgr.Instance;
        var soundMgr = SoundMgr.Instance;
        var poolMgr = PoolMgr.Instance;
    }

    void Start()
    {
        UIMgr.Instance.ShowUI<UI_MainMenu>(UIType.UI_MainMenu);
    }
}
