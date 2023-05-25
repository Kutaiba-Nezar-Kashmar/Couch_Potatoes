$BUCKET = "gs://couch-potatoes-sep6.appspot.com"

$gatewayUrl = terraform output -raw gateway_url

$couchConfig = @{
    'baseUrl' = "$gatewayUrl/couch-potatoes/api/v1"
}

$couchConfigJson = $couchConfig | ConvertTo-Json

$couchConfigJson | Out-File -FilePath '.\couch-config.json' -Force

gcloud storage cp '.\couch-config.json' $BUCKET

