//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LHitShockData_Input.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LHitShockData_Input")]
  public partial class LHitShockData_Input : global::ProtoBuf.IExtensible
  {
    public LHitShockData_Input() {}
    
    private float _power = (float)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"power", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float power
    {
      get { return _power; }
      set { _power = value; }
    }
    private float _angle = (float)0;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"angle", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float angle
    {
      get { return _angle; }
      set { _angle = value; }
    }
    private float _time = (float)1;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"time", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)1)]
    public float time
    {
      get { return _time; }
      set { _time = value; }
    }
    private float _frequency = (float)1;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"frequency", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)1)]
    public float frequency
    {
      get { return _frequency; }
      set { _frequency = value; }
    }
    private float _decay = (float)0.98;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"decay", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0.98)]
    public float decay
    {
      get { return _decay; }
      set { _decay = value; }
    }
    private bool _randomAngle = (bool)true;
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"randomAngle", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)true)]
    public bool randomAngle
    {
      get { return _randomAngle; }
      set { _randomAngle = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}