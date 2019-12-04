namespace Res
{
    /// <summary>
    /// 资源加载路径类型
    /// </summary>

    public enum enResPathType
    {
        /// <summary>
        /// 从读写路径加载
        /// </summary>

        LoadPathFromReadWrite = 0,

        /// <summary>
        /// 从只读路径加载
        /// </summary>

        LoadPathFromOnlyRead,

        /// <summary>
        /// 从磁盘路径加载(仅在PC上使用)
        /// </summary>

        LoadPathFromDirctory,
    }
}