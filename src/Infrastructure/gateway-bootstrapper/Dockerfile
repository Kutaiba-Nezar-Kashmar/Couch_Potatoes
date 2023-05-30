FROM golang:alpine3.17 as build

WORKDIR /app
COPY . /app

RUN go build


FROM kong:3.3.0-alpine as final

USER root

RUN mkdir -p -- /bootstrapper/kong/declarative

COPY --from=build /app/gateway-bootstrapper /bootstrapper
COPY --from=build /app/bootstrap.sh /bootstrap.sh
COPY --from=build /app/services.json /bootstrapper/services.json

RUN chmod +x /bootstrap.sh

EXPOSE 8000
EXPOSE 8001

ENTRYPOINT [ "sh" ]
CMD ["/bootstrap.sh"]

