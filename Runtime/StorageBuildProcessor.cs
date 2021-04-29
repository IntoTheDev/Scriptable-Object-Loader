#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace ToolBox.Loader.Editor
{
	public class StorageBuildProcessor : IPreprocessBuildWithReport
	{
		public int callbackOrder => 0;

		public void OnPreprocessBuild(BuildReport report)
		{
			var assets = EditorStorage.GetAllAssetsOfType<ScriptableObject>();
			var loadables = assets.Where(x => x is ILoadable).ToArray();
			var initializables = assets.Where(x => x is IInitializableBeforeBuild).Cast<IInitializableBeforeBuild>();

			Resources.Load<Storage>("ToolBoxStorage").SetAssets(loadables);

			foreach (var initializable in initializables)
				initializable.Init();
		}
	}
}
#endif
