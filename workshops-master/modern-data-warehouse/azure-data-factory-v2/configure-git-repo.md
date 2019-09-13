# Configure Git Repository

> **NOTE:** If you are using Azure DevOps, you must have your Azure DevOps Organization already created prior to proceeding.

## Pre-requisite: [Provision Azure Data Factory V2](provision-azure-data-factory-v2.md)

1. On the ADF overview page, click **Set up Code Repository**

    ![Setup Code Repository](media/repo/1.png)

1. Select **Repository Type** and select the appropriate organization/project/branch.

    > **NOTE:** ADF supports either Azure DevOps or GitHub.

    - ### Settings for Azure DevOps:

        ![Azure DevOps](media/repo/2.png)

    - ### Settings for GitHub:

        ![GitHub](media/repo/3.png)

1. In the Overview page, click the **Create pipeline from template** button.

    ![Create pipeline from template](media/repo/4.png)

1. If prompted, select the working branch and click the **Save** button.

    ![Select working branch](media/repo/5.png)

1. Verify the correct branch is selected.

    ![Verify branch](media/repo/6.png)

1. Once you select the branch your ADF workspace will now be connected to your Git repo. To publish workspace changes, simply click the **Publish** button in to commit and push changes to your remote repository.

    ![](media/repo/7.png)
    
## Next task: [Build copy pipeline using Azure Data Factory](copy-file-into-adls-gen2.md)