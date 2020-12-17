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
		public int callbackOrder => 0;

		public void OnPostprocessBuild(BuildReport report)
		{
			var loadables = Resources.FindObjectsOfTypeAll<ScriptableObject>()
				.Where(x => x is ILoadable 
				&& AssetDatabase.GetAssetPath(x).Contains("Resources"));

			foreach (var loadable in loadables)
				Object.DestroyImmediate(loadable, true);

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}

		public void OnPreprocessBuild(BuildReport report)
		{
			if (!AssetDatabase.IsValidFolder("Assets/Resources"))
				AssetDatabase.CreateFolder("Assets", "Resources");

			var loadables = Resources.FindObjectsOfTypeAll<ScriptableObject>().Where(x => x is ILoadable);

			foreach (var loadable in loadables)
			{
				var copy = Object.Instantiate(loadable);
				var path = $"Assets/Resources/{loadable.name}.asset";

				AssetDatabase.CreateAsset(copy, path);
			}

			AssetDatabase.SaveAssets();
			AssetDatabase.Refresh();
		}
	}
}
#endif
