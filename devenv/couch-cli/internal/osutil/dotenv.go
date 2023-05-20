package osutil

import (
	"bufio"
	"io/fs"
	"io/ioutil"
	"log"
	"os"
	"path/filepath"
	"strings"
)

type MaybeEnv struct {
	HasEnvFile  bool
	EnvFilePath string
}

func FindDotEnvFile(searchStartingPath string) (*MaybeEnv, error) {
	envFile := ""

	err := filepath.Walk(searchStartingPath, func(path string, info fs.FileInfo, err error) error {
		if info.Name() == ".env" {
			envFile = path
		}

		return nil
	})

	if err != nil {
		return &MaybeEnv{
			HasEnvFile:  false,
			EnvFilePath: "",
		}, err
	}

	if envFile == "" {
		return &MaybeEnv{
			HasEnvFile:  false,
			EnvFilePath: "",
		}, nil
	}

	return &MaybeEnv{HasEnvFile: true, EnvFilePath: envFile}, nil
}

func ApplyDotenv(dotenvFile string) error {
	bytes, err := ioutil.ReadFile(dotenvFile)
	if err != nil {
		return err
	}

	fileContents := string(bytes)
	scanner := bufio.NewScanner(strings.NewReader(fileContents))
	scanner.Split(bufio.ScanLines)
	for scanner.Scan() {
		parts := strings.Split(scanner.Text(), "=")
		if len(parts) == 2 {
			key := parts[0]
			val := parts[1]
			log.Printf("Setting environment variable %v", key)
			err2 := os.Setenv(key, val)
			if err2 != nil {
				return err2
			}
		}
	}

	return nil
}
