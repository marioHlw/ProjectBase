//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LChangeAutoMoveSpeed.proto
// Note: requires additional types generated from: LTimeEventProType.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LChangeAutoMoveSpeed")]
  public partial class LChangeAutoMoveSpeed : global::ProtoBuf.IExtensible
  {
    public LChangeAutoMoveSpeed() {}
    
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
    private float _accelerationX = (float)0;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"accelerationX", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float accelerationX
    {
      get { return _accelerationX; }
      set { _accelerationX = value; }
    }
    private float _accelerationY = (float)0;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"accelerationY", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float accelerationY
    {
      get { return _accelerationY; }
      set { _accelerationY = value; }
    }
    private float _accelerationZ = (float)0;
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"accelerationZ", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float accelerationZ
    {
      get { return _accelerationZ; }
      set { _accelerationZ = value; }
    }
    private ActionData.LTimeEventProType _eventType;
    [global::ProtoBuf.ProtoMember(7, IsRequired = true, Name=@"eventType", DataFormat = global::ProtoBuf.DataFormat.Default)]
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