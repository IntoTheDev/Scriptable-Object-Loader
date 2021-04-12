#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
#if ODIN_INSPECTOR
using Sirenix.Utilities.Editor;
#endif

namespace ToolBox.Loader.Editor
{
	public static class EditorStorage
	{
		public static List<T> GetAllAssetsOfType<T>() where T : Object
		{
#if ODIN_INSPECTOR
			var assets = AssetUtilities.GetAllAssetsOfType<T>().ToList();
#else
			var paths = AssetDatabase.GetAllAssetPaths();
			var assets = new List<T>();

			foreach (var path in paths)
			{
				var asset = AssetDatabase.LoadAssetAtPath<T>(path);

				if (asset is T value)
					assets.Add(value);
			}

#endif
			return assets;
		}
	}
}
#endif
