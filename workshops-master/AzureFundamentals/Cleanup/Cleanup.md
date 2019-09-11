

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

# Clean Up Resources

Once you complete this lab, you can delete all resources

## Remove Backup Jobs

```bash
az backup protection disable --resource-group AzureLab --vault-name myRecoveryServicesVault --container-name myVM1 --item-name myVM1 --delete-backup-data true

az backup vault delete --resource-group AzureLab --name myRecoveryServicesVault az group delete --name myResourceGroup
```

----

## Delete Resource Group

```bash
az group delete -n AzureLab
```
[Back to Start]

[Back to Start]: <https://github.com/Microsoft-USEduAzure/workshops/tree/master/AzureFundamentals>

