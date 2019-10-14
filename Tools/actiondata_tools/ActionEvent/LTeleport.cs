//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LTeleport.proto
// Note: requires additional types generated from: LVector3.proto
// Note: requires additional types generated from: LTimeEventProType.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LTeleport")]
  public partial class LTeleport : global::ProtoBuf.IExtensible
  {
    public LTeleport() {}
    
    private ActionData.LVector3 _distance = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"distance", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 distance
    {
      get { return _distance; }
      set { _distance = value; }
    }
    private bool _randomDistance = default(bool);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"randomDistance", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool randomDistance
    {
      get { return _randomDistance; }
      set { _randomDistance = value; }
    }
    private ActionData.LVector3 _offset = null;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"offset", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 offset
    {
      get { return _offset; }
      set { _offset = value; }
    }
    private ActionData.LTeleport.MoveType _moveType = ActionData.LTeleport.MoveType.TO_TARGET;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"moveType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(ActionData.LTeleport.MoveType.TO_TARGET)]
    public ActionData.LTeleport.MoveType moveType
    {
      get { return _moveType; }
      set { _moveType = value; }
    }
    private bool _onFloor = default(bool);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"onFloor", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool onFloor
    {
      get { return _onFloor; }
      set { _onFloor = value; }
    }
    private bool _updateWithScale = default(bool);
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"updateWithScale", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool updateWithScale
    {
      get { return _updateWithScale; }
      set { _updateWithScale = value; }
    }
    private ActionData.LTimeEventProType _eventType;
    [global::ProtoBuf.ProtoMember(5, IsRequired = true, Name=@"eventType", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ActionData.LTimeEventProType eventType
    {
      get { return _eventType; }
      set { _eventType = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"MoveType")]
    public enum MoveType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"DISTANCE", Value=2)]
      DISTANCE = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"TO_TARGET", Value=3)]
      TO_TARGET = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CLOSER_TO_TARGET", Value=4)]
      CLOSER_TO_TARGET = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"RANDOM", Value=6)]
      RANDOM = 6
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}