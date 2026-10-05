using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace HerculesThrow;

[BepInPlugin("com.DogEggz.HerculesThrow", "Hercules Throw", "0.2.0")]
public sealed class Plugin : BaseUnityPlugin
{
	public const string PluginGuid = "com.DogEggz.HerculesThrow";

	public const string PluginName = "Hercules Throw";

	public const string PluginVersion = "0.2.0";

	private const int VanillaThrowForce = 900;

	private const int MinimumThrowForce = 900;

	private const int MaximumThrowForce = 100000;

	private const int ThrowForceStep = 10;

	private const int DefaultBoatPushForceMultiplier = 5;

	private const int MinimumBoatPushForceMultiplier = 0;

	private const int MaximumBoatPushForceMultiplier = 100000;

	private const int BoatPushForceMultiplierStep = 5;

	private const float VanillaDockPushForceMultiplier = -0.55f;

	private const float MinimumDockPushForceMultiplier = -100000f;

	private const float MaximumDockPushForceMultiplier = -0.55f;

	private const float DockPushForceMultiplierStep = 5f;

	private Harmony harmony;

	internal static ConfigEntry<int> ThrowForce { get; private set; }

	internal static ConfigEntry<int> BoatPushForceMultiplier { get; private set; }

	internal static ConfigEntry<float> DockPushForceMultiplier { get; private set; }

	private void Awake()
	{
		ThrowForce = base.Config.Bind("Settings", "Throw force", 900, new ConfigDescription("Controls the force used when throwing an item with the desktop throw control. Vanilla is 900.", new SteppedAcceptableIntRange(900, 100000, 10), new ConfigurationManagerAttributes
		{
			ShowRangeAsPercent = false
		}));
		BoatPushForceMultiplier = base.Config.Bind("Settings", "Boat push force multiplier", 5, new ConfigDescription("Overrides the force multiplier used by every boat push collider. Default is 5.", new SteppedAcceptableIntRange(0, 100000, 5), new ConfigurationManagerAttributes
		{
			ShowRangeAsPercent = false
		}));
		DockPushForceMultiplier = base.Config.Bind("Settings", "Dock push force multiplier", -0.55f, new ConfigDescription("Replaces the vanilla dock push multiplier. Vanilla is -0.55; more-negative values push harder in the same direction.", new SteppedAcceptableFloatRange(-100000f, -0.55f, 5f, 0f), new ConfigurationManagerAttributes
		{
			ShowRangeAsPercent = false
		}));
		harmony = new Harmony("com.DogEggz.HerculesThrow");
		harmony.PatchAll(Assembly.GetExecutingAssembly());
		base.Logger.LogInfo("Hercules Throw 0.2.0 loaded.");
	}

	private void OnDestroy()
	{
		harmony?.UnpatchSelf();
	}
}
