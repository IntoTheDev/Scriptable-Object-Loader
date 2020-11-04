#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

public abstract class Config
#if ODIN_INSPECTOR
	: SerializedScriptableObject
#else
	: ScriptableObject
#endif
{
	private static Config[] _configs = null;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void Load() =>
		_configs = Resources.LoadAll<Config>("");

	public static T Get<T>() where T : Config
	{
		for (int i = 0; i < _configs.Length; i++)
			if (_configs[i] is T config)
				return config;

		return null;
	}
}
