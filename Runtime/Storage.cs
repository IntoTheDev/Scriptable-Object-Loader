using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
#if UNITY_EDITOR
using ToolBox.Loader.Editor;
using UnityEditor;
#endif
using UnityEngine;

namespace ToolBox.Loader
{
	public class Storage : ScriptableObject
	{
		[SerializeField] private ScriptableObject[] _assets = null;

		private static ILoadable[] _loadables = new ILoadable[0];

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Setup()
		{
#if UNITY_EDITOR
			LoadAssetsInEditor();
#else
			_loadables = Resources.Load<Storage>("ToolBoxStorage")._assets.Cast<ILoadable>().ToArray();
#endif
			for (int i = 0; i < _loadables.Length; i++)
				_loadables[i].Load();
		}

		public static T Get<T>() where T : ScriptableObject, ILoadable
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
				LoadAssetsInEditor();
#endif

			for (int i = 0; i < _loadables.Length; i++)
				if (_loadables[i] is T loadable)
					return loadable;

			return null;
		}

		public static IEnumerable<T> GetAll<T>() where T : ScriptableObject, ILoadable
		{
#if UNITY_EDITOR
			if (!Application.isPlaying)
				LoadAssetsInEditor();
#endif

			return _loadables.Where(a => a is T).Cast<T>();
		}

#if UNITY_EDITOR
		private static void LoadAssetsInEditor()
		{
			_loadables = EditorStorage.
				GetAllAssetsOfType<ScriptableObject>().
				Where(x => x is ILoadable).
				Cast<ILoadable>().
				ToArray();
		}

		internal void LoadAssets()
		{
			// I don't know why but if you haven't selected this asset, array will be empty in build and after
			Selection.activeObject = this;

			var assets = EditorStorage.GetAllAssetsOfType<ScriptableObject>();
			var loadables = assets.Where(x => x is ILoadable).ToArray();
			var initializables = assets.Where(x => x is IInitializableBeforeBuild).Cast<IInitializableBeforeBuild>();

			_assets = new ScriptableObject[loadables.Length];
			Array.Copy(loadables, _assets, loadables.Length);

			foreach (var initializable in initializables)
				initializable.Init();
		}
#endif
	}
}
