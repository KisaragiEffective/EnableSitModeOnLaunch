using FrooxEngine;
using FrooxEngine.UIX;
using HarmonyLib;
using JetBrains.Annotations;
using NeosModLoader;

namespace EnableSitModeOnLaunch
{
    // Please do not use LocaleString. It will require System.ValueTuple @ 4.0.3.0 which is not solvable
    // with somewhat reason.
    [UsedImplicitly]
    public class EnableSitModeOnLaunch : NeosMod
    {
        public override string Name => "EnableSitModeOnLaunch";
        public override string Author => "kisaragi marine";
        public override string Version => "0.1.1";

        public override void OnEngineInit()
        {
            var harmony = new Harmony("com.github.kisaragieffective.neos.EnableSitModeOnLaunch");
            harmony.PatchAll();
            Msg("Injected");
        }
    }

    [HarmonyPatch(typeof(SeatedModeFacetPreset), "Build")]
    [UsedImplicitly]
    internal class Patcher
    {
        // ReSharper disable once UnusedParameter.Local
        [UsedImplicitly]
        private static bool Prefix(Facet facet, Slot root)
        {
            var ui = new UIBuilder(root);
            RadiantUI_Constants.SetupDefaultStyle(ui);
            var trackingSpaceSync = root.AttachComponent<UserTrackingSpaceSync>();
            trackingSpaceSync.SeatedMode.Value = true;
            // TODO: this should be translated (blocked by local build issue)
            ui.Button(NeosAssets.Graphics.Icons.General.ExitAvatarAnchor, (LocaleString) "---").SetupToggle(trackingSpaceSync.SeatedMode, new OptionDescription<bool>
            {
                spriteUrl = NeosAssets.Graphics.Icons.General.EnterAvatarAnchor,
                label = "Options.SeatedMode.On"
            }, new OptionDescription<bool>
            {
                spriteUrl = NeosAssets.Graphics.Icons.General.ExitAvatarAnchor,
                label = "Options.SeatedMode.Off"
            });
            return false; // overwrite behavior
        }
    }
}
