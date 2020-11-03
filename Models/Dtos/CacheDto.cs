namespace SEBtask.Models.Dtos
{
    public class CacheDto<T>
    {
        public bool IsSuccess { get; set; }
        public T Property { get; set; }
    }
}
