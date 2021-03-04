FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY omnia-blazor-demo.csproj .
RUN dotnet restore "omnia-blazor-demo.csproj"
COPY . .
RUN dotnet build "omnia-blazor-demo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "omnia-blazor-demo.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf