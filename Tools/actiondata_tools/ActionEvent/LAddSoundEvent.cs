//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LAddSoundEvent.proto
// Note: requires additional types generated from: LTimeEventProType.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LAddSoundEvent")]
  public partial class LAddSoundEvent : global::ProtoBuf.IExtensible
  {
    public LAddSoundEvent() {}
    
    private string _soundUrl = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"soundUrl", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string soundUrl
    {
      get { return _soundUrl; }
      set { _soundUrl = value; }
    }
    private float _vol = (float)0.7;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"vol", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0.7)]
    public float vol
    {
      get { return _vol; }
      set { _vol = value; }
    }
    private readonly global::System.Collections.Generic.List<string> _RateList = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(4, Name=@"RateList", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> RateList
    {
      get { return _RateList; }
    }
  
    private readonly global::System.Collections.Generic.List<string> _fileName = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(5, Name=@"fileName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> fileName
    {
      get { return _fileName; }
    }
  
    private readonly global::System.Collections.Generic.List<string> _bankName = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(6, Name=@"bankName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> bankName
    {
      get { return _bankName; }
    }
  
    private bool _isEmotionSound = default(bool);
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"isEmotionSound", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool isEmotionSound
    {
      get { return _isEmotionSound; }
      set { _isEmotionSound = value; }
    }
    private bool _mainTrack = default(bool);
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"mainTrack", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool mainTrack
    {
      get { return _mainTrack; }
      set { _mainTrack = value; }
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