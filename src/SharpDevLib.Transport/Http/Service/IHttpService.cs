namespace SharpDevLib.Transport;

/// <summary>
/// http服务抽象
/// </summary>
public interface IHttpService
{
    /// <summary>
    /// get请求
    /// </summary>
    /// <typeparam name="T">返回数据的类型</typeparam>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancllation token</param>
    /// <returns>http响应</returns>
    Task<HttpResponse<T>> GetAsync<T>(HttpKeyValueRequest request, CancellationToken? cancellationToken = null);

    /// <summary>
    /// get请求
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancllation token</param>
    /// <returns>http响应</returns>
    Task<HttpResponse> GetAsync(HttpKeyValueRequest request, CancellationToken? cancellationToken = null);

    /// <summary>
    /// get流请求
    /// </summary>
    /// <param name="request">请求</param>
    /// <returns>流</returns>
    Task<Stream> GetStreamAsync(HttpKeyValueRequest request);

    /// <summary>
    /// post请求
    /// </summary>
    /// <typeparam name="T">响应数据类型</typeparam>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancllation token</param>
    /// <returns>http响应</returns>
    Task<HttpResponse<T>> PostAsync<T>(HttpJsonRequest request, CancellationToken? cancellationToken = null);

    /// <summary>
    /// post请求
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancllation token</param>
    /// <returns>http响应</returns>
    Task<HttpResponse> PostAsync(HttpJsonRequest request, CancellationToken? cancellationToken = null);

    /// <summary>
    /// post请求
    /// </summary>
    /// <typeparam name="T">响应数据类型</typeparam>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancllation token</param>
    /// <returns>http响应</returns>
    Task<HttpResponse<T>> PostAsync<T>(HttpMultiPartFormDataRequest request, CancellationToken? cancellationToken = null);

    /// <summary>
    /// post请求
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancllation token</param>
    /// <returns>http响应</returns>
    Task<HttpResponse> PostAsync(HttpMultiPartFormDataRequest request, CancellationToken? cancellationToken = null);

    /// <summary>
    /// post请求
    /// </summary>
    /// <typeparam name="T">响应数据类型</typeparam>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancllation token</param>
    /// <returns>http响应</returns>
    Task<HttpResponse<T>> PostAsync<T>(HttpUrlEncodedFormRequest request, CancellationToken? cancellationToken = null);

    /// <summary>
    /// post请求
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancllation token</param>
    /// <returns>http响应</returns>
    Task<HttpResponse> PostAsync(HttpUrlEncodedFormRequest request, CancellationToken? cancellationToken = null);

    /// <summary>
    /// put请求
    /// </summary>
    /// <typeparam name="T">响应数据类型</typeparam>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancllation token</param>
    /// <returns>http响应</returns>
    Task<HttpResponse<T>> PutAsync<T>(HttpJsonRequest request, CancellationToken? cancellationToken = null);

    /// <summary>
    /// put请求
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancllation token</param>
    /// <returns>http响应</returns>
    Task<HttpResponse> PutAsync(HttpJsonRequest request, CancellationToken? cancellationToken = null);

    /// <summary>
    /// delete请求
    /// </summary>
    /// <typeparam name="T">响应数据类型</typeparam>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancllation token</param>
    /// <returns>http响应</returns>
    Task<HttpResponse<T>> DeleteAsync<T>(HttpKeyValueRequest request, CancellationToken? cancellationToken = null);

    /// <summary>
    /// delete请求
    /// </summary>
    /// <param name="request">请求</param>
    /// <param name="cancellationToken">cancllation token</param>
    /// <returns>http响应</returns>
    Task<HttpResponse> DeleteAsync(HttpKeyValueRequest request, CancellationToken? cancellationToken = null);
}