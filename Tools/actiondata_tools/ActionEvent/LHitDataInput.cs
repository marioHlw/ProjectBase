//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LHitDataInput.proto
// Note: requires additional types generated from: LHitBehaviorData_Input.proto
// Note: requires additional types generated from: LHitShockData_Input.proto
// Note: requires additional types generated from: LHitEffectData_Input.proto
// Note: requires additional types generated from: LHitSoundData_Input.proto
// Note: requires additional types generated from: LHitBuff_Input.proto
// Note: requires additional types generated from: LShakeCamera.proto
// Note: requires additional types generated from: LDamageFormula.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LHitDataInput")]
  public partial class LHitDataInput : global::ProtoBuf.IExtensible
  {
    public LHitDataInput() {}
    
    private string _info = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"info", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string info
    {
      get { return _info; }
      set { _info = value; }
    }
    private int _hitId = (int)0;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"hitId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int hitId
    {
      get { return _hitId; }
      set { _hitId = value; }
    }
    private bool _enabled = (bool)true;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"enabled", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)true)]
    public bool enabled
    {
      get { return _enabled; }
      set { _enabled = value; }
    }
    private bool _hitPointByBearer = (bool)false;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"hitPointByBearer", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool hitPointByBearer
    {
      get { return _hitPointByBearer; }
      set { _hitPointByBearer = value; }
    }
    private ActionData.LHitBehaviorData_Input _hitBehaviorData_Input = null;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"hitBehaviorData_Input", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LHitBehaviorData_Input hitBehaviorData_Input
    {
      get { return _hitBehaviorData_Input; }
      set { _hitBehaviorData_Input = value; }
    }
    private int _defaultEffectType = (int)0;
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"defaultEffectType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int defaultEffectType
    {
      get { return _defaultEffectType; }
      set { _defaultEffectType = value; }
    }
    private ActionData.LHitShockData_Input _shockData = null;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"shockData", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LHitShockData_Input shockData
    {
      get { return _shockData; }
      set { _shockData = value; }
    }
    private ActionData.LShakeCamera _shakeCamera = null;
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"shakeCamera", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LShakeCamera shakeCamera
    {
      get { return _shakeCamera; }
      set { _shakeCamera = value; }
    }
    private readonly global::System.Collections.Generic.List<ActionData.LHitEffectData_Input> _effectList = new global::System.Collections.Generic.List<ActionData.LHitEffectData_Input>();
    [global::ProtoBuf.ProtoMember(8, Name=@"effectList", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<ActionData.LHitEffectData_Input> effectList
    {
      get { return _effectList; }
    }
  
    private ActionData.LHitSoundData_Input _hitSoundData = null;
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"hitSoundData", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LHitSoundData_Input hitSoundData
    {
      get { return _hitSoundData; }
      set { _hitSoundData = value; }
    }
    private readonly global::System.Collections.Generic.List<ActionData.LHitBuff_Input> _hitBuffList = new global::System.Collections.Generic.List<ActionData.LHitBuff_Input>();
    [global::ProtoBuf.ProtoMember(10, Name=@"hitBuffList", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<ActionData.LHitBuff_Input> hitBuffList
    {
      get { return _hitBuffList; }
    }
  
    private readonly global::System.Collections.Generic.List<ActionData.LDamageFormula> _damageFormula = new global::System.Collections.Generic.List<ActionData.LDamageFormula>();
    [global::ProtoBuf.ProtoMember(13, Name=@"damageFormula", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<ActionData.LDamageFormula> damageFormula
    {
      get { return _damageFormula; }
    }
  
    private bool _ignoreDamageProtection = default(bool);
    [global::ProtoBuf.ProtoMember(14, IsRequired = false, Name=@"ignoreDamageProtection", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool ignoreDamageProtection
    {
      get { return _ignoreDamageProtection; }
      set { _ignoreDamageProtection = value; }
    }
    private bool _showDamage = (bool)true;
    [global::ProtoBuf.ProtoMember(15, IsRequired = false, Name=@"showDamage", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)true)]
    public bool showDamage
    {
      get { return _showDamage; }
      set { _showDamage = value; }
    }
    private int _buffId = (int)0;
    [global::ProtoBuf.ProtoMember(16, IsRequired = false, Name=@"buffId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int buffId
    {
      get { return _buffId; }
      set { _buffId = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}