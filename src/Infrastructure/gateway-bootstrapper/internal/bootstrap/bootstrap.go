package bootstrap

import (
    "fmt"
    "gateway-bootstrapper/internal/kong"
    "gopkg.in/yaml.v3"
    "log"
    "os"
    "strings"
)

func Gateway() error {
    serviceConfig, err := kong.ReadServicesConfiguration("/bootstrapper/services.json")
    if err != nil {
        return err
    }

    log.Printf("Detected %v services: \n", len(serviceConfig.Services))
    for i, service := range serviceConfig.Services {
        log.Printf("Service %v: %v", i+1, service.Name)
    }

    kongFile := kong.Kong{
        FormatVersion: "3.0",
        Transform:     true,
    }

    kongServices := make([]kong.KongService, 0)

    for _, service := range serviceConfig.Services {
        kongRoutes := make([]kong.KongRoute, 0)
        host := os.Getenv(serviceNameToEnvironmentVariableFormat(service.Name))

        log.Printf("%v will be used as URL for service %v", host, service.Name)

        for _, route := range service.Routes {
            kongRoutes = append(kongRoutes, kong.KongRoute{
                Name:  route.Name,
                Paths: route.Paths,
            })
        }

        kongService := kong.KongService{
            Name:   service.Name,
            URL:    host,
            Routes: kongRoutes,
        }
        kongServices = append(kongServices, kongService)
    }

    kongFile.Services = kongServices

    kongFileSerialized, err := yaml.Marshal(&kongFile)
    if err != nil {
        return err
    }

    log.Printf("Writing the following kong.yaml configuration:\n%s\n", kongFileSerialized)
    err = kongFile.WriteKongFile("/bootstrapper/kong/declarative")
    if err != nil {
        return err
    }
    log.Println("Kong files has been created.")

    return nil
}

func serviceNameToEnvironmentVariableFormat(s string) string {
    sUpper := strings.ToUpper(s)
    sSeparatorReplaced := strings.ReplaceAll(sUpper, "-", "_")
    environmentVariableKey := fmt.Sprintf("GATEWAY_%v", sSeparatorReplaced)
    return environmentVariableKey
}
