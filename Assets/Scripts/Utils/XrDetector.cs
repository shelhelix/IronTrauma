using UnityEngine.XR.Management;

namespace IronTrauma.Utils {
    public class XrDetector {
        public static bool IsXrActive {
            get {
                var settings = XRGeneralSettings.Instance;
                if ( !settings ) {
                    return false;
                }
                var manager = settings.Manager;
                if ( !manager ) {
                    return false;
                }
                return manager.activeLoader;
            }
        }
    }
}
