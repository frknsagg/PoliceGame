using Extension;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PrefsSignals : MonoSingleton<PrefsSignals>
    {

        public UnityAction<float, int, int> OnSaveLevelData = delegate { };
    }
}
