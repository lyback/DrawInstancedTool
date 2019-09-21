using UnityEngine;

//|M3x3 T3|
//|t0   1 |

public static class Matrix4x4Helper
{
    #region 矩阵计算
    /// <summary>
    /// 重置矩阵参数
    /// </summary>
    /// <param name="m"></param>
    /// <param name="p"></param>
    /// <param name="q"></param>
    /// <param name="s"></param>
    public static void SetTRS(ref Matrix4x4 m, Vector3 p, Vector3 r, Vector3 s)
    {
        SetMatrixPosition(ref m, p);
        SetRS(ref m, r, s);
    }

    /// <summary>
    /// 设置矩阵旋转和缩放
    /// </summary>
    /// <param name="m"></param>
    /// <param name="p"></param>
    /// <param name="q"></param>
    public static void SetRS(ref Matrix4x4 m, Vector3 r, Vector3 s)
    {
        Quaternion q = Quaternion.Euler(r);
        //旋转
        float x = q.x * 2.0F;
        float y = q.y * 2.0F;
        float z = q.z * 2.0F;
        float xx = q.x * x;
        float yy = q.y * y;
        float zz = q.z * z;
        float xy = q.x * y;
        float xz = q.x * z;
        float yz = q.y * z;
        float wx = q.w * x;
        float wy = q.w * y;
        float wz = q.w * z;

        m.m00 = 1.0f - (yy + zz);
        m.m10 = xy + wz;
        m.m20 = xz - wy;
        m.m30 = 0.0F;

        m.m01 = xy - wz;
        m.m11 = 1.0f - (xx + zz);
        m.m21 = yz + wx;
        m.m31 = 0.0F;

        m.m02 = xz + wy;
        m.m12 = yz - wx;
        m.m22 = 1.0f - (xx + yy);
        m.m32 = 0.0F;

        m.m33 = 1.0F;

        //乘以缩放
        m.m00 *= s.x;
        m.m10 *= s.x;
        m.m20 *= s.x;

        m.m01 *= s.y;
        m.m11 *= s.y;
        m.m21 *= s.y;

        m.m02 *= s.z;
        m.m12 *= s.z;
        m.m22 *= s.z;
    }

    /// <summary>
    /// 矩阵位移
    /// </summary>
    /// <param name="m"></param>
    /// <param name="p"></param>
    public static void SetMatrixPosition(ref Matrix4x4 m, Vector3 p)
    {
        m.m03 = p.x;
        m.m13 = p.y;
        m.m23 = p.z;
    }
    /// <summary>
    /// 矩阵缩放（会重置旋转）
    /// </summary>
    /// <param name="m"></param>
    /// <param name="s"></param>
    public static void SetMatrixScaleAndResetRot(ref Matrix4x4 m, Vector3 s)
    {
        m.m00 = s.x;
        m.m11 = s.y;
        m.m22 = s.z;
    }
    /// <summary>
    /// 矩阵旋转（会重置缩放）
    /// </summary>
    /// <param name="m"></param>
    /// <param name="q"></param>
    public static void SetMatrixRotationAndResetScale(ref Matrix4x4 m, Vector3 r)
    {
        Quaternion q = Quaternion.Euler(r);
        float x = q.x * 2.0F;
        float y = q.y * 2.0F;
        float z = q.z * 2.0F;
        float xx = q.x * x;
        float yy = q.y * y;
        float zz = q.z * z;
        float xy = q.x * y;
        float xz = q.x * z;
        float yz = q.y * z;
        float wx = q.w * x;
        float wy = q.w * y;
        float wz = q.w * z;

        m.m00 = 1.0f - (yy + zz);
        m.m10 = xy + wz;
        m.m20 = xz - wy;
        m.m30 = 0.0F;

        m.m01 = xy - wz;
        m.m11 = 1.0f - (xx + zz);
        m.m21 = yz + wx;
        m.m31 = 0.0F;

        m.m02 = xz + wy;
        m.m12 = yz - wx;
        m.m22 = 1.0f - (xx + yy);
        m.m32 = 0.0F;

        m.m33 = 1.0F;
    }
    #endregion
}
