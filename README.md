# Configs
Load Scriptable Objects via code

## Usage

### Creating ScriptableObject:

```csharp
	[CreateAssetMenu(menuName = "Game/Configs/Player")]
	public class PlayerConfig : ScriptableObject
	{
		[SerializeField] private float _startHealth = 100f;

		public float StartHealth => _startHealth;
	}
```

Now you need to create an asset and put it in Resources folder

### Get Data from ScriptableObject:

```csharp
	public class Player : MonoBehaviour
	{
		private float _health = 0f;

		private void Awake()
		{
			_health = Storage.Get<PlayerConfig>().StartHealth;
		}
	}
```
