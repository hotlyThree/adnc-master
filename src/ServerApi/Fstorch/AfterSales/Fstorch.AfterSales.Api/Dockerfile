#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Fstorch/AfterSales/Fstorch.AfterSales.Api/Fstorch.AfterSales.Api.csproj", "Fstorch/AfterSales/Fstorch.AfterSales.Api/"]
COPY ["Demo/Shared/Adnc.Demo.Shared.Const/Adnc.Demo.Shared.Const.csproj", "Demo/Shared/Adnc.Demo.Shared.Const/"]
COPY ["ServiceShared/Adnc.Shared.WebApi/Adnc.Shared.WebApi.csproj", "ServiceShared/Adnc.Shared.WebApi/"]
COPY ["ServiceShared/Adnc.Shared.Application.Contracts/Adnc.Shared.Application.Contracts.csproj", "ServiceShared/Adnc.Shared.Application.Contracts/"]
COPY ["Infrastructures/Adnc.Infra.Core/Adnc.Infra.Core.csproj", "Infrastructures/Adnc.Infra.Core/"]
COPY ["ServiceShared/Adnc.Shared.Rpc/Adnc.Shared.Rpc.csproj", "ServiceShared/Adnc.Shared.Rpc/"]
COPY ["ServiceShared/Adnc.Shared/Adnc.Shared.csproj", "ServiceShared/Adnc.Shared/"]
COPY ["Infrastructures/Adnc.Infra.Helper/Adnc.Infra.Helper.csproj", "Infrastructures/Adnc.Infra.Helper/"]
COPY ["Infrastructures/Adnc.Infra.Consul/Adnc.Infra.Consul.csproj", "Infrastructures/Adnc.Infra.Consul/"]
COPY ["Infrastructures/Adnc.Infra.EventBus/Adnc.Infra.EventBus.csproj", "Infrastructures/Adnc.Infra.EventBus/"]
COPY ["Infrastructures/Adnc.Infra.Redis.Caching/Adnc.Infra.Redis.Caching.csproj", "Infrastructures/Adnc.Infra.Redis.Caching/"]
COPY ["Infrastructures/Adnc.Infra.Redis/Adnc.Infra.Redis.csproj", "Infrastructures/Adnc.Infra.Redis/"]
COPY ["Fstorch/AfterSales/Fstorch.AfterSales.Application/Fstorch.AfterSales.Application.csproj", "Fstorch/AfterSales/Fstorch.AfterSales.Application/"]
COPY ["Demo/Shared/Adnc.Demo.Shared.Rpc.Http/Adnc.Demo.Shared.Rpc.Http.csproj", "Demo/Shared/Adnc.Demo.Shared.Rpc.Http/"]
COPY ["ServiceShared/Adnc.Shared.Application/Adnc.Shared.Application.csproj", "ServiceShared/Adnc.Shared.Application/"]
COPY ["ServiceShared/Adnc.Shared.Repository/Adnc.Shared.Repository.csproj", "ServiceShared/Adnc.Shared.Repository/"]
COPY ["Infrastructures/Adnc.Infra.Repository/Adnc.Infra.Repository.csproj", "Infrastructures/Adnc.Infra.Repository/"]
COPY ["Infrastructures/Adnc.Infra.Dapper/Adnc.Infra.Repository.Dapper.csproj", "Infrastructures/Adnc.Infra.Dapper/"]
COPY ["Infrastructures/Adnc.Infra.EfCore.MySql/Adnc.Infra.Repository.EfCore.MySql.csproj", "Infrastructures/Adnc.Infra.EfCore.MySql/"]
COPY ["Infrastructures/Adnc.Infra.EfCore/Adnc.Infra.Repository.EfCore.csproj", "Infrastructures/Adnc.Infra.EfCore/"]
COPY ["Infrastructures/Adnc.Infra.IdGenerater/Adnc.Infra.IdGenerater.csproj", "Infrastructures/Adnc.Infra.IdGenerater/"]
COPY ["Infrastructures/Adnc.Infra.Mapper/Adnc.Infra.Mapper.csproj", "Infrastructures/Adnc.Infra.Mapper/"]
COPY ["Infrastructures/Adnc.Infra.Mongo/Adnc.Infra.Repository.Mongo.csproj", "Infrastructures/Adnc.Infra.Mongo/"]
COPY ["Fstorch/AfterSales/Fstorch.AfterSales.Repository/Fstorch.AfterSales.Repository.csproj", "Fstorch/AfterSales/Fstorch.AfterSales.Repository/"]
RUN dotnet restore "Fstorch/AfterSales/Fstorch.AfterSales.Api/Fstorch.AfterSales.Api.csproj"
COPY . .
WORKDIR "/src/Fstorch/AfterSales/Fstorch.AfterSales.Api"
RUN dotnet build "Fstorch.AfterSales.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fstorch.AfterSales.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fstorch.AfterSales.Api.dll"]