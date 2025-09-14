using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//UI管理器
public class UIMgr : MonoSingleton<UIMgr>
{
    Dictionary<UIType, UI_Base> Dic_UI;
    RectTransform canvas;
    float fadeDuration;



    void Awake()
    {
        Dic_UI = new Dictionary<UIType, UI_Base>();
        fadeDuration = 0.5f;

        ResMgr.Instance.LoadRes<GameObject>("UI/Canvas", (obj) =>
        {
            obj.name = "Canvas";
            canvas = obj.GetComponent<RectTransform>();
            GameObject.DontDestroyOnLoad(obj);
        });

        ResMgr.Instance.LoadRes<GameObject>("UI/EventSystem", (obj) =>
        {
            obj.name = "EventSystem";
            GameObject.DontDestroyOnLoad(obj);
        });
    }

    #region  Public Methods

    public void ShowUI<T>(UIType uIType, UnityAction<T> unityAction = null) where T : UI_Base
    {
        //重复打开相同UI视为关闭
        if (Dic_UI.ContainsKey(uIType))
        {
            HideUI(uIType);
            return;
        }

        ResMgr.Instance.LoadRes<GameObject>("UI/" + uIType.ToString(), (obj) =>
        {
            obj.transform.SetParent(canvas);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;
            T uI = obj.GetComponent<T>();

            StartCoroutine(FadeIn(uI, unityAction));
            Dic_UI.Add(uIType, uI);
        });
    }

    public void HideUI(UIType uIType, UnityAction unityAction = null)
    {

        if (Dic_UI.ContainsKey(uIType))
        {
            StartCoroutine(FadeOut(Dic_UI[uIType], unityAction));
            Dic_UI.Remove(uIType);
        }
    }

    public T GetUI<T>(UIType uIType) where T : UI_Base
    {
        if (Dic_UI.ContainsKey(uIType)) return Dic_UI[uIType] as T;
        return null;
    }

    public void Clear()
    {
        foreach (var uI in Dic_UI)
        {
            HideUI(uI.Key);
        }
    }

    #endregion

    #region  Private Methods

    IEnumerator FadeIn<T>(T uI, UnityAction<T> unityAction = null) where T : UI_Base
    {
        float time = 0f;
        float startAlpha = uI.canvasGroup.alpha;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            uI.canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, time / fadeDuration);
            yield return null;
        }
        uI.canvasGroup.alpha = 1f;

        unityAction?.Invoke(uI);
    }

    IEnumerator FadeOut(UI_Base uI, UnityAction unityAction = null)
    {
        float time = 0f;
        float startAlpha = uI.canvasGroup.alpha;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            uI.canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, time / fadeDuration);
            yield return null;
        }
        uI.canvasGroup.alpha = 0f;

        unityAction?.Invoke();
        GameObject.Destroy(uI.gameObject);
    }

    #endregion
}
