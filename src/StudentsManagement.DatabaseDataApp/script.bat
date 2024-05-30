@echo off

set OUTPUT_FILE=output.txt

del %OUTPUT_FILE%

for /r %%i in (*.cs *.xaml *.csproj) do (
    echo %%i | findstr /I /C:"\bin\" /C:"\obj\" /C:"\Properties\" > nul
    if errorlevel 1 (
        echo %%i >> %OUTPUT_FILE%
        echo. >> %OUTPUT_FILE%
        type "%%i" >> %OUTPUT_FILE%
        echo. >> %OUTPUT_FILE%
    )
)

echo All .cs, .xaml, and .csproj files content has been written to %OUTPUT_FILE%
