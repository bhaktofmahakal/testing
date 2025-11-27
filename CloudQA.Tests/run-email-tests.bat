@echo off
REM Run only Email field tests
echo ========================================
echo Running Email Field Tests
echo ========================================
dotnet test --filter "Category=Email" --settings .runsettings --verbosity normal
pause
