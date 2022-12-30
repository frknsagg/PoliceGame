using Enums;
using Extension;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onLevelFailed = delegate {  };
        public UnityAction onStealFinish = delegate {  };
    }
}
