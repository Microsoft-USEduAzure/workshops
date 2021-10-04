# Azure Serverless Workshop
**Deployment Instructions**


> * 1. Deploy the Function app ARM template.

[![Deploy FunctionApp To Azure](https://aka.ms/deploytoazurebutton)]("https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2FMicrosoft-USEduAzure%2Fworkshop%2Fmaster%2Fserverless%2Fdeploy%2Ffunction-http-trigger%2Fazuredeploy.json")

            Note: 
                - Choose the same Resource Group where you will deploy the Logic App. The Logic App template automatically derives the Function url, and assumes the Function resource exists in the same Resource Group.
                - The template also deploys a Key Vault and populates a secret with the function app's host key. This is an optional step since the Logic App does not fetch the host key from the vault. But this is the recommended secure method to retrieve secrets if you need to call the function from another app.
> * 2. Replace contents of the default run.csx file with the contents from the run.csx file in the repo.

> * 3. Deploy the logic app ARM template.

[![Deploy LogicApp To Azure](https://aka.ms/deploytoazurebutton)]("https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2FMicrosoft-USEduAzure%2Fworkshop%2Fmaster%2Fserverless%2Fdeploy%2Fazuredeploy-logicApp-SentimentAnalysis.json")

            Note: The Logic App template has been created to allow for flexibility i.e. it creates the full workflow with all the API connection resources but does not require those resources to pre-exist for the template to deploy successfully. This is useful because not all the API connectors may be relevant to your use case - so this approach allows you to easily remove those connections from the provisioned logic app, instead of being forced to create all the API connectors prior to being able to provision the Logic App. This also reduces the number of parameters (e.g. API keys) needed at deploy time. This means that you can create the API resources after the fact and just configure the connection in the appropriate Logic App step.
            
> * 4. Create the API connection resources you want to keep (and remove the ones that you don't need from the provisioned Logic App)
        e.g.
            Twitter - OAuth
            Azure Blob
            Sql Server
            Cognitive Services - Text Analytics
            Cognitive Services - Language Translator
            Cognitive Services - Content Moderation
            Office365

> * Thats it! You are set to run your Logic App and do some Sentiment Analysis!