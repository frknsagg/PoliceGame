using System;
using System.Collections;
using System.Collections.Generic;
using Data.ValueObject;
using Enums;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class GunData
{
   public SerializedDictionary<GunTypes, GunSpecs> GunDatas;
}
