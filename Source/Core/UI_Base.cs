using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();
    protected SceneLoader _sceneLoader = new SceneLoader();
    public abstract void Init();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = UIUtil.FindChild(gameObject, names[i], true);
            else
                objects[i] = UIUtil.FindChild<T>(gameObject, names[i], true);
        }
    }

    public static void BindEvent(GameObject go, Action<PointerEventData> action, UIEventDefine.UIEvent type = UIEventDefine.UIEvent.Click)
    {
        UI_EventHandler evt = UIUtil.GetOrAddComponent<UI_EventHandler>(go);    
        
        switch (type)
        {
            case UIEventDefine.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case UIEventDefine.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
            case UIEventDefine.UIEvent.DownClick:
                evt.OnDownHandler -= action;
                evt.OnDownHandler += action;
                break;
            case UIEventDefine.UIEvent.UpClick:
                evt.OnUPHandler -= action;
                evt.OnUPHandler += action;
                break;
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }


    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }

}