//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LHitEffectsData_Input.proto
// Note: requires additional types generated from: LHitShockData_Input.proto
// Note: requires additional types generated from: LHitEffectData_Input.proto
// Note: requires additional types generated from: LHitSoundData_Input.proto
// Note: requires additional types generated from: LHitBuff_Input.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LHitEffectsData_Input")]
  public partial class LHitEffectsData_Input : global::ProtoBuf.IExtensible
  {
    public LHitEffectsData_Input() {}
    
    private int _defaultEffectType = (int)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"defaultEffectType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int defaultEffectType
    {
      get { return _defaultEffectType; }
      set { _defaultEffectType = value; }
    }
    private ActionData.LHitShockData_Input _shockData = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"shockData", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LHitShockData_Input shockData
    {
      get { return _shockData; }
      set { _shockData = value; }
    }
    private readonly global::System.Collections.Generic.List<ActionData.LHitEffectData_Input> _effectList = new global::System.Collections.Generic.List<ActionData.LHitEffectData_Input>();
    [global::ProtoBuf.ProtoMember(3, Name=@"effectList", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<ActionData.LHitEffectData_Input> effectList
    {
      get { return _effectList; }
    }
  
    private readonly global::System.Collections.Generic.List<ActionData.LHitSoundData_Input> _hitSoundDataList = new global::System.Collections.Generic.List<ActionData.LHitSoundData_Input>();
    [global::ProtoBuf.ProtoMember(4, Name=@"hitSoundDataList", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<ActionData.LHitSoundData_Input> hitSoundDataList
    {
      get { return _hitSoundDataList; }
    }
  
    private readonly global::System.Collections.Generic.List<ActionData.LHitBuff_Input> _hitBuffList = new global::System.Collections.Generic.List<ActionData.LHitBuff_Input>();
    [global::ProtoBuf.ProtoMember(5, Name=@"hitBuffList", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<ActionData.LHitBuff_Input> hitBuffList
    {
      get { return _hitBuffList; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}