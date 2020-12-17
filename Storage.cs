using System.Linq;
using UnityEngine;

namespace ToolBox.Loader
{
	public static class Storage
	{
		private static ILoadable[] _assets = null;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Setup()
		{
#if UNITY_EDITOR
			var assets = Resources.FindObjectsOfTypeAll<ScriptableObject>();
#else
			var assets = Resources.LoadAll<ScriptableObject>("");
#endif
			_assets = assets
				.Where(x => x is ILoadable)
				.Cast<ILoadable>()
				.ToArray();

			Debug.LogError($"Assets count: {_assets.Length}");
		}

		public static T Get<T>() where T : ILoadable
		{
			for (int i = 0; i < _assets.Length; i++)
				if (_assets[i] is T asset)
					return asset;

			return default;
		}
	}

	public interface ILoadable { }
}
