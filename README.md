# Web API for BDSA 2021 project



## Code analysis 

To see a report on static code analysis, go to [SonarCloud](https://sonarcloud.io/summary/overall?id=wegger-BDSA2021_webapi "SonarCloud dashboard"). The analysis is computed from the main branch - only direct pushes and merge requests to the main branch will generate a new SonarCloud report.  

## Running the web API

This guide assumes, that you are running the application with Ubuntu in WSL2.
Before executing the run script, make sure your Docker Daemon is running, and that ports `5001`, `5000`, `1433` are free.

Start in the root directory and run the following commands :

```bash 
cd scripts/
```

```bash 
bash run
```

Or, 

```bash 
chmod +x run
```

and then 

```bash 
./run
```

Once you have killed the application, you can run the cleanup script in the same folder in order to kill the containers.
