//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LCheckValueData.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LCheckValueData")]
  public partial class LCheckValueData : global::ProtoBuf.IExtensible
  {
    public LCheckValueData() {}
    
    private string _name;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name; }
      set { _name = value; }
    }
    private ActionData.LCheckValueData.ValueCompareType _compareType;
    [global::ProtoBuf.ProtoMember(2, IsRequired = true, Name=@"compareType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public ActionData.LCheckValueData.ValueCompareType compareType
    {
      get { return _compareType; }
      set { _compareType = value; }
    }
    private int _value;
    [global::ProtoBuf.ProtoMember(3, IsRequired = true, Name=@"value", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int value
    {
      get { return _value; }
      set { _value = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"ValueCompareType")]
    public enum ValueCompareType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"BE_EQUAL", Value=0)]
      BE_EQUAL = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MORE_THAN", Value=1)]
      MORE_THAN = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"LESS_THAN", Value=2)]
      LESS_THAN = 2
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}