# Cloud Infrastructure (WORK IN PROGRESS!)

Declares cloud infrastructure needed for deploying the full solution of Couch Potatoes.

# Local Setup

## Install gcloud cli

The easist way to authenticate against our cloud infrastructure is by using `gcloud` (Google's CLI tool). By using gcloud we can interact with GCP without having to generate a service account key and setting up the `GOOGLE_APPLICATION_CREDENTIALS` environment variable (YIKES).

Follow the steps on this link:

```link
https://cloud.google.com/sdk/docs/install#windows
```

After gcloud has been installed run:

```bash
$ glcoud init
```

and

```bash
$ gcloud auth application-default login
```

# Usage

## See changes

To preview changes before deploying run:

```bash
$ terraform plan
```

## Provision infrastructure

To deploy all infrastructure run:

```bash
$ terraform apply
```

## Shutdown infrastructure

To shutdown all provisioned resources run:

```bash
$ terraform destroy
```
