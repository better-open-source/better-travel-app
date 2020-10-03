ARG DotnetVersion=3.1

FROM mcr.microsoft.com/dotnet/core/sdk:$DotnetVersion-alpine AS build
WORKDIR /app

COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do \
        echo $file \
        && mkdir -p src/${file%.*}/ \
        && mv $file src/${file%.*}/; \
    done

RUN dotnet restore ./src/BetterTravel.App/BetterTravel.App.csproj

COPY . ./

FROM build AS publish
RUN dotnet publish ./src/BetterTravel.App/BetterTravel.App.csproj -c Release --no-restore -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

COPY --from=publish /app/publish/wwwroot /usr/local/webapp/nginx/html
COPY nginx.conf /etc/nginx/nginx.conf