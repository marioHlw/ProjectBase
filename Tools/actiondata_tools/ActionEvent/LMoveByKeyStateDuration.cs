//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LMoveByKeyStateDuration.proto
// Note: requires additional types generated from: LTimeEventProType.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LMoveByKeyStateDuration")]
  public partial class LMoveByKeyStateDuration : global::ProtoBuf.IExtensible
  {
    public LMoveByKeyStateDuration() {}
    
    private float _speedX = (float)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"speedX", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float speedX
    {
      get { return _speedX; }
      set { _speedX = value; }
    }
    private float _speedY = (float)0;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"speedY", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float speedY
    {
      get { return _speedY; }
      set { _speedY = value; }
    }
    private float _speedZ = (float)0;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"speedZ", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float speedZ
    {
      get { return _speedZ; }
      set { _speedZ = value; }
    }
    private float _rate = (float)1;
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"rate", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
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
    private bool _syn = default(bool);
    [global::ProtoBuf.ProtoMember(20, IsRequired = false, Name=@"syn", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool syn
    {
      get { return _syn; }
      set { _syn = value; }
    }
    private bool _isBodyMove = default(bool);
    [global::ProtoBuf.ProtoMember(21, IsRequired = false, Name=@"isBodyMove", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool isBodyMove
    {
      get { return _isBodyMove; }
      set { _isBodyMove = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}