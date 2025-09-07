using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//UI基类
public class UI_Base : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public CanvasGroup canvasGroup;

    Dictionary<string, List<UIBehaviour>> controlDic = new();

    protected virtual void Awake()
    {
        FindChildrenControl<Text>();
        FindChildrenControl<Image>();
        FindChildrenControl<Button>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<Slider>();
        FindChildrenControl<ScrollRect>();
        FindChildrenControl<InputField>();

        canvasGroup.alpha = 0f;
    }

    #region  Public Methods

    //指针进入，点击，退出
    public virtual void OnPointerEnter(PointerEventData eventData) { }
    public virtual void OnPointerClick(PointerEventData eventData) { }
    public virtual void OnPointerExit(PointerEventData eventData) { }

    #endregion

    #region  Protected Methods

    //控件监听
    protected virtual void onClick(string btnName) { }
    protected virtual void onValueChanged(string toggleName, bool value) { }
    protected virtual void onValueChanged(string sliderName, float value) { }
    protected virtual void onValueChanged(string inputFieldName, string value) { }

    //获得控件
    protected T GetControl<T>(string controlName) where T : UIBehaviour
    {
        if (controlDic.ContainsKey(controlName))
        {
            for (int i = 0; i < controlDic[controlName].Count; i++)
            {
                if (controlDic[controlName][i] is T)
                    return controlDic[controlName][i] as T;
            }
        }
        return null;
    }

    #endregion

    #region  Private Methods

    //子控件全部添加监听
    void FindChildrenControl<T>() where T : UIBehaviour
    {
        T[] controls = GetComponentsInChildren<T>();
        for (int i = 0; i < controls.Length; i++)
        {
            string controlName = controls[i].gameObject.name;

            if (controlDic.ContainsKey(controlName)) controlDic[controlName].Add(controls[i]);
            else controlDic.Add(controlName, new List<UIBehaviour>() { controls[i] });

            if (controls[i] is Button)
            {
                (controls[i] as Button).onClick.AddListener(() =>
                {
                    onClick(controlName);
                });
            }
            else if (controls[i] is Toggle)
            {
                (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                {
                    onValueChanged(controlName, value);
                });
            }
            else if (controls[i] is Slider)
            {
                (controls[i] as Slider).onValueChanged.AddListener((value) =>
                {
                    onValueChanged(controlName, value);
                });
            }
            else if (controls[i] is InputField)
            {
                (controls[i] as InputField).onValueChanged.AddListener((value) =>
                {
                    onValueChanged(controlName, value);
                });
            }
        }
    }

    #endregion
}
