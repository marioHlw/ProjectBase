//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LMoveToNearTarget.proto
// Note: requires additional types generated from: LTimeEventProType.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LMoveToNearTarget")]
  public partial class LMoveToNearTarget : global::ProtoBuf.IExtensible
  {
    public LMoveToNearTarget() {}
    
    private float _speed = (float)1;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"speed", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)1)]
    public float speed
    {
      get { return _speed; }
      set { _speed = value; }
    }
    private float _distance = (float)5;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"distance", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)5)]
    public float distance
    {
      get { return _distance; }
      set { _distance = value; }
    }
    private float _rate = (float)1;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"rate", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)1)]
    public float rate
    {
      get { return _rate; }
      set { _rate = value; }
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
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"eventType", DataFormat = global::ProtoBuf.DataFormat.Default)]
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