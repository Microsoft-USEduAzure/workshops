
# Azure Serverless Workshop
**Function Apps, Logic Apps, and Cognitive Services**

###
Note:
For a quickstart deploy of this full solution, go [**HERE**](https://github.com/Microsoft-USEduAzure/workshops/tree/master/serverless/deploy).
###

Are you ready for a taste of serverless?

This tutorial shows you how to use Azure Functions with Logic Apps and Cognitive Services to run language detection, sentiment analysis, language translation, and a profanity filter on Twitter tweets.  

An HTTP-triggered function categorizes tweets as green, yellow, or red based on the sentiment score. An email is sent that includes the original tweet and a translation if applicable when poor sentiment is detected. The tweet is retweeted if it has positive sentiment and profanity is not detected. 

Finally, we will create a Serverless instance of Azure SQL DB to import the data into and visualize the results with Power BI.

![Search for Text Translator](media/serverless-diagram.png)

In this tutorial, you learn how to:

> * Create a Cognitive Services API Resource.
> * Create a function that categorizes tweet sentiment.
> * Create a logic app that connects to Twitter.
> * Add sentiment detection, language detection, and content moderation to the logic app.
> * Translate text in the logic app
> * Connect the logic app to the function.
> * Send an email & retweet based on the response from the function.
> * Save data into a serverless database.
> * Visualize data with Power BI



## Prerequisites
+ An Azure subscription
+ An active [Twitter](https://twitter.com/) account. 
+ An [Outlook.com](https://outlook.com/) or [Office 365 Outlook](https://outlook.office.com) for account (for sending notifications).


## Steps
- [Create Cognitive Services Resources](./create-cognitive-services-resources.md)
- [Create a Function App](./create-a-function-app.md)
- [Create a Logic App](./create-a-logic-app.md)
- [Test the Workflow](./test-the-workflow.md)
- [Create Serverless SQL Database](./create-serverless-sql.md)
- [Configure & Access your SQL Database](./sql-database-access.md)
- [Save Twitter Sentiment to SQL](./sql-save-twitter-to-sql.md)
- [Visualize Data](./visualize-data.md)
- [Clean up](./clean-up.md)
