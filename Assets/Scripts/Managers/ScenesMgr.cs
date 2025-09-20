using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//场景管理器
public class ScenesMgr : MonoSingleton<ScenesMgr>
{
    #region  Public Methods

    //异步加载场景
    public void LoadScene(SceneType sceneType, UnityAction unityAction = null)
    {
        //场景转换清空内存池
        PoolMgr.Instance.Clear();
        //场景转换清空UI
        UIMgr.Instance.Clear();
        //场景转换禁用输入
        InputMgr.Instance.Disable();

        //过渡UI淡入后启动场景加载
        UIMgr.Instance.ShowUI<UI_Loading>(UIType.UI_Loading, (obj) =>
        {
            StartCoroutine(LoadSceneCor(sceneType, unityAction));
        });
    }

    #endregion

    #region  Private Methods

    IEnumerator LoadSceneCor(SceneType sceneType, UnityAction unityAction = null)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneType.ToString());
        //禁止自动进入场景激活阶段
        ao.allowSceneActivation = false;
        //循环检查是否加载完成
        while (!ao.isDone)
        {
            //资源加载阶段（进度达到90%）
            if (ao.progress >= 0.9f)
            {
                // yield return new WaitForSecondsRealtime(3.0f);//模拟场景加载时的等待
                //（可选）uiProgress = ao.progress / 0.9f;//将进度条映射到0~1
                //（可选）"按任意键继续..."

                //场景激活阶段（进度达到100%）
                ao.allowSceneActivation = true;
            }
            yield return null;
        }

        //过渡UI淡出
        UIMgr.Instance.HideUI(UIType.UI_Loading, () => { unityAction?.Invoke(); });

        //场景转换完成启用输入
        InputMgr.Instance.Enable();
    }

    #endregion
}
