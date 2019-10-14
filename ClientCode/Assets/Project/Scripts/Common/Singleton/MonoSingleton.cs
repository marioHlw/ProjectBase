using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : Component
{
	private static bool _destroyed;
	private static T _instance;

	// Methods
	protected virtual void Awake()
	{
		if ((MonoSingleton<T>._instance != null) && (MonoSingleton<T>._instance.gameObject != base.gameObject))
		{
			if (Application.isPlaying)
			{
				Object.Destroy(base.gameObject);
			}
			else
			{
				Object.DestroyImmediate(base.gameObject);
			}
		}
		else if (MonoSingleton<T>._instance == null)
		{
			MonoSingleton<T>._instance = base.GetComponent<T>();
		}

		Object.DontDestroyOnLoad(base.gameObject);

		Init();
	}

	public static void ClearDestroy()
	{
		MonoSingleton<T>.DestroyInstance();

		MonoSingleton<T>._destroyed = false;
	}

	public static void DestroyInstance()
	{
		if (MonoSingleton<T>._instance != null)
		{
			Object.Destroy(MonoSingleton<T>._instance.gameObject);
		}

		MonoSingleton<T>._destroyed = true;

		MonoSingleton<T>._instance = null;
	}

	public static T GetInstance()
	{
		if ((MonoSingleton<T>._instance == null) && !MonoSingleton<T>._destroyed)
		{
			MonoSingleton<T>._instance = (T)UnityEngine.Object.FindObjectOfType(typeof(T));

			if (MonoSingleton<T>._instance == null)
			{
				object[] customAttributes = typeof(T).GetCustomAttributes(typeof(AutoSingletonAttribute), true);

				if ((customAttributes.Length > 0) && !((AutoSingletonAttribute)customAttributes[0]).bAutoCreate)
				{
					return null;
				}

				GameObject obj2 = new GameObject(typeof(T).Name);

				MonoSingleton<T>._instance = obj2.AddComponent<T>();

				GameObject obj3 = GameObject.Find("GameFramework");

				if (obj3 != null)
				{
					obj2.transform.SetParent(obj3.transform);
				}
			}
		}

		return MonoSingleton<T>._instance;
	}

	public static bool HasInstance()
	{
		return MonoSingleton<T>._instance != null;
	}

	protected virtual void Init()
	{

	}

	protected virtual void OnDestroy()
	{
		if ((MonoSingleton<T>._instance != null) && (MonoSingleton<T>._instance.gameObject == base.gameObject))
		{
			MonoSingleton<T>._instance = null;
		}
	}

	// Properties
	public static T instance()
	{
		return MonoSingleton<T>.GetInstance();
	}
}

public class AutoSingletonAttribute : System.Attribute
{
	public bool bAutoCreate;

	public AutoSingletonAttribute(bool bCreate)
	{
		bAutoCreate = bCreate;
	}
}



