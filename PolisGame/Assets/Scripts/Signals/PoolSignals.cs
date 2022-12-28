using System;
using Enums;
using Extension;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class PoolSignals : MonoSingleton<PoolSignals>
    {
        public Func<string, Transform, GameObject> onGetPoolObject = delegate{ return default; };
        public UnityAction<PoolType,GameObject> onReleasePoolObject = delegate {  };
    }
}
