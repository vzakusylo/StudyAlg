// Specify the target Azure Resource Group and region
targetScope = 'subscription'
param location string = 'eastus'
param rgName string = 'MyResourceGroup'

// Define the Load Balancer properties
resource lb 'Microsoft.Network/loadBalancers@2021-03-01' = {
  name: 'myLoadBalancer'
  location: location
  properties: {
    frontendIPConfigurations: [
      {
        name: 'myFrontendIP'
        properties: {
          publicIPAddress: {
            id: resourceId('Microsoft.Network/publicIPAddresses', 'myPublicIP')
          }
        }
      }
    ]
    backendAddressPools: [
      {
        name: 'myBackendPool'
      }
    ]
    loadBalancingRules: [
      {
        name: 'myLBRule'
        properties: {
          frontendIPConfiguration: {
            id: lb.frontendIPConfigurations[0].id
          }
          backendAddressPool: {
            id: lb.backendAddressPools[0].id
          }
          protocol: 'Tcp'
          frontendPort: 80
          backendPort: 80
          enableFloatingIP: false
        }
      }
    ]
  }
}

// Create a public IP address
resource publicIP 'Microsoft.Network/publicIPAddresses@2021-03-01' = {
  name: 'myPublicIP'
  location: location
  properties: {
    publicIPAllocationMethod: 'Dynamic'
  }
}

// Output the Load Balancer public IP address
output lbPublicIP string = publicIP.properties.ipAddress
