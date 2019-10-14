//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LActionBaseInfo.proto
// Note: requires additional types generated from: LCheckValueData.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LActionBaseInfo")]
  public partial class LActionBaseInfo : global::ProtoBuf.IExtensible
  {
    public LActionBaseInfo() {}
    
    private string _actionLabel = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"actionLabel", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string actionLabel
    {
      get { return _actionLabel; }
      set { _actionLabel = value; }
    }
    private string _animationName = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"animationName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string animationName
    {
      get { return _animationName; }
      set { _animationName = value; }
    }
    private ActionData.LActionBaseInfo.ActionFunctionType _actionFunctionType = ActionData.LActionBaseInfo.ActionFunctionType.NO_TYPE;
    [global::ProtoBuf.ProtoMember(30, IsRequired = false, Name=@"actionFunctionType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(ActionData.LActionBaseInfo.ActionFunctionType.NO_TYPE)]
    public ActionData.LActionBaseInfo.ActionFunctionType actionFunctionType
    {
      get { return _actionFunctionType; }
      set { _actionFunctionType = value; }
    }
    private int _id = (int)0;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int id
    {
      get { return _id; }
      set { _id = value; }
    }
    private int _bandSkillId = (int)0;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"bandSkillId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int bandSkillId
    {
      get { return _bandSkillId; }
      set { _bandSkillId = value; }
    }
    private ActionData.LActionBaseInfo.ActionSectionSign _actionSectionSign = ActionData.LActionBaseInfo.ActionSectionSign.START;
    [global::ProtoBuf.ProtoMember(20, IsRequired = false, Name=@"actionSectionSign", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(ActionData.LActionBaseInfo.ActionSectionSign.START)]
    public ActionData.LActionBaseInfo.ActionSectionSign actionSectionSign
    {
      get { return _actionSectionSign; }
      set { _actionSectionSign = value; }
    }
    private int _usedLimit = (int)1;
    [global::ProtoBuf.ProtoMember(24, IsRequired = false, Name=@"usedLimit", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)1)]
    public int usedLimit
    {
      get { return _usedLimit; }
      set { _usedLimit = value; }
    }
    private ActionData.LActionBaseInfo.UseType _useType = ActionData.LActionBaseInfo.UseType.LAND;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"useType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(ActionData.LActionBaseInfo.UseType.LAND)]
    public ActionData.LActionBaseInfo.UseType useType
    {
      get { return _useType; }
      set { _useType = value; }
    }
    private string _actionName = "";
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"actionName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string actionName
    {
      get { return _actionName; }
      set { _actionName = value; }
    }
    private float _speed = (float)1;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"speed", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)1)]
    public float speed
    {
      get { return _speed; }
      set { _speed = value; }
    }
    private bool _lockSpeed = (bool)false;
    [global::ProtoBuf.ProtoMember(33, IsRequired = false, Name=@"lockSpeed", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool lockSpeed
    {
      get { return _lockSpeed; }
      set { _lockSpeed = value; }
    }
    private bool _loop = (bool)false;
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"loop", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool loop
    {
      get { return _loop; }
      set { _loop = value; }
    }
    private bool _reverseAnimation = (bool)false;
    [global::ProtoBuf.ProtoMember(29, IsRequired = false, Name=@"reverseAnimation", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool reverseAnimation
    {
      get { return _reverseAnimation; }
      set { _reverseAnimation = value; }
    }
    private float _fadeInLength = (float)0.2;
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"fadeInLength", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0.2)]
    public float fadeInLength
    {
      get { return _fadeInLength; }
      set { _fadeInLength = value; }
    }
    private float _fadeOutLength = (float)0;
    [global::ProtoBuf.ProtoMember(28, IsRequired = false, Name=@"fadeOutLength", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float fadeOutLength
    {
      get { return _fadeOutLength; }
      set { _fadeOutLength = value; }
    }
    private int _cancelPriority = (int)0;
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"cancelPriority", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int cancelPriority
    {
      get { return _cancelPriority; }
      set { _cancelPriority = value; }
    }
    private int _cancelPriorityLimit = (int)-1;
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"cancelPriorityLimit", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)-1)]
    public int cancelPriorityLimit
    {
      get { return _cancelPriorityLimit; }
      set { _cancelPriorityLimit = value; }
    }
    private int _linkSkillId = (int)0;
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"linkSkillId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)0)]
    public int linkSkillId
    {
      get { return _linkSkillId; }
      set { _linkSkillId = value; }
    }
    private bool _linkUpFrame = (bool)false;
    [global::ProtoBuf.ProtoMember(13, IsRequired = false, Name=@"linkUpFrame", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool linkUpFrame
    {
      get { return _linkUpFrame; }
      set { _linkUpFrame = value; }
    }
    private bool _synLand = (bool)true;
    [global::ProtoBuf.ProtoMember(14, IsRequired = false, Name=@"synLand", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)true)]
    public bool synLand
    {
      get { return _synLand; }
      set { _synLand = value; }
    }
    private bool _synAir = (bool)true;
    [global::ProtoBuf.ProtoMember(19, IsRequired = false, Name=@"synAir", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)true)]
    public bool synAir
    {
      get { return _synAir; }
      set { _synAir = value; }
    }
    private bool _bothDirection = (bool)false;
    [global::ProtoBuf.ProtoMember(15, IsRequired = false, Name=@"bothDirection", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool bothDirection
    {
      get { return _bothDirection; }
      set { _bothDirection = value; }
    }
    private int _useCountInAir = (int)-1;
    [global::ProtoBuf.ProtoMember(16, IsRequired = false, Name=@"useCountInAir", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)-1)]
    public int useCountInAir
    {
      get { return _useCountInAir; }
      set { _useCountInAir = value; }
    }
    private int _maxSummonCount = (int)-1;
    [global::ProtoBuf.ProtoMember(17, IsRequired = false, Name=@"maxSummonCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)-1)]
    public int maxSummonCount
    {
      get { return _maxSummonCount; }
      set { _maxSummonCount = value; }
    }
    private bool _entranceBreak = (bool)true;
    [global::ProtoBuf.ProtoMember(23, IsRequired = false, Name=@"entranceBreak", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)true)]
    public bool entranceBreak
    {
      get { return _entranceBreak; }
      set { _entranceBreak = value; }
    }
    private readonly global::System.Collections.Generic.List<string> _animationNames = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(18, Name=@"animationNames", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> animationNames
    {
      get { return _animationNames; }
    }
  
    private bool _isTargetMode = (bool)false;
    [global::ProtoBuf.ProtoMember(21, IsRequired = false, Name=@"isTargetMode", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool isTargetMode
    {
      get { return _isTargetMode; }
      set { _isTargetMode = value; }
    }
    private bool _isParallelAction = (bool)false;
    [global::ProtoBuf.ProtoMember(22, IsRequired = false, Name=@"isParallelAction", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool isParallelAction
    {
      get { return _isParallelAction; }
      set { _isParallelAction = value; }
    }
    private bool _parallelInNumbing = (bool)false;
    [global::ProtoBuf.ProtoMember(27, IsRequired = false, Name=@"parallelInNumbing", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool parallelInNumbing
    {
      get { return _parallelInNumbing; }
      set { _parallelInNumbing = value; }
    }
    private readonly global::System.Collections.Generic.List<ActionData.LActionBaseInfo.StanceData> _stanceData = new global::System.Collections.Generic.List<ActionData.LActionBaseInfo.StanceData>();
    [global::ProtoBuf.ProtoMember(25, Name=@"stanceData", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<ActionData.LActionBaseInfo.StanceData> stanceData
    {
      get { return _stanceData; }
    }
  
    private readonly global::System.Collections.Generic.List<ActionData.LCheckValueData> _checkValue = new global::System.Collections.Generic.List<ActionData.LCheckValueData>();
    [global::ProtoBuf.ProtoMember(26, Name=@"checkValue", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<ActionData.LCheckValueData> checkValue
    {
      get { return _checkValue; }
    }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"StanceData")]
  public partial class StanceData : global::ProtoBuf.IExtensible
  {
    public StanceData() {}
    
    private string _stanceName;
    [global::ProtoBuf.ProtoMember(1, IsRequired = true, Name=@"stanceName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string stanceName
    {
      get { return _stanceName; }
      set { _stanceName = value; }
    }
    private string _linkAction = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"linkAction", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string linkAction
    {
      get { return _linkAction; }
      set { _linkAction = value; }
    }
    private bool _clearLabel = default(bool);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"clearLabel", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool clearLabel
    {
      get { return _clearLabel; }
      set { _clearLabel = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
    [global::ProtoBuf.ProtoContract(Name=@"UseType")]
    public enum UseType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"LAND", Value=0)]
      LAND = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"AIR", Value=1)]
      AIR = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"BOTH", Value=2)]
      BOTH = 2
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"ActionSectionSign")]
    public enum ActionSectionSign
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"START", Value=0)]
      START = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MIDDLE", Value=1)]
      MIDDLE = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"OVER", Value=2)]
      OVER = 2
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"ActionFunctionType")]
    public enum ActionFunctionType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"NO_TYPE", Value=0)]
      NO_TYPE = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"MOVE", Value=1)]
      MOVE = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"ATTACK", Value=2)]
      ATTACK = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SKILL", Value=3)]
      SKILL = 3
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}