//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LResetCamera.proto
// Note: requires additional types generated from: LTimeEventProType.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LResetCamera")]
  public partial class LResetCamera : global::ProtoBuf.IExtensible
  {
    public LResetCamera() {}
    
    private bool _position = (bool)true;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"position", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)true)]
    public bool position
    {
      get { return _position; }
      set { _position = value; }
    }
    private float _positionAniTime = (float)0.5;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"positionAniTime", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0.5)]
    public float positionAniTime
    {
      get { return _positionAniTime; }
      set { _positionAniTime = value; }
    }
    private bool _cameraAttribute = (bool)true;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"cameraAttribute", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)true)]
    public bool cameraAttribute
    {
      get { return _cameraAttribute; }
      set { _cameraAttribute = value; }
    }
    private float _cameraAttributeAniTime = (float)0.5;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"cameraAttributeAniTime", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0.5)]
    public float cameraAttributeAniTime
    {
      get { return _cameraAttributeAniTime; }
      set { _cameraAttributeAniTime = value; }
    }
    private ActionData.LTimeEventProType _eventType;
    [global::ProtoBuf.ProtoMember(10, IsRequired = true, Name=@"eventType", DataFormat = global::ProtoBuf.DataFormat.Default)]
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