//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LTimeEventProType.proto
// Note: requires additional types generated from: LCheckSkillSwitchData.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LTimeEventProType")]
  public partial class LTimeEventProType : global::ProtoBuf.IExtensible
  {
    public LTimeEventProType() {}
    
    private string _ItemType = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"ItemType", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ItemType
    {
      get { return _ItemType; }
      set { _ItemType = value; }
    }
    private float _StartTime = (float)0;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"StartTime", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float StartTime
    {
      get { return _StartTime; }
      set { _StartTime = value; }
    }
    private bool _isDuration = default(bool);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"isDuration", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool isDuration
    {
      get { return _isDuration; }
      set { _isDuration = value; }
    }
    private float _EndTime = (float)0;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"EndTime", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float EndTime
    {
      get { return _EndTime; }
      set { _EndTime = value; }
    }
    private bool _enable = default(bool);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"enable", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool enable
    {
      get { return _enable; }
      set { _enable = value; }
    }
    private bool _isFold = default(bool);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"isFold", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool isFold
    {
      get { return _isFold; }
      set { _isFold = value; }
    }
    private string _linkId = "";
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"linkId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string linkId
    {
      get { return _linkId; }
      set { _linkId = value; }
    }
    private float _startPosition = (float)0;
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"startPosition", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float startPosition
    {
      get { return _startPosition; }
      set { _startPosition = value; }
    }
    private float _length = (float)0;
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"length", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float length
    {
      get { return _length; }
      set { _length = value; }
    }
    private bool _executed = default(bool);
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"executed", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool executed
    {
      get { return _executed; }
      set { _executed = value; }
    }
    private bool _once = default(bool);
    [global::ProtoBuf.ProtoMember(13, IsRequired = false, Name=@"once", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool once
    {
      get { return _once; }
      set { _once = value; }
    }
    private int _index = default(int);
    [global::ProtoBuf.ProtoMember(14, IsRequired = false, Name=@"index", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int index
    {
      get { return _index; }
      set { _index = value; }
    }
    private bool _copyMark = default(bool);
    [global::ProtoBuf.ProtoMember(15, IsRequired = false, Name=@"copyMark", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool copyMark
    {
      get { return _copyMark; }
      set { _copyMark = value; }
    }
    private readonly global::System.Collections.Generic.List<ActionData.LCheckSkillSwitchData> _checkSkillSwitchData = new global::System.Collections.Generic.List<ActionData.LCheckSkillSwitchData>();
    [global::ProtoBuf.ProtoMember(101, Name=@"checkSkillSwitchData", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<ActionData.LCheckSkillSwitchData> checkSkillSwitchData
    {
      get { return _checkSkillSwitchData; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}