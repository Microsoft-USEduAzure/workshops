# .NET Core MVC

## Task: Create MVC web application using .NET Core SDK

1. Open VS Code, and go to **Terminal** --> **New Terminal**

    ![](img/01.png)
    
1. In the terminal, execute the following commands:

    ```
    mkdir mytestwebapp
    cd mytestwebapp
    dotnew new mvc
    dotnet build
    dotnet run
    ```

    ![](img/02.png)

    > The code above will create a new directory named `mytestwebapp` which you will navigate into and use the .NET Core CLI to create a new MVC web application. In this case you do not need to specify a project name as it assumes the name of the directory in which the project was created in. From there you will build the application and run it.

1. The application should now be running on `http://localhost:5000`. Open a web browser and enter the URL into the address bar.

    ![](img/03.png)

    > You may get a security warning from your browser, stating that the connection is not private. Click on the **Advanced** and click the *Proceed to localhost (unsafe)* link. This is due to the fact that the SSL certificate is not installed on your PC. 

    ![](img/04.png)

1. You should see a webpage that looks like this

    ![](img/05.png)

1. Go back to VS Code and enter `Ctrl + C` to stop the web application

1. Click on the **Extensions** icon in the left navigation column, enter *azure app service* into the search text box, and click the **Install** button when you find **Azure App Service** by Microsoft

    ![](img/06.png)

    > When prompted, sign into your Azure Account

1. Click on the *Azure* icon in the left navigation column and click on the *upload* button in the **AZURE: APP SERVICE** pane.

    ![](img/07.png)

1. Follow the prompts to upload the web application to the previously created app service, and click the **Deploy** button

    ![](img/08.png)

