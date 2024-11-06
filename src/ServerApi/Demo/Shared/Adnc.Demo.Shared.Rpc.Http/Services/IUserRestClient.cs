namespace Adnc.Demo.Shared.Rpc.Http.Services
{
    public interface IUserRestClient
    {
        [Put("/message/api/change/path")]
        Task ChangeThumingAsync(MessageUpdationRto input);

        [Post("/message/api/ins")]
        Task<ApiResponse<long>> CreateMessageAsync(MessageCreationRto input);

        [Put("/auth/api/change/photo")]
        Task<ApiResponse<string>> ChangePhotoAsync(ChangePhotoRto input);

        [Post("/user/api/media/check")]
        Task<ApiResponse<object>> MediaCheckDto(MediaCheckRto input);

        [Get("/auth/api/account/list")]
        Task<ApiResponse<List<AccountRto>>> GetAccountList();
    }
}
