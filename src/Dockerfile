FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["SouliCool.Tutorials/SouliCool.Tutorials.csproj", "SouliCool.Tutorials/"]
RUN dotnet restore "SouliCool.Tutorials/SouliCool.Tutorials.csproj"
COPY . .
WORKDIR "/src/SouliCool.Tutorials"
RUN dotnet build "SouliCool.Tutorials.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SouliCool.Tutorials.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SouliCool.Tutorials.dll"]