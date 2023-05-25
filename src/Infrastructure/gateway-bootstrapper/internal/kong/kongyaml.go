package kong

import (
    "fmt"
    "gopkg.in/yaml.v3"
    "io/ioutil"
)

type Kong struct {
    FormatVersion string        `yaml:"_format_version"`
    Transform     bool          `yaml:"_transform"`
    Services      []KongService `yaml:"services"`
}

type KongService struct {
    Name   string      `yaml:"name"`
    URL    string      `yaml:"url"`
    Routes []KongRoute `yaml:"routes"`
}

type KongRoute struct {
    Name      string   `yaml:"name"`
    StripPath bool     `yaml:"strip_path"`
    Paths     []string `yaml:"paths"`
}

func (kong *Kong) WriteKongFile(folder string) error {

    asYaml, err := yaml.Marshal(&kong)
    if err != nil {
        return err
    }

    err = ioutil.WriteFile(fmt.Sprintf("%v/kong.yaml", folder), asYaml, 0644)
    if err != nil {
        return err
    }

    return nil
}
