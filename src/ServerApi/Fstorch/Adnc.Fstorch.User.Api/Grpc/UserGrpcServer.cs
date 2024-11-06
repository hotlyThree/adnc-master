
using Adnc.Demo.Shared.Rpc.Grpc.Messages;
using Adnc.Demo.Shared.Rpc.Grpc.Services;
using Grpc.Core;

namespace Adnc.Fstorch.User.Api.Grpc
{
    public class UserGrpcServer : Adnc.Demo.Shared.Rpc.Grpc.Services.MsgGrpc.MsgGrpcBase
    {
        private readonly IMessageAppService _messageAppService;

        public UserGrpcServer(IMessageAppService messageAppService)
        {
            _messageAppService = messageAppService;
        }

        public override async Task<GrpcResponse> ChangeThumimg(ChangeThumimgRequest request, ServerCallContext context)
        {
            var changeResult = await _messageAppService.ChangeThumimg(new Application.Dtos.Message.MessageThumimgUpdationDto { Id = request.Id, Path = request.Path});
            var grpcResponse = new GrpcResponse() { IsSuccessStatusCode = changeResult.IsSuccess };
            if (!grpcResponse.IsSuccessStatusCode)
            {
                grpcResponse.Error = changeResult.ProblemDetails?.Detail;
                return grpcResponse;
            }
            return grpcResponse;
        }

    }
}
