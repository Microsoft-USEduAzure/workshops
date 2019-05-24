[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

# Backup a VM in Azure

In this document you will learn how to:

- Create a recovery services vault
- Enable backup for an Azure VM
- Start a backup job
- Monitor the backup job

----

## Create a recovery services vault

A Recovery Services vault is a logical container that stores the backup data for each protected resource, such as Azure VMs. When the backup job for a protected resource runs, it creates a recovery point inside the Recovery Services vault. You can then use one of these recovery points to restore data to a given point in time.
Create a Recovery Services vault with az backup vault create. Specify the same resource group and location as the VM you wish to protect. On previous labs, you created:

a resource group named AzureLab,
a VM named myVM1,
resources in the eastus2 location.

```sh
az backup vault create --resource-group AzureLab --name myRecoveryServicesVault --location eastus2
```

By default, the Recovery Services vault is set for Geo-Redundant storage. Geo-Redundant storage ensures your backup data is replicated to a secondary Azure region that is hundreds of miles away from the primary region. If the storage redundancy setting needs to be modified, use az backup vault backup-properties set cmdlet.

```sh
az backup vault backup-properties set --name myRecoveryServicesVault --resource-group AzureLab --backup-storage-redundancy "LocallyRedundant/GeoRedundant"
```

Further documentation:  [Backup VM]

### Enable backup for an Azure VM

```sh
az backup protection enable-for-vm --resource-group AzureLab --vault-name myRecoveryServicesVault --vm $(az vm show -g AzureLab -n MyVm1 --query id | tr -d '"') --policy-name DefaultPolicy
```

----

### Start a backup job

To start a backup now rather than wait for the default policy to run the job at the scheduled time, use az backup protection backup-now. This first backup job creates a full recovery point. Each backup job after this initial backup creates incremental recovery points. Incremental recovery points are storage and time-efficient, as they only transfer changes made since the last backup.

The following parameters are used to back up the VM:
--container-name is the name of your VM
--item-name is the name of your VM
--retain-until value should be set to the last available date, in UTC time format (dd-mm-yyyy), that you wish the recovery point to be available

The following example backs up the VM named myVM and sets the expiration of the recovery point to October 18, 2019:

```sh
az backup protection backup-now \
    --resource-group AzureLab \
    --vault-name myRecoveryServicesVault \
    --container-name myVM1 \
    --item-name myVM1 \
    --retain-until 18-10-2019
```

### Monitor the backup job

```sh
az backup job list --resource-group AzureLab --vault-name myRecoveryServicesVault --output table
```

Output should look like this:

```sh
Name      Operation        Status      Item Name    Start Time UTC       Duration
--------  ---------------  ----------  -----------  -------------------  --------------
a0a8e5e6  Backup           InProgress  myVm1         2019-09-19T03:09:21  0:00:48.718366
fe5d0414  ConfigureBackup  Completed   myVm1         2019-09-19T03:03:57  0:00:31.191807

```

[Next -> Clean Up]

[Backup VM]: <https://docs.microsoft.com/en-us/azure/backup/quick-backup-vm-cli>
[Next -> Clean Up]:<https://github.com/Microsoft-USEduAzure/workshops/blob/master/AzureFundamentals/Cleanup/Cleanup.md>



