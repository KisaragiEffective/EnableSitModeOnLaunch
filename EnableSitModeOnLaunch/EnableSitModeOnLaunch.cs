using FrooxEngine;
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
        public override string Version => "0.1.2";

        public override void OnEngineInit()
        {
            Engine.Current.RunPostInit(() => Engine.Current.InputInterface.SeatedMode = true);
        }
    }
}
