//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LChangeGravity.proto
// Note: requires additional types generated from: LTimeEventProType.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LChangeGravity")]
  public partial class LChangeGravity : global::ProtoBuf.IExtensible
  {
    public LChangeGravity() {}
    
    private float _speedY = (float)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"speedY", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float speedY
    {
      get { return _speedY; }
      set { _speedY = value; }
    }
    private float _gravity = (float)0;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"gravity", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float gravity
    {
      get { return _gravity; }
      set { _gravity = value; }
    }
    private ActionData.LTimeEventProType _eventType;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"eventType", DataFormat = global::ProtoBuf.DataFormat.Default)]
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