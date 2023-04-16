using System;
using UnityEngine;

namespace Yukgaejang.Utility
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;

		private static readonly object _lock = new object();

		public static T Instance
		{
			get
			{
				if (applicationIsQuitting)
				{
					Debug.LogWarning("[Singleton] Instance '" + typeof(T) + "' already destroyed on application quit. Won't create again - returning null.");
					return null;
				}

				lock (_lock)
				{
					if (_instance == null)
					{
						var all = Resources.FindObjectsOfTypeAll<T>();
						_instance = all != null && all.Length > 0 ? all[0] : null;

						if (all != null && all.Length > 1)
						{
							Debug.LogWarning("[Singleton] There are " + all.Length + " instances of " + typeof(T) +
											 "... This may happen if your singleton is also a prefab, in which case there is nothing to worry about.");
							return _instance;
						}

						if (_instance == null)
						{
							GameObject singleton = new GameObject();
							_instance = singleton.AddComponent<T>();
							singleton.name = "(singleton) " + typeof(T).ToString();

							if (Application.isPlaying)
								DontDestroyOnLoad(singleton);

							Debug.Log("[Singleton] An instance of " + typeof(T) + " is needed in the scene, so '" + singleton + "' was created with DontDestroyOnLoad.");
						}
						else
						{
							Debug.Log("[Singleton] Using instance already created: " +
									  _instance.gameObject.name);
						}
					}

					return _instance;
				}
			}
		}

		private static bool applicationIsQuitting = false;

		public void OnDestroy()
		{
			applicationIsQuitting = true;
		}
	}
}