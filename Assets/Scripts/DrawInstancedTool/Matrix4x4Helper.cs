using UnityEngine;

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
    public static void SetTRS(ref Matrix4x4 m, ref Vector3 p, ref Quaternion q, ref Vector3 s)
    {
        SetMatrixRotation(ref m, ref q);
        SetMatrixScale(ref m, ref s);
        SetMatrixPosition(ref m, ref p);
    }

    /// <summary>
    /// 设置矩阵旋转和位移
    /// </summary>
    /// <param name="m"></param>
    /// <param name="p"></param>
    /// <param name="q"></param>
    public static void SetTRS(ref Matrix4x4 m, ref Vector3 p, ref Quaternion q)
    {
        //计算旋转
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

        //矩阵位移
        m.m03 = p.x;
        m.m13 = p.y;
        m.m23 = p.z;
    }

    public static void SetTRS_YZ(ref Matrix4x4 m, ref Vector3 p, ref Quaternion q)
    {
        //计算旋转
        float y = q.y * 2.0F;
        float z = q.z * 2.0F;
        float yy = q.y * y;
        float zz = q.z * z;
        float yz = q.y * z;

        m.m00 = 1.0f - (yy + zz);
        m.m11 = 1.0f - zz;
        m.m21 = yz;
        m.m12 = yz;
        m.m22 = 1.0f - yy;
        m.m33 = 1.0F;

        //矩阵位移
        m.m03 = p.x;
        m.m13 = p.y;
        m.m23 = p.z;
    }

    /// <summary>
    /// 设置矩阵旋转和位移
    /// </summary>
    /// <param name="m"></param>
    /// <param name="p"></param>
    /// <param name="q"></param>
    public static void SetTRSHorizontal(ref Matrix4x4 m, ref Vector3 p, ref Quaternion q)
    {
        //计算旋转
        float y = q.y * 2.0F;
        float yy = q.y * y;
        float wy = q.w * y;

        m.m00 = 1.0f - yy;
        m.m20 = -wy;
        m.m11 = 1.0f;
        m.m02 = wy;
        m.m22 = 1.0f - yy;
        m.m33 = 1.0F;

        //矩阵位移
        m.m03 = p.x;
        m.m13 = p.y;
        m.m23 = p.z;
    }

    /// <summary>
    /// 矩阵缩放
    /// </summary>
    /// <param name="m"></param>
    /// <param name="s"></param>
    public static void SetMatrixScale(ref Matrix4x4 m, ref Vector3 s)
    {
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
    public static void SetMatrixPosition(ref Matrix4x4 m, ref Vector3 p)
    {
        m.m03 = p.x;
        m.m13 = p.y;
        m.m23 = p.z;
    }

    /// <summary>
    /// 矩阵旋转
    /// </summary>
    /// <param name="m"></param>
    /// <param name="q"></param>
    public static void SetMatrixRotation(ref Matrix4x4 m, ref Quaternion q)
    {
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
