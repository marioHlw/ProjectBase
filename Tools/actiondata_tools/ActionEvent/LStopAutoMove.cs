//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LStopAutoMove.proto
// Note: requires additional types generated from: LTimeEventProType.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LStopAutoMove")]
  public partial class LStopAutoMove : global::ProtoBuf.IExtensible
  {
    public LStopAutoMove() {}
    
    private ActionData.LTimeEventProType _eventType;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"eventType", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ActionData.LTimeEventProType eventType
    {
      get { return _eventType; }
      set { _eventType = value; }
    }
    private bool _stopMove = (bool)true;
    [global::ProtoBuf.ProtoMember(20, IsRequired = false, Name=@"stopMove", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)true)]
    public bool stopMove
    {
      get { return _stopMove; }
      set { _stopMove = value; }
    }
    private bool _stopBodyMove = (bool)true;
    [global::ProtoBuf.ProtoMember(21, IsRequired = false, Name=@"stopBodyMove", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)true)]
    public bool stopBodyMove
    {
      get { return _stopBodyMove; }
      set { _stopBodyMove = value; }
    }
    private bool _stopOtherMove = (bool)true;
    [global::ProtoBuf.ProtoMember(22, IsRequired = false, Name=@"stopOtherMove", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)true)]
    public bool stopOtherMove
    {
      get { return _stopOtherMove; }
      set { _stopOtherMove = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}