//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LAddGodRay.proto
// Note: requires additional types generated from: LTimeEventProType.proto
// Note: requires additional types generated from: LVector3.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LAddGodRay")]
  public partial class LAddGodRay : global::ProtoBuf.IExtensible
  {
    public LAddGodRay() {}
    
    private ActionData.LVector3 _offset = null;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"offset", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 offset
    {
      get { return _offset; }
      set { _offset = value; }
    }
    private float _startStrengthInward = (float)0.2;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"startStrengthInward", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0.2)]
    public float startStrengthInward
    {
      get { return _startStrengthInward; }
      set { _startStrengthInward = value; }
    }
    private float _endStrengthInward = (float)0;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"endStrengthInward", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float endStrengthInward
    {
      get { return _endStrengthInward; }
      set { _endStrengthInward = value; }
    }
    private float _startStrengthOutward = (float)0;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"startStrengthOutward", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float startStrengthOutward
    {
      get { return _startStrengthOutward; }
      set { _startStrengthOutward = value; }
    }
    private float _endStrengthOutward = (float)0;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"endStrengthOutward", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float endStrengthOutward
    {
      get { return _endStrengthOutward; }
      set { _endStrengthOutward = value; }
    }
    private float _startDistance = (float)0.5;
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"startDistance", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0.5)]
    public float startDistance
    {
      get { return _startDistance; }
      set { _startDistance = value; }
    }
    private float _endDistance = (float)0;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"endDistance", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float endDistance
    {
      get { return _endDistance; }
      set { _endDistance = value; }
    }
    private int _startStep = (int)8;
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"startStep", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)8)]
    public int startStep
    {
      get { return _startStep; }
      set { _startStep = value; }
    }
    private int _endStep = (int)8;
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"endStep", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)8)]
    public int endStep
    {
      get { return _endStep; }
      set { _endStep = value; }
    }
    private float _time = (float)0.3;
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"time", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0.3)]
    public float time
    {
      get { return _time; }
      set { _time = value; }
    }
    private ActionData.LTimeEventProType _eventType;
    [global::ProtoBuf.ProtoMember(101, IsRequired = true, Name=@"eventType", DataFormat = global::ProtoBuf.DataFormat.Default)]
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