To run the application:
    In the root of application in command line enter: "dotnet run --p C:\globalX\unsorted-names-list.txt"
    --p is for refering to the file that you want to sort.
    you can configure the path of output in "appsetting.json"
    "FakeNames" configuration should be in place if you want to run the unit tests.

Application has health check API, you can change the port in "appsettings.json"
Health check API is: http://localhost:8000/hc
To check the liveness of application: http://localhost:8000/liveness
Application will log everything in the "GlobalX.Coding.Assessment\bin\Debug\netcoreapp3.1\" you can configure this path in "appsettings.json"
