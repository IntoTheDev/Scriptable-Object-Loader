#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace ToolBox.Loader
{
	public class StorageBuildProcessor : IPreprocessBuildWithReport, IPostprocessBuildWithReport
	{
		private ScriptableObject[] _loadables = null;

		public int callbackOrder => 0;

		public void OnPreprocessBuild(BuildReport report)
		{
			if (!AssetDatabase.IsValidFolder("Assets/Resources"))
				AssetDatabase.CreateFolder("Assets", "Resources");

			var loadables = Resources.FindObjectsOfTypeAll<ScriptableObject>().Where(x => x is ILoadable).ToArray();
			_loadables = new ScriptableObject[loadables.Length];

			for (int i = 0; i < loadables.Length; i++)
			{
				var loadable = loadables[i];
				var copy = Object.Instantiate(loadable);
				var path = $"Assets/Resources/{loadable.name}.asset";

				AssetDatabase.CreateAsset(copy, path);
				_loadables[i] = copy;
			}

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

		public void OnPostprocessBuild(BuildReport report)
		{
			foreach (var loadable in _loadables)
				Object.DestroyImmediate(loadable, true);

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}
}
#endif
