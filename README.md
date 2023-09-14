# Reenbit Task Test 

![image](https://github.com/olehkavetskyi/ReenbitTest/assets/110283090/4a97653c-bfd0-45e2-89f0-b45ac9e62e39)

## Functionality

This app represts a form with two input fieds. The First one for a file with .docx extencsion and the second one for an email. After user click a button "Upload" form data will send to an API and then the file will be uploaded to an Azure Blob Storage container. After it an  Azure Function will be triggered and send a message using SendGrid to the email specified in the form. 

## Techlogies

1. ASP.NET 7
2. Azure Blob Storage
3. SendGrid
4. Angular 16
5. Azure Function

## Unit Tests

This app uses xUnit framework to test key functionalities

![image](https://github.com/olehkavetskyi/ReenbitTest/assets/110283090/4af60847-1098-46c8-8a6d-98afeb6e8483)

## Links

1. [A deployed website  ](https://reenbittestclient.azurewebsites.net/)
