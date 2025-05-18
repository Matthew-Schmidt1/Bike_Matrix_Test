# Bike_Matrix_Test

##Things I want to Improve but don't feel its a requirement

Making an interface for the SQLConnection that includes Dapper methods for test
 - I want to do this so that I can inject a fake SqlConnection and then test happy day scenarios currtly only things that fail validation can be tested.
 - I just wanted to focus on getting the test working and the function app running so I didn't focus on that level of testing. Instead, I did end-to-end testing manually. Thinking of how nice it would be to get it automated.
Making a Database.Test project with tSQLt which is a unit test tool for SQL.
 - I stopped as this was getting into required system database tables and other weird stuff.
    - I think this is because I forgot to add the master.dacpac in the SQL project.

Make a Make and Model Table:
 - I feel this will help the performance of the bike table and then we will just have a table of INT so lots of rows on a page. so if we went full production we can just say give me every bike of x and since we are dealing with a small row size then more will be loaded in memory with faster scan speed
    - It's just a way to get extreme optimisation out of the database.
