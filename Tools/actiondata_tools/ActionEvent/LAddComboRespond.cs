//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LAddComboRespond.proto
// Note: requires additional types generated from: LVector3.proto
// Note: requires additional types generated from: LTimeEventProType.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LAddComboRespond")]
  public partial class LAddComboRespond : global::ProtoBuf.IExtensible
  {
    public LAddComboRespond() {}
    
    private string _combo = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"combo", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string combo
    {
      get { return _combo; }
      set { _combo = value; }
    }
    private int _extendFrame = default(int);
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"extendFrame", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int extendFrame
    {
      get { return _extendFrame; }
      set { _extendFrame = value; }
    }
    private int _times = default(int);
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"times", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int times
    {
      get { return _times; }
      set { _times = value; }
    }
    private readonly global::System.Collections.Generic.List<string> _successActionCommandId = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(2, Name=@"successActionCommandId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> successActionCommandId
    {
      get { return _successActionCommandId; }
    }
  
    private readonly global::System.Collections.Generic.List<string> _failActionCommandId = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(3, Name=@"failActionCommandId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> failActionCommandId
    {
      get { return _failActionCommandId; }
    }
  
    private readonly global::System.Collections.Generic.List<string> _untriggeredActionCommandId = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(11, Name=@"untriggeredActionCommandId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> untriggeredActionCommandId
    {
      get { return _untriggeredActionCommandId; }
    }
  
    private ActionData.LVector3 _condition_frameInterval;
    [global::ProtoBuf.ProtoMember(4, IsRequired = true, Name=@"condition_frameInterval", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ActionData.LVector3 condition_frameInterval
    {
      get { return _condition_frameInterval; }
      set { _condition_frameInterval = value; }
    }
    private readonly global::System.Collections.Generic.List<string> _condition_keyIsUp = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(5, Name=@"condition_keyIsUp", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> condition_keyIsUp
    {
      get { return _condition_keyIsUp; }
    }
  
    private readonly global::System.Collections.Generic.List<string> _condition_keyIsDown = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(6, Name=@"condition_keyIsDown", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> condition_keyIsDown
    {
      get { return _condition_keyIsDown; }
    }
  
    private bool _condition_directionForward = default(bool);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"condition_directionForward", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool condition_directionForward
    {
      get { return _condition_directionForward; }
      set { _condition_directionForward = value; }
    }
    private bool _condition_directionNegative = default(bool);
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"condition_directionNegative", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool condition_directionNegative
    {
      get { return _condition_directionNegative; }
      set { _condition_directionNegative = value; }
    }
    private ActionData.LTimeEventProType _eventType;
    [global::ProtoBuf.ProtoMember(9, IsRequired = true, Name=@"eventType", DataFormat = global::ProtoBuf.DataFormat.Default)]
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