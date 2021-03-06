//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LAddSceneEffect.proto
// Note: requires additional types generated from: LTimeEventProType.proto
// Note: requires additional types generated from: LVector3.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LAddSceneEffect")]
  public partial class LAddSceneEffect : global::ProtoBuf.IExtensible
  {
    public LAddSceneEffect() {}
    
    private float _lifeTime = (float)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"lifeTime", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float lifeTime
    {
      get { return _lifeTime; }
      set { _lifeTime = value; }
    }
    private ActionData.LAddSceneEffect.EffectType _effectName = ActionData.LAddSceneEffect.EffectType.None;
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"effectName", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(ActionData.LAddSceneEffect.EffectType.None)]
    public ActionData.LAddSceneEffect.EffectType effectName
    {
      get { return _effectName; }
      set { _effectName = value; }
    }
    private ActionData.LVector3 _position = null;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"position", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 position
    {
      get { return _position; }
      set { _position = value; }
    }
    private ActionData.LVector3 _rotation = null;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"rotation", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 rotation
    {
      get { return _rotation; }
      set { _rotation = value; }
    }
    private ActionData.LVector3 _scale = null;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"scale", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 scale
    {
      get { return _scale; }
      set { _scale = value; }
    }
    private bool _fixGround = default(bool);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"fixGround", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool fixGround
    {
      get { return _fixGround; }
      set { _fixGround = value; }
    }
    private bool _isBindAction = (bool)true;
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"isBindAction", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)true)]
    public bool isBindAction
    {
      get { return _isBindAction; }
      set { _isBindAction = value; }
    }
    private bool _modifyMapColor = (bool)false;
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"modifyMapColor", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool modifyMapColor
    {
      get { return _modifyMapColor; }
      set { _modifyMapColor = value; }
    }
    private float _brightness = (float)0.84;
    [global::ProtoBuf.ProtoMember(13, IsRequired = false, Name=@"brightness", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0.84)]
    public float brightness
    {
      get { return _brightness; }
      set { _brightness = value; }
    }
    private float _contrast = (float)2.03;
    [global::ProtoBuf.ProtoMember(14, IsRequired = false, Name=@"contrast", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)2.03)]
    public float contrast
    {
      get { return _contrast; }
      set { _contrast = value; }
    }
    private float _saturation = (float)1.51;
    [global::ProtoBuf.ProtoMember(15, IsRequired = false, Name=@"saturation", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)1.51)]
    public float saturation
    {
      get { return _saturation; }
      set { _saturation = value; }
    }
    private ActionData.LVector3 _addColor = null;
    [global::ProtoBuf.ProtoMember(16, IsRequired = false, Name=@"addColor", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 addColor
    {
      get { return _addColor; }
      set { _addColor = value; }
    }
    private ActionData.LTimeEventProType _eventType;
    [global::ProtoBuf.ProtoMember(7, IsRequired = true, Name=@"eventType", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ActionData.LTimeEventProType eventType
    {
      get { return _eventType; }
      set { _eventType = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"EffectType")]
    public enum EffectType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"None", Value=1)]
      None = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"UI_Radiate_001", Value=2)]
      UI_Radiate_001 = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"UI_Radiate_002", Value=3)]
      UI_Radiate_002 = 3
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}