# Scriptable Object Loader
Load Scriptable Objects via code

## Usage

### Creating ScriptableObject:
Your ```ScriptableObject``` must implement ```ILoadable``` interface.

```csharp
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

	public class Player : MonoBehaviour
	{
		private float _health = 0f;

		private void Awake()
		{
			_health = Storage.Get<PlayerConfig>().StartHealth;
		}
	}
```
