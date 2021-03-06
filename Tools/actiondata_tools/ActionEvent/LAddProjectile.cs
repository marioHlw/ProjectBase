//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: LAddProjectile.proto
// Note: requires additional types generated from: LTimeEventProType.proto
// Note: requires additional types generated from: LVector3.proto
namespace ActionData
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"LAddProjectile")]
  public partial class LAddProjectile : global::ProtoBuf.IExtensible
  {
    public LAddProjectile() {}
    
    private ActionData.LAddProjectile.TypeName _typeName = ActionData.LAddProjectile.TypeName.DEFAULT;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"typeName", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(ActionData.LAddProjectile.TypeName.DEFAULT)]
    public ActionData.LAddProjectile.TypeName typeName
    {
      get { return _typeName; }
      set { _typeName = value; }
    }
    private string _prefabName = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"prefabName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string prefabName
    {
      get { return _prefabName; }
      set { _prefabName = value; }
    }
    private ActionData.LAddProjectile.FunctionType _functionType = ActionData.LAddProjectile.FunctionType.NORMAL;
    [global::ProtoBuf.ProtoMember(38, IsRequired = false, Name=@"functionType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(ActionData.LAddProjectile.FunctionType.NORMAL)]
    public ActionData.LAddProjectile.FunctionType functionType
    {
      get { return _functionType; }
      set { _functionType = value; }
    }
    private string _startAction = "";
    [global::ProtoBuf.ProtoMember(37, IsRequired = false, Name=@"startAction", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string startAction
    {
      get { return _startAction; }
      set { _startAction = value; }
    }
    private int _recursionTimes = (int)1;
    [global::ProtoBuf.ProtoMember(34, IsRequired = false, Name=@"recursionTimes", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)1)]
    public int recursionTimes
    {
      get { return _recursionTimes; }
      set { _recursionTimes = value; }
    }
    private int _count = (int)1;
    [global::ProtoBuf.ProtoMember(24, IsRequired = false, Name=@"count", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue((int)1)]
    public int count
    {
      get { return _count; }
      set { _count = value; }
    }
    private bool _blocked = (bool)false;
    [global::ProtoBuf.ProtoMember(59, IsRequired = false, Name=@"blocked", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool blocked
    {
      get { return _blocked; }
      set { _blocked = value; }
    }
    private bool _emitByTargetCount = default(bool);
    [global::ProtoBuf.ProtoMember(22, IsRequired = false, Name=@"emitByTargetCount", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool emitByTargetCount
    {
      get { return _emitByTargetCount; }
      set { _emitByTargetCount = value; }
    }
    private bool _destroyWithDead = default(bool);
    [global::ProtoBuf.ProtoMember(30, IsRequired = false, Name=@"destroyWithDead", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool destroyWithDead
    {
      get { return _destroyWithDead; }
      set { _destroyWithDead = value; }
    }
    private bool _destroyWithChangeAction = default(bool);
    [global::ProtoBuf.ProtoMember(40, IsRequired = false, Name=@"destroyWithChangeAction", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool destroyWithChangeAction
    {
      get { return _destroyWithChangeAction; }
      set { _destroyWithChangeAction = value; }
    }
    private bool _isSelfDisciplineMode = (bool)false;
    [global::ProtoBuf.ProtoMember(57, IsRequired = false, Name=@"isSelfDisciplineMode", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)false)]
    public bool isSelfDisciplineMode
    {
      get { return _isSelfDisciplineMode; }
      set { _isSelfDisciplineMode = value; }
    }
    private int _skillId = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"skillId", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int skillId
    {
      get { return _skillId; }
      set { _skillId = value; }
    }
    private float _scale = (float)1;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"scale", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)1)]
    public float scale
    {
      get { return _scale; }
      set { _scale = value; }
    }
    private ActionData.LVector3 _scale3Axis = null;
    [global::ProtoBuf.ProtoMember(39, IsRequired = false, Name=@"scale3Axis", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 scale3Axis
    {
      get { return _scale3Axis; }
      set { _scale3Axis = value; }
    }
    private float _randomScale = (float)0;
    [global::ProtoBuf.ProtoMember(25, IsRequired = false, Name=@"randomScale", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float randomScale
    {
      get { return _randomScale; }
      set { _randomScale = value; }
    }
    private bool _emitByTarget = default(bool);
    [global::ProtoBuf.ProtoMember(23, IsRequired = false, Name=@"emitByTarget", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool emitByTarget
    {
      get { return _emitByTarget; }
      set { _emitByTarget = value; }
    }
    private ActionData.LVector3 _position = null;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"position", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 position
    {
      get { return _position; }
      set { _position = value; }
    }
    private ActionData.LVector3 _position2 = null;
    [global::ProtoBuf.ProtoMember(36, IsRequired = false, Name=@"position2", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 position2
    {
      get { return _position2; }
      set { _position2 = value; }
    }
    private bool _autoHalfHeight = (bool)true;
    [global::ProtoBuf.ProtoMember(63, IsRequired = false, Name=@"autoHalfHeight", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue((bool)true)]
    public bool autoHalfHeight
    {
      get { return _autoHalfHeight; }
      set { _autoHalfHeight = value; }
    }
    private ActionData.LVector3 _startAngle = null;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"startAngle", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 startAngle
    {
      get { return _startAngle; }
      set { _startAngle = value; }
    }
    private ActionData.LVector3 _randomStartAngleRange = null;
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"randomStartAngleRange", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 randomStartAngleRange
    {
      get { return _randomStartAngleRange; }
      set { _randomStartAngleRange = value; }
    }
    private ActionData.LVector3 _startSelfAngle = null;
    [global::ProtoBuf.ProtoMember(18, IsRequired = false, Name=@"startSelfAngle", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 startSelfAngle
    {
      get { return _startSelfAngle; }
      set { _startSelfAngle = value; }
    }
    private ActionData.LVector3 _randomStartSelfAngleRange = null;
    [global::ProtoBuf.ProtoMember(19, IsRequired = false, Name=@"randomStartSelfAngleRange", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 randomStartSelfAngleRange
    {
      get { return _randomStartSelfAngleRange; }
      set { _randomStartSelfAngleRange = value; }
    }
    private bool _effectNotRotating = default(bool);
    [global::ProtoBuf.ProtoMember(44, IsRequired = false, Name=@"effectNotRotating", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool effectNotRotating
    {
      get { return _effectNotRotating; }
      set { _effectNotRotating = value; }
    }
    private bool _autoChangeAngle = default(bool);
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"autoChangeAngle", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool autoChangeAngle
    {
      get { return _autoChangeAngle; }
      set { _autoChangeAngle = value; }
    }
    private bool _placeToFoothol = default(bool);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"placeToFoothol", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool placeToFoothol
    {
      get { return _placeToFoothol; }
      set { _placeToFoothol = value; }
    }
    private float _startSpeed = (float)5;
    [global::ProtoBuf.ProtoMember(10, IsRequired = false, Name=@"startSpeed", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)5)]
    public float startSpeed
    {
      get { return _startSpeed; }
      set { _startSpeed = value; }
    }
    private float _acceleration = (float)0;
    [global::ProtoBuf.ProtoMember(11, IsRequired = false, Name=@"acceleration", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float acceleration
    {
      get { return _acceleration; }
      set { _acceleration = value; }
    }
    private float _randomSpeed = (float)0;
    [global::ProtoBuf.ProtoMember(56, IsRequired = false, Name=@"randomSpeed", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float randomSpeed
    {
      get { return _randomSpeed; }
      set { _randomSpeed = value; }
    }
    private bool _randomSpeedDirection = default(bool);
    [global::ProtoBuf.ProtoMember(55, IsRequired = false, Name=@"randomSpeedDirection", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool randomSpeedDirection
    {
      get { return _randomSpeedDirection; }
      set { _randomSpeedDirection = value; }
    }
    private ActionData.LVector3 _fieldForce = null;
    [global::ProtoBuf.ProtoMember(12, IsRequired = false, Name=@"fieldForce", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 fieldForce
    {
      get { return _fieldForce; }
      set { _fieldForce = value; }
    }
    private bool _lockDirection = default(bool);
    [global::ProtoBuf.ProtoMember(21, IsRequired = false, Name=@"lockDirection", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool lockDirection
    {
      get { return _lockDirection; }
      set { _lockDirection = value; }
    }
    private ActionData.LVector3 _sizeBySpeed = null;
    [global::ProtoBuf.ProtoMember(35, IsRequired = false, Name=@"sizeBySpeed", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 sizeBySpeed
    {
      get { return _sizeBySpeed; }
      set { _sizeBySpeed = value; }
    }
    private float _trackingRate = (float)0;
    [global::ProtoBuf.ProtoMember(13, IsRequired = false, Name=@"trackingRate", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float trackingRate
    {
      get { return _trackingRate; }
      set { _trackingRate = value; }
    }
    private ActionData.LAddProjectile.Camp _camp = ActionData.LAddProjectile.Camp.CURRENT_CAMP;
    [global::ProtoBuf.ProtoMember(14, IsRequired = false, Name=@"camp", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(ActionData.LAddProjectile.Camp.CURRENT_CAMP)]
    public ActionData.LAddProjectile.Camp camp
    {
      get { return _camp; }
      set { _camp = value; }
    }
    private bool _bindBone = default(bool);
    [global::ProtoBuf.ProtoMember(15, IsRequired = false, Name=@"bindBone", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool bindBone
    {
      get { return _bindBone; }
      set { _bindBone = value; }
    }
    private string _bindName = "";
    [global::ProtoBuf.ProtoMember(16, IsRequired = false, Name=@"bindName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string bindName
    {
      get { return _bindName; }
      set { _bindName = value; }
    }
    private ActionData.LAddProjectile.EmitterAreaShape _emitterAreaShape = ActionData.LAddProjectile.EmitterAreaShape.CLOSE;
    [global::ProtoBuf.ProtoMember(31, IsRequired = false, Name=@"emitterAreaShape", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(ActionData.LAddProjectile.EmitterAreaShape.CLOSE)]
    public ActionData.LAddProjectile.EmitterAreaShape emitterAreaShape
    {
      get { return _emitterAreaShape; }
      set { _emitterAreaShape = value; }
    }
    private ActionData.LVector3 _emitterAreaScale = null;
    [global::ProtoBuf.ProtoMember(32, IsRequired = false, Name=@"emitterAreaScale", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 emitterAreaScale
    {
      get { return _emitterAreaScale; }
      set { _emitterAreaScale = value; }
    }
    private ActionData.LVector3 _emitterAreaRotation = null;
    [global::ProtoBuf.ProtoMember(33, IsRequired = false, Name=@"emitterAreaRotation", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 emitterAreaRotation
    {
      get { return _emitterAreaRotation; }
      set { _emitterAreaRotation = value; }
    }
    private bool _surfaceDistribution = default(bool);
    [global::ProtoBuf.ProtoMember(62, IsRequired = false, Name=@"surfaceDistribution", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool surfaceDistribution
    {
      get { return _surfaceDistribution; }
      set { _surfaceDistribution = value; }
    }
    private ActionData.LAddProjectile.SurroundType _surroundType = ActionData.LAddProjectile.SurroundType.SURROUND_LOCATE;
    [global::ProtoBuf.ProtoMember(26, IsRequired = false, Name=@"surroundType", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(ActionData.LAddProjectile.SurroundType.SURROUND_LOCATE)]
    public ActionData.LAddProjectile.SurroundType surroundType
    {
      get { return _surroundType; }
      set { _surroundType = value; }
    }
    private ActionData.LVector3 _surroundOffset = null;
    [global::ProtoBuf.ProtoMember(20, IsRequired = false, Name=@"surroundOffset", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 surroundOffset
    {
      get { return _surroundOffset; }
      set { _surroundOffset = value; }
    }
    private ActionData.LVector3 _surroundOffsetRandom = null;
    [global::ProtoBuf.ProtoMember(50, IsRequired = false, Name=@"surroundOffsetRandom", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 surroundOffsetRandom
    {
      get { return _surroundOffsetRandom; }
      set { _surroundOffsetRandom = value; }
    }
    private float _surroundRadius = (float)1;
    [global::ProtoBuf.ProtoMember(27, IsRequired = false, Name=@"surroundRadius", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)1)]
    public float surroundRadius
    {
      get { return _surroundRadius; }
      set { _surroundRadius = value; }
    }
    private float _surroundRadiusChangeSpeed = (float)0;
    [global::ProtoBuf.ProtoMember(58, IsRequired = false, Name=@"surroundRadiusChangeSpeed", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float surroundRadiusChangeSpeed
    {
      get { return _surroundRadiusChangeSpeed; }
      set { _surroundRadiusChangeSpeed = value; }
    }
    private float _surroundRadiusRandom = (float)0;
    [global::ProtoBuf.ProtoMember(51, IsRequired = false, Name=@"surroundRadiusRandom", DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
    [global::System.ComponentModel.DefaultValue((float)0)]
    public float surroundRadiusRandom
    {
      get { return _surroundRadiusRandom; }
      set { _surroundRadiusRandom = value; }
    }
    private ActionData.LVector3 _surroundScale = null;
    [global::ProtoBuf.ProtoMember(28, IsRequired = false, Name=@"surroundScale", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 surroundScale
    {
      get { return _surroundScale; }
      set { _surroundScale = value; }
    }
    private ActionData.LVector3 _surroundRotation = null;
    [global::ProtoBuf.ProtoMember(29, IsRequired = false, Name=@"surroundRotation", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 surroundRotation
    {
      get { return _surroundRotation; }
      set { _surroundRotation = value; }
    }
    private bool _lockLinkRotationX = default(bool);
    [global::ProtoBuf.ProtoMember(60, IsRequired = false, Name=@"lockLinkRotationX", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool lockLinkRotationX
    {
      get { return _lockLinkRotationX; }
      set { _lockLinkRotationX = value; }
    }
    private bool _dontDestroyWithTarget = default(bool);
    [global::ProtoBuf.ProtoMember(61, IsRequired = false, Name=@"dontDestroyWithTarget", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool dontDestroyWithTarget
    {
      get { return _dontDestroyWithTarget; }
      set { _dontDestroyWithTarget = value; }
    }
    private bool _withMoveControl = default(bool);
    [global::ProtoBuf.ProtoMember(41, IsRequired = false, Name=@"withMoveControl", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool withMoveControl
    {
      get { return _withMoveControl; }
      set { _withMoveControl = value; }
    }
    private ActionData.LVector3 _moveSpeed = null;
    [global::ProtoBuf.ProtoMember(43, IsRequired = false, Name=@"moveSpeed", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public ActionData.LVector3 moveSpeed
    {
      get { return _moveSpeed; }
      set { _moveSpeed = value; }
    }
    private ActionData.LTimeEventProType _eventType;
    [global::ProtoBuf.ProtoMember(17, IsRequired = true, Name=@"eventType", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public ActionData.LTimeEventProType eventType
    {
      get { return _eventType; }
      set { _eventType = value; }
    }
    [global::ProtoBuf.ProtoContract(Name=@"TypeName")]
    public enum TypeName
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"DEFAULT", Value=0)]
      DEFAULT = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"FOLLOW", Value=1)]
      FOLLOW = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SURROUND", Value=2)]
      SURROUND = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"LINK", Value=3)]
      LINK = 3,
            
      [global::ProtoBuf.ProtoEnum(Name=@"CHAIN", Value=4)]
      CHAIN = 4,
            
      [global::ProtoBuf.ProtoEnum(Name=@"PARABOLA", Value=5)]
      PARABOLA = 5
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
  
    [global::ProtoBuf.ProtoContract(Name=@"EmitterAreaShape")]
    public enum EmitterAreaShape
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"CLOSE", Value=0)]
      CLOSE = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"EMITTER_BOX", Value=1)]
      EMITTER_BOX = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"EMITTER_SPHERE", Value=2)]
      EMITTER_SPHERE = 2
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"SurroundType")]
    public enum SurroundType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"SURROUND_LOCATE", Value=0)]
      SURROUND_LOCATE = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SURROUND_RING", Value=1)]
      SURROUND_RING = 1,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SURROUND_SPHERE", Value=2)]
      SURROUND_SPHERE = 2,
            
      [global::ProtoBuf.ProtoEnum(Name=@"SURROUND_RANDOM", Value=3)]
      SURROUND_RANDOM = 3
    }
  
    [global::ProtoBuf.ProtoContract(Name=@"FunctionType")]
    public enum FunctionType
    {
            
      [global::ProtoBuf.ProtoEnum(Name=@"NORMAL", Value=0)]
      NORMAL = 0,
            
      [global::ProtoBuf.ProtoEnum(Name=@"IMPORTENT", Value=1)]
      IMPORTENT = 1
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}