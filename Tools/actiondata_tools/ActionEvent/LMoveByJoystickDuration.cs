//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LMoveByJoystickDuration.proto
// Note: requires additional types generated from: LTimeEventProType.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LMoveByJoystickDuration")]
  public partial class LMoveByJoystickDuration : global::ProtoBuf.IExtensible
  {
    public LMoveByJoystickDuration() {}
    
    private float _speed = (float)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"speed", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float speed
    {
      get { return _speed; }
      set { _speed = value; }
    }
    private bool _useSpeedRate = default(bool);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"useSpeedRate", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool useSpeedRate
    {
      get { return _useSpeedRate; }
      set { _useSpeedRate = value; }
    }
    private ActionData.LTimeEventProType _eventType;
    [global::ProtoBuf.ProtoMember(101, IsRequired = true, Name=@"eventType", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ActionData.LTimeEventProType eventType
    {
      get { return _eventType; }
      set { _eventType = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}