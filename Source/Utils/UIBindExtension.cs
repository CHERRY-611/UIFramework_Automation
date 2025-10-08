using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class UIBindExtension
{
	public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
	{
		return UIUtil.GetOrAddComponent<T>(go);
	}

	public static void BindEvent(this GameObject go, Action<PointerEventData> action, UIEventDefine.UIEvent type = UIEventDefine.UIEvent.Click)
	{
		UI_Base.BindEvent(go, action, type);
	}
}