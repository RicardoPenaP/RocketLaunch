
namespace Settings
{
    public enum QualityOptions { Ultra, High, Mid, Low}
    public enum TargetFPSOptions { Low, Mid, High, Ultra, Unlimited}


    public static class SettingsData
    {
        public const QualityOptions DEFAULT_QUALITY = QualityOptions.Ultra;
        public const int DEFAULT_TARGET_FPS = 60;
        public const bool DEFAULT_FULLSCREEN_MODE_STATE = true;
    }
}


