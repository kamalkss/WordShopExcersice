namespace WordShopExcersice.Services;

public class ServiceResponse<T>
{
    public bool IsSuccessed { get; set; }
    public string message { get; set; }
    public DateTime DateTime { get; set; }
    public T Result { get; set; }
}