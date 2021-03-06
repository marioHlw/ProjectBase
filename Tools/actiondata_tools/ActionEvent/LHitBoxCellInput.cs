//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LHitBoxCellInput.proto
// Note: requires additional types generated from: LVector3.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LHitBoxCellInput")]
  public partial class LHitBoxCellInput : global::ProtoBuf.IExtensible
  {
    public LHitBoxCellInput() {}
    
    private ActionData.LVector3 _boxColliderCenter;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"boxColliderCenter", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ActionData.LVector3 boxColliderCenter
    {
      get { return _boxColliderCenter; }
      set { _boxColliderCenter = value; }
    }
    private ActionData.LVector3 _boxColliderSize;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"boxColliderSize", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ActionData.LVector3 boxColliderSize
    {
      get { return _boxColliderSize; }
      set { _boxColliderSize = value; }
    }
    private ActionData.LVector3 _localPosition;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"localPosition", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ActionData.LVector3 localPosition
    {
      get { return _localPosition; }
      set { _localPosition = value; }
    }
    private ActionData.LVector3 _localRotation = null;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"localRotation", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 localRotation
    {
      get { return _localRotation; }
      set { _localRotation = value; }
    }
    private ActionData.LHitBoxCellInput.Shape _shape = ActionData.LHitBoxCellInput.Shape.BOX;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"shape", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(ActionData.LHitBoxCellInput.Shape.BOX)]
    public ActionData.LHitBoxCellInput.Shape shape
    {
      get { return _shape; }
      set { _shape = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"Shape")]
    public enum Shape
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"BOX", Value=1)]
      BOX = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SPHERE", Value=2)]
      SPHERE = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SEMICIRCLE", Value=3)]
      SEMICIRCLE = 3
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}