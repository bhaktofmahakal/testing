@echo off
REM Run only First Name field tests
echo ========================================
echo Running First Name Field Tests
echo ========================================
dotnet test --filter "Category=FirstName" --settings .runsettings --verbosity normal
pause
