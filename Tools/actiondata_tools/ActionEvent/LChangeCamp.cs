//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LChangeCamp.proto
// Note: requires additional types generated from: LTimeEventProType.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LChangeCamp")]
  public partial class LChangeCamp : global::ProtoBuf.IExtensible
  {
    public LChangeCamp() {}
    
    private ActionData.LChangeCamp.Camp _camp = ActionData.LChangeCamp.Camp.CURRENT_CAMP;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"camp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(ActionData.LChangeCamp.Camp.CURRENT_CAMP)]
    public ActionData.LChangeCamp.Camp camp
    {
      get { return _camp; }
      set { _camp = value; }
    }
    private ActionData.LTimeEventProType _eventType;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"eventType", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ActionData.LTimeEventProType eventType
    {
      get { return _eventType; }
      set { _eventType = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"Camp")]
    public enum Camp
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"CURRENT_CAMP", Value=0)]
      CURRENT_CAMP = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"INVERSE_CAMP", Value=1)]
      INVERSE_CAMP = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"NEUTRAL_CAMP", Value=2)]
      NEUTRAL_CAMP = 2
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}