//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: Proto/goods_info.proto
namespace NetProto
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"goods_info")]
  public partial class goods_info : global::ProtoBuf.IExtensible
  {
    public goods_info() {}
    
    private uint _goods_id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"goods_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint goods_id
    {
      get { return _goods_id; }
      set { _goods_id = value; }
    }
    private byte[] _name = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] name
    {
      get { return _name; }
      set { _name = value; }
    }
    private uint _sex = (uint)0;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"sex", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint sex
    {
      get { return _sex; }
      set { _sex = value; }
    }
    private uint _level_limit = (uint)0;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"level_limit", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint level_limit
    {
      get { return _level_limit; }
      set { _level_limit = value; }
    }
    private int _club_vip_level = (int)0;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"club_vip_level", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int club_vip_level
    {
      get { return _club_vip_level; }
      set { _club_vip_level = value; }
    }
    private int _qb_2 = (int)0;
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"qb_2", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int qb_2
    {
      get { return _qb_2; }
      set { _qb_2 = value; }
    }
    private uint _consume_type = (uint)0;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"consume_type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint consume_type
    {
      get { return _consume_type; }
      set { _consume_type = value; }
    }
    private uint _time_unit = (uint)0;
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"time_unit", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint time_unit
    {
      get { return _time_unit; }
      set { _time_unit = value; }
    }
    private uint _is_online = (uint)0;
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"is_online", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint is_online
    {
      get { return _is_online; }
      set { _is_online = value; }
    }
    private uint _can_buy = (uint)0;
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"can_buy", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint can_buy
    {
      get { return _can_buy; }
      set { _can_buy = value; }
    }
    private uint _can_recharge = (uint)0;
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"can_recharge", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint can_recharge
    {
      get { return _can_recharge; }
      set { _can_recharge = value; }
    }
    private byte[] _online_time = null;
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"online_time", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] online_time
    {
      get { return _online_time; }
      set { _online_time = value; }
    }
    private byte[] _offline_time = null;
    [global::ProtoBuf.ProtoMember(13, IsRequired = false, Name=@"offline_time", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] offline_time
    {
      get { return _offline_time; }
      set { _offline_time = value; }
    }
    private readonly global::System.Collections.Generic.List<uint> _privileged_plat_id_list = new global::System.Collections.Generic.List<uint>();
    [global::ProtoBuf.ProtoMember(14, Name=@"privileged_plat_id_list", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<uint> privileged_plat_id_list
    {
      get { return _privileged_plat_id_list; }
    }
  
    private uint _privileged_plat_is_online = (uint)0;
    [global::ProtoBuf.ProtoMember(15, IsRequired = false, Name=@"privileged_plat_is_online", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint privileged_plat_is_online
    {
      get { return _privileged_plat_is_online; }
      set { _privileged_plat_is_online = value; }
    }
    private uint _privileged_plat_can_buy = (uint)0;
    [global::ProtoBuf.ProtoMember(16, IsRequired = false, Name=@"privileged_plat_can_buy", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint privileged_plat_can_buy
    {
      get { return _privileged_plat_can_buy; }
      set { _privileged_plat_can_buy = value; }
    }
    private uint _privileged_plat_can_recharge = (uint)0;
    [global::ProtoBuf.ProtoMember(17, IsRequired = false, Name=@"privileged_plat_can_recharge", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint privileged_plat_can_recharge
    {
      get { return _privileged_plat_can_recharge; }
      set { _privileged_plat_can_recharge = value; }
    }
    private uint _payment_terms = (uint)0;
    [global::ProtoBuf.ProtoMember(18, IsRequired = false, Name=@"payment_terms", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint payment_terms
    {
      get { return _payment_terms; }
      set { _payment_terms = value; }
    }
    private byte[] _valid_time = null;
    [global::ProtoBuf.ProtoMember(19, IsRequired = false, Name=@"valid_time", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] valid_time
    {
      get { return _valid_time; }
      set { _valid_time = value; }
    }
    private uint _sort_priority = (uint)0;
    [global::ProtoBuf.ProtoMember(20, IsRequired = false, Name=@"sort_priority", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint sort_priority
    {
      get { return _sort_priority; }
      set { _sort_priority = value; }
    }
    private int _suit_number = (int)0;
    [global::ProtoBuf.ProtoMember(21, IsRequired = false, Name=@"suit_number", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int suit_number
    {
      get { return _suit_number; }
      set { _suit_number = value; }
    }
    private uint _bag_sort_priority = (uint)0;
    [global::ProtoBuf.ProtoMember(22, IsRequired = false, Name=@"bag_sort_priority", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint bag_sort_priority
    {
      get { return _bag_sort_priority; }
      set { _bag_sort_priority = value; }
    }
    private byte[] _status = null;
    [global::ProtoBuf.ProtoMember(23, IsRequired = false, Name=@"status", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] status
    {
      get { return _status; }
      set { _status = value; }
    }
    private uint _rank = (uint)0;
    [global::ProtoBuf.ProtoMember(24, IsRequired = false, Name=@"rank", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint rank
    {
      get { return _rank; }
      set { _rank = value; }
    }
    private uint _price_discount = (uint)0;
    [global::ProtoBuf.ProtoMember(25, IsRequired = false, Name=@"price_discount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint price_discount
    {
      get { return _price_discount; }
      set { _price_discount = value; }
    }
    private uint _vip_discount = (uint)0;
    [global::ProtoBuf.ProtoMember(26, IsRequired = false, Name=@"vip_discount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint vip_discount
    {
      get { return _vip_discount; }
      set { _vip_discount = value; }
    }
    private readonly global::System.Collections.Generic.List<NetProto.goods_info.Price> _price_table = new global::System.Collections.Generic.List<NetProto.goods_info.Price>();
    [global::ProtoBuf.ProtoMember(27, Name=@"price_table", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<NetProto.goods_info.Price> price_table
    {
      get { return _price_table; }
    }
  
    private readonly global::System.Collections.Generic.List<NetProto.goods_info.GoodsAttr> _goods_attr = new global::System.Collections.Generic.List<NetProto.goods_info.GoodsAttr>();
    [global::ProtoBuf.ProtoMember(28, Name=@"goods_attr", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<NetProto.goods_info.GoodsAttr> goods_attr
    {
      get { return _goods_attr; }
    }
  
    private byte[] _description = null;
    [global::ProtoBuf.ProtoMember(29, IsRequired = false, Name=@"description", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] description
    {
      get { return _description; }
      set { _description = value; }
    }
    private uint _limit_id = (uint)0;
    [global::ProtoBuf.ProtoMember(30, IsRequired = false, Name=@"limit_id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint limit_id
    {
      get { return _limit_id; }
      set { _limit_id = value; }
    }
    private byte[] _subSystemId = null;
    [global::ProtoBuf.ProtoMember(31, IsRequired = false, Name=@"subSystemId", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] subSystemId
    {
      get { return _subSystemId; }
      set { _subSystemId = value; }
    }
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"Price")]
  public partial class Price : global::ProtoBuf.IExtensible
  {
    public Price() {}
    
    private uint _price_dq = (uint)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"price_dq", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint price_dq
    {
      get { return _price_dq; }
      set { _price_dq = value; }
    }
    private uint _price_gold = (uint)0;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"price_gold", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint price_gold
    {
      get { return _price_gold; }
      set { _price_gold = value; }
    }
    private uint _price_value = (uint)0;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"price_value", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint price_value
    {
      get { return _price_value; }
      set { _price_value = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"GoodsAttr")]
  public partial class GoodsAttr : global::ProtoBuf.IExtensible
  {
    public GoodsAttr() {}
    
    private uint _attr_type = (uint)0;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"attr_type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint attr_type
    {
      get { return _attr_type; }
      set { _attr_type = value; }
    }
    private uint _attr_value = (uint)0;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"attr_value", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((uint)0)]
    public uint attr_value
    {
      get { return _attr_value; }
      set { _attr_value = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"goods_info_ARRAY")]
  public partial class goods_info_ARRAY : global::ProtoBuf.IExtensible
  {
    public goods_info_ARRAY() {}
    
    private readonly global::System.Collections.Generic.List<NetProto.goods_info> _items = new global::System.Collections.Generic.List<NetProto.goods_info>();
    [global::ProtoBuf.ProtoMember(1, Name=@"items", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<NetProto.goods_info> items
    {
      get { return _items; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}