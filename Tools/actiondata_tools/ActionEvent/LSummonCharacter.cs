//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LSummonCharacter.proto
// Note: requires additional types generated from: LVector3.proto
// Note: requires additional types generated from: LTimeEventProType.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LSummonCharacter")]
  public partial class LSummonCharacter : global::ProtoBuf.IExtensible
  {
    public LSummonCharacter() {}
    
    private string _modelID = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"modelID", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string modelID
    {
      get { return _modelID; }
      set { _modelID = value; }
    }
    private int _id = default(int);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int id
    {
      get { return _id; }
      set { _id = value; }
    }
    private bool _dieWithSummoner = (bool)true;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"dieWithSummoner", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)true)]
    public bool dieWithSummoner
    {
      get { return _dieWithSummoner; }
      set { _dieWithSummoner = value; }
    }
    private ActionData.LSummonCharacter.Camp _camp = ActionData.LSummonCharacter.Camp.CURRENT_CAMP;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"camp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(ActionData.LSummonCharacter.Camp.CURRENT_CAMP)]
    public ActionData.LSummonCharacter.Camp camp
    {
      get { return _camp; }
      set { _camp = value; }
    }
    private ActionData.LVector3 _positionOffset = null;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"positionOffset", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 positionOffset
    {
      get { return _positionOffset; }
      set { _positionOffset = value; }
    }
    private ActionData.LVector3 _scaleOffset = null;
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"scaleOffset", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 scaleOffset
    {
      get { return _scaleOffset; }
      set { _scaleOffset = value; }
    }
    private bool _preLoad = default(bool);
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"preLoad", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool preLoad
    {
      get { return _preLoad; }
      set { _preLoad = value; }
    }
    private ActionData.LTimeEventProType _eventType;
    [global::ProtoBuf.ProtoMember(10, IsRequired = true, Name=@"eventType", DataFormat = global::ProtoBuf.DataFormat.Default)]
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