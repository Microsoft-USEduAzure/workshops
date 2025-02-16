
# Use Power BI Desktop to create a report on COVID-19 cases in the US

## Part 1 - Task List

- [Get data from web data source](#Get-data-from-web-data-source)
- [Transform and prepare data](#Transform-and-prepare-data)

### Get data from web data source

1. Open Power BI Desktop.  Click on the **image** icon to create a new data set:
![image](media/image001.png?raw=true)
1. Type **web** to filter the data source types.  Click on **Web**, then click **Connect**:
![image](media/image002.png?raw=true)
1. Copy and paste the URL for the **USAFacts - Confirmed Cases** data set, click **OK**
![image](media/image003.png?raw=true)
    | Data Set Description | URL  |
    |----------------------|------|
    |**USAFacts - Confirmed Cases**  | <https://usafactsstatic.blob.core.windows.net/public/data/covid-19/covid_confirmed_usafacts.csv> |
    |**USAFacts - Deaths** | <https://usafactsstatic.blob.core.windows.net/public/data/covid-19/covid_deaths_usafacts.csv>|
1. When accessing data from the web, you may be prompted for authentication.  The USAFacts data set can be accessed using anonymous access.  Click on **Anonymous**, then **Connect** to complete the connection:
![image](media/image003a.png?raw=true)
1. A preview of the **USAFacts - Confirmed Cases** data set should appear, click **Transform Data**:
![image](media/image004.png?raw=true)

### Transform and prepare data

1. The **Power Query Editor** will appear and can be used to transform the raw data imported from the USAFacts web site.  Notice that the first row of the data set contains the column hearders.  Click **Use First Row as Headers**:
![image](media/image005.png?raw=true)
1. Notice that there are rows with a **countyFIPS** value of zero that need to be filtered out of the data set.  
![image](media/image006.png?raw=true)
To filter rows based on the **countyFIPS** value, click on the **filter button** to the right of the column header.  Highlight **Number Filters** and click **Greater Than...**:
![image](media/image007.png?raw=true)
Enter **0** to keep rows where **countyFIPS** is greater than zero, click **OK**:
![image](media/image008.png?raw=true)
1. Notice that the data for each date is stored in a seperate column.  In order to create measures that aggregate row values by State and by County by Date, the date columns need to be unpivoted into rows.
![image](media/image009.png?raw=true)
To unpivot the date columns, click on the column header for the first date column 2020-01-22).  Scroll all the way to the last column on the right and while holding down the **Shift** key, click on the last column header (2021-08-16 when this screen shot was taken):
![image](media/image010.png?raw=true)
Click on the **Transform** page, then click on the **Unpivot Columns icon** and select **Unpivot Columns**:
![image](media/image011.png?raw=true)
1. After the date columns are unpivoted, there will be an **Attribute** column containing the dates and a **Value** column containing the **Confirmed Cases** for each row.  Right click on the **Attribute** column header and rename the column **Date**:
![image](media/image012.png?raw=true)
1. Next, change the data type of the **Date** column by clicking to the left of the column header on **"ABC"**:
![image](media/image013.png?raw=true)
1. Rename the **Value** column to **Confirmed Cases**:
![image](media/image014.png?raw=true)
1. Notice as the data has been transformed, each step is captured in the **APPLIED STEPS** listing.  You can select on any of the applied steps to view the state of the data set after all of the steps before and including the selected step have been applied:
![image](media/image015.png?raw=true)
1. To add the second data set containing deaths by State by County by Date, duplicate the **covid_confirmed_usafacts** query.  Right click on the **covid_confirmed_usafacts** query and select **Duplicate**:

    ![image](media/image016.png?raw=true)
1. Right click on the **covid_confimed_usafacts (2)** query and select **Rename**.  Rename the query **covid_deaths_usafacts**:

    ![image](media/image017.png?raw=true)
1. Use the **APPLIED STEPS** list to modify the data source to import the file containing data on deaths.  Click on the **Cog icon** to the right of the **Source** step.  Change the URL to <https://usafactsstatic.blob.core.windows.net/public/data/covid-19/covid_deaths_usafacts.csv> and click **OK**:

    ![image](media/image018.png?raw=true)
1. Delete the last step, which renamed the **Value** column to **Confirmed Cases**:
![image](media/image019.png?raw=true)
1. Rename the **Value** column to **Deaths**:

    ![image](media/image020.png?raw=true)
1. Merge the **Confirmed Cases** and **Deaths** queries into a single table.  Select **Merge Queries**, then click on **Merge Queries as New**:
![image](media/image021.png?raw=true)
1. The **Merge** dialog will appear.  Use the drop downs to set the tables to **covid_confirmed_usafacts** and **covid_deaths_usafacts**.  Holding the **CTRL** key select **countyFIPS**, **State** and **Date** in that order for each of the tables to be merged:
![image](media/image022.png?raw=true)
1. When combining data from multiple tables, you may be prompted to set privacy levels for the combined data set.  Select **Public**, then click **Save**:

    ![image](media/image022a.png)
1. Once the privacy levels have been set, Click **OK**:
![image](media/image022b.png?raw=true)
1. Once the tables are merged, scroll all the way to the last column on the right.  The column name will be **covid_deaths_usafacts** and the data in the column will be labled **Table**.  Click on the **Expand icon** to the right of the column name.  Unselect all columns, except **Deaths**.  Make sure the **Use original column name as prefix** checkbox is unchecked and click **OK**
![image](media/image022c.png?raw=true)
1. Rename the new merged query table **US Covid** and click on **Close & Apply**:

    ![image](media/image023.png?raw=true)
1. The **Power Query Editor** will close and the queries will be executed to import data into the internal tabular model:
![image](media/image024.png?raw=true)

## *You have completed Part 1 - Use Power BI Desktop workshop*

## [Back to Syllabus](readme.md)
