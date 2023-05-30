#!/bin/sh

echo Bootstrapping gateway...

export KONG_DATABASE=off
export KONG_DECLARATIVE_CONFIG=/bootstrapper/kong/declarative/kong.yaml
export KONG_PROXY_ACCESS_LOG=/dev/stdout
export KONG_ADMIN_ACCESS_LOG=/dev/stdout
export KONG_PROXY_ERROR_LOG=/dev/stderr
export KONG_ADMIN_LISTEN=0.0.0.0:8001

echo Kong API Gateway starting with configuration:
cat /bootstrapper/kong/declarative/kong.yaml

/bootstrapper/gateway-bootstrapper && /docker-entrypoint.sh kong docker-start