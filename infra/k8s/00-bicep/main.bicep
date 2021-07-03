param storageAccountName string = 'stgOslo'
param storageAccountLocation string = 'westeurope'

resource stg 'Microsoft.Storage/storageAccounts@2021-04-01' = {
  name: storageAccountName
  location: storageAccountLocation
  kind: 'StorageV2'
  sku:{
    name:'Standard_LRS'
    tier:'Standard'
  }
  properties:{
    accessTier:'Cool'
  }
}

output stgout string = stg.properties.primaryEndpoints.blob
