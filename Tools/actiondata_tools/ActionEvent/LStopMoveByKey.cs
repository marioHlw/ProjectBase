//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LStopMoveByKey.proto
// Note: requires additional types generated from: LTimeEventProType.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LStopMoveByKey")]
  public partial class LStopMoveByKey : global::ProtoBuf.IExtensible
  {
    public LStopMoveByKey() {}
    
    private ActionData.LTimeEventProType _eventType;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"eventType", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ActionData.LTimeEventProType eventType
    {
      get { return _eventType; }
      set { _eventType = value; }
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