using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
	public static T GetOrAddComponent<T>(this GameObject go) where T : Component
	{
		return Util.GetOrAddComponent<T>(go);
	}

	public static void BindEvent(this GameObject go, Action<PointerEventData> action = null, EUIEvent type = EUIEvent.Click)
	{
		UI_Base.BindEvent(go, action, type);
	}

	public static bool IsValid(this GameObject go)
	{
		return go != null && go.activeSelf;
	}

	/*public static void MakeMask(this ref LayerMask mask, List<Define.ELayer> list)
	{
		foreach (Define.ELayer layer in list)
			mask |= (1 << (int)layer);
	}

	public static void AddLayer(this ref LayerMask mask, Define.ELayer layer)
	{
		mask |= (1 << (int)layer);
	}

	public static void RemoveLayer(this ref LayerMask mask, Define.ELayer layer)
	{
		mask &= ~(1 << (int)layer);
	}*/

	public static void Shuffle<T>(this IList<T> list)
	{
		int n = list.Count;

		while (n > 1)
		{
			n--;
			int k = UnityEngine.Random.Range(0, n + 1);
			(list[k], list[n]) = (list[n], list[k]); //swap
		}
	}

	public static void SetPositionAndRotation(this Transform transform, Transform point)
	{
        transform.SetPositionAndRotation(point.position, point.rotation);

    }

	public static T GetComponentInParents<T>(this Transform transform, bool includeInactive) where T : Component
    {
		T target = null;

		if (transform.parent != null)
		{
            target = transform.GetComponentInParent<T>(includeInactive);

			if (target == null)
			{
				target = GetComponentInParents<T>(transform.parent, includeInactive);
            }
        }

		return target;
    }

    public static void GameLog(this MonoBehaviour mono, string log, bool onlyMaster = false)
    {
#if UNITY_EDITOR
        string addText = onlyMaster ? "(OnlyMaster)" : string.Empty;
        string colorCode = onlyMaster ? "#174FE5" : "#00D908";
        Debug.Log($"<color={colorCode}>GameLogic{addText}::</color>{log}");
#endif
    }
}
