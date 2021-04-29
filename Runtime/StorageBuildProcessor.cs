#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace ToolBox.Loader.Editor
{
	public class StorageBuildProcessor : IPreprocessBuildWithReport
	{
		public int callbackOrder => 0;

		public void OnPreprocessBuild(BuildReport report) =>
			Resources.Load<Storage>("ToolBoxStorage").LoadAssets();
	}
}
#endif
