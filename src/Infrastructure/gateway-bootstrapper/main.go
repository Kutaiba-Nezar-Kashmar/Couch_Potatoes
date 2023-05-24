package main

import (
    "gateway-bootstrapper/internal/bootstrap"
    "log"
)

func main() {
    err := bootstrap.Gateway()
    if err != nil {
        log.Fatal(err)
    }
}
