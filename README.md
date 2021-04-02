# Scriptable Object Loader
Load Scriptable Objects via code

### TODO
- [ ] Replace ```Resources``` with ```Asset Bundles``` or ```Addressables```
- [x] Git Package

## How to Install
### Git Installation (Best way to get latest version)

If you have Git on your computer, you can open Package Manager indside Unity, select "Add package from Git url...", and paste link ```https://github.com/IntoTheDev/Scriptable-Object-Loader.git```

or

Open the manifest.json file of your Unity project.
Add ```"com.intothedev.loader": "https://github.com/IntoTheDev/Scriptable-Object-Loader.git"```

### Manual Installation (Version can be outdated)
Download latest package from the Release section.
Import Storage.unitypackage to your Unity Project

## Usage

### Creating ScriptableObject:
Your ```ScriptableObject``` must implement ```ILoadable``` interface.

```csharp
using ToolBox.Loader;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Configs/Player")]
public class PlayerConfig : ScriptableObject, ILoadable
{
	[SerializeField] private float _startHealth = 100f;

	public float StartHealth => _startHealth;
}
```

Now you need to create an asset.

### Get Data from ScriptableObject:

```csharp
using ToolBox.Loader;
using UnityEngine;

public class Player : MonoBehaviour
{
	private float _health = 0f;

	private void Awake()
	{
		// Get single asset
		_health = Storage.Get<PlayerConfig>().StartHealth;
		
		// Get all assets
		var configs = Storage.GetAll<PlayerConfig>();
		
		foreach (var config in configs)
		{
			// Do something
		}
	}
}
```
