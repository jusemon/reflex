[System.Serializable]
public struct ApiResponse<T>
{
    public T[] data;
    public int page;
    public int pageSize;
}
