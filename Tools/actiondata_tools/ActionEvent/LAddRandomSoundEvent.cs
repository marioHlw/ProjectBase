//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LAddRandomSoundEvent.proto
// Note: requires additional types generated from: LTimeEventProType.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LAddRandomSoundEvent")]
  public partial class LAddRandomSoundEvent : global::ProtoBuf.IExtensible
  {
    public LAddRandomSoundEvent() {}
    
    private readonly global::System.Collections.Generic.List<string> _soundUrlList = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(1, Name=@"soundUrlList", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> soundUrlList
    {
      get { return _soundUrlList; }
    }
  
    private float _vol = (float)1;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"vol", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)1)]
    public float vol
    {
      get { return _vol; }
      set { _vol = value; }
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