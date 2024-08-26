/// <summary>
/// Adjusts the value to be within the range of -180 to 180.
/// </summary>
private float ArrangeRotationValue(float value)
{
    if (value > 180f)
    {
        value -= 360f;
    }
    else if (value < -180f)
    {
        value += 360f;
    }

    return value;
}

/// <summary>
/// Adjusts the angle to 0 degrees if it is less than or equal to 45 degrees, 
/// or to 90 degrees if it is greater than 45 degrees.
/// </summary>
private float AdjustAngle(float angle, float roundAngle)
{
    int meta = Mathf.FloorToInt(angle / roundAngle) + (angle > 0 ? 1 : -1);
    meta = Mathf.FloorToInt(meta / 2);
    return Mathf.Round(meta * (roundAngle * 2));
}
