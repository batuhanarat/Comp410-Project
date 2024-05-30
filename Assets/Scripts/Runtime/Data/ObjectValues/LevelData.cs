using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Enums;

[Serializable]
public struct ObjectData
{
    public List<ObjectType> Targets;
    public List<byte> TargetCounts;
    public List<ObjectType> RemainingObjects;
    public List<byte> RemainingObjectsCounts;
}


[Serializable]
public struct LevelData
{
    public ObjectData ClawObject;
    
    public LevelData(ObjectData data)
    {
        ClawObject = data;
    }
}
