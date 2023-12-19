using UnityEngine;

namespace Settings
{    
    public static class SettingsController
    {
        private const QualityOptions DEFAULT_QUALITY = QualityOptions.Ultra;

        public static float FpsTarget { get; private set; }

        public static void ChangeQuality(QualityOptions qualityOption)
        {
            QualitySettings.SetQualityLevel((int)qualityOption, false);
        }
    }
}

