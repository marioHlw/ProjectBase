//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LGenericSwitcher.proto
// Note: requires additional types generated from: LTimeEventProType.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LGenericSwitcher")]
  public partial class LGenericSwitcher : global::ProtoBuf.IExtensible
  {
    public LGenericSwitcher() {}
    
    private ActionData.LGenericSwitcher.BoolValue _passThroughAirWall = ActionData.LGenericSwitcher.BoolValue.NONE;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"passThroughAirWall", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(ActionData.LGenericSwitcher.BoolValue.NONE)]
    public ActionData.LGenericSwitcher.BoolValue passThroughAirWall
    {
      get { return _passThroughAirWall; }
      set { _passThroughAirWall = value; }
    }
    private ActionData.LGenericSwitcher.BoolValue _switchBarrier = ActionData.LGenericSwitcher.BoolValue.NONE;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"switchBarrier", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(ActionData.LGenericSwitcher.BoolValue.NONE)]
    public ActionData.LGenericSwitcher.BoolValue switchBarrier
    {
      get { return _switchBarrier; }
      set { _switchBarrier = value; }
    }
    private ActionData.LGenericSwitcher.BoolValue _switchShadow = ActionData.LGenericSwitcher.BoolValue.NONE;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"switchShadow", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(ActionData.LGenericSwitcher.BoolValue.NONE)]
    public ActionData.LGenericSwitcher.BoolValue switchShadow
    {
      get { return _switchShadow; }
      set { _switchShadow = value; }
    }
    private ActionData.LTimeEventProType _eventType;
    [global::ProtoBuf.ProtoMember(100, IsRequired = true, Name=@"eventType", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ActionData.LTimeEventProType eventType
    {
      get { return _eventType; }
      set { _eventType = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"BoolValue")]
    public enum BoolValue
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"NONE", Value=0)]
      NONE = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"TRUE", Value=1)]
      TRUE = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FALSE", Value=2)]
      FALSE = 2
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}