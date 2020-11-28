using UnityEngine;

public static class Storage
{
	private static ScriptableObject[] _assets = Resources.LoadAll<ScriptableObject>("");

	public static T Get<T>() where T : ScriptableObject
	{
		for (int i = 0; i < _assets.Length; i++)
			if (_assets[i] is T asset)
				return asset;

		return null;
	}
}
