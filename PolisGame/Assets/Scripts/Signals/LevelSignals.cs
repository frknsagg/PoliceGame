using System.Collections.Generic;
using Enums;
using Extension;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class LevelSignals : MonoSingleton<LevelSignals>
    {
        public UnityAction onLevelCompleted = delegate { };
        public UnityAction<EnemyTypes,int> onLevelCreate = delegate { };
    }
}
