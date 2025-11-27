@echo off
REM Run all tests
echo ========================================
echo Running ALL Tests
echo ========================================
dotnet test --settings .runsettings --verbosity normal
pause
