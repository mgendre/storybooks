# storybooks

## How to start

First run the docker-compose on the root folder.

Then access the Cosmos DB configuration page:
https://localhost:8081/_explorer/index.html

You will also find the connection parameters.
By default the development profile is already configured.
For the production you should override parameters with environment variables.

### Prepare CosmosDB emulator

**Linux**

https://docs.microsoft.com/en-us/azure/cosmos-db/linux-emulator?tabs=ssl-netstd21

```
curl -k https://localhost:8081/_explorer/emulator.pem > ~/emulatorcert.crt
sudo cp ~/emulatorcert.crt /usr/local/share/ca-certificates/
sudo update-ca-certificates
```