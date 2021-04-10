using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using ToolBox.Loader.Editor;
#endif
using UnityEngine;

namespace ToolBox.Loader
{
	public static class Storage
	{
		private static ScriptableObject[] _assets = null;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Setup()
		{
#if UNITY_EDITOR
			var assets = EditorStorage.GetAllAssetsOfType<ScriptableObject>().ToArray();
#else
			var assets = Resources.LoadAll<ScriptableObject>("");
#endif
			_assets = assets
				.Where(x => x is ILoadable)
				.ToArray();
		}

		public static T Get<T>() where T : ScriptableObject, ILoadable
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
				_assets = EditorStorage.GetAllAssetsOfType<ScriptableObject>().ToArray();
#endif

			for (int i = 0; i < _assets.Length; i++)
				if (_assets[i] is T asset)
					return asset;

			return null;
		}

		public static IEnumerable<T> GetAll<T>() where T : ScriptableObject, ILoadable
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
				_assets = EditorStorage.GetAllAssetsOfType<ScriptableObject>().ToArray();
#endif

			return _assets.Where(a => a is T).Cast<T>();
		}
	}
}
