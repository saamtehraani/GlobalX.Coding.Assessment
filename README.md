# GlobalX.Coding.Assessment

<h4>To run the application: </h4>
<ul>
    <li>In the root of application in command line enter: "dotnet run --p C:\globalX\unsorted-names-list.txt"</li>
    <li>--p is for refering to the file that you want to sort.</li>
    <li>You can configure the path of output in "appsetting.json"</li>
    <li>"FakeNames" configuration should be in place if you want to run the unit tests.</li>
</ul>

<ul>
<li>Application has health check API, you can change the port in "appsettings.json"</li>
<li>Health check API is: http://localhost:8000/hc</li>
<li>To check the liveness of application: http://localhost:8000/liveness</li>
<li>Application will log everything in the "GlobalX.Coding.Assessment\bin\Debug\netcoreapp3.1\" you can configure this path in "appsettings.json"</li>
</ul>
