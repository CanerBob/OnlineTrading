namespace Trading.Service.Layer.Abstract;
public interface IValidator<T> where T : class
{
    public string ErrorMessage { get; set; }
    bool Validation (T entity);
}