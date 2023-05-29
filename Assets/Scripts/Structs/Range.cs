[System.Serializable]
public struct Range
{
    public Range(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
    public float min;
    public float max;
}
