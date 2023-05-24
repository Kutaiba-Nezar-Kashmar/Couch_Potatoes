package kong

import (
    "encoding/json"
    "io/ioutil"
    "os"
)

type ServicesConfiguration struct {
    Services []ServicesConfigurationService `json:services`
}

type ServicesConfigurationService struct {
    Name   string                       `json:name`
    Routes []ServicesConfigurationRoute `json:routes`
}

type ServicesConfigurationRoute struct {
    Name  string   `json:name`
    Paths []string `json:paths`
}

func ReadServicesConfiguration(file string) (*ServicesConfiguration, error) {
    jsonFile, err := os.Open(file)
    if err != nil {
        return &ServicesConfiguration{}, err
    }
    defer jsonFile.Close()

    bytes, err := ioutil.ReadAll(jsonFile)
    var config ServicesConfiguration

    err = json.Unmarshal(bytes, &config)
    if err != nil {
        return &ServicesConfiguration{}, err
    }

    return &config, nil
}
